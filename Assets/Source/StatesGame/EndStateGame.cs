using System;
using Audio;
using Player;
using UI;
using Windows;
using YG;

namespace StatesGame
{
    public class EndStateGame
    {
        private readonly Menu _menu;
        private readonly EndGameScreen _endScreen;
        private readonly PlayerMoverPresenter _presenterMover;
        private readonly PlayerPresenter _presenter;
        private readonly PlayerResurrect _playerResurrect;
        private readonly HudWindow _hudWindow;
        private readonly LeaderboardYG _leaderboard;
        private readonly SoundSwitcher _soundSwitcher;

        public EndStateGame(
            Menu menu,
            PlayerPresenter presenter,
            PlayerMoverPresenter presenterMover,
            PlayerResurrect playerResurrect,
            EndGameScreen endScreen,
            HudWindow hudWindow,
            LeaderboardYG leaderboard,
            SoundSwitcher soundSwitcher)
        {
            _menu = menu;
            _presenterMover = presenterMover;
            _presenter = presenter;
            _playerResurrect = playerResurrect;
            _endScreen = endScreen;
            _hudWindow = hudWindow;
            _leaderboard = leaderboard;
            _soundSwitcher = soundSwitcher;
        }

        public event Action GameEnded;

        public void Enable()
        {
            YandexGame.GetDataEvent += Load;
            _presenter.GameEnded += End;
            _playerResurrect.Restarting += OpenWindows;
        }

        public void Disable()
        {
            YandexGame.GetDataEvent -= Load;
            _presenter.GameEnded -= End;
            _playerResurrect.Restarting -= OpenWindows;
        }

        private void End()
        {
            _soundSwitcher.Pause("Music");

            _presenterMover.EndPlayerMove();
            _playerResurrect.OpenWindow();
            _menu.SetDistance(_presenter.TakeTotalDistance());
            YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(_presenter.TakeTotalDistance()));
            _leaderboard.NewScore(Convert.ToInt32(_presenter.TakeTotalDistance()));
            _leaderboard.UpdateLB();

            Save();
        }

        private void OpenWindows()
        {
            _soundSwitcher.Play("GameOver");
            _soundSwitcher.Stop("Music");

            _menu.GetComponent<MenuWindow>().OpenWithoutSound();
            _hudWindow.CloseWithoutSound();
            _soundSwitcher.UnPause("Music2");

            GameEnded?.Invoke();
        }

        private void Save()
        {
            YandexGame.savesData.Record = _presenter.TakeTotalDistance();
            YandexGame.SaveProgress();
        }

        private void Load()
        {
            float record = YandexGame.savesData.Record;

            _menu.SetDistance(record);
            YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(record));
            _leaderboard.NewScore(Convert.ToInt32(record));
            _leaderboard.UpdateLB();
        }
    }
}