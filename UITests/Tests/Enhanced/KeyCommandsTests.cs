using UIAutomationHelpers;
using UITests.NUnit;

namespace UITests.Tests.Enhanced
{
    [EnhancedTest]
    internal sealed class KeyCommandsTests(FrameworkVersion frameworkVersion) : FindToolBarTestsBase(false, frameworkVersion)
    {
        [Test]
        public void Should_Execute_The_KeyBindings_In_InputBindings_When_Pressed()
        {
            FlaUI.Core.AutomationElements.AutomationElement? flowDocument = ControlFinder.FindFlowDocument(Window);
            flowDocument!.Focus();

            Typer.TypeCtrlF();

            AssertShowsFindToolbar();
        }
    }
}
