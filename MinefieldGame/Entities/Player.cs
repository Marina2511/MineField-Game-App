namespace MinefieldGame.Entities
{
    public class Player(int initialPlayerLives)
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Lives { get; set; } = initialPlayerLives;
        public int Moves { get; set; } = 0;

        public  string GetPosition()
        {
            char column = (char)('A' + X);
            int row = Y + 1;
            return $"{column}{row}";
        }
    }

}
