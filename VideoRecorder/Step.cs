using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;

namespace VideoRecorder
{
    internal sealed class Step
    {
        public Step(Action<Window> action, int wait)
        {
            Action = action;
            Wait = wait;
        }
        public Action<Window> Action { get; }
        public int Wait { get; }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIAware();
    }
}
