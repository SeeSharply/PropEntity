using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vsixprop.Orm
{
	public class SqliteDbOperator : DbOperatorBase
	{
		public SqliteDbOperator(OptionPageCustom c) : base(c)
		{

		}
		public override void OpenDB()
		{
			try
			{
				
				dbConnection = new SQLiteConnection(config.connectString);
				dbConnection.Open();
			}
			catch (Exception e)
			{
				throw e;
				
			}
		}
		/// <summary>
		/// sqlite获取数据库字段方法
		/// 这个其实是通用的
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public override string GetTableColumnStr(string tableName)
		{
			try
			{
				var sb = new StringBuilder();
				ExecuteQuery(string.Format("select * from {0}  limit 1", tableName));

				var table = reader.GetSchemaTable();
				if (table!=null)
				{
					var typeList = TypeConvert.Instance.GetList();
					var typeString = "string";
					foreach (DataRow row in table.Rows)
					{
						var colType = row["DataType"].ToString().ToLower();
						var colName = row["ColumnName"].ToString();
						if (typeList.ContainsKey(colType))
						{
							typeString = typeList[colType.ToLower()];
						}
						var singleStr = Utils.DoSingleStrOperate(Utils.FirstToUpper(colName), typeString,config.AddSummary);
						sb.Append(singleStr);
					}
				}
				//以下为类似实现方式，奈何sqlite没有备注一栏，没法获取来生成
				//for (int i = 0; i < reader.FieldCount; i++)
				//{
				//	var colType = reader.GetDataTypeName(i);
				//	var colName = reader.GetName(i);
				//	//
				//	var a = TypeConvert.Instance;
				//	var typeString = "string";
				//	if (a.GetList().ContainsKey(colType.ToLower()))
				//	{
				//		typeString = a.GetList()[colType.ToLower()];
				//	}
				//	var singleStr = Utils.DoSingleStrOperate(Utils.FirstToUpper(colName), typeString);
				//	sb.Append(singleStr);
				//}
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

		private DataTable  GetReaderSchema(string tableName)
		{
			string sql = string.Format(@"select * from [{0}]",tableName);
			ExecuteQuery(sql);
			return reader.GetSchemaTable();
		}
	}
}
