using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

namespace UIAutomationHelpers
{
    public static class Typer
    {
        public static void TypeShiftF3() => Keyboard.TypeSimultaneously(VirtualKeyShort.SHIFT, VirtualKeyShort.F3);

        public static void TypeF3() => Keyboard.Type(VirtualKeyShort.F3);

        public static void TypeEnter() => Keyboard.Type(VirtualKeyShort.ENTER);

        public static void TypeEsc() => Keyboard.Type(VirtualKeyShort.ESC);

        public static void TypeTab(int times = 1)
            => TypeRepeatedly(VirtualKeyShort.TAB, times);

        public static void TypeDown(int times = 1)
            => TypeRepeatedly(VirtualKeyShort.DOWN, times);

        private static void TypeRepeatedly(VirtualKeyShort key,int times)
        {
            for (var i = 0; i < times; i++)
            {
                Keyboard.Type(key);
            }
        }


        public static void TypeShiftTab()
        {
            Keyboard.TypeSimultaneously(VirtualKeyShort.SHIFT, VirtualKeyShort.TAB);
        }

        public static IEnumerable<Action> TypeWord(string word)
        {
            return word.Select(ch =>
            {
                Action action;
                // Map char to VirtualKeyShort
                if (Enum.TryParse<VirtualKeyShort>($"KEY_{ch.ToString().ToUpper()}", out var key))
                {
                    if (char.IsUpper(ch))
                    {
                        action = () => Keyboard.TypeSimultaneously(VirtualKeyShort.SHIFT, key);
                    }
                    else
                    {
                        action = () => Keyboard.Type(key);
                    }
                    return action;
                }
                else
                {
                    throw new NotSupportedException($"Character '{ch}' is not mapped to VirtualKeyShort");
                }
            });
        }
    }
}
