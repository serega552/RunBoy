using Player;

namespace Items.BoostItems
{
    public class EnergyItem : Item
    {
        protected float Energy = 50;

        protected override void GetResourses(PlayerView playerView)
        {
            PlayerView.OnEnergyChanged(Energy);
        }
    }
}