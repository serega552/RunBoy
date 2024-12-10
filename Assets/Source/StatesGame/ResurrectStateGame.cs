using Player;
using UI;

namespace StatesGame
{
    public class ResurrectStateGame
    {
        private readonly PlayerResurrect _playerResurrect;

        private PlayerMoverView _playerMover;
        private PlayerView _player;

        public ResurrectStateGame(
            PlayerView player,
            PlayerMoverView playerMover,
            PlayerResurrect playerResurrect)
        {
            _playerMover = playerMover;
            _player = player;
            _playerResurrect = playerResurrect;
        }

        public void Enable()
        {
            _playerResurrect.Resurrected += Resurrect;
        }

        public void Disable()
        {
            _playerResurrect.Resurrected -= Resurrect;
        }

        public void AddPlayer(PlayerView player, PlayerMoverView playerMover)
        {
            _player = player;
            _playerMover = playerMover;
        }

        private void Resurrect(float energy)
        {
            _player.ResurrectPlayer(energy);
            _playerMover.StartMove();
        }
    }
}