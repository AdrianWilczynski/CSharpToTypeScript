using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CSharpToTypeScript.Blazor.Tests
{
    public class DotnetRunFixture : IDisposable
    {
        private readonly Process _process;

        public DotnetRunFixture()
        {
            _process = new Process
            {
                StartInfo =
                {
                    FileName = "dotnet",
                    Arguments = "run --pathbase=/CSharpToTypeScript",
                    WorkingDirectory = Path.Join(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        "..", "..", "..", "..", "..", "src", "CSharpToTypeScript.Blazor"),
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            _process.Start();

            while (!_process.StandardOutput.ReadLine().Contains("Now listening on:")) { }
        }

        public void Dispose()
        {
            _process.CloseMainWindow();
            if (!_process.WaitForExit(TimeSpan.FromSeconds(3).Milliseconds))
            {
                try { _process.Kill(); } catch { }
            }
            _process.Dispose();
        }
    }
}