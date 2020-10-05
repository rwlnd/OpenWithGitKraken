using System.IO;

namespace OpenWithGitKraken.Tests.Setup
{
    public static class TestRepositoriesSetup
    {
        /// <summary>
        /// Sets up the necessary folders to test the repository selection logic
        /// </summary>
        public static void EnsureRepositoriesTestSetup()
        {
            // TestSetups folder
            Directory.CreateDirectory(TestSetupRootDirectory);

            // solution-git-repo
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-git-repo");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-git-repo\.git");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-git-repo\project-git-repo");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-git-repo\project-git-repo\.git");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-git-repo\project-no-git-repo");

            // solution-no-git-repo
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-no-git-repo");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-no-git-repo\project-git-repo");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-no-git-repo\project-git-repo\.git");
            Directory.CreateDirectory($@"{TestSetupRootDirectory}\solution-no-git-repo\project-no-git-repo");
        }


        public static string RootDirectory => Directory.GetCurrentDirectory();
        public static string TestSetupRootDirectory => $@"{RootDirectory}\TestRepositories";
    }
}
