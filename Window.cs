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
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,  0.0f,  0.0f, -1.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 0.0f,  0.0f,  0.0f, -1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  0.0f,  0.0f, -1.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  0.0f,  0.0f, -1.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,  0.0f,  0.0f, -1.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,  0.0f,  0.0f, -1.0f,

    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  0.0f,  0.0f,  1.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,  0.0f,  0.0f,  1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,  0.0f,  0.0f,  1.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,  0.0f,  0.0f,  1.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,  0.0f,  0.0f,  1.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  0.0f,  0.0f,  1.0f,

    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f, -1.0f,  0.0f,  0.0f,
    -0.5f,  0.5f, -0.5f,  1.0f, 1.0f, -1.0f,  0.0f,  0.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, -1.0f,  0.0f,  0.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, -1.0f,  0.0f,  0.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f, -1.0f,  0.0f,  0.0f,
    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f, -1.0f,  0.0f,  0.0f,

     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  1.0f,  0.0f,  0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  1.0f,  0.0f,  0.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  1.0f,  0.0f,  0.0f,
     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  1.0f,  0.0f,  0.0f,
     0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  1.0f,  0.0f,  0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  1.0f,  0.0f,  0.0f,

    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  0.0f, -1.0f,  0.0f,
     0.5f, -0.5f, -0.5f,  1.0f, 1.0f,  0.0f, -1.0f,  0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,  0.0f, -1.0f,  0.0f,
     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,  0.0f, -1.0f,  0.0f,
    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,  0.0f, -1.0f,  0.0f,
    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,  0.0f, -1.0f,  0.0f,

    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,  0.0f,  1.0f,  0.0f,
     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,  0.0f,  1.0f,  0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  0.0f,  1.0f,  0.0f,
     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,  0.0f,  1.0f,  0.0f,
    -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,  0.0f,  1.0f,  0.0f,
    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,  0.0f,  1.0f,  0.0f
    };
    private readonly float[] _ground = {
    -50.0f, 0.0f,  50.0f,   0.0f,  50.0f, 0.0f, 1.0f, 0.0f,
     50.0f, 0.0f,  50.0f,  50.0f,  50.0f, 0.0f, 1.0f, 0.0f,
    -50.0f, 0.0f, -50.0f,   0.0f,   0.0f, 0.0f, 1.0f, 0.0f,
     50.0f, 0.0f,  50.0f,  50.0f,  50.0f, 0.0f, 1.0f, 0.0f,
    -50.0f, 0.0f, -50.0f,   0.0f,   0.0f, 0.0f, 1.0f, 0.0f,
     50.0f, 0.0f, -50.0f,  50.0f,   0.0f, 0.0f, 1.0f, 0.0f
    };
    private int _vertexBufferObject, _vertexArrayObject;
    private Shader _shader, groundShader, lightShader;
    private Texture _texture0;
    private Texture _texture1;
    private int groundVAO, groundVBO;
    private bool _firstMove = true;
    private Vector2 _lastPos;
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
      base.OnLoad();
      GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
      _camera = new Camera(new Vector3(0.0f, 1.0f, 0.0f), Size.X / (float)Size.Y);
      CursorGrabbed = true;

      _vertexArrayObject = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject);
      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
      _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
      _shader.Use();
      lightShader = new Shader("Shaders/light.vert", "Shaders/light.frag");
      lightShader.Use();

      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
      GL.EnableVertexAttribArray(2);

      _texture0 = Texture.LoadFromFile("Resources/container.png");
      _texture0.Use(TextureUnit.Texture0);
      _shader.SetInt("texture0", 0);
      // _shader.SetInt("texture1", 1);

      groundVAO = GL.GenVertexArray();
      GL.BindVertexArray(groundVAO);
      groundVBO = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, groundVBO);
      GL.BufferData(BufferTarget.ArrayBuffer, _ground.Length * sizeof(float), _ground, BufferUsageHint.StaticDraw);
      _shader.Use();

      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
      GL.EnableVertexAttribArray(2);

      _texture1 = Texture.LoadFromFile("Resources/ground.png");
      _texture1.Use(TextureUnit.Texture1);

      // GL.PolygonMode(MaterialFace.FronCosdBack, PolygonMode.Line);
    }
    private int time = Environment.TickCount;
    private int count = 0;
    private Vector3 lightPos, lightColor;
    protected override void OnRenderFrame(FrameEventArgs e)
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

      lightPos = new Vector3(
        (float)Math.Sin(tick / 5000.0f + Math.PI) * 2.5f,
        (float)Math.Cos(tick / 2500.0f + Math.PI) * 0.5f + 4.0f,
        (float)Math.Cos(tick / 5000.0f + Math.PI) * 2.5f
      );
      lightColor = new Vector3(1.0f, (float)Math.Sin(tick / 1000.0f), 1.0f);

      GL.BindVertexArray(_vertexArrayObject);
      _shader.Use();
      _shader.SetVector3("lightPos", lightPos);
      _shader.SetVector3("lightColor", lightColor);
      _shader.SetVector3("viewPos", _camera.Position);

      _texture0.Use(TextureUnit.Texture0);
      _shader.SetInt("texture0", 0);

      _shader.SetMatrix4("view", _camera.GetViewMatrix());
      _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shader.SetMatrix4("model", Matrix4.CreateScale(1.0f));
      GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);
      _shader.SetMatrix4("model",
             Matrix4.CreateTranslation(
               (float)Math.Sin(tick / 1000.0f) * 2.0f,
               (float)Math.Cos(tick / 1000.0f) + 3.0f,
              (float)Math.Cos(tick / 1000.0f) * 2.0f
             ));
      GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);
      _shader.SetMatrix4("model",
             Matrix4.CreateTranslation(
               (float)Math.Sin(tick / 1000.0f + Math.PI) * 2.0f,
               (float)Math.Cos(tick / 1000.0f + Math.PI) + 3.0f,
              (float)Math.Cos(tick / 1000.0f + Math.PI) * 2.0f
             ));
      GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);

      GL.BindVertexArray(groundVAO);
      _shader.Use();
      _shader.SetMatrix4("model", Matrix4.CreateScale(1.0f));

      _texture1.Use(TextureUnit.Texture1);
      _shader.SetInt("texture0", 1);
      GL.DrawArrays(PrimitiveType.Triangles, 0, _ground.Length / 5);

      GL.BindVertexArray(_vertexArrayObject);
      lightShader.Use();
      lightShader.SetVector3("lightColor", lightColor);
      lightShader.SetMatrix4("view", _camera.GetViewMatrix());
      lightShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
      lightShader.SetMatrix4("model", Matrix4.CreateScale(0.1f) * Matrix4.CreateTranslation(lightPos));
      GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 5);

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