#pragma checksum "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "22b308ca92a868f94b4f47dbd5a4256ea1fce315"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tables_GetFurniture), @"mvc.1.0.view", @"/Views/Tables/GetFurniture.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"22b308ca92a868f94b4f47dbd5a4256ea1fce315", @"/Views/Tables/GetFurniture.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"912c052c1394ef2c0bed763d52b2ffdcc7425164", @"/Views/_ViewImports.cshtml")]
    public class Views_Tables_GetFurniture : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<CourseProject.Models.Furniture>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
  
    ViewBag.Title = "Таблица мебели";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
:
<div>
    <table border=""1"">
        <thead>
            <tr>
                <td>
                    Id
                </td>
                <td>
                    Название
                </td>
                <td>
                    Описание
                </td>
                <td>
                    Материал
                </td>
                <td>
                    Цена
                </td>
                <td>
                    Количество
                </td>
            </tr>
        </thead>
");
#nullable restore
#line 32 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
         foreach (var elem in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 36 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
               Write(elem.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 39 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
               Write(elem.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 42 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
               Write(elem.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 45 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
               Write(elem.Material);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 48 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
               Write(elem.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
               Write(elem.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 54 "D:\Shelyahin_Mihail_ITP-31-CourseProject\CourseProject\CourseProject\Views\Tables\GetFurniture.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<CourseProject.Models.Furniture>> Html { get; private set; }
    }
}
#pragma warning restore 1591