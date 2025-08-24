using System.Collections;
using UIAutomationHelpers;

namespace UITests.NUnit
{
    internal sealed class ComparisonsTestFixtureSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            bool[] isNormals = [true, false];
            FrameworkVersion[] frameworkVersions = Enum.GetValues<FrameworkVersion>();
            foreach (bool isNormal in isNormals)
            {
                foreach (FrameworkVersion frameworkVersion in frameworkVersions)
                {
                    yield return new object[] { isNormal, frameworkVersion };
                }
            }
        }
    }
}
