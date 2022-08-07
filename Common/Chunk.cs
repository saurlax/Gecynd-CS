using System.Collections;
using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class Chunk
  {
    public static readonly int MAX_LEVEL = 8;
    public static readonly int SIZE = (1 << MAX_LEVEL);

    ChunkNode _node = new ChunkNode(MAX_LEVEL, null);

    void TestPosition(Vector3i position)
    {
      if (position.X > SIZE || position.Y > SIZE || position.Z > SIZE ||
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

    public class ChunkNode : IEnumerable
    {
      public int Level { get; }
      int _size, _half;
      public Block? All { get; private set; } = Block.NULL;
      public ChunkNode? ParentNode { get; }
      public ChunkNode[] ChildNodes { get; } = new ChunkNode[8];

      public ChunkNode(int level, ChunkNode? parent)
      {
        Level = level;
        _size = 1 << Level;
        _half = _size >> 1;
      }

      int GetChildNodeIndex(Vector3i position)
      {
        int index = 0;
        if (position.X % _size >= _half) index |= 0b100;
        if (position.Y % _size >= _half) index |= 0b010;
        if (position.Z % _size >= _half) index |= 0b001;
        return index;
      }

      public Block GetBlock(Vector3i position)
      {
        if (All == null)
        {
          return ChildNodes[GetChildNodeIndex(position)].GetBlock(position);
        }
        else
        {
          return All;
        }
      }

      public void SetBlock(Vector3i position, Block block)
      {
        if (Level == Chunk.MAX_LEVEL)
        {
          All = block;
        }
        else
        {
          int index = GetChildNodeIndex(position);
          if (ChildNodes[index] == null)
          {
            for (int i = 0; i < 8; i++)
            {
              ChildNodes[i] = new ChunkNode(Level - 1, this);
            }
          }
          All = null;
          ChildNodes[index].SetBlock(position, block);
        }
      }

      public IEnumerator GetEnumerator()
      {
        return new ChunkNodeEnumerator(this);
      }
    }

    public class ChunkNodeEnumerator : IEnumerator
    {
      ChunkNode _node;
      int _level, _index;

      public ChunkNodeEnumerator(ChunkNode node)
      {
        _node = node;
      }

      public bool MoveNext()
      {
        if (_index == 7)
        {
          _index = 0;
        }
        return true;
      }

      public object Current
      {
        get
        {
          return _node.ChildNodes[_index];
        }
      }

      public void Reset()
      {
        _level = _node.Level;
        _index = -1;
      }
    }
  }
}