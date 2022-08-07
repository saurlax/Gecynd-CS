using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Project2398.Client;
using Project2398.Common;

namespace Project2398
{
  public static class Program
  {
    public static Project2398 Instance = new Project2398();

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
          Title = "Project2398",
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