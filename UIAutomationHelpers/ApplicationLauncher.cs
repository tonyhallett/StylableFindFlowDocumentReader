using FlaUI.Core;

namespace UIAutomationHelpers
{
    public static class ApplicationLauncher
    {
        public static Application Launch(string projectName)
        {
            return Application.Launch(GetAppPath(projectName));
        }

        private static string GetAppPath(string projectName)
        {
            var uiTestsDirectory =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var root = new DirectoryInfo(uiTestsDirectory!)!.Parent!.Parent!.Parent!.Parent;
            return Path.Combine(root!.FullName, projectName, "bin", "Debug", $"{projectName}.exe");
        }
    }
}
