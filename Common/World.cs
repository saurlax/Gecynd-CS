using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class World
  {
    private Dictionary<Vector3i, Chunk> _chunks = new Dictionary<Vector3i, Chunk>();
    private Camera _camera;
    public World(Camera camera)
    {
      _camera = camera;
    }
    public Vector3i CaclulateChunkIndex(Vector3i position)
    {
      return position / 128;
    }
    public Vector3i CalculatePositionInChunk(Vector3i position)
    {
      return new Vector3i(position.X % 128, position.Y % 128, position.Z % 128);
    }
    public Block GetBlock(Vector3i position)
    {
      Chunk? chunk;
      if (_chunks.TryGetValue(CaclulateChunkIndex(position), out chunk))
      {
        return chunk.GetBlock(CalculatePositionInChunk(position));
      }
      else
      {
        throw new Exception("The requested position has not been loaded");
      }
    }
    public void SetBlock(Vector3i position, Block block)
    {
      Chunk? chunk;
      if (_chunks.TryGetValue(CaclulateChunkIndex(position), out chunk))
      {
        chunk.SetBlock(CalculatePositionInChunk(position), block);
      }
      else
      {
        throw new Exception("The requested position has not been loaded");
      }
    }
    public void SetBlocks(Vector3i start, Vector3i end, Block block)
    {
      // TODO
    }
    public void LoadChunk(Vector3i position)
    {
      Chunk chunk = new Chunk();
      _chunks.Add(position, chunk);
    }
    public void Render()
    {
      foreach (KeyValuePair<Vector3i, Chunk> pair in _chunks)
      {
        pair.Value.Render(pair.Key, _camera);
      }
    }
  }
}