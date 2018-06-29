using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop
{
	public class Utils
	{

		
		public static string FirstToUpper(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				return string.Empty;

			char[] s = str.ToCharArray();
			char c = s[0];

			if ('a' <= c && c <= 'z')
				c = (char)(c & ~0x20);

			s[0] = c;

			return new string(s);
		}
	}
}
