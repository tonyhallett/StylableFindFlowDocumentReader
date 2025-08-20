using UIAutomationHelpers;

namespace VideoRecorder
{
    internal class RecordSteps
    {
        private static int navigationDelay = 500;
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

        
        private static Step KeepAlive(int duration)
        {
            return new Step(_ => { }, duration);
        }

        private static Step ClickFindButtonStep()
        {
            return new Step(window =>
            {
                var findButton = ControlFinder.FindFindButton(window!);
                findButton.Click();
            }, 1000);
        }
        private static List<Step> InputTextSteps(string text)
        {
            List<Step> steps = [ new Step(window =>
    {
        var findTextBox = ControlFinder.FindFindTextBox(window!);
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

        private static List<Step> NavigateToSearchForwardAndPressSteps()
        {
            return new List<Step>
    {
        new Step(_ =>
        {
            Typer.TypeTab();
        },navigationDelay),
        new Step(_ =>
        {
            Typer.TypeTab();
        },navigationDelay),
        new Step(_ =>
        {
            Typer.TypeTab();
        },navigationDelay),
        new Step(_ =>
        {
            Typer.TypeEnter();
        },navigationDelay)
    };
        }

        private static List<Step> NavigateToMenuSelectMatchCaseSteps(int numTabs)
        {
            List<Step> steps =
            [
            ..Enumerable.Range(0,numTabs).Select(_ =>
            new Step(_ =>
            {
                Typer.TypeTab();
            },navigationDelay)),
        new Step(_ =>
        {
            Typer.TypeEnter();
        },navigationDelay),
        new Step(window =>
        {
            Typer.TypeDown();
        },navigationDelay),
        new Step(window =>
        {
            Typer.TypeEnter();
        },navigationDelay)
            ];

            return steps;
        }

        private static Step CloseFindWindowStep()
        {
            return new Step(window =>
            {
                ControlFinder.FindCannotFindWindow(window)!.Close();
            }, 2000);
        }
    }
}
