namespace Items.OtherItems
{
    public class CoinCollectibleItem : OtherItem
    {
        private readonly int _boostCount = 40;
        private readonly int _deBoostCount = -5;
        private readonly bool _isBoost = true;

        public override void Boost()
        {
            PlayerView.AddMoney(_boostCount, _isBoost);
        }

        public override void DeBoost()
        {
            PlayerView.AddMoney(_deBoostCount, _isBoost);
        }
    }
}