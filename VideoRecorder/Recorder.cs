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

        public static async Task<string?> Record(bool isDemo, List<Step> steps, AutomationBase automation, CaptureSettings? captureSettings = null)
        {
            var projectName = isDemo ? "Demo" : "Normal";
            _application = ApplicationLauncher.Launch(projectName);
            try
            {
                _window = _application.GetMainWindow(automation)!;
                Thread.Sleep(1000);
                var videoPath = await StartRecording(projectName,captureSettings);
                ExecuteSteps(steps);
                StopRecording();
                return videoPath;
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

        private static async Task<string> StartRecording(string projectName, CaptureSettings? captureSettings)
        {
            var videosDirectory = Path.Combine(ApplicationLauncher.GetSolutionPath(), "videos");
            if (!Directory.Exists(videosDirectory))
            {
                Directory.CreateDirectory(videosDirectory);
            }

            VideoRecorderSettings videoRecorderSettings = new VideoRecorderSettings
            {
                VideoFormat = VideoFormat.xvid,
                VideoQuality = 6,
                TargetVideoPath = Path.Combine(videosDirectory,$"{projectName}.avi"),
                LogMissingFrames = false,
            };
           
            videoRecorderSettings.ffmpegPath = await FfmpegInstallationHelper.GetFfmpegPathAsync();

            _videoRecorder = new FlauVideoRecorder(videoRecorderSettings, (_) =>
            {
                return Capture.Rectangle(AddSpaceForMenu(_window!.BoundingRectangle), captureSettings);
                
            });
            return videoRecorderSettings.TargetVideoPath;
        }

        private static System.Drawing.Rectangle AddSpaceForMenu(System.Drawing.Rectangle windowBounds)
        {
            var space = 200;
            return new System.Drawing.Rectangle
            (
                windowBounds.X - space,
                windowBounds.Y - space,
                windowBounds.Width + 2 * space,
                windowBounds.Height + 2 * space
            );
        }
    }
}
