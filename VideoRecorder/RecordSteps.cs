using UIAutomationHelpers;

namespace VideoRecorder
{
    internal sealed class RecordSteps
    {
        private static readonly int s_navigationDelay = 500;
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
            => new(window =>
            {
                FlaUI.Core.AutomationElements.Button findButton = ControlFinder.FindFindButton(window!);
                findButton.Click();
            }, 1000);
        private static List<Step> InputTextSteps(string text)
        {
            List<Step> steps = [
                new Step(window =>
                {
                    FlaUI.Core.AutomationElements.TextBox? findTextBox = ControlFinder.FindFindTextBox(window!);
                    findTextBox!.Focus();
                }, 1000),
                ..TypeWordSteps(text,100)
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
            new(_ => Typer.TypeTab(),s_navigationDelay),
            new(_ => Typer.TypeTab(),s_navigationDelay),
            new(_ => Typer.TypeTab(),s_navigationDelay),
            new(_ => Typer.TypeEnter(),s_navigationDelay)
        ];

        private static List<Step> NavigateToMenuSelectMatchCaseSteps(int numTabs)
        {
            List<Step> steps =
            [
                ..Enumerable.Range(0,numTabs).Select(_ =>
                    new Step(_ => Typer.TypeTab(),s_navigationDelay)
                ),
                new Step(_ => Typer.TypeEnter(),s_navigationDelay),
                new Step(_ => Typer.TypeDown(),s_navigationDelay),
                new Step(_ => Typer.TypeEnter(),s_navigationDelay)
            ];

            return steps;
        }

        private static Step CloseFindWindowStep() => new(window => ControlFinder.FindCannotFindWindow(window)!.Close(), 2000);
    }
}
