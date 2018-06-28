using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop
{
	[ContentType("text")]
	[Export(typeof(IWpfTextViewCreationListener))]
	[TextViewRole(PredefinedTextViewRoles.Editable)]
	internal sealed class TextViewCreationListener : IWpfTextViewCreationListener
	{
		public static IWpfTextView _view;

		public void TextViewCreated(IWpfTextView textView)
		{
			if (textView !=null)
			{
				_view = textView;

			}
		}
	}
}
