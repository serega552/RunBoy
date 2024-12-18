using Audio;
using Player;
using UI;
using Windows;

namespace StatesGame
{
    public class StartStateGame
    {
        private readonly Menu _menu;
        private readonly HudWindow _hudWindow;
        private readonly SoundSwitcher _soundSwitcher;

        private PlayerView _playerView;
        private PlayerMoverView _playerMover;

        public StartStateGame(
            Menu menu,
            PlayerView player,
            PlayerMoverView playerMover,
            HudWindow hudWindow,
            SoundSwitcher soundSwitcher)
        {
            _menu = menu;
            _playerView = player;
            _playerMover = playerMover;
            _hudWindow = hudWindow;
            _soundSwitcher = soundSwitcher;
        }

        public void AddPlayer(PlayerView player, PlayerMoverView playerMover)
        {
            _playerView = player;
            _playerMover = playerMover;
        }

        public void Enable()
        {
            _menu.StartClicking += Start;
        }

        public void Disable()
        {
            _menu.StartClicking -= Start;
        }

        private void Start()
        {
            _soundSwitcher.Play("StartGame");
            _soundSwitcher.Play("Music");
            _soundSwitcher.Pause("Music2");
            _soundSwitcher.Pause("MenuMusic");

            _hudWindow.OpenWithoutSound();
            _playerView.StartMove();
            _playerMover.StartMove();
        }
    }
}