using Chunks;
using Player;
using UI;

namespace StatesGame
{
    public class RestartStateGame
    {
        private readonly ChunksPlacer _chunksPlacer;
        private readonly ChunksPlacer _backgroundChunksPlacer;
        private readonly PlayerResurrect _playerResurrect;

        private PlayerView _player;
        private PlayerMoverView _playerMover;

        public RestartStateGame(
            PlayerView player,
            PlayerMoverView playerMover,
            ChunksPlacer chunksPlacer,
            ChunksPlacer backgroundChunksPlacer,
            PlayerResurrect playerResurrect)
        {
            _player = player;
            _playerMover = playerMover;
            _chunksPlacer = chunksPlacer;
            _backgroundChunksPlacer = backgroundChunksPlacer;
            _playerResurrect = playerResurrect;
        }

        public void Enable()
        {
            _playerResurrect.Restarting += ResetGame;
        }

        public void Disable()
        {
            _playerResurrect.Restarting -= ResetGame;
        }

        public void AddPlayer(PlayerView player, PlayerMoverView playerMover)
        {
            _player = player;
            _playerMover = playerMover;
        }

        public void ResetGame()
        {
            _chunksPlacer.ResetFirstChunk();
            _backgroundChunksPlacer.ResetFirstChunk();
            _playerMover.ResetMove();
            _player.ResetPlayer();
        }
    }
}