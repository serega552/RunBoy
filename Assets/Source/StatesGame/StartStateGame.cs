public class StartStateGame
{
    private readonly Menu _menu;
    private readonly PlayerPresenter _presenter;
    private readonly PlayerMoverPresenter _presenterMover;
    private readonly HudWindow _hudWindow;

    public StartStateGame(Menu menu, PlayerPresenter presenter, PlayerMoverPresenter presenterMover, HudWindow hudWindow)
    {
        _menu = menu;
        _presenter = presenter;
        _presenterMover = presenterMover;
        _hudWindow = hudWindow;
    }

    private void Start()
    {
        AudioManager.Instance.Play("StartGame"); 
        AudioManager.Instance.Play("Music");
        AudioManager.Instance.Pause("Music2");
        AudioManager.Instance.Pause("MenuMusic");

        _hudWindow.OpenWithoutSound();
        _presenter.StartGame();
        _presenterMover.StartPlayerMove();
    }

    public void Enable()
    {
        _menu.OnClickStart += Start;
    }

    public void Disable()
    {
        _menu.OnClickStart -= Start;
    }
}
