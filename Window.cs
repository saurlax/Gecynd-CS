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
    private Camera _camera;
    private readonly float[] _vertices =
    {
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,

    // ground
    -50.0f, 0.0f,  50.0f,   0.0f,  50.0f,
     50.0f, 0.0f,  50.0f,  50.0f,  50.0f,
    -50.0f, 0.0f, -50.0f,   0.0f,   0.0f,
     50.0f, 0.0f,  50.0f,  50.0f,  50.0f,
    -50.0f, 0.0f, -50.0f,   0.0f,   0.0f,
     50.0f, 0.0f, -50.0f,  50.0f,   0.0f
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
    private Shader groundsha;
    private Texture _texture0;
    private Texture _texture1;
    private bool _firstMove = true;
    private Vector2 _lastPos;
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

      _camera = new Camera(new Vector3(0.0f, 1.0f, 0.0f), Size.X / (float)Size.Y);
      CursorGrabbed = true;
      _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
      groundsha = new Shader("Shaders/ground.vert", "Shaders/ground.frag");
      _shader.Use();
      groundsha.Use();

      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(1);

      _texture0 = Texture.LoadFromFile("Resources/container.png");
      _texture1 = Texture.LoadFromFile("Resources/ground.png");
      _texture0.Use(TextureUnit.Texture0);
      _texture1.Use(TextureUnit.Texture1);
      _shader.SetInt("texture0", 0);
      groundsha.SetInt("texture1", 1);
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
        Title =  $"Projec2398 - FPS: {count}, Size: {Size}";
        time = tick;
        count = 0;
      }

      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      GL.Enable(EnableCap.DepthTest);
      GL.BindVertexArray(_vertexArrayObject);
      _shader.Use();
      _shader.SetMatrix4("view", _camera.GetViewMatrix());
      _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shader.SetMatrix4("model",
       Matrix4.CreateRotationY(tick / 2000.0f) *
      Matrix4.CreateTranslation(
        (float)Math.Sin(tick / 1000.0f),
        0.5f,
        (float)Math.Cos(tick / 1000.0f) - 3.0f));
      // _texture0.Use(TextureUnit.Texture0);
      // _texture1.Use(TextureUnit.Texture1);
      GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
      groundsha.Use();
      groundsha.SetMatrix4("view", _camera.GetViewMatrix());
      groundsha.SetMatrix4("projection", _camera.GetProjectionMatrix());
      groundsha.SetMatrix4("model", Matrix4.CreateScale(1.0f));
      GL.DrawArrays(PrimitiveType.Triangles, 36, 6);
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

      const float cameraSpeed = 5.0f;
      const float sensitivity = 0.3f;

      if (input.IsKeyDown(Keys.W))
      {
        _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
      }

      if (input.IsKeyDown(Keys.S))
      {
        _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
      }
      if (input.IsKeyDown(Keys.A))
      {
        _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
      }
      if (input.IsKeyDown(Keys.D))
      {
        _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
      }
      if (input.IsKeyDown(Keys.Space))
      {
        _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
      }
      if (input.IsKeyDown(Keys.LeftShift))
      {
        _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
      }
      var mouse = MouseState;
      if (_firstMove)
      {
        _lastPos = new Vector2(mouse.X, mouse.Y);
        _firstMove = false;
      }
      else
      {
        var deltaX = mouse.X - _lastPos.X;
        var deltaY = mouse.Y - _lastPos.Y;
        _lastPos = new Vector2(mouse.X, mouse.Y);
        _camera.Yaw += deltaX * sensitivity;
        _camera.Pitch -= deltaY * sensitivity;
      }
    }
    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
      base.OnMouseWheel(e);
      _camera.Fov -= e.OffsetY;
    }
    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);
      GL.Viewport(0, 0, Size.X, Size.Y);
      _camera.AspectRatio = Size.X / (float)Size.Y;
    }
  }
}