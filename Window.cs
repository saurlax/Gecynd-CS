using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace Project2398
{
  public class Window : GameWindow
  {
    private readonly float[] _vertices =
    {
       -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        0.0f,  0.5f,0.0f
    };
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    // private Shader _shader;
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }
    protected override void OnLoad()
    {
      base.OnLoad();
      GL.ClearColor(1.0f, 0.3f, 0.3f, 1.0f);
      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
      _vertexArrayObject = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
    }
    int time = Environment.TickCount;
    int count = 0;
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      count++;
      if (Environment.TickCount - time >= 1000)
      {
        Console.WriteLine("Current FPS: {0}", count);
        count = 0;
        time = Environment.TickCount;
      }
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.BindVertexArray(_vertexArrayObject);
      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
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