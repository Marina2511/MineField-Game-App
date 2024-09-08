using Microsoft.Extensions.Options;
using MinefieldGame.Entities;
using MinefieldGame.Interfaces;
using MinefieldGame.Options;

namespace MinefieldGame.Services
{
    public class GameService(IPlayerService playerService,
        IMineService mineService,IConsoleService consoleService, IOptions<GameOptions> gameOptions) : IGameService
    {
        private readonly IPlayerService _playerService = playerService;
        private readonly IMineService _mineService = mineService;
        private readonly IConsoleService _consoleService= consoleService;
        private readonly GameOptions _gameOptions = gameOptions.Value;

        public void PlayGame(Game game)
        {
            if (game.Chessboard.Mines == null) // Assuming mines array starts with false
            {
                InitializeGame(game); // Only initialize if not already done
            }

            while (game.Player.Lives > 0)
            {
                DisplayBoard(game);

                _consoleService.Write("Enter your move (up, down, left, right): ");
                string input = _consoleService.ReadLine();

                _playerService.Move(game.Player, input, game.Chessboard.Size);

                if (game.Chessboard.Mines[game.Player.X, game.Player.Y])
                {
                    HandleMineHit(game);

                    if (game.Player.Lives == 0)
                    {
                        _consoleService.WriteLine("Game Over!");
                        return;
                    }
                }

                if (HasPlayerWon(game))
                {
                    _consoleService.WriteLine($"Congratulations! You've reached the other side of the board!\\nFinal score: {game.Player.Moves} moves.");
                    return;
                }

                _consoleService.ClearConsole();
            }
        }

        #region Private Methods

        private void DisplayBoard(Game game)
        {
            for (int y = 0; y < game.Chessboard.Size; y++)
            {
                for (int x = 0; x < game.Chessboard.Size; x++)
                {
                    if (x == game.Player.X && y == game.Player.Y)
                    {
                        _consoleService.Write(" P "); // Player's position
                    }
                    else if (x < game.Chessboard.Size && y < game.Chessboard.Size && game.Chessboard.Mines[x, y] && game.Player.Lives == 0)
                    {
                        _consoleService.Write(" * "); // Show mine if player loses or you want to show mines
                    }
                    else
                    {
                        _consoleService.Write(" . "); // Empty space
                    }
                }
                _consoleService.WriteLine();
            }
            _consoleService.WriteLine($"Position: {game.Player.GetPosition()}, Lives: {game.Player.Lives}, Moves: {game.Player.Moves}");
        }

        public void InitializeGame(Game game)
        {
            game.Chessboard.Mines = _mineService.InitializeMines(_gameOptions.ChessboardSize, _gameOptions.MineCount);
        }
        private void HandleMineHit(Game game)
        {
            _playerService.LooseLife(game.Player);
            _consoleService.WriteLine("Boom! You hit a mine!");
        }

        public bool HasPlayerWon(Game game)
        {
            return game.Player.X == game.Chessboard.Size - 1;
        }
        #endregion
    }
}
