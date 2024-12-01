using Items.ItemsFactory;
using Player;

namespace Items.OtherItems
{
    public class OtherItem : Item
    {
        private ItemViewSpawner _spawner;

        public virtual void Boost()
        {
        }

        public virtual void DeBoost()
        {
        }

        public void Init(ItemViewSpawner spawner)
        {
            _spawner = spawner;
        }

        protected override void GetResourses(PlayerView playerView)
        {
            _spawner.Spawn(this);
        }

        protected override void GetMoverResourses(PlayerMoverView playerMoverView)
        {
            PlayerMoverView = playerMoverView;
        }
    }
}