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

    public static void Load(String path)
    {
      env.DoChunk(path + "/init.lua");
    }
  }
}