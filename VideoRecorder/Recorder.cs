using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Capturing;
using FlaUI.Core.Tools;
using UIAutomationHelpers;
using FlauVideoRecorder = FlaUI.Core.Capturing.VideoRecorder;

namespace VideoRecorder
{
    internal sealed class Recorder
    {
        private static Application? s_application;
        private static Window? s_window;
        private static FlauVideoRecorder? s_videoRecorder;

        public static async Task<string?> Record(bool isDemo, List<Step> steps, AutomationBase automation, CaptureSettings? captureSettings = null)
        {
            string projectName = isDemo ? "Demo" : "Normal";
            s_application = ApplicationLauncher.Launch(projectName, FrameworkVersion.Net472);
            try
            {
                Window? mainWindow = s_application.GetMainWindow(automation);
                s_window = mainWindow!;
                Thread.Sleep(1000);
                string videoPath = await StartRecording(projectName, captureSettings);
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
            foreach (Step step in steps)
            {
                Thread.Sleep(step.Wait);
                step.Action(s_window!);
            }
        }

        private static void CloseApp()
        {
            _ = s_application!.Close();
            _ = Retry.WhileFalse(() => s_application.HasExited, TimeSpan.FromSeconds(2.0), null, throwOnTimeout: false, ignoreException: true);
            s_application.Dispose();
            s_application = null;
        }

        private static void StopRecording() => s_videoRecorder!.Stop();

        private static async Task<string> StartRecording(string projectName, CaptureSettings? captureSettings)
        {
            string videosDirectory = Path.Combine(ApplicationLauncher.GetSolutionPath(), "videos");
            if (!Directory.Exists(videosDirectory))
            {
                _ = Directory.CreateDirectory(videosDirectory);
            }

            var videoRecorderSettings = new VideoRecorderSettings
            {
                VideoFormat = VideoFormat.xvid,
                VideoQuality = 6,
                TargetVideoPath = Path.Combine(videosDirectory, $"{projectName}.avi"),
                LogMissingFrames = false,
                ffmpegPath = await FfmpegInstallationHelper.GetFfmpegPathAsync(),
            };

            s_videoRecorder = new FlauVideoRecorder(
                videoRecorderSettings,
                (_) => Capture.Rectangle(AddSpaceForMenu(s_window!.BoundingRectangle), captureSettings));
            return videoRecorderSettings.TargetVideoPath;
        }

        private static System.Drawing.Rectangle AddSpaceForMenu(System.Drawing.Rectangle windowBounds)
        {
            const int space = 200;
            return new System.Drawing.Rectangle(
                windowBounds.X - space,
                windowBounds.Y - space,
                windowBounds.Width + (2 * space),
                windowBounds.Height + (2 * space));
        }
    }
}
