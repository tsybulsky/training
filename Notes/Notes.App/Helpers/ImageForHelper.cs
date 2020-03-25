using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.App.Helpers
{
    public static class ImageForHelper
    {
        public static MvcHtmlString ImageFor(this HtmlHelper html, byte[] imageData, string mimeType = "image/jpeg", string cssStyles="")
        {
            if ((imageData == null) || (imageData.Length == 0))
                return new MvcHtmlString("");
            else
            {
                TagBuilder img = new TagBuilder("img");
                img.Attributes.Add("src", $"data:{mimeType};base64," + Convert.ToBase64String(imageData));
                if (!String.IsNullOrWhiteSpace(cssStyles))
                    img.Attributes.Add("style", cssStyles);
                return new MvcHtmlString(img.ToString());
            }
        }
    }
}