using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using Gecynd.Common;

namespace Gecynd.Client
{
  public class Renderer
  {
    static Shader _shader;
    static Texture _texture;
    public static Camera Camera;

    static readonly float[] _vertices = {
    0.0f, 0.0f, 0.0f,  0.0f, 0.0f,  0.0f,  0.0f, -1.0f,
    1.0f, 1.0f, 0.0f,  1.0f, 1.0f,  0.0f,  0.0f, -1.0f,
    1.0f, 0.0f, 0.0f,  1.0f, 0.0f,  0.0f,  0.0f, -1.0f,
    1.0f, 1.0f, 0.0f,  1.0f, 1.0f,  0.0f,  0.0f, -1.0f,
    0.0f, 0.0f, 0.0f,  0.0f, 0.0f,  0.0f,  0.0f, -1.0f,
    0.0f, 1.0f, 0.0f,  0.0f, 1.0f,  0.0f,  0.0f, -1.0f,

    0.0f, 0.0f, 1.0f,  0.0f, 0.0f,  0.0f,  0.0f,  1.0f,
    1.0f, 0.0f, 1.0f,  1.0f, 0.0f,  0.0f,  0.0f,  1.0f,
    1.0f, 1.0f, 1.0f,  1.0f, 1.0f,  0.0f,  0.0f,  1.0f,
    1.0f, 1.0f, 1.0f,  1.0f, 1.0f,  0.0f,  0.0f,  1.0f,
    0.0f, 1.0f, 1.0f,  0.0f, 1.0f,  0.0f,  0.0f,  1.0f,
    0.0f, 0.0f, 1.0f,  0.0f, 0.0f,  0.0f,  0.0f,  1.0f,

    0.0f, 1.0f, 1.0f,  1.0f, 0.0f, -1.0f,  0.0f,  0.0f,
    0.0f, 1.0f, 0.0f,  1.0f, 1.0f, -1.0f,  0.0f,  0.0f,
    0.0f, 0.0f, 0.0f,  0.0f, 1.0f, -1.0f,  0.0f,  0.0f,
    0.0f, 0.0f, 0.0f,  0.0f, 1.0f, -1.0f,  0.0f,  0.0f,
    0.0f, 0.0f, 1.0f,  0.0f, 0.0f, -1.0f,  0.0f,  0.0f,
    0.0f, 1.0f, 1.0f,  1.0f, 0.0f, -1.0f,  0.0f,  0.0f,

    1.0f, 1.0f, 1.0f,  1.0f, 0.0f,  1.0f,  0.0f,  0.0f,
    1.0f, 0.0f, 0.0f,  0.0f, 1.0f,  1.0f,  0.0f,  0.0f,
    1.0f, 1.0f, 0.0f,  1.0f, 1.0f,  1.0f,  0.0f,  0.0f,
    1.0f, 0.0f, 0.0f,  0.0f, 1.0f,  1.0f,  0.0f,  0.0f,
    1.0f, 1.0f, 1.0f,  1.0f, 0.0f,  1.0f,  0.0f,  0.0f,
    1.0f, 0.0f, 1.0f,  0.0f, 0.0f,  1.0f,  0.0f,  0.0f,

    0.0f, 0.0f, 0.0f,  0.0f, 1.0f,  0.0f, -1.0f,  0.0f,
    1.0f, 0.0f, 0.0f,  1.0f, 1.0f,  0.0f, -1.0f,  0.0f,
    1.0f, 0.0f, 1.0f,  1.0f, 0.0f,  0.0f, -1.0f,  0.0f,
    1.0f, 0.0f, 1.0f,  1.0f, 0.0f,  0.0f, -1.0f,  0.0f,
    0.0f, 0.0f, 1.0f,  0.0f, 0.0f,  0.0f, -1.0f,  0.0f,
    0.0f, 0.0f, 0.0f,  0.0f, 1.0f,  0.0f, -1.0f,  0.0f,

    0.0f, 1.0f, 0.0f,  0.0f, 1.0f,  0.0f,  1.0f,  0.0f,
    1.0f, 1.0f, 1.0f,  1.0f, 0.0f,  0.0f,  1.0f,  0.0f,
    1.0f, 1.0f, 0.0f,  1.0f, 1.0f,  0.0f,  1.0f,  0.0f,
    1.0f, 1.0f, 1.0f,  1.0f, 0.0f,  0.0f,  1.0f,  0.0f,
    0.0f, 1.0f, 0.0f,  0.0f, 1.0f,  0.0f,  1.0f,  0.0f,
    0.0f, 1.0f, 1.0f,  0.0f, 0.0f,  0.0f,  1.0f,  0.0f
    };

    static int VAO, VBO;
    public static void Init()
    {
      _shader = new Shader("./Shaders/Block.vert", "./Shaders/Block.frag");
      _texture = Texture.LoadFromFile("./Mods/Native/Assets/Textures/Blocks/oak_planks.png");

      _texture.Use(TextureUnit.Texture0);
      _shader.Use();
      _shader.SetInt("texture0", 0);

      VAO = GL.GenVertexArray();
      GL.BindVertexArray(VAO);
      VBO = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
      GL.EnableVertexAttribArray(2);
    }
    public static void Render(Gecynd instance)
    {
      _shader.Use();
      _shader.SetMatrix4("view", Camera.GetViewMatrix());
      _shader.SetMatrix4("projection", Camera.GetProjectionMatrix());

      // foreach (KeyValuePair<Vector3i, Chunk> pair in instance.World.GetChunks())
      // {
      //   foreach (KeyValuePair<Vector4i, Block> i in pair.Value.GetRootNode().GetAllNodes(pair.Key * Chunk.SIZE))
      //   {
      //     Console.WriteLine(i);
      //     _shader.SetMatrix4("model", Matrix4.CreateTranslation(i.Key.X, i.Key.Y, i.Key.Z) * Matrix4.CreateScale(i.Key.W / 16.0f));
      //     _shader.SetVector4("offset", i.Key);
      //     GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 3);
      //   }
      // }
    }
  }
}