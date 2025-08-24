using FlaUI.Core.Capturing;
using FlaUI.UIA3;
using UIAutomationHelpers;
using VideoRecorder;

// https://github.com/FlaUI/FlaUI/issues/359
// https://github.com/FlaUI/FlaUI/issues/306
NativeMethods.SetProcessDPIAware();

(string DemoVideoPath, string NormalVideoPath) = await Record();

VideoDeleter.Delete(DemoVideoPath, NormalVideoPath);

static async Task<(string DemoVideoPath, string NormalVideoPath)> Record()
{
    CaptureSettings? captureSettings = null;
    List<Step> steps = RecordSteps.GetSteps();
    using var automation = new UIA3Automation();
    string? demoVideoPath = await Recorder.Record(true, steps, automation, captureSettings);
    string? normalVideoPath = await Recorder.Record(false, steps, automation, captureSettings);
    if (demoVideoPath == null || normalVideoPath == null)
    {
        throw new Exception("Recording failed. One of the video paths is null.");
    }

    await GifConverter.ConvertAsync(demoVideoPath, normalVideoPath);
    return (demoVideoPath, normalVideoPath);
}
