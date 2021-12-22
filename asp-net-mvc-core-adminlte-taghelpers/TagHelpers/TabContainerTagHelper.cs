using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace asp_net_mvc_core_adminlte_taghelpers.TagHelpers
{
    public class ParentChildContext
    {
        public ParentChildContext()
        {
            Titles = new List<Tab>();
        }

        public List<Tab> Titles { get; set; }
    }

    public class Tab
    {
        public bool Active { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class TabContainerTagHelper : TagHelper
    {
        private ParentChildContext _context;

        //3F2504E0-4F89-11D3-9A0C-0305E82C3301
        [HtmlAttributeName("asp-id")]
        public string TabContainerId { get; set; } = "GEN" + Guid.NewGuid().ToString().Substring(24);

        public override void Init(TagHelperContext context)
        {
            _context = new ParentChildContext();
            context.Items.Add(typeof(ParentChildContext), _context);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = output.Content.IsModified ? output.Content.GetContent() : (await output.GetChildContentAsync()).GetContent();
            var items = _context.Titles;

            output.TagName = "div";
            output.Attributes.Add("class", "card card-warning card-outline card-outline-tabs");

            using (var writer = new StringWriter())
            {
                var cascadeScript = new TagBuilder("div");
                cascadeScript.Attributes.Add("class", "card-header p-0 border-bottom-0");
                cascadeScript.InnerHtml.AppendHtml("<ul class=\"nav nav-tabs\" id=\"" + TabContainerId + "\" role=\"tablist\">");

                foreach (var item in items)
                {
                    cascadeScript.InnerHtml.AppendHtml("<li class=\"nav-item\">");
                    string cssClass = "nav-link";
                    if (item.Active)
                    {
                        cssClass = "nav-link active";
                    }
                    cascadeScript.InnerHtml.AppendHtml("<a class=\"" + cssClass + "\" id=\"" + item.Id + "-tab\" data-toggle=\"pill\" href=\"#" + item.Id + "\" role=\"tab\" aria-controls=\"" + item.Id + "\" aria-selected=\"true\">" + item.Title + "</a>");
                    cascadeScript.InnerHtml.AppendHtml("</li>");
                }

                cascadeScript.InnerHtml.AppendHtml("</ul>");
                cascadeScript.InnerHtml.AppendHtml("</div>");
                cascadeScript.InnerHtml.AppendHtml("<div class=\"card-body\">");
                cascadeScript.InnerHtml.AppendHtml("<div class=\"tab-content\" id=\"" + TabContainerId + "Content\">");

                cascadeScript.InnerHtml.AppendHtml(childContent);

                cascadeScript.InnerHtml.AppendHtml("</div>");

                cascadeScript.WriteTo(writer, NullHtmlEncoder.Default);
                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }

    public class TabTagHelper : TagHelper
    {
        public TabTagHelper()
        {
        }

        [HtmlAttributeName("asp-active")]
        public bool Active { get; set; }

        [HtmlAttributeName("asp-id")]
        public string Id { get; set; } = "GEN" + Guid.NewGuid().ToString().Substring(24);

        [HtmlAttributeName("asp-title")]
        public string Title { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var parentContext = (ParentChildContext)context.Items[typeof(ParentChildContext)];
            parentContext.Titles.Add(new Tab { Id = Id, Title = Title, Active = Active });

            output.TagName = "div";
            string cssClass = "tab-pane fade";
            if (Active)
            {
                cssClass = "tab-pane fade active show";
            }

            output.Attributes.SetAttribute("class", cssClass);
            output.Attributes.SetAttribute("id", Id);
            output.Attributes.SetAttribute("role", "tabpanel");
            output.Attributes.SetAttribute("aria-labelledby", Id + "-tab");
        }
    }
}