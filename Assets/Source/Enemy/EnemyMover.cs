using UnityEngine;
using DG.Tweening;
using Player;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _kickStateHash = Animator.StringToHash("Kick");

        [SerializeField] private GameObject _enemy;
        [SerializeField] private PlayerMoverView _player;
        [SerializeField] private float _moveDistance = 10f;
        [SerializeField] private float _duration = 5f;

        private Animator _animator;
        private Vector3 _startPositionEnemy;

        private void OnEnable()
        {
            _animator = _enemy.GetComponent<Animator>();
            _player.OnStarted += StartMove;
            _player.OnStoped += EndMove;
            _player.OnRestart += ResetEnemy;

            _animator.Play(_idleStateHash);
        }

        private void Awake()
        {
            _startPositionEnemy = _enemy.transform.position;
        }

        private void StartMove()
        {
            Vector3 endPosition = _enemy.transform.position + Vector3.forward * _moveDistance;

            _enemy.transform.DOMove(endPosition, _duration).SetEase(Ease.Linear);
            _animator.Play(_runStateHash);
        }

        private void EndMove()
        {
            Vector3 endPosition = new Vector3(_player.transform.position.x - 0.2f, _player.transform.position.y, _player.transform.position.z - 0.2f);

            _enemy.transform.DOMove(endPosition, 0.7f).SetEase(Ease.Linear).OnComplete(HitPlayer);
        }

        private void ResetEnemy()
        {
            _enemy.transform.position = _startPositionEnemy;
            _animator.Play(_idleStateHash);
        }

        private void HitPlayer()
        {
            _animator.Play(_kickStateHash);
        }
    }
}