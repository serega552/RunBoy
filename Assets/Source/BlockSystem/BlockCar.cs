using Audio;
using Player;
using UnityEngine;

namespace BlockSystem
{
    public class BlockCar : Block
    {
        protected override void Activate(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerMoverView player))
            {
                player.CrashOnCar();
                Instantiate(CrashParticle.gameObject, transform.position, transform.rotation);
                GetComponent<Collider>().enabled = false;

                Invoke(nameof(Destroy), 0.1f);
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}