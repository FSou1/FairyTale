﻿using System;
using System.Web.Mvc;
using FT.Components.Serializer;
using FT.Components.Serializer.Json;
using log4net;

namespace FT.MvcApp.Filters {
    public class ExceptionHandlerAttribute : HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            var serializer = DependencyResolver.Current.GetService<ISerializer>() 
                ?? new NewtonsoftJsonSerializer();

            var url = filterContext.HttpContext.Request.Url;

            var exception = filterContext.Exception;

            var json = serializer.Serialize(new {
                url,
                exception.Message,
                exception.StackTrace
            });

            LogManager.GetLogger(typeof(ExceptionHandlerAttribute)).Error(json);

            base.OnException(filterContext);
        }
    }
}