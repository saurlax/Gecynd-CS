using System;
using OpenTK.Graphics.OpenGL4;
using Project2398.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace Project2398
{
  public class Window : GameWindow
  {
    private readonly float[] _vertices =
    {
//     ---- 位置 ----       ---- 颜色 ----     - 纹理坐标 -
     0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f,   // 右上
     0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f,   // 右下
    -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f,   // 左下
    -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f    // 左上
    };
    private readonly uint[] _indices =
    {
    0, 1, 3,
    1, 2, 3
    };
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _elementBufferObject;
    private Shader _shader;
    private Texture _texture0;
    private Texture _texture1;
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
      base.OnLoad();
      GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
      _vertexArrayObject = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject);

      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
      _elementBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(int), _indices, BufferUsageHint.StaticDraw);

      _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
      _shader.Use();

      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
      GL.EnableVertexAttribArray(2);

      _texture0 = Texture.LoadFromFile("Resources/container.png");
      _texture0.Use(TextureUnit.Texture0);
      _texture1 = Texture.LoadFromFile("Resources/awesomeface.png");
      _texture1.Use(TextureUnit.Texture1);
      _shader.SetInt("texture0", 0);
      _shader.SetInt("texture1", 1);
      // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
    }
    private int time = Environment.TickCount;
    private int count = 0;
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      count++;
      int tick = Environment.TickCount;
      if (tick - time > 1000)
      {
        Console.WriteLine($"Time: {time}, FPS: {count}");
        time = tick;
        count = 0;
      }

      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.BindVertexArray(_vertexArrayObject);
      _shader.Use();
      _shader.SetMatrix4("transform", Matrix4.CreateRotationZ(tick / 100.0f) * Matrix4.CreateRotationY(tick / 100.0f));

      _texture0.Use(TextureUnit.Texture0);
      _texture1.Use(TextureUnit.Texture1);
      GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      var input = KeyboardState;
      if (input.IsKeyDown(Keys.Escape))
      {
        Close();
      }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);
      GL.Viewport(0, 0, Size.X, Size.Y);
    }
  }
}