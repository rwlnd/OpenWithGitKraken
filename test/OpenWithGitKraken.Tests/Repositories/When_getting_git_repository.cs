using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenWithGitKraken.Utils;

namespace OpenWithGitKraken.Tests.Repositories
{
    [TestClass]
    public class When_getting_git_repository
    {

        [TestMethod]
        public void With_project_selected_with_git_repository__Should_return_repository_location()
        {
            // Arrange
            var mockedDTE = new Mock<DTE2>();
            var mockedProject = new Mock<Project>();
            mockedProject.Setup(x => x.FullName).Returns("test");
            var activeSolutionProjects = new[] { mockedProject.Object };
            mockedDTE.Setup(x => x.ActiveSolutionProjects).Returns(activeSolutionProjects);

            // Act
            var result = GitRepository.GetFromSelection(mockedDTE.Object);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
