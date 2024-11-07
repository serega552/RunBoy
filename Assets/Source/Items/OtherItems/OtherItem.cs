public class OtherItem : Item
{
    public string itemType;

    private ItemViewSpawner _spawner;

    public virtual void Boost()
    { }

    public virtual void DeBoost()
    { }

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