using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsixprop;
using Vsixprop.Orm;

namespace Vsixprop.TextOperator
{
	public class OrmOperator : OperatorBase
	{
		private DbOperatorBase db { get; set; }

		//打开数据库连接  
		public void SetDBType()
		{
			try
			{
				switch (config.DBType)
				{
					case 0:
						db = new MysqlDbOperator(config);
						break;
					case 1:
						db = new SqliteDbOperator(config);
						break;
				}

			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 数据库读取处理
		/// </summary>
		/// <param name="inputStr"></param>
		/// <returns></returns>
		public override string DoPropString(string inputStr)
		{
			SetDBType();
			var str=db.GetTableColumnStr(inputStr);
			return str.ToString();

		}

	}
}
