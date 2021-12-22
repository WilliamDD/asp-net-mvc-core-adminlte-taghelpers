using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace asp_net_mvc_core_adminlte_taghelpers.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    public class EditorTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public EditorTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-multiline-columns")]
        public int Columns { get; set; } = 100;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-multiline")]
        public bool IsMultiline { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool IsReadOnly { get; set; }

        [HtmlAttributeName("asp-multiline-rows")]
        public int Rows { get; set; } = 3;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //< div class="input-group mb-3">
            //  <input asp-for="Input.Email" class="form-control" placeholder="Email" />
            //  <div class="input-group-append">
            //      <div class="input-group-text">
            //          <span class="fas fa-envelope"></span>
            //      </div>
            //  </div>
            //</div>
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
                TagBuilder textArea;
                if (IsMultiline)
                {
                    textArea = _generator.GenerateTextArea(ViewContext,
                                        For.ModelExplorer,
                                        For.Name,
                                        Rows,
                                        Columns,
                                        IsReadOnly ? new { @class = "form-control-plaintext", @placeholder = For.Metadata.DisplayName, @disabled = "disabled", @autocomplete = "new-" + For.Name } : new { @class = "form-control", @placeholder = For.Metadata.DisplayName, @autocomplete = "new-" + For.Name });
                }
                else
                {
                    textArea = _generator.GenerateTextBox(ViewContext,
                                        For.ModelExplorer,
                                        For.Name,
                                        For.Model,
                                        For.Metadata.EditFormatString,
                                        IsReadOnly ? new { @class = "form-control-plaintext", @placeholder = For.Metadata.DisplayName, @disabled = "disabled", @autocomplete = "new-" + For.Name } : new { @class = "form-control", @placeholder = For.Metadata.DisplayName, @autocomplete = "new-" + For.Name });

                    if (For.ModelExplorer.Metadata.DataTypeName == nameof(System.ComponentModel.DataAnnotations.DataType.EmailAddress))
                    {
                        textArea.Attributes["type"] = "email";
                    }
                    if (For.ModelExplorer.Metadata.DataTypeName == nameof(System.ComponentModel.DataAnnotations.DataType.PhoneNumber))
                    {
                        textArea.Attributes["type"] = "tel";
                    }
                    if (For.ModelExplorer.Metadata.DataTypeName == nameof(System.ComponentModel.DataAnnotations.DataType.Url))
                    {
                        textArea.Attributes["type"] = "url";
                    }
                    if (For.ModelExplorer.Metadata.DataTypeName == nameof(System.ComponentModel.DataAnnotations.DataType.DateTime))
                    {
                        //textArea.Attributes["type"] = "datetime-local";
                        textArea.Attributes["class"] = textArea.Attributes["class"] + " datetimepicker";
                        textArea.Attributes["data-toggle"] = "datetimepicker";
                        textArea.Attributes["data-target"] = "#" + textArea.Attributes["Id"];
                    }
                    if (For.ModelExplorer.Metadata.DataTypeName == nameof(System.ComponentModel.DataAnnotations.DataType.Date))
                    {
                        //textArea.Attributes["type"] = "date";
                        textArea.Attributes["class"] = textArea.Attributes["class"] + " datepicker";
                        textArea.Attributes["data-toggle"] = "datetimepicker";
                        textArea.Attributes["data-target"] = "#" + textArea.Attributes["Id"];
                    }
                    if (For.ModelExplorer.Metadata.DataTypeName == nameof(System.ComponentModel.DataAnnotations.DataType.Time))
                    {
                        //textArea.Attributes["type"] = "time";
                        textArea.Attributes["class"] = textArea.Attributes["class"] + " timepicker";
                        textArea.Attributes["data-toggle"] = "datetimepicker";
                        textArea.Attributes["data-target"] = "#" + textArea.Attributes["Id"];
                    }
                    else
                    {
                    }
                }
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