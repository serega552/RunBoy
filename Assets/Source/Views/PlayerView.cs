using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _distance;
    [SerializeField] private TMP_Text _energy;
    [SerializeField] private TMP_Text _maxEnergy;
    [SerializeField] private HudWindow _headUpDisplay;
    [SerializeField] private EnergyBoost _energyBoost;
    [SerializeField] private MoneyBoost _moneyBoost;
    [SerializeField] private EnergyUpgrade _energyUpgrade;
    [SerializeField] private Bank _bank;

    private Button _energyBoostButton;
    private Button _moneyBoostButton;
    private Button _energyUpgradeButton;
    private bool _isMoneyBoost = false;
    private float _moneyBoostTime;

    public event Action<float> OnEnergyChanging;
    public event Action<float> OnMaxEnergyChanging;
    public event Action<float, bool> OnMoneyChanging;
    public event Action<EnergyBoost> OnDistanceBoostChanging;
    public event Action OnGameOvered;

    private void Awake()
    {
        _moneyBoostButton = _moneyBoost.GetComponent<Button>();
        _energyBoostButton = _energyBoost.GetComponent<Button>();
        _energyUpgradeButton = _energyUpgrade.GetComponent<Button>();

        UpdateUI(0);
    }

    private void OnEnable()
    {
        _moneyBoostButton.onClick.AddListener(UseMoneyBoost);
        _energyBoostButton.onClick.AddListener(UseEnergyBoost);
        _energyUpgradeButton.onClick.AddListener(OnChangeMaxEnergy);
    }

    private void OnDisable()
    {
        _moneyBoostButton.onClick.RemoveListener(UseMoneyBoost);
        _energyBoostButton.onClick.RemoveListener(UseEnergyBoost);
        _energyUpgradeButton.onClick.RemoveListener(OnChangeMaxEnergy);
    }

    public void GameOver()
    {
        OnGameOvered?.Invoke();
    }

    public void OnEnergyChanged(float energyAmount)
    {
        AudioManager.Instance.Play("UseBoost");
        OnEnergyChanging?.Invoke(energyAmount);
    }

    public void OnChangeMaxEnergy()
    {
        OnMaxEnergyChanging?.Invoke(_energyUpgrade.Upgrade());
    }

    public void SetDistance(float distance)
    {
        _distance.text = $"{Convert.ToInt32(distance)}";
    }

    public void SetEnergy(float energyAmount)
    {
        _energy.text = $"{Convert.ToInt32(energyAmount)}";
    }

    public void UpdateUI(float maxEnergy)
    {
        _maxEnergy.text = maxEnergy.ToString();

        if(maxEnergy == 0)
            _maxEnergy.text = YandexGame.savesData.MaxEnergy.ToString();
    }

    public void AddMoney(int count, bool isBoost)
    {
        if (_isMoneyBoost || isBoost)
        {
            OnMoneyChanging?.Invoke(count * _moneyBoost.Bonus, isBoost);
            _bank.GiveMoneyForGame(count * Convert.ToInt32(_moneyBoost.Bonus));
        }
        else
        {
            _bank.GiveMoneyForGame(count);
        }
    }

    public void SetEnergyTime(float time)
    {
        _energyBoost.SetTimeText(time);
    }

    private void UseMoneyBoost()
    {
        if (_moneyBoost.TryUse())
        {
            _isMoneyBoost = true;
            _moneyBoostTime = _moneyBoost.Time;
            StartCoroutine(TimeChanging());
        }
        else
            Debug.Log("ErrorUseBoost");
    }

    private void UseEnergyBoost()
    {
        if (_energyBoost.TryUse())
            OnDistanceBoostChanging?.Invoke(_energyBoost);
        else
            Debug.Log("ErrorUseBoost");
    }

    private IEnumerator TimeChanging()
    {
        while (_isMoneyBoost)
        {
            _moneyBoostTime -= Time.deltaTime;
            _moneyBoost.SetTimeText(_moneyBoostTime);

            if (_moneyBoostTime <= 0)
                _isMoneyBoost = false;

            yield return null;
        }
    }
}
