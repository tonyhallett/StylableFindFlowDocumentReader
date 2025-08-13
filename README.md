# StylableFindFlowDocumentReader

Note that there are some keys that are processed by the FlowDocumentReader
Ctrl~M for cycling between the modes.
Ctrl~ + and - for zooming in and out.
F3 for showing the find toolbar.  Once shown will search. Shift key to search backwards.
Enter key when text box has focus will search with current search direction.


This fills the gap - specify a KeyBinding in the XAML - e.g

<FindRestylingFlowDocumentReader.InputBindings>
    <KeyBinding Command="ApplicationCommands.Find" Modifiers="Ctrl" Key="F" />
    <KeyBinding Command="NavigationCommands.NextPage" Modifiers="Ctrl" Key="N" />
    <KeyBinding Command="NavigationCommands.PreviousPage" Modifiers="Ctrl" Key="P" />
</FindRestylingFlowDocumentReader.InputBindings>

Also supports NavigationCommands.LastPage and NavigationCommands.FirstPage.