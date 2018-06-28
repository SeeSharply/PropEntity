using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop
{
	public class ServiceHelper
	{
		public static IVsTextView GetIvsTextView(IServiceProvider provider)
		{
			IVsMonitorSelection selection = provider.GetService(typeof(IVsMonitorSelection)) as IVsMonitorSelection;
			object frameObj = null;

			ErrorHandler.ThrowOnFailure(selection.GetCurrentElementValue((uint)VSConstants.VSSELELEMID.SEID_DocumentFrame, out frameObj));

			IVsWindowFrame frame = frameObj as IVsWindowFrame;
			if (frame == null)
			{ }

			object pvar;
			ErrorHandler.ThrowOnFailure(frame.GetProperty((int)__VSFPROPID.VSFPROPID_DocView,
														   out pvar));

			IVsTextView textView = pvar as IVsTextView;
			if (textView == null)
			{
				IVsCodeWindow codeWin = pvar as IVsCodeWindow;
				if (codeWin != null)
				{
					ErrorHandler.ThrowOnFailure(codeWin.GetLastActiveView(out textView));
				}
			}
			return textView;
		}

		public static IWpfTextView GetWpfTextView(IServiceProvider provider)
		{
			var textView = GetIvsTextView(provider);
			IVsUserData userData = textView as IVsUserData;
			if (userData == null)
			{ }
			object objTextViewHost;
			if (VSConstants.S_OK
					!= userData.GetData(Microsoft.VisualStudio.Editor.DefGuidList
																	 .guidIWpfTextViewHost,
										 out objTextViewHost))
			{

			}

			IWpfTextViewHost textViewHost = objTextViewHost as IWpfTextViewHost;
			if (textViewHost == null)
			{ }

			IWpfTextView text_View = textViewHost.TextView;
			return text_View;
		}
		
	}
}
