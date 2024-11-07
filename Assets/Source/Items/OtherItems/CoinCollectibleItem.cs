public class CoinCollectibleItem : OtherItem
{
    private int _boostCount = 40;
    private int _deBoostCount = -5;
    private bool _isBoost = true;

    public override void Boost()
    {
        PlayerView.AddMoney(_boostCount, _isBoost);
    }

    public override void DeBoost()
    {
        PlayerView.AddMoney(_deBoostCount, _isBoost);
    }
}
