using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.TestUtilities;
using FlaUI.UIA3;
using UIAutomationHelpers;

namespace UITests
{
    public abstract class FindToolBarTestsBase : FlaUITestBase
    {
        protected Window? window;
        private string _projectName;
        protected readonly bool _isNormal;

        protected override VideoRecordingMode VideoRecordingMode => VideoRecordingMode.NoVideo;

        public FindToolBarTestsBase(bool isNormal)
        {
            _isNormal = isNormal;
            _projectName = isNormal ? "Normal" : "Demo";
        }

        protected override AutomationBase GetAutomation() => new UIA3Automation();

        protected override Application StartApplication()
        {
            var application = ApplicationLauncher.Launch(_projectName);
            window = application.GetMainWindow(Automation);
            return application;
        }
    }
}
