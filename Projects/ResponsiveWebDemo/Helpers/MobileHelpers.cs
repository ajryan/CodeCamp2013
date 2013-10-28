using System.Web;
using System.Web.Mvc;

namespace ResponsiveWebDesignDemo.Helpers
{
    public static class MobileHelpers
    {
        private const string CssClassPostfixForMobileClients = "mobile";
        private const string SmallFilenamePostfix = "-small";

        /// <summary>
        /// Returns the css class postfix of "mobile" for mobile clients 
        /// </summary>
        public static MvcHtmlString MobileClass(this HtmlHelper helper)
        {
            var @class = IsMobileDevice ? CssClassPostfixForMobileClients : string.Empty;
            return new MvcHtmlString(@class);
        }

        /// <summary>
        /// Returns the small or large sized filename postfix for the image path depending upon whether the client
        /// is a mobile device or not 
        /// </summary>
        public static string MobileImageContent(this UrlHelper helper, string largeImgRelativePath)
        {
            if (!IsMobileDevice)
            {
                return helper.Content(largeImgRelativePath);
            }
            int extensionDotIndex = largeImgRelativePath.LastIndexOf('.');
            string smallImgRelativePath = largeImgRelativePath.Insert(extensionDotIndex, SmallFilenamePostfix);
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