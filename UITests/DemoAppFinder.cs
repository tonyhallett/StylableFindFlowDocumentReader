using FlaUI.Core.AutomationElements;

namespace UITests
{
    internal static class DemoAppFinder {
        public static Button FindFindButton(Window window) 
            => window.FindFirstDescendant(cf => cf.ByAutomationId("FindButton")).AsButton()!;

        public static AutomationElement? FindFindToolbar(Window window)
            => window.FindFirstDescendant(cf => cf.ByAutomationId("findToolBar"));

        public static TextBox? FindFindTextBox(Window window)
            => window.FindFirstDescendant(cf => cf.ByAutomationId("findTextBox")).AsTextBox()!;

        public static AutomationElement? FindFlowDocument(Window window)
            => window.FindFirstDescendant(cf => cf.ByClassName("Document"));

        public static Menu FindFindMenu(Window window)
        {
            return window.FindFirstDescendant(cf => cf.ByAutomationId("findToolbarMenu")).AsMenu()!;
        }
    }
}
