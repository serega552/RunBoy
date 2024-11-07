using System.Collections.Generic;
using UnityEngine;

public class Ceilings : MonoBehaviour
{
    private List<Collider> _colliders = new List<Collider>();

    private void Awake()
    {
        Collider[] collidersCelling = GetComponentsInChildren<Collider>();
        Collider[] collidersBlock = GetComponents<Collider>();
        _colliders.AddRange(collidersCelling);
        _colliders.AddRange(collidersBlock);
    }

    public void DisableColliders()
    {
        foreach (Collider collider in _colliders)
        {
            collider.enabled = false;
        }
    }
}
