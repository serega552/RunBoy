using Player;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        protected ParticleSystem ExplosionParticle;
        protected float Delay = 2f;
        protected PlayerView PlayerView;
        protected PlayerMoverView PlayerMoverView;

        private Collider _collider;
        private MeshRenderer _mesh;

        private void Start()
        {
            ExplosionParticle = GetComponentInChildren<ParticleSystem>();
            _mesh = GetComponentInChildren<MeshRenderer>();
            _collider = GetComponent<Collider>();
        }

        protected void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerView playerView) && collision.gameObject.TryGetComponent(out PlayerMoverView playerMoverView))
            {
                PlayerView = playerView;
                PlayerMoverView = playerMoverView;
                AddMoverResourses(PlayerMoverView);
                AddResourses(PlayerView);
                StartDestroy();
            }
        }

        protected virtual void AddResourses(PlayerView playerView) { }

        protected virtual void AddMoverResourses(PlayerMoverView playerMoverView) { }

        private void StartDestroy()
        {
            _mesh.enabled = false;
            _collider.enabled = false;
            ExplosionParticle!.Play();
            Invoke(nameof(Destroy), Delay);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}