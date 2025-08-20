using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using UIAutomationHelpers;

namespace UITests
{
    [TestFixture(false)]
    [TestFixture(true)]
    internal sealed class OpenedFindToolbarTests(bool isNormal) : FindToolBarTestsBase(isNormal)
    {
        private Button? _findButton;

        protected override Application StartApplication()
        {
            Application application = base.StartApplication();
            _findButton = ControlFinder.FindFindButton(Window);
            _findButton.Click();
            AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
            return application;
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Close_Find_Toolbar_When_Click_Find_Button()
        {
            _findButton!.Click();

            AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Null, "Find toolbar should be null");
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Close_Find_Toolbar_When_Press_Escape()
        {
            Typer.TypeEsc();

            AssertClosed();
        }

        /*
            Below is the text 
            This is a simple example of a FlowDocumentReader with a stylable Find area.
        */

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Up_When_ShiftF3()
        {
            SelectMiddle();
            FindsTest("s", "simple ", Typer.TypeShiftF3);
        }

        private void SelectMiddle()
        {
            FocusFindTextAndSetText("FlowDocumentReader");
            Typer.TypeEnter();

            AssertSelected("FlowDocumentReader ", "FlowDocumentReader");
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Down_When_F3()
        {
            SelectMiddle();

            FindsTest("s", "stylable ", Typer.TypeF3);
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Down_When_FindDown_Button_Is_Clicked()
            => FindsTest("s", "This ", () =>
            {
                Typer.TypeTab(2);
                Typer.TypeEnter();
            });

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Up_When_FindUp_Button_Is_Clicked()
        {
            SelectMiddle();

            FindsTest("s", "simple ", () =>
            {
                Typer.TypeTab();
                Typer.TypeEnter();
            });
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        // search down is the default
        public void Should_Find_Down_When_Enter_First_Pressed()
            => FindsTest("s", "This ", Typer.TypeEnter);

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Last_Searched_Direction_When_Press_Enter()
        {
            // start at the far right
            FocusFindTextAndSetText("area");
            Typer.TypeEnter();

            // switch from default
            FindsTest("s", "stylable ", Typer.TypeShiftF3);

            FindsTest("s", "simple ", Typer.TypeEnter);

            FindsTest("s", "is ", Typer.TypeEnter);

            // switch
            FindsTest("s", "simple ", Typer.TypeF3);

            FindsTest("s", "stylable ", Typer.TypeEnter);
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Menu_Test()
        {
            FocusFindTextAndSetText("Area");
            Menu findMenu = ControlFinder.FindFindMenu(Window);
            MenuItem rootItem = findMenu.Items[0];
            _ = rootItem.Expand();
            MenuItem matchCaseMenuItem = rootItem.Items[1];
            _ = matchCaseMenuItem.Invoke();

            Typer.TypeF3();
            Window? findWindow = ControlFinder.FindCannotFindWindow(Window);
            string message = findWindow!.FindFirstChild(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text)).AsTextBox()!.Text;
            Assert.That(message, Is.EqualTo("Searched to the end of this document. Cannot find 'Area'."));
            findWindow.Close();
        }

        private void FindsTest(string findText, string expectedEnclosingWord, Action findAction)
        {
            FocusFindTextAndSetText(findText);

            findAction();

            AssertSelected(expectedEnclosingWord, findText);
        }

        private void FocusFindTextAndSetText(string text)
        {
            TextBox? findTextBox = ControlFinder.FindFindTextBox(Window);
            findTextBox!.Focus();
            findTextBox!.Text = text;
        }

        private void AssertSelected(string expectedEnclosingWord, string expectedSelectedText)
        {
            AutomationElement? document = ControlFinder.FindFlowDocument(Window);
            ITextRange[] selectionTextRanges = document!.Patterns.Text.Pattern.GetSelection();
            Assert.That(selectionTextRanges.Count, Is.EqualTo(1));
            ITextRange selectedTextRange = selectionTextRanges[0];
            Assert.That(selectedTextRange.GetText(-1), Is.EqualTo(expectedSelectedText));
            selectedTextRange.ExpandToEnclosingUnit(FlaUI.Core.Definitions.TextUnit.Word);
            Assert.That(selectedTextRange.GetText(-1), Is.EqualTo(expectedEnclosingWord));
        }

        private void AssertClosed()
        {
            AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Null, "Find toolbar should be null");
        }
    }
}
