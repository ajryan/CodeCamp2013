using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResponsiveWebDesignDemo.Helpers
{
    public static class VideoHelpers
    {
        private static readonly string FlowPlayerPath = VirtualPathUtility.ToAbsolute("~/Content/video/motu/flowplayer-3.2.16.swf");
        private const string FlashVarsFormat = "config={{'playlist':['{0}',{{'url':'{1}','autoPlay':false}}]}}";
        
        /// <summary>
        /// Returns an optimized video tag for the device type that will play this video
        /// </summary>
        /// <param name="helper">the HtmlHelper associated with this method call</param>
        /// <param name="sourceUrls">a string array representing the Url paths to the different video formats available for this video</param>
        /// <param name="flashFallbackSourceUrl">the default or fallback Url for the video</param>
        /// <param name="posterRelativePath">the relative path to the image to display before the video is played</param>
        /// <param name="controls">Adds a 'controls' attribute to the video tag if true</param>
        /// <param name="preload">Controls whether or not the video should preload or not</param>
        /// <param name="id">Adds an id attribute to the tag if desired</param>
        /// <returns></returns>
        public static MvcHtmlString CreateVideoTag(
            this HtmlHelper helper,
            IEnumerable<string> sourceUrls,
            string flashFallbackSourceUrl,
            string posterRelativePath,
            bool controls = true,
            bool preload = false,
            string id = null)
        {
            var videoTag = new TagBuilder("video");
            var posterUrl = VirtualPathUtility.ToAbsolute(posterRelativePath);
            var videoAttributes = new
            {
                preload = preload ? "auto" : "none",
                poster = posterUrl
            };
            videoTag.MergeTagAttributes(videoAttributes);
            if (controls)
            {
                videoTag.Attributes["controls"] = "controls";
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                videoTag.Attributes["id"] = id;
            }
            var sourceUrlArray = sourceUrls.ToArray();
            for (var sourceIndex = 0; sourceIndex < sourceUrlArray.Length; sourceIndex++)
            {
                var sourceUrl = sourceUrlArray[sourceIndex];
                var sourceTag = new TagBuilder("source");
                sourceTag.Attributes["src"] = sourceUrl;
                // Omit type from final source
                if (sourceIndex != sourceUrlArray.Length - 1)
                {
                    var extension = Path.GetExtension(sourceUrl);
                    if (extension != null)
                    {
                        sourceTag.Attributes["type"] = "video/" + extension.Substring(1);
                    }
                }
                videoTag.InnerHtml += sourceTag.ToString(TagRenderMode.SelfClosing);
            }
            videoTag.InnerHtml += CreateFlowPlayerDiv(posterUrl, flashFallbackSourceUrl);
            return new MvcHtmlString(videoTag.ToString(TagRenderMode.Normal));
        }
        

        /// <summary>
        /// Returns a div tag for use with the (Flash Shockwave video based) FlowPlayer
        /// </summary>
        /// <param name="posterUrl">the image to display before the video is played</param>
        /// <param name="sourceUrl">the source url for the video</param>
        /// <returns>a string representing a div tag</returns>
        private static string CreateFlowPlayerDiv(string posterUrl, string sourceUrl)
        {
            var wrapperTag = new TagBuilder("div");
            wrapperTag.Attributes["class"] = "object-wrapper";
            wrapperTag.Attributes["style"] = "position: relative; height: 0px; padding-top: 25px; padding-bottom: 56.25%;";
            var objectTag = new TagBuilder("object");
            var objectAttributes = new
            {
                type = "application/x-shockwave-flash",
                data = FlowPlayerPath,
                style = "position: absolute; top: 0; left: 0; width: 100%; height: 100%",
                width = "800",
                height = "450"
            };
            objectTag.MergeTagAttributes(objectAttributes);
            objectTag.InnerHtml += CreateParamTag("movie", FlowPlayerPath);
            objectTag.InnerHtml += CreateParamTag("allowFullScreen", "true");
            objectTag.InnerHtml += CreateParamTag("wmode", "transparent");
            objectTag.InnerHtml += CreateParamTag(
                "flashVars",
                String.Format(FlashVarsFormat, posterUrl, sourceUrl));
            wrapperTag.InnerHtml += objectTag.ToString(TagRenderMode.Normal);
            return wrapperTag.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// A helper function designed to deduplicate and optimize the attibutes that have been added to a tagbuilder object
        /// </summary>
        /// <param name="tagBuilder">the TagBuilder object you wish to optimize</param>
        /// <param name="attributesObject">the attributes you want to merge into the TagBuilder object, expressed as an object</param>
        /// <remarks>Wraps the MergeAttributes method of the TagBuilder class, substituting and object for a dictionary</remarks>
        public static void MergeTagAttributes(this TagBuilder tagBuilder, object attributesObject)
        {
            var attributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(attributesObject);
            tagBuilder.MergeAttributes(attributesDictionary);
        }


        /// <summary>
        /// Creates a param tag based upon the name and value params
        /// </summary>
        /// <param name="name">the name for the param tag</param>
        /// <param name="value">the value for the param tag</param>
        /// <returns></returns>
        private static string CreateParamTag(string name, string value)
        {
            var paramTag = new TagBuilder("param");
            paramTag.Attributes["name"] = name;
            paramTag.Attributes["value"] = value;
            return paramTag.ToString(TagRenderMode.SelfClosing);
        }
    }
}