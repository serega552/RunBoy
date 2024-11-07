using UnityEngine;

public class Skin : Product
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private GameObject _prefabSkin;

    public PlayerView GetView()
    {
        return _playerView;
    }

    public GameObject GetPrefab()
    {
        return _prefabSkin;
    }

    public void TurnOffSkin()
    {
        _playerView.gameObject.SetActive(false);
    }
}
