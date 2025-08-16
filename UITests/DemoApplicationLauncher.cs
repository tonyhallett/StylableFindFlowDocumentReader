using FlaUI.Core;

namespace UITests
{
    internal static class DemoApplicationLauncher
    {
        public static Application Launch()
        {
            return Application.Launch(GetAppPath());
        }

        private static string GetAppPath()
        {
            var uiTestsDirectory =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var root = new DirectoryInfo(uiTestsDirectory).Parent.Parent.Parent.Parent;
            return Path.Combine(root!.FullName, "Demo", "bin", "Debug", "Demo.exe");
        }
    }
}
