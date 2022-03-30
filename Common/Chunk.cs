namespace Project2398.Common
{
  public class Chunk
  {
    // 一个区块的大小为16^3 * 16^3 = 2^8^3 = 16,777,216体素
    // 八叉树最大深度为8
    public class ChunkNode
    {
      public ChunkNode[] _nodes;
      // _voxel应该是材质id，之后从世界材质表中获取真实材质
      private int _voxel;
      public int GetVoxel(int x, int y, int z, int depth)
      {
        if (depth > 7) throw new Exception("Depth overflow");
        // 通常不需要使用GetVoxel()方法，渲染时只需要顺序遍历就可以了
        // 此方法只在随机读取和写入方块时会用到

        // 传入的x, y, z为整个Chunk内的相对坐标，由Chunk将世界坐标翻译为区块内相对坐标
        // 0 < x, y, z < 256
        // 0 < depth < 7
        // depth为当前树深度
        if (_nodes != null)
        {
          int i = 0;
          // 如果子节点不为空，继续向下查找
          int half = (1 << depth) - 1;
          if (x > half) i += 1; x -= half;
          if (y > half) i += 2; y -= half;
          if (z > half) i += 4; z -= half;
          return _nodes[i].GetVoxel(x, y, z, depth++);
        }
        else
        {
          // 如果为没有子节点了，那么就直接返回本块
          return _voxel;
        }
      }
      public ChunkNode(int voxel)
      {
        _voxel = voxel;
      }
      public ChunkNode(params ChunkNode[] nodes)
      {
        _nodes = nodes;
      }
      public ChunkNode[] GetChildNodes()
      {
        return _nodes;
      }
      public int GetVoxel()
      {
        return _voxel;
      }
    }
    private ChunkNode _chunkNode;
    public Chunk()
    {
      _chunkNode = new ChunkNode(
        new ChunkNode(1),
        new ChunkNode(1),
        new ChunkNode(1),
        new ChunkNode(0),
        new ChunkNode(0),
        new ChunkNode(1),
        new ChunkNode(
          new ChunkNode(1),
          new ChunkNode(1),
          new ChunkNode(1),
          new ChunkNode(1),
          new ChunkNode(0),
          new ChunkNode(0),
          new ChunkNode(1),
          new ChunkNode(0)
        ),
        new ChunkNode(1)
      );
    }
    public ChunkNode GetChunkNode()
    {
      return _chunkNode;
    }
    public void StoreChunk()
    {
      // TODO 存储区块，使用线性八叉树
    }
  }
}