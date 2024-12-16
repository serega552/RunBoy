using Audio;
using Chunks;
using Player;
using ShopSystem;
using StatesGame;
using UI;
using UnityEngine;
using Windows;
using YG;

namespace Initers
{
    public class GameStatesCreater : MonoBehaviour
    {
        [SerializeField] private PlayerMoverView _playerMover;
        [SerializeField] private PlayerView _player;
        [SerializeField] private Menu _menu;
        [SerializeField] private EndGameScreen _endGameScreen;
        [SerializeField] private ChunksPlacer _chunksPlacer;
        [SerializeField] private ChunksPlacer _backgroundChunksPlacer;
        [SerializeField] private HudWindow _hudWindow;
        [SerializeField] private PlayerResurrect _playerResurrect;
        [SerializeField] private LeaderboardYG _leaderboard;
        [SerializeField] private SoundSwitcher _soundSwitcher;
        [SerializeField] private SkinSelecter _skinSelecter;

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

            _skinSelecter.SkinChanging += RefreshStates;
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

            _skinSelecter.SkinChanging -= RefreshStates;
        }

        private void RefreshStates(PlayerView player)
        {
            _player = player;
            _playerMover = player.GetComponent<PlayerMoverView>();

            _restartStateGame.AddPlayer(_player, _playerMover);
            _endStateGame.AddPlayer(_player, _playerMover);
            _resurrectStateGame.AddPlayer(_player, _playerMover);
            _startStateGame.AddPlayer(_player, _playerMover);
        }

        private void CreateStates()
        {
            _restartStateGame = new RestartStateGame(
                _player,
                _playerMover,
                _chunksPlacer,
                _backgroundChunksPlacer,
                _playerResurrect);

            _endStateGame = new EndStateGame(
                _menu,
                _player,
                _playerMover,
                _playerResurrect,
                _hudWindow,
                _leaderboard,
                _soundSwitcher);

            _resurrectStateGame = new ResurrectStateGame(
                _player,
                _playerMover,
                _playerResurrect);

            _startStateGame = new StartStateGame(
                _menu,
                _player,
                _playerMover,
                _hudWindow,
                _soundSwitcher);
        }
    }
}