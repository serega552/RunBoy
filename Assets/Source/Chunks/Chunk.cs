using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    public event UnityAction<Chunk> OnSpawned;
    public event UnityAction<Chunk> OnDeactivated;

    public BeginPoint Begin { get; private set; }
    public EndPoint End { get; private set; }
    public LenghtChunk LenghChunk { get; private set; }

    private void Awake()
    {
        Begin = GetComponentInChildren<BeginPoint>();
        End = GetComponentInChildren<EndPoint>();
        LenghChunk = GetComponentInChildren<LenghtChunk>();
    }

    private void OnEnable()
    {
        OnSpawned?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDeactivated?.Invoke(this);
    }
}
