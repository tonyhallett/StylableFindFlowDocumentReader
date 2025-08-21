using UIAutomationHelpers;

namespace UITests.Tests.Enhanced
{
    internal sealed class KeyCommandsTests() : FindToolBarTestsBase(false)
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
