using Player;

namespace Items.BoostItems
{
    public class CoinItem : Item
    {
     private readonly int _reward = 2;

        protected override void GetResourses(PlayerView playerView)
        {
            playerView.AddMoney(_reward, false);
        }
    }
}