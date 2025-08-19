using System.Reflection;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using FlaUI.Core.Tools;
using UIAutomationHelpers;
using FlauVideoRecorder = FlaUI.Core.Capturing.VideoRecorder;

namespace VideoRecorder
{
    internal class Recorder
    {
        private static Application? _application;
        private static Window? _window;
        private static FlauVideoRecorder? _videoRecorder;

        public static async Task Record(bool isDemo, List<Step> steps, AutomationBase automation)
        {
            var projectName = isDemo ? "Demo" : "Normal";
            _application = ApplicationLauncher.Launch(projectName);
            try
            {
                _window = _application.GetMainWindow(automation)!;
                await StartRecording(projectName);
                ExecuteSteps(steps);
                StopRecording();
            }
            finally
            {
                CloseApp();
            }
        }

        private static void ExecuteSteps(List<Step> steps)
        {
            foreach (var step in steps)
            {
                Thread.Sleep(step.Wait);
                step.Action(_window!);
            }
        }

        private static void CloseApp()
        {
            _application!.Close();
            Retry.WhileFalse(() => _application.HasExited, TimeSpan.FromSeconds(2.0), null, throwOnTimeout: false, ignoreException: true);
            _application.Dispose();
            _application = null;
        }

        private static void StopRecording() => _videoRecorder!.Stop();

        private static async Task StartRecording(string projectName)
        {
            var directory = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            while(directory!.Name != "StylableFindFlowDocumentReader")
            {
                directory = directory.Parent;
            }

            var videosDirectory = Path.Combine(directory.FullName, "videos");
            if (!Directory.Exists(videosDirectory))
            {
                Directory.CreateDirectory(videosDirectory);
            }

            VideoRecorderSettings videoRecorderSettings = new VideoRecorderSettings
            {
                VideoFormat = VideoFormat.xvid,
                VideoQuality = 6,
                TargetVideoPath = Path.Combine(videosDirectory,$"{projectName}.avi")
            };
           
            var ffmpegPath = GetFfmpegPath();
            if (ffmpegPath == null)
            {
                // it won't download again if ffmpeg.exe exists in the target folder 
                // todo create directory in the solution
                ffmpegPath = await FlauVideoRecorder.DownloadFFMpeg("C:\\temp");
            }
            videoRecorderSettings.ffmpegPath = ffmpegPath;
            // check a directory for ffmpeg and download to it
            // could check an xml file if present for it 

            _videoRecorder = new FlauVideoRecorder(videoRecorderSettings, (_) =>
            {
                return Capture.MainScreen();
            });
        }

        private static string? GetFfmpegPath()
        {
            return null;
        }

    }
}
