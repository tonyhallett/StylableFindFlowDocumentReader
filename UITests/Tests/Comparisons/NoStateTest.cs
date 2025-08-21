using FlaUI.Core.AutomationElements;
using UIAutomationHelpers;
using UITests.TestHelpers;

namespace UITests.Tests.Comparisons
{
    [TestFixture(false)]
    [TestFixture(true)]
    internal sealed class NoStateTest(bool isNormal)
        : FindToolBarTestsBase(isNormal)
    {
        [Test]
        public void Closing_Removes_Find_Toolbar_State()
        {
            ControlFinder.FindFindButton(Window).Click(); // open

            TextBox? findTextBox = ControlFinder.FindFindTextBox(Window);
            findTextBox!.Text = "abc";
            MenuItem matchCaseMenuItem = MenuHelper.SelectMatchCase(Window);
            Assert.That(matchCaseMenuItem.IsChecked, Is.True);

            ControlFinder.FindFindButton(Window).Click(); // close

            ControlFinder.FindFindButton(Window).Click(); // open

            findTextBox = ControlFinder.FindFindTextBox(Window);
            Assert.That(findTextBox!.Text, Is.Empty);
            matchCaseMenuItem = MenuHelper.GetMatchCaseMenuItem(Window);
            Assert.That(matchCaseMenuItem.IsChecked, Is.False);
        }
    }
}
