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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(ex, "Error on Admin/Account/Edit");
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //[Authorize(Roles = "GameMaster, God")]
        //public ActionResult EditConfirm(UserEditForm input)
        //{
        //    if (input == null)
        //    {
        //        throw new ArgumentNullException("input");
        //    }

        //    if (!TryValidateModel(input))
        //    {
        //        ModelState.AddModelError(
        //            "ConfirmEmail",
        //            Messages.UnexpectedErrorToValidate);
        //    }

        //    var user = RepoCommerce.GetUser(input.Id);

        //    if (ModelState.IsValid && user == null)
        //    {
        //        ModelState.AddModelError(
        //            "ConfirmEmail",
        //            Messages.UnableToFindUser);
        //    }

        //    // Valida e-mail (se ele for alterado)
        //    if (ModelState.IsValid)
        //    {
        //        if ((!String.IsNullOrEmpty(input.ConfirmEmail)) && input.ConfirmEmail != input.Email)
        //            ModelState.AddModelError(
        //                "ConfirmEmail",
        //                Messages.EmailDoesntMatch);
        //    }

        //    // Valida duplicidade de cadastro em outros usuários
        //    if (ModelState.IsValid)
        //    {
        //        var userByEmail = RepoCommerce.GetUserByEmail(input.Email);
        //        if (userByEmail != null && user.Email != userByEmail.Email)
        //        {
        //            ModelState.AddModelError(
        //                "Email",
        //                Messages.EmailAlreadyInUse);
        //        }

        //        var userByName = RepoCommerce.GetUserByUserName(input.UserName);
        //        if ((userByName != null && user.UserName != userByName.UserName) ||
        //            input.UserName == ConfigurationProvider.GetAppSetting(AppSetting.MasterLoginForTests))
        //        {
        //            ModelState.AddModelError(
        //                "UserName",
        //                Messages.UserNameAlreadyInUse);
        //        }
        //    }

        //    // Valida se avatar é imagem
        //    if (ModelState.IsValid)
        //    {
        //        if (!ImageService.IsImage(input.File))
        //            ModelState.AddModelError(
        //                "File",
        //                Messages.FileIsNotAnImage);
        //    }

        //    // Valida tamanho (em espaço em disco) do avatar
        //    if (ModelState.IsValid)
        //    {
        //        if (input.File != null && ImageService.IsValidLenght(input.File, 8 * 1024 * 1024))
        //        {
        //            ModelState.AddModelError(
        //                "File",
        //                string.Format(Messages.ImageSizeBiggerThanExpected, 1));
        //        }
        //    }

        //    // Conclui Edição
        //    if (ModelState.IsValid)
        //    {
        //        if (input.File != null)
        //        {
        //            var image = new Bitmap(Image.FromStream(input.File.InputStream));
        //            var path = Server.MapPath("~/Content/Images/Site/Users/Avatar/");
        //            var imageKey = Guid.NewGuid().ToString() + Path.GetExtension(input.File.FileName);

        //            image = ImageService.ScaleBySquare(image, ImageDimensions.profileAvatar);
        //            ImageService.SaveImage(path, imageKey, image);

        //            image = ImageService.ScaleBySquare(image, ImageDimensions.thumbAvatar);
        //            ImageService.SaveImage(path, imageKey, image, true);

        //            if (!String.IsNullOrEmpty(user.Avatar))
        //            {
        //                path = Server.MapPath("~/Content/Images/Site/Users/Avatar/");
        //                ImageService.DeleteImage(path, user.Avatar, true);
        //            }

        //            user.Avatar = imageKey;
        //        }

        //        if (!String.IsNullOrEmpty(input.ConfirmEmail))
        //            user.Email = input.Email;

        //        user.Quote = input.Quote;
        //        user.UserName = input.UserName;
        //        user.ReciveNews = input.ReciveNews;

        //        Security.ChangeMemberRole
        //            (user,
        //             input.Role.ToString("F"));

        //        RepoCommerce.CommitChanges();

        //        SuccessMessage(Messages.UserSuccessfulUpdated);

        //        return RedirectToAction("List");
        //    }

        //    return View(
        //        "Edit",
        //        input);
        //}

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
