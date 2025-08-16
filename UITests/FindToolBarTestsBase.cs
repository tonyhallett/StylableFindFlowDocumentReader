using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.TestUtilities;
using FlaUI.UIA3;

namespace UITests
{
    public abstract class FindToolBarTestsBase : FlaUITestBase
    {
        protected Window? window;
        protected override VideoRecordingMode VideoRecordingMode => VideoRecordingMode.NoVideo;

        protected override AutomationBase GetAutomation() => new UIA3Automation();

        protected override Application StartApplication()
        {
            var application = DemoApplicationLauncher.Launch();
            window = application.GetMainWindow(Automation);
            return application;
        }
    }
}
