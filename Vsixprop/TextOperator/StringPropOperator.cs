using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop.TextOperator
{
	public class StringPropOperator : OperatorBase
	{
		/// <summary>
		/// 字符串方面的处理
		/// </summary>
		/// <param name="inputStr"></param>
		/// <returns></returns>
		public override string DoPropString(string inputStr)
		{
			var strArr = inputStr.Replace(" ", ",").Replace("\r\n", ",").Replace("\t","").Split(',');
			var sb = new StringBuilder();
			foreach (var singleStr in strArr)
			{
				var dataType = GetStringDataType(singleStr);
				sb.Append(DoSingleStrOperate(singleStr,dataType));
			}
			return sb.ToString();

		}

		/// <summary>
		/// 暂时使用
		/// </summary>
		/// <param name="singleStr"></param>
		/// <returns></returns>
		private string GetStringDataType(string singleStr)
		{
			string datatype = "string";
			var str = singleStr.ToLower();
			//time type
			if (str.Contains("date")||str.Contains("time"))
			{
				datatype= "DateTime";
			}
			//caclute type
			if (str.Contains("qty") || str.Contains("number")||str.Contains("total")||str.Contains("price"))
			{
				datatype= "decimal";
			}
			//primerykey type
			if (str.Contains("id") || str.Contains("vhcode") || str.Contains("dlyorder"))
			{
				datatype = "ulong";
			}
			//bool type
			if (str.Contains("is"))
			{
				datatype = "bool";
			}
			return datatype;
		}

	}
}
