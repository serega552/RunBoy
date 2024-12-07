using Player;

namespace Items.BoostItems
{
    public class SpeedItem : Item
    {
        private readonly float _value = 5f;
        private readonly float _time = 5f;

        protected override void AddMoverResourses(PlayerMoverView playerMoverView)
        {
            PlayerMoverView.ChangeSpeed(_value, _time);
        }
    }
}
