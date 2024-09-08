namespace MinefieldGame.Entities
{
    public class Game(Player player, Chessboard chessboard)
    {
        public Player Player { get; } = player;
        public Chessboard Chessboard { get; } = chessboard;
    }
}
