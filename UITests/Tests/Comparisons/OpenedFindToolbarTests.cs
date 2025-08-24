using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using UIAutomationHelpers;
using UITests.NUnit;
using UITests.TestHelpers;

namespace UITests.Tests.Comparisons
{
    [ComparisonTest]
    internal sealed class OpenedFindToolbarTests(bool isNormal, FrameworkVersion frameworkVersion)
        : FindToolBarTestsBase(isNormal, frameworkVersion)
    {
        private Button? _findButton;

        protected override Application StartApplication()
        {
            Application application = base.StartApplication();
            _findButton = ControlFinder.FindFindButton(Window);
            _findButton!.Click();
            AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
            return application;
        }

        [Test]
        public void Should_Focus_The_TextBox()
        {
            RetryResult<TextBox?> findTextBoxResult = Retry.WhileNull(() =>
            {
                TextBox? findTextBox = ControlFinder.FindFindTextBox(Window);
                return findTextBox?.Properties.HasKeyboardFocus ? findTextBox : null;
            });
            Assert.That(findTextBoxResult.Result, Is.Not.Null, "Find text box should not be null");
        }

        [Test]
        public void Should_Close_Find_Toolbar_When_Click_Find_Button()
        {
            _findButton!.Click();

            AutomationElement? findToolbar = ControlFinder.FindFindToolbar(Window);
            Assert.That(findToolbar, Is.Null, "Find toolbar should be null");
        }

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

        [Test]
        public void Should_Find_Down_When_F3()
        {
            SelectMiddle();

            FindsTest("s", "stylable ", Typer.TypeF3);
        }

        [Test]
        public void Should_Find_Down_When_FindDown_Button_Is_Clicked()
            => FindsTest("s", "This ", () =>
            {
                Typer.TypeTab(2);
                Typer.TypeEnter();
            });

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

        [Test]
        public void Should_Find_Down_When_Enter_First_Pressed() // search down is the default
            => FindsTest("s", "This ", Typer.TypeEnter);

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

        [Test]
        public void Menu_Test()
        {
            const string cannotFindText = "Area";
            FocusFindTextAndSetText(cannotFindText);
            _ = MenuHelper.SelectMatchCase(Window);
            Typer.TypeF3();

            AssertCannotFind(cannotFindText);
        }

        [Test]
        public void Should_Respect_IsFindEnabled()
        {
            FocusFindTextAndSetText("abc");

            RadioButton? findDisabledRadioButton = ControlFinder.FindFindDisabledRadioButton(Window);
            findDisabledRadioButton!.IsChecked = true;

            _ = Retry.WhileNotNull(() => ControlFinder.FindFindToolbar(Window), throwOnTimeout: true);

            RadioButton? findEnabledRadioButton = ControlFinder.FindFindEnabledRadioButton(Window);
            findEnabledRadioButton!.IsChecked = true;

            RetryResult<Button?> findButtonResult = Retry.WhileNull(() => ControlFinder.FindFindButton(Window));
            findButtonResult.Result!.Click();

            AssertShowsFindToolbar();

            TextBox? findTextBox = ControlFinder.FindFindTextBox(Window);
            Assert.That(findTextBox!.Text, Is.Empty);
        }

        [Test]
        public void Should_Hide_ToolBar_When_Document_Is_Set_To_Null() => throw new System.NotImplementedException();

        private void AssertCannotFind(string findText)
        {
            RetryResult<Window?> findWindowResult = Retry.WhileNull(() => ControlFinder.FindCannotFindWindow(Window));
            Window? findWindow = findWindowResult.Result;
            TextBox? findWindowTextBox = findWindow!.FindFirstChild(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text)).AsTextBox();
            Assert.That(findWindowTextBox, Is.Not.Null, "Find window text box should not be null");
            Assert.That(findWindowTextBox!.Text, Is.EqualTo($"Searched to the end of this document. Cannot find '{findText}'."));
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
