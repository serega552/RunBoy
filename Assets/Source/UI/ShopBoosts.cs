using BankSystem;
using BoostSystem;
using Tasks;
using Tasks.SO;
using UnityEngine;

namespace UI
{
    public class ShopBoosts : MonoBehaviour
    {
        [SerializeField] private Bank _bank;

        public void Buy(Boost boost, int price)
        {
            if (_bank.CanTakeCurrency(_bank.Money, price))
            {
                _bank.TryTakeMoney(price);
                boost.Increase();
                TaskCounter.IncereaseProgress(1, TaskType.BuyBoost.ToString());
            }
        }

        public void BuyUpgrade(Boost boost, int price)
        {
            if (_bank.CanTakeCurrency(_bank.Money, price))
            {
                _bank.TryTakeMoney(price);
                boost.Upgrade();
            }
        }
    }
}