using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace StylableFindFlowDocumentReader
{
    internal class KeyCommand {
        private static List<RoutedUICommand> keyCommands = new List<RoutedUICommand>
        {
            ApplicationCommands.Find,
            NavigationCommands.NextPage,
            NavigationCommands.PreviousPage,
            NavigationCommands.LastPage,
            NavigationCommands.FirstPage,
        };
        private static bool IsKeyCommand(ICommand command) => keyCommands.Contains(command);
        public static KeyCommand TryCreate(KeyBinding kb)
        {
            if (KeyCommand.IsKeyCommand(kb.Command) && kb.Gesture is KeyGesture gesture)
            {
                return new KeyCommand(kb.Command as RoutedUICommand, gesture, kb.CommandParameter);
            }
            return null;
        }

        private readonly KeyGesture gesture;
        private readonly object commandParameter;
        private readonly RoutedUICommand command;
        public KeyCommand(RoutedUICommand command, KeyGesture gesture,object commandParameter)
        {
            this.command = command;
            this.gesture = gesture;
            this.commandParameter = commandParameter;
        }

        private bool GestureMatch(KeyEventArgs e)
        {
            return e.Key == gesture.Key &&
            Keyboard.Modifiers == gesture.Modifiers;
        }

        public bool IsMatch(KeyEventArgs e, UIElement target)
        {
            if (GestureMatch(e))
            {
                if (command.CanExecute(commandParameter, target))
                {
                    command.Execute(commandParameter, target);
                }
                return true;
            }
            return false;
        }
    }
}
