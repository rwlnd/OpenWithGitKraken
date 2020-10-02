using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using EnvDTE;
using EnvDTE80;
using OpenWithGitKraken.Utils;
using Process = System.Diagnostics.Process;
using Task = System.Threading.Tasks.Task;

namespace OpenWithGitKraken
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class OpenWithGitKraken
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("07eba17a-57a2-465e-b9b9-6aa91293a3f8");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;


        /// <summary>
        /// Reference to the the top-level object in the Visual Studio automation object model
        /// </summary>
        private readonly DTE2 _dte;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenWithGitKraken"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private OpenWithGitKraken(AsyncPackage package, OleMenuCommandService commandService)
        {
            _dte = Package.GetGlobalService(typeof(DTE)) as DTE2;

            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static OpenWithGitKraken Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in OpenWithGitKraken's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new OpenWithGitKraken(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                var command = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\gitkraken\\update.exe";
                var gitFolder = GitRepository.GetFromSelection(_dte);
                var arguments = $"--processStart=gitkraken.exe --process-start-args=\"-p \"{gitFolder}\"";

                var start = new ProcessStartInfo(command, arguments)
                {
                    LoadUserProfile = true,
                    UseShellExecute = false
                };

                using (Process.Start(start)) { }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
