using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Scriban;
using VirtoCommerce.Storefront.Infrastructure;
using VirtoCommerce.Storefront.Model;
using VirtoCommerce.Storefront.Model.Common;
using VirtoCommerce.Storefront.Model.StaticContent;

namespace VirtoCommerce.Storefront.Controllers
{
    [StorefrontRoute]
    [AllowAnonymous]
    public class DesignerPreviewController : StorefrontControllerBase
    {
        public DesignerPreviewController(IWorkContextAccessor workContextAccessor, IStorefrontUrlBuilder urlBuilder)
            : base(workContextAccessor, urlBuilder)
        {
        }

        [HttpGet("designer-preview")]
        public IActionResult Index()
        {
            WorkContext.Layout = Request.Query["layout"].ToString();
            return View("json-preview", WorkContext);
        }

        [HttpPost("designer-preview/block")]
        //We can't use AntiForgery check here due to IFrame limitations. Browsers don't send cookies from IFrames.
        //[ValidateAntiForgeryToken]
        public IActionResult Block([FromBody] dynamic data)
        {
            var page = new ContentPage
            {
                Content = $"[{data}]"
            };

            WorkContext.CurrentPage = page;
            WorkContext.IsPreviewMode = true;
            var viewName = "json-blocks";

            return PartialView(viewName, WorkContext);
        }


        [HttpPost("designer-preview/template")]
        public IActionResult Template([FromBody] dynamic data)
        {
            var template = new Dictionary<string, object> { { "sections", new[] { data.template } } };

            var model = data.model;

            WorkContext.TemplateConfig = template;
            WorkContext.TemplateModel = model;
            WorkContext.IsPreviewMode = true;
            var viewName = "json-sections";
            return PartialView(viewName, WorkContext);
        }
    }
}
