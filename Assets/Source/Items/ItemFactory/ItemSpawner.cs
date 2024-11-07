using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private int _itemSpawnCount = 7;
    [SerializeField] private List<GameObject> _itemPrefabs;
    [SerializeField] private ChunksPlacer _chunksPlacer;
    [SerializeField] private PlayerView _player;
    [SerializeField] private BoostItemFactory _boostItemFactory;
    [SerializeField] private OtherItemFactory _otherItemFactory;
    [SerializeField] private BlockSpawner _blockSpawner;

    private PlayerMoverView _mover;
    private Dictionary<Chunk, List<GameObject>> _spawnedItems = new Dictionary<Chunk, List<GameObject>>();

    private void OnEnable()
    {
        _player = GetComponentInParent<PlayerView>();
        _mover = GetComponentInParent<PlayerMoverView>();

        _mover.OnStarted += SpawnFirstChunk;

        foreach (var chunk in _chunksPlacer.Chunks)
        {
            chunk.OnSpawned += OnChunkSpawned;
            chunk.OnDeactivated += OnChunkDeactivated;
        }
    }

    private void OnDisable()
    {
        _mover.OnStarted -= SpawnFirstChunk;

        foreach (var chunk in _chunksPlacer.Chunks)
        {
            chunk.OnSpawned -= OnChunkSpawned;
            chunk.OnDeactivated -= OnChunkDeactivated;
        }
    }

    private void SpawnFirstChunk()
    {
        OnChunkSpawned(_chunksPlacer.Chunks[0]);
        OnChunkSpawned(_chunksPlacer.Chunks[0]);
    }

    private void OnChunkSpawned(Chunk chunk)
    {
        int itemSpawnCount = Random.Range(5, _itemSpawnCount);

        for (int i = 0; i < itemSpawnCount; i++)
        {
            GameObject randomItemPrefab = _itemPrefabs[Random.Range(0, _itemPrefabs.Count)];
            ItemFactory factory = ChooseFactory(randomItemPrefab);
            GameObject spawnedItem = factory.CreateItem(randomItemPrefab, chunk, _player);

            if (!_spawnedItems.ContainsKey(chunk))
                _spawnedItems[chunk] = new List<GameObject>();

            _spawnedItems[chunk].Add(spawnedItem);
        }

        _blockSpawner.SpawnBlocks(chunk);
    }


    private void OnChunkDeactivated(Chunk chunk)
    {
        if (!_spawnedItems.ContainsKey(chunk)) return;

        foreach (var item in _spawnedItems[chunk])
            Destroy(item);

        _spawnedItems[chunk].Clear();
        _blockSpawner.RemoveBlocks(chunk);
    }

    private ItemFactory ChooseFactory(GameObject prefab)
    {
        if (prefab.GetComponent<OtherItem>() != null)
        {
            return _otherItemFactory;
        }
        else
        {
            return _boostItemFactory;
        }
    }
}