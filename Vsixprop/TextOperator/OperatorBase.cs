using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop.TextOperator
{
	public abstract class OperatorBase
	{
		
		/// <summary>
		/// 执行命令，利用字符串生成属性
		/// </summary>
		/// <returns></returns>
		public abstract string DoPropString(string inputStr);
		public OptionPageCustom config { get; set; }

		public bool AddSummary { get; set; }
		/// <summary>
		/// 单个字段的处理
		/// </summary>
		/// <param name="singleStr"></param>
		/// <param name="dataType"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public  string DoSingleStrOperate(string singleStr, string dataType, string comment = "")
		{
			StringBuilder propStr = new StringBuilder();
			if (string.IsNullOrEmpty(singleStr))
			{
				return string.Empty;
			}
			singleStr = Utils.FirstToUpper(singleStr);
			//var dataType = GetStringDataType(singleStr);
			propStr.Append("\r\n");
			if (config.AddSummary)
			{
				propStr.AppendTab(2).Append("/// <summary>\r\n");
				propStr.AppendTab(2).AppendFormat("///{0}\r\n", comment);
				propStr.AppendTab(2).Append("/// <summary>\r\n");
			}
			propStr.AppendTab(2).AppendFormat("public {0} {1} ", dataType, singleStr);
			propStr.Append("{ get; set; }");
			return propStr.ToString();
		}
	}
}
