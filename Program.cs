using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Project2398.Client;
using Project2398.Common;

namespace Project2398
{
  public static class Program
  {
    static World world = new World();
    static String[] modsDir = Directory.GetDirectories("./Mods");

    static void Init()
    {
      Array.ForEach<String>(modsDir, (String s) =>
      {
        Console.WriteLine($"Loading mod: {s}");
        Mod.Load(s);
      });
    }

    static void Main(String[] args)
    {
      Init();

      if (Array.Exists<String>(args, (String arg) => { return arg == "--server"; }))
      {
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