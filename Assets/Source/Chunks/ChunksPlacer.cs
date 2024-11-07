using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    [SerializeField] private Chunk[] _chunks;
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private int _spawnLenght = 40;

    private float _disableLenght = 5f;
    private Vector3 _startFirstChunkPosition;
    private Transform _player;
    private List<Chunk> _disabledChunks = new List<Chunk>();
    private List<Chunk> _spawnedChunks = new List<Chunk>();

    public Chunk[] Chunks => _chunks;

    private void Start()
    {
        _startFirstChunkPosition = _firstChunk.transform.position;
        _spawnedChunks.Add(_firstChunk);
    }

    private void Update()
    {
        if (_player.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.transform.position.z - _spawnLenght && _player != null)
        {
            SpawnChunk();
        }

        if(_player.position.z > _spawnedChunks[0].End.transform.position.z + _disableLenght)
        {
            DisableChunks();
        }
    }

    public void GetPlayerTransform(Transform player)
    {
        _player = player;
    }

    public void ResetFirstChunk()
    {
        foreach (Chunk chunk in _spawnedChunks)
        {
            chunk.gameObject.SetActive(false);
        }

        _spawnedChunks.Clear();

        _firstChunk.gameObject.SetActive(true);
        _firstChunk.transform.position = _startFirstChunkPosition;
        _spawnedChunks.Add(_firstChunk);
    }

    private void SpawnChunk()
    {
        foreach (Chunk chunk in _chunks)
        {
            if (chunk.gameObject.activeSelf == false)
            {
                _disabledChunks.Add(chunk);
            }
        }

        Chunk newChunk = _disabledChunks[Random.Range(0, _disabledChunks.Count)];

        foreach (Chunk chunk in _chunks)
        {
            if (chunk.gameObject.activeSelf == false)
            {
                _disabledChunks.Remove(chunk);
            }
        }

        float position = _spawnedChunks[_spawnedChunks.Count - 1].LenghChunk.transform.localScale.z;

        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].LenghChunk.transform.position;
        newChunk.transform.position += new Vector3(0, 0, position);
        newChunk.gameObject.SetActive(true);

        _spawnedChunks.Add(newChunk);
    }

    private void DisableChunks()
    {
        for (int i = 0; i < _spawnedChunks.Count - 1; i++)
        {
            _spawnedChunks[i].gameObject.SetActive(false);
            _spawnedChunks.Remove(_spawnedChunks[i]);
        }
    }
}