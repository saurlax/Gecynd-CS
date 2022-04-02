using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Project2398.Common
{
  public class Chunk
  {
    private ChunkNode _node;

    private Dictionary<Vector4i, Block> _blocks;
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
        int half = 1 << (6 - _depth);
        if (position.X >= half) index += 1;
        if (position.Y >= half) index += 2;
        if (position.Z >= half) index += 4;
        return index;
      }
      public Vector3i CalculatePositionInChild(Vector3i position)
      {
        int half = 1 << (6 - _depth);
        if (position.X >= half) position.X -= half;
        if (position.Y >= half) position.Y -= half;
        if (position.Z >= half) position.Z -= half;
        return position;
      }
      public void OptimizeNodes()
      {
        if (_children == null) return;
        Block? firstChildBlock = _children[0].GetBlockIfNoChild();
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
      }
      public Block GetBlock(Vector3i position)
      {
        if (_children == null) return _block;
        return _children[CalculateChildIndex(position)].GetBlock(CalculatePositionInChild(position));
      }
      public Dictionary<Vector4i, Block> GetAllBlocks()
      {
        Dictionary<Vector4i, Block> blocks = new Dictionary<Vector4i, Block>();
        if (_children == null)
        {
          blocks.Add(new Vector4i(0, 0, 0, 1 << (7 - _depth)), _block);
        }
        else
        {
          for (int i = 0; i < 8; i++)
          {
            foreach (KeyValuePair<Vector4i, Block> pair in _children[i].GetAllBlocks())
            {
              int half = 1 << (6 - _depth);
              blocks.Add(new Vector4i(
                pair.Key.X + half * (i % 2),
                pair.Key.Y + half * ((i / 2) % 2),
                pair.Key.Z + half * (i / 4),
                pair.Key.W
              ), pair.Value);
            }
          }
        }
        return blocks;
      }
      public Block? GetBlockIfNoChild()
      {
        return _block;
      }
      public void SetBlock(Vector3i position, Block block)
      {
        if (_depth == 7)
        {
          _block = block;
          _children = null;
          return;
        }
        if (_children == null)
        {
          _block = null;
          _children = new ChunkNode[8];
          for (int i = 0; i < 8; i++)
          {
            _children[i] = new ChunkNode(this, null, Block.NULL, _depth + 1);
          }
          _children[CalculateChildIndex(position)].SetBlock(CalculatePositionInChild(position), block);
        }
        else
        {
          _children[CalculateChildIndex(position)].SetBlock(CalculatePositionInChild(position), block);
          OptimizeNodes();
        }
      }
      public void SetBlocks(Vector3i start, Vector3i end, Block block)
      {
        // TODO
        OptimizeNodes();
      }
    }
    public Chunk()
    {
      _node = new ChunkNode(null, null, Block.NULL, 0);
      _blocks = _node.GetAllBlocks();
      _entities = new Dictionary<string, Entity>();
    }
    public Block GetBlock(Vector3i position)
    {
      return _node.GetBlock(position);
    }
    public void SetBlock(Vector3i position, Block block)
    {
      _node.SetBlock(position, block);
      // TODO Too waste
      _blocks = _node.GetAllBlocks();
    }
    public void SetBlocks(Vector3i start, Vector3i end, Block block)
    {
      _node.SetBlocks(start, end, block);
      _blocks = _node.GetAllBlocks();
    }
    public void Render(Vector3i position, Camera camera)
    {
      foreach (KeyValuePair<Vector4i, Block> pair in _blocks)
      {
        pair.Value.Render(new Vector4i(
          pair.Key.X + position.X,
          pair.Key.Y + position.Y,
          pair.Key.Z + position.Z,
          pair.Key.W
        ), camera);
      }
    }
  }
}
