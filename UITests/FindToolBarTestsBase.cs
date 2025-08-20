using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.TestUtilities;
using FlaUI.UIA3;
using UIAutomationHelpers;

namespace UITests
{
    internal abstract class FindToolBarTestsBase : FlaUITestBase
    {
        private Window? _window;
        protected Window Window => _window!; // For derived classes to access the window directly
        private readonly string _projectName;
        protected bool IsNormal { get; }

        protected override VideoRecordingMode VideoRecordingMode => VideoRecordingMode.NoVideo;

        public FindToolBarTestsBase(bool isNormal)
        {
            IsNormal = isNormal;
            _projectName = isNormal ? "Normal" : "Demo";
        }

        protected override AutomationBase GetAutomation() => new UIA3Automation();

        protected override Application StartApplication()
        {
            Application application = ApplicationLauncher.Launch(_projectName);
            _window = application.GetMainWindow(Automation);
            return application;
        }
    }
}
