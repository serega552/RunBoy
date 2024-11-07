using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Chuchpan : MonoBehaviour
{
    private readonly int FallState = Animator.StringToHash("Hit");

    [SerializeField] private ParticleSystem _fall;
    [SerializeField] private ParticleSystem _moneyDrop;
    [SerializeField] private GameObject _model;

    private int _reward = 5;
    private Collider _collider;
    private Animator _animator;

    public event UnityAction<float> OnHit;

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
            mover.Kick();
            StartCoroutine(ExecuteActionsSequence(playerView, mover));
        }
    }

    private IEnumerator ExecuteActionsSequence(PlayerView playerView, PlayerMoverView mover)
    {
        OnHit?.Invoke(mover.CurrentSpeed);

        _animator.Play(FallState);
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
        playerView.AddMoney(_reward,false);
        Invoke("Destroy", 1f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
