using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tripeace.Application.Areas.Admin.ViewModels.Account;
using Tripeace.Domain.Consts;
using Tripeace.Service.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tripeace.Service.Exceptions;
using Tripeace.Application.MVC;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Tripeace.Service.DTO.Account;

namespace Tripeace.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Roles = "GameMaster, God")]
    public class AccountController : BaseController<AccountController>
    {
        private IAccountService _accountService;

        public AccountController(
            IAccountService accountService,
            IStringLocalizer<AccountController> localizer,
            ILogger<AccountController> logger)
            : base(localizer, logger)
        {
            _accountService = accountService;
        }

        public async Task<ActionResult> List(
            int? pageNumber,
            string Query = "")
        {
            var model = new ListModel();
            model.Data = await _accountService.GetAccountList(pageNumber, Query);
            model.Paging.CurrentPage = pageNumber ?? 1;
            model.Paging.ItemsPerPage = ServerInfo.ItemsPerPage;
            model.Paging.TotalItems = model.Data.TotalResults;
            model.Query = Query;

            return View("List", model);
        }
        
        [HttpGet]
        [Route("admin/[controller]/Edit")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var dto = await _accountService.GetAccountToAdminEdit(id);
                var model = Mapper<AccountToAdminEditDTO, EditModel>(dto);

                return View(model);
            }
            catch (InvalidIdException)
            {
                // id of an account that does not exist
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error on Admin/Account/Edit");
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [Route("admin/[controller]/Edit")]
        [Authorize(Roles = "GameMaster, God")]
        public async Task<ActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = Mapper<EditModel, AccountToAdminEditDTO>(model);

                try
                {
                    await _accountService.SetAccountToAdminEdit(dto);
                }
                catch (InvalidIdException)
                {
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
                await _accountService.LockAccount(id);
            }
            catch (InvalidIdException)
            {
                // id of an account that does not exist (dows not need to log, just an Admin can try to cheat lol)
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
                await _accountService.UnlockAccount(id);
            }
            catch (InvalidIdException)
            {
                // id of an account that does not exist (dows not need to log, just an Admin can try to cheat lol)
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

        //[HttpGet]
        //[Authorize(Roles = "God")]
        //public ActionResult Delete(int id)
        //{
        //    var user = RepoCommerce.GetUser(id);

        //    if (user == null)
        //    {
        //        ModelState.AddModelError(
        //            "user",
        //            Messages.UnableToFindUser);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (!String.IsNullOrEmpty(user.Avatar))
        //        {
        //            var path = Server.MapPath("~/Content/Images/Site/Users/Avatar/");
        //            ImageService.DeleteImage(path, user.Avatar, true);
        //        }

        //        Security.DeleteUser(user.UserName);
        //        RepoCommerce.DeleteUser(user);
        //        RepoCommerce.CommitChanges();

        //        SuccessMessage(Messages.UserSuccessfulDeleted);
        //    }

        //    return RedirectToAction("List");
        //}
    }
}
