public class RestartStateGame
{
    private readonly PlayerPresenter _playerPresenter;
    private readonly PlayerMoverPresenter _playerMoverPresenter;
    private readonly ChunksPlacer _chunksPlacer;
    private readonly ChunksPlacer _backgroundChunksPlacer;
    private readonly PlayerResurrect _playerResurrect;

    public RestartStateGame(PlayerPresenter playerPresenter, PlayerMoverPresenter playerMoverPresenter, ChunksPlacer chunksPlacer, ChunksPlacer backgroundChunksPlacer, PlayerResurrect playerResurrect)
    {
        _playerPresenter = playerPresenter;
        _playerMoverPresenter = playerMoverPresenter;
        _chunksPlacer = chunksPlacer;
        _backgroundChunksPlacer = backgroundChunksPlacer;
        _playerResurrect = playerResurrect;
    }

    public void Enable()
    {
        _playerResurrect.OnRestarting += ResetGame;
    }

    public void Disable()
    {
        _playerResurrect.OnRestarting -= ResetGame;
    }

    public void ResetGame()
    {
        _chunksPlacer.ResetFirstChunk();
        _backgroundChunksPlacer.ResetFirstChunk();
        _playerMoverPresenter.ResetPlayerMove();
        _playerPresenter.ResetPlayer();
    }
}
