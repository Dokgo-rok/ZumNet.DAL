using System;
using System.Collections.Generic;
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
    public class AspServiceDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public AspServiceDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public AspServiceDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 등록된 서버와 고객사 서버 정보
		/// </summary>
		/// <returns></returns>
		public DataSet GetServer()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.ph_up_GetServer", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 등록된 서버 종류
		/// </summary>
		/// <returns></returns>
		public DataSet GetServerType()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.ph_up_GetServerType", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 등록된 Option 종류
		/// </summary>
		/// <returns></returns>
		public DataSet GetOptionType()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.ph_up_GetOptionType", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 서버 생성
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="serverinfo"></param>
		public void CreateServer(string actionKind, string serverinfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@cmd", SqlDbType.VarChar, 30, actionKind),
				ParamSet.Add4Sql("@server_info", SqlDbType.NText, serverinfo)
			};

			ParamData pData = new ParamData("admin.ph_up_HDServer", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// Option 생성
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="serverinfo"></param>
		public void CreateOption(string actionKind, string serverinfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@cmd", SqlDbType.VarChar, 30, actionKind),
				ParamSet.Add4Sql("@server_info", SqlDbType.NText, serverinfo)
			};

			ParamData pData = new ParamData("admin.ph_up_HDOptionType", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// Option 생성
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="serverinfo"></param>
		public void HDOption(string actionKind, string serverinfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@cmd", SqlDbType.VarChar, 30, actionKind),
				ParamSet.Add4Sql("@com_info", SqlDbType.NText, serverinfo)
			};

			ParamData pData = new ParamData("admin.ph_up_HDOptionType", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 고객사 코드 중복체크
		/// </summary>	
		/// <param name="companycode">CompanyCode</param>
		/// <returns></returns>
		public string DuplicationChk(string companycode)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@result", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DupCompanyCode", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			
			return strReturn;
		}

		/// <summary>
		/// ASP Server DB Query
		/// </summary>
		/// <param name="companycode"></param>
		/// <returns></returns>
		public DataSet GetReSessionInfo(string companycode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode)
			};

			ParamData pData = new ParamData("admin.ph_up_ReSession", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리툴에서 고객사 리스트 쿼리
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetASPCompanyList(int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn
									, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetServiceCompanyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = Convert.ToInt32(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// XML 형식으로 ASP 고객사 등록, 변경
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="comInfo"></param>
		/// <returns></returns>
		public void SetCompanyInfo(string cmd, string comInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@cmd", SqlDbType.VarChar, 30, cmd),
				ParamSet.Add4Sql("@com_info", SqlDbType.NText, comInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_HDCompany", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companycode"></param>
		/// <returns></returns>
		public DataSet GetASPRegistCount(string companycode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode)
			};

			ParamData pData = new ParamData("admin.ph_up_GetPerLogonCount", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companycode"></param>
		/// <returns></returns>
		public DataSet GetASPCompanyAtt(string companycode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode)
			};

			ParamData pData = new ParamData("admin.ph_up_GetServiceCompanyAtt", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// ASP Server DB Query
		/// </summary>	
		/// <param name="domainID">Domain ID</param>
		/// <returns></returns>
		public DataSet GetServiceServerName(int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domain", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetServiceServerName", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
