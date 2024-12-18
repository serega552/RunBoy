using Player;

namespace Items.BoostItems
{
    public class CoinItem : Item
    {
        private readonly int _reward = 2;

        protected override void AddResourses(PlayerView playerView)
        {
            playerView.TryToAddMoney(_reward, false);
        }
    }
}