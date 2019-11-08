using CSharpToTypeScript.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToTypeScript.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCSharpToTypeScript(this IServiceCollection services)
            => services.AddTransient(_ => TypeConverterFactory.Create())
                .AddTransient<RootTypeConverter>()
                .AddTransient<RootEnumConverter>()
                .AddTransient<SyntaxTreeConverter>()
                .AddTransient<ICodeConverter, CodeConverter>()
                .AddTransient<IFileNameConverter, FileNameConverter>();
    }
}