using System;

namespace Server.Services
{
    public class Stdio : IStdio
    {
        public string ReadLine() => Console.ReadLine();
        public void WriteLine(string line) => Console.WriteLine(line);
    }
}