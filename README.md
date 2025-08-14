# StylableFindFlowDocumentReader

The FindToolBar is an internal class and as such its elements can only be accessed in code through the wpf tree.

The FindRestylingFlowDocumentReader supports supplying your own Find toolbar and has some additional features.

**Due to the fact that it uses knowledge of the internals of FlowDocumentReader, it is only known to work with .Net Framework 4.7.2**

**Has only been tested in the scenario where the FlowDocument is set once and IsFindEnabled is true and does not change. **

For reference the original FlowDocumentReader xaml, extracted with DotPeek, is available for viewing in the Xaml directory.

The FlowDocumentReader is in generic.xaml obtained from PresentationUI Themes/generic.baml
```
  <Style x:Key="{ComponentResourceKey TypeInTargetAssembly={x:Type MappingPIGen2:PresentationUIStyleResources}, ResourceId=PUIFlowDocumentReader}"
         x:Uid="Style_732" TargetType="{x:Type FlowDocumentReader}">
```
For the find toolbar
generic.xaml
```
  <Style x:Key="{ComponentResourceKey TypeInTargetAssembly={x:Type MappingPIGen2:PresentationUIStyleResources}, ResourceId=PUIFlowViewers_FindToolBar}"
         x:Uid="Style_733" TargetType="{x:Type ToolBar}">
```
FindToolbar.xaml obtained from PresentationUI ms/internal/documents/findtoolbar.baml

# How to use.

There are two ways to use this control.
1. Use the restyle functionality.
... todo
2. Set the FindToolbarContent property in xaml and bind to the properties of the FindToolBarViewModel.

Controls are provided for the parts of the find toolbar and are pretty much the same as the original.

FindToolbar - replicates the actual find toolbar.  Just add your controls inside.

FindTextBox is a TextBox and a hint Label, both with Transparent Background.  If you provide your own you need to bind to the FindToolBarViewModel FindText as well as
setting `x:Name="findTextBox"`



Dependency properties.
| Property | Default value |
| --- | --- |
| ShowTooltip | true |
| Tooltip | "Search for a word or phrase in this document." |
| HintText | "Search" |
| HintOpacity | 0.7 |
| HintFontStyle | FontStyles.Italic |
| TextBoxWidth | 183 |
| Foreground | Brushes.Black| 

FindNextPreviousButtons.  If you provide your own you need to bind to the FindToolBarViewModel NextCommand and PreviousCommand.

Dependency properties.
| Property | Default value |
| --- | --- |
| ShowTooltips | true |
| FindNextTooltip | "Find Next" |
| FindPreviousTooltip | "Find Previous" |
| Foreground ( icon colour ) | Default |

The FindToolBarViewModel also has IsSearchUp and IsSearchDown properties if you want to style your own buttons.


FindMenu. If you provide your own your MenuItem options need to bind to the FindToolBarViewModel properties:
MatchWholeWord
MatchCase
MatchDiacritic
MatchKashida
MatchAlefHamza

Dependency properties.

The "State" properties are fallbacks for the
IsMouseOver
IsKeyboardFocused
triggers and multi trigger
IsPressed trigger.


| Property | Default value |
| --- | --- |
| MenuBackground | null |
| MenuBorder | null |
| MenuItemBackground | null |
| MenuItemHoverBackground | null |
| MenuItemForeground | null |
| MenuItemHoverForeground | null |
| MenuItemBorderBrush | null |
| MenuItemHoverBorderBrush | null |
| SelectedGlyphBrush | null |
| SelectedGlyphBackground | null |
| SelectedGlyphBorder | null |
| DropDownGlyphBrush | null |
| DropDownGlyphMouseOver | null |
| DropDownGlyphKeyboardFocused | null |
| DropDownGlyphMouseOverFocused | null |
| DropDownGlyphPressed | null |
| DropDownStateGlyph | null |
| DropDownGlyphDisabledBrush | null |
| DropDownStateBackground | null |
| DropDownBackgroundPressed | null |
| DropDownBackgroundMouseOver | null |
| DropDownBackgroundKeyboardFocused | null |
| DropDownBackgroundMouseOverFocused | null |
| DropDownStateBorder | null |
| DropDownBorderPressed | null |
| DropDownBorderMouseOver | null |
| DropDownBorderKeyboardFocused | null |
| DropDownBorderMouseOverFocused | null |
| DropDownTooltip | "Find..." |
| ShowDropDownTooltip | true |


## Key bindings
Note that there are some keys that are processed by the FlowDocumentReader

Ctrl~M for cycling between the modes.

Ctrl~ + and - for zooming in and out.

F3 for showing the find toolbar.  Once shown will search. Shift key to search backwards.

Enter key when text box has focus will search with current search direction.


This fills the gap - specify a KeyBinding in the XAML - e.g
```xaml
<FindRestylingFlowDocumentReader.InputBindings>
    <KeyBinding Command="ApplicationCommands.Find" Modifiers="Ctrl" Key="F" />
    <KeyBinding Command="NavigationCommands.NextPage" Modifiers="Ctrl" Key="N" />
    <KeyBinding Command="NavigationCommands.PreviousPage" Modifiers="Ctrl" Key="P" />
</FindRestylingFlowDocumentReader.InputBindings>
```
Also supports ``NavigationCommands.LastPage`` and ``NavigationCommands.FirstPage``.

## Protected methods

The FindRestylingFlowDocumentReader has some protected methods that can be overridden.

To function, the PART_FindToolBarHost, which by default is 
```xaml
<Border x:Name="PART_FindToolBarHost" VerticalAlignment="Center"
        HorizontalAlignment="Left" Visibility="Collapsed"/>
```
has to be replaced with another decorator that has a conditional `Child` property.
When called for WPF rendering, the `Child` property is the original host with child the FindToolbarContent.
If the wrapping decorator needs properties then override CopyHostProperties.

`DoDispatch` used for focusing the textbox when the toolbar is shown. Defaults to `Dispatcher.BeginInvoke(action, DispatcherPriority.Background)`
