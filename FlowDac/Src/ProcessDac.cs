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
    /// <summary>
	/// 
	/// </summary>
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

		#region [프로세스 정의]
		/// <summary>
		/// 프로세스 정의 생성
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="fromDate">유효 시작일</param>
		/// <param name="toDate">유효 종료일</param>
		/// <param name="name">프로세스명</param>
		/// <param name="priority">중요도</param>
		/// <param name="description">설명글</param>
		/// <param name="inUse">사용여부</param>
		/// <param name="creator">작성자</param>
		/// <param name="reserved1">예약필드</param>
		/// <returns>반환 프로세스 정의 식별자</returns>
		public int CreateProcessDefinition(int dnID, string fromDate, string toDate, string name, int priority
										, string description, string inUse, string creator, string reserved1)
        {
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@from", SqlDbType.Char, 10, fromDate),
				ParamSet.Add4Sql("@to", SqlDbType.Char, 10, toDate),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 200, name),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),

				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessDefinition", "", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 정의 변경
		/// </summary>
		/// <param name="processID">프로셋 정의 식별자</param>
		/// <param name="fromDate">유효 시작일</param>
		/// <param name="toDate">유효 종료일</param>
		/// <param name="name">프로세스명</param>
		/// <param name="priority">중요도</param>
		/// <param name="description">설명글</param>
		/// <param name="inUse">사용여부</param>
		/// <param name="creator">작성자</param>
		/// <param name="reserved1">예약필드</param>
		public void UpdateProcessDefinition(int processID, string fromDate, string toDate, string name, int priority
										, string description, string inUse, string creator, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@from", SqlDbType.Char, 10, fromDate),
				ParamSet.Add4Sql("@to", SqlDbType.Char, 10, toDate),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 200, name),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessDefinition", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 복사 - 단위업무, 참여자, 속성 포함
		/// </summary>
		/// <param name="processID">복사할 프로셋 정의 식별자</param>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="fromDate">유효 시작일</param>
		/// <param name="toDate">유효 종료일</param>
		/// <param name="name">프로세스명</param>
		/// <param name="priority">중요도</param>
		/// <param name="description">설명글</param>
		/// <param name="inUse">사용여부</param>
		/// <param name="creator">작성자</param>
		/// <param name="reserved1">예약필드</param>
		public void CopyProcessDefinition(int processID, int dnID, string fromDate, string toDate, string name
								, int priority, string description, string inUse, string creator, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@from", SqlDbType.Char, 10, fromDate),
				ParamSet.Add4Sql("@to", SqlDbType.Char, 10, toDate),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 200, name),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessDefinition", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
		#endregion

		#region [프로세스 인스턴스, 참여자]
		/// <summary>
		/// 초기 프로세스를 구성한다. 시스템 처리중인 상태로 목록에 표시되지 않느다.
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="sessionCode">회기코드</param>
		/// <param name="xfAlias">양식 정보</param>
		/// <param name="messageID">게시물 식별자</param>
		/// <param name="processID">프로세스 정의 식별자</param>
		/// <param name="name">프로세스 인스턴스 명</param>
		/// <param name="state">상태값</param>
		/// <param name="priority">우선순위</param>
		/// <param name="permission">결재처리 결과 오픈 권한</param>
		/// <param name="creatorID">프로세스 생성자</param>
		/// <param name="creatorDeptID">프로세스 생성자 부서</param>
		/// <param name="started">처리 시작일</param>
		/// <param name="expectedEnd">처리 마감 예정일</param>
		/// <param name="reserved1">예약 필드</param>
		/// <param name="reserved2">예약 필드</param>
		/// <returns>반환되는 프로세스 인스턴스 식별자</returns>
		public int CreateProcessInstance(int dnID, string sessionCode, string xfAlias, int messageID, int processID, string name, int state, int priority
							, string permission, int creatorID, int creatorDeptID, string started, string expectedEnd, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@sessioncode", SqlDbType.VarChar, 7, sessionCode),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 200, name),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@permission", SqlDbType.Char, 1, permission),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@started", SqlDbType.VarChar, 20, started),
				ParamSet.Add4Sql("@expectedend", SqlDbType.VarChar, 20, expectedEnd),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 1000, reserved2),

				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessInstance", "", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 참여자 정보를 구성한다. 여러명일 경우는 이 메소드를 반복 호출한다.
		/// </summary>
		/// <param name="partType"></param>
		/// <param name="partID"></param>
		/// <param name="wID"></param>
		/// <param name="oid"></param>
		/// <param name="parentWID"></param>
		/// <param name="priority"></param>
		/// <param name="step"></param>
		/// <param name="subStep"></param>
		/// <param name="seq"></param>
		/// <param name="state"></param>
		/// <param name="signStatus"></param>
		/// <param name="signKind"></param>
		/// <param name="viewState"></param>
		/// <param name="flag"></param>
		/// <param name="designator"></param>
		/// <param name="activityID"></param>
		/// <param name="bizRole"></param>
		/// <param name="actRole"></param>
		/// <param name="limited"></param>
		/// <param name="receivedDate"></param>
		/// <param name="completedDate"></param>
		/// <param name="competencyCode"></param>
		/// <param name="signature"></param>
		/// <param name="comment"></param>
		/// <param name="partName"></param>
		/// <param name="partDeptCode"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		/// <returns></returns>
		public string CreateWorkItem(string partType, string partID, string wID, int oid, string parentWID, int priority
							, int step, int subStep, int seq, int state, int signStatus, int signKind, int viewState, string flag, string designator
							, string activityID, string bizRole, string actRole, string limited, string receivedDate, string completedDate
							, int competencyCode, string signature, string comment, string partName, string partDeptCode, string reserved1, string reserved2)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 5, partType),
				ParamSet.Add4Sql("@part_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@parent_wid", SqlDbType.VarChar, 33, parentWID),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@signStatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@viewstate", SqlDbType.Int, 4, viewState),
				ParamSet.Add4Sql("@flag", SqlDbType.VarChar, 33, flag),
				ParamSet.Add4Sql("@designator", SqlDbType.VarChar, 33, designator),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@limited", SqlDbType.VarChar, 20, limited),
				ParamSet.Add4Sql("@receiveddate", SqlDbType.VarChar, 20, receivedDate),
				ParamSet.Add4Sql("@completeddate", SqlDbType.VarChar, 20, completedDate),
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyCode),
				ParamSet.Add4Sql("@signature", SqlDbType.NVarChar, 250, signature),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@part_name", SqlDbType.NVarChar, 200, partName),
				ParamSet.Add4Sql("@part_deptcode", SqlDbType.NVarChar, 63, partDeptCode),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 500, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 1000, reserved2),

				ParamSet.Add4Sql("@return_notice", SqlDbType.Char, 2, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateWorkItem", "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}
		#endregion

		#region [작업 목록 및 갯수 조회]
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

		/// <summary>
		/// 문서관리 이관된 결재문서 검색
		/// </summary>
		/// <param name="dnId"></param>
		/// <param name="admin"></param>
		/// <param name="urId"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="baseSort"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="search"></param>
		/// <returns></returns>
		public DataSet GetListTransfer(int dnId, string admin, int urId, int page, int pageSize
								, string baseSort, string sortCol, string sortType, string search)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.SmallInt, 2, dnId),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, admin),
				ParamSet.Add4Sql("@viewer", SqlDbType.Int, 4, urId),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, page),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, 2, pageSize),
				ParamSet.Add4Sql("@base_sort_col", SqlDbType.VarChar, 100, baseSort),
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 100, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 1000, search),

				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetListTransfer", "", "ListTransfer", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 각 결재함별 리스트 갯수 가져오기
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="xfAlias">게시물 종류</param>
		/// <param name="session"></param>
		/// <param name="location">결재함 종류</param>
		/// <param name="actRole">단계구분</param>
		/// <param name="userID">사용자 UserID</param>
		/// <param name="userCN">사용자 LogonID</param>
		/// <param name="userDeptID">사용자 부서ID</param>
		/// <param name="userDeptCD">사용자 부서코드</param>
		/// <param name="adminYN">관리자여부</param>
		/// <returns></returns>
		public string GetWorkItemCount(int dnID, string xfAlias, string session, string location, string actRole
							, string userID, string userCN, string userDeptID, string userDeptCD, string adminYN)
		{
			SqlDataReader dr = null;
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@session", SqlDbType.VarChar, 7, session),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 63, location),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 63, userID),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 63, userCN),
				ParamSet.Add4Sql("@userdeptid", SqlDbType.VarChar, 63, userDeptID),
				ParamSet.Add4Sql("@userdeptcd", SqlDbType.VarChar, 63, userDeptCD),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, adminYN)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetWorkItemCount", "", "WorkItemCount", 30, parameters);

			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					string strCountInfo = "";
					char cFirstDel = (char)12;
					char cSecondDel = (char)11;

					do
					{
						while (dr.Read())
						{
							if (strCountInfo == "") strCountInfo = dr["Location"].ToString() + cFirstDel + dr["Count"].ToString();
							else strCountInfo += cSecondDel + dr["Location"].ToString() + cFirstDel + dr["Count"].ToString();
						}
					}
					while (dr.NextResult());

					strReturn = strCountInfo;
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetWorkItemCount");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return strReturn;
		}

		/// <summary>
		/// 각 결재함별 리스트 갯수 가져오기
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="xfAlias">게시물 종류</param>
		/// <param name="session"></param>
		/// <param name="location">결재함 종류</param>
		/// <param name="actRole">단계구분</param>
		/// <param name="userID">사용자 UserID</param>
		/// <param name="userCN">사용자 LogonID</param>
		/// <param name="userDeptID">사용자 부서ID</param>
		/// <param name="userDeptCD">사용자 부서코드</param>
		/// <param name="adminYN">관리자여부</param>
		/// <param name="reserved1">예비필드1</param>
		/// <param name="reserved2">예비필드2</param>
		/// <param name="reserved3">예비필드3</param>
		/// <returns></returns>
		public string GetWorkItemCount(int dnID, string xfAlias, string session, string location, string actRole, string userID, string userCN
								, string userDeptID, string userDeptCD, string adminYN, string reserved1, string reserved2, string reserved3)
		{
			SqlDataReader dr = null;
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@session", SqlDbType.VarChar, 7, session),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 63, location),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 63, userID),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 63, userCN),
				ParamSet.Add4Sql("@userdeptid", SqlDbType.VarChar, 63, userDeptID),
				ParamSet.Add4Sql("@userdeptcd", SqlDbType.VarChar, 63, userDeptCD),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, adminYN),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 100, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 100, reserved2),
				ParamSet.Add4Sql("@reserved3", SqlDbType.NVarChar, 100, reserved3)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetWorkItemCount", "", "WorkItemCount", 30, parameters);

            try
            {
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					string strCountInfo = "";
					char cFirstDel = (char)12;
					char cSecondDel = (char)11;

					do
					{
						while (dr.Read())
						{
							if (strCountInfo == "") strCountInfo = dr["Location"].ToString() + cFirstDel + dr["Count"].ToString();
							else strCountInfo += cSecondDel + dr["Location"].ToString() + cFirstDel + dr["Count"].ToString();
						}
					}
					while (dr.NextResult());

					strReturn = strCountInfo;
				}
				dr.Close();
			}
			catch(Exception ex)
            {
				ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetWorkItemCount");
			}
            finally
            {
				if (dr != null) dr.Dispose();
			}

			return strReturn;
		}
		#endregion

		#region [첨언 및 댓글 작업]
		/// <summary>
		/// 첨언,댓글 가져오기
		/// </summary>
		/// <param name="regId"></param>
		/// <param name="msgId"></param>
		/// <returns></returns>
		public DataSet SelectWorkItemCmnt(int regId, int msgId)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, msgId)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItemCmnt", "", "WorkItemCmnt", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 첨언,댓글 추가
		/// </summary>
		/// <param name="msgId"></param>
		/// <param name="parentId"></param>
		/// <param name="subParent"></param>
		/// <param name="comment"></param>
		/// <param name="creator"></param>
		/// <param name="creatorId"></param>
		/// <param name="creatorDept"></param>
		/// <param name="creatorDeptId"></param>
		/// <returns></returns>
		public int InsertWorkItemCmnt(int msgId, int parentId, string subParent, string comment
									, string creator, int creatorId, string creatorDept, int creatorDeptId)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, msgId),
				ParamSet.Add4Sql("@parentid", SqlDbType.Int, 4, parentId),
				ParamSet.Add4Sql("@subparent", SqlDbType.NVarChar, 50, subParent),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 50, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorId),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 50, creatorDept),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptId),

				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFInsertWorkItemCmnt", "", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 첨언,댓글 변경
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="regId"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public int UpdateWorkItemCmnt(string mode, int regId, string comment)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@regId", SqlDbType.Int, 4, regId),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment)
			};

			ParamData pData = new ParamData("admin.ph_up_BFUpdateWorkItemCmnt", "", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="processID"></param>
		/// <param name="isInUse"></param>
		/// <returns></returns>
		public DataSet GetProcessListByCondition(int domainID, int processID, string isInUse)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainid", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@isinuse", SqlDbType.Char, 1, isInUse)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessListByCondition", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <param name="domainID"></param>
		/// <param name="formID"></param>
		/// <param name="classID"></param>
		/// <param name="processID"></param>
		/// <param name="docName"></param>
		/// <param name="description"></param>
		/// <param name="selectable"></param>
		/// <param name="xslName"></param>
		/// <param name="cssName"></param>
		/// <param name="jsName"></param>
		/// <param name="usage"></param>
		/// <param name="mainTable"></param>
		/// <returns></returns>
		public int HandleEAFormBasicManagement(string command, int domainID, string formID, int classID, int processID, string docName, string description, string selectable, string xslName, string cssName, string jsName, string usage, string mainTable)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@command", SqlDbType.VarChar, 6, command),
				ParamSet.Add4Sql("@domainid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@classid", SqlDbType.Int, 4, classID),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@docname", SqlDbType.NVarChar, 100, docName),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, description),
				ParamSet.Add4Sql("@selectable", SqlDbType.Char, 1, selectable),
				ParamSet.Add4Sql("@xslname", SqlDbType.VarChar, 100, xslName),
				ParamSet.Add4Sql("@cssname", SqlDbType.VarChar, 100, cssName),
				ParamSet.Add4Sql("@jsname", SqlDbType.VarChar, 100, jsName),
				ParamSet.Add4Sql("@usage", SqlDbType.Char, 1, usage),
				ParamSet.Add4Sql("@mainTable", SqlDbType.VarChar, 100, mainTable)
			};

			ParamData pData = new ParamData("admin.ph_up_BFHandleEAFormBasicManagement", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return iReturn;
		}

		/// <summary>
		/// 폼 관련 업데이트
		/// </summary>
		/// <param name="command"></param>
		/// <param name="formID"></param>
		/// <param name="tableDef"></param>
		/// <param name="tableCount"></param>
		/// <param name="usage"></param>
		/// <returns></returns>
		public int HandleEAFormTableManagement(string command, string formID, string tableDef, int tableCount, string usage)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@command", SqlDbType.Char, 2, command),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@tableDef", SqlDbType.NText, tableDef),
				ParamSet.Add4Sql("@tableCount", SqlDbType.TinyInt, 1, tableCount),
				ParamSet.Add4Sql("@usage", SqlDbType.Char, 1, usage)
			};

			ParamData pData = new ParamData("admin.ph_up_BFHandleEAFormTableManagement", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return iReturn;
		}

		/// <summary>
		/// 양식 폼 기타 정보 저장
		/// </summary>
		/// <param name="formID"></param>
		/// <param name="webEditor"></param>
		/// <param name="htmlFile"></param>
		/// <param name="processNameString"></param>
		/// <param name="validation"></param>
		/// <returns></returns>
		public int HandleEAFormEtcManagement(string formID, string webEditor, string htmlFile, string processNameString, string validation)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				ParamSet.Add4Sql("@webEditor", SqlDbType.VarChar, 20, webEditor),
				ParamSet.Add4Sql("@htmlFile", SqlDbType.NVarChar, 255, htmlFile),
				ParamSet.Add4Sql("@processNameString", SqlDbType.VarChar, 200, processNameString),
				ParamSet.Add4Sql("@validation", SqlDbType.VarChar, 1000, validation)
			};

			ParamData pData = new ParamData("admin.ph_up_BFHandleEAFormEtcManagement", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="oID"></param>
		/// <returns></returns>
		public DataSet GetBFWorkItemOID(int oID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItemOID", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
