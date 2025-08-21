using System.Reflection;
using FlaUI.Core;

namespace UIAutomationHelpers
{
    public static class ApplicationLauncher
    {
        public static Application Launch(string projectName) => Application.Launch(GetAppPath(projectName));

        public static string GetSolutionPath()
        {
            var directory = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            while (directory!.Name != "StylableFindFlowDocumentReader")
            {
                directory = directory.Parent;
            }

            return directory.FullName;
        }

        private static string GetAppPath(string projectName)
        {
            string root = GetSolutionPath();
            string bin = Path.Combine(root, projectName, "bin");
            return GetLatest(bin, projectName);
        }

        private static string GetLatest(string binDirectory, string projectName)
        {
            string debugPath = GetExePath(binDirectory, projectName, true);
            string releasePath = GetExePath(binDirectory, projectName, false);
            bool debugPathExists = File.Exists(debugPath);
            bool releasePathExists = File.Exists(releasePath);
            if (debugPathExists && !releasePathExists)
            {
                return debugPath;
            }

            if (!debugPathExists && releasePathExists)
            {
                return releasePath;
            }

            if (debugPathExists && releasePathExists)
            {
                DateTime debugTime = File.GetLastWriteTime(debugPath);
                DateTime releaseTime = File.GetLastWriteTime(releasePath);
                return debugTime > releaseTime ? debugPath : releasePath;
            }

            throw new Exception($"Neither {debugPath} nor {releasePath} exists.");
        }

        private static string GetExePath(string binDirectory, string projectName, bool isDebug)
            => Path.Combine(binDirectory, isDebug ? "Debug" : "Release", "net472", $"{projectName}.exe");
    }
}
