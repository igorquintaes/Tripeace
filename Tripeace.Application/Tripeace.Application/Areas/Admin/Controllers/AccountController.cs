using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tripeace.Application.Areas.Admin.ViewModels.Account;
using Tripeace.Domain.Consts;
using Tripeace.Service.Services.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Service.Exceptions;
using Tripeace.Application.MVC;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Tripeace.Service.DTO.Account;
using Tripeace.Application.Areas.Admin.ViewModels.Shared;
using System.Globalization;
using cloudscribe.Web.Pagination;

namespace Tripeace.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Roles = "GameMaster, God")]
    public class AccountController : BaseController<AccountController>
    {
        private IAccountService _accountService;
        private IBanService _banService;

        public AccountController(
            IAccountService accountService,
            IBanService banService,
            IStringLocalizer<AccountController> localizer,
            ILogger<AccountController> logger)
            : base(localizer, logger)
        {
            _accountService = accountService;
            _banService = banService;
        }

        public async Task<ActionResult> List(
            int? pageNumber,
            string Query = "")
        {
            var data = await _accountService.GetAccountList(pageNumber, Query);

            var model = new ListViewModel()
            {
                Data = data,
                Query = Query,
                Paging = new PaginationSettings()
                {
                    CurrentPage = pageNumber ?? 1,
                    ItemsPerPage = ServerInfo.ItemsPerPage,
                    TotalItems = data.TotalResults
                }
            };

            return View("List", model);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var dto = await _accountService.GetAccountToAdminEdit(id, User.Identity.Name);
                var model = Mapper<AccountToAdminEditDTO, EditViewModel>(dto);

                return View(model);
            }
            catch (InvalidIdException)
            {
                // id of an account that does not exist
                return RedirectToAction("List");
            }
            catch (NoAuthorizationException)
            {
                // Game Master trying to cheat
                LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Edit:Get");
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error on Admin/Account/Edit");
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = Mapper<EditViewModel, AccountToAdminEditDTO>(model);
                dto.AccountWhoRequested = User.Identity.Name;

                try
                {
                    await _accountService.SetAccountToAdminEdit(dto);
                    ViewData["SuccessMessage"] = _localizer["SuccessMessage"];
                }
                catch (InvalidIdException)
                {
                    return RedirectToAction("List");
                }
                catch (NoAuthorizationException)
                {
                    // Game Master trying to cheat
                    LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Edit:Post");
                    return RedirectToAction("List");
                }
                catch (EmailInUseException)
                {
                    AddModelErrors(_localizer["EmailInUse"]);
                    return View(model);
                }
                catch (AccountInUseException)
                {
                    AddModelErrors(_localizer["AccountInUse"]);
                    return View(model);
                }
                catch (Exception ex)
                {
                    LogError(ex, "Error on /Admin/Account/Edit");
                    AddModelErrors(_localizer["UnknownErrorContactAnAdmin"]);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Lock(int id)
        {
            try
            {
                await _accountService.LockAccount(id, User.Identity.Name);
            }
            catch (InvalidIdException)
            {
                // id of an account that does not exist (dows not need to log, just an Admin can try to cheat lol)
                return RedirectToAction("List");
            }
            catch (NoAuthorizationException)
            {
                // Game Master trying to cheat
                LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Lock:Get");
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Unknow error
                LogError(ex, "Error on Admin/Account/Lock");
                return RedirectToAction("List");
            }

            // Success
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Unlock(int id)
        {
            try
            {
                await _accountService.UnlockAccount(id, User.Identity.Name);
            }
            catch (InvalidIdException)
            {
                // id of an account that does not exist (dows not need to log, just an Admin can try to cheat lol)
                return RedirectToAction("List");
            }
            catch (NoAuthorizationException)
            {
                // Game Master trying to cheat
                LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Unlock:Get");
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Unknow error
                LogError(ex, "Error on Admin/Account/Lock");
                return RedirectToAction("List");
            }

            // Unlocked
            return RedirectToAction("List");
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Ban([FromBody]BanViewModel model)
        {
            var ajaxReturn = new AjaxFeedbackViewModel();

            try
            {
                var dto = Mapper<BanViewModel, BanDTO>(model);
                dto.AdminAccount = User.Identity.Name;
                dto.ExpiresAt = Convert.ToDateTime(model.Date, CultureInfo.CurrentCulture.DateTimeFormat);

                await _banService.BanAccount(dto);
            }
            catch (InvalidIdException)
            {
                // Id of an account that does not exist. 
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["InvalidAttempt"];

                return Json(ajaxReturn);
            }
            catch (NoAuthorizationException)
            {
                // Game Master trying to cheat
                LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Ban:Post");
                return RedirectToAction("List");
            }
            catch (InvalidAdminAccountException)
            {
                // Invalid logged-in admin account. 
                // I don't think is possible throw this error by this controller
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["InvalidAttempt2"];

                return Json(ajaxReturn);
            }
            catch (RequiredAdminCharacterException)
            {
                // Need a god character or game master character to do it.
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["YouNeedAGodCharacter"];

                return Json(ajaxReturn);
            }
            catch (AccountAlreadyBannedException)
            {
                // Can not ban, because the target account is already banned.
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["AlreadyBanned"];

                return Json(ajaxReturn);
            }
            catch (Exception ex)
            {
                // Unknow error
                LogError(ex, "Error on Admin/Account/Ban");

                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["UnknowError"];
                return Json(ajaxReturn);
            }

            // Success
            ajaxReturn.Title = _localizer["Success"];
            ajaxReturn.Message = _localizer["BannedSuccefully"];
            return Json(ajaxReturn);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Unban([FromBody]UnbanViewModel model)
        {
            var ajaxReturn = new AjaxFeedbackViewModel();

            try
            {
                var dto = Mapper<UnbanViewModel, UnbanDTO>(model);
                dto.AccountWhoRequested = User.Identity.Name;

                await _banService.UnbanAccount(dto);
            }
            catch (InvalidIdException)
            {
                // Id of an account that does not exist. 
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["InvalidAttempt"];

                return Json(ajaxReturn);
            }
            catch (NoAuthorizationException)
            {
                // Game Master trying to cheat
                LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Unban:Post");
                return RedirectToAction("List");
            }
            catch (AccountIsNotBannedException)
            {
                // Trying to unban a unbanned account
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["AccountIsAlreadyUnbanned"];

                return Json(ajaxReturn);
            }
            catch (Exception ex)
            {
                // Unknow error
                LogError(ex, "Error on Admin/Account/Unban");

                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["UnknowError"];
                return Json(ajaxReturn);
            }

            // Success
            ajaxReturn.Title = _localizer["Success"];
            ajaxReturn.Message = _localizer["UnbannedSuccefully"];
            return Json(ajaxReturn);
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "God")]
        public async Task<ActionResult> Delete([FromBody]int id)
        {
            var ajaxReturn = new AjaxFeedbackViewModel();
            try
            {
                await _accountService.DeleteAccount(id, User.Identity.Name);
            }
            catch (InvalidIdException)
            {
                // Id of an account that does not exist. 
                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["InvalidAttempt"];

                return Json(ajaxReturn);
            }
            catch (NoAuthorizationException)
            {
                // Game Master trying to cheat
                LogUnauthorizedAccess(User.Identity.Name, "Admin/Account/Delete:Post");
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Unknow error
                LogError(ex, "Error on Admin/Account/Delete");

                ajaxReturn.Title = _localizer["Error"];
                ajaxReturn.Message = _localizer["UnknowError"];
                return Json(ajaxReturn);
            }

            // Success
            ajaxReturn.Title = _localizer["Success"];
            ajaxReturn.Message = _localizer["AccountDeletedSuccefully"];
            return Json(ajaxReturn);
        }
    }
}
