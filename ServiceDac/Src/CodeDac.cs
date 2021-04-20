using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZumNet.DAL.Base;

namespace ZumNet.DAL.ServiceDac
{
	public class CodeDac : DacBase
	{
		/// <summary>
		/// 관리되는 코드값을 가져온다.
		/// </summary>
		public DataSet SelectCodeDescription(string key1, string key2, string key3)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@key1", SqlDbType.VarChar, 63, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.VarChar, 63, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.VarChar, 63, key3)
			};

			ParamData pData = new ParamData("admin.ph_up_DomainSearchUsers", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리되는 코드 저장
		/// </summary>
		public int InsertCodeDescription(string key1, string key2, string key3, string item1, string item2, string item3, string item4, string item5)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 관리되는 코드 변경
		/// </summary>
		public int UpdateCodeDescription(string key1, string key2, string key3, string item1, string item2, string item3, string item4, string item5)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 관리되는 코드 삭제
		/// </summary>
		public int DeleteCodeDescription(string key1, string key2, string key3)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@key1", SqlDbType.VarChar, 63, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.VarChar, 63, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.VarChar, 63, key3)
			};

			ParamData pData = new ParamData("admin.ph_up_DeleteCodeDescription", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}
	}
}
