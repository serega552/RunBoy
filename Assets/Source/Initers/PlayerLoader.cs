using Chunks;
using FunDanceSystem;
using Player;
using PlayerEffects;
using ShopSystem;
using UI;
using UnityEngine;

namespace Initers
{
    public class PlayerLoader : MonoBehaviour
    {
        [SerializeField] private Menu _viewMenu;
        [SerializeField] private SkinSelecter _skinSelecter;
        [SerializeField] private Shop _danceShop;
        [SerializeField] private Shop _skinShop;
        [SerializeField] private ChunksPlacer _chunksPlacer;
        [SerializeField] private ChunksPlacer _backChunksPlacer;
        [SerializeField] private PlayerEffectsSelecter _playerEffectsSelecter;
        [SerializeField] private FunDance _funDance;
        [SerializeField] private PlayerAnimatorController _playerAnimatorController;

        private PlayerEffectController _playerEffects;
        private Rigidbody _rigidbody;
        private PlayerView _view;
        private PlayerMoverView _viewMover;
        private Animator _animator;

        private void OnEnable()
        {
            _skinSelecter.SkinChanging += LoadPlayer;
        }

        private void OnDisable()
        {
            _skinSelecter.SkinChanging -= LoadPlayer;
        }

        public void LoadPlayer(PlayerView playerView)
        {
            playerView.gameObject.SetActive(true);

            _view = playerView;
            _viewMover = playerView.GetComponent<PlayerMoverView>();
            _animator = playerView.GetComponent<Animator>();
            _rigidbody = playerView.GetComponent<Rigidbody>();
            _chunksPlacer.AddPlayerTransform(playerView.transform);
            _backChunksPlacer.AddPlayerTransform(playerView.transform);

            _funDance.Init(_viewMover);
            _playerAnimatorController.Init(_viewMover, _animator);
            _playerEffectsSelecter.InitPlayer(_view);
            _playerEffects = _playerEffectsSelecter.GetEffects();
            _playerEffects.Init(_viewMover, _view);
            _chunksPlacer.AddPlayerTransform(playerView.transform);

            _danceShop.AddView(_viewMover);
            _skinShop.AddView(_viewMover);
        }
    }
}