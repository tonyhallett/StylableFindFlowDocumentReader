using FlaUI.Core.AutomationElements;

namespace UIAutomationHelpers
{
    public static class ControlFinder
    {
        public static Button FindFindButton(Window window)
#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly - https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3515
            => window.FindFirstDescendant(cf => cf.ByAutomationId("FindButton")).AsButton()!;
#pragma warning restore SA1009 // Closing parenthesis should be spaced correctly

        public static AutomationElement? FindFindToolbar(Window window)
            => window.FindFirstDescendant(cf => cf.ByClassName("ToolBar"));

        public static TextBox? FindFindTextBox(Window window)
           => window.FindFirstDescendant(cf => cf.ByClassName("TextBox")).AsTextBox();

        public static AutomationElement? FindFlowDocument(Window window)
            => window.FindFirstDescendant(cf => cf.ByClassName("Document"));

        public static Menu? FindFindMenu(Window window)
            => window.FindFirstDescendant(cf => cf.ByClassName("Menu")).AsMenu();

        public static AutomationElement? FindFlowDocumentReader(Window window)
            => window!.FindFirstDescendant(cf => cf.ByClassName("FlowDocumentReader"));

        public static Window? FindCannotFindWindow(Window window)
            => window.FindFirstDescendant(cf => cf.ByName("Find")).AsWindow();
    }
}
