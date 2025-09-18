using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ZumNet.Framework.Base;
using ZumNet.Framework.Data;

namespace ZumNet.DAL.InterfaceDac
{
    /// <summary>
	/// 연동 처리
	/// </summary>
	public class InterfaceDac : DacBase
    {
        #region [생성자]
        /// <summary>
        /// 
        /// </summary>
        public InterfaceDac(string connectionString = "") : base(connectionString)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public InterfaceDac(SqlConnection connection) : base(connection)
		{
		}
        #endregion

        #region [연동 대상 데이타 저장, 조회 및 모니터링]
        /// <summary>
        /// 외부시스템과 연동하기 위한 데이타 쿼리
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="requestState"></param>
        /// <param name="companyCode"></param>
        /// <param name="stampID"></param>
        /// <returns></returns>
        public DataTable GetIFSendSync(string dbName, int requestState, string companyCode, string stampID)
		{
			DataSet ds = null;
			DataTable dtReturn = null;

			if (dbName != "") dbName += ".";
			string strSP = dbName + "admin.ph_up_GetIFSendSync";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@requeststate", SqlDbType.Int, 4, requestState),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@stampid", SqlDbType.VarChar, 33, stampID)
			};

			ParamData pData = new ParamData(strSP, "", "IFSendSync", 30, parameters);

			try
			{
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables[0];
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (ds != null) ds.Dispose();
			}

