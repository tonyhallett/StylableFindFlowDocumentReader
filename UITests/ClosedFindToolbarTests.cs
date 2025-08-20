using UIAutomationHelpers;

namespace UITests
{
    [TestFixture(false)]
    [TestFixture(true)]
    internal sealed class ClosedFindToolbarTests(bool isNormal) : FindToolBarTestsBase(isNormal)
    {
        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Show_Find_Toolbar_When_Click_Find_Button()
        {
            FlaUI.Core.AutomationElements.Button findButton = ControlFinder.FindFindButton(Window);
            findButton.Click();
            AssertShowsFindToolbar();
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Show_Find_Toolbar_When_Press_F3()
        {
            FlaUI.Core.AutomationElements.AutomationElement? fdr = ControlFinder.FindFlowDocument(Window);
            // throws when try to focus...
            fdr!.Click();
            Typer.TypeF3();

            AssertShowsFindToolbar();
        }

        private void AssertShowsFindToolbar()
        {
            FlaUI.Core.AutomationElements.AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
            string expectedAutomationId = IsNormal ? "FindToolbar" : "replacedfindToolBar";
            Assert.That(findToolbar.AutomationId, Is.EqualTo(expectedAutomationId));
        }
    }
}
