using System;
using Audio;
using Player;
using UI;
using UnityEngine;
using Windows;
using YG;

namespace StatesGame
{
    public class EndStateGame
    {
        private readonly Menu _menu;
        private readonly EndGameScreen _endScreen;
        private readonly PlayerResurrect _playerResurrect;
        private readonly HudWindow _hudWindow;
        private readonly LeaderboardYG _leaderboard;
        private readonly SoundSwitcher _soundSwitcher;

        private PlayerMoverView _playerMover;
        private PlayerView _player;

        public EndStateGame(
            Menu menu,
            PlayerView player,
            PlayerMoverView playerMover,
            PlayerResurrect playerResurrect,
            EndGameScreen endScreen,
            HudWindow hudWindow,
            LeaderboardYG leaderboard,
            SoundSwitcher soundSwitcher)
        {
            _menu = menu;
            _playerMover = playerMover;
            _player = player;
            _playerResurrect = playerResurrect;
            _endScreen = endScreen;
            _hudWindow = hudWindow;
            _leaderboard = leaderboard;
            _soundSwitcher = soundSwitcher;
        }

        public void Enable()
        {
            YandexGame.GetDataEvent += Load;
            _player.GameEnded += End;
            _playerResurrect.Restarting += OpenWindows;
        }

        public void Disable()
        {
            YandexGame.GetDataEvent -= Load;
            _player.GameEnded -= End;
            _playerResurrect.Restarting -= OpenWindows;
        }

        public void AddPlayer(PlayerView player, PlayerMoverView playerMover)
        {
            _player.GameEnded -= End;

            _player = player;
            _playerMover = playerMover;

            _player.GameEnded += End;
        }

        private void End()
        {
            Debug.Log("End");

            _soundSwitcher.Pause("Music");

            _playerMover.EndMove();
            _playerResurrect.OpenWindow();
            _menu.SetDistance(_player.TakeTotalDistance());
            YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(_player.TakeTotalDistance()));
            _leaderboard.NewScore(Convert.ToInt32(_player.TakeTotalDistance()));
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
        }

        private void Save()
        {
            YandexGame.savesData.Record = _player.TakeTotalDistance();
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