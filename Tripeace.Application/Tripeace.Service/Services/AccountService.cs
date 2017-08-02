using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Service.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using Tripeace.Domain.Entities;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tripeace.Domain.Consts;
using Microsoft.EntityFrameworkCore;

namespace Tripeace.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IServerRepository _serverRepository;
        private readonly UserManager<AccountIdentity> _userManager;
        private readonly SignInManager<AccountIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(
            IServerRepository serverRepository,
            UserManager<AccountIdentity> userManager,
            SignInManager<AccountIdentity> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _serverRepository = serverRepository;
        }

        public async Task<IEnumerable<string>> TryLogin(LoginDTO data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Account, data.Password, data.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return null;
            }
            if (result.RequiresTwoFactor)
            {
                throw new RequiresConfirmationException();
            }
            if (result.IsLockedOut)
            {
                throw new LockedAccountException();
            }

            throw new InvalidLogInAttemptException();
        }

        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<string>> TryRegisterAccount(RegisterDTO data)
        {
            if (await _serverRepository.GetAccountByName(data.AccountName) != null)
            {
                throw new AccountInUseException();
            }

            if (await _serverRepository.GetAccountByName(data.Email) != null)
            {
                throw new EmailInUseException();
            }

            var user = new AccountIdentity
            {
                News = data.AgreeReciveNews,
                UserName = data.AccountName,
                Account = new Account
                {
                    Creation = DateTime.Now,
                    Email = data.Email,
                    Name = data.AccountName,
                    Password = GetHash(data.Password)
                }
            };

            var result = await _userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AccountType.Player.ToString());

                // todo in future: email autentication

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                await _signInManager.SignInAsync(user, isPersistent: false);
                user.LockoutEnabled = true;

                return null;
            }
            else
            {
                return result.Errors.Select(x => x.Description);
            }
        }

        public async Task<IndexDTO> GetPlayerInfoIndex(string accountName)
        {
            var user = await _serverRepository.GetAccountByName(accountName);

            return new IndexDTO()
            {
                AccountName = user.Name,
                Email = user.Email,
                Characters = user.Players.Select(x => new IndexPlayerDTO()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Level = x.Level,
                    Name = x.Name,
                    Sex = x.Sex,
                    Vocation = x.Vocation
                })
            };
        }

        // todo move to a security service
        private static string GetHash(string input)
        {
            return string.Join("", (SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input))).Select(x => x.ToString("X2")).ToArray());
        }

        public async Task<int> GetCharactersQuantity(string accountName)
        {
            var account = await _serverRepository.GetAccountByName(accountName);
            return account.Players.Count;
        }

        public async Task<AccountListDTO> GetAccountList(int? pageNumber, string searchKey)
        {
            var users = _serverRepository.GetAccounts();

            if (!String.IsNullOrEmpty(searchKey))
            {
                searchKey = searchKey.ToLower();

                users = users.Where(x =>
                    x.Name.ToLower().Contains(searchKey) ||
                    x.Email.ToLower().Contains(searchKey) ||
                    x.Players.Any(y => y.Name.ToLower().Contains(searchKey)));
            }
            
            var currentPageNum = pageNumber.HasValue ? pageNumber.Value : 1;
            var offset = (ServerInfo.ItemsPerPage * currentPageNum) - ServerInfo.ItemsPerPage;

            var model = new AccountListDTO();
            model.TotalResults = await users.CountAsync();

            var result = await users
                .Skip(offset)
                .Take(ServerInfo.ItemsPerPage)
                .ToListAsync();

            foreach (var account in result)
            {
                var accountListItem = new AccountListItemDTO()
                {
                    Id = account.Id,
                    AccountName = account.Name,
                    Email = account.Email,
                    Characters = account.Players.Select(x => x.Name),
                    IsLocked = (await _userManager.IsLockedOutAsync(account.AccountIdentity)),
                    Role = (await _userManager.GetRolesAsync(account.AccountIdentity)).FirstOrDefault()
                };

                model.Accounts.Add(accountListItem);
            }

            return model;
        }

        public async Task LockAccount(int id)
        {
            var account = await _serverRepository.GetAccount(id);

            if (account == null)
            {
                throw new InvalidIdException();
            }

            try
            {
                account.AccountIdentity.LockoutEnabled = true;

                while (!(await _userManager.IsLockedOutAsync(account.AccountIdentity)))
                {
                    await _signInManager.PasswordSignInAsync(account.Name, "locktheuser010203" + DateTime.Now.ToString("ddMMyyysszzf"), false, true);
                }
            }
            catch (LockedAccountException)
            {
                account.AccountIdentity.LockoutEnd = DateTime.MaxValue;
            }

            return;
        }

        public async Task UnlockAccount(int id)
        {
            var account = await _serverRepository.GetAccount(id);

            if (account == null)
            {
                throw new InvalidIdException();
            }

            account.AccountIdentity.LockoutEnd = null;
        }
    }
}
