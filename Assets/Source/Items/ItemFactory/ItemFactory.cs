using UnityEngine;

public abstract class ItemFactory : MonoBehaviour
{
    public virtual GameObject CreateItem(GameObject prefab, Chunk chunk, PlayerView player)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition(chunk, player);
        return InstantiateItem(prefab, spawnPosition);
    }

    protected GameObject InstantiateItem(GameObject prefab, Vector3 spawnPosition)
    {
        return Instantiate(prefab, spawnPosition, Quaternion.Euler(0,90,0));
    }

    protected Vector3 GetRandomSpawnPosition(Chunk chunk, PlayerView player)
    {
        Collider chunkCollider = chunk.GetComponent<Collider>();
        Bounds bounds = chunkCollider.bounds;
        Vector3 chunkCenter = chunk.transform.position;
        Vector3 extents = bounds.extents;

        float randomX = chunkCenter.x + Random.Range(-extents.x, extents.x);
        float randomY = bounds.max.y + 0.3f;
        float randomZ = chunkCenter.z + Random.Range(-extents.z, extents.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
