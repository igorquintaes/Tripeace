using Tripeace.Domain.Contracts.Repositories;
using Tripeace.Service.Services.Server.Contracts;
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

namespace Tripeace.Service.Services.Server
{
    public class AccountService : ServiceBase<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBanService _banService;
        private readonly UserManager<AccountIdentity> _userManager;
        private readonly SignInManager<AccountIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(
            IAccountRepository accountRepository,
            IBanService banService,
            UserManager<AccountIdentity> userManager,
            SignInManager<AccountIdentity> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _banService = banService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accountRepository = accountRepository;
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
            if (await _accountRepository.GetByName(data.AccountName) != null)
            {
                throw new AccountInUseException();
            }

            if (await _accountRepository.GetByEmail(data.Email) != null)
            {
                throw new EmailInUseException();
            }

            var account = new Account
            {
                Creation = DateTime.Now,
                Email = data.Email,
                Name = data.AccountName,
                Password = GetHash(data.Password)
            };

            await _accountRepository.Insert(account);

            var user = new AccountIdentity
            {
                News = data.AgreeReciveNews,
                UserName = data.AccountName,
                Account = account
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
            var user = await _accountRepository.GetByName(accountName);

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

        // TODO move to a security service
        private static string GetHash(string input)
        {
            return string.Join("", (SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input))).Select(x => x.ToString("X2")).ToArray());
        }

        public async Task<int> GetCharactersQuantity(string accountName)
        {
            var account = await _accountRepository.GetByName(accountName);
            return account.Players.Count;
        }

        public async Task<AccountListDTO> GetAccountList(int? pageNumber, string searchKey)
        {
            var users = _accountRepository.Query();

            if (!String.IsNullOrEmpty(searchKey))
            {
                searchKey = searchKey.ToLower();

                users = users.Where(x =>
                    x.Name.ToLower().Contains(searchKey) ||
                    x.Email.ToLower().Contains(searchKey) ||
                    x.Players.Any(y => y.Name.ToLower().Contains(searchKey)));
            }
            
            var currentPageNum = pageNumber ?? 1;
            var offset = (ServerInfo.ItemsPerPage * currentPageNum) - ServerInfo.ItemsPerPage;

            var model = new AccountListDTO()
            {
                TotalResults = await users.CountAsync()
            };

            var result = await users
                .Skip(offset)
                .Take(ServerInfo.ItemsPerPage)
                .ToListAsync();

            foreach (var account in result)
            {
                var isAccountBanned = await _banService.IsBanned(account.Id);
                var isAccountLocked = await _userManager.IsLockedOutAsync(account.AccountIdentity);
                var AccountRole = (await _userManager.GetRolesAsync(account.AccountIdentity)).Single();
                var banReason = isAccountBanned
                        ? account.AccountBan.Reason
                        : String.Empty;

                var accountListItem = new AccountListItemDTO()
                {
                    Id = account.Id,
                    AccountName = account.Name,
                    Email = account.Email,
                    Characters = account.Players.Select(x => x.Name),
                    IsLocked = isAccountLocked,
                    Role = AccountRole,
                    IsBanned = isAccountBanned,
                    IsBannedReason = banReason
                };

                model.Accounts.Add(accountListItem);
            }

            return model;
        }

        public async Task<AccountToAdminEditDTO> GetAccountToAdminEdit(int id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
            {
                throw new InvalidIdException();
            }

            return new AccountToAdminEditDTO()
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                ReciveNews = account.AccountIdentity.News
            };
        }

        public async Task SetAccountToAdminEdit(AccountToAdminEditDTO dto)
        {
            var account = await _accountRepository.GetById(dto.Id);

            if (account == null)
            {
                // Probably an invalid or outdated data, or maybe a someone trying to exploit the system.
                throw new InvalidIdException();
            }

            var checkAccount = await _accountRepository.GetByName(dto.Name);
            if (checkAccount != null && 
                checkAccount.Id != dto.Id)
            {
                // If tried to change to a account name that already exist
                // and that is different from the edited account itself
                throw new AccountInUseException();
            }

            var checkEmail = await _accountRepository.GetByEmail(dto.Email);
            if (checkEmail != null &&
                checkEmail.Id != dto.Id)
            {
                // If tried to change to a e-mail that already exist
                // and that is different from the edited account itself
                throw new EmailInUseException();
            }

            account.Name = dto.Name;
            account.Email = dto.Email;
            account.AccountIdentity.News = dto.ReciveNews;
            account.AccountIdentity.UserName = dto.Name;

            await _userManager.UpdateAsync(account.AccountIdentity);
            await _accountRepository.Update(account);
        }

        public async Task LockAccount(int id)
        {
            var account = await _accountRepository.GetById(id);

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
            }

            account.AccountIdentity.LockoutEnd = DateTime.Now.AddYears(10);
            await _userManager.UpdateAsync(account.AccountIdentity);

            return;
        }

        public async Task UnlockAccount(int id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
            {
                throw new InvalidIdException();
            }

            account.AccountIdentity.LockoutEnd = null;
            await _userManager.UpdateAsync(account.AccountIdentity);
        }

        public async Task DeleteAccount(int id)
        {
            var account = await _accountRepository.GetById(id);

            if (account == null)
            {
                throw new InvalidIdException();
            }

            await _accountRepository.Delete(account);
        }
    }
}
