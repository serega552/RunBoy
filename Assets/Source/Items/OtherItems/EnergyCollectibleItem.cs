public class EnergyCollectibleItem : OtherItem
{
    private int _boostCount = 100;
    private int _deBoostCount = -50;

    public override void Boost()
    {
        PlayerView.OnEnergyChanged(_boostCount);
    }

    public override void DeBoost()
    {
        PlayerView.OnEnergyChanged(_deBoostCount);
    }
}
