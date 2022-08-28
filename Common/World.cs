using OpenTK.Mathematics;

using Gecynd.Client;

namespace Gecynd.Common
{
  public class World
  {
    Dictionary<Vector3i, Chunk> _chunks = new Dictionary<Vector3i, Chunk>();

    public void LoadChunk(Vector3i position)
    {
      _chunks.TryAdd(position, new Chunk());
    }

    public Dictionary<Vector3i, Chunk> GetChunks()
    {
      return _chunks;
    }

    public Chunk GetChunk(Vector3i position)
    {
      Chunk chunk;
      if (!_chunks.TryGetValue(position / Chunk.SIZE, out chunk))
      {
        throw new Exception("Position not loaded yet");
      }
      else
      {
        return chunk;
      }
    }

    public Block GetBlock(Vector3i position)
    {
      return GetChunk(position).GetBlock(new Vector3i(position.X % Chunk.SIZE, position.Y % Chunk.SIZE, position.Z % Chunk.SIZE));

    }

    public void SetBlock(Vector3i position, Block block)
    {
      GetChunk(position).SetBlock(new Vector3i(position.X % Chunk.SIZE, position.Y % Chunk.SIZE, position.Z % Chunk.SIZE), block);
    }
  }
}