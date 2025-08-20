using System.Reflection;
using FlaUI.Core;

namespace UIAutomationHelpers
{
    public static class ApplicationLauncher
    {
        public static Application Launch(string projectName)
        {
            return Application.Launch(GetAppPath(projectName));
        }

        public static string GetSolutionPath()
        {

            var directory = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            while (directory!.Name != "StylableFindFlowDocumentReader")
            {
                directory = directory.Parent;
            }
            return directory.FullName;
            //var assemblyDirectoryPath =
            //    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //return new DirectoryInfo(assemblyDirectoryPath!)!.Parent!.Parent!.Parent!.Parent!.FullName;
        }

        private static string GetAppPath(string projectName)
        {
            var root = GetSolutionPath();
            var bin = Path.Combine(root, projectName, "bin");
            return GetLatest(bin, projectName);
        }

        private static string GetLatest(string binDirectory,string projectName)
        {
            var debugPath = GetExePath(binDirectory, projectName, true);
            var releasePath = GetExePath(binDirectory, projectName, false);
            var debugPathExists = File.Exists(debugPath);
            var releasePathExists = File.Exists(releasePath);
            if ((debugPathExists && !releasePathExists))
            {
                return debugPath;
            }
            if((!debugPathExists && releasePathExists))
            {
                return releasePath;
            }
            if (debugPathExists && releasePathExists)
            {
                var debugTime = File.GetLastWriteTime(debugPath);
                var releaseTime = File.GetLastWriteTime(releasePath);
                return debugTime > releaseTime ? debugPath : releasePath;
            }
            throw new Exception($"Neither {debugPath} nor {releasePath} exists.");
        }

        private static string GetExePath(string binDirectory, string projectName, bool isDebug)
            => Path.Combine(binDirectory, isDebug ? "Debug" : "Release", $"{projectName}.exe");
    }
}
