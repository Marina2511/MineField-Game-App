using MinefieldGame.Interfaces;

namespace MinefieldGame.Services
{
    public class MineService : IMineService
    {
        public bool[,] InitializeMines(int size, int mineCount)
        {
            var mines = new bool[size, size];
            var random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < mineCount)
            {
                int x = random.Next(size);
                int y = random.Next(size);
                if (!mines[x, y] && (x != 0 || y != 0))
                {
                    mines[x, y] = true;
                    minesPlaced++;
                }
            }

            return mines;
        }
    }
}
