namespace MinefieldGame.Interfaces
{
    public interface IConsoleService
    {
        void WriteLine(string message);
        void Write(string message);
        void WriteLine();
        string ReadLine();
        void ClearConsole();
    }
}
