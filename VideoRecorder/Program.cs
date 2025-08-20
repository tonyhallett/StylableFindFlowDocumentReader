using FlaUI.Core.Capturing;
using FlaUI.UIA3;
using VideoRecorder;

// https://github.com/FlaUI/FlaUI/issues/359
// https://github.com/FlaUI/FlaUI/issues/306
NativeMethods.SetProcessDPIAware();

using (var automation = new UIA3Automation())
{
    List<Step> steps = RecordSteps.GetSteps();
    CaptureSettings? captureSettings = null;
    var demoVideoPath = await Recorder.Record(true, steps, automation,captureSettings);
    var normalVideoPath = await Recorder.Record(false, steps, automation, captureSettings);
    if (demoVideoPath != null && normalVideoPath != null)
    {
        await GifConverter.ConvertAsync(demoVideoPath, normalVideoPath);
    }
    VideoDeleter.Delete(demoVideoPath, normalVideoPath);
}
