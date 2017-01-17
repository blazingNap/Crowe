using Crowe.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
//using System.Web.Mvc;

namespace CroweWebAPI.Filters
{
    public class ProductsReversedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                List<Category> _articles = objectContent.Value as List<Category>;
                if (_articles != null && _articles.Count > 0)
                {
                    _articles.Reverse();
                }
            }
        }
    }
}