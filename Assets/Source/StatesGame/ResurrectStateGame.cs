public class ResurrectStateGame
{
    private readonly PlayerMoverPresenter _presenterMover;
    private readonly PlayerPresenter _presenter;
    private readonly PlayerResurrect _playerResurrect;

    public ResurrectStateGame(PlayerPresenter presenter, PlayerMoverPresenter presenterMover, PlayerResurrect playerResurrect)
    {
        _presenterMover = presenterMover;
        _presenter = presenter;
        _playerResurrect = playerResurrect;
    }

    public void Enable()
    {
        _playerResurrect.OnResurrected += Resurrect;
    }

    public void Disable()
    {
        _playerResurrect.OnResurrected -= Resurrect;
    }

    private void Resurrect(float energy)
    {
        _presenter.ResurrectPlayer(energy);
        _presenterMover.StartPlayerMove();
    }
}
