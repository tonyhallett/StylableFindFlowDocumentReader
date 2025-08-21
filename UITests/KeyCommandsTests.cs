using UIAutomationHelpers;
using UITests.NUnit;

namespace UITests
{
    internal sealed class KeyCommandsTests() : FindToolBarTestsBase(false)
    {
        [UiTest]
        public void Should_Execute_The_KeyBindings_In_InputBindings_WHen_Pressed()
        {
            FlaUI.Core.AutomationElements.AutomationElement? flowDocument = ControlFinder.FindFlowDocument(Window);
            flowDocument!.Focus();

            Typer.TypeCtrlF();

            AssertShowsFindToolbar();
        }
    }
}
