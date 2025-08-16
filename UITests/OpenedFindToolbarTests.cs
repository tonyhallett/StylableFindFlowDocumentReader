using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

namespace UITests
{
    public class OpenedFindToolbarTests : FindToolBarTestsBase
    {
        private Button? findButton;

        protected override Application StartApplication()
        {
            var application = base.StartApplication();
            findButton = DemoAppFinder.FindFindButton(window!);
            findButton.Click();
            var findToolbar = DemoAppFinder.FindFindToolbar(window!);
            Assert.That(findToolbar, Is.Not.Null, "Find toolbar should not be null");
            return application;
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Close_Find_Toolbar_When_Click_Find_Button()
        {
            findButton!.Click();

            var findToolbar = DemoAppFinder.FindFindToolbar(window!);
            Assert.That(findToolbar, Is.Null, "Find toolbar should be null");
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Close_Find_Toolbar_When_Press_Escape()
        {
            PressEsc();

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
            FindsTest("s", "simple ", () => PressShiftF3());
        }

        private void PressShiftF3() => Keyboard.TypeSimultaneously(VirtualKeyShort.SHIFT, VirtualKeyShort.F3);
        private void PressF3() => Keyboard.Type(VirtualKeyShort.F3);
        private void PressEnter() => Keyboard.Type(VirtualKeyShort.ENTER);
        private void PressEsc() => Keyboard.Type(VirtualKeyShort.ESC);
        private void PressTab(int times = 1)
        {
            for(var i = 0; i < times; i++)
            {   
                Keyboard.Type(VirtualKeyShort.TAB);
            }
        }

        private void SelectMiddle()
        {
            SetFindText("FlowDocumentReader");
            PressEnter();

            AssertSelected("FlowDocumentReader ","FlowDocumentReader");
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Down_When_F3()
        {
            SelectMiddle();
            
            FindsTest("s", "stylable ", () => PressF3());
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Down_When_FindDown_Button_Is_Clicked()
            => FindsTest("s", "This ", () =>
            {
                PressTab(2);
                PressEnter();
            });

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Up_When_FindUp_Button_Is_Clicked()
        {
            SelectMiddle();

            FindsTest("s", "simple ", () =>
            {
                PressTab();
                PressEnter();
            });
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        // search down is the default
        public void Should_Find_Down_When_Enter_First_Pressed()
            => FindsTest("s", "This ", () => PressEnter());


        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Should_Find_Last_Searched_Direction_When_Press_Enter()
        {
            // start at the far right
            SetFindText("area");
            PressEnter();

            // switch from default
            FindsTest("s", "stylable ", () => PressShiftF3());

            FindsTest("s", "simple ", () => PressEnter());

            FindsTest("s", "is ", () => PressEnter());

            // switch
            FindsTest("s", "simple ", () => PressF3());

            FindsTest("s", "stylable ", () => PressEnter());
        }

        [RequiresThread(ApartmentState.STA)]
        [Test]
        public void Menu_Test()
        {
            SetFindText("Area");
            var findMenu = DemoAppFinder.FindFindMenu(window!);
            var rootItem = findMenu.Items.First();
            rootItem.Expand();
            var matchCaseMenuItem = rootItem.Items[1];
            matchCaseMenuItem.Invoke();

            PressF3();
            var findWindow = window!.FindFirstDescendant(cf => cf.ByName("Find")).AsWindow();
            var message = findWindow!.FindFirstChild(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text)).AsTextBox()!.Text;
            // check source to see if this is language dependent
            Assert.That(message, Is.EqualTo("Searched to the end of this document. Cannot find 'Area'."));
        }

        private void FindsTest(string findText,string expectedEnclosingWord,Action findAction)
        {
            SetFindText(findText);
            
            findAction();

            AssertSelected(expectedEnclosingWord, findText);
        }

        private void SetFindText(string text)
        {
            var findTextBox = DemoAppFinder.FindFindTextBox(window!);
            findTextBox!.Text = text;
        }

        private void AssertSelected(string expectedEnclosingWord,string expectedSelectedText)
        {
            var document = DemoAppFinder.FindFlowDocument(window!);
            var selectionTextRanges = document!.Patterns.Text.Pattern.GetSelection();
            Assert.That(selectionTextRanges.Count, Is.EqualTo(1));
            var selectedTextRange = selectionTextRanges[0];
            Assert.That(selectedTextRange.GetText(-1), Is.EqualTo(expectedSelectedText));
            selectedTextRange.ExpandToEnclosingUnit(FlaUI.Core.Definitions.TextUnit.Word);
            Assert.That(selectedTextRange.GetText(-1), Is.EqualTo(expectedEnclosingWord));
        }

        private void AssertClosed()
        {
            var findToolbar = DemoAppFinder.FindFindToolbar(window!);
            Assert.That(findToolbar, Is.Null, "Find toolbar should be null");
        }
    }
}
