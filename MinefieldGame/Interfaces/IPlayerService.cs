using MinefieldGame.Entities;

namespace MinefieldGame.Interfaces
{
    public interface IPlayerService
    {
        void Move(Player player, string direction, int boardSize);
        void LooseLife(Player player);
    }
}
