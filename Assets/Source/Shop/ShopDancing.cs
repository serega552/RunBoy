using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShopDancing : Shop
{
    [SerializeField] private List<Dance> _dancesForSale;

    private Dance _selectedDance;
    private DanceSelecter _selecter;

    private void Awake()
    {
        _selecter = GetComponent<DanceSelecter>();
    }

    private void OnEnable()
    {
        BuyButton.onClick.AddListener(TryBuyProduct);
        SelectButton.onClick.AddListener(SelectProduct);
        OnChangingSkin += ChooseDance;

        foreach (var dance in _dancesForSale)
        {
            dance.OnSelected += ShowInfoProduct;
        }
    }

    private void OnDisable()
    {
        BuyButton.onClick.RemoveListener(TryBuyProduct);
        SelectButton.onClick.RemoveListener(SelectProduct);
        OnChangingSkin -= ChooseDance;

        foreach (var dance in _dancesForSale)
        {
            dance.OnSelected -= ShowInfoProduct;         
        }
    }

    public override void BuyProduct()
    {
        base.BuyProduct();
        _selectedDance.Unlock();
        _selecter.AddDance(_selectedDance);
        _dancesForSale.Remove(_selectedDance);
        SelectProduct();
    }

    public override void ShowInfoProduct(Product dance)
    {
        _selectedDance = dance.GetComponent<Dance>();
        Description.text = _selectedDance.Description;

        if (_selectedDance.IsBought)
        {
            SelectButton.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(false);
        }
        else
        {
            BuyButton.gameObject.SetActive(true);
            SelectButton.gameObject.SetActive(false);
        }

        SpawnDance(_selectedDance);
    }

    public override void SelectProduct()
    {
        _selecter.SelectDance(_selectedDance);
    }

    public override void TryBuyProduct()
    {
        if (BankMoney.TryTakeMoney(_selectedDance.Price))
        {
            BankMoney.TakeMoney(_selectedDance.Price);
            BuyProduct();
        }
        else
            ThrowErrorBuySkin();
    }

    public override void TurnOnModel()
    {
        base.TurnOnModel();
        ModelSkin?.GetComponent<Animator>().Play(_selectedDance.NameDanceAnim);
    }

    private void ChooseDance()
    {
        _selecter.ChooseDance();
        Load();
    }

    private void ThrowErrorBuySkin()
    {
        Debug.Log("ErrorBuy");
    }

    private void Load()
    {
        List<int> boughtDancesId = YandexGame.savesData.BoughtDances;
        int selectedDanceId = YandexGame.savesData.SelectedDance;

        if (boughtDancesId.Count != 0)
        {
            for (int i = 0; i < _dancesForSale.Count; i++)
            {
                for (int j = 0; j < boughtDancesId.Count; j++)
                {
                    if (_dancesForSale[i].Id == boughtDancesId[j])
                    {
                        _selectedDance = _dancesForSale[i];
                        base.BuyProduct();
                        _selectedDance.Unlock();
                        _selecter.AddDance(_selectedDance);
                    }

                    if (_dancesForSale[i].Id == selectedDanceId)
                    {
                        _selecter.SelectDance(_selectedDance);
                    }
                }
            }
        }
        else
        {
            _selecter.InitFirstDance();
        }
    }
}
