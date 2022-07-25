using System;
using System.Net.Http;
using CSharpToTypeScript.Core.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<CSharpToTypeScript.Blazor.Pages.Index>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCSharpToTypeScript();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();

await app.RunAsync();