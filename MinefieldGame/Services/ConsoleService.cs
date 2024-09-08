using MinefieldGame.Interfaces;

namespace MinefieldGame.Services
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine(string message) => Console.WriteLine(message);
        public void WriteLine() => Console.WriteLine();
        public void Write(string message) => Console.Write(message);
        public string ReadLine() => Console.ReadLine();
        public void ClearConsole() => Console.Clear();
    }
}
