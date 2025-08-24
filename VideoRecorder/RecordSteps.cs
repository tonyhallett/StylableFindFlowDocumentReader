using UIAutomationHelpers;

namespace VideoRecorder
{
    internal sealed class RecordSteps
    {
        private const int NavigationDelay = 500;

        public static List<Step> GetSteps()
        {
            List<Step> steps = [
              ClickFindButtonStep(),
              ..InputTextSteps("flowDocumentReader"),
              ..NavigateAndToggleMatchCaseAndSearch(3),
              CloseFindWindowStep(),
              ..NavigateAndToggleMatchCaseAndSearch(4),
              KeepAlive(2000)];
            return steps;
        }

        private static Step KeepAlive(int duration) => new(_ => { }, duration);

        private static Step ClickFindButtonStep()
            => new(
                window =>
                {
                    FlaUI.Core.AutomationElements.Button findButton = ControlFinder.FindFindButton(window!)!;
                    findButton.Click();
                },
                1000);

        private static List<Step> InputTextSteps(string text)
        {
            List<Step> steps = [
                new Step(
                    window =>
                    {
                        FlaUI.Core.AutomationElements.TextBox? findTextBox = ControlFinder.FindFindTextBox(window!);
                        findTextBox!.Focus();
                    },
                    1000),
                ..TypeWordSteps(text, 100)
            ];
            return steps;
        }

        private static IEnumerable<Step> TypeWordSteps(string word, int keyDelay)
            => Typer.TypeWord(word).Select(action => new Step(_ => action(), keyDelay));

        private static List<Step> NavigateAndToggleMatchCaseAndSearch(int numTabs)
        {
            List<Step> steps = [
              ..NavigateToMenuSelectMatchCaseSteps(numTabs),
              ..NavigateToSearchForwardAndPressSteps(),
              ];
            return steps;

        }

        private static List<Step> NavigateToSearchForwardAndPressSteps() => [
            new(_ => Typer.TypeTab(), NavigationDelay),
            new(_ => Typer.TypeTab(), NavigationDelay),
            new(_ => Typer.TypeTab(), NavigationDelay),
            new(_ => Typer.TypeEnter(), NavigationDelay)
        ];

        private static List<Step> NavigateToMenuSelectMatchCaseSteps(int numTabs)
        {
            List<Step> steps =
            [
                ..Enumerable.Range(0, numTabs).Select(_ => new Step(_ => Typer.TypeTab(), NavigationDelay)),
                new Step(_ => Typer.TypeEnter(), NavigationDelay),
                new Step(_ => Typer.TypeDown(), NavigationDelay),
                new Step(_ => Typer.TypeEnter(), NavigationDelay)
            ];

            return steps;
        }

        private static Step CloseFindWindowStep() => new(
            window =>
            {
                FlaUI.Core.AutomationElements.Window? cannotFindWindow = ControlFinder.FindCannotFindWindow(window);
                cannotFindWindow!.Close();
            },
            2000);
    }
}
