using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class Chunk
  {
    static readonly int DEPTH = 8;
    static readonly int CHUNK_SIZE = 1 << 8;

    ChunkNode _node = new ChunkNode(0);

    public Block GetBlock(Vector3i position)
    {
      if (position.X > CHUNK_SIZE || position.Y > CHUNK_SIZE || position.Z > CHUNK_SIZE ||
          position.X < 0 || position.Y < 0 || position.Z < 0)
      {
        throw new Exception("The position provided is beyond the scope of the chunk.");
      }
      return _node.GetBlock(position);
    }
  }

  class ChunkNode
  {
    int _depth;
    Block? _all;
    ChunkNode[] _nodes = new ChunkNode[8];

    public ChunkNode(int depth)
    {

    }

    public Block GetBlock(Vector3i position)
    {
      if (_all != null)
      {
        return _all;
      }
      else
      {
        return _nodes[1].GetBlock(position);
      }
    }

    public void SetBlock(Vector3i position, Block block)
    {

    }
  }
}