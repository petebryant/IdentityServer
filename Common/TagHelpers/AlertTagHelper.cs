using IdentityServer.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IdentityServer.Common.TagHelpers
{
    [HtmlTargetElement("alert")]
    public class AlertTegHelper : TagHelper
    {
        public AlertType Type {get;set;}
        public string Message {get;set;}

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"alert {GetTypeClass(Type)} alert-dismissable");

            var button = new TagBuilder("button");
            button.Attributes.Add("type", "button");
            button.Attributes.Add("data-dismiss", "alert");
            button.Attributes.Add("aria_label", "Close");
            button.AddCssClass("close");

            var span = new TagBuilder("span");
            span.Attributes.Add("aria_hidden", "true");
            span.InnerHtml.SetHtmlContent("&times;");

            button.InnerHtml.SetHtmlContent(span);

            var spanContent = new TagBuilder("span");
            spanContent.InnerHtml.SetHtmlContent(Message);

            output.Content.AppendHtml(spanContent);
            output.Content.AppendHtml(button);

        }
        private string GetTypeClass(AlertType type)
        {
            switch (type)
            {
                case AlertType.Error:
                    return "alert-danger";
                case AlertType.Success:
                    return "alert-success";
                case AlertType.Warning:
                    return "alert-warning";
                default:
                    return "alert-info";                    
            }
        }
    }
}