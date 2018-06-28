using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Vsixprop.TextOperator;
using Task = System.Threading.Tasks.Task;

namespace Vsixprop
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class PropCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0101;
		/// <summary>
		/// Command ID.
		/// </summary>
		public const int SqlCommandId = 0x0102;

		/// <summary>
		/// Command menu group (command set GUID).
		/// </summary>
		public static readonly Guid CommandSet = new Guid("7146690e-d52d-482d-a0b0-6def6d78d46d");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private PropCommand(Package package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);

			CommandID subCommandID = new CommandID(CommandSet, (int)PropCommand.SqlCommandId);
			MenuCommand subItem = new MenuCommand(
				new EventHandler(SubItemCallback), subCommandID);
			commandService.AddCommand(subItem);
		}
		private void SubItemCallback(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			var abc = GetSelection(ServiceProvider);
			var myToolsOptionsPackage = this.package as PropCommandPackage;
			var operate = new OrmOperator();
			operate.ParamObj = myToolsOptionsPackage.config;
			operate.ConnectStr = myToolsOptionsPackage.config.OptionString;
			operate.DbType = myToolsOptionsPackage.config.DBType;
			var propString = operate.DoPropString(abc.Text);
			if (string.IsNullOrEmpty(propString))
			{
				return;
			}
			SetCurrentTextView();
			SetSelection(propString, abc);
		}

		/// <summary>
		/// Gets the instance of the command.
		/// </summary>
		public static PropCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
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
            // Verify the current thread is the UI thread - the call to AddCommand in PropCommand's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();
            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new PropCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var lineSection = GetSelection(ServiceProvider);
			if (string.IsNullOrEmpty(lineSection.Text))
			{
				return;
			}
			var propString = new StringPropOperator().DoPropString(lineSection.Text);
			SetCurrentTextView();
			SetSelection(propString, lineSection);
        }


		private void SetCurrentTextView()
		{
			TextViewCreationListener._view = ServiceHelper .GetWpfTextView(this.ServiceProvider);

		}
		private void SetSelection(string str, TextViewSelection selection)
		{
			var edit=TextViewCreationListener._view.TextBuffer.CreateEdit();
			var viewSelection = TextViewCreationListener._view.Selection;
			var startPoint = (int)viewSelection.Start.Position;
			var endPoint = (int)viewSelection.End.Position;
			if (!selection.IsSelection)
			{
				startPoint = selection.StartPosition.Posion;
				endPoint = selection.EndPosition.Posion;
			}
			edit.Delete(startPoint, endPoint - startPoint);
			edit.Insert(startPoint, str);
			edit.Apply();
		}
		/// <summary>
		/// get string you selected or string of the current line
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		private TextViewSelection GetSelection(IServiceProvider serviceProvider)
        {
			bool isSelection = true;
            var view= ServiceHelper.GetIvsTextView(this.ServiceProvider);
			view.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn);//end could be before beginning
            view.GetSelectedText(out string selectedText);
			var start = new TextViewPosition(startLine, startColumn,0);
			var end = new TextViewPosition(endLine, endColumn,0);
			//if selectedtext=empty get current line text
			if (string.IsNullOrEmpty(selectedText))
			{
				var wpf = ServiceHelper.GetWpfTextView(this.ServiceProvider);
				var line = wpf.TextViewLines[startLine];
				startColumn = 0;
				endColumn = (int)line.Length;
				view.GetTextStream(startLine, startColumn, startLine, (int)line.Length, out selectedText);
				if (selectedText!=null)
				{
					selectedText = selectedText.Replace("\t", "").Trim();
					start = new TextViewPosition(startLine, startColumn, line.Start);
					end = new TextViewPosition(endLine, endColumn, line.End);
					isSelection = false;
				}
			
			}
			
			TextViewSelection selection = new TextViewSelection(start, end, selectedText);
			selection.IsSelection = isSelection;

			return selection;
        }
    }
}
