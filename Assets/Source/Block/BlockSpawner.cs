using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks;

    private Dictionary<Chunk, List<GameObject>> _spawnedBlocks = new Dictionary<Chunk, List<GameObject>>();
    private Dictionary<Chunk, int> _chunkSpawnCount = new Dictionary<Chunk, int>();

    private int _minCount = 3;
    private int _baseMaxCount = 7;

    public void SpawnBlocks(Chunk chunk)
    {
        if (!_chunkSpawnCount.ContainsKey(chunk))
        {
            _chunkSpawnCount[chunk] = _chunkSpawnCount.Count + 1;
        }

        int maxCount = _baseMaxCount + (_chunkSpawnCount[chunk] - 1) * 2;
        int count = GetCountToSpawn(_minCount, maxCount);

        if (_chunkSpawnCount[chunk] == 1)
        {
            return;
        }

        if (!_spawnedBlocks.ContainsKey(chunk))
        {
            _spawnedBlocks[chunk] = new List<GameObject>();
        }

        Vector3 spawnPossition;

        for (int i = 0; i < count; i++)
        {
            do
            {
                spawnPossition = GetRandomSpawnPosition(chunk);
            }
            while (CheckCollision(spawnPossition));

            Block block = SelectBlock();
            GameObject blockObj = Instantiate(block.gameObject, spawnPossition, Quaternion.identity);
            _spawnedBlocks[chunk].Add(blockObj);
        }
    }

    public void RemoveBlocks(Chunk chunk)
    {
        if (_spawnedBlocks.ContainsKey(chunk))
        {
            foreach (GameObject block in _spawnedBlocks[chunk])
            {
                Destroy(block);
            }
            _spawnedBlocks[chunk].Clear();
        }
    }

    private bool CheckCollision(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Item>())
            {
                return true;
            }
            
            if (hitCollider.gameObject.GetComponent<Block>())
            {
                return false;
            }
        }
        return false;
    }

    private Block SelectBlock()
    {
        Block block = _blocks[Random.Range(0, _blocks.Count)];
        return block;
    }

    private int GetCountToSpawn(int minCount, int maxCount)
    {
        return Random.Range(minCount, maxCount);
    }

    private Vector3 GetRandomSpawnPosition(Chunk chunk)
    {
        Collider chunkCollider = chunk.GetComponent<Collider>();
        Bounds bounds = chunkCollider.bounds;
        Vector3 chunkCenter = chunk.transform.position;
        Vector3 extents = bounds.extents;

        float minZ = chunkCenter.z - extents.z + 5f; 
        float maxZ = chunkCenter.z + extents.z - 5f;

        minZ = Mathf.Min(minZ, maxZ);
        maxZ = Mathf.Max(minZ, maxZ);

        float randomX = chunkCenter.x + Random.Range(-extents.x, extents.x);
        float randomY = chunkCenter.y;
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, randomY, randomZ);
    }
}
