using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class Chunk
  {
    private ChunkNode _node;
    private Dictionary<string, Entity> _entities;
    private class ChunkNode
    {
      private ChunkNode? _parent;
      private ChunkNode[]? _children;
      private Block? _block;
      private int _depth;
      public ChunkNode(ChunkNode? parent, ChunkNode[]? children, Block? block, int depth)
      {
        _parent = parent;
        _children = children;
        _block = block;
        _depth = depth;
      }
      public int CalculateChildIndex(Vector3i position)
      {
        int index = 0;
        int half = 1 << _depth;
        if (position.X >= half) index += 1;
        if (position.Y >= half) index += 2;
        if (position.Z >= half) index += 4;
        return index;
      }
      public Vector3i CalculatePositionInChild(Vector3i position)
      {
        int half = 1 << _depth;
        if (position.X >= half) position.X -= half;
        if (position.Y >= half) position.Y -= half;
        if (position.Z >= half) position.Z -= half;
        return position;
      }
      public void OptimizeNodes()
      {
        if (_children == null) return;
        Block firstChildBlock = _children[0].GetBlockIfNoChild();
        if (firstChildBlock == null) return;
        bool matchAllChildren = true;

        foreach (ChunkNode child in _children)
        {
          if (child.GetBlockIfNoChild() != firstChildBlock) matchAllChildren = false;
        }
        if (matchAllChildren)
        {
          _children = null;
          _block = firstChildBlock;
        }
        _parent?.OptimizeNodes();
      }
      public Block GetBlock(Vector3i position)
      {
        if (_children == null) return _block;
        return _children[CalculateChildIndex(position)].GetBlock(CalculatePositionInChild(position));
      }
      public Block GetBlockIfNoChild()
      {
        return _block;
      }
      public void SetBlock(Vector3i position, Block block)
      {
        if (_depth == 7)
        {
          _block = block;
          return;
        }
        if (_children == null)
        {
          _children = new ChunkNode[8];
          Array.Fill<ChunkNode>(_children, new ChunkNode(this, null, _block, _depth + 1));
        }
        _children[CalculateChildIndex(position)].SetBlock(position, block);
        OptimizeNodes();
      }
      public void SetBlocks(Vector3i start, Vector3i end, Block block)
      {
        // TODO
        OptimizeNodes();
      }
    }
    public Chunk()
    {
    }
    public Block GetBlock(Vector3i position)
    {
      return _node.GetBlock(position);
    }
    public void SetBlock(Vector3i position, Block block)
    {
      _node.SetBlock(position, block);
    }
    public void SetBlocks(Vector3i start, Vector3i end, Block block)
    {
      _node.SetBlocks(start, end, block);
    }
  }
}