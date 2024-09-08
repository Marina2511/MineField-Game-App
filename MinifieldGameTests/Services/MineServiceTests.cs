using MinefieldGame.Services;

namespace MinifieldGameTests.Services
{
    public class MineServiceTests
    {
        [Fact]
        public void InitializeMines_CorrectNumberOfMines()
        {
            // Arrange
            var mineService = new MineService();
            int boardSize = 5;
            int mineCount = 3;

            // Act
            var mines = mineService.InitializeMines(boardSize, mineCount);

            // Assert
            int actualMineCount = 0;
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (mines[x, y]) actualMineCount++;
                }
            }
            Assert.Equal(mineCount, actualMineCount);
        }

        [Fact]
        public void InitializeMines_NoMineAtStartingPosition()
        {
            // Arrange
            var mineService = new MineService();
            int boardSize = 5;
            int mineCount = 3;

            // Act
            var mines = mineService.InitializeMines(boardSize, mineCount);

            // Assert
            Assert.False(mines[0, 0], "There should be no mine at the starting position (0, 0).");
        }

        [Fact]
        public void InitializeMines_NoDuplicateMines()
        {
            // Arrange
            var mineService = new MineService();
            int boardSize = 5;
            int mineCount = 5;

            // Act
            var mines = mineService.InitializeMines(boardSize, mineCount);

            // Assert
            var minePositions = new HashSet<(int x, int y)>();
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    if (mines[x, y])
                    {
                        // Try to add the mine position, if it already exists, test will fail
                        bool isUnique = minePositions.Add((x, y));
                        Assert.True(isUnique, $"Duplicate mine found at position ({x}, {y})");
                    }
                }
            }
        }

    }
}
