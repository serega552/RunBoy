using UnityEngine;

public class Ceiling : MonoBehaviour
{
    private Ceilings _ceilings;

    private void Start()
    {
        _ceilings = GetComponentInParent<Ceilings>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMoverView player))
        {
            player.Somersault();
            _ceilings.DisableColliders();
        }
    }
}
