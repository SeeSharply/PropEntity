using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop.Orm
{
	public abstract class DbOperatorBase
	{
		protected DbConnection dbConnection;

		protected DbCommand dbCommand;

		public DbDataReader reader;
		public string ConnectString { get; set; }
		public DbOperatorBase(string connStr)
		{
			ConnectString = connStr;
		}

		//打开数据库连接  
		public virtual void OpenDB()
		{
			
		}
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

		public virtual string GetTableColumnStr(string tableName) {
			return "";
		}
	}
}
