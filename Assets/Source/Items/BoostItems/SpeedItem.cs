public class SpeedItem : Item
{
    private float _value = 5f;
    private float _time = 5f;

    protected override void GetMoverResourses(PlayerMoverView playerMoverView)
    {
        PlayerMoverView.ChangeSpeed(_value, _time);
    }
}

