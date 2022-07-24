namespace Server.Services
{
    public interface IStdio
    {
        string ReadLine();
        void WriteLine(string line);
    }
}