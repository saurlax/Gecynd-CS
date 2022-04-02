using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Project2398
{
  public static class Program
  {
    private class TestC
    {
      public string myString;
    }
    private static void Main()
    {
      Console.WriteLine("Hello Project2398");

      // TestC t1 = new TestC();
      // t1.myString = "haha1";
      // TestC t2 = new TestC();
      // t2.myString = "ahah2";
      // TestC[] tcs = new TestC[8];
      // Array.Fill<TestC>(tcs, t1);
      // foreach (TestC c in tcs)
      // {
      //   Console.WriteLine("Let's see value of c before repace element: " + c.myString);
      // }
      // tcs[0] = t2;
      // foreach (TestC c in tcs)
      // {
      //   Console.WriteLine("Let's see value of c after repace element: " + c.myString);
      // }

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