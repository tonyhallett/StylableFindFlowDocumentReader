using UIAutomationHelpers;

namespace VideoRecorder
{
    internal sealed class RecordSteps
    {
        private const int NavigationDelay = 500;

        public static List<Step> GetSteps()
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            List<Step> steps = [
              ClickFindButtonStep(),
              ..InputTextSteps("flowDocumentReader"),
              ..NavigateAndToggleMatchCaseAndSearch(3),
              CloseFindWindowStep(),
              ..NavigateAndToggleMatchCaseAndSearch(4),
              KeepAlive(2000)];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
            return steps;
        }

#pragma warning disable SA1000 // Keywords should be spaced correctly
        private static Step KeepAlive(int duration) => new(_ => { }, duration);
#pragma warning restore SA1000 // Keywords should be spaced correctly

        private static Step ClickFindButtonStep()
#pragma warning disable SA1000 // Keywords should be spaced correctly
            => new(window =>
            {
                FlaUI.Core.AutomationElements.Button findButton = ControlFinder.FindFindButton(window!);
                findButton.Click();
            }, 1000);
#pragma warning restore SA1000 // Keywords should be spaced correctly

        private static List<Step> InputTextSteps(string text)
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            List<Step> steps = [
                new Step(
                    window =>
                {
                    FlaUI.Core.AutomationElements.TextBox? findTextBox = ControlFinder.FindFindTextBox(window!);
                    findTextBox!.Focus();
                }, 1000),
                ..TypeWordSteps(text, 100)
            ];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
            return steps;
        }

        private static IEnumerable<Step> TypeWordSteps(string word, int keyDelay)
            => Typer.TypeWord(word).Select(action => new Step(_ => action(), keyDelay));

        private static List<Step> NavigateAndToggleMatchCaseAndSearch(int numTabs)
        {
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            List<Step> steps = [
              ..NavigateToMenuSelectMatchCaseSteps(numTabs),
              ..NavigateToSearchForwardAndPressSteps(),
              ];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
            return steps;

        }

        // https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3850
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly

        // https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3214
#pragma warning disable SA1000 // Keywords should be spaced correctly
        private static List<Step> NavigateToSearchForwardAndPressSteps() => [
            new(_ => Typer.TypeTab(), NavigationDelay),
            new(_ => Typer.TypeTab(), NavigationDelay),
            new(_ => Typer.TypeTab(), NavigationDelay),
            new(_ => Typer.TypeEnter(), NavigationDelay)
        ];
#pragma warning restore SA1000 // Keywords should be spaced correctly
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

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

#pragma warning disable SA1000 // Keywords should be spaced correctly
        private static Step CloseFindWindowStep() => new(window =>
        {
            FlaUI.Core.AutomationElements.Window? cannotFindWindow = ControlFinder.FindCannotFindWindow(window);
            cannotFindWindow!.Close();
        }, 2000);
#pragma warning restore SA1000 // Keywords should be spaced correctly
    }
}
