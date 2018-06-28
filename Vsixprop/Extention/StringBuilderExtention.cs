using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop
{
	public static class StringBuilderExtention
	{
		public static StringBuilder AppendTab(this StringBuilder builder,int number=1)
		{
			for (int i = 0; i < number; i++)
			{
				builder.Append("\t");
			}
			return builder;
		}
	}
}
