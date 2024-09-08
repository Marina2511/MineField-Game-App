using MinefieldGame.Entities;
using MinefieldGame.Interfaces;

namespace MinefieldGame.Services
{
    public class PlayerService : IPlayerService
    {
        public void Move(Player player, string direction, int boardSize)
        {
            switch (direction.ToLower())
            {
                case "up": player.Y = Math.Max(0, player.Y - 1); break;
                case "down": player.Y = Math.Min(boardSize - 1,  player.Y + 1); break;
                case "left": player.X = Math.Max(0, player.X - 1); break;
                case "right": player.X = Math.Min(boardSize - 1, player.X + 1); break;
                default:
                    Console.WriteLine("Invalid move! Please enter 'up', 'down', 'left', or 'right'.");
                    return;
            }

            player.Moves++;
        }


        public void LooseLife(Player player)
        {
            if (player.Lives > 0)
            {
                player.Lives--;
            }
        }
    }
}
