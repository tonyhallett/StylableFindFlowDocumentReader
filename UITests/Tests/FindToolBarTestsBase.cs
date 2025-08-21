using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.TestUtilities;
using FlaUI.UIA3;
using UIAutomationHelpers;

[assembly: RequiresThread(ApartmentState.STA)]

namespace UITests.Tests
{
    internal abstract class FindToolBarTestsBase(bool isNormal)
        : FlaUITestBase
    {
        private Window? _window;

        protected Window Window => _window!; // For derived classes to access the window directly

        private readonly string _projectName = isNormal ? "Normal" : "Demo";

        protected bool IsNormal { get; } = isNormal;

        protected override VideoRecordingMode VideoRecordingMode => VideoRecordingMode.NoVideo;

        protected override AutomationBase GetAutomation() => new UIA3Automation();

        protected override Application StartApplication()
        {
            Application application = ApplicationLauncher.Launch(_projectName);
            _window = application.GetMainWindow(Automation);
            return application;
        }

        protected void AssertShowsFindToolbar()
        {
            AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
            string expectedAutomationId = IsNormal ? "FindToolbar" : "replacedFindToolBar";
            Assert.That(findToolbar!.AutomationId, Is.EqualTo(expectedAutomationId));
        }
    }
}
