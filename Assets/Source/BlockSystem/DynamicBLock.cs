using System;
using System.Collections;
using Player;
using UnityEngine;

namespace BlockSystem
{
    public class DynamicBLock : MonoBehaviour
    {
        private readonly int _fallState = Animator.StringToHash("Hit");
        private readonly int _reward = 5;

        [SerializeField] private ParticleSystem _fall;
        [SerializeField] private ParticleSystem _moneyDrop;
        [SerializeField] private GameObject _model;

        private Collider _collider;
        private Animator _animator;

        public event Action<float> Hiting;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _collider = GetComponentInChildren<Collider>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerView playerView))
            {
                PlayerMoverView mover = collider.GetComponent<PlayerMoverView>();
                mover.OnKick();
                StartCoroutine(ExecuteActionsSequence(playerView, mover));
            }
        }

        private IEnumerator ExecuteActionsSequence(PlayerView playerView, PlayerMoverView mover)
        {
            Hiting?.Invoke(mover.CurrentSpeed);

            _animator.Play(_fallState);
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
            _collider.enabled = false;

            _fall.Play();
            yield return new WaitForSeconds(_fall.main.duration);

            _moneyDrop.Play();
            _model.SetActive(false);

            Robbery(playerView);
        }

        private void Robbery(PlayerView playerView)
        {
            playerView.TryToAddMoney(_reward, false);
            Invoke(nameof(Destroy), 1f);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}