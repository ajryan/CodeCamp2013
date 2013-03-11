using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeMasteryDemo.Helpers
{
    public static class VideoHelpers
    {
        public static MvcHtmlString Video(
            this HtmlHelper helper,
            IEnumerable<string> sourceUrls,
            string flashFallbackSourceUrl,
            string posterRelativePath,
            bool controls = true,
            bool preload = false,
            string id = null)
        {
            var posterUrl = VirtualPathUtility.ToAbsolute(posterRelativePath);

            var videoTag = new TagBuilder("video");
            var videoAttributes = new
            {
                preload = preload ? "auto" : "none",
                poster = posterUrl
            };
            videoTag.MergeAttributes(videoAttributes);
            if (controls)
                videoTag.Attributes["controls"] = "controls";
            if (!string.IsNullOrWhiteSpace(id))
                videoTag.Attributes["id"] = id;

            var sourceUrlArray = sourceUrls.ToArray();
            for (int sourceIndex = 0; sourceIndex < sourceUrlArray.Length; sourceIndex++)
            {
                var sourceUrl = sourceUrlArray[sourceIndex];

                var sourceTag = new TagBuilder("source");
                sourceTag.Attributes["src"] = sourceUrl;
                // omit type from final source
                if (sourceIndex != sourceUrlArray.Length - 1)
                    sourceTag.Attributes["type"] = "video/" + Path.GetExtension(sourceUrl).Substring(1);

                videoTag.InnerHtml += sourceTag.ToString(TagRenderMode.SelfClosing);
            }

            videoTag.InnerHtml += FlowPlayer(posterUrl, flashFallbackSourceUrl);

            return new MvcHtmlString(videoTag.ToString(TagRenderMode.Normal));
        }

        private static string FlowPlayer(string posterUrl, string sourceUrl)
        {
            var wrapperTag = new TagBuilder("div");
            wrapperTag.Attributes["class"] = "object-wrapper";
            wrapperTag.Attributes["style"] = "position: relative; height: 0px; padding-top: 25px; padding-bottom: 56.25%;";

            var objectTag = new TagBuilder("object");
            var objectAttributes = new
            {
                type = "application/x-shockwave-flash",
                data = _FlowPlayerPath,
                style = "position: absolute; top: 0; left: 0; width: 100%; height: 100%",
                width = "800",
                height = "450"
            };
            objectTag.MergeAttributes(objectAttributes);
            objectTag.InnerHtml += ParamTag("movie", _FlowPlayerPath);
            objectTag.InnerHtml += ParamTag("allowFullScreen", "true");
            objectTag.InnerHtml += ParamTag("wmode", "transparent");
            objectTag.InnerHtml += ParamTag(
                "flashVars",
                String.Format(FLASH_VARS_FORMAT, posterUrl, sourceUrl));

            wrapperTag.InnerHtml += objectTag.ToString(TagRenderMode.Normal);
            return wrapperTag.ToString(TagRenderMode.Normal);
        }

        public static void MergeAttributes(this TagBuilder tagBuilder, object attributesObject)
        {
            var attributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(attributesObject);
            tagBuilder.MergeAttributes(attributesDictionary);
        }


        private static string ParamTag(string name, string value)
        {
            var paramTag = new TagBuilder("param");
            paramTag.Attributes["name"] = name;
            paramTag.Attributes["value"] = value;
            return paramTag.ToString(TagRenderMode.SelfClosing);
        }

        private static readonly string _FlowPlayerPath = VirtualPathUtility.ToAbsolute("~/Content/video/motu/flowplayer-3.2.16.swf");
        private const string FLASH_VARS_FORMAT = "config={{'playlist':['{0}',{{'url':'{1}','autoPlay':false}}]}}";
    }
}