using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using Vsixprop.UserControls;
using Task = System.Threading.Tasks.Task;

namespace Vsixprop
{
	[Guid("00000000-0000-0000-0000-000000000000")]
	public class OptionPageCustom : DialogPage
	{
		private bool addSummary = true;

		public bool AddSummary
		{
			get { return addSummary; }
			set { addSummary = value; }
		}

		private string optionValue = "";

		public string connectString
		{
			get { return optionValue; }
			set { optionValue = value; }
		}

		private int dbType = 0;

		public int DBType { get { return dbType; } set { dbType = value; } }
		protected override IWin32Window Window
		{
			get
			{
				MyUserControl page = new MyUserControl();
				page.optionsPage = this;
				page.Initialize();
				return page;
			}
		}
	}
	public class OptionPageGrid : DialogPage
	{
		private int optionInt = 256;

		[Category("PropConfig")]
		[DisplayName("DBType")]
		[Description("数据库类型,0=Mysql,1=Sqlite")]
		public int OptionInteger
		{
			get { return optionInt; }
			set { optionInt = value; }
		}
		private int database = 2;

		[Category("PropConfig")]
		[DisplayName("connectionStr")]
		[Description("数据库连接字符串")]
		private int OptionDataBase
		{
			get { return database; }
			set { database = value; }
		}
	}


	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the
	/// IVsPackage interface and uses the registration attributes defined in the framework to
	/// register itself and its components with the shell. These attributes tell the pkgdef creation
	/// utility what data to put into .pkgdef file.
	/// </para>
	/// <para>
	/// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
	/// </para>
	/// </remarks>
	[PackageRegistration(UseManagedResourcesOnly = true,AllowsBackgroundLoading =true)]
	[InstalledProductRegistration("#110", "#112", "1.1", IconResourceID = 400)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[Guid(PackageGuidString)]
	//[ProvideOptionPage(typeof(OptionPageGrid),
	// "PropConfig", "StringConfig", 0, 0, true)]
	[ProvideOptionPage(typeof(OptionPageCustom),
	 "PropConfig", "DataBaseConfig", 0, 0, true)]
	public sealed class PropCommandPackage : AsyncPackage
	{
		public OptionPageCustom config
		{
			get
			{
				OptionPageCustom page = (OptionPageCustom)GetDialogPage(typeof(OptionPageCustom));
				return page;
			}
		}
		public int DbType
		{
			get
			{
				OptionPageCustom page = (OptionPageCustom)GetDialogPage(typeof(OptionPageCustom));
				return page.DBType;
			}
		}
		public string ConnectString
		{
			get
			{
				var page = (OptionPageCustom)GetDialogPage(typeof(OptionPageCustom));
				return page.connectString;
			}
		}
		/// <summary>
		/// PropCommandPackage GUID string.
		/// </summary>
		public const string PackageGuidString = "fa1bc4a3-4fe2-4e3f-be25-cb67888fe375";

		/// <summary>
		/// Initializes a new instance of the <see cref="PropCommand"/> class.
		/// </summary>
		public PropCommandPackage()
		{
			// Inside this method you can place any initialization code that does not require
			// any Visual Studio service because at this point the package object is created but
			// not sited yet inside Visual Studio environment. The place to do all the other
			// initialization is the Initialize method.
		}

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
		/// <param name="progress">A provider for progress updates.</param>
		/// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			// When initialized asynchronously, the current thread may be a background thread at this point.
			// Do any initialization that requires the UI thread after switching to the UI thread.
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
			await PropCommand.InitializeAsync(this);
		}

		#endregion
	}
}
