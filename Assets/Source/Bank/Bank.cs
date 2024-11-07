using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Bank : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _moneyText;
    [SerializeField] private List<TMP_Text> _diamondText;
    [SerializeField] private List<TMP_Text> _moneyForGameText;

    private int _diamond = 0;
    private int _moneyForGame;

    public event Action OnBuy;

    public int Money { get; private set; }  = 0;

    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            Load();
        }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += Load;
        AwardGiver.OnReward += GiveReward;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Load;
        AwardGiver.OnReward -= GiveReward;
    }

    public void TakeMoney(int money)
    {
        if (TryTakeMoney(money))
        {
            Money -= money;
            TaskCounter.IncereaseProgress(money, Convert.ToString(TaskType.SpendMoney));
            AudioManager.Instance.Play("Buy");
            OnBuy?.Invoke();
            UpdateText();
        }
    }

    public void UpdateText()
    {
        foreach(TMP_Text money in _moneyText)
        {
            money.text = Money.ToString();
        }
        
        foreach(TMP_Text diamond in _diamondText)
        {
            diamond.text = _diamond.ToString();
        }
        
        foreach (TMP_Text money in _moneyForGameText)
        {
            money.text = _moneyForGame.ToString();
        }

        Save();
    }

    public bool TryTakeMoney(int value)
    {
        if(Money >= value)
            return true;
        else 
            return false; 
    } 

    public bool TryTakeDiamond(int value)
    {
        if(_diamond >= value)
            return true;
        else 
            return false; 
    }

    public void GiveMoney(int money)
    {
        Money += money;
        UpdateText();
    }
    
    public void GiveDiamond(int diamond)
    {
        _diamond += diamond;
        UpdateText();
    }

    public void GiveMoneyForGame(int money)
    {
        _moneyForGame += money;
        Money += money;
        UpdateText();

        TaskCounter.IncereaseProgress(Convert.ToInt32(money), TaskType.CollectMoney.ToString());
    }

    public void MoneyMultiplyAd()
    {
        GiveMoney(_moneyForGame);
        _moneyForGame *= 2;
    }

    public void TakeDiamond(int diamond)
    {
        if (TryTakeDiamond(diamond))
        {
            _diamond -= diamond;
            UpdateText();
        }
    }

    public void ResetValueForGame()
    {
        _moneyForGame = 0;
        UpdateText();
    }

    private void GiveReward(string name, int amount)
    {
        if (name == Convert.ToString(ResourceType.Money))
        {
            GiveMoney(amount);
        }
        else if (name == Convert.ToString(ResourceType.Diamond))
        {
            GiveDiamond(amount);
        }
    }

    private void Save()
    {
        YandexGame.savesData.Money = Money;
        YandexGame.savesData.Diamond = _diamond;
        YandexGame.SaveProgress();
    }

    private void Load()
    {
        Money = YandexGame.savesData.Money;
        _diamond = YandexGame.savesData.Diamond;

        UpdateText();
    }
}
