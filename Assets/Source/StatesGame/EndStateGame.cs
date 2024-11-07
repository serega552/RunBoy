using System;
using YG;

public class EndStateGame
{
    private readonly Menu _menu;
    private readonly EndGameScreen _endScreen;
    private readonly PlayerMoverPresenter _presenterMover;
    private readonly PlayerPresenter _presenter;
    private readonly PlayerResurrect _playerResurrect;
    private readonly HudWindow _hudWindow;
    private readonly LeaderboardYG _leaderboard;

    public EndStateGame(Menu menu, PlayerPresenter presenter, PlayerMoverPresenter presenterMover, PlayerResurrect playerResurrect, EndGameScreen endScreen, HudWindow hudWindow, LeaderboardYG leaderboard)
    {
        _menu = menu;
        _presenterMover = presenterMover;
        _presenter = presenter;
        _playerResurrect = playerResurrect;
        _endScreen = endScreen;
        _hudWindow = hudWindow;
        _leaderboard = leaderboard;
    }

    public event Action OnEndGame;

    public void Enable()
    {
        YandexGame.GetDataEvent += Load;
        _presenter.OnEndGame += End;
        _playerResurrect.OnRestarting += OpenWindows;
    }

    public void Disable()
    {
        YandexGame.GetDataEvent -= Load;
        _presenter.OnEndGame -= End;
        _playerResurrect.OnRestarting -= OpenWindows;
    }

    private void End()
    {
        AudioManager.Instance.Pause("Music");

        _presenterMover.EndPlayerMove();
        _playerResurrect.OpenWindow();
        _menu.SetDistance(_presenter.TakeTotalDistance());
        YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(_presenter.TakeTotalDistance()));
        _leaderboard.NewScore(Convert.ToInt32(_presenter.TakeTotalDistance()));
        _leaderboard.UpdateLB();

        Save();
    }

    private void OpenWindows()
    {
        AudioManager.Instance.Play("GameOver");
        AudioManager.Instance.Stop("Music");

        _menu.GetComponent<MenuWindow>().OpenWithoutSound();
        _hudWindow.CloseWithoutSound();
        AudioManager.Instance.UnPause("Music2");

        OnEndGame?.Invoke();
    }

    private void Save()
    {
        YandexGame.savesData.Record = _presenter.TakeTotalDistance();
        YandexGame.SaveProgress();
    }

    private void Load()
    {
        float record = YandexGame.savesData.Record;

        _menu.SetDistance(record);
        YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(record));
        _leaderboard.NewScore(Convert.ToInt32(record));
        _leaderboard.UpdateLB();
    }
}
