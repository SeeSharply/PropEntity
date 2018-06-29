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
		
	}
}
