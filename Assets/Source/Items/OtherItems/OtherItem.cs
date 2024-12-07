using Items.ItemsFactory;
using Player;

namespace Items.OtherItems
{
    public class OtherItem : Item
    {
        private ItemViewSpawner _spawner;

        public virtual void Boost() { }

        public virtual void DeBoost() { }

        public void Init(ItemViewSpawner spawner)
        {
            _spawner = spawner;
        }

        protected override void AddResourses(PlayerView playerView)
        {
            _spawner.Spawn(this);
        }

        protected override void AddMoverResourses(PlayerMoverView playerMoverView)
        {
            PlayerMoverView = playerMoverView;
        }
    }
}