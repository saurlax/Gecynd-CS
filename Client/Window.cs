using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace Project2398.Client
{
  public class Window : GameWindow
  {
    public Player player;
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
      player = new Player(new Vector3(0, 0, 0));
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

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      
      if (KeyboardState.IsKeyDown(Keys.Escape)) Close();

      player.HandleMove(e, KeyboardState);
    }
  }
}