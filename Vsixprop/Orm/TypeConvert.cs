using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Vsixprop
{
	public class TypeConvert
	{
		public static Dictionary<string, string> list = new Dictionary<string, string>() {
		};
		public static readonly TypeConvert Instance = new TypeConvert();
		public TypeConvert()
		{
			list.Add("bigint", "ulong");
			list.Add("binary", "Object");
			list.Add("bit", "bool");
			list.Add("char", "DateTime");
			list.Add("decimal", "decimal");
			list.Add("float", "double");
			list.Add("image", "byte[]");
			list.Add("int", "int");
			list.Add("numberic", "decimal");
			list.Add("real", "float");
			list.Add("date", "DateTime");
			list.Add("smalldatetime", "DateTime");
			list.Add("smallint", "short");
			list.Add("smallmoney", "short");
			list.Add("timestamp", "byte[]");
			list.Add("tinyint", "bool");
			list.Add("varbinary", "byte[]");
			list.Add("sql_variant", "object");
		}
		public  Dictionary<string, string> GetList()
		{
			return list;
		}

	}
}
