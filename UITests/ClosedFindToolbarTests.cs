using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

namespace UITests
{
    public class ClosedFindToolbarTests : FindToolBarTestsBase
    {
        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Replace_With_FindToolbarContent_When_Click_Find_Button()
        {
            var findButton = DemoAppFinder.FindFindButton(window!);
            findButton.Click();
            AssertReplacesWithFindToolbarContent();
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Replace_With_FindToolbarContent_When_Press_F3()
        {
            var fdr = window!.FindFirstDescendant(cf => cf.ByAutomationId("FindRestylingFlowDocumentReader"));
            // throws when try to focus...
            fdr!.Click();
            Keyboard.Type(VirtualKeyShort.F3);

            AssertReplacesWithFindToolbarContent();
        }

        private void AssertReplacesWithFindToolbarContent()
        {
            var findToolbar = DemoAppFinder.FindFindToolbar(window!);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
        }
    }
}
