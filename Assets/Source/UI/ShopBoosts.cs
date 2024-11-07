using UnityEngine;

public class ShopBoosts : MonoBehaviour
{
    [SerializeField] private Bank _bank;

    public void Buy(Boost boost, int price)
    {
        if (_bank.TryTakeMoney(price))
        {
            _bank.TakeMoney(price);
            boost.Increase();
            TaskCounter.IncereaseProgress(1, TaskType.BuyBoost.ToString());
        }
    }

    public void BuyUpgrade(Boost boost, int price)
    {
        if (_bank.TryTakeMoney(price))
        {
            _bank.TakeMoney(price);
            boost.Upgrade();
        }
    }
}
