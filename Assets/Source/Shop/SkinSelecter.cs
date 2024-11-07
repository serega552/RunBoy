using System;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class SkinSelecter : MonoBehaviour
{
    [SerializeField] private Skin _firstSkin;

    private List<Skin> _boughtSkins = new List<Skin>();
    private Skin _selectedSkin;

    public event Action<PlayerView> OnChangingSkin;

    public PlayerView Player { get; private set; }

    public void InitFirstSkin()
    {
        _selectedSkin = _firstSkin;
        _selectedSkin.Unlock();
        _selectedSkin.ChangeStatus();
        AddSkin(_selectedSkin);
        InitSkin();
    }

    public void AddSkin(Skin skin)
    {
        _boughtSkins.Add(skin);
    }

    public void SelectSkin(Skin skin)
    {
        if (_selectedSkin != null)
            _selectedSkin.ChangeStatus();

        _selectedSkin = skin;

        _selectedSkin.ChangeStatus();
        InitSkin();
    }

    private void InitSkin()
    {
        foreach (Skin skin in _boughtSkins)
        {
            if (skin.IsSelected == false)
            {
                skin.TurnOffSkin();
            }
        }

        Player = _selectedSkin.GetView();
        OnChangingSkin?.Invoke(Player);

        Save();
    }

    private void Save()
    {
        for (int i = 0; i < _boughtSkins.Count; i++)
        {
            if (YandexGame.savesData.BoughtSkins.Contains(_boughtSkins[i].Id) == false)
                YandexGame.savesData.BoughtSkins.Add(_boughtSkins[i].Id);
        }

        YandexGame.savesData.SelectedSkin = _selectedSkin.Id;
        YandexGame.SaveProgress();
    }
}
