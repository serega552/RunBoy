using System.Collections.Generic;
using UnityEngine;
using YG;

public class DanceSelecter : MonoBehaviour
{
    [SerializeField] private Dance _firstDance;

    private List<Dance> _boughtDances = new List<Dance>();
    private Dance _selectedDance;
    private ShopDancing _shopDancing;

    private void Awake()
    {
        _shopDancing = GetComponent<ShopDancing>();
    }

    public void InitFirstDance()
    {
        _selectedDance = _firstDance;
        _firstDance.ChangeStatus();
        _firstDance.Unlock();
        AddDance(_firstDance);
        ChooseDance();
    }

    public void AddDance(Dance dance)
    {
        _boughtDances.Add(dance);
        Save();
    }

    public void SelectDance(Dance dance)
    {
        if (_selectedDance != null)
            _selectedDance.ChangeStatus();

        _selectedDance = dance;
        _selectedDance.ChangeStatus();

        ChooseDance();
    }

    public void ChooseDance()
    {
        if (_selectedDance != null)
        {
            _shopDancing.Player.GetNameDance(_selectedDance.NameDanceAnim);
            Save();
        }
    }

    private void Save()
    {
        for (int i = 0; i < _boughtDances.Count; i++)
        {
            if (YandexGame.savesData.BoughtDances.Contains(_boughtDances[i].Id) == false)
                YandexGame.savesData.BoughtDances.Add(_boughtDances[i].Id);
        }

        if (_selectedDance != null)
            YandexGame.savesData.SelectedDance = _selectedDance.Id;

        YandexGame.SaveProgress();

        YandexGame.SaveProgress();
    }
}