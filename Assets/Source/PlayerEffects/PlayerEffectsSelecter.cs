using UnityEngine;

public class PlayerEffectsSelecter : MonoBehaviour
{
    private PlayerEffectController _playerEffectController;
    private PlayerView _playerView;

    public PlayerEffectController GetEffects()
    {
        return _playerEffectController;
    }

    public void InitPlayer(PlayerView view)
    {
        _playerView = view;
        _playerEffectController = _playerView.GetComponent<PlayerEffectController>();
    }
}
