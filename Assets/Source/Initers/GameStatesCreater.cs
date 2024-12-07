using Audio;
using Chunks;
using Player;
using StatesGame;
using UI;
using UnityEngine;
using Windows;
using YG;

namespace Initers
{
    public class GameStatesCreater : MonoBehaviour
    {
        [SerializeField] private PlayerMoverPresenter _playerMoverPresenter;
        [SerializeField] private PlayerPresenter _playerPresenter;
        [SerializeField] private Menu _menu;
        [SerializeField] private EndGameScreen _endGameScreen;
        [SerializeField] private ChunksPlacer _chunksPlacer;
        [SerializeField] private ChunksPlacer _backgroundChunksPlacer;
        [SerializeField] private HudWindow _hudWindow;
        [SerializeField] private PlayerResurrect _playerResurrect;
        [SerializeField] private LeaderboardYG _leaderboard;
        [SerializeField] private SoundSwitcher _soundSwitcher;

        private ResurrectStateGame _resurrectStateGame;
        private RestartStateGame _restartStateGame;
        private StartStateGame _startStateGame;
        private EndStateGame _endStateGame;

        private void OnEnable()
        {
            _resurrectStateGame.Enable();
            _restartStateGame.Enable();
            _startStateGame.Enable();
            _endStateGame.Enable();
        }

        private void Awake()
        {
            CreateStates();
        }

        private void OnDisable()
        {
            _resurrectStateGame.Disable();
            _restartStateGame.Disable();
            _startStateGame.Disable();
            _endStateGame.Disable();
        }

        private void CreateStates()
        {
            _restartStateGame = new RestartStateGame(
                _playerPresenter,
                _playerMoverPresenter,
                _chunksPlacer,
                _backgroundChunksPlacer,
                _playerResurrect);

            _endStateGame = new EndStateGame(
                _menu,
                _playerPresenter,
                _playerMoverPresenter,
                _playerResurrect,
                _endGameScreen,
                _hudWindow,
                _leaderboard,
                _soundSwitcher);

            _resurrectStateGame = new ResurrectStateGame(
                _playerPresenter,
                _playerMoverPresenter,
                _playerResurrect);

            _startStateGame = new StartStateGame(
                _menu,
                _playerPresenter,
                _playerMoverPresenter,
                _hudWindow,
                _soundSwitcher);
        }
    }
}