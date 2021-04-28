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
	public class WorkItemDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public WorkItemDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public WorkItemDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 초기 프로세스를 구성한다. 시스템 처리중인 상태로 목록에 표시되지 않느다.
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="sessionCode">회기코드</param>
		/// <param name="xfAlias">양식 정보</param>
		/// <param name="messageID">게시물 식별자</param>
		/// <param name="processID">프로세스 정의 식별자</param>
		/// <param name="name">프로세스 인스턴스 명</param>
		/// <param name="priority">우선순위</param>
		/// <param name="permission">결재처리 결과 오픈 권한</param>
		/// <param name="creatorID">프로세스 생성자</param>
		/// <param name="expectedEnd">처리 마감 예정일</param>
		/// <param name="reserved1">예약 필드</param>
		/// <param name="oid">반환되는 프로세스 인스턴스 식별자</param>
		/// <returns></returns>
		public int CreateProcessInstance(int dnID, string sessionCode, string xfAlias, int messageID, int processID, string name, int state, int priority, string permission, int creatorID, string expectedEnd, string reserved1, out int oid)
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
				ParamSet.Add4Sql("@expectedend", SqlDbType.Char, 10, expectedEnd),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_TMCreateProcessInstance", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 참여자 정보를 구성한다. 여러명일 경우는 이 메소드를 반복 호출한다.
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="step">진행 단계</param>
		/// <param name="subStep">병렬 내에서의 단계</param>
		/// <param name="seq">각 단계에서의 순서</param>
		/// <param name="state">결재 처리 상태</param>
		/// <param name="signStatus">결재 처리 종류</param>
		/// <param name="viewState">뷰 상태</param>
		/// <param name="signKind">결재 처리 역할</param>
		/// <param name="partRole">참여 역할</param>
		/// <param name="partGR">참여 그룹 코드</param>
		/// <param name="partID">참여자 코드</param>
		/// <param name="partType">참여 구분</param>
		/// <param name="partName">참여명</param>
		/// <param name="competencyCode">평가표 코드</param>
		/// <param name="comment">첨언</param>
		/// <param name="partCN">참여자 AD 코드</param>
		/// <param name="partDeptCode">참여자 부서 코드</param>
		/// <param name="wid">반환되는 참여자 식별자</param>
		/// <returns></returns>
		public int CreateWorkItem(int oid, int activityID, int step, int subStep, int seq, int state, int signStatus, int viewState, int signKind, string partRole, string partGR, string partID, string partType, string partName, int competencyCode, string comment, string partCN, string partDeptCode, out int wid, out string return_notice)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@activityid", SqlDbType.Int, 4, activityID),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@signStatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@viewstate", SqlDbType.Int, 4, viewState),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@participant_role", SqlDbType.VarChar, 63, partRole),
				ParamSet.Add4Sql("@participant_gr", SqlDbType.VarChar, 63, partGR),
				ParamSet.Add4Sql("@participant_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 1, partType),
				ParamSet.Add4Sql("@participant_name", SqlDbType.NVarChar, 63, partName),
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyCode),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@participant_cn", SqlDbType.VarChar, 30, partCN),
				ParamSet.Add4Sql("@participant_deptcode", SqlDbType.VarChar, 30, partDeptCode),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, ParameterDirection.Output),
				ParamSet.Add4Sql("@return_notice", SqlDbType.Char, 2, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_TMCreateWorkItem", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				wid = int.Parse(pData.GetParamValue("@wid").ToString());
				return_notice = pData.GetParamValue("@return_notice").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 기존 결재선 삭제 - 결재선은 완전 삭제로 한다.(결재선 추가 변경시만 사용)
		/// </summary>
		/// <param name="connect">DB 연결 정보</param>
		/// <param name="wid">결재자 식별자</param>
		/// <returns></returns>
		public int DeleteSignLine(object connect, int wid)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid)
			};

			ParamData pData = new ParamData("DELETE FROM admin.PH_WORK_ITEM WITH (ROWLOCK) WHERE WorkItemID=@wid", "text", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 현 결재자 위의 결재선 순서 및 결재 역할 변경 반영
		/// </summary>
		/// <param name="wid">결재자 식별자</param>
		/// <param name="step">결재 단계</param>
		/// <param name="subStep">결재 하위 단계</param>
		/// <param name="seq">결재 단계 내에서의 순서</param>
		/// <param name="signKind">결재 역할</param>
		/// <returns></returns>
		public int UpdateSignLine(int wid, int step, int subStep, int seq, int signKind)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid)
			};

			string strQuery = "UPDATE admin.PH_WORK_ITEM WITH (ROWLOCK) SET Step = @step, ";
			strQuery += "SubStep = @substep, Seq = @seq, SignKind = @signkind WHERE WorkItemID=@wid";

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 리스트뷰 상의 선택된 결재문서 삭제
		/// </summary>
		/// <param name="wid">선택된 WorkItemID</param>
		/// <returns></returns>
		public int DeleteWorkListItem(int wid)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid)
			};

			string strQuery = "UPDATE admin.PH_WORK_ITEM WITH (ROWLOCK) ";
			strQuery += " SET DeleteDate = GETDATE() WHERE WorkItemID=@wid";

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 리스트뷰 상의 선택된 결재문서들 삭제
		/// </summary>
		/// <param name="wids">선택된 WorkItemID들</param>
		/// <returns></returns>
		public int DeleteWorkListItems(string wids)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wids", SqlDbType.VarChar, 3000, wids)
			};

			ParamData pData = new ParamData("admin.ph_up_TMDeleteWorkListItems", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 리스트뷰 상의 선택된 보고서 양식(저장된)들 삭제
		/// </summary>
		/// <param name="wids">선택된 ReportID들</param>
		/// <returns></returns>
		public int RemoveSavedListItems(string reportIDs)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@reportds", SqlDbType.VarChar, 3000, reportIDs)
			};

			ParamData pData = new ParamData("admin.ph_up_RemoveReportData", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// Process, WorkItem, Report Master State 처리 상태 변경
		/// </summary>
		/// <param name="entityKind">Process(P)인지 WorkItem(W), Report(R)인지 구별</param>
		/// <param name="stateValue">변경 시킬 상태 값</param>
		/// <returns></returns>
		public int ChangeState(string entityKind, int targetID, int stateValue)
		{
			int iReturn = 0;
			string strQuery = null;
			
			if (entityKind == "P")
			{
				if (stateValue == 7)
				{
					strQuery = "UPDATE admin.PH_PROCESS_INSTANCE WITH (ROWLOCK) SET State=@state, CompletedDate=GETDATE() WHERE OID=@targetid";
				}
				else
				{
					strQuery = "UPDATE admin.PH_PROCESS_INSTANCE WITH (ROWLOCK) SET State=@state WHERE OID=@targetid";
				}
			}
			else if (entityKind == "W")
			{
				if (stateValue == 7)
				{
					strQuery = "UPDATE admin.PH_WORK_ITEM WITH (ROWLOCK) SET State=@state, CompletedDate=GETDATE() WHERE WorkItemID=@targetid";
				}
				else
				{
					strQuery = "UPDATE admin.PH_WORK_ITEM WITH (ROWLOCK) SET State=@state WHERE WorkItemID=@targetid";
				}
			}
			else if (entityKind == "R")
			{
				strQuery = "UPDATE admin.PH_REPORT_MASTER WITH (ROWLOCK) SET DOCSTATE=@state, OUTENDDT=GETDATE() WHERE REPORTID=@targetid";
			}

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, stateValue),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 단일 참여자 정보를 가져온다.
		/// </summary>
		/// <param name="wid">참여자 식별자</param>
		/// <returns></returns>
		public DataSet GetParticipant(int wid)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid)
			};

			StringBuilder sbQuery = new StringBuilder();
			sbQuery.Append("SELECT OID, ActivityID, Priority, Step, SubStep, Seq, State, SignStatus, ViewState, SignKind");
			sbQuery.Append(", ParticipantRole, ParticipantGR, participantID, PartType, ParticipantName");
			sbQuery.Append(", CreateDate, ISNULL(CompletedDate, '2999-12-31') AS CompletedDate, CompetencyCode");
			sbQuery.Append(", ISNULL(Point, 0) AS Point, ISNULL(Comment, '') AS Comment, ParticipantCN");
			sbQuery.Append(", ParticipantMail, ParticipantEmpNo, ParticipantDeptCode, ParticipantDeptName");
			sbQuery.Append(", ISNULL(ParticipantGrade1, '') AS ParticipantGrade1");
			sbQuery.Append(", ISNULL(ParticipantGrade2, '') AS ParticipantGrade2");
			sbQuery.Append(", ISNULL(ParticipantGrade2, '') AS ParticipantGrade3");
			sbQuery.Append(" FROM admin.PH_WORK_ITEM (NOLOCK) WHERE WorkItemID=@wid");

			ParamData pData = new ParamData(sbQuery.ToString(), "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 특정 프로세스 인스턴스에 해당하는 참여자들 정보를 가져온다.
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <returns></returns>
		public DataSet GetParticipants(int oid)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid)
			};

			StringBuilder sbQuery = new StringBuilder();
			sbQuery.Append("SELECT ActivityID, WorkItemID, Priority, Step, SubStep, Seq, State, SignStatus, ViewState");
			sbQuery.Append(", SignKind, ParticipantRole, ParticipantGR, participantID, PartType, ParticipantName");
			sbQuery.Append(", CreateDate, ISNULL(CompletedDate, '2999-12-31') AS CompletedDate, CompetencyCode");
			sbQuery.Append(", ISNULL(Point, 0) AS Point, ISNULL(Comment, '') AS Comment, ParticipantCN");
			sbQuery.Append(", ParticipantMail, ParticipantEmpNo, ParticipantDeptCode, ParticipantDeptName");
			sbQuery.Append(", ISNULL(ParticipantGrade1, '') AS ParticipantGrade1");
			sbQuery.Append(", ISNULL(ParticipantGrade2, '') AS ParticipantGrade2");
			sbQuery.Append(", ISNULL(ParticipantGrade2, '') AS ParticipantGrade3");
			sbQuery.Append(" FROM admin.PH_WORK_ITEM (NOLOCK) WHERE OID=@oid");
			sbQuery.Append(" AND (CHARINDEX('_', ParticipantRole) < 1");
			sbQuery.Append(" OR (ParticipantRole = '_r' AND State <> 7)");
			sbQuery.Append(" OR (ParticipantRole = '_r' AND State = 7 AND SignStatus = 5))");
			sbQuery.Append(" AND ViewState > 0");
			sbQuery.Append(" ORDER BY Step, SubStep, Seq");

			ParamData pData = new ParamData(sbQuery.ToString(), "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 특정 양식에 해당하는 참여자들 정보를 가져온다.
		/// </summary>
		/// <param name="msgID">메세지 식별자</param>
		/// <param name="xfAlias">양식 구별자</param>
		/// <returns></returns>
		public DataSet GetParticipants(int msgID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, msgID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			StringBuilder sbQuery = new StringBuilder();
			sbQuery.Append("SELECT b.OID, a.ActivityID, a.WorkItemID, a.Priority, a.Step, a.SubStep, a.Seq");
			sbQuery.Append(", a.State, a.SignStatus, a.ViewState, a.SignKind, a.ParticipantRole, a.ParticipantGR");
			sbQuery.Append(", a.ParticipantID, a.PartType, a.ParticipantName, a.CreateDate");
			sbQuery.Append(", ISNULL(a.CompletedDate, '2999-12-31') AS CompletedDate, a.CompetencyCode");
			sbQuery.Append(", ISNULL(Point, 0) AS Point, ISNULL(Comment, '') AS Comment, ParticipantCN");
			sbQuery.Append(", a.ParticipantMail, a.ParticipantEmpNo, a.ParticipantDeptCode, a.ParticipantDeptName");
			sbQuery.Append(", ISNULL(a.ParticipantGrade1, '') AS ParticipantGrade1");
			sbQuery.Append(", ISNULL(a.ParticipantGrade2, '') AS ParticipantGrade2");
			sbQuery.Append(", ISNULL(a.ParticipantGrade2, '') AS ParticipantGrade3");
			sbQuery.Append(" FROM admin.PH_WORK_ITEM a (NOLOCK)");
			sbQuery.Append(" INNER JOIN admin.PH_PROCESS_INSTANCE b (NOLOCK)");
			sbQuery.Append(" ON a.OID = b.OID");
			sbQuery.Append(" WHERE b.XFAlias = @xfalias AND b.MessageID = @messageid");
			sbQuery.Append(" AND (CHARINDEX('_', a.ParticipantRole) < 1");
			sbQuery.Append(" OR (a.ParticipantRole = '_r' AND a.State <> 7)");
			sbQuery.Append(" OR (a.ParticipantRole = '_r' AND a.State = 7 AND a.SignStatus = 5))");
			sbQuery.Append(" AND a.ViewState > 0");
			sbQuery.Append(" ORDER BY a.Step DESC, a.SubStep DESC, a.Seq DESC");

			ParamData pData = new ParamData(sbQuery.ToString(), "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 특정 Activity에 미리 지정된 참여자 정보 가져오기
		/// </summary>
		/// <param name="activityID">Activity 식별자</param>
		/// <param name="isGroup">참여자의 그룹 여부</param>
		/// <param name="partType">참여 종류</param>
		/// <param name="optionValue">타 시스템 쿼리를 위한 값</param>
		/// <returns></returns>
		public DataSet GetActivityParticipants(string companycode, int activityID, string isGroup, string partType, string optionValue)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@activityid", SqlDbType.Int, 4, activityID),
				ParamSet.Add4Sql("@isgroup", SqlDbType.Char, 1, isGroup),
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 1, partType),
				ParamSet.Add4Sql("@option", SqlDbType.VarChar, 100, optionValue)
			};

			ParamData pData = new ParamData("admin.ph_up_TMGetActivityParticipants", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 다음 Activity에 미리 지정된 참여자 정보를 반환한다.
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="activityID">Activity 식별자</param>
		/// <param name="optionValue">타 시스템 쿼리를 위한 값</param>
		/// <returns></returns>
		public DataSet GetNextActivityParticipants(string companycode, int oid, int activityID, string optionValue)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@activityid", SqlDbType.Int, 4, activityID),
				ParamSet.Add4Sql("@option", SqlDbType.VarChar, 100, optionValue)
			};

			ParamData pData = new ParamData("admin.ph_up_TMGetNextActivityParticipants", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당 Activity 가져오기
		/// </summary>
		/// <param name="activityID">Activity 식별자</param>
		/// <returns></returns>
		public DataSet GetProcessActivity(int activityID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.Int, 4, activityID)
			};

			StringBuilder sbQuery = new StringBuilder();
			sbQuery.Append("SELECT ProcessID, ActivityRole, Name, Seq, IsGroup, PartType");
			sbQuery.Append(", Progress, PostCond, EvalChart, ISNULL(Description, '') AS Description");
			sbQuery.Append(" FROM admin.PH_PROCESS_ACTIVITY (NOLOCK) WHERE ActivityID=@activityid");

			ParamData pData = new ParamData(sbQuery.ToString(), "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 다음 Activity 가져오기
		/// </summary>
		/// <param name="processID">프로세스 정의 식별자</param>
		/// <param name="activityID">Activity 식별자</param>
		/// <returns></returns>
		public DataSet GetProcessNextActivity(int processID, int activityID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@activityid", SqlDbType.Int, 4, activityID)
			};

			StringBuilder sbQuery = new StringBuilder();
			sbQuery.Append("SELECT a.ActivityID, a.ActivityRole, a.Name, a.Seq, a.IsGroup, a.PartType");
			sbQuery.Append(", a.Progress, a.PostCond, a.EvalChart, ISNULL(a.Description, '') AS Description");
			sbQuery.Append(" FROM admin.PH_PROCESS_ACTIVITY a (NOLOCK)");
			sbQuery.Append(" INNER JOIN admin.PH_PROCESS_ACTIVITY b (NOLOCK)");
			sbQuery.Append(" ON a.ProcessID = b.ProcessID AND a.Seq = b.Seq + 1");
			sbQuery.Append(" WHERE b.ProcessID = @processid AND b.ActivityID=@activityid");

			ParamData pData = new ParamData(sbQuery.ToString(), "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 프로세스에 정의된 Activity들을 가져오기
		/// </summary>
		/// <param name="processID">프로세스 정의 식별자</param>
		/// <returns></returns>
		public DataSet GetProcessActivities(int processID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID)
			};

			StringBuilder sbQuery = new StringBuilder();
			sbQuery.Append("SELECT ActivityID, ActivityRole, Name, Seq, IsGroup, PartType");
			sbQuery.Append(", Progress, PostCond, EvalChart, ISNULL(Description, '') AS Description");
			sbQuery.Append(" FROM admin.PH_PROCESS_ACTIVITY (NOLOCK) WHERE ProcessID=@processid");

			ParamData pData = new ParamData(sbQuery.ToString(), "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 결재 처리
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="wid">결재자 식별자</param>
		/// <param name="signStatus">결재 처리 역할</param>
		/// <param name="signKind">결재 처리 종류</param>
		/// <param name="comment">첨언</param>
		/// <param name="returnNotice">처리 구분</param>
		/// <returns></returns>
		public DataSet SetCurrentWorkItem(int oid, int wid, int signStatus, int signKind, string comment, out string returnNotice)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@return_notice", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetCurrentWorkItem", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				returnNotice = pData.GetParamValue("@return_notice").ToString();
			}

			return dsReturn;
		}

		/// <summary>
		/// 평가, 제안 시스템에서의 WorkItem 승인, 평가 처리를 한다.(대결 처리가 없다)
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="wid">WorkItem 식별자</param>
		/// <returns></returns>
		public DataSet SetCurrentWorkItemForKM(int oid, int wid, int signStatus, string comment, out string parellelGoing)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@return_notice", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetCurrentWorkItemForKM", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				parellelGoing = pData.GetParamValue("@return_notice").ToString();
			}

			return dsReturn;
		}

		/// <summary>
		/// 반송, 취소, 보류등의 처리
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="wid">WorkItem 식별자</param>
		/// <param name="signStatus">결재 처리 역할</param>
		/// <param name="comment">첨언</param>
		/// <returns></returns>
		public DataSet SetCurrentWorkItemOnlyOut(int oid, int wid, int signStatus, string comment)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetCurrentWorkItemOnlyOut", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 전달 처리 - 사용자 또는 부서(수신함에서만)
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="wid">결재자 식별자</param>
		/// <param name="signStatus">결재 처리 역할</param>
		/// <param name="comment">첨언</param>
		/// <param name="targetID">전달 받는 사용자 또는 그룹</param>
		/// <param name="isGroup">사용자 그룹 여부(그룹이면 Y)</param>
		/// <param name="targetSignKind">전달 받는 참여자 결지 종류</param>
		/// <param name="targetDeptCode">전달 받는 사용자 부서 코드</param>
		/// <param name="returnNotice">성공이면 OK</param>
		/// <returns></returns>
		public DataSet SetCurrentWorkItemTransfer(int oid, int wid, int signStatus, string comment, string targetID, string isGroup, string targetSignKind, string targetDeptCode, out string returnNotice)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@target_id", SqlDbType.VarChar, 63, targetID),
				ParamSet.Add4Sql("@target_isgroup", SqlDbType.Char, 1, isGroup),
				ParamSet.Add4Sql("@target_signkind", SqlDbType.Int, 4, targetSignKind),
				ParamSet.Add4Sql("@target_deptcode", SqlDbType.VarChar, 30, targetDeptCode),
				ParamSet.Add4Sql("@return_notice", SqlDbType.VarChar, 20, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetCurrentWorkItemTransfer", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				returnNotice = pData.GetParamValue("@return_notice").ToString();
			}

			return dsReturn;
		}

		/// <summary>
		/// 평가 제안 시스템 전달 처리
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="wid">결재자 식별자</param>
		/// <param name="signStatus">결재 처리 역할</param>
		/// <param name="comment">첨언</param>
		/// <param name="targetID">전달 받는 사용자 또는 그룹</param>
		/// <param name="targetSignKind">전달 받는 참여자 결지 종류</param>
		/// <param name="targetDeptCode">전달 받는 사용자 부서 코드</param>
		/// <param name="returnNotice">성공이면 OK</param>
		/// <returns></returns>
		public DataSet SetCurrentWorkItemTransferForKM(int oid, int wid, int signStatus, string comment, string targetID, string targetSignKind, string targetDeptCode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@target_id", SqlDbType.VarChar, 63, targetID),
				ParamSet.Add4Sql("@target_signkind", SqlDbType.Int, 4, targetSignKind),
				ParamSet.Add4Sql("@target_deptcode", SqlDbType.VarChar, 30, targetDeptCode)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetCurrentWorkItemTransferForKM", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 평가 제안 시스템 지정 반송 처리 - 평가자에서 다시 관리자로
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="targetWID">지정 반송 대상자</param>
		/// <param name="currentWID">현 결재자</param>
		/// <param name="comment">첨언</param>
		/// <returns></returns>
		public int SetWorkItemBackForKM(int oid, int targetWID, int currentWID, string comment)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@target_wid", SqlDbType.Int, 4, targetWID),
				ParamSet.Add4Sql("@current_wid", SqlDbType.Int, 4, currentWID),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetWorkItemBackForKM", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 마지막 결재자 처리
		/// </summary>
		/// <param name="oid">프로세스 인스턴스 식별자</param>
		/// <param name="wid">마지막 결재자 식별자</param>
		/// <returns></returns>
		public int SetLastWorkItem(int oid, int wid)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid)
			};

			ParamData pData = new ParamData("admin.ph_up_TMSetLastWorkItem", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 부재자 설정에 의한 대결자 정보 가져오기
		/// </summary>
		/// <param name="participantID">부재자 ID</param>
		/// <returns></returns>
		public DataSet GetDeputyParticipant(int participantID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@participant_id", SqlDbType.VarChar, 63, participantID)
			};

			ParamData pData = new ParamData("admin.ph_up_TMGetDeputyParticipant", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서함에 따른 작업 목록 가져오기
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="xfAlias">문서 종류</param>
		/// <param name="session">회기(기본 '')</param>
		/// <param name="partGR">조회하려는 부서</param>
		/// <param name="partID">조회하려는 사용자</param>
		/// <param name="piState">프로세스 인스턴스 상태</param>
		/// <param name="state">작업 처리 상태</param>
		/// <param name="viewState">작업 조회 상태</param>
		/// <param name="page">조회하고자 하는 페이지</param>
		/// <param name="count">페이지당 리스트 수</param>
		/// <param name="sortCol">정렬 테이블 필드명</param>
		/// <param name="sortType">정렬 종류</param>
		/// <param name="searchCol">검색 테이블 필드명</param>
		/// <param name="searchText">검색 텍스트</param>
		/// <param name="searchDate">날짜 검색 조건</param>
		/// <param name="totalCnt">총 수를 넘겨주기 위해 선언</param>
		/// <returns></returns>
		public DataSet GetProcessWorkList(string companycode, int dnID, string xfAlias, string session, string location, string partGR, string partID, int piState, int state, int viewState, int page, int count, string baseSortCol, string sortCol, string sortType, string searchCol, string searchText, string searchDate, out int totalCnt)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@session", SqlDbType.VarChar, 7, session),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 63, location),
				ParamSet.Add4Sql("@part_gr", SqlDbType.VarChar, 63, partGR),
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

			ParamData pData = new ParamData("admin.ph_up_TMGetProcessWorkList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCnt = int.Parse(pData.GetParamValue("@total_cnt").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 양식담당자가 수신담당함을 열때 해당 문서 가져오기
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="partGR">조회하려는 부서</param>
		/// <param name="chargeLogonID">담당사용자 로그온아이디</param>
		/// <param name="chargeDeptCode">담당사용자 부서코드</param>
		/// <param name="page">조회하고자 하는 페이지</param>
		/// <param name="count">페이지당 리스트 수</param>
		/// <param name="sortCol">정렬 테이블 필드명</param>
		/// <param name="sortType">정렬 종류</param>
		/// <param name="searchCol">검색 테이블 필드명</param>
		/// <param name="searchText">검색 텍스트</param>
		/// <param name="searchDate">날짜 검색 조건</param>
		/// <param name="totalCnt">총 수를 넘겨주기 위해 선언</param>
		/// <returns></returns>
		public DataSet GetProcessWorkListForMgrBox(string companycode, int dnID, string partGR, string chargeLogonID, string chargeDeptCode
									, int page, int count, string baseSortCol, string sortCol, string sortType, string searchCol, string searchText, string searchDate, out int totalCnt)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@part_gr", SqlDbType.VarChar, 63, partGR),
				ParamSet.Add4Sql("@mgr_logonid", SqlDbType.VarChar, 30, chargeLogonID),
				ParamSet.Add4Sql("@mgr_gralias", SqlDbType.VarChar, 30, chargeDeptCode),
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

			ParamData pData = new ParamData("admin.ph_up_TMGetProcessWorkListForMgrBox", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCnt = int.Parse(pData.GetParamValue("@total_cnt").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// COVIOrg 테이블의 전자결재 암호를 가져온다.
		/// </summary>
		/// <param name="logonID">로그온 계정</param>
		/// <returns></returns>
		public string GetApprovalPassword(string logonID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 256, logonID)
			};

			ParamData pData = new ParamData("SELECT AppPW FROM admin.PH_USER_DETAIL (NOLOCK) WHERE USERID=@userid", "text", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 결재 암호 변경
		/// </summary>
		/// <param name="userid">EKP ID</param>
		/// <param name="newpw">새암호</param>
		/// <returns></returns>
		public int UpdateApprovalPassword(int userid, string newpw)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userid),
				ParamSet.Add4Sql("@newpw", SqlDbType.VarChar, 120, newpw)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateApprovalPassword", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 문서번호 발번
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="prefix">문서번호 첫부분 값(부서코드)</param>
		/// <param name="xfAlias">문서 구분</param>
		/// <param name="docNo">발급된 문서번호</param>
		/// <returns></returns>
		public int GetDocumentNumber(int dnID, string prefix, string xfAlias, out string docNumber)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@groupID", SqlDbType.VarChar, 3, prefix),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@docnumber", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetDocumentNumber", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				docNumber = pData.GetParamValue("@docnumber").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 보고서 시스템 양식 프로세스 정보 가져오기
		/// </summary>
		/// <param name="reportID">프로세스 인스턴스 식별자</param>
		/// <returns></returns>
		public DataSet GetReportProcessInfo(string companycode, int oid)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid)
			};

			ParamData pData = new ParamData("admin.ph_up_TMGetReportProcessInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		///  각 결재함별 리스트 갯수를 가져온다.
		/// </summary>
		/// <param name="dnID">도메인 식별자</param>
		/// <param name="xfAlias">게시물 종류</param>
		/// <param name="session"></param>
		/// <param name="location">결재함 종류</param>
		/// <param name="partGR">부서 GR_ID</param>
		/// <param name="partID">사용자 UserID</param>
		/// <param name="partCN">사용자 LogonID</param>
		/// <returns></returns>
		public DataSet GetWorkItemCount(string companycode, int dnID, string xfAlias, string session, string location, string partGR, string partID, string partCN)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@session", SqlDbType.VarChar, 7, session),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 63, location),
				ParamSet.Add4Sql("@part_gr", SqlDbType.VarChar, 63, partGR),
				ParamSet.Add4Sql("@part_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@part_cn", SqlDbType.VarChar, 63, partCN)
			};

			ParamData pData = new ParamData("admin.ph_up_TMGetWorkItemCount", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 결재양식별로 프로세스 처리 후 작업 할 내용
		/// </summary>
		/// <param name="formID">요청 양식종류</param>
		/// <param name="reportID">요청 양식 인스턴스</param>
		/// <param name="processState">프로세스 처리 상태</param>
		/// <returns></returns>
		public int ChangeReportFormOtherState(string formID, int reportID, string processState)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 63, formID),
				ParamSet.Add4Sql("@reportid", SqlDbType.Int, 4, reportID),
				ParamSet.Add4Sql("@process_state", SqlDbType.VarChar, 5, processState)
			};

			ParamData pData = new ParamData("admin.ph_up_ReportChangeFormOtherState", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}
	}
}
