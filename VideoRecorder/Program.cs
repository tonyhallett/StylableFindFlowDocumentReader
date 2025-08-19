using FlaUI.UIA3;
using UIAutomationHelpers;
using VideoRecorder;

using (var automation = new UIA3Automation())
{
    List<Step> steps = [
        ClickFindButtonStep(),
        ..InputTextSteps("flowDocumentReader"),
        ..NavigateAndToggleMatchCaseAndSearch(3),
        CloseFindWindowStep(),
        ..NavigateAndToggleMatchCaseAndSearch(4),
        KeepAlive(2000)];
    await Recorder.Record(true, steps, automation);
    await Recorder.Record(false, steps, automation);
}

Step KeepAlive(int duration)
{
    return new Step(_ => { }, duration);
}

Step ClickFindButtonStep()
{
    return new Step(window =>
    {
        var findButton = ControlFinder.FindFindButton(window!);
        findButton.Click();
    }, 1000);
}
List<Step> InputTextSteps(string text)
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

IEnumerable<Step> TypeWordSteps(string word, int keyDelay)
    => Typer.TypeWord(word).Select(action => new Step(_ => action(), keyDelay));


List<Step> NavigateAndToggleMatchCaseAndSearch(int numTabs)
{
    List<Step> steps = [
        ..NavigateToMenuSelectMatchCaseSteps(numTabs),
        ..NavigateToSearchForwardAndPressSteps(),
        ];
    return steps;

}

List<Step> NavigateToSearchForwardAndPressSteps()
{
    return new List<Step>
    {
        new Step(_ =>
        {
            Typer.TypeTab();
        },1000),
        new Step(_ =>
        {
            Typer.TypeTab();
        },1000),
        new Step(_ =>
        {
            Typer.TypeTab();
        },1000),
        new Step(_ =>
        {
            Typer.TypeEnter();
        },1000)
    };
}

List<Step> NavigateToMenuSelectMatchCaseSteps(int numTabs)
{
    List<Step> steps =
    [
    ..Enumerable.Range(0,numTabs).Select(_ =>
            new Step(_ =>
            {
                Typer.TypeTab();
            },1000)),
        new Step(_ =>
        {
            Typer.TypeEnter();
        },1000),
        new Step(window =>
        {
            Typer.TypeDown();
        },1000),
        new Step(window =>
        {
            Typer.TypeEnter();
        },1000)
    ];

    return steps;
}

Step CloseFindWindowStep()
{
    return new Step(window =>
    {
        ControlFinder.FindCannotFindWindow(window)!.Close();
    }, 2000);
}


