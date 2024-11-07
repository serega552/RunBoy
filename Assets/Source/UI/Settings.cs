using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class Settings : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private Slider _sound;

    private void Start()
    {
        if(YandexGame.SDKEnabled)
            Load();
    }
    
    private void OnEnable()
    {
        YandexGame.GetDataEvent += Load;
        _sound.onValueChanged.AddListener(ChangeSound);
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Load;
        _sound.onValueChanged.RemoveListener(ChangeSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Save();
    }

    private void ChangeSound(float value)
    {
        _sound.value = value;
        AudioListener.volume = value;
    }

    private void Save()
    {
        YandexGame.savesData.WorldSound = _sound.value;
        YandexGame.SaveProgress();
    }

    private void Load()
    {
        _sound.value = YandexGame.savesData.WorldSound;
        AudioListener.volume = YandexGame.savesData.WorldSound;
    }
}
