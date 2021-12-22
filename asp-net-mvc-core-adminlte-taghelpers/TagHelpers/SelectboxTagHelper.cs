using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace asp_net_mvc_core_adminlte_taghelpers.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    public class SelectboxTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public SelectboxTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-multiselect")]
        public bool IsMulti { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool IsReadOnly { get; set; }

        [HtmlAttributeName("asp-items")]
        public IEnumerable<SelectListItem> Items { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //<div class="form-group">
            //    <label>Minimal</label>
            //    <select class="form-control select2bs4" style="width: 100%;">
            //    <option selected = "selected" > Alabama </ option >
            //    < option > Alaska </ option >
            //    < option > California </ option >
            //    < option > Delaware </ option >
            //    < option > Tennessee </ option >
            //    < option > Texas </ option >
            //    < option > Washington </ option >
            //    </ select >
            //</ div >
            //< !-- /.form - group-- >

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-group");

            using (var writer = new StringWriter())
            {
                //writer.Write(@"<div class=""form-group"">");

                var label = _generator.GenerateLabel(
                                ViewContext,
                                For.ModelExplorer,
                                For.Name, null,
                                new { @class = "" });

                label.WriteTo(writer, NullHtmlEncoder.Default);

                var textArea = _generator.GenerateSelect(ViewContext,
                                    For.ModelExplorer,
                                    "&nbsp",
                                    For.Name,
                                    Items,
                                    IsMulti,
                                    IsReadOnly ? new { @class = "form-control combobox", style = "width: 100%;", @placeholder = For.Metadata.DisplayName, @disabled = "disabled" } : new { @class = "form-control combobox", style = "width: 100%;", @placeholder = For.Metadata.DisplayName });
                textArea.WriteTo(writer, NullHtmlEncoder.Default);

                var validationMsg = _generator.GenerateValidationMessage(
                                        ViewContext,
                                        For.ModelExplorer,
                                        For.Name,
                                        null,
                                        ViewContext.ValidationMessageElement,
                                        new { @class = "text-danger" });

                validationMsg.WriteTo(writer, NullHtmlEncoder.Default);
                //writer.Write(@"</div>");

                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}
