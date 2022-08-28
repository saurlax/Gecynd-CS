using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Gecynd.Client;
using Gecynd.Common;

namespace Gecynd
{
  public static class Program
  {
    public static Gecynd Instance = new Gecynd();

    static void Main(String[] args)
    {
      Instance.LoadMod();

      if (Array.Exists<String>(args, (String arg) => { return arg == "--server"; }))
      {
        Console.WriteLine("Starting in pure server mode.");
      }
      else
      {
        NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
        {
          Size = new Vector2i(800, 600),
          Title = "Gecynd",
          NumberOfSamples = 4,
          // This is needed to run on macos
          Flags = ContextFlags.ForwardCompatible,
        };
        Window window = new Window(GameWindowSettings.Default, nativeWindowSettings);
        window.Run();
      }
    }
  }
}