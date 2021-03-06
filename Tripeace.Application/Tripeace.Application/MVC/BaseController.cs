﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Tripeace.Application.Controllers;
using System;
using System.Collections.Generic;

namespace Tripeace.Application.MVC
{
    public class BaseController<T> : Controller
    {
        protected readonly IStringLocalizer<T> _localizer;
        private readonly ILogger<T> _logger;

        public BaseController(
            IStringLocalizer<T> localizer,
            ILogger<T> logger)
        {
            _localizer = localizer;
            _logger = logger;
        }

        protected Destiny Mapper<Source, Destiny>(Source model) => 
            new MapperConfiguration(cfg => cfg.CreateMap<Source, Destiny>())
                .CreateMapper()
                .Map<Destiny>(model);

        protected IActionResult RedirectToReturnUrl(string returnUrl) => 
            Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl) as IActionResult
                : RedirectToAction(nameof(HomeController.Index));

        protected void AddModelErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                AddModelErrors(error);
            }
        }

        protected void AddModelErrors(string error) => 
            ModelState.AddModelError(string.Empty, error);

        protected void LogError(Exception ex, string controllerName, string actionName) =>
            LogError(ex, $"Error on /{controllerName}/{actionName}");

        protected void LogError(Exception ex, string message)
        {
            _logger.LogError($"Message: {message}");
            _logger.LogError($"Exception: {ex.Message}");
            _logger.LogError($"StackTrace: {ex.StackTrace}");
            _logger.LogError($"Source{ex.Source}");

            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                _logger.LogError($"Inner StackTrace: {ex.InnerException.StackTrace}");
                _logger.LogError($"Inner Source: {ex.InnerException.Source}");
            }

            LogInformation($"Error. For more Information, check error log. {message}");
        }

        protected void LogInformation(string text) => 
            _logger.LogInformation(text);

        protected void LogUnauthorizedAccess(string userName, string place) => 
            _logger.LogInformation("UNAUTHORIZED ACCESS BLOCKED. Requested by: " + userName + ". Request: " + place);
    }
}
