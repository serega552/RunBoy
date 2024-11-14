using Player;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        protected float Delay = 2f;
        protected float Resourses;
        protected ParticleSystem ExplosionParticle;
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
                GetMoverResourses(PlayerMoverView);
                GetResourses(PlayerView);
                StartDestroy();
            }
        }

        protected virtual void GetResourses(PlayerView playerView) { }

        protected virtual void GetMoverResourses(PlayerMoverView playerMoverView) { }

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