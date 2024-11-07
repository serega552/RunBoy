using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Shop : MonoBehaviour
{
    [SerializeField] private Transform _placeSkin;
    [SerializeField] private Bank _bank;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private TMP_Text _description;

    public event Action OnChangingSkin;

    public Bank BankMoney => _bank;
    public Transform PlaceSkin => _placeSkin;
    public Button BuyButton => _buyButton;
    public Button SelectButton => _selectButton;
    public TMP_Text Description => _description;
    public PlayerMoverView Player { get; private set; }
    public GameObject ModelSkin { get; private set; }

    private void Start()
    {
        _buyButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(false);
    }

    public void GetView(PlayerMoverView view)
    {
        Player = view;
        OnChangingSkin?.Invoke();
    }

    public void TurnOffModel()
    {
        ModelSkin?.SetActive(false);
    }

    public void SpawnDance(Dance dance)
    {
        if (ModelSkin != null)
            Destroy(ModelSkin);

        ModelSkin = Instantiate(Player.GetPrefab(), _placeSkin);
        ModelSkin.GetComponent<Animator>().Play(dance.NameDanceAnim);

        SetPositionModel();
    }

    public virtual void TurnOnModel()
    {
        ModelSkin?.SetActive(true);
    }

    public virtual void BuyProduct()
    {
        _buyButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);
    }

    public void SpawnSkin(Skin skin)
    {
        if (ModelSkin != null)
            Destroy(ModelSkin);

        ModelSkin = Instantiate(skin.GetPrefab(), _placeSkin);
        SetPositionModel();
    }

    public abstract void ShowInfoProduct(Product skin);

    public abstract void SelectProduct();

    public abstract void TryBuyProduct();

    private void SetPositionModel()
    {
        Vector3 position = new Vector3(_placeSkin.position.x, ModelSkin.transform.position.y, _placeSkin.position.z);
        ModelSkin.transform.position = position;
        ModelSkin.GetComponent<Animator>().Play("Idle");
    }
}
