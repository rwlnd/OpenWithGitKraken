using EnvDTE;
using EnvDTE80;
using Moq;
using OpenWithGitKraken.Tests.Fixtures;
using OpenWithGitKraken.Tests.Setup;
using OpenWithGitKraken.Utils;
using Xunit;

namespace OpenWithGitKraken.Tests
{
    [VsTestSettings(UIThread = true)]
    [Collection(CollectionName.SetupTestRepositories)]
    public class When_solution_is_not_git_repository
    {
        private readonly string _rootDir;

        public When_solution_is_not_git_repository()
        {
            _rootDir = TestRepositoriesSetup.TestSetupRootDirectory;
        }

        [VsFact(Version = "2019")]
        public void With_project_selected_FullPath_empty__Should_return_null()
        {
            // Arrange
            var SELECTION_LOCATION = $@"{_rootDir}\solution-no-git-repo\project-git-repo";

            var mockedDTE = new Mock<DTE2>();
            var mockedProject = new Mock<Project>();
            mockedProject.Setup(x => x.FullName).Returns("");
            mockedProject.Setup(x => x.Properties.Item("FullPath").Value).Returns(SELECTION_LOCATION);

            var activeSolutionProjects = new[] { mockedProject.Object };
            mockedDTE.Setup(x => x.ActiveSolutionProjects).Returns(activeSolutionProjects);

            // Act
            var result = GitRepository.GetFromSelection(mockedDTE.Object);

            // Assert
            Assert.Null(result);
        }

        [VsFact(Version = "2019")]
        public void With_project_no_git_repo_selected__Should_return_null()
        {
            // Arrange
            var SOLUTION_LOCATION = $@"{_rootDir}\solution-no-git-repo";
            var SELECTION_LOCATION = $@"{_rootDir}\solution-no-git-repo\project-no-git-repo";

            var mockedDTE = new Mock<DTE2>();
            var mockedProject = new Mock<Project>();
            mockedProject.Setup(x => x.FullName).Returns(SELECTION_LOCATION);
            mockedProject.Setup(x => x.Properties.Item("FullPath").Value).Returns(SELECTION_LOCATION);
            mockedDTE.Setup(x => x.Solution.FullName).Returns(SOLUTION_LOCATION);

            var activeSolutionProjects = new[] { mockedProject.Object };
            mockedDTE.Setup(x => x.ActiveSolutionProjects).Returns(activeSolutionProjects);

            // Act
            var result = GitRepository.GetFromSelection(mockedDTE.Object);

            // Assert
            Assert.Null(result);
        }

        [VsFact(Version = "2019")]
        public void With_project_git_repo_selected__Should_return_repository_location()
        {
            // Arrange
            var SELECTION_LOCATION = $@"{_rootDir}\solution-no-git-repo\project-git-repo";

            var mockedDTE = new Mock<DTE2>();
            var mockedProject = new Mock<Project>();
            mockedProject.Setup(x => x.FullName).Returns(SELECTION_LOCATION);
            mockedProject.Setup(x => x.Properties.Item("FullPath").Value).Returns(SELECTION_LOCATION);

            var activeSolutionProjects = new[] { mockedProject.Object };
            mockedDTE.Setup(x => x.ActiveSolutionProjects).Returns(activeSolutionProjects);

            // Act
            var result = GitRepository.GetFromSelection(mockedDTE.Object);

            // Assert
            Assert.Equal(SELECTION_LOCATION, result);
        }

        [VsFact(Version = "2019")]
        public void With_solution_selected_FullName_empty__Should_return_null()
        {
            // Arrange
            var mockedDTE = new Mock<DTE2>();
            var mockedProject = new Mock<Project>();
            mockedProject.Setup(x => x.FullName).Returns("");
            mockedDTE.Setup(x => x.Solution.FullName).Returns("");

            var activeSolutionProjects = new[] { mockedProject.Object };
            mockedDTE.Setup(x => x.ActiveSolutionProjects).Returns(activeSolutionProjects);

            // Act
            var result = GitRepository.GetFromSelection(mockedDTE.Object);

            // Assert
            Assert.Null(result);
        }

        [VsFact(Version = "2019")]
        public void With_solution_selected__Should_return_repository_location()
        {
            // Arrange
            var SELECTION_LOCATION = $@"{_rootDir}\solution-git-repo";

            var mockedDTE = new Mock<DTE2>();
            var mockedProject = new Mock<Project>();
            mockedProject.Setup(x => x.FullName).Returns("");
            mockedDTE.Setup(x => x.Solution.FullName).Returns(SELECTION_LOCATION);

            var activeSolutionProjects = new[] { mockedProject.Object };
            mockedDTE.Setup(x => x.ActiveSolutionProjects).Returns(activeSolutionProjects);

            // Act
            var result = GitRepository.GetFromSelection(mockedDTE.Object);

            // Assert
            Assert.Equal(SELECTION_LOCATION, result);
        }
    }
}
