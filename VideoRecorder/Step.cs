using FlaUI.Core.AutomationElements;

namespace VideoRecorder
{
    internal class Step
    {
        public Step(Action<Window> action, int wait)
        {
            Action = action;
            Wait = wait;
        }
        public Action<Window> Action { get; }
        public int Wait { get; }
    }
}
