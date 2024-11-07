using UnityEngine;

public class BlockCar : Block
{
    protected override void Activate(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMoverView player))
        {
            player.CrashOnCar();
            AudioManager.Instance.Play("CarCrash");
            Instantiate(CrashParticle.gameObject, transform.position, transform.rotation);
            GetComponent<Collider>().enabled = false;

            Invoke("Destroy", 0.1f);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
