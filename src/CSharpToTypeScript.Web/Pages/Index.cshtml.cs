using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSharpToTypeScript.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSharpToTypeScript.Web.Pages
{
    public class IndexModel : PageModel
    {
        public class SettingsModel : IValidatableObject
        {
            [Required, Display(Name = "Use Tabs")]
            public bool UseTabs { get; set; } = false;

            [Required]
            public bool Export { get; set; } = true;

            [Range(1, 8), Display(Name = "Tab Size")]
            public int? TabSize { get; set; } = 2;

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (!UseTabs && TabSize is null)
                {
                    yield return new ValidationResult(
                        "Provide Tab Size value or toggle Use Tabs.",
                        new[] { nameof(TabSize), nameof(UseTabs) });
                }
            }
        }

        private readonly CodeConverter _codeConverter;

        public IndexModel(CodeConverter codeConverter)
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
                new CookieOptions { Expires = DateTimeOffset.MaxValue });

            PreviousInputCode = InputCode;

            ConvertedCode = _codeConverter.ConvertToTypeScript(
                InputCode ?? string.Empty, Settings.UseTabs, Settings.TabSize, Settings.Export);

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