using Audio;
using Player;
using UI;
using Windows;

namespace StatesGame
{
    public class StartStateGame
    {
        private readonly Menu _menu;
        private readonly PlayerPresenter _presenter;
        private readonly PlayerMoverPresenter _presenterMover;
        private readonly HudWindow _hudWindow;
        private readonly SoundSwitcher _soundSwitcher;

        public StartStateGame(
            Menu menu,
            PlayerPresenter presenter,
            PlayerMoverPresenter presenterMover,
            HudWindow hudWindow,
            SoundSwitcher soundSwitcher)
        {
            _menu = menu;
            _presenter = presenter;
            _presenterMover = presenterMover;
            _hudWindow = hudWindow;
            _soundSwitcher = soundSwitcher;
        }

        private void Start()
        {
            _soundSwitcher.Play("StartGame");
            _soundSwitcher.Play("Music");
            _soundSwitcher.Pause("Music2");
            _soundSwitcher.Pause("MenuMusic");

            _hudWindow.OpenWithoutSound();
            _presenter.StartGame();
            _presenterMover.StartPlayerMove();
        }

        public void Enable()
        {
            _menu.StartClicking += Start;
        }

        public void Disable()
        {
            _menu.StartClicking -= Start;
        }
    }
}