			return dtReturn;
		}

		/// <summary>
		/// 외부시스템과 연동하기 위한 데이타 쿼리
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="interfaceID"></param>
		/// <returns></returns>
		public DataTable GetIFSendSync(string dbName, long interfaceID)
		{
			DataSet ds = null;
			DataTable dtReturn = null;

			if (dbName != "") dbName += ".";
			string strSP = dbName + "admin.ph_up_GetIFSendSync";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, 8, interfaceID)
			};

			ParamData pData = new ParamData(strSP, "", "IFSendSync", 30, parameters);

			try
			{
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables[0];
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (ds != null) ds.Dispose();
			}

			return dtReturn;
		}

		/// <summary>
		/// 외부시스템과 연동하기 위한 데이타 저장
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="companyCode"></param>
		/// <param name="stampID"></param>
		/// <param name="moduleID"></param>
		/// <param name="requestDesc"></param>
		/// <param name="requestDate"></param>
		/// <param name="procInstID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="formID"></param>
		/// <param name="workitemID"></param>
		/// <param name="workitemStatus"></param>
		/// <param name="col1"></param>
		/// <param name="col2"></param>
		/// <param name="col3"></param>
		/// <param name="col4"></param>
		/// <param name="col5"></param>
		/// <param name="col6"></param>
		/// <param name="col7"></param>
		/// <param name="col8"></param>
		/// <param name="col9"></param>
		/// <param name="col10"></param>
		public void InsertIFSendSync(string dbName, string companyCode, string stampID, string moduleID, string requestDesc, string requestDate
								, string procInstID, string xfAlias, string messageID, string formID, string workitemID, int workitemStatus
								, string col1, string col2, string col3, string col4, string col5, string col6, string col7, string col8, string col9, string col10)
		{

			if (dbName != "") dbName += ".";
			string strSP = dbName + "admin.ph_up_InsertIFSendSync";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@stampid", SqlDbType.VarChar, 33, stampID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@reqeustdesc", SqlDbType.NVarChar, 200, requestDesc),
				ParamSet.Add4Sql("@requestdate", SqlDbType.VarChar, 20, requestDate),
				ParamSet.Add4Sql("@procinstid", SqlDbType.VarChar, 33, procInstID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@workitemid", SqlDbType.VarChar, 33, workitemID),
				ParamSet.Add4Sql("@workitemstatus", SqlDbType.Int, 4, workitemStatus),
				ParamSet.Add4Sql("@col1", SqlDbType.NVarChar, 500, col1),
				ParamSet.Add4Sql("@col2", SqlDbType.NVarChar, 500, col2),
				ParamSet.Add4Sql("@col3", SqlDbType.NVarChar, 500, col3),
				ParamSet.Add4Sql("@col4", SqlDbType.NVarChar, 500, col4),
				ParamSet.Add4Sql("@col5", SqlDbType.NVarChar, 500, col5),
				ParamSet.Add4Sql("@col6", SqlDbType.NVarChar, 500, col6),
				ParamSet.Add4Sql("@col7", SqlDbType.NVarChar, 500, col7),
				ParamSet.Add4Sql("@col8", SqlDbType.NVarChar, 500, col8),
				ParamSet.Add4Sql("@col9", SqlDbType.NVarChar, 500, col9),
				ParamSet.Add4Sql("@col10", SqlDbType.NVarChar, 500, col10)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 외부시스템과 연동하기 위한 데이타 처리 후 상태값 갱신
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="ifID"></param>
		/// <param name="requestState"></param>
		/// <returns></returns>
		public void ChangeIFSendSyncState(string dbName, long ifID, int requestState)
		{
			if (dbName != "") dbName += ".";
			string strSP = dbName + "admin.ph_up_ChangeIFSendSyncState";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, 8, ifID),
				ParamSet.Add4Sql("@requeststate", SqlDbType.Int, 4, requestState)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 연동 결과 모니터링을 위한 데이타 가져오기
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="mode"></param>
		/// <param name="ekpDB"></param>
		/// <param name="companyCode"></param>
		/// <param name="module"></param>
		/// <param name="fromMonth"></param>
		/// <param name="toMonth"></param>
		/// <param name="fromDay"></param>
		/// <param name="toDay"></param>
		/// <returns></returns>
		public DataSet GetMonitorIFSendSync(string dbName, string mode, string ekpDB, string companyCode
								, string module, string fromMonth, string toMonth, string fromDay, string toDay)
		{
			DataSet dsReturn = null;
			if (dbName != "") dbName += ".";
			string strSP = dbName + "admin.ph_up_GetMonitorIFSendSync";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@module", SqlDbType.VarChar, 50, module),
				ParamSet.Add4Sql("@from", SqlDbType.VarChar, 10, fromMonth),
				ParamSet.Add4Sql("@to", SqlDbType.VarChar, 10, toMonth),
				ParamSet.Add4Sql("@fromday", SqlDbType.Char, 2, fromDay),
				ParamSet.Add4Sql("@today", SqlDbType.Char, 2, toDay)
			};

			ParamData pData = new ParamData(strSP, "", "IFSendSync", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
		#endregion

		#region [집계 및 외부연동 데이터 가져오기]
		/// <summary>
		/// 집계
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="targetId"></param>
		/// <param name="formTable"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cond1"></param>
		/// <param name="cond2"></param>
		/// <param name="cond3"></param>
		/// <param name="cond4"></param>
		/// <param name="cond5"></param>
		/// <returns></returns>
		public DataSet GetReport(string mode, int targetId, string formTable, string from, string to
							, string cond1, string cond2, string cond3, string cond4, string cond5)
		{
			DataSet dsReturn = null;
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetReport";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
				ParamSet.Add4Sql("@from", SqlDbType.VarChar, 10, from),
				ParamSet.Add4Sql("@to", SqlDbType.VarChar, 10, to),
				ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 50, cond1),
				ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 50, cond2),
				ParamSet.Add4Sql("@cond3", SqlDbType.NVarChar, 50, cond3),
				ParamSet.Add4Sql("@cond4", SqlDbType.NVarChar, 50, cond4),
				ParamSet.Add4Sql("@cond5", SqlDbType.NVarChar, -1, cond5)
			};

			ParamData pData = new ParamData(strSP, "", "ReportSheet", 60, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 외부연동용 양식 정보 및 ERP 정보 가져오기
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="targetId"></param>
		/// <param name="formTable"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cond1"></param>
		/// <param name="cond2"></param>
		/// <param name="cond3"></param>
		/// <param name="cond4"></param>
		/// <param name="cond5"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="baseSort"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="search"></param>
		/// <returns></returns>
		public DataSet GetReport(string mode, int targetId, string formTable, string from, string to
							, string cond1, string cond2, string cond3, string cond4, string cond5
							, int page, int pageSize, string baseSort, string sortCol, string sortType, string search)
		{
			DataSet dsReturn = null;
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetReport";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
				ParamSet.Add4Sql("@from", SqlDbType.VarChar, 10, from),
				ParamSet.Add4Sql("@to", SqlDbType.VarChar, 10, to),
				ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 50, cond1),
				ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 50, cond2),
				ParamSet.Add4Sql("@cond3", SqlDbType.NVarChar, 50, cond3),
				ParamSet.Add4Sql("@cond4", SqlDbType.NVarChar, 50, cond4),
				ParamSet.Add4Sql("@cond5", SqlDbType.NVarChar, -1, cond5),
				ParamSet.Add4Sql("@page", SqlDbType.Int, page),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, pageSize),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.NVarChar, 100, baseSort),
				ParamSet.Add4Sql("@sort_col", SqlDbType.NVarChar, 100, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.NVarChar, 5, sortType),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 1000, search),

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", "ReportSheet", 60, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// ERP 정보 가져오기
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="targetId"></param>
		/// <param name="formTable"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cond1"></param>
		/// <param name="cond2"></param>
		/// <param name="cond3"></param>
		/// <param name="cond4"></param>
		/// <param name="cond5"></param>
		/// <returns></returns>
		public DataSet GetReportERP(string mode, int targetId, string formTable, string from, string to
							, string cond1, string cond2, string cond3, string cond4, string cond5)
		{
			DataSet dsReturn = null;
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetReporteERP";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
				ParamSet.Add4Sql("@from", SqlDbType.VarChar, 10, from),
				ParamSet.Add4Sql("@to", SqlDbType.VarChar, 10, to),
				ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 50, cond1),
				ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 50, cond2),
				ParamSet.Add4Sql("@cond3", SqlDbType.NVarChar, 50, cond3),
				ParamSet.Add4Sql("@cond4", SqlDbType.NVarChar, 50, cond4),
				ParamSet.Add4Sql("@cond5", SqlDbType.NVarChar, -1, cond5)
			};

			ParamData pData = new ParamData(strSP, "", "ReportSheet", 60, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 외부연동을 위한 사전 데이타 가져오기
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="curWIId"></param>
		/// <param name="curPartId"></param>
		/// <param name="status"></param>
		/// <param name="col1"></param>
		/// <param name="col2"></param>
		/// <returns></returns>
		public DataSet GetIFInfo(int dnID, string moduleID, string companyCode, string ifDB, string ekpDB, string formDB, string formTable, int version
								, string xfAlias, string formID, string msgID, string oID, string curWIId, string curPartId, int status, string col1, string col2)
		{
			DataSet dsReturn = null;
			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_GetIFInfo";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, curWIId),
				ParamSet.Add4Sql("@curpartid", SqlDbType.VarChar, 33, curPartId),
				ParamSet.Add4Sql("@status", SqlDbType.Int, status),
				ParamSet.Add4Sql("@col1", SqlDbType.NVarChar, 200, col1),
				ParamSet.Add4Sql("@col2", SqlDbType.NVarChar, 200, col2)
			};

			ParamData pData = new ParamData(strSP, "", "IFInfo", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
        #endregion

        #region [비동기 ERP 연동 및 대장 등록]
        /// <summary>
        /// ERP 연동 호출
        /// </summary>
        /// <param name="ifId"></param>
        /// <param name="dnID"></param>
        /// <param name="moduleID"></param>
        /// <param name="companyCode"></param>
        /// <param name="ifDB"></param>
        /// <param name="ekpDB"></param>
        /// <param name="formDB"></param>
        /// <param name="formTable"></param>
        /// <param name="xfAlias"></param>
        /// <param name="formID"></param>
        /// <param name="msgID"></param>
        /// <param name="oID"></param>
        public void InvokeErpProcedure(string ifId, int dnID, string moduleID, string companyCode, string ifDB, string ekpDB
									, string formDB, string formTable, string xfAlias, string formID, string msgID, string oID)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFERP";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 대장 등록
		/// </summary>
		/// <param name="ifId"></param>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="nextStep"></param>
		/// <param name="dateInfo"></param>
		/// <param name="requesterInfo"></param>
		/// <param name="colInfo1"></param>
		/// <param name="colInfo2"></param>
		/// <param name="colInfo3"></param>
		/// <param name="colInfo4"></param>
		/// <param name="colInfo5"></param>
		/// <param name="colInfo6"></param>
		/// <param name="colInfo7"></param>
		public void InvokeRegisterProcedure(string ifId, int dnID, string moduleID, string companyCode, string ifDB
									, string ekpDB, string formDB, string formTable, int version, string xfAlias, string formID
									, string msgID, string oID, string nextStep, string dateInfo, string requesterInfo
									, string colInfo1, string colInfo2, string colInfo3, string colInfo4, string colInfo5, string colInfo6, string colInfo7)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFRegister";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@nextstep", SqlDbType.VarChar, 20, nextStep),
				ParamSet.Add4Sql("@dateinfo", SqlDbType.VarChar, 100, dateInfo),
				ParamSet.Add4Sql("@requesterinfo", SqlDbType.NVarChar, 500, requesterInfo),
				ParamSet.Add4Sql("@colinfo1", SqlDbType.NVarChar, 500, colInfo1),
				ParamSet.Add4Sql("@colinfo2", SqlDbType.NVarChar, 500, colInfo2),
				ParamSet.Add4Sql("@colinfo3", SqlDbType.NVarChar, 500, colInfo3),
				ParamSet.Add4Sql("@colinfo4", SqlDbType.NVarChar, 500, colInfo4),
				ParamSet.Add4Sql("@colinfo5", SqlDbType.NVarChar, 500, colInfo5),
				ParamSet.Add4Sql("@colinfo6", SqlDbType.NVarChar, 500, colInfo6),
				ParamSet.Add4Sql("@colinfo7", SqlDbType.NVarChar, 500, colInfo7)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [동기식 ERP 연동 및 대장 등록 - 동일 트랜잭션]
		/// <summary>
		/// 동기식 ERP 연동 처리
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="curPartID"></param>
		/// <param name="externalKey1"></param>
		/// <param name="externalKey2"></param>
		/// <param name="col1"></param>
		/// <param name="col2"></param>
		/// <param name="col3"></param>
		/// <param name="col4"></param>
		/// <param name="col5"></param>
		public void SetIFServiceERP(int dnID, string moduleID, string companyCode, string ifDB, string ekpDB, string formDB, string formTable
								, int version, string xfAlias, string formID, string msgID, string oID, string curPartID, string externalKey1
								, string externalKey2, string col1, string col2, string col3, string col4, string col5)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFServiceERP";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@curpartid", SqlDbType.VarChar, 33, curPartID),
				ParamSet.Add4Sql("@externalkey1", SqlDbType.NVarChar, 50, externalKey1),
				ParamSet.Add4Sql("@externalkey2", SqlDbType.NVarChar, 50, externalKey2),
				ParamSet.Add4Sql("@col1", SqlDbType.NVarChar, 100, col1),
				ParamSet.Add4Sql("@col2", SqlDbType.NVarChar, 100, col2),
				ParamSet.Add4Sql("@col3", SqlDbType.NVarChar, 100, col3),
				ParamSet.Add4Sql("@col4", SqlDbType.NVarChar, 100, col4),
				ParamSet.Add4Sql("@col5", SqlDbType.NVarChar, -1, col5)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 동기식 대장 등록
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="state"></param>
		/// <param name="nextStep"></param>
		/// <param name="dateInfo"></param>
		/// <param name="requesterInfo"></param>
		/// <param name="colInfo1"></param>
		/// <param name="colInfo2"></param>
		/// <param name="colInfo3"></param>
		/// <param name="colInfo4"></param>
		/// <param name="colInfo5"></param>
		/// <param name="colInfo6"></param>
		/// <param name="colInfo7"></param>
		public void SetIFServiceRegister(int dnID, string moduleID, string companyCode, string ifDB, string ekpDB, string formDB, string formTable
								, int version, string xfAlias, string formID, string msgID, string oID, int state, string nextStep, string dateInfo, string requesterInfo
								, string colInfo1, string colInfo2, string colInfo3, string colInfo4, string colInfo5, string colInfo6, string colInfo7)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFServiceRegister";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, state),
				ParamSet.Add4Sql("@nextstep", SqlDbType.VarChar, 20, nextStep),
				ParamSet.Add4Sql("@dateinfo", SqlDbType.VarChar, 100, dateInfo),
				ParamSet.Add4Sql("@requesterinfo", SqlDbType.NVarChar, 500, requesterInfo),
				ParamSet.Add4Sql("@colinfo1", SqlDbType.NVarChar, 500, colInfo1),
				ParamSet.Add4Sql("@colinfo2", SqlDbType.NVarChar, 500, colInfo2),
				ParamSet.Add4Sql("@colinfo3", SqlDbType.NVarChar, 500, colInfo3),
				ParamSet.Add4Sql("@colinfo4", SqlDbType.NVarChar, 500, colInfo4),
				ParamSet.Add4Sql("@colinfo5", SqlDbType.NVarChar, 500, colInfo5),
				ParamSet.Add4Sql("@colinfo6", SqlDbType.NVarChar, 500, colInfo6),
				ParamSet.Add4Sql("@colinfo7", SqlDbType.NVarChar, 500, colInfo7)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [문서이관]
		/// <summary>
		/// 문서이관 작업을 수행
		/// </summary>
		/// <param name="ifId"></param>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="transferInfo"></param>
		/// <param name="fileInfo"></param>
		/// <param name="signLine"></param>
		public void InvokeTransferProcedure(string ifId, int dnID, string moduleID, string companyCode, string ifDB
									, string ekpDB, string formDB, string formTable, string xfAlias, string formID
									, string msgID, string oID, string transferInfo, string fileInfo, string signLine)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFTransfer";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@trasferinfo", SqlDbType.NVarChar, 2000, transferInfo),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NVarChar, -1, fileInfo),
				ParamSet.Add4Sql("@signinfo", SqlDbType.NVarChar, -1, signLine)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 문서이관:신규문서관리
		/// </summary>
		/// <param name="ifId"></param>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="transferInfo"></param>
		/// <param name="fileInfo"></param>
		/// <param name="signLine"></param>
		/// <param name="bodyInfo"></param>
		public void InvokeTransferProcedure(string ifId, int dnID, string moduleID, string companyCode, string ifDB
									, string ekpDB, string formDB, string formTable, string xfAlias, string formID
									, string msgID, string oID, string transferInfo, string fileInfo, string signLine, string bodyInfo)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFTransferRule";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@trasferinfo", SqlDbType.NVarChar, 2000, transferInfo),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NVarChar, -1, fileInfo),
				ParamSet.Add4Sql("@signinfo", SqlDbType.NVarChar, -1, signLine),
				ParamSet.Add4Sql("@bodyinfo", SqlDbType.NVarChar, -1, bodyInfo)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [게시물 등록]
		/// <summary>
		/// 게시물 등록
		/// </summary>
		/// <param name="ifId"></param>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="folderId"></param>
		/// <param name="tgtXFAlias"></param>
		/// <param name="creator"></param>
		/// <param name="creatorId"></param>
		/// <param name="creatorDeptId"></param>
		/// <param name="creatorDept"></param>
		/// <param name="publishDate"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="fileInfo"></param>
		public void InvokeTransferPubProcedure(string ifId, int dnID, string moduleID, string companyCode, string ifDB
									, string ekpDB, string formDB, string formTable, int version, string xfAlias, string formID
									, string msgID, string oID, int folderId, string tgtXFAlias, string creator, int creatorId
									, int creatorDeptId, string creatorDept, string publishDate, string subject, string body, string fileInfo)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFTransferPub";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, folderId),
				ParamSet.Add4Sql("@tgtxfalias", SqlDbType.VarChar, 30, tgtXFAlias),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, creatorId),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, creatorDeptId),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@body", SqlDbType.NVarChar, -1, body),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NVarChar, -1, fileInfo)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [문서배포, 메일 알림 및 메신저 알림 등록]
		/// <summary>
		/// 문서배포 및 메일알림
		/// </summary>
		/// <param name="ifId"></param>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="ifDB"></param>
		/// <param name="ekpDB"></param>
		/// <param name="formDB"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="requesterInfo"></param>
		/// <param name="colInfo1"></param>
		/// <param name="colInfo2"></param>
		public void InvokeMailCabinet(string ifId, int dnID, string moduleID, string companyCode, string ifDB, string ekpDB, string formDB
						, string formTable, int version, string xfAlias, string formID, string msgID, string oID, string requesterInfo, string colInfo1, string colInfo2)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFMailCabinet";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@requesterinfo", SqlDbType.NVarChar, 500, requesterInfo),
				ParamSet.Add4Sql("@colinfo1", SqlDbType.NVarChar, 500, colInfo1),
				ParamSet.Add4Sql("@colinfo2", SqlDbType.NVarChar, 500, colInfo2)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 메신저 알림 등록
		/// </summary>
		/// <param name="ifDB"></param>
		/// <param name="ifId"></param>
		/// <param name="dnID"></param>
		/// <param name="moduleID"></param>
		/// <param name="companyCode"></param>
		/// <param name="formTable"></param>
		/// <param name="version"></param>
		/// <param name="xfAlias"></param>
		/// <param name="formID"></param>
		/// <param name="msgID"></param>
		/// <param name="oID"></param>
		/// <param name="sender"></param>
		/// <param name="receiveIds"></param>
		/// <param name="subject"></param>
		/// <param name="content"></param>
		/// <param name="url"></param>
		public void InvokeMessengerAlarm(string ifDB, string ifId, int dnID, string moduleID, string companyCode
									, string formTable, int version, string xfAlias, string formID, string msgID, string oID
									, string sender, string receiveIds, string subject, string content, string url)
		{

			if (ifDB != "") ifDB += ".";
			string strSP = ifDB + "admin.ph_up_SetIFMessengerAlarm";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ifid", SqlDbType.BigInt, Convert.ToInt64(ifId)),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, dnID),
				ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 50, companyCode),
				//ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, ekpDB),
				//ParamSet.Add4Sql("@formdb", SqlDbType.VarChar, 100, formDB),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@version", SqlDbType.Int, version),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, msgID),
				ParamSet.Add4Sql("@oid", SqlDbType.VarChar, 33, oID),
				ParamSet.Add4Sql("@sender", SqlDbType.NVarChar, 200, sender),
				ParamSet.Add4Sql("@recvid", SqlDbType.VarChar, 4000, receiveIds),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@content", SqlDbType.NVarChar, 4000, content),
				ParamSet.Add4Sql("@url", SqlDbType.VarChar, 500, url)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [집계 항목 변경 처리]
		/// <summary>
		/// 출장현황 일자 변경
		/// </summary>
		/// <param name="msgId"></param>
		/// <param name="step"></param>
		/// <param name="urId"></param>
		/// <param name="deptId"></param>
		/// <param name="urName"></param>
		/// <param name="deptName"></param>
		/// <param name="tripFrom"></param>
		/// <param name="tripTo"></param>
		/// <param name="reason"></param>
		public void InsertRegisterBIZTRIP(int msgId, int step, int urId, int deptId, string urName, string deptName, string tripFrom, string tripTo, string reason)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_InsertRegisterBIZTRIP";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgId),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@registrantid", SqlDbType.Int, 4, urId),
				ParamSet.Add4Sql("@registrantdeptid", SqlDbType.Int, 4, deptId),
				ParamSet.Add4Sql("@registrant", SqlDbType.NVarChar, 100, urName),
				ParamSet.Add4Sql("@registrantdept", SqlDbType.NVarChar, 100, deptName),
				ParamSet.Add4Sql("@tripfrom", SqlDbType.VarChar, 10, tripFrom),
				ParamSet.Add4Sql("@tripto", SqlDbType.VarChar, 10, tripTo),
				ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, 200, reason)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 출장현황 일자 변경 정보 가져오기
		/// </summary>
		/// <param name="msgId"></param>
		/// <param name="step"></param>
		/// <returns></returns>
		public DataSet SelectRegisterBIZTRIP(int msgId, int step)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SelectRegisterBIZTRIP";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgId),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step)
			};

			ParamData pData = new ParamData(strSP, "", "RegisterBIZTRIP", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 집계 register_ 테이블 변경되는 필드 정보를 가져오기
		/// </summary>
		/// <param name="formTable"></param>
		/// <param name="msgId"></param>
		/// <param name="regId"></param>
		/// <returns></returns>
		public DataSet SelectRegisterChangeData(string formTable, int msgId, int regId)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SelectRegisterChangeData";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgId),
				ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId)
			};

			ParamData pData = new ParamData(strSP, "", "RegisterChangeData", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 집계 register_ 테이블 데이터 변경
		/// </summary>
		/// <param name="formTable"></param>
		/// <param name="msgId"></param>
		/// <param name="regId"></param>
		/// <param name="formData"></param>
		public void SetRegisterChangeData(string formTable, int msgId, int regId, string formData)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterChangeData";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgId),
				ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
				ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, -1, formData)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 연차관리대장
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="empId"></param>
		/// <param name="seq"></param>
		/// <param name="userDn"></param>
		/// <param name="orgInfo"></param>
		/// <param name="inDate"></param>
		/// <param name="numberA"></param>
		/// <param name="numberB"></param>
		/// <param name="numberC"></param>
		/// <returns></returns>
		public string SetRegisterLEAVE(string stdYear, string empId, string seq, string userDn, string orgInfo, string inDate, string numberA, string numberB, string numberC)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLEAVE";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar,100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 30, empId),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
				ParamSet.Add4Sql("@orginfo", SqlDbType.NVarChar, 100, orgInfo),
				ParamSet.Add4Sql("@indate", SqlDbType.VarChar, 10, inDate),
				ParamSet.Add4Sql("@numbera", SqlDbType.VarChar, 10, numberA),
				ParamSet.Add4Sql("@numberb", SqlDbType.VarChar, 10, numberB),
				ParamSet.Add4Sql("@numberc", SqlDbType.VarChar, 10, numberC),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 휴가 설정
		/// </summary>
		/// <param name="empId"></param>
		/// <param name="leaveDate"></param>
		/// <param name="leaveClass"></param>
		/// <param name="fromTime"></param>
		/// <param name="leaveDn"></param>
		/// <param name="isDel"></param>
		/// <returns></returns>
		public string SetRegisterLEAVEEVENT(string empId, string leaveDate, string leaveClass, string fromTime, string leaveDn, string isDel)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLEAVEEVENT";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, "UR"),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 30, empId),
				ParamSet.Add4Sql("@leavedate", SqlDbType.VarChar, 10, leaveDate),
				ParamSet.Add4Sql("@leaveclass", SqlDbType.NVarChar, 10, leaveClass),
				ParamSet.Add4Sql("@fromtime", SqlDbType.VarChar, 5, fromTime),
				ParamSet.Add4Sql("@leavedn", SqlDbType.NVarChar, 20, leaveDn),
				ParamSet.Add4Sql("@isdel", SqlDbType.Char, 1, isDel),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 계획연차
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="empId"></param>
		/// <param name="seq"></param>
		/// <param name="userDn"></param>
		/// <param name="orgInfo"></param>
		/// <param name="inDate"></param>
		/// <param name="numberA"></param>
		/// <param name="numberB"></param>
		/// <returns></returns>
		public string SetRegisterLEAVEUSE(string stdYear, string empId, string seq, string userDn, string orgInfo, string inDate, string numberA, string numberB)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLEAVEUSE";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 30, empId),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
				ParamSet.Add4Sql("@orginfo", SqlDbType.NVarChar, 100, orgInfo),
				ParamSet.Add4Sql("@indate", SqlDbType.VarChar, 10, inDate),
				ParamSet.Add4Sql("@numbera", SqlDbType.VarChar, 10, numberA),
				ParamSet.Add4Sql("@numberb", SqlDbType.VarChar, 10, numberB),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 계획휴가 설정
		/// </summary>
		/// <param name="empId"></param>
		/// <param name="leaveDate"></param>
		/// <param name="leaveClass"></param>
		/// <param name="fromTime"></param>
		/// <param name="leaveDn"></param>
		/// <param name="isDel"></param>
		/// <returns></returns>
		public string SetRegisterLEAVEUSEEVENT(string empId, string leaveDate, string leaveClass, string fromTime, string leaveDn, string isDel)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.SetRegisterLEAVEUSEEVENT";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, "UR"),
				ParamSet.Add4Sql("@empid", SqlDbType.VarChar, 30, empId),
				ParamSet.Add4Sql("@leavedate", SqlDbType.VarChar, 10, leaveDate),
				ParamSet.Add4Sql("@leaveclass", SqlDbType.NVarChar, 10, leaveClass),
				ParamSet.Add4Sql("@fromtime", SqlDbType.VarChar, 5, fromTime),
				ParamSet.Add4Sql("@leavedn", SqlDbType.NVarChar, 20, leaveDn),
				ParamSet.Add4Sql("@isdel", SqlDbType.Char, 1, isDel),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 승진포인트
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="regId"></param>
		/// <param name="desc"></param>
		/// <param name="header"></param>
		/// <param name="rowInfo"></param>
		public void SetRegisterPROMOTIONPOINT(string mode, int regId, string desc, string header, string rowInfo)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterPROMOTIONPOINT";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 200, desc),
				ParamSet.Add4Sql("@headername", SqlDbType.NText, header),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, rowInfo)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 퇴근현황
		/// </summary>
		/// <param name="stdDept"></param>
		/// <param name="stdPerson"></param>
		/// <param name="logonId"></param>
		/// <param name="stdDate"></param>
		/// <param name="stdTime"></param>
		/// <returns></returns>
		public string SetRegisterINANDOUT(string stdDept, string stdPerson, string logonId, string stdDate, string stdTime)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterINANDOUT";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stddept", SqlDbType.NVarChar, 30, stdDept),
				ParamSet.Add4Sql("@stdperson", SqlDbType.NVarChar, 30, stdPerson),
				ParamSet.Add4Sql("@logonid", SqlDbType.NVarChar, 30, logonId),
				ParamSet.Add4Sql("@stddate", SqlDbType.NVarChar, 30, stdDate),
				ParamSet.Add4Sql("@stdtime", SqlDbType.NVarChar, 30, stdTime),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 유류현황
		/// </summary>
		/// <param name="stdDate"></param>
		/// <param name="normal"></param>
		/// <param name="gas"></param>
		/// <param name="lpg"></param>
		/// <returns></returns>
		public string SetRegisterGAS(string stdDate, string normal, string gas, string lpg)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterGAS";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stddate", SqlDbType.NVarChar, 30, stdDate),
				ParamSet.Add4Sql("@normal", SqlDbType.NVarChar, 30, normal),
				ParamSet.Add4Sql("@gas", SqlDbType.NVarChar, 30, gas),
				ParamSet.Add4Sql("@lpg", SqlDbType.NVarChar, 30, lpg),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 교육포인트
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="regId"></param>
		/// <param name="desc"></param>
		/// <param name="header"></param>
		/// <param name="rowInfo"></param>
		public void SetRegisterEDUCATIONPOINT(string mode, int regId, string desc, string header, string rowInfo)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterEDUCATIONPOINT";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 200, desc),
				ParamSet.Add4Sql("@headername", SqlDbType.NText, header),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, rowInfo)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 환율정보
		/// </summary>
		/// <param name="exDate"></param>
		/// <param name="exType"></param>
		/// <param name="exTypeDesc"></param>
		/// <param name="currency01"></param>
		/// <param name="currency02"></param>
		/// <param name="currency03"></param>
		/// <param name="currency04"></param>
		/// <param name="currency05"></param>
		/// <param name="currency06"></param>
		/// <param name="currency07"></param>
		/// <param name="currency08"></param>
		/// <param name="currency09"></param>
		/// <param name="currency10"></param>
		/// <returns></returns>
		public string SetRegisterEXCHANGEINFO(string exDate, string exType, string exTypeDesc, string currency01, string currency02, string currency03
							, string currency04, string currency05, string currency06, string currency07, string currency08, string currency09, string currency10)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterEXCHANGEINFO";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@exdate", SqlDbType.VarChar, 10, exDate),
				ParamSet.Add4Sql("@extype", SqlDbType.Char, 1, exType),
				ParamSet.Add4Sql("@extypedesc", SqlDbType.NVarChar, 50, exTypeDesc),
				ParamSet.Add4Sql("@currency01", SqlDbType.VarChar, 20, currency01),
				ParamSet.Add4Sql("@currency02", SqlDbType.VarChar, 20, currency02),
				ParamSet.Add4Sql("@currency03", SqlDbType.VarChar, 20, currency03),
				ParamSet.Add4Sql("@currency04", SqlDbType.VarChar, 20, currency04),
				ParamSet.Add4Sql("@currency05", SqlDbType.VarChar, 20, currency05),
				ParamSet.Add4Sql("@currency06", SqlDbType.VarChar, 20, currency06),
				ParamSet.Add4Sql("@currency07", SqlDbType.VarChar, 20, currency07),
				ParamSet.Add4Sql("@currency08", SqlDbType.VarChar, 20, currency08),
				ParamSet.Add4Sql("@currency09", SqlDbType.VarChar, 20, currency09),
				ParamSet.Add4Sql("@currency10", SqlDbType.VarChar, 20, currency10),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 전화번호리스트
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="regId"></param>
		/// <param name="desc"></param>
		/// <param name="header"></param>
		/// <param name="rowInfo"></param>
		public void SetRegisterTELLNUMLIST(string mode, int regId, string desc, string header, string rowInfo)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterTELLNUMLIST";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 200, desc),
				ParamSet.Add4Sql("@headername", SqlDbType.NText, header),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, rowInfo)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 연봉
		/// </summary>
		/// <param name="stdYeaar"></param>
		/// <param name="salaryType"></param>
		/// <param name="empId"></param>
		/// <param name="seq"></param>
		/// <param name="userDn"></param>
		/// <param name="orgInfo"></param>
		/// <param name="inDate"></param>
		/// <param name="contractDate"></param>
		/// <param name="periodFrom"></param>
		/// <param name="periodTo"></param>
		/// <param name="payMonth"></param>
		/// <param name="payTime"></param>
		/// <param name="payA"></param>
		/// <param name="payB"></param>
		/// <param name="payC"></param>
		/// <param name="payD"></param>
		/// <param name="payE"></param>
		/// <returns></returns>
		public string SetRegisterSALARY(string stdYeaar, string salaryType, string empId, string seq, string userDn, string orgInfo, string inDate, string contractDate
						, string periodFrom, string periodTo, string payMonth, string payTime, string payA, string payB, string payC, string payD, string payE)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterSALARY";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYeaar),
				ParamSet.Add4Sql("@salarytype", SqlDbType.Char, 1, salaryType),
				ParamSet.Add4Sql("@empid", SqlDbType.VarChar, 30, empId),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
				ParamSet.Add4Sql("@orginfo", SqlDbType.NVarChar, 100, orgInfo),
				ParamSet.Add4Sql("@indate", SqlDbType.VarChar, 10, inDate),
				ParamSet.Add4Sql("@contractdate", SqlDbType.VarChar, 10, contractDate),
				ParamSet.Add4Sql("@periodfrom", SqlDbType.VarChar, 10, periodFrom),
				ParamSet.Add4Sql("@periodto", SqlDbType.VarChar, 10, periodTo),
				ParamSet.Add4Sql("@paymonth", SqlDbType.VarChar, 10, payMonth),
				ParamSet.Add4Sql("@paytime", SqlDbType.VarChar, 10, payTime),
				ParamSet.Add4Sql("@paya", SqlDbType.VarChar, 10, payA),
				ParamSet.Add4Sql("@payb", SqlDbType.VarChar, 10, payB),
				ParamSet.Add4Sql("@payc", SqlDbType.VarChar, 10, payC),
				ParamSet.Add4Sql("@payd", SqlDbType.VarChar, 10, payD),
				ParamSet.Add4Sql("@paye", SqlDbType.VarChar, 10, payE),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 윤리
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="logonId"></param>
		/// <param name="seq"></param>
		/// <param name="userDn"></param>
		/// <param name="orgInfo"></param>
		/// <returns></returns>
		public string SetRegisterETHICAL(string stdYear, string logonId, string seq, string userDn, string orgInfo)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterETHICAL";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@logonid", SqlDbType.NVarChar, 30, logonId),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
				ParamSet.Add4Sql("@orginfo", SqlDbType.NVarChar, 100, orgInfo),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 근태관리
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="userID"></param>
		/// <param name="stdDate"></param>
		/// <param name="stdTime"></param>
		/// <param name="status"></param>
		/// <param name="ip"></param>
		/// <param name="etc"></param>
		/// <param name="reg"></param>
		/// <returns></returns>
		public string SetRegisterWORKSTATUS(string stdYear, int userID, string stdDate, string stdTime, string status, string ip, string etc, string reg)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterWORKSTATUS";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@stddate", SqlDbType.Char, 10, stdDate),
				ParamSet.Add4Sql("@stdtime", SqlDbType.VarChar, 30, stdTime),
				ParamSet.Add4Sql("@stdstatus", SqlDbType.Char, 1, status),
				ParamSet.Add4Sql("@stdip", SqlDbType.VarChar, 20, ip),
				ParamSet.Add4Sql("@stdetc", SqlDbType.VarChar, 30, etc),
				ParamSet.Add4Sql("@stdreg", SqlDbType.VarChar, 30, reg),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 사용자현황
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="userID"></param>
		/// <param name="handPhone"></param>
		/// <param name="emgPhone"></param>
		/// <param name="comPhone"></param>
		/// <param name="stdWork"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public string SetRegisterRegisteruserinfo(string stdYear, string userID, string handPhone, string emgPhone, string comPhone, string stdWork, string startDate, string endDate)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterRegisteruserinfo";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@stdhandphone", SqlDbType.VarChar, 20, handPhone),
				ParamSet.Add4Sql("@stdemgphone", SqlDbType.VarChar, 20, emgPhone),
				ParamSet.Add4Sql("@stdcomphone", SqlDbType.VarChar, 20, comPhone),
				ParamSet.Add4Sql("@stdwork", SqlDbType.VarChar, 20, stdWork),
				ParamSet.Add4Sql("@stdstrdate", SqlDbType.Char, 10, startDate),
				ParamSet.Add4Sql("@stdenddate", SqlDbType.Char, 10, endDate),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 사용자현황
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="userID"></param>
		/// <param name="school"></param>
		/// <returns></returns>
		public string SetRegisterRegisteruserinfoDETAIL(string stdYear, string userID, string school)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterRegisteruserinfoDETAIL";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 20, userID),
				ParamSet.Add4Sql("@stdschool", SqlDbType.VarChar, 100, school),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 공동연차
		/// </summary>
		/// <param name="stdYear"></param>
		/// <param name="logonId"></param>
		/// <param name="seq"></param>
		/// <param name="userDn"></param>
		/// <param name="orgInfo"></param>
		/// <param name="strday"></param>
		/// <param name="strdate1"></param>
		/// <param name="strtype1"></param>
		/// <param name="strdate2"></param>
		/// <param name="strtype2"></param>
		/// <param name="strdate3"></param>
		/// <param name="strtype3"></param>
		/// <param name="strdate4"></param>
		/// <param name="strtype4"></param>
		/// <param name="strdate5"></param>
		/// <param name="strtype5"></param>
		/// <returns></returns>
		public string SetRegisterGONGGA(string stdYear, string logonId, string seq, string userDn, string orgInfo, string strday, string strdate1, string strtype1
							, string strdate2, string strtype2, string strdate3, string strtype3, string strdate4, string strtype4, string strdate5, string strtype5)
		{
			string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterGONGGA";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 100, strEkpDB),
				ParamSet.Add4Sql("@stdyear", SqlDbType.Char, 4, stdYear),
				ParamSet.Add4Sql("@logonid", SqlDbType.NVarChar, 30, logonId),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
				ParamSet.Add4Sql("@orginfo", SqlDbType.NVarChar, 100, orgInfo),
				ParamSet.Add4Sql("@strday", SqlDbType.NVarChar, 50, strday),
				ParamSet.Add4Sql("@strdate1", SqlDbType.NVarChar, 10, strdate1),
				ParamSet.Add4Sql("@strtype1", SqlDbType.NVarChar, 10, strtype1),
				ParamSet.Add4Sql("@strdate2", SqlDbType.NVarChar, 10, strdate2),
				ParamSet.Add4Sql("@strtype2", SqlDbType.NVarChar, 10, strtype2),
				ParamSet.Add4Sql("@strdate3", SqlDbType.NVarChar, 10, strdate3),
				ParamSet.Add4Sql("@strtype3", SqlDbType.NVarChar, 10, strtype3),
				ParamSet.Add4Sql("@strdate4", SqlDbType.NVarChar, 10, strdate4),
				ParamSet.Add4Sql("@strtype4", SqlDbType.NVarChar, 10, strtype4),
				ParamSet.Add4Sql("@strdate5", SqlDbType.NVarChar, 10, strdate5),
				ParamSet.Add4Sql("@strtype5", SqlDbType.NVarChar, 10, strtype5),

				ParamSet.Add4Sql("@return", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}
		#endregion

		#region [나의할일 (결재문서, ECN, 금형) 조회기록]
		/// <summary>
		/// 조회 로깅 설정
		/// </summary>
		/// <param name="xfAlias">양식구분</param>
		/// <param name="fdID">폴더구별자</param>
		/// <param name="actorID">조회자</param>
		/// <param name="msgID">양식식별자</param>
		/// <param name="ip">IP</param>
		public void ViewCount(string xfAlias, int fdID, int actorID, string msgID, string ip)
		{
			string strQuery = @"
IF @xfalias = 'tooling' -- 금형대장
BEGIN
    SELECT @msgid = fiid FROM BFFLOWFORMS_CRESYN.admin.REGISTER_TOOLING (NOLOCK) WHERE TOOLING_NUMBER = @msgid
END

IF NOT EXISTS (SELECT Viewer FROM admin.PH_EVENT_VIEW (NOLOCK) 
	WHERE XFAlias = @xfalias AND MessageID = @msgid AND Viewer = @actorid)
BEGIN
	INSERT INTO admin.PH_EVENT_VIEW WITH(ROWLOCK)
	(XFAlias, MessageID, Viewer, ViewDate)
	VALUES (@xfalias, @msgid, @actorid, GETDATE())
END

INSERT INTO admin.PH_EVENT_AUDIT WITH (ROWLOCK)
(FD_ID, ActTime, XFAlias, MessageID, Actor, IP)
VALUES
(@fdid, GETDATE(), @xfalias, @msgid, @actorid, @ip)
";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 30, msgID),
				ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 30, ip)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 나의할일 조회설정
		/// </summary>
		/// <param name="xfAlias">양식구분</param>
		/// <param name="actorID">조회자</param>
		/// <param name="msgID">양식식별자</param>
		/// <param name="wnID">연결작업ID</param>
		public void ViewCount(string xfAlias, int actorID, string msgID, string wnID)
		{
			string strQuery = @"
IF @xfalias = 'ecnplan'
BEGIN
    UPDATE admin.BF_WORK_ITEM_NOTICE WITH (ROWLOCK)
    SET ViewDate = GETDATE()
    WHERE AppAlias = @xfalias AND AppMID = @msgid AND PartID = @actorid --WnID = @wnid
    AND (ViewDate IS NULL OR ViewDate > GETDATE())
END
ELSE
BEGIN
    UPDATE admin.BF_WORK_ITEM_NOTICE WITH (ROWLOCK)
    SET ViewDate = GETDATE()
    WHERE WnID = @wnid
    AND (ViewDate IS NULL OR ViewDate > GETDATE())
END
";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 30, msgID),
				ParamSet.Add4Sql("@wnid", SqlDbType.BigInt, 8, Convert.ToInt64(wnID))
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// ECN업무계획서 작업 상태 설정
		/// </summary>
		/// <param name="ecnType"></param>
		/// <param name="xfAlias"></param>
		/// <param name="actorID"></param>
		/// <param name="msgID"></param>
		/// <param name="wnID"></param>
		public void SetECNFlag(string ecnType, string xfAlias, int actorID, int msgID, string wnID)
		{
			string strQuery = @"
IF @type = 'P'
BEGIN
    UPDATE admin.BF_WORK_ITEM_NOTICE WITH (ROWLOCK)
    SET Col1 = 'Y'
    WHERE AppAlias = @xfalias AND AppMID = @msgid AND PartID = @actorid --WnID = @wnid
END
ELSE IF @type = 'A'
BEGIN
    UPDATE admin.BF_WORK_ITEM_NOTICE WITH (ROWLOCK)
    SET Col2 = 'Y'
    WHERE AppAlias = @xfalias AND AppMID = @msgid
END
";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@type", SqlDbType.Char, 1, ecnType),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 30, msgID),
				ParamSet.Add4Sql("@wnid", SqlDbType.BigInt, 8, Convert.ToInt64(wnID))
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 나의할일 이관
		/// </summary>
		/// <param name="wnIDs"></param>
		/// <param name="targetUserID"></param>
		/// <param name="targetName"></param>
		/// <param name="targetCode"></param>
		/// <param name="targetMail"></param>
		public void TransferWorkNotice(string wnIDs, string targetUserID, string targetName, string targetCode, string targetMail)
		{
			string strQuery = @"
INSERT INTO admin.BF_WORK_ITEM_NOTICE
(
	CompanyCode, WnDisplay, WnState, WnKind, PartType, PartID, PartName, PartCode, PartMail, ReqDate, CreateDate
	, AppAlias, AppID, AppDN, AppMID, AppPIID, PreAppAlias, PreAppID, PreAppDN, PreMID, PrePIID, PreWIID
	, RequesterID, Requester, RequesterDeptID, RequesterDept, Col3, Col4, Col5
)
SELECT CompanyCode, WnDisplay, WnState, WnKind, PartType, @partid, @partname, @partcode, @partmail, CONVERT(VARCHAR(19), GETDATE(), 121), GETDATE()
	, AppAlias, AppID, AppDN, AppMID, AppPIID, PreAppAlias, PreAppID, PreAppDN, PreMID, PrePIID, PreWIID
	, RequesterID, Requester, RequesterDeptID, RequesterDept, WnID, PartID, PartName
FROM admin.BF_WORK_ITEM_NOTICE
WHERE WnID IN (" + wnIDs + @")

UPDATE admin.BF_WORK_ITEM_NOTICE SET WnState = 5, CompletedDate = GETDATE(), Col4 = @partid, Col5 = @partname
WHERE WnID IN (" + wnIDs + ")";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@partid", SqlDbType.VarChar, 50, targetUserID),
				ParamSet.Add4Sql("@partname", SqlDbType.NVarChar, 50, targetName),
				ParamSet.Add4Sql("@partcode", SqlDbType.NVarChar, 50, targetCode),
				ParamSet.Add4Sql("@partmail", SqlDbType.NVarChar, 50, targetMail)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [VOC 관련]
		/// <summary>
		/// AS대장 목록 및 통계 가져오기
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="cond1"></param>
		/// <param name="cond2"></param>
		/// <param name="cond3"></param>
		/// <param name="cond4"></param>
		/// <param name="cond5"></param>
		/// <param name="cond6"></param>
		/// <param name="cond7"></param>
		/// <param name="page"></param>
		/// <param name="pageCount"></param>
		/// <param name="baseSort"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="searchCol"></param>
		/// <param name="searchText"></param>
		/// <returns></returns>
		public DataSet GetRegisterVOC(string mode, string from, string to
						, string cond1, string cond2, string cond3, string cond4, string cond5, string cond6, string cond7, string cond8, string cond9
                        , int page, int pageCount, string baseSort, string sortCol, string sortType, string searchCol, string searchText)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetRegisterVOC";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
				ParamSet.Add4Sql("@from", SqlDbType.VarChar, 10, from),
				ParamSet.Add4Sql("@to", SqlDbType.VarChar, 10, to),
				ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 50, cond1),
				ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 50, cond2),
				ParamSet.Add4Sql("@cond3", SqlDbType.NVarChar, 50, cond3),
				ParamSet.Add4Sql("@cond4", SqlDbType.NVarChar, 50, cond4),
				ParamSet.Add4Sql("@cond5", SqlDbType.NVarChar, 50, cond5),
				ParamSet.Add4Sql("@cond6", SqlDbType.NVarChar, 50, cond6),
				ParamSet.Add4Sql("@cond7", SqlDbType.NVarChar, 50, cond7),
                ParamSet.Add4Sql("@cond8", SqlDbType.NVarChar, 50, cond8),
                ParamSet.Add4Sql("@cond9", SqlDbType.NVarChar, 50, cond9),
                ParamSet.Add4Sql("@page", SqlDbType.Int, page),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, pageCount),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.NVarChar, 50, baseSort),
				ParamSet.Add4Sql("@sort_col", SqlDbType.NVarChar, 50, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.NVarChar, 5, sortType),
				ParamSet.Add4Sql("@search_col", SqlDbType.NVarChar, 50, searchCol),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 50, searchText),

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", "RegisterVOC", 60, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

        /// <summary>
        /// AS관리대장 등록
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="regId"></param>
        /// <param name="request"></param>
        /// <param name="rctDT"></param>
        /// <param name="customer"></param>
        /// <param name="contact"></param>
        /// <param name="address"></param>
        /// <param name="modelNmA"></param>
        /// <param name="modelNmB"></param>
        /// <param name="modelColor"></param>
        /// <param name="serialNo"></param>
        /// <param name="purchDT"></param>
        /// <param name="kind"></param>
        /// <param name="result"></param>
        /// <param name="status"></param>
        /// <param name="repair"></param>
        /// <param name="trouble"></param>
        /// <param name="brand"></param>
        /// <param name="usage"></param>
        /// <param name="exptDT"></param>
        /// <param name="compDT"></param>
        /// <param name="tat"></param>
        /// <param name="contents"></param>
        /// <param name="reason"></param>
        /// <param name="reasonC"></param>
        /// <param name="description"></param>
        /// <param name="etc"></param>
        /// <param name="memo1"></param>
        /// <param name="memo2"></param>
        /// <param name="cgId"></param>
        /// <param name="cgNm"></param>
        /// <param name="cgDptId"></param>
        /// <param name="cgDpt"></param>
        /// <param name="currentId"></param>
        /// <param name="currentDn"></param>
        /// <param name="currentDeptId"></param>
        /// <param name="currentDept"></param>
        /// <param name="etcField"></param>
        public void SetRegisterVOC(string mode, long regId, string request, string rctDT, string customer, string contact, string address
							, string modelNmA, string modelNmB, string modelColor, string serialNo, string purchDT, string kind, string result
							, string status, string repair, string trouble, string brand, string usage, string exptDT, string compDT
                            , string tat, string contents, string reason, string reasonC, string description, string etc, string memo1, string memo2
							, string cgId, string cgNm, string cgDptId, string cgDpt, string currentId, string currentDn, string currentDeptId, string currentDept, string etcField)
		{

			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterVOC";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@regid", SqlDbType.BigInt, regId),
				ParamSet.Add4Sql("@request", SqlDbType.NVarChar, 20, request),
				ParamSet.Add4Sql("@rctdt", SqlDbType.VarChar, 10, rctDT),
				ParamSet.Add4Sql("@customer", SqlDbType.NVarChar, 50, customer),
				ParamSet.Add4Sql("@contact", SqlDbType.NVarChar, 50, contact),
				ParamSet.Add4Sql("@address", SqlDbType.NVarChar, 200, address),
				ParamSet.Add4Sql("@modelnma", SqlDbType.NVarChar, 50, modelNmA),
				ParamSet.Add4Sql("@modelnmb", SqlDbType.NVarChar, 50, modelNmB),
				ParamSet.Add4Sql("@modelcolor", SqlDbType.NVarChar, 20, modelColor),
				ParamSet.Add4Sql("@serialno", SqlDbType.NVarChar, 30, serialNo),
				ParamSet.Add4Sql("@purchdt", SqlDbType.NVarChar, 20, purchDT),
				ParamSet.Add4Sql("@kind", SqlDbType.NVarChar, 100, kind),
				ParamSet.Add4Sql("@result", SqlDbType.NVarChar, 20, result),
				ParamSet.Add4Sql("@status", SqlDbType.NVarChar, 20, status),
				ParamSet.Add4Sql("@repair", SqlDbType.NVarChar, 100, repair),
				ParamSet.Add4Sql("@trouble", SqlDbType.NVarChar, 100, trouble),
                ParamSet.Add4Sql("@brand", SqlDbType.NVarChar, 100, brand),
                ParamSet.Add4Sql("@usage", SqlDbType.NVarChar, 100, usage),
                ParamSet.Add4Sql("@exptdt", SqlDbType.VarChar, 10, exptDT),
				ParamSet.Add4Sql("@compdt", SqlDbType.VarChar, 10, compDT),
				ParamSet.Add4Sql("@tat", SqlDbType.NVarChar, 20, tat),
				ParamSet.Add4Sql("@contents", SqlDbType.NVarChar, 1000, contents),
				ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, 1000, reason),
				ParamSet.Add4Sql("@reasonc", SqlDbType.NVarChar, 200, reasonC),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, description),
				ParamSet.Add4Sql("@etc", SqlDbType.NVarChar, 50, etc),
				ParamSet.Add4Sql("@memo1", SqlDbType.NVarChar, 1000, memo1),
				ParamSet.Add4Sql("@memo2", SqlDbType.NVarChar, 1000, memo2),
				ParamSet.Add4Sql("@cgid", SqlDbType.VarChar, 20, cgId),
				ParamSet.Add4Sql("@cgnm", SqlDbType.NVarChar, 50, cgNm),
				ParamSet.Add4Sql("@cgdptid", SqlDbType.VarChar, 20, cgDptId),
				ParamSet.Add4Sql("@cgdpt", SqlDbType.NVarChar, 50, cgDpt),
				ParamSet.Add4Sql("@currentid", SqlDbType.VarChar, 20, currentId),
				ParamSet.Add4Sql("@currentdn", SqlDbType.NVarChar, 50, currentDn),
				ParamSet.Add4Sql("@currentdeptid", SqlDbType.VarChar, 20, currentDeptId),
				ParamSet.Add4Sql("@currentdept", SqlDbType.NVarChar, 50, currentDept),
				ParamSet.Add4Sql("@etcfield", SqlDbType.NVarChar, 2000, etcField)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// VOC 정보 불러오기
		/// </summary>
		/// <param name="regId"></param>
		/// <returns></returns>
		public DataSet GetVOC(long regId)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strQuery = "SELECT * FROM " + strFormDB + ".admin.REGISTER_VOC (NOLOCK) WHERE REGID = @regid";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 2, regId)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
		#endregion

		#region [교육관리]
		/// <summary>
		/// 교육 메인 저장
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="lcmId"></param>
		/// <param name="stdYear"></param>
		/// <param name="applId"></param>
		/// <param name="applDeptId"></param>
		/// <param name="applDN"></param>
		/// <param name="applEmpNo"></param>
		/// <param name="applGrade"></param>
		/// <param name="applDept"></param>
		/// <param name="applCorp"></param>
		/// <param name="clsDN1"></param>
		/// <param name="clsDN2"></param>
		/// <param name="clsDN3"></param>
		/// <param name="clsDN4"></param>
		/// <param name="clsDN5"></param>
		/// <param name="clsCD1"></param>
		/// <param name="clsCD2"></param>
		/// <param name="clsCD3"></param>
		/// <param name="clsCD4"></param>
		/// <param name="clsCD5"></param>
		/// <param name="courseId"></param>
		/// <param name="instId"></param>
		/// <param name="courseDN"></param>
		/// <param name="instDN"></param>
		/// <param name="place"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		/// <param name="durDay"></param>
		/// <param name="durTime"></param>
		/// <param name="point"></param>
		/// <param name="cost"></param>
		/// <param name="contents"></param>
		/// <param name="summary"></param>
		/// <param name="remark"></param>
		/// <param name="currentId"></param>
		/// <param name="currentDN"></param>
		/// <param name="currentDeptId"></param>
		/// <param name="currentDept"></param>
		public void SetLCM(string mode, int lcmId, string stdYear
							, string applId, string applDeptId, string applDN, string applEmpNo, string applGrade, string applDept, string applCorp
							, string clsDN1, string clsDN2, string clsDN3, string clsDN4, string clsDN5
							, string clsCD1, string clsCD2, string clsCD3, string clsCD4, string clsCD5
							, string courseId, int instId, string courseDN, string instDN, string place
							, string fromDate, string toDate, string durDay, string durTime, string point
							, string cost, string contents, string summary, string remark
							, int currentId, string currentDN, int currentDeptId, string currentDept)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLCM";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@lcmid", SqlDbType.Int, 4, lcmId),
				ParamSet.Add4Sql("@stdyear", SqlDbType.NVarChar, 20, stdYear),
				ParamSet.Add4Sql("@applid", SqlDbType.NVarChar, 30, applId),
				ParamSet.Add4Sql("@appldeptid", SqlDbType.NVarChar, 30, applDeptId),
				ParamSet.Add4Sql("@appldn", SqlDbType.NVarChar, 100, applDN),
				ParamSet.Add4Sql("@applempno", SqlDbType.NVarChar, 50, applEmpNo),
				ParamSet.Add4Sql("@applgrade", SqlDbType.NVarChar, 50, applGrade),
				ParamSet.Add4Sql("@appldept", SqlDbType.NVarChar, 100, applDept),
				ParamSet.Add4Sql("@applcorp", SqlDbType.NVarChar, 100, applCorp),
				ParamSet.Add4Sql("@clsdn1", SqlDbType.NVarChar, 50, clsDN1),
				ParamSet.Add4Sql("@clsdn2", SqlDbType.NVarChar, 50, clsDN2),
				ParamSet.Add4Sql("@clsdn3", SqlDbType.NVarChar, 50, clsDN3),
				ParamSet.Add4Sql("@clsdn4", SqlDbType.NVarChar, 50, clsDN4),
				ParamSet.Add4Sql("@clsdn5", SqlDbType.NVarChar, 50, clsDN5),
				ParamSet.Add4Sql("@clscd1", SqlDbType.NVarChar, 20, clsCD1),
				ParamSet.Add4Sql("@clscd2", SqlDbType.NVarChar, 20, clsCD2),
				ParamSet.Add4Sql("@clscd3", SqlDbType.NVarChar, 20, clsCD3),
				ParamSet.Add4Sql("@clscd4", SqlDbType.NVarChar, 20, clsCD4),
				ParamSet.Add4Sql("@clscd5", SqlDbType.NVarChar, 20, clsCD5),
				ParamSet.Add4Sql("@courseid", SqlDbType.Int, 4, courseId), //0
				ParamSet.Add4Sql("@instid", SqlDbType.Int, 4, instId), //0
				ParamSet.Add4Sql("@coursedn", SqlDbType.NVarChar, 200, courseDN),
				ParamSet.Add4Sql("@instdn", SqlDbType.NVarChar, 100, instDN),
				ParamSet.Add4Sql("@place", SqlDbType.NVarChar, 200, place),				
				ParamSet.Add4Sql("@fromdate", SqlDbType.NVarChar, 20, fromDate),
				ParamSet.Add4Sql("@todate", SqlDbType.NVarChar, 20, toDate),
				ParamSet.Add4Sql("@durday", SqlDbType.NVarChar, 20, durDay),
				ParamSet.Add4Sql("@durtime", SqlDbType.NVarChar, 20, durTime),
				ParamSet.Add4Sql("@point", SqlDbType.NVarChar, 20, point),
				ParamSet.Add4Sql("@cost", SqlDbType.NVarChar, 20, cost),
				ParamSet.Add4Sql("@contents", SqlDbType.NVarChar, 2000, contents),
				ParamSet.Add4Sql("@summary", SqlDbType.NVarChar, 1000, summary),
				ParamSet.Add4Sql("@remark", SqlDbType.NVarChar, 2000, remark),
				ParamSet.Add4Sql("@currentid", SqlDbType.Int, 4, currentId),
				ParamSet.Add4Sql("@currentdn", SqlDbType.NVarChar, 50, currentDN),
				ParamSet.Add4Sql("@currentdeptid", SqlDbType.Int, 4, currentDeptId),
				ParamSet.Add4Sql("@currentdept", SqlDbType.NVarChar, 50, currentDept)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 교육과정 저장, 변경
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="courseId"></param>
		/// <param name="stdYear"></param>
		/// <param name="clsDN1"></param>
		/// <param name="clsDN2"></param>
		/// <param name="clsDN3"></param>
		/// <param name="clsDN4"></param>
		/// <param name="clsDN5"></param>
		/// <param name="clsCD1"></param>
		/// <param name="clsCD2"></param>
		/// <param name="clsCD3"></param>
		/// <param name="clsCD4"></param>
		/// <param name="clsCD5"></param>
		/// <param name="courseDN"></param>
		/// <param name="instId"></param>
		/// <param name="instDN"></param>
		/// <param name="place"></param>
		/// <param name="csOrder"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		/// <param name="fromTime"></param>
		/// <param name="toTime"></param>
		/// <param name="durDay"></param>
		/// <param name="durTime"></param>
		/// <param name="point"></param>
		/// <param name="limit"></param>
		/// <param name="stayYN"></param>
		/// <param name="mealYN"></param>
		/// <param name="cost1"></param>
		/// <param name="cost2"></param>
		/// <param name="cost3"></param>
		/// <param name="cost4"></param>
		/// <param name="cost5"></param>
		/// <param name="instructorId"></param>
		/// <param name="instructor"></param>
		/// <param name="instructorInfo1"></param>
		/// <param name="instructorInfo2"></param>
		/// <param name="instructorInfo3"></param>
		/// <param name="contents"></param>
		/// <param name="etc"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="currentId"></param>
		/// <param name="currentDN"></param>
		/// <param name="currentDeptId"></param>
		/// <param name="currentDept"></param>
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		public void SetLCMCOURSE(string mode, int courseId, string stdYear
							, string clsDN1, string clsDN2, string clsDN3, string clsDN4, string clsDN5
							, string clsCD1, string clsCD2, string clsCD3, string clsCD4, string clsCD5
							, string courseDN, int instId, string instDN, string place, int csOrder
							, string fromDate, string toDate, string fromTime, string toTime, string durDay, string durTime, string point, string limit
							, string stayYN, string mealYN, string cost1, string cost2, string cost3, string cost4, string cost5
							, string instructorId, string instructor, string instructorInfo1, string instructorInfo2, string instructorInfo3
							, string contents, string etc, string startDate, string endDate
							, int currentId, string currentDN, int currentDeptId, string currentDept, string isFile, string fileInfo)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLCMCOURSE";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@courseid", SqlDbType.Int, 4, courseId),
				ParamSet.Add4Sql("@stdyear", SqlDbType.NVarChar, 20, stdYear),
				ParamSet.Add4Sql("@clsdn1", SqlDbType.NVarChar, 50, clsDN1),
				ParamSet.Add4Sql("@clsdn2", SqlDbType.NVarChar, 50, clsDN2),
				ParamSet.Add4Sql("@clsdn3", SqlDbType.NVarChar, 50, clsDN3),
				ParamSet.Add4Sql("@clsdn4", SqlDbType.NVarChar, 50, clsDN4),
				ParamSet.Add4Sql("@clsdn5", SqlDbType.NVarChar, 50, clsDN5),
				ParamSet.Add4Sql("@clscd1", SqlDbType.NVarChar, 20, clsCD1),
				ParamSet.Add4Sql("@clscd2", SqlDbType.NVarChar, 20, clsCD2),
				ParamSet.Add4Sql("@clscd3", SqlDbType.NVarChar, 20, clsCD3),
				ParamSet.Add4Sql("@clscd4", SqlDbType.NVarChar, 20, clsCD4),
				ParamSet.Add4Sql("@clscd5", SqlDbType.NVarChar, 20, clsCD5),
				ParamSet.Add4Sql("@coursedn", SqlDbType.NVarChar, 200, courseDN),
				ParamSet.Add4Sql("@instid", SqlDbType.Int, 4, instId), //0
				ParamSet.Add4Sql("@instdn", SqlDbType.NVarChar, 100, instDN),
				ParamSet.Add4Sql("@place", SqlDbType.NVarChar, 200, place),
				ParamSet.Add4Sql("@csorder", SqlDbType.SmallInt, 2, csOrder), //차수, 1
				ParamSet.Add4Sql("@fromdate", SqlDbType.NVarChar, 20, fromDate),
				ParamSet.Add4Sql("@todate", SqlDbType.NVarChar, 20, toDate),
				ParamSet.Add4Sql("@formtime", SqlDbType.NVarChar, 20, fromTime),
				ParamSet.Add4Sql("@totime", SqlDbType.NVarChar, 20, toTime),
				ParamSet.Add4Sql("@durday", SqlDbType.NVarChar, 20, durDay),
				ParamSet.Add4Sql("@durtime", SqlDbType.NVarChar, 20, durTime),
				ParamSet.Add4Sql("@point", SqlDbType.NVarChar, 20, point),
				ParamSet.Add4Sql("@limit", SqlDbType.NVarChar, 20, limit),
				ParamSet.Add4Sql("@stayyn", SqlDbType.NVarChar, 20, stayYN),
				ParamSet.Add4Sql("@mealyn", SqlDbType.NVarChar, 20, mealYN),
				ParamSet.Add4Sql("@cost1", SqlDbType.NVarChar, 20, cost1),
				ParamSet.Add4Sql("@cost2", SqlDbType.NVarChar, 20, cost2),
				ParamSet.Add4Sql("@cost3", SqlDbType.NVarChar, 20, cost3),
				ParamSet.Add4Sql("@cost4", SqlDbType.NVarChar, 20, cost4),
				ParamSet.Add4Sql("@cost5", SqlDbType.NVarChar, 20, cost5),
				ParamSet.Add4Sql("@instructorid", SqlDbType.VarChar, 50, instructorId),
				ParamSet.Add4Sql("@instructor", SqlDbType.NVarChar, 50, instructor),
				ParamSet.Add4Sql("@instructorinfo1", SqlDbType.NVarChar, 100, instructorInfo1),
				ParamSet.Add4Sql("@instructorinfo2", SqlDbType.NVarChar, 100, instructorInfo2),
				ParamSet.Add4Sql("@instructorinfo3", SqlDbType.NVarChar, 100, instructorInfo3),
				ParamSet.Add4Sql("@contents", SqlDbType.NVarChar, 2000, contents),
				ParamSet.Add4Sql("@etc", SqlDbType.NVarChar, 500, etc),
				ParamSet.Add4Sql("@startdate", SqlDbType.NVarChar, 20, startDate),
				ParamSet.Add4Sql("@enddate", SqlDbType.NVarChar, 20, endDate),
				ParamSet.Add4Sql("@currentid", SqlDbType.Int, 4, currentId),
				ParamSet.Add4Sql("@currentdn", SqlDbType.NVarChar, 50, currentDN),
				ParamSet.Add4Sql("@currentdeptid", SqlDbType.Int, 4, currentDeptId),
				ParamSet.Add4Sql("@currentdept", SqlDbType.NVarChar, 50, currentDept),
				ParamSet.Add4Sql("@isfile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NVarChar, -1, fileInfo)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 교육과정 삭제, 복원
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="courseId"></param>
		/// <param name="currentId"></param>
		/// <param name="currentDN"></param>
		/// <param name="currentDeptId"></param>
		/// <param name="currentDept"></param>
		public void DeleteLCMCOURSE(string mode, int courseId, int currentId, string currentDN, int currentDeptId, string currentDept)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLCMCOURSE_DEL";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@courseid", SqlDbType.Int, 4, courseId),
				ParamSet.Add4Sql("@currentid", SqlDbType.Int, 4, currentId),
				ParamSet.Add4Sql("@currentdn", SqlDbType.NVarChar, 50, currentDN),
				ParamSet.Add4Sql("@currentdeptid", SqlDbType.Int, 4, currentDeptId),
				ParamSet.Add4Sql("@currentdept", SqlDbType.NVarChar, 50, currentDept)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 사내교육강사 정보 관리
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="stdYear"></param>
		/// <param name="instructorId"></param>
		/// <param name="ePoint"></param>
		/// <param name="eOpinion"></param>
		/// <param name="isApproved"></param>
		/// <param name="currentId"></param>
		/// <param name="currentDN"></param>
		/// <param name="currentDeptId"></param>
		/// <param name="currentDept"></param>
		public void SetLCMINSTRUCTOR(string mode, string stdYear, string instructorId, string ePoint, string eOpinion
						, string isApproved, int currentId, string currentDN, int currentDeptId, string currentDept)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterLCMINSTRUCTOR";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@stdyear", SqlDbType.NVarChar, 20, stdYear),
				ParamSet.Add4Sql("@instructorid", SqlDbType.VarChar, 30, instructorId),
				ParamSet.Add4Sql("@epoint", SqlDbType.NVarChar, 20, ePoint),
				ParamSet.Add4Sql("@eopinion", SqlDbType.NVarChar, 500, eOpinion),
				ParamSet.Add4Sql("@isapproved", SqlDbType.Char, 1, isApproved),
				ParamSet.Add4Sql("@currentid", SqlDbType.Int, 4, currentId),
				ParamSet.Add4Sql("@currentdn", SqlDbType.NVarChar, 50, currentDN),
				ParamSet.Add4Sql("@currentdeptid", SqlDbType.Int, 4, currentDeptId),
				ParamSet.Add4Sql("@currentdept", SqlDbType.NVarChar, 50, currentDept)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 교육관리에서 사번으로 사용자 정보 가져오기
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		public DataSet GetFormEDUREPORTA(string empId)
		{
			DataSet dsReturn = null;
			string strQuery = @"
SELECT UserID, GR_ID, DisplayName, EmpID, Grade1, GroupName,
(SELECT TOP 1 DN FROM admin.ph_fn_GetParentGRTableForEA(1, 'D', GR_ID, CONVERT(CHAR(10), GETDATE(), 121)) ORDER BY Lev DESC) AS Belong
FROM admin.ph_VIEW_OBJECT_UR_LIST WHERE EmpID = @empid AND Role IN ('chief', 'regular')
";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@empid", SqlDbType.VarChar, 50, empId)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
		#endregion

		#region [품번채번]
		/// <summary>
		/// 품번채번 목록 가져오기
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="formTable"></param>
		/// <param name="cond1"></param>
		/// <param name="cond2"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="baseSort"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="searchCol"></param>
		/// <param name="search"></param>
		/// <returns></returns>
		public DataSet GetRegisterNumberList(string mode, string formTable, string cond1, string cond2, int page, int pageSize
									, string baseSort, string sortCol, string sortType, string searchCol, string search)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetRegisterNUMBERLIST";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
				ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
				ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 50, cond1),
				ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 50, cond2),
				ParamSet.Add4Sql("@page", SqlDbType.Int, page),
				ParamSet.Add4Sql("@count", SqlDbType.Int, pageSize),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.NVarChar, 100, baseSort),
				ParamSet.Add4Sql("@sort_col", SqlDbType.NVarChar, 50, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.NVarChar, 5, sortType),
				ParamSet.Add4Sql("@search_col", SqlDbType.NVarChar, 50, searchCol),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 50, search),

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData(strSP, "", "NumberList", 60, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 채번표
		/// </summary>
		/// <param name="sheetCat"></param>
		/// <returns></returns>
		public DataSet GetRegisterNumberSheet(string sheetCat)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetRegisterNUMBERSHEET";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@sheetcat", SqlDbType.NVarChar, 100, sheetCat)
			};

			ParamData pData = new ParamData(strSP, "", "NumberSheet", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 채번표 코드
		/// </summary>
		/// <param name="sheetCat"></param>
		/// <returns></returns>
		public DataSet GetRegisterNumberSheetCode(string sheetCat)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetRegisterNUMBERSHEET_DDL";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@sheetcat", SqlDbType.NVarChar, 100, sheetCat)
			};

			ParamData pData = new ParamData(strSP, "", "NumberSheetCode", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 품번가져오기
		/// </summary>
		/// <param name="category"></param>
		/// <param name="useNo"></param>
		/// <returns></returns>
		public DataSet GetRegisterNumber(string category, string useNo)
		{
			DataSet dsReturn = null;
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_GetRegisterNUMBERING_MECHANISM";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@category", SqlDbType.NVarChar, 20, category),
				ParamSet.Add4Sql("@useno", SqlDbType.NVarChar, 100, useNo)
			};

			ParamData pData = new ParamData(strSP, "", "NumberSheet", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 품번 목록 추가
		/// </summary>
		/// <param name="category"></param>
		/// <param name="number"></param>
		/// <param name="urId"></param>
		/// <param name="urName"></param>
		/// <param name="deptId"></param>
		/// <param name="deptName"></param>
		/// <param name="model"></param>
		/// <param name="item"></param>
		/// <param name="etc"></param>
		/// <returns></returns>
		public string SetRegisterNumber(string category, string number, int urId, string urName
							, int deptId, string deptName, string model, string item, string etc)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetRegisterNumber";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@category", SqlDbType.NVarChar, 20, category),
				ParamSet.Add4Sql("@number", SqlDbType.NVarChar, 20, number),
				ParamSet.Add4Sql("@uid", SqlDbType.Int, urId),
				ParamSet.Add4Sql("@urnm", SqlDbType.NVarChar, 20, urName),
				ParamSet.Add4Sql("@deptid", SqlDbType.Int, deptId),
				ParamSet.Add4Sql("@deptnm", SqlDbType.NVarChar, 20, deptName),
				ParamSet.Add4Sql("@model", SqlDbType.NVarChar, 20, model),
				ParamSet.Add4Sql("@item", SqlDbType.NVarChar, 20, item),
				ParamSet.Add4Sql("@etc", SqlDbType.NVarChar, 100, etc)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return strReturn;
		}

		/// <summary>
		/// 품번표 변경
		/// </summary>
		/// <param name="sheetCat"></param>
		/// <param name="colNm"></param>
		/// <param name="colValue"></param>
		public void SetRegisterNumberSheet(string sheetCat, string colNm, string colValue)
		{
			string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);
			string strSP = strFormDB + ".admin.ph_up_SetNumberingSheet";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@sheetcat", SqlDbType.NVarChar, 100, sheetCat),
				ParamSet.Add4Sql("@colnm", SqlDbType.NVarChar, 10, colNm),
				ParamSet.Add4Sql("@colval", SqlDbType.NVarChar, 100, colValue)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion
	}
}
