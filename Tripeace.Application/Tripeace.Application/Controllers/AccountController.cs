using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Tripeace.Application.MVC;
using Tripeace.Application.ViewModels.Account;
using Tripeace.Service.DTO.Account;
using Tripeace.Service.DTO.Character;
using Tripeace.Service.Exceptions;
using Tripeace.Service.Services.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.Controllers
{
    [Authorize(Roles = "Player, Tutor, SeniorTutor, GameMaster, God")]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;
        private readonly ICharacterService _characterService;

        public AccountController(
            IAccountService accountService,
            ICharacterService characterService,
            IStringLocalizer<AccountController> localizer,
            ILogger<AccountController> logger)
            : base(localizer, logger)
        {
            _accountService = accountService;
            _characterService = characterService;
        }

        //
        // GET: /Account
        [HttpGet]
        public async Task<IActionResult> Index(bool newAccount = false)
        {
            try
            {
                var dto = await _accountService.GetPlayerInfoIndex(User.Identity.Name);
                var data = new IndexViewModel()
                {
                    AccountName = dto.AccountName,
                    Email = dto.Email,
                    IsNewAccount = newAccount,
                    Characters = dto.Characters.Select(x => new IndexPlayerViewModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Level = x.Level,
                        Name = x.Name,
                        Sex = x.Sex,
                        Vocation = x.Vocation
                    })
                };

                return View(data);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error on /Account/LogIn");
                ViewData["Error"] = _localizer["UnknownErrorContactAnAdmin"];
            }

            return RedirectToAction(nameof(HomeController.Index));
        }

        //
        // GET: /Account/LogIn
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl = null)
        {
            if (!String.IsNullOrEmpty(User?.Identity?.Name))
                return RedirectToAction("Index");

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/LogIn
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginViewModel model, string returnUrl = null)
        {
            if (!String.IsNullOrEmpty(User?.Identity?.Name))
                return RedirectToAction("Index");

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var dto = Mapper<LoginViewModel, LoginDTO>(model);

                try
                {
                    var errors = await _accountService.TryLogin(dto);
                    if (errors == null || !errors.Any())
                    {
                        LogInformation("User Logged In. Account: " + dto.Account);
                        return RedirectToReturnUrl(returnUrl);
                    }

                    AddModelErrors(errors);
                }
                catch (InvalidLogInAttemptException)
                {
                    AddModelErrors(_localizer["InvalidAttempt"]);
                }
                catch (LockedAccountException)
                {
                    AddModelErrors(_localizer["AccountLocked"]);
                }
                catch (RequiresConfirmationException)
                {
                    // todo
                    throw new NotImplementedException();
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error on /Account/LogIn");
                    AddModelErrors(_localizer["UnknownErrorContactAnAdmin"]);
                }
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            var account = User.Identity.Name;

            try
            {
                await _accountService.LogOff();
                LogInformation("User Logged Out. Account: " + account);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error on /Account/LogOff");
            }

            return RedirectToAction(nameof(HomeController.Index));
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (!String.IsNullOrEmpty(User?.Identity?.Name))
                return RedirectToAction("Index");

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!String.IsNullOrEmpty(User?.Identity?.Name))
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var dto = Mapper<RegisterViewModel, RegisterDTO>(model);

                try
                {
                    var errors = await _accountService.TryRegisterAccount(dto);
                    if (errors == null || !errors.Any())
                    {
                        LogInformation("New User Registered! Account: " + dto.AccountName);
                        return RedirectToAction("Index", new { newAccount = true });
                    }

                    AddModelErrors(errors);
                }
                catch (AccountInUseException)
                {
                    AddModelErrors(_localizer["AccountInUse"]);
                }
                catch (EmailInUseException)
                {
                    AddModelErrors(_localizer["EmailInUse"]);
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error on /Account/Register");
                    AddModelErrors(_localizer["UnknownErrorContactAnAdmin"]);
                }
            }

            return View(model);
        }

        //
        // GET: /Account/CreateCharacter
        [HttpGet]
        public async Task<IActionResult> CreateCharacter()
        {
            var charactersQuantityInAccount = await _accountService.GetCharactersQuantity(User.Identity.Name);

            if (charactersQuantityInAccount >= 20)
            {
                // Only enter here if player forces a post by http injection or already opened tab
                return RedirectToAction("Index", "Account");
            }

            var model = new CreateCharacterViewModel()
            {
                AllowedVocations = _characterService.GetVocationsOnCreate()
            };

            return View(model);
        }

        //
        // POST: /Account/CreateCharacter
        [HttpPost]
        public async Task<IActionResult> CreateCharacter(CreateCharacterViewModel model)
        {
            var charactersQuantityInAccount = await _accountService.GetCharactersQuantity(User.Identity.Name);

            if (charactersQuantityInAccount >= 20)
            {
                return RedirectToAction("Index", "Account");
            }

            if (ModelState.IsValid)
            {
                var dto = Mapper<CreateCharacterViewModel, CreateCharacterDTO>(model);

                try
                {
                    await _characterService.CreateNewCharacter(dto, User.Identity.Name);
                    LogInformation("New Character Created. Account: " + User.Identity.Name + ", Character: " + dto.Name);

                    return RedirectToAction("Index", "Account");
                }
                catch (CharacterAlreadyRegisteredException)
                {
                    AddModelErrors(_localizer["CharacterAlreadyRegistered"]);
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error on /Account/CreateCharacter");
                    AddModelErrors(_localizer["UnknownErrorContactAnAdmin"]);
                }
            }

            model.AllowedVocations = _characterService.GetVocationsOnCreate();

            return View(model);
        }

        //
        // GET: /Account/EditCharacter
        [HttpGet]
        public async Task<IActionResult> EditCharacter(int id)
        {
            try
            {
                var dto = await _characterService.GetCharacterEdit(id, User.Identity.Name);
                var model = Mapper<EditCharacterDTO, EditCharacterViewModel>(dto);

                return View(model);
            }
            catch (TryingToAccessOtherAccountException)
            {
                // If someone try to access a character of other account
                LogInformation(" Trying to access other persons character edit page. Account: " + User.Identity.Name);
                return RedirectToAction("Index");
            }
            catch (InvalidIdException)
            {
                // id of a character that does not exist
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error on /Account/EditCharacter");
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Account/EditCharacter
        [HttpPost]
        public async Task<IActionResult> EditCharacter(EditCharacterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = Mapper<EditCharacterViewModel, EditCharacterDTO>(model);
                    await _characterService.UpdateCharacter(dto, User.Identity.Name);

                    return View(model);
                }
                catch (TryingToAccessOtherAccountException)
                {
                    // If someone try to access a character of other account
                    LogInformation(" Trying to access other persons character edit page. Account: " + User.Identity.Name);
                    return RedirectToAction("Index");
                }
                catch (InvalidIdException)
                {
                    // id of a character that does not exist
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error on /Account/EditCharacter");
                    AddModelErrors(_localizer["UnknownErrorContactAnAdmin"]);
                }
            }

            return View(model);
        }
    }
}
