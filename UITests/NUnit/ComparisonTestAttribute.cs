namespace UITests.NUnit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    internal sealed class ComparisonTestAttribute : TestFixtureSourceAttribute
    {
        public ComparisonTestAttribute()
            : base(typeof(ComparisonsTestFixtureSource))
        {
        }
    }
}
