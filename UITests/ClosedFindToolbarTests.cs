using UIAutomationHelpers;
using UITests.NUnit;

namespace UITests
{
    [TestFixture(false)]
    [TestFixture(true)]
    internal sealed class ClosedFindToolbarTests(bool isNormal)
        : FindToolBarTestsBase(isNormal)
    {
        [UiTest]
        public void Should_Show_Find_Toolbar_When_Click_Find_Button()
        {
            FlaUI.Core.AutomationElements.Button findButton = ControlFinder.FindFindButton(Window);
            findButton.Click();
            AssertShowsFindToolbar();
        }

        [UiTest]
        public void Should_Show_Find_Toolbar_When_Press_F3()
        {
            FlaUI.Core.AutomationElements.AutomationElement? flowDocument = ControlFinder.FindFlowDocument(Window);

            flowDocument!.Click();
            Typer.TypeF3();

            AssertShowsFindToolbar();
        }
    }
}
