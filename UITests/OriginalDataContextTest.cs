using System.Drawing;
using FlaUI.Core.AutomationElements;
using UIAutomationHelpers;
using UITests.NUnit;

namespace UITests
{
    internal sealed class OriginalDataContextTest() : FindToolBarTestsBase(false)
    {
        [UiTest]
        public void Should_Be_What_Would_Have_Inherited()
        {
            ControlFinder.FindFindButton(Window).Click();

            TextBox? findTextBox = ControlFinder.FindFindTextBox(Window);

            AssertForegroundColorsEqual(findTextBox!, Color.FromArgb(0, Color.DarkBlue));

            RadioButton? pinkRadioButton = Window.FindFirstDescendant(cf => cf.ByName("Pink")).AsRadioButton();
            pinkRadioButton!.IsChecked = true;

            AssertForegroundColorsEqual(findTextBox!, Color.FromArgb(0, Color.DeepPink));
        }

        private void AssertForegroundColorsEqual(TextBox textBox, Color expectedColor)
        {
            FlaUI.Core.ITextRange textRangePattern = textBox.Patterns.Text.Pattern.DocumentRange;
            int foregroundInt = (int)textRangePattern.GetAttributeValue(Automation.TextAttributeLibrary.ForegroundColor);
            Color foregroundColor = Color.FromArgb(foregroundInt);
            Assert.That(foregroundColor.ToArgb(), Is.EqualTo(expectedColor.ToArgb()));
        }
    }
}
