using EnvDTE;
using EnvDTE80;
using Moq;
using OpenWithGitKraken.Utils;
using Xunit;

namespace OpenWithGitKraken.Tests
{
    [VsTestSettings(UIThread = true)]
    public class When_solution_is_git_repository
    {
        private readonly string _rootDir;

        public When_solution_is_git_repository()
        {
            TestSetup.EnsureRepositoriesTestSetup();
            _rootDir = TestSetup.TestSetupRootDirectory;
        }

        [VsFact(Version = "2019")]
        public void With_project_no_git_repo_selected__Should_return_solution_repo_location()
        {
            // Arrange
            var SOLUTION_LOCATION = $@"{_rootDir}\solution-git-repo";
            var SELECTION_LOCATION = $@"{_rootDir}\solution-git-repo\project-no-git-repo";

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
            Assert.Equal(SOLUTION_LOCATION, result);
        }

        [VsFact(Version = "2019")]
        public void With_project_git_repo_selected__Should_return_project_repo_location()
        {
            // Arrange
            var SOLUTION_LOCATION = $@"{_rootDir}\solution-git-repo";
            var SELECTION_LOCATION = $@"{_rootDir}\solution-git-repo\project-git-repo";

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
            Assert.Equal(SELECTION_LOCATION, result);
        }
    }
}
