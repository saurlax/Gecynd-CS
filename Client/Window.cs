using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

using Project2398.Common;

namespace Project2398.Client
{
  public class Window : GameWindow
  {
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

    protected override void OnLoad()
    {
      base.OnLoad();
      GL.ClearColor(0, 0, 0, 1.0f);
      GL.Enable(EnableCap.DepthTest);
      Renderer.Init();
      Renderer.Camera = new Camera(new Vector3(8, 18, 8), Size.X / (float)Size.Y);
      CursorGrabbed = true;
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      Renderer.Render(Program.Instance);
      SwapBuffers();
    }

    int time = Environment.TickCount;
    int fps = 0;
    int lastFPS = 0;

    bool _firstMove = true;
    Vector2 _lastPos;
    const float cameraSpeed = 5.0f;
    const float sensitivity = 0.3f;

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      if (KeyboardState.IsKeyDown(Keys.Escape)) Close();

      fps++;
      int tick = Environment.TickCount;
      if (tick - time >= 1000)
      {
        lastFPS = fps;
        fps = 0;
        time = tick;
      }
      Title = $"Project2398 | FPS: {lastFPS} | {Renderer.Camera.Position.X.ToString("f3")}/{Renderer.Camera.Position.Y.ToString("f3")}/{Renderer.Camera.Position.Z.ToString("f3")}";

      if (KeyboardState.IsKeyDown(Keys.E))
      {
        CursorGrabbed = !CursorGrabbed;
      }

      if (KeyboardState.IsKeyDown(Keys.W))
      {
        Renderer.Camera.Position += Vector3.Normalize(Renderer.Camera.Front * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Forward
      }
      if (KeyboardState.IsKeyDown(Keys.S))
      {
        Renderer.Camera.Position -= Vector3.Normalize(Renderer.Camera.Front * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Backwards
      }
      if (KeyboardState.IsKeyDown(Keys.A))
      {
        Renderer.Camera.Position -= Vector3.Normalize(Renderer.Camera.Right * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Left
      }
      if (KeyboardState.IsKeyDown(Keys.D))
      {
        Renderer.Camera.Position += Vector3.Normalize(Renderer.Camera.Right * new Vector3(1.0f, 0.0f, 1.0f)) * cameraSpeed * (float)e.Time; // Right
      }
      if (KeyboardState.IsKeyDown(Keys.Space))
      {
        Renderer.Camera.Position += Vector3.UnitY * cameraSpeed * (float)e.Time; // Up
      }
      if (KeyboardState.IsKeyDown(Keys.LeftShift))
      {
        Renderer.Camera.Position -= Vector3.UnitY * cameraSpeed * (float)e.Time; // Down
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
        Renderer.Camera.Yaw += deltaX * sensitivity;
        Renderer.Camera.Pitch -= deltaY * sensitivity;
      }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);
      GL.Viewport(0, 0, Size.X, Size.Y);
      Renderer.Camera.AspectRatio = Size.X / (float)Size.Y;
    }
  }
}