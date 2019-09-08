using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSharpToTypeScript.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CSharpToTypeScript.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CodeConverter _codeConverter;

        public IndexModel(CodeConverter codeConverter)
        {
            _codeConverter = codeConverter;
        }

        [BindProperty, Display(Name = "C# Code")]
        public string InputCode { get; set; }

        [BindProperty, Required, Display(Name = "Use Tabs")]
        public bool UseTabs { get; set; }

        [BindProperty, Required]
        public bool Export { get; set; }

        [BindProperty, Range(2, 8), Display(Name = "Tab Size")]
        public int? TabSize { get; set; }

        [TempData]
        public string PreviousInputCode { get; set; }

        [TempData]
        public string ConvertedCode { get; set; }

        public IActionResult OnPost()
        {
            Validate();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cookieOptions = new CookieOptions { Expires = DateTimeOffset.MaxValue };
            Response.Cookies.Append(nameof(TabSize), TabSize.ToString(), cookieOptions);
            Response.Cookies.Append(nameof(UseTabs), UseTabs.ToString(), cookieOptions);
            Response.Cookies.Append(nameof(Export), Export.ToString(), cookieOptions);

            PreviousInputCode = InputCode;

            ConvertedCode = _codeConverter.ConvertToTypeScript(InputCode ?? string.Empty, UseTabs, TabSize, Export);

            return RedirectToPage();
        }

        public void OnGet()
        {
            TabSize = int.TryParse(Request.Cookies[nameof(TabSize)], out var tabSize) ? tabSize : (int?)null;
            UseTabs = bool.TryParse(Request.Cookies[nameof(UseTabs)], out var useTabs) && useTabs;
            Export = bool.TryParse(Request.Cookies[nameof(Export)], out var export) ? export : true;

            InputCode = PreviousInputCode;
        }

        private void Validate()
        {
            if (!UseTabs && TabSize is null)
            {
                ModelState.AddModelError(nameof(TabSize), "Provie Tab Size value or toggle Use Tabs.");
            }
        }
    }
}

