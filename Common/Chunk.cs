using System.Collections;
using OpenTK.Mathematics;

namespace Gecynd.Common
{
  public class Chunk
  {
    public static readonly int MAX_LEVEL = 8;
    public static readonly int SIZE = (1 << MAX_LEVEL);
    public ChunkNode RootNode { get; private set; } = new ChunkNode(MAX_LEVEL, null);

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
      return RootNode.GetBlock(position);
    }

    public void SetBlock(Vector3i position, Block block)
    {
      TestPosition(position);
      RootNode.SetBlock(position, block);
    }

    /// <summary>
    /// Block storage structure implemented by octree.
    /// </summary>
    public class ChunkNode : IEnumerable<Block>
    {
      /// <summary>
      /// Size level of this ChunkNode.
      /// </summary>
      public int Level { get; }
      /// <summary>
      /// If all nodes of this ChunkNode are blocks of the same type, it will be designated as blocks of this type; If all nodes are not the same, it will be null. Used to optimize rendering.
      /// </summary>
      public Block? All { get; private set; } = Block.NULL;
      /// <summary>
      /// A Block that can be roughly regarded as this ChunkNode. It is often used to optimize the rendering of distant ChunkNode.
      /// </summary>
      public Block Rough { get; private set; } = Block.NULL;
      public ChunkNode? ParentNode { get; }
      public ChunkNode[] ChildNodes { get; } = new ChunkNode[8];

      int _size, _half;

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

      public IEnumerator<Block> GetEnumerator()
      {
        return new ChunkNodeEnumerator(this);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return this.GetEnumerator();
      }
    }

    class ChunkNodeEnumerator : IEnumerator<Block>
    {
      ChunkNode _node;
      IEnumerator<Block>? _childEnum;
      int _index = -1;

      public ChunkNodeEnumerator(ChunkNode node)
      {
        _node = node;
      }

      public Block Current
      {
        get
        {
          if (_node.All == null)
          {
            return _childEnum.Current;
          }
          else
          {
            return _node.All;
          }
        }
      }
      object? IEnumerator.Current
      {
        get { return this.Current; }
      }

      public bool MoveNext()
      {
        if (_node.All == null)
        {
          if (_childEnum == null || !_childEnum.MoveNext())
          {
            _index++;
            if (_index < 8)
            {
              _childEnum = _node.ChildNodes[_index].GetEnumerator();
              _childEnum.MoveNext();
              return true;
            }
            else
            {
              return false;
            }
          }
          else
          {
            return true;
          }
        }
        else
        {
          _index++;
          return _index == 0;
        }
      }

      public void Reset()
      {
        _index = -1;
      }

      public void Dispose() { }
    }
  }
}