using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class World
  {
    private Dictionary<string, Chunk> _chunks = new Dictionary<string, Chunk>();
    public World() { }
    public string CaclulateChunkIndex(Vector3i position)
    {
      return $"{position.X / 256}.{position.Y / 256}.{position.Z / 256}";
    }
    public Vector3i CalculatePositionInChunk(Vector3i position)
    {
      return new Vector3i(position.X % 256, position.Y % 256, position.Z % 256);
    }
    public Block GetBlock(Vector3i position)
    {
      Chunk? chunk;
      if (_chunks.TryGetValue(CaclulateChunkIndex(position), out chunk))
      {
        return chunk.GetBlock(CalculatePositionInChunk(position));
      }
      throw new Exception("The requested position has not been loaded");
    }
    public void SetBlock(Vector3i position, Block block)
    {
      Chunk? chunk;
      if (_chunks.TryGetValue(CaclulateChunkIndex(position), out chunk))
      {
        chunk.SetBlock(CalculatePositionInChunk(position), block);
      }
      throw new Exception("The requested position has not been loaded");
    }
    public void SetBlocks(Vector3i start, Vector3i end, Block block)
    {
      // TODO
    }
  }
}