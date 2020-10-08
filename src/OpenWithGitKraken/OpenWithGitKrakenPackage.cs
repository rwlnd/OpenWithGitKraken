using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace OpenWithGitKraken
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(OpenWithGitKrakenPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class OpenWithGitKrakenPackage : AsyncPackage
    {
        /// <summary>
        /// OpenWithGitKrakenPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "bdc0ff4d-06dc-48c1-b8c1-5674a6fc19d0";

        #region Package Members

        /// <summary>
        /// Initialization of the package
        /// </summary>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await OpenWithGitKraken.InitializeAsync(this);
        }

        #endregion
    }
}
