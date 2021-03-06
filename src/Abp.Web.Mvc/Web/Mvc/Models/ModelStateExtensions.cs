﻿using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Web.Localization;
using Abp.Web.Models;

namespace Abp.Web.Mvc.Models
{
    public static class ModelStateExtensions
    {
        public static MvcAjaxResponse ToMvcAjaxResponse(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return new MvcAjaxResponse();
            }

            var validationErrors = new List<ValidationErrorInfo>();

            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    validationErrors.Add(new ValidationErrorInfo(error.ErrorMessage, state.Key));
                }
            }

            var errorInfo = new ErrorInfo(AbpWebLocalizedMessages.ValidationError)
                            {
                                ValidationErrors = validationErrors.ToArray()
                            };

            return new MvcAjaxResponse(errorInfo);
        }
    }
}
