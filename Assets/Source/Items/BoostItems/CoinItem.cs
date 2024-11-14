using Player;

namespace Items.BoostItems
{
    public class CoinItem : Item
    {
        protected readonly int Reward = 2;

        protected override void GetResourses(PlayerView playerView)
        {
            playerView.AddMoney(Reward, false);
        }
    }
}