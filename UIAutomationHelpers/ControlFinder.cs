using FlaUI.Core.AutomationElements;

namespace UIAutomationHelpers
{
    public static class ControlFinder
    {
        public static Button FindFindButton(Window window)
            => window.FindFirstDescendant(cf => cf.ByAutomationId("FindButton")).AsButton()!;

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
