using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace asp_net_mvc_core_adminlte_taghelpers.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    public class CheckboxTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public CheckboxTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool IsReadOnly { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //<div class="form-group">
            //    <div class="custom-control custom-switch">
            //        <input type = "checkbox" class="custom-control-input" id="customSwitch1">
            //        <label class="custom-control-label" for="customSwitch1">Toggle this custom switch element</label>
            //    </div>
            //</div>
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "form-group");

            using (var writer = new StringWriter())
            {
                //writer.Write(@"<div class=""form-group"">");
                writer.Write(@"<div class=""custom-control custom-switch"">");

                var textArea = _generator.GenerateCheckBox(ViewContext,
                                    For.ModelExplorer,
                                    For.Name,
                                    (bool)For.Model,
                                    IsReadOnly ? new { @class = "custom-control-input", @placeholder = For.Metadata.DisplayName, @disabled = "disabled" } : new { @class = "custom-control-input", @placeholder = For.Metadata.DisplayName });
                textArea.WriteTo(writer, NullHtmlEncoder.Default);

                var label = _generator.GenerateLabel(
                                ViewContext,
                                For.ModelExplorer,
                                For.Name, null,
                                new { @class = "custom-control-label" });

                label.WriteTo(writer, NullHtmlEncoder.Default);

                //var validationMsg = _generator.GenerateValidationMessage(
                //                        ViewContext,
                //                        For.ModelExplorer,
                //                        For.Name,
                //                        null,
                //                        ViewContext.ValidationMessageElement,
                //                        new { @class = "text-danger" });

                //validationMsg.WriteTo(writer, NullHtmlEncoder.Default);
                writer.Write(@"</div>");
                //writer.Write(@"</div>");

                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}