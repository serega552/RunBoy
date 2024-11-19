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

        public StartStateGame(Menu menu, PlayerPresenter presenter, PlayerMoverPresenter presenterMover, HudWindow hudWindow)
        {
            _menu = menu;
            _presenter = presenter;
            _presenterMover = presenterMover;
            _hudWindow = hudWindow;
        }

        private void Start()
        {
            SoundSwitcher.Instance.Play("StartGame");
            SoundSwitcher.Instance.Play("Music");
            SoundSwitcher.Instance.Pause("Music2");
            SoundSwitcher.Instance.Pause("MenuMusic");

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