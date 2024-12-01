using System.Collections.Generic;
using Chunks;
using Items;
using UnityEngine;

namespace BlockSystem
{
    public class BlockSpawner : MonoBehaviour
    {
        private readonly Dictionary<Chunk, List<GameObject>> _spawnedBlocks = new Dictionary<Chunk, List<GameObject>>();
        private readonly Dictionary<Chunk, int> _chunkSpawnCount = new Dictionary<Chunk, int>();
        private readonly int _minCount = 3;
        private readonly int _maxCountMultiply = 2;
        private readonly int _baseMaxCount = 7;
        private readonly float _radiusCollider = 0.5f;
        private readonly float _positionNumber = 5f;

        [SerializeField] private List<Block> _blocks;

        private Collider[] _colliderBuffer = new Collider[10];

        public void SpawnBlocks(Chunk chunk)
        {
            if (!_chunkSpawnCount.ContainsKey(chunk))
            {
                _chunkSpawnCount[chunk] = _chunkSpawnCount.Count + 1;
            }

            int maxCount = _baseMaxCount + (_chunkSpawnCount[chunk] - 1) * _maxCountMultiply;
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
            int colliderCount = Physics.OverlapSphereNonAlloc(position, _radiusCollider, _colliderBuffer);

            for (int i = 0; i < colliderCount; i++)
            {
                if (_colliderBuffer[i].GetComponent<Block>())
                {
                    return false;
                }

                if (_colliderBuffer[i].GetComponent<Item>())
                {
                    return true;
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

            float minZ = chunkCenter.z - extents.z + _positionNumber;
            float maxZ = chunkCenter.z + extents.z - _positionNumber;

            minZ = Mathf.Min(minZ, maxZ);
            maxZ = Mathf.Max(minZ, maxZ);

            float randomX = chunkCenter.x + Random.Range(-extents.x, extents.x);
            float randomY = chunkCenter.y;
            float randomZ = Random.Range(minZ, maxZ);

            return new Vector3(randomX, randomY, randomZ);
        }
    }
}