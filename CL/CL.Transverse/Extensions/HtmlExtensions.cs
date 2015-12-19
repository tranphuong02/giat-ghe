using System.Web.Mvc;
using System.Web.Routing;
using CL.Transverse.Enums;

namespace CL.Transverse.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Emits a stylized status message.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/></param>
        /// <param name="messageType">The type of message being displayed, determines the style to be used.</param>
        /// <param name="messageText">Text or html of the message.</param>
        /// <param name="htmlAttributes">Additional html attributes for the outer message container.</param>
        public static MvcHtmlString ShowStatusMessage(this HtmlHelper htmlHelper, StatusMessageType messageType = StatusMessageType.Success, string messageText = null, object htmlAttributes = null)
        {
            // required for legacy webform pages
            if (htmlHelper == null)
                return MvcHtmlString.Empty;

            var tempData = htmlHelper.ViewContext.TempData;

            if (string.IsNullOrEmpty(messageText))
            {
                messageText = (string)tempData[Constants.GeneralAdminConfigs.StatusMessageText];
            }

            if (string.IsNullOrEmpty(messageText))
                return MvcHtmlString.Empty;

            // if dictionary contains keys for type use appropriate StatusMessage overload
            if (tempData["StatusMessageType"] != null)
            {
                messageType = (StatusMessageType)tempData[Constants.GeneralAdminConfigs.StatusMessageType];
            }

            var innerSpan = new TagBuilder("span") { InnerHtml = messageText };

            var outerDiv = new TagBuilder("div")
            {
                InnerHtml = "<button type='button' class='close' data-dismiss='alert'" +
                            " aria-hidden='true'>&times;</button>" +
                            innerSpan
            };

            var attribs = htmlAttributes == null ? new RouteValueDictionary() : new RouteValueDictionary(htmlAttributes);
            outerDiv.MergeAttributes(attribs);
            outerDiv.AddCssClass("alert alert-dismissable");

            switch (messageType)
            {
                case StatusMessageType.Success:
                    outerDiv.AddCssClass("alert-success");
                    break;
                case StatusMessageType.Info:
                    outerDiv.AddCssClass("alert-info");
                    break;
                case StatusMessageType.Warning:
                    outerDiv.AddCssClass("alert-warning");
                    break;
                case StatusMessageType.Danger:
                    outerDiv.AddCssClass("alert-danger");
                    break;
            }

            return MvcHtmlString.Create(outerDiv.ToString());
        }
    }
}
