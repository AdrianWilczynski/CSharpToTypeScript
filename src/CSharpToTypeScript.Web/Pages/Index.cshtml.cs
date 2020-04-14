using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSharpToTypeScript.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Web.Pages
{
    public class IndexModel : PageModel
    {
        public class SettingsModel : IValidatableObject
        {
            [Display(Name = "Use Tabs")]
            public bool UseTabs { get; set; }

            public bool Export { get; set; } = true;

            [Range(1, 8), Display(Name = "Tab Size")]
            public int? TabSize { get; set; } = 4;

            [Display(Name = "Convert Dates To")]
            public DateOutputType ConvertDatesTo { get; set; }

            [Display(Name = "Convert Nullables To")]
            public NullableOutputType ConvertNullablesTo { get; set; }

            [Display(Name = "To Camel Case")]
            public bool ToCamelCase { get; set; } = true;

            [Display(Name = "Remove Interface Prefix")]
            public bool RemoveInterfacePrefix { get; set; } = true;

            [Display(Name = "Generate Imports")]
            public bool GenerateImports { get; set; } = true;

            [Display(Name = "Use Kebab Case")]
            public bool UseKebabCase { get; set; }

            [Display(Name = "Append Model Suffix")]
            public bool AppendModelSuffix { get; set; }

            [Display(Name = "Quotation Mark")]
            public QuotationMark QuotationMark { get; set; }

            [Display(Name = "Append New Line")]
            public bool AppendNewLine { get; set; }

            [Display(Name = "String Enums")]
            public bool StringEnums { get; set; }

            [Display(Name = "Enum String To Camel Case")]
            public bool EnumStringToCamelCase { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (!UseTabs && TabSize is null)
                {
                    yield return new ValidationResult(
                        "Provide Tab Size value or toggle Use Tabs.",
                        new[] { nameof(TabSize), nameof(UseTabs) });
                }
            }

            public CodeConversionOptions MapToCodeConversionOptions()
                => new CodeConversionOptions(Export, UseTabs, TabSize, ConvertDatesTo, ConvertNullablesTo, ToCamelCase, RemoveInterfacePrefix,
                    GenerateImports ? ImportGenerationMode.Simple : ImportGenerationMode.None,
                    UseKebabCase, AppendModelSuffix, QuotationMark, AppendNewLine, StringEnums, EnumStringToCamelCase);
        }

        private readonly ICodeConverter _codeConverter;

        public IndexModel(ICodeConverter codeConverter)
        {
            _codeConverter = codeConverter;
        }

        [BindProperty, Display(Name = "C# Code"), MaxLength(10000)]
        public string InputCode { get; set; }

        [BindProperty]
        public SettingsModel Settings { get; set; }

        [TempData]
        public string PreviousInputCode { get; set; }

        [TempData]
        public string ConvertedCode { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Response.Cookies.Append(
                nameof(Settings),
                JsonConvert.SerializeObject(Settings),
                new CookieOptions { Expires = DateTimeOffset.Now.AddYears(1) });

            PreviousInputCode = InputCode;

            ConvertedCode = _codeConverter.ConvertToTypeScript(InputCode ?? string.Empty, Settings.MapToCodeConversionOptions());

            return RedirectToPage();
        }

        public void OnGet()
        {
            Settings = Request.Cookies[nameof(Settings)] is string settings
                ? JsonConvert.DeserializeObject<SettingsModel>(settings)
                : new SettingsModel();

            InputCode = PreviousInputCode;
        }
    }
}