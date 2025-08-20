using FlaUI.Core.AutomationElements;

namespace VideoRecorder
{
    internal sealed class Step(Action<Window> action, int wait)
    {
        public Action<Window> Action { get; } = action;

        public int Wait { get; } = wait;
    }
}
