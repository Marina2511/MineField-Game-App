using MinefieldGame.Entities;
using MinefieldGame.Services;

namespace MinifieldGameTests.Services
{
    public class PlayerServiceTests
    {
        private readonly PlayerService _playerService;

        public PlayerServiceTests()
        {
            _playerService = new PlayerService();
        }

        [Fact]
        public void Move_PlayerMovesUp_ValidMovement()
        {
            // Arrange
            var player = new Player(3) { X = 1, Y = 1, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "up", boardSize);

            // Assert
            Assert.Equal(0, player.Y); // Y coordinate should decrease
            Assert.Equal(1, player.X); // X coordinate should stay the same
            Assert.Equal(1, player.Moves); // Moves should increment
        }


        [Fact]
        public void Move_PlayerMovesDown_ValidMovement()
        {
            // Arrange
            var player = new Player(3) { X = 1, Y = 1, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "down", boardSize);

            // Assert
            Assert.Equal(2, player.Y); // Y coordinate should increase
            Assert.Equal(1, player.X); // X coordinate should stay the same
            Assert.Equal(1, player.Moves); // Moves should increment
        }

        [Fact]
        public void Move_PlayerMovesLeft_ValidMovement()
        {
            // Arrange
            var player = new Player(3) { X = 1, Y = 1, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "left", boardSize);

            // Assert
            Assert.Equal(0, player.X); // X coordinate should decrease
            Assert.Equal(1, player.Y); // Y coordinate should stay the same
            Assert.Equal(1, player.Moves); // Moves should increment
        }

        // Test moving player right within bounds
        [Fact]
        public void Move_PlayerMovesRight_ValidMovement()
        {
            // Arrange
            var player = new Player(3) { X = 1, Y = 1, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "right", boardSize);

            // Assert
            Assert.Equal(2, player.X); // X coordinate should increase
            Assert.Equal(1, player.Y); // Y coordinate should stay the same
            Assert.Equal(1, player.Moves); // Moves should increment
        }

        // Test that player doesn't move beyond the upper edge
        [Fact]
        public void Move_PlayerAtTopBoundary_CannotMoveUp()
        {
            // Arrange
            var player = new Player(3) { X = 1, Y = 0, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "up", boardSize);

            // Assert
            Assert.Equal(0, player.Y); // Y should stay at 0 (boundary)
            Assert.Equal(1, player.X); // X should stay the same
            Assert.Equal(1, player.Moves); // Moves should increment
        }

        // Test that player doesn't move beyond the left edge
        [Fact]
        public void Move_PlayerAtLeftBoundary_CannotMoveLeft()
        {
            // Arrange
            var player = new Player(3) { X = 0, Y = 1, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "left", boardSize);

            // Assert
            Assert.Equal(0, player.X); // X should stay at 0 (boundary)
            Assert.Equal(1, player.Y); // Y should stay the same
            Assert.Equal(1, player.Moves); // Moves should increment
        }

        // Test invalid move direction
        [Fact]
        public void Move_InvalidDirection_NoMovement()
        {
            // Arrange
            var player = new Player(3) { X = 1, Y = 1, Moves = 0 };
            int boardSize = 5;

            // Act
            _playerService.Move(player, "invalid", boardSize);

            // Assert
            Assert.Equal(1, player.X); // X should stay the same
            Assert.Equal(1, player.Y); // Y should stay the same
            Assert.Equal(0, player.Moves); // Moves should not increment
        }

        // Test player loses life
        [Fact]
        public void LooseLife_PlayerLosesOneLife()
        {
            // Arrange
            var player = new Player(3) { Lives = 3 };

            // Act
            _playerService.LooseLife(player);

            // Assert
            Assert.Equal(2, player.Lives); // Lives should decrement by 1
        }

        // Test player does not lose life below 0
        [Fact]
        public void LooseLife_PlayerLivesDoNotGoBelowZero()
        {
            // Arrange
            var player = new Player(3) { Lives = 0 };

            // Act
            _playerService.LooseLife(player);

            // Assert
            Assert.Equal(0, player.Lives); // Lives should stay at 0
        }
    }
}
