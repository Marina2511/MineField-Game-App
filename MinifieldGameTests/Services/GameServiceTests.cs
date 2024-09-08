using Microsoft.Extensions.Options;
using MinefieldGame.Entities;
using MinefieldGame.Interfaces;
using MinefieldGame.Options;
using MinefieldGame.Services;
using Moq;

namespace MinifieldGameTests.Services
{
    public class GameServiceTests
    {
        private readonly Mock<IPlayerService> _playerServiceMock;
        private readonly Mock<IMineService> _mineServiceMock;
        private readonly Mock<IConsoleService> _consoleServiceMock;
        private readonly GameService _gameService;
        private readonly GameOptions _gameOptions;

        public GameServiceTests()
        {
            _playerServiceMock = new Mock<IPlayerService>();
            _mineServiceMock = new Mock<IMineService>();
            _consoleServiceMock = new Mock<IConsoleService>();
            _gameOptions = new GameOptions { ChessboardSize = 5, MineCount = 5 };
            var options = Options.Create(_gameOptions);
            _gameService = new GameService(_playerServiceMock.Object, _mineServiceMock.Object, _consoleServiceMock.Object, options);
        }


        [Fact]
        public void PlayGame_OutputsWinMessage_WhenPlayerReachesEnd()
        {
            // Arrange
            var player = new Player(3) { Lives = 3, X = 4, Y = 0 }; // End of the board
            var chessboard = new Chessboard(5) { Mines = new bool[5, 5] };
            var game = new Game(player, chessboard);

            _consoleServiceMock.Setup(cs => cs.ReadLine()).Returns("right");
            _consoleServiceMock.Setup(cs => cs.Write(It.IsAny<string>())).Verifiable();
            _consoleServiceMock.Setup(cs => cs.WriteLine(It.IsAny<string>())).Verifiable();

            // Act
            _gameService.PlayGame(game);

            // Assert
            _consoleServiceMock.Verify(cs => cs.WriteLine(It.Is<string>(s => s.Contains("Congratulations!"))), Times.Once);
        }

        [Fact]
        public void PlayGame_HandlesMineHit_WhenPlayerHitsMine()
        {
            // Arrange
            var player = new Player(1) { X = 0, Y = 0 };
            var chessboard = new Chessboard(5) { Mines = new bool[5, 5] };
            chessboard.Mines[0, 0] = true; // Mine at the starting position
            var game = new Game(player, chessboard);

            _playerServiceMock.Setup(ps => ps.LooseLife(It.IsAny<Player>())).Callback(() => player.Lives--);
            _consoleServiceMock.Setup(cs => cs.ReadLine()).Returns("right");
            _consoleServiceMock.Setup(cs => cs.Write(It.IsAny<string>())).Verifiable();
            _consoleServiceMock.Setup(cs => cs.WriteLine(It.IsAny<string>())).Verifiable();

            // Act
            _gameService.PlayGame(game);

            // Assert
            _consoleServiceMock.Verify(cs => cs.WriteLine("Boom! You hit a mine!"),Times.Once);
            Assert.Equal(0, player.Lives); // Expect lives to be decremented
        }

        [Fact]
        public void PlayGame_PlayerLosesAllLives_GameEnds()
        {
            // Arrange
            var player = new Player(3) { Lives = 3, X = 0, Y = 0 };
            var chessboard = new Chessboard(5) { Mines = new bool[5, 5] };
            // Place a mine at the player's starting position
            chessboard.Mines[0, 0] = true;

            var game = new Game(player, chessboard);

            // Setup mocks
            _consoleServiceMock.Setup(cs => cs.ReadLine()).Returns("right"); 
            _consoleServiceMock.Setup(cs => cs.Write(It.IsAny<string>())).Verifiable();
            _consoleServiceMock.Setup(cs => cs.WriteLine(It.IsAny<string>())).Verifiable();

            // Simulate LooseLife behavior
            _playerServiceMock.Setup(ps => ps.LooseLife(It.IsAny<Player>())).Callback(() => player.Lives--);

            // Act
            _gameService.PlayGame(game);

            // Assert
            _playerServiceMock.Verify(ps => ps.LooseLife(It.IsAny<Player>()), Times.Exactly(3)); 
            _consoleServiceMock.Verify(cs => cs.WriteLine("Game Over!"), Times.Once); 
        }
       
    }
}