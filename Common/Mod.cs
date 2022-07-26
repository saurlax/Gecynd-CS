using System.Xml;

using Neo.IronLua;

namespace Project2398.Common
{
  public class Mod
  {
    static Lua lua = new Lua();
    static LuaGlobal env = lua.CreateEnvironment();

    public Mod()
    {
    }

    public static void LoadAll()
    {
      Array.ForEach<String>(Directory.GetDirectories("./Mods"), (String s) => { Load(s); });
    }

    public static void Load(String path)
    {
      try
      {
        Console.WriteLine($"Loading mod {path}");
        env.DoChunk(path + "/Init.lua");
      }
      catch (Exception e)
      {
        Console.WriteLine($"Could not load mod {path}:");
        Console.WriteLine(e.Message);
      }
    }
  }
}