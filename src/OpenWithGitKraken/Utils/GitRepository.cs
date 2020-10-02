using System;
using System.Diagnostics;
using System.IO;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace OpenWithGitKraken.Utils
{
    public class GitRepository
    {
        public static string GetFromSelection(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // selection on Project level
            var projectFolder = GetProjectFolder(dte);
            if (!string.IsNullOrEmpty(projectFolder) && ContainsDotGitFolder(projectFolder))
            {
                return projectFolder;
            }

            // selection on Solution level
            var solutionFolder = GetSolutionFolder(dte);
            if (!string.IsNullOrEmpty(solutionFolder) && ContainsDotGitFolder(solutionFolder))
            {
                return solutionFolder;
            }

            // no valid .git folder found
            return null;
        }

        private static string GetProjectFolder(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Project project = null;
            try
            {
                var activeProjects = (Array)dte.ActiveSolutionProjects;
                if (activeProjects != null && activeProjects.Length > 0)
                {
                    project = activeProjects.GetValue(0) as Project;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            if (project == null || string.IsNullOrEmpty(project.FullName))
            {
                return null;
            }

            string fullPath = null;
            try
            {
                fullPath = project.Properties.Item("FullPath").Value as string;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return Directory.Exists(fullPath) ? fullPath : null;
        }

        private static string GetSolutionFolder(DTE2 dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (dte.Solution != null && !string.IsNullOrEmpty(dte.Solution.FullName))
            {
                return Path.GetDirectoryName(dte.Solution.FullName);
            }

            return null;
        }

        private static bool ContainsDotGitFolder(string folderPath)
        {
            return Directory.Exists($"{folderPath}\\.git");
        }
    }
}
