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

namespace ZumNet.DAL.FlowDac
{
    public class ProcessDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public ProcessDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public ProcessDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 문서함에 따른 작업 목록 가져오기
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="xfAlias">문서 종류</param>
		/// <param name="session">회기(기본 '')</param>
		/// <param name="location">조회하려는 부서</param>
		/// <param name="actRole">ActRole</param>
		/// <param name="partID">조회하려는 사용자</param>
		/// <param name="piState">프로세스 인스턴스 상태</param>
		/// <param name="state">작업 처리 상태</param>
		/// <param name="viewState">작업 조회 상태</param>
		/// <param name="page">조회하고자 하는 페이지</param>
		/// <param name="count">페이지당 리스트 수</param>
		/// <param name="baseSortCol">기본 정렬 필드명</param>
		/// <param name="sortCol">정렬 테이블 필드명</param>
		/// <param name="sortType">정렬 종류</param>
		/// <param name="searchCol">검색 테이블 필드명</param>
		/// <param name="searchText">검색 텍스트</param>
		/// <param name="searchDate">날짜 검색 조건</param>
		/// <returns></returns>
		public DataSet GetProcessWorkList(int dnID, string xfAlias, string session, string location, string actRole, string partID
									, int piState, int state, int viewState, int page, int count, string baseSortCol
									, string sortCol, string sortType, string searchCol, string searchText, string searchDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@session", SqlDbType.VarChar, 7, session),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 63, location),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@part_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@pistate", SqlDbType.SmallInt, 2, piState),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
				ParamSet.Add4Sql("@viewstate", SqlDbType.SmallInt, 2, viewState),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, page),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, 2, count),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.VarChar, 100, baseSortCol),
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 100, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType),
				ParamSet.Add4Sql("@search_col", SqlDbType.VarChar, 100, searchCol),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 100, searchText),
				ParamSet.Add4Sql("@search_date", SqlDbType.VarChar, 500, searchDate),

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessWorkList", "", "ProcessWorkList", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서함에 따른 작업 목록 가져오기
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="session"></param>
		/// <param name="location"></param>
		/// <param name="actRole"></param>
		/// <param name="partID"></param>
		/// <param name="page"></param>
		/// <param name="count"></param>
		/// <param name="baseSortCol"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="searchCol"></param>
		/// <param name="searchText"></param>
		/// <param name="searchDate"></param>
		/// <param name="viewer"></param>
		/// <returns></returns>
		public DataSet GetProcessWorkList(int dnID, string xfAlias, string session, string location, string actRole, string partID
									, int page, int count, string baseSortCol, string sortCol, string sortType, string searchCol
									, string searchText, string searchDate, int viewer)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@session", SqlDbType.VarChar, 7, session),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 63, location),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@part_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, page),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, 2, count),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.VarChar, 100, baseSortCol),
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 100, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType),
				ParamSet.Add4Sql("@search_col", SqlDbType.VarChar, 100, searchCol),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 100, searchText),
				ParamSet.Add4Sql("@search_date", SqlDbType.VarChar, 500, searchDate),
				ParamSet.Add4Sql("@viewer", SqlDbType.Int, 4, viewer),

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessWorkList", "", "ProcessWorkList", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 조건에 따른 프로세스 리스트 (프로셋별, 양식별, 상태별..)
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="admin"></param>
		/// <param name="formId"></param>
		/// <param name="defId"></param>
		/// <param name="viewer"></param>
		/// <param name="state"></param>
		/// <param name="page"></param>
		/// <param name="count"></param>
		/// <param name="baseSortCol"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="searchCol"></param>
		/// <param name="searchText"></param>
		/// <param name="searchDate"></param>
		/// <returns></returns>
		public DataSet GetListPerMenu(string mode, string admin, string formId, int defId, int viewer, int state, int page, int count
								, string baseSortCol, string sortCol, string sortType, string searchCol, string searchText, string searchDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, admin),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formId),
				ParamSet.Add4Sql("@defid", SqlDbType.Int, 4, defId),
				ParamSet.Add4Sql("@viewer", SqlDbType.Int, 4, viewer),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, page),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, 2, count),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.VarChar, 100, baseSortCol),
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 100, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType),
				ParamSet.Add4Sql("@search_col", SqlDbType.VarChar, 100, searchCol),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 100, searchText),
				ParamSet.Add4Sql("@search_date", SqlDbType.VarChar, 500, searchDate),				

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetListPerMenu", "", "Monitoring", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}


	}
}
