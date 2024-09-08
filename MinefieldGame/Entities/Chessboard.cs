namespace MinefieldGame.Entities
{
    public class Chessboard(int size)
    {

        public int Size { get; } = size;
        public bool[,] Mines { get; set; }
    }
    
}
