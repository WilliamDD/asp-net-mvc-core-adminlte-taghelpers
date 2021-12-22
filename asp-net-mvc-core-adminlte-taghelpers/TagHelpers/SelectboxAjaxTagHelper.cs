using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace asp_net_mvc_core_adminlte_taghelpers.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    public class SelectboxAjaxTagHelper : TagHelper
    {
        private readonly IHtmlGenerator _generator;

        public SelectboxAjaxTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        [HtmlAttributeName("asp-cascade")]
        public ModelExpression cascade { get; set; }

        [HtmlAttributeName("asp-data-url")]
        public string dataUrl { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-multiselect")]
        public bool IsMulti { get; set; }

        [HtmlAttributeName("asp-readonly")]
        public bool IsReadOnly { get; set; }

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

                writer.WriteLine("<select id=\"" + For.Name + "\" name=\"" + For.Name + "\" class=\"form-control \" style=\"width: 100%;\"" + (IsMulti == true ? "multiple" : "") + "\"></select>"); ;
                var cascadeScript = new TagBuilder("script");
                cascadeScript.InnerHtml.AppendHtml("$(document).ready(function ()\n");
                cascadeScript.InnerHtml.AppendHtml("{ \n");
                cascadeScript.InnerHtml.AppendHtml("    $('#" + For.Name + "').select2(\n");
                cascadeScript.InnerHtml.AppendHtml("    {\n");
                cascadeScript.InnerHtml.AppendHtml("        theme:'bootstrap4',\n");
                cascadeScript.InnerHtml.AppendHtml("        minimumInputLength:'2',\n");
                cascadeScript.InnerHtml.AppendHtml("        placeholder:'Select an option',\n");
                cascadeScript.InnerHtml.AppendHtml("        allowClear:'true',\n");
                cascadeScript.InnerHtml.AppendHtml("        ajax:\n");
                cascadeScript.InnerHtml.AppendHtml("        {\n");
                cascadeScript.InnerHtml.AppendHtml("            url: '" + dataUrl + "',\n");
                cascadeScript.InnerHtml.AppendHtml("            data: function(params)\n");
                cascadeScript.InnerHtml.AppendHtml("            {\n");
                cascadeScript.InnerHtml.AppendHtml("                var query = \n");
                cascadeScript.InnerHtml.AppendHtml("                {\n");
                cascadeScript.InnerHtml.AppendHtml("                    term: params.term,\n");
                cascadeScript.InnerHtml.AppendHtml("                    q: params.term,\n");
                cascadeScript.InnerHtml.AppendHtml("                    _type: 'public',\n");
                if (cascade != null)
                {
                    cascadeScript.InnerHtml.AppendHtml("                    parentId: $(\"#" + cascade.Name + "\").val()\n");
                }
                else
                {
                }
                cascadeScript.InnerHtml.AppendHtml("                }\n");
                cascadeScript.InnerHtml.AppendHtml("                return query;\n");
                cascadeScript.InnerHtml.AppendHtml("            }\n");
                cascadeScript.InnerHtml.AppendHtml("        }\n");
                cascadeScript.InnerHtml.AppendHtml("    })\n");
                cascadeScript.InnerHtml.AppendHtml("});\n");

                // Fetch the preselected item, and add to the control
                cascadeScript.InnerHtml.AppendHtml("var " + For.Name + "Select = $('#" + For.Name + "');\n");
                cascadeScript.InnerHtml.AppendHtml("$.ajax({\n");
                cascadeScript.InnerHtml.AppendHtml("    type: 'GET',\n");
                cascadeScript.InnerHtml.AppendHtml("    url: '" + dataUrl + "/" + For.Model + "',\n");
                cascadeScript.InnerHtml.AppendHtml("}).then(function(data) {\n");
                cascadeScript.InnerHtml.AppendHtml("    // create the option and append to Select2\n");
                cascadeScript.InnerHtml.AppendHtml("    var option = new Option(data.text, data.id, true, true);\n");
                cascadeScript.InnerHtml.AppendHtml("    " + For.Name + "Select.append(option).trigger('change');\n");
                cascadeScript.InnerHtml.AppendHtml("    // manually trigger the `select2:select` event\n");
                cascadeScript.InnerHtml.AppendHtml("    " + For.Name + "Select.trigger({\n");
                cascadeScript.InnerHtml.AppendHtml("        type: 'select2:select',\n");
                cascadeScript.InnerHtml.AppendHtml("        params: {\n");
                cascadeScript.InnerHtml.AppendHtml("            data: data\n");
                cascadeScript.InnerHtml.AppendHtml("        }\n");
                cascadeScript.InnerHtml.AppendHtml("    });\n");
                cascadeScript.InnerHtml.AppendHtml("});\n");

                cascadeScript.WriteTo(writer, NullHtmlEncoder.Default);

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