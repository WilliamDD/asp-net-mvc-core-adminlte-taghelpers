using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace asp_net_mvc_core_adminlte_taghelpers.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    public class CardTagHelper : TagHelper
    {
        public CardTagHelper(IHtmlGenerator generator)
        {
        }

        [HtmlAttributeName("asp-color")]
        public string Color { get; set; } = "card-warning";

        [HtmlAttributeName("asp-icon")]
        public string Icon { get; set; }

        [HtmlAttributeName("asp-padding")]
        public string Padding { get; set; }

        [HtmlAttributeName("asp-title")]
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? output.Content.GetContent() :
              (await output.GetChildContentAsync()).GetContent();

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "card card-outline " + Color);

            using (var writer = new StringWriter())
            {
                if (!string.IsNullOrWhiteSpace(Title))
                {
                    writer.Write(@"<div class=""card-header"">");
                    writer.Write(@"<h5 class=""card-title"">" + (string.IsNullOrWhiteSpace(Icon) ? "" : "<i class=\"fad " + Icon + " \"></i> ") + Title + "</h5>");
                    writer.Write(@"</div>");
                }

                writer.Write(@"<div class=""card-body table-responsive " + Padding + " \">");
                writer.Write(childContent);
                writer.Write(@"</div>");

                //writer.Write(@"<div class=""card-footer"">");
                //writer.Write(@"&nbsp;");
                //writer.Write(@"</div>");

                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}