using Xunit;

namespace OpenWithGitKraken.Tests.Fixtures
{
    [CollectionDefinition(CollectionName.SetupTestRepositories)]
    public class RepositoriesCollection : ICollectionFixture<RepositoriesFixture>
    {
    }
}
