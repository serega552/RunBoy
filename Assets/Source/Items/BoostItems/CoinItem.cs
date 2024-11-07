public class CoinItem : Item
{
    protected int Reward = 2;

    protected override void GetResourses(PlayerView playerView)
    {
        playerView.AddMoney(Reward, false);
    }
}
