using UIAutomationHelpers;

namespace UITests
{
    [TestFixture(false)]
    [TestFixture(true)]
    public class ClosedFindToolbarTests : FindToolBarTestsBase
    {
        public ClosedFindToolbarTests(bool isNormal):base(isNormal) { }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Show_Find_Toolbar_When_Click_Find_Button()
        {
            var findButton = ControlFinder.FindFindButton(window!);
            findButton.Click();
            AssertShowsFindToolbar();
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Show_Find_Toolbar_When_Press_F3()
        {
            var fdr = ControlFinder.FindFlowDocument(window!);
            // throws when try to focus...
            fdr!.Click();
            Typer.TypeF3();

            AssertShowsFindToolbar();
        }

        private void AssertShowsFindToolbar()
        {
            var findToolbar = ControlFinder.FindFindToolbar(window!);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
            var expectedAutomationId = _isNormal ? "FindToolbar" : "replacedfindToolBar";
            Assert.That(findToolbar.AutomationId, Is.EqualTo(expectedAutomationId));
        }
    }
}
