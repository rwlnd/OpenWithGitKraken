using OpenWithGitKraken.Tests.Setup;

namespace OpenWithGitKraken.Tests.Fixtures
{
    public class RepositoriesFixture
    {
        public RepositoriesFixture()
        {
            TestRepositoriesSetup.EnsureRepositoriesTestSetup();
        }
    }
}
