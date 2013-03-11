using System;
using System.Web;
using System.Web.Mvc;

namespace CodeMasteryDemo.Helpers
{
    public static class MobileHelpers
    {
        public static MvcHtmlString MobileClass(this HtmlHelper helper)
        {
            string @class = IsMobileDevice? "mobile" : "";
            return new MvcHtmlString(@class);
        }

        public static string MobileImageContent(this UrlHelper helper, string largeImgRelativePath)
        {
            if (!IsMobileDevice)
                return helper.Content(largeImgRelativePath);

            int extensionDotIndex = largeImgRelativePath.LastIndexOf('.');
            string smallImgRelativePath = largeImgRelativePath.Insert(
                extensionDotIndex, "-small");
            
            return helper.Content(smallImgRelativePath);
        }

        /// <summary>
        /// Mobile device check enhanced by 51degrees.mobi
        /// </summary>
        private static bool IsMobileDevice
        {
            get { return HttpContext.Current.Request.Browser.IsMobileDevice; }
        }
    }
}