using System;
using System.Collections.Generic;

namespace CSharpToTypeScript.CLITool.Utilities
{
    public static class ExceptionMessage
    {
        public static IEnumerable<string> Flatten(Exception exception)
        {
            do
            {
                yield return exception.Message;
                exception = exception.InnerException;
            } while (!(exception is null));
        }
    }
}