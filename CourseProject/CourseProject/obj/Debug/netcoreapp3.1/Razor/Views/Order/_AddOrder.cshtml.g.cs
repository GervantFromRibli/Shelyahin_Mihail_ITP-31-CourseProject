#pragma checksum "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7756bd9580182361d453c7c1c1732a0f5a7095f4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order__AddOrder), @"mvc.1.0.view", @"/Views/Order/_AddOrder.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\_ViewImports.cshtml"
using CourseProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\_ViewImports.cshtml"
using CourseProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7756bd9580182361d453c7c1c1732a0f5a7095f4", @"/Views/Order/_AddOrder.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"912c052c1394ef2c0bed763d52b2ffdcc7425164", @"/Views/_ViewImports.cshtml")]
    public class Views_Order__AddOrder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OrderIndexViewModel>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h3>\r\n    Добавление записи\r\n</h3>\r\n<hr />\r\n");
#nullable restore
#line 6 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
 using (Html.BeginForm("AddOrder", "Order", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        ");
#nullable restore
#line 9 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.ClientName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            <select name=\"ClientName\">\r\n");
#nullable restore
#line 12 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                 foreach (var name in Model.ClientNames)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7756bd9580182361d453c7c1c1732a0f5a7095f44139", async() => {
#nullable restore
#line 14 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                       Write(name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 15 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
#nullable restore
#line 20 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.EmployeeFIO));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            <select name=\"EmployeeFIO\">\r\n");
#nullable restore
#line 23 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                 foreach (var fio in Model.EmployeesFIOs)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7756bd9580182361d453c7c1c1732a0f5a7095f46229", async() => {
#nullable restore
#line 25 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                       Write(fio);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 26 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
#nullable restore
#line 31 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.FurnitureName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            <select name=\"FurnitureName\">\r\n");
#nullable restore
#line 34 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                 foreach (var name in Model.FurnitureNames)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7756bd9580182361d453c7c1c1732a0f5a7095f48324", async() => {
#nullable restore
#line 36 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                       Write(name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 37 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </select>\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
#nullable restore
#line 42 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.FurnitureCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            ");
#nullable restore
#line 44 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
       Write(Html.TextBoxFor(m => m.FurnitureCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
#nullable restore
#line 48 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.DiscountPercent));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            ");
#nullable restore
#line 50 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
       Write(Html.TextBoxFor(m => m.DiscountPercent));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
#nullable restore
#line 54 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            ");
#nullable restore
#line 56 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
       Write(Html.TextBoxFor(m => m.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
#nullable restore
#line 60 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
   Write(Html.LabelFor(m => m.IsCompleted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <div class=\"col-md-10\">\r\n            ");
#nullable restore
#line 62 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
       Write(Html.CheckBoxFor(m => m.IsCompleted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <br />\r\n    <input type=\"submit\" class=\"btn btn-default\" style=\"left:15px; position:relative\" value=\"Добавить\" />\r\n");
#nullable restore
#line 67 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Order\_AddOrder.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OrderIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
