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
    private World _world;
    private bool _firstMove = true;
    private Vector2 _lastPos;
    private Block _block;
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
      base.OnLoad();
      GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
      _camera = new Camera(new Vector3(0.0f, 10.0f, 0.0f), Size.X / (float)Size.Y);
      CursorGrabbed = true;
      _block = new RenderBlock();
      _world = new World(_camera);
      _world.LoadChunk(new Vector3i(0, 0, 0));
      _world.LoadChunk(new Vector3i(1, 0, 0));
      _world.LoadChunk(new Vector3i(0, 0, 1));
      _world.LoadChunk(new Vector3i(1, 0, 1));
      _world.SetBlock(new Vector3i(1, 56, 32), _block);
      _world.SetBlock(new Vector3i(1, 56, 36), _block);
      _world.SetBlock(new Vector3i(342, 56, 32), _block);
    }
    private int time = Environment.TickCount;
    private int count = 0;
    private Vector3 lightPos, lightColor;
    private Chunk chunk;
    protected override async void OnRenderFrame(FrameEventArgs e)
    {
      count++;
      int tick = Environment.TickCount;
      if (tick - time > 1000)
      {
        Title = $"Projec2398 - FPS: {count}, Size: {Size}";
        time = tick;
        count = 0;
      }

      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      GL.Enable(EnableCap.DepthTest);
      // GL.Enable(EnableCap.CullFace);
      _world.Render();
      SwapBuffers();
    }
    private int polygonMode = 0;
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      var input = KeyboardState;
      if (input.IsKeyDown(Keys.Escape))
      {
        Close();
      }
      if (input.IsKeyPressed(Keys.F1))
      {
        switch (polygonMode)
        {
          case 0:
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            polygonMode++;
            break;
          case 1:
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point);
            polygonMode++;
            break;
          default:
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            polygonMode = 0;
            break;
        }
      }

      const float cameraSpeed = 5.0f;
      const float sensitivity = 0.3f;

      if (input.IsKeyDown(Keys.W))
      {
        _camera.Position += Vector3.Normalize(_camera.Front * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Forward
      }
      if (input.IsKeyDown(Keys.S))
      {
        _camera.Position -= Vector3.Normalize(_camera.Front * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Backwards
      }
      if (input.IsKeyDown(Keys.A))
      {
        _camera.Position -= Vector3.Normalize(_camera.Right * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Left
      }
      if (input.IsKeyDown(Keys.D))
      {
        _camera.Position += Vector3.Normalize(_camera.Right * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Right
      }
      if (input.IsKeyDown(Keys.Space))
      {
        _camera.Position += Vector3.UnitY * cameraSpeed * (float)e.Time; // Up
      }
      if (input.IsKeyDown(Keys.LeftShift))
      {
        _camera.Position -= Vector3.UnitY * cameraSpeed * (float)e.Time; // Down
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