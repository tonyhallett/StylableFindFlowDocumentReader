using System.Collections;
using UIAutomationHelpers;

namespace UITests.NUnit
{
    internal sealed class FrameworkVersionsFixtureSource
    {
        public static IEnumerable FixtureArgs { get; } = Enum.GetValues<FrameworkVersion>();
    }
}
