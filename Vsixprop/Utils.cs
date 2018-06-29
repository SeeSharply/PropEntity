using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop
{
	public class Utils
	{

		/// <summary>
		/// 单个字段的处理
		/// </summary>
		/// <param name="singleStr"></param>
		/// <param name="dataType"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public static string DoSingleStrOperate(string singleStr, string dataType, bool addSummary, string comment = "")
		{
			StringBuilder propStr = new StringBuilder();
			if (string.IsNullOrEmpty(singleStr))
			{
				return string.Empty;
			}
			singleStr = Utils.FirstToUpper(singleStr);
			//var dataType = GetStringDataType(singleStr);
			propStr.Append("\r\n");
			if (addSummary)
			{
				propStr.AppendTab(2).Append("/// <summary>\r\n");
				propStr.AppendTab(2).AppendFormat("///{0}\r\n", comment);
				propStr.AppendTab(2).Append("/// <summary>\r\n");
			}
			propStr.AppendTab(2).AppendFormat("public {0} {1} ", dataType, singleStr);
			propStr.Append("{ get; set; }");
			return propStr.ToString();
		}
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
