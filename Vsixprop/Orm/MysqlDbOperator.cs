using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vsixprop.Orm
{
	public class MysqlDbOperator:DbOperatorBase
	{
		public MysqlDbOperator(string connStr) : base(connStr)
		{

		}
		public override void OpenDB()
		{
			try
			{
				
				dbConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectString);
				dbConnection.Open();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// mysql获取数据库字段方法
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public override string GetTableColumnStr(string tableName)
		{
			try
			{
				string s = ConnectString.ToLower(); ;
				string dataBase = Regex.Match(s, @"database=([^;]+)").Groups[1].Value;

				ExecuteQuery(string.Format("select column_name,column_comment,data_type from information_schema.columns where table_schema ='{1}'  and table_name = '{0}'; ", tableName, dataBase));
				var sb = new StringBuilder();
				while (reader.Read())
				{
					var colName = reader.GetString(0);
					var comment = reader.GetString(1);
					var dataType = reader.GetString(2);
					var a = TypeConvert.Instance;
					var typeString = "string";
					if (a.GetList().ContainsKey(dataType.ToLower()))
					{
						typeString = a.GetList()[dataType.ToLower()];
					}
					var singleStr = Utils.DoSingleStrOperate(Utils.FirstToUpper(colName), typeString, comment);
					sb.Append(singleStr);
				}
				return sb.ToString();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				CloseSqlConnection();
			}
		}
	}
}
