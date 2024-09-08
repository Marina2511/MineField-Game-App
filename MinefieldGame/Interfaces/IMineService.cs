namespace MinefieldGame.Interfaces
{
    public interface IMineService
    {
        bool[,] InitializeMines(int size, int mineCount);
    }
}
