using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZumNet.Framework.Base;
using ZumNet.Framework.Data;

namespace ZumNet.DAL.ServiceDac
{
	/// <summary>
	/// 
	/// </summary>
	public class CodeDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public CodeDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public CodeDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 관리되는 코드값을 가져온다.
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="key3"></param>
		/// <returns></returns>
		public ArrayList SelectCodeDescription(string key1, string key2, string key3)
		{
			ArrayList rowList = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@key1", SqlDbType.VarChar, 63, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.VarChar, 63, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.VarChar, 63, key3)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectCodeDescription", parameters);

			using (DbBase db = new DbBase())
			{
				rowList = db.ExecuteListNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return rowList;
		}

		/// <summary>
		/// 관리되는 코드 저장
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="key3"></param>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <param name="item3"></param>
		/// <param name="item4"></param>
		/// <param name="item5"></param>
		public void InsertCodeDescription(string key1, string key2, string key3, string item1, string item2, string item3, string item4, string item5)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@key1", SqlDbType.VarChar, 63, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.VarChar, 63, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.VarChar, 63, key3),
				ParamSet.Add4Sql("@item1", SqlDbType.NVarChar, 200, item1),
				ParamSet.Add4Sql("@item2", SqlDbType.NVarChar, 200, item2),
				ParamSet.Add4Sql("@item3", SqlDbType.NVarChar, 200, item3),
				ParamSet.Add4Sql("@item4", SqlDbType.NVarChar, 200, item4),
				ParamSet.Add4Sql("@item5", SqlDbType.NVarChar, 200, item5)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertCodeDescription", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 관리되는 코드 변경
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="key3"></param>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <param name="item3"></param>
		/// <param name="item4"></param>
		/// <param name="item5"></param>
		public void UpdateCodeDescription(string key1, string key2, string key3, string item1, string item2, string item3, string item4, string item5)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@key1", SqlDbType.VarChar, 63, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.VarChar, 63, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.VarChar, 63, key3),
				ParamSet.Add4Sql("@item1", SqlDbType.NVarChar, 200, item1),
				ParamSet.Add4Sql("@item2", SqlDbType.NVarChar, 200, item2),
				ParamSet.Add4Sql("@item3", SqlDbType.NVarChar, 200, item3),
				ParamSet.Add4Sql("@item4", SqlDbType.NVarChar, 200, item4),
				ParamSet.Add4Sql("@item5", SqlDbType.NVarChar, 200, item5)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateCodeDescription", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 관리되는 코드 삭제
		/// </summary>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="key3"></param>
		public void DeleteCodeDescription(string key1, string key2, string key3)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@key1", SqlDbType.VarChar, 63, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.VarChar, 63, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.VarChar, 63, key3)
			};

			ParamData pData = new ParamData("admin.ph_up_DeleteCodeDescription", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
	}
}
