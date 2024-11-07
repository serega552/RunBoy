using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShopSkins : Shop
{
    [SerializeField] private List<Skin> _skinsForSale;

    private Skin _selectedSkin;
    private SkinSelecter _selecter;

    private void Awake()
    {
        _selecter = GetComponent<SkinSelecter>();

        if (YandexGame.SDKEnabled)
            Load();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += Load;

        BuyButton.onClick.AddListener(TryBuyProduct);
        SelectButton.onClick.AddListener(SelectProduct);

        foreach (var skin in _skinsForSale)
        {
            skin.OnSelected += ShowInfoProduct;
        }
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Load;

        BuyButton.onClick.RemoveListener(TryBuyProduct);
        SelectButton.onClick.RemoveListener(SelectProduct);

        foreach (var skin in _skinsForSale)
        {
            skin.OnSelected -= ShowInfoProduct;
        }
    }

    public override void TurnOnModel()
    {
        base.TurnOnModel();
        ModelSkin?.GetComponent<Animator>().Play("Idle");
    }

    public override void BuyProduct()
    {
        base.BuyProduct();
        _selectedSkin.Unlock();
        _selecter.AddSkin(_selectedSkin);
        _selecter.SelectSkin(_selectedSkin);
    }

    public override void ShowInfoProduct(Product skin)
    {
        _selectedSkin = skin.GetComponent<Skin>();
        Description.text = _selectedSkin.Description;

        if (_selectedSkin.IsBought)
        {
            SelectButton.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(false);
        }
        else
        {
            BuyButton.gameObject.SetActive(true);
            SelectButton.gameObject.SetActive(false);
        }

        SpawnSkin(_selectedSkin);
    }

    public override void SelectProduct()
    {
        _selecter.SelectSkin(_selectedSkin);
    }

    public override void TryBuyProduct()
    {
        if (BankMoney.TryTakeMoney(_selectedSkin.Price))
        {
            BankMoney.TakeMoney(_selectedSkin.Price);
            BuyProduct();
        }
    }

    private void Load()
    {
        List<int> boughtSkinsId = YandexGame.savesData.BoughtSkins;
        int selectedSkinId = YandexGame.savesData.SelectedSkin;

        if (boughtSkinsId.Count != 0)
        {
            for (int i = 0; i < _skinsForSale.Count; i++)
            {
                for (int j = 0; j < boughtSkinsId.Count; j++)
                {
                    if (_skinsForSale[i].Id == boughtSkinsId[j])
                    {
                        _selectedSkin = _skinsForSale[i];
                        base.BuyProduct();
                        _selectedSkin.Unlock();
                        _selecter.AddSkin(_selectedSkin);
                    }

                    if(_skinsForSale[i].Id == selectedSkinId)
                    {
                        _selecter.SelectSkin(_selectedSkin);
                        break;
                    }
                }
            }
        }
        else
        {
            _selecter.InitFirstSkin();
        }
    }
}
