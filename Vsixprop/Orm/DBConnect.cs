using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vsixprop
{
	class DBConnect
	{
		private DbConnection dbConnection;

		private DbCommand dbCommand;

		public DbDataReader reader;
		private string ConnectString { get; set; }

		private int dbType { get; set; }
		public DBConnect(string connectStr, int type)
		{
			this.ConnectString = connectStr;
			this.dbType = type;
		}


		//打开数据库连接  
		public void OpenDB()
		{
			try
			{
				switch (dbType)
				{
					case 0:
						//dbConnection = new MySql.Data.MySqlClient.MySqlConnection("Server=172.16.0.243;User Id=website;Password=test@2011;Database=retail;OldSyntax=true;port=4306;Allow User Variables=True");
						dbConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectString);
						break;
					case 1:
						throw new Exception("暂时不支持Sqlite");
				}
				
				dbConnection.Open();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		//关闭数据库连接  
		public void CloseSqlConnection()
		{
			if (dbCommand != null)
			{
				dbCommand.Dispose();
			}
			dbCommand = null;
			if (reader != null)
			{
				reader.Dispose();
			}
			reader = null;
			if (dbConnection != null && dbConnection.State == ConnectionState.Open)
			{
				dbConnection.Close();
				dbConnection.Dispose();
			}
			dbConnection = null;
		}

		//执行Sql命令  
		public int ExecuteQuery(string sql)
		{
			OpenDB();
			dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = sql;
			reader = dbCommand.ExecuteReader();
			return reader.RecordsAffected;
		}
		/// <summary>
		/// 通过info数据库直接获取表信息来构建实体
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public string GetTableColumnStr2(string tableName)
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
				Debug.WriteLine("生成实体出现异常：" + e.Message + "\r\n" + e.StackTrace);
				return string.Empty;
			}
			finally
			{
				CloseSqlConnection();
			}
		}
		/// <summary>
		/// 直接通过查询表来生成数据实体，无法获取字段备注comment信息
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public string  GetTableColumnStr(string tableName)
		{
			try
			{
				ExecuteQuery(string.Format("select column_name,column_comment from information_schema.columns where table_schema ='retail'  and table_name = '{0}'; ", tableName));
				var sb = new StringBuilder();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					var colType = reader.GetDataTypeName(i);
					var colName = reader.GetName(i);
					//
					var a = TypeConvert.Instance;
					var typeString = "string";
					if (a.GetList().ContainsKey(colType.ToLower()))
					{
						typeString = a.GetList()[colType.ToLower()];
					}
					var singleStr = Utils.DoSingleStrOperate(Utils.FirstToUpper(colName), typeString);
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
