using Player;

namespace Items.BoostItems
{
    public class EnergyItem : Item
    {
        private float _energy = 50;

        protected override void GetResourses(PlayerView playerView)
        {
            PlayerView.OnEnergyChanged(_energy);
        }
    }
}