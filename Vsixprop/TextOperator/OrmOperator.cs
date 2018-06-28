using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsixprop;

namespace Vsixprop.TextOperator
{
	public class OrmOperator : OperatorBase
	{
		public string ConnectStr{ get; set; }
		public int DbType { get; set; }
		/// <summary>
		/// 数据库读取处理
		/// </summary>
		/// <param name="inputStr"></param>
		/// <returns></returns>
		public override string DoPropString(string inputStr)
		{
			var sb = new StringBuilder();
			var db = new DBConnect(ConnectStr,DbType);
			var str=db.GetTableColumnStr2(inputStr);
			return str.ToString();

		}
	}
}
