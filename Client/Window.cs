using System.Diagnostics;
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
    static String name = Process.GetCurrentProcess().ProcessName;
    PerformanceCounter cpu = new PerformanceCounter("Process", "% Processor Time", name);
    PerformanceCounter ram = new PerformanceCounter("Process", "Working Set", name);

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
      base.OnLoad();
      GL.ClearColor(0, 0, 0, 1.0f);
      GL.Enable(EnableCap.DepthTest);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    int time = Environment.TickCount;
    int fps = 0;

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      fps++;
      int tick = Environment.TickCount;
      if (tick - time >= 1000)
      {
        Title = $"Project2398 Early Access | FPS: {fps} | CPU: {cpu.NextValue().ToString("f2")}% | RAM: {(ram.NextValue() / 1024 / 1024).ToString("f2")}MB";
        fps = 0;
        time = tick;
      }
      base.OnUpdateFrame(e);
      if (KeyboardState.IsKeyDown(Keys.Escape)) Close();

    }
  }
}