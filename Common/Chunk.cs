using System.Collections;
using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class Chunk
  {
    public static readonly int DEPTH = 8;
    public static readonly int CHUNK_SIZE = (1 << DEPTH) - 1;

    ChunkNode _node = new ChunkNode(0);

    void TestPosition(Vector3i position)
    {
      if (position.X > CHUNK_SIZE || position.Y > CHUNK_SIZE || position.Z > CHUNK_SIZE ||
          position.X < 0 || position.Y < 0 || position.Z < 0)
      {
        throw new Exception("The position provided is beyond the scope of the chunk.");
      }
    }

    public Block GetBlock(Vector3i position)
    {
      TestPosition(position);
      return _node.GetBlock(position);
    }

    public void SetBlock(Vector3i position, Block block)
    {
      TestPosition(position);
      _node.SetBlock(position, block);
    }

    public ChunkNode GetRootNode()
    {
      return _node;
    }

    public class ChunkNode
    {
      int _depth, _size, _half;
      Block _all = Block.NULL;
      ChunkNode[] _nodes = new ChunkNode[8];

      public ChunkNode(int depth)
      {
        _depth = depth;
        _size = (1 << (Chunk.DEPTH - _depth)) - 1;
        _half = (_size - 1) >> 1;
      }

      int GetChildNodeIndex(Vector3i position)
      {
        int index = 0;
        if (position.X % _size > _half) index |= 0b100;
        if (position.Y % _size > _half) index |= 0b010;
        if (position.Z % _size > _half) index |= 0b101;
        return index;
      }

      public Block GetBlock(Vector3i position)
      {
        if (_all != null)
        {
          return _all;
        }
        else
        {
          return _nodes[GetChildNodeIndex(position)].GetBlock(position);
        }
      }

      public void SetBlock(Vector3i position, Block block)
      {
        if (_depth != Chunk.DEPTH)
        {
          int index = GetChildNodeIndex(position);
          if (_nodes[index] == null)
          {
            for (int i = 0; i < 8; i++)
            {
              _nodes[i] = new ChunkNode(_depth + 1);
            }
          }
          _all = null;
          _nodes[index].SetBlock(position, block);
        }
        else
        {
          _all = block;
        }
      }

      public Dictionary<Vector4i, Block> GetAllNodes(Vector3i offset)
      {
        Dictionary<Vector4i, Block> nodes = new Dictionary<Vector4i, Block>();
        if (_all == null)
        {
          for (int i = 0; i < 8; i++)
          {
            nodes.Concat(_nodes[i].GetAllNodes(offset + new Vector3i((i & 0b100) >> 2, (i & 0b010) >> 1, i & 0b100) * _half));
          }
        }
        else
        {
          nodes.Add(new Vector4i(offset, _size), _all);
        }
        return nodes;
      }
    }
  }
}