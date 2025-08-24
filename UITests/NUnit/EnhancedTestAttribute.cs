namespace UITests.NUnit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    internal sealed class EnhancedTestAttribute : TestFixtureSourceAttribute
    {
        public EnhancedTestAttribute()
            : base(typeof(FrameworkVersionsFixtureSource), nameof(FrameworkVersionsFixtureSource.FixtureArgs))
        {
        }
    }
}
