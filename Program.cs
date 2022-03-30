using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Project2398
{
  public static class Program
  {
    private static void Main()
    {
      Console.WriteLine(2 << 1 - 1);
      NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
      {
        Size = new Vector2i(800, 600),
        Title = "Project2398",
        // This is needed to run on macos
        Flags = ContextFlags.ForwardCompatible,
      };

      Window window = new Window(GameWindowSettings.Default, nativeWindowSettings);
      window.Run();
    }
  }
}