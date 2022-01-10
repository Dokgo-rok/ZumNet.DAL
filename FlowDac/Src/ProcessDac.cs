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

			ParamData pData = new ParamData("admin.ph_up_BFUpdateProcessDefinition", "", parameters);

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

			ParamData pData = new ParamData("admin.ph_up_BFCopyProcessDefinition", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 특정 프로세스 정의 가져오기
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.ProcessDefinition GetProcessDefinition(int processID)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.ProcessDefinition pd = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessDefinition", parameters);
            try
            {
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					pd = new Framework.Entities.Flow.ProcessDefinition();

					while (dr.Read())
					{
						pd.ProcessID = processID;
						pd.DnID = Convert.ToInt16(dr["DN_ID"]);
						pd.ValidFromDate = dr["ValidFromDate"].ToString();
						pd.ValidToDate = dr["ValidToDate"].ToString();
						pd.Name = dr["Name"].ToString();
						pd.Priority = Convert.ToInt16(dr["Priority"]);
						pd.Description = dr["Description"].ToString();
						pd.InUse = dr["InUse"].ToString();
						pd.Creator = dr["Creator"].ToString();
						pd.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						pd.Reserved1 = dr["Reserved1"].ToString();
					}
				}
				dr.Close();
			}
			catch(Exception ex)
            {
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
            finally
            {
				if (dr != null) dr.Dispose();
			}

			return pd;
		}

		/// <summary>
		/// 특정 프로세스 정의 가져오기
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="option"></param>
		/// <returns></returns>
		public DataTable GetProcessDefinition(int processID, string option)
		{
			DataSet ds = null;
			DataTable dtReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessDefinition", parameters);

            try
            {
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables["DefList"];
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
		/// 프로세스 정의 목록을 가져온다.
		/// </summary>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public DataTable GetProcessDefinitions(string sortCol, string sortType)
		{
			DataSet ds = null;
			DataTable dtReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 100, sortCol),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessDefinitions", parameters);

			try
			{
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables["DefList"];
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
		/// 프로세스정의를 xml로 만든다.
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public DataSet GetProcessDefinitionForExport(int processID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessDefinitionForExport", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 프로세스 정의 가져오기
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
		/// 프로세스 인스턴스 정보를 가져온다.
		/// </summary>
		/// <param name="oID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.ProcessInstance SelectProcessInstance(int oID)
        {
			SqlDataReader dr = null;
			Framework.Entities.Flow.ProcessInstance pi = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectProcessInstance", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					pi = new Framework.Entities.Flow.ProcessInstance();

					while (dr.Read())
					{
						pi.OID = oID;
						pi.DnID = Convert.ToInt16(dr["DN_ID"]);
						pi.SessionCode = dr["SessionCode"].ToString();
						pi.XfAlias = dr["XFAlias"].ToString();
						pi.MessageID = Convert.ToInt32(dr["MessageID"]);
						pi.ProcessID = Convert.ToInt32(dr["ProcessID"]);
						pi.Name = dr["Name"].ToString();
						pi.State = Convert.ToInt16(dr["State"]);
						pi.Priority = Convert.ToInt16(dr["Priority"]);
						pi.Permission = dr["Permission"].ToString();
						pi.Creator = dr["Creator"].ToString();
						pi.CreatorID = Convert.ToInt32(dr["CreatorID"]);
						pi.CreatorCN = dr["CreatorCN"].ToString();
						pi.CreatorGrade = dr["CreatorGrade"].ToString();
						pi.CreatorDept = dr["CreatorDept"].ToString();
						pi.CreatorDeptID = Convert.ToInt32(dr["CreatorDeptID"]);
						pi.CreatorDeptCode = dr["CreatorDeptCode"].ToString();
						pi.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.Started = Convert.ToDateTime(dr["Started"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.ExpectedEnd = Convert.ToDateTime(dr["ExpectedEnd"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.CompletedDate = Convert.ToDateTime(dr["CompletedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.Reserved1 = dr["Reserved1"].ToString();
						pi.Reserved2 = dr["Reserved2"].ToString();
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return pi;
		}

		/// <summary>
		/// 프로세스 인스턴스 정보를 가져온다.
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.ProcessInstance SelectProcessInstance(int msgID, string xfAlias)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.ProcessInstance pi = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, msgID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectProcessInstanceMsgID", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					pi = new Framework.Entities.Flow.ProcessInstance();

					while (dr.Read())
					{
						pi.OID = Convert.ToInt32(dr["OID"]);
						pi.DnID = Convert.ToInt16(dr["DN_ID"]);
						pi.SessionCode = dr["SessionCode"].ToString();
						pi.XfAlias = xfAlias;
						pi.MessageID = msgID;
						pi.ProcessID = Convert.ToInt32(dr["ProcessID"]);
						pi.Name = dr["Name"].ToString();
						pi.State = Convert.ToInt16(dr["State"]);
						pi.Priority = Convert.ToInt16(dr["Priority"]);
						pi.Permission = dr["Permission"].ToString();
						pi.Creator = dr["Creator"].ToString();
						pi.CreatorID = Convert.ToInt32(dr["CreatorID"]);
						pi.CreatorCN = dr["CreatorCN"].ToString();
						pi.CreatorGrade = dr["CreatorGrade"].ToString();
						pi.CreatorDept = dr["CreatorDept"].ToString();
						pi.CreatorDeptID = Convert.ToInt32(dr["CreatorDeptID"]);
						pi.CreatorDeptCode = dr["CreatorDeptCode"].ToString();
						pi.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.Started = Convert.ToDateTime(dr["Started"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.ExpectedEnd = Convert.ToDateTime(dr["ExpectedEnd"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.CompletedDate = Convert.ToDateTime(dr["CompletedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						pi.Reserved1 = dr["Reserved1"].ToString();
						pi.Reserved2 = dr["Reserved2"].ToString();
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return pi;
		}

		/// <summary>
		/// 단일 참여자 정보를 가져온다.
		/// </summary>
		/// <param name="wID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.WorkItem SelectWorkItem(string wID)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.WorkItem wi = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItem", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					wi = new Framework.Entities.Flow.WorkItem();

					while (dr.Read())
					{
						wi.WorkItemID = wID;
						wi.OID = Convert.ToInt32(dr["OID"]);
						wi.ParentWorkItemID = dr["ParentWorkItemID"].ToString();
						wi.Priority = Convert.ToInt16(dr["Priority"]);
						wi.Step = Convert.ToInt16(dr["Step"]);
						wi.SubStep = Convert.ToInt16(dr["SubStep"]);
						wi.Seq = Convert.ToInt16(dr["Seq"]);
						wi.State = Convert.ToInt16(dr["State"]);
						wi.SignStatus = Convert.ToInt16(dr["SignStatus"]);
						wi.SignKind = Convert.ToInt16(dr["SignKind"]);
						wi.ViewState = Convert.ToInt16(dr["ViewState"]);
						wi.Flag = dr["Flag"].ToString();
						wi.Designator = dr["Designator"].ToString();
						wi.ActivityID = dr["ActivityID"].ToString();
						wi.BizRole = dr["BizRole"].ToString();
						wi.ActRole = dr["ActRole"].ToString();
						wi.ParticipantID = dr["ParticipantID"].ToString();
						wi.PartType = dr["PartType"].ToString();
						wi.Limited = dr["Limited"].ToString();
						wi.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ViewDate = Convert.ToDateTime(dr["ViewDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompletedDate = Convert.ToDateTime(dr["CompletedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompetencyCode = Convert.ToInt32(dr["CompetencyCode"]);
						wi.Point = dr["Point"].ToString();
						wi.Signature = dr["Signature"].ToString();
						wi.Comment = dr["Comment"].ToString();
						wi.ParticipantName = dr["ParticipantName"].ToString();
						wi.ParticipantDeptCode = dr["ParticipantDeptCode"].ToString();
						wi.ParticipantInfo1 = dr["ParticipantInfo1"].ToString();
						wi.ParticipantInfo2 = dr["ParticipantInfo2"].ToString();
						wi.ParticipantInfo3 = dr["ParticipantInfo3"].ToString();
						wi.ParticipantInfo4 = dr["ParticipantInfo4"].ToString();
						wi.ParticipantInfo5 = dr["ParticipantInfo5"].ToString();
						wi.ParticipantInfo6 = dr["ParticipantInfo6"].ToString();
						wi.Reserved1 = dr["Reserved1"].ToString();
						wi.Reserved2 = dr["Reserved2"].ToString();
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return wi;
		}

		/// <summary>
		/// 특정 프로세스에 해당하는 참여자들 정보를 가져온다.
		/// </summary>
		/// <param name="oID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.WorkItemList SelectWorkItems(int oID)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.WorkItemList wiList = null;
			Framework.Entities.Flow.WorkItem wi = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItemOID", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					wiList = new Framework.Entities.Flow.WorkItemList();

					while (dr.Read())
					{
						wi = new Framework.Entities.Flow.WorkItem();

						wi.WorkItemID = dr["WorkItemID"].ToString();
						wi.OID = Convert.ToInt32(dr["OID"]);
						wi.ParentWorkItemID = dr["ParentWorkItemID"].ToString();
						wi.Priority = Convert.ToInt16(dr["Priority"]);
						wi.Step = Convert.ToInt16(dr["Step"]);
						wi.SubStep = Convert.ToInt16(dr["SubStep"]);
						wi.Seq = Convert.ToInt16(dr["Seq"]);
						wi.State = Convert.ToInt16(dr["State"]);
						wi.SignStatus = Convert.ToInt16(dr["SignStatus"]);
						wi.SignKind = Convert.ToInt16(dr["SignKind"]);
						wi.ViewState = Convert.ToInt16(dr["ViewState"]);
						wi.Flag = dr["Flag"].ToString();
						wi.Designator = dr["Designator"].ToString();
						wi.ActivityID = dr["ActivityID"].ToString();
						wi.BizRole = dr["BizRole"].ToString();
						wi.ActRole = dr["ActRole"].ToString();
						wi.ParticipantID = dr["ParticipantID"].ToString();
						wi.PartType = dr["PartType"].ToString();
						wi.Limited = dr["Limited"].ToString();
						wi.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ViewDate = Convert.ToDateTime(dr["ViewDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompletedDate = Convert.ToDateTime(dr["CompletedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompetencyCode = Convert.ToInt32(dr["CompetencyCode"]);
						wi.Point = dr["Point"].ToString();
						wi.Signature = dr["Signature"].ToString();
						wi.Comment = dr["Comment"].ToString();
						wi.ParticipantName = dr["ParticipantName"].ToString();
						wi.ParticipantDeptCode = dr["ParticipantDeptCode"].ToString();
						wi.ParticipantInfo1 = dr["ParticipantInfo1"].ToString();
						wi.ParticipantInfo2 = dr["ParticipantInfo2"].ToString();
						wi.ParticipantInfo3 = dr["ParticipantInfo3"].ToString();
						wi.ParticipantInfo4 = dr["ParticipantInfo4"].ToString();
						wi.ParticipantInfo5 = dr["ParticipantInfo5"].ToString();
						wi.ParticipantInfo6 = dr["ParticipantInfo6"].ToString();
						wi.Reserved1 = dr["Reserved1"].ToString();
						wi.Reserved2 = dr["Reserved2"].ToString();

						wiList.Add(wi);
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return wiList;
		}

		/// <summary>
		/// 특정 양식에 해당하는 참여자들 정보를 가져온다.
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.WorkItemList SelectWorkItems(int msgID, string xfAlias)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.WorkItemList wiList = null;
			Framework.Entities.Flow.WorkItem wi = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, msgID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItemMsgID", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					wiList = new Framework.Entities.Flow.WorkItemList();

					while (dr.Read())
					{
						wi = new Framework.Entities.Flow.WorkItem();

						wi.WorkItemID = dr["WorkItemID"].ToString();
						wi.OID = Convert.ToInt32(dr["OID"]);
						wi.ParentWorkItemID = dr["ParentWorkItemID"].ToString();
						wi.Priority = Convert.ToInt16(dr["Priority"]);
						wi.Step = Convert.ToInt16(dr["Step"]);
						wi.SubStep = Convert.ToInt16(dr["SubStep"]);
						wi.Seq = Convert.ToInt16(dr["Seq"]);
						wi.State = Convert.ToInt16(dr["State"]);
						wi.SignStatus = Convert.ToInt16(dr["SignStatus"]);
						wi.SignKind = Convert.ToInt16(dr["SignKind"]);
						wi.ViewState = Convert.ToInt16(dr["ViewState"]);
						wi.Flag = dr["Flag"].ToString();
						wi.Designator = dr["Designator"].ToString();
						wi.ActivityID = dr["ActivityID"].ToString();
						wi.BizRole = dr["BizRole"].ToString();
						wi.ActRole = dr["ActRole"].ToString();
						wi.ParticipantID = dr["ParticipantID"].ToString();
						wi.PartType = dr["PartType"].ToString();
						wi.Limited = dr["Limited"].ToString();
						wi.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ViewDate = Convert.ToDateTime(dr["ViewDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompletedDate = Convert.ToDateTime(dr["CompletedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompetencyCode = Convert.ToInt32(dr["CompetencyCode"]);
						wi.Point = dr["Point"].ToString();
						wi.Signature = dr["Signature"].ToString();
						wi.Comment = dr["Comment"].ToString();
						wi.ParticipantName = dr["ParticipantName"].ToString();
						wi.ParticipantDeptCode = dr["ParticipantDeptCode"].ToString();
						wi.ParticipantInfo1 = dr["ParticipantInfo1"].ToString();
						wi.ParticipantInfo2 = dr["ParticipantInfo2"].ToString();
						wi.ParticipantInfo3 = dr["ParticipantInfo3"].ToString();
						wi.ParticipantInfo4 = dr["ParticipantInfo4"].ToString();
						wi.ParticipantInfo5 = dr["ParticipantInfo5"].ToString();
						wi.ParticipantInfo6 = dr["ParticipantInfo6"].ToString();
						wi.Reserved1 = dr["Reserved1"].ToString();
						wi.Reserved2 = dr["Reserved2"].ToString();

						wiList.Add(wi);
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return wiList;
		}

		/// <summary>
		/// 특정 카테고리 역할에 해당하는 참여자 정보를 가져온다.
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="bizRole"></param>
		/// <param name="parentWID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.WorkItemList SelectWorkItems(int oID, string bizRole, string parentWID)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.WorkItemList wiList = null;
			Framework.Entities.Flow.WorkItem wi = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
				ParamSet.Add4Sql("@parent_wid", SqlDbType.VarChar, 33, parentWID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItemBizRole", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					wiList = new Framework.Entities.Flow.WorkItemList();

					while (dr.Read())
					{
						wi = new Framework.Entities.Flow.WorkItem();

						wi.OID = oID;
						wi.WorkItemID = dr["WorkItemID"].ToString();
						wi.ParentWorkItemID = dr["ParentWorkItemID"].ToString();
						wi.Priority = Convert.ToInt16(dr["Priority"]);
						wi.Step = Convert.ToInt16(dr["Step"]);
						wi.SubStep = Convert.ToInt16(dr["SubStep"]);
						wi.Seq = Convert.ToInt16(dr["Seq"]);
						wi.State = Convert.ToInt16(dr["State"]);
						wi.SignStatus = Convert.ToInt16(dr["SignStatus"]);
						wi.SignKind = Convert.ToInt16(dr["SignKind"]);
						wi.ViewState = Convert.ToInt16(dr["ViewState"]);
						wi.Flag = dr["Flag"].ToString();
						wi.Designator = dr["Designator"].ToString();
						wi.ActivityID = dr["ActivityID"].ToString();
						wi.BizRole = dr["BizRole"].ToString();
						wi.ActRole = dr["ActRole"].ToString();
						wi.ParticipantID = dr["ParticipantID"].ToString();
						wi.PartType = dr["PartType"].ToString();
						wi.Limited = dr["Limited"].ToString();
						wi.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ReceivedDate = Convert.ToDateTime(dr["ReceivedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.ViewDate = Convert.ToDateTime(dr["ViewDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompletedDate = Convert.ToDateTime(dr["CompletedDate"]).ToString("yyyy-MM-dd HH:mm:ss");
						wi.CompetencyCode = Convert.ToInt32(dr["CompetencyCode"]);
						wi.Point = dr["Point"].ToString();
						wi.Signature = dr["Signature"].ToString();
						wi.Comment = dr["Comment"].ToString();
						wi.ParticipantName = dr["ParticipantName"].ToString();
						wi.ParticipantDeptCode = dr["ParticipantDeptCode"].ToString();
						wi.ParticipantInfo1 = dr["ParticipantInfo1"].ToString();
						wi.ParticipantInfo2 = dr["ParticipantInfo2"].ToString();
						wi.ParticipantInfo3 = dr["ParticipantInfo3"].ToString();
						wi.ParticipantInfo4 = dr["ParticipantInfo4"].ToString();
						wi.ParticipantInfo5 = dr["ParticipantInfo5"].ToString();
						wi.ParticipantInfo6 = dr["ParticipantInfo6"].ToString();
						wi.Reserved1 = dr["Reserved1"].ToString();
						wi.Reserved2 = dr["Reserved2"].ToString();

						wiList.Add(wi);
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return wiList;
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

		/// <summary>
		/// 프로세스 인스턴스 속성을 이용해서 일괄 참여자 정보를 구성한다. attribute = workitem 인 경우
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="activityID"></param>
		/// <param name="attribute"></param>
		/// <param name="partType"></param>
		/// <param name="parentWID"></param>
		/// <param name="priority"></param>
		/// <param name="step"></param>
		/// <param name="seq"></param>
		/// <param name="state"></param>
		/// <param name="signStatus"></param>
		/// <param name="signKind"></param>
		/// <param name="viewState"></param>
		/// <param name="bizRole"></param>
		/// <param name="actRole"></param>
		/// <param name="limited"></param>
		/// <param name="receivedDate"></param>
		/// <param name="completedDate"></param>
		/// <param name="competencyCode"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public string CreateWorkItemWithAttributes(int oid, string activityID, string attribute, string partType, string parentWID, int priority
							, int step, int seq, int state, int signStatus, int signKind, int viewState, string bizRole, string actRole
							, string limited, string receivedDate, string completedDate, int competencyCode, string comment)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@attribute", SqlDbType.VarChar, 50, attribute),
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 5, partType),
				ParamSet.Add4Sql("@parent_wid", SqlDbType.VarChar, 33, parentWID),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@signStatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@viewstate", SqlDbType.Int, 4, viewState),
				ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@limited", SqlDbType.VarChar, 20, limited),
				ParamSet.Add4Sql("@receiveddate", SqlDbType.VarChar, 20, receivedDate),
				ParamSet.Add4Sql("@completeddate", SqlDbType.VarChar, 20, completedDate),
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyCode),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@attributeinfo", SqlDbType.NText, ""),

				ParamSet.Add4Sql("@return_notice", SqlDbType.Char, 2, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateWorkItemWithAttributes", "", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 대결자 정보를 구성한다.
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
		/// <param name="comment"></param>
		/// <param name="partName"></param>
		/// <param name="partDeptCode"></param>
		public void CreateDeputyParticipant(string partType, string partID, string wID, int oid, string parentWID, int priority
								, int step, int subStep, int seq, int state, int signStatus, int signKind, int viewState
								, string flag, string designator, string activityID, string bizRole, string actRole, string limited, string receivedDate
								, string completedDate, int competencyCode, string comment, string partName, string partDeptCode)
		{
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
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@part_name", SqlDbType.NVarChar, 200, partName),
				ParamSet.Add4Sql("@part_deptcode", SqlDbType.NVarChar, 63, partDeptCode),
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateWorkItemWithAttributes", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 기존 결재선 삭제 - 결재선은 완전 삭제로 한다.(결재선 추가 변경시만 사용)
		/// </summary>
		/// <param name="wID">결재자 식별자</param>
		public void DeleteSignLine(string wID)
        {
			string strQuery = "DELETE FROM admin.BF_WORK_ITEM WITH (ROWLOCK) WHERE WorkItemID=@wid";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 기존 결재선 삭제
		/// </summary>
		/// <param name="wIDs">삭제할 결재자들</param>
		public void DeleteSignLines(string wIDs)
		{
			string strQuery = "DELETE FROM admin.BF_WORK_ITEM WITH (ROWLOCK) WHERE WorkItemID IN ('" + wIDs.Replace(",", "','") + "')";

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 현 결재자 위의 결재선 순서 및 결재 역할 변경 반영
		/// </summary>
		/// <param name="wID">결재자 식별자</param>
		/// <param name="step">결재 단계</param>
		/// <param name="subStep">결재 하위 단계</param>
		/// <param name="seq">결재 단계 내에서의 순서</param>
		/// <param name="signKind">결재 역할</param>
		public void UpdateSignLine(string wID, int step, int subStep, int seq, int signKind)
		{
			string strQuery = "UPDATE admin.BF_WORK_ITEM WITH (ROWLOCK) SET Step = @step, SubStep = @substep, Seq = @seq, SignKind = @signkind WHERE WorkItemID=@wid";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 참여자 정보를 갱신한다.
		/// </summary>
		/// <param name="wID"></param>
		/// <param name="step"></param>
		/// <param name="subStep"></param>
		/// <param name="seq"></param>
		/// <param name="priority"></param>
		/// <param name="state"></param>
		/// <param name="signStatus"></param>
		/// <param name="signKind"></param>
		/// <param name="viewState"></param>
		/// <param name="flag"></param>
		/// <param name="designator"></param>
		/// <param name="receivedDate"></param>
		/// <param name="completedDate"></param>
		/// <param name="competencyCode"></param>
		/// <param name="point"></param>
		/// <param name="signature"></param>
		/// <param name="comment"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		public void UpdateWorkItem(string wID, int step, int subStep, int seq, int priority, int state, int signStatus
							, int signKind, int viewState, string flag, string designator, string receivedDate, string completedDate
							, int competencyCode, string point, string signature, string comment, string reserved1, string reserved2)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@signStatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@signkind", SqlDbType.Int, 4, signKind),
				ParamSet.Add4Sql("@viewstate", SqlDbType.Int, 4, viewState),
				ParamSet.Add4Sql("@flag", SqlDbType.VarChar, 33, flag),
				ParamSet.Add4Sql("@designator", SqlDbType.VarChar, 33, designator),
				ParamSet.Add4Sql("@receiveddate", SqlDbType.VarChar, 20, receivedDate),
				ParamSet.Add4Sql("@completeddate", SqlDbType.VarChar, 20, completedDate),
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyCode),
				ParamSet.Add4Sql("@point", SqlDbType.VarChar, 15, point),
				ParamSet.Add4Sql("@signature", SqlDbType.NVarChar, 250, signature),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 500, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 1000, reserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_BFUpdateWorkItem", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// WorkItem 읽은 일자 기록
		/// </summary>
		/// <param name="wID">작업 식별자</param>
		public void UpdateWorkItemViewDate(string wID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFUpdateWorkItemViewDate", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 리스트뷰 상의 선택된 결재문서 삭제
		/// </summary>
		/// <param name="wID">선택된 WorkItemID</param>
		public void DeleteWorkListItem(string wID)
		{
			string strQuery = "UPDATE admin.BF_WORK_ITEM WITH (ROWLOCK) SET DeleteDate = GETDATE() WHERE WorkItemID=@wid";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 리스트뷰 상의 선택된 결재문서들 삭제(최대 100건)
		/// </summary>
		/// <param name="wids">선택된 WorkItemID들</param>
		public void DeleteWorkListItems(string wids)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@wids", SqlDbType.VarChar, 3500, wids)
			};

			ParamData pData = new ParamData("admin.ph_up_BFDeleteWorkListItems", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// Process, WorkItem, Report Master State 처리 상태 변경
		/// </summary>
		/// <param name="entityKind">Process(p)인지 WorkItem(w), Report(r)인지 구별</param>
		/// <param name="targetID"></param>
		/// <param name="stateValue"></param>
		public void ChangeState(string entityKind, string targetID, int stateValue)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@entity", SqlDbType.Char, 1, entityKind),
				ParamSet.Add4Sql("@targetid", SqlDbType.VarChar, 33, targetID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, stateValue)
			};

			ParamData pData = new ParamData("admin.ph_up_BFChangeState", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 반송, 취소, 보류등의 처리
		/// </summary>
		/// <param name="oID">프로세스 인스턴스 식별자</param>
		/// <param name="wID">WorkItem 식별자</param>
		/// <param name="signStatus">결재 처리 역할</param>
		/// <param name="comment">첨언</param>
		public void SetCurrentWorkItemOnlyOut(int oID, string wID, int signStatus, string comment)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 100, comment)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSetCurrentWorkItemOnlyOut", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 부재자 설정에 의한 대결자 정보 가져오기
		/// </summary>
		/// <param name="partID"></param>
		/// <param name="partDeptCode"></param>
		/// <param name="processID"></param>
		/// <param name="formID"></param>
		/// <returns></returns>
		public DataTable GetDeputyParticipant(string partID, string partDeptCode, int processID, string formID)
		{
			string strNext = "";
			return GetDeputyParticipant(partID, partDeptCode, processID, formID, out strNext);
		}

		/// <summary>
		/// 부재자 설정에 의한 대결자 정보 가져오기
		/// </summary>
		/// <param name="partID"></param>
		/// <param name="partDeptCode"></param>
		/// <param name="processID"></param>
		/// <param name="formID"></param>
		/// <param name="nextStep"></param>
		/// <returns></returns>
		public DataTable GetDeputyParticipant(string partID, string partDeptCode, int processID, string formID, out string nextStep)
		{
			DataSet ds = null;
			DataTable dtReturn = null;
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@part_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@part_deptcode", SqlDbType.VarChar, 63, partDeptCode),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
				
				ParamSet.Add4Sql("@next", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetDeputyParticipant", "", parameters);

            try
            {
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables["DeputyParticipant"];
			}
			catch (Exception ex)
            {
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
            finally
            {
				if (ds != null) ds.Dispose();
            }

			nextStep = pData.GetParamValue("@next").ToString();
			return dtReturn;
		}

		/// <summary>
		/// 대결자 정보 변경
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="userID"></param>
		/// <param name="deputyID"></param>
		/// <param name="deputyDept"></param>
		/// <param name="processID"></param>
		/// <param name="formID"></param>
		public void SetDeputy(string mode, int userID, string deputyID, string deputyDept, int processID, string formID)
        {
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@deputyid", SqlDbType.VarChar, 50, deputyID),
				ParamSet.Add4Sql("@deputydept", SqlDbType.VarChar, 50, deputyDept),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSetDeputy", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 병렬 처리시 같은 레벨의 다른 작업들이 완료 됐는지를 확인
		/// </summary>
		/// <param name="oID">프로세스 인스턴스</param>
		/// <param name="parentWID">상위 작업</param>
		/// <param name="wID">현 작업</param>
		/// <param name="step">단계</param>
		/// <returns>0이면 병렬 완료 시킨다.</returns>
		public int ConfirmParallelWorkItem(int oID, string parentWID, string wID, int step)
		{
			int iReturn = 0;
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@parent_wid", SqlDbType.VarChar, 33, parentWID),
				ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step)
			};

			ParamData pData = new ParamData("admin.ph_up_BFConfirmParallelWorkItem", "", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}
			return iReturn;
		}
		#endregion

		#region [프로세스 Activity 관련]
		/// <summary>
		/// 해당 Activity 가져오기
		/// </summary>
		/// <param name="activityID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.Activity GetProcessActivity(string activityID)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.Activity activity = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessActivity", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					activity = new Framework.Entities.Flow.Activity();

					while (dr.Read())
					{
						activity.ActivityID = activityID;
						activity.ProcessID = Convert.ToInt32(dr["ProcessID"]);
						activity.ParentActivityID = dr["ParentActivityID"].ToString();
						activity.Step = Convert.ToInt16(dr["Step"]);
						activity.SubStep = Convert.ToInt16(dr["SubStep"]);
						activity.Random = dr["Random"].ToString();
						activity.Inline = dr["Inline"].ToString();
						activity.BizRole = dr["BizRole"].ToString();
						activity.ActRole = dr["ActRole"].ToString();
						activity.DisplayName = dr["DisplayName"].ToString();
						activity.Progress = dr["Progress"].ToString();
						activity.PartType = dr["PartType"].ToString();
						activity.Limited = dr["Limited"].ToString();
						activity.Review = dr["Review"].ToString();
						activity.ShowLine = dr["ShowLine"].ToString();
						activity.ScopeLine = dr["ScopeLine"].ToString();
						activity.Mandatory = dr["Mandatory"].ToString();
						activity.EvalChart = Convert.ToInt32(dr["EvalChart"]);
						activity.ActivityPreCond = dr["ActivityPreCond"].ToString();
						activity.ActivityPostCond = dr["ActivityPostCond"].ToString();
						activity.WorkItemPreCond = dr["WorkItemPreCond"].ToString();
						activity.WorkItemPostCond = dr["WorkItemPostCond"].ToString();
						activity.Description = dr["Description"].ToString();
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return activity;
		}

		/// <summary>
		/// 프로세스에 정의된 Activity들을 가져오기
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="parentActivity"></param>
		/// <param name="showLine"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.ActivityList GetProcessActivities(int processID, string parentActivity, string showLine)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.ActivityList activityList = null;
			Framework.Entities.Flow.Activity activity = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@parent_activityid", SqlDbType.VarChar, 33, parentActivity),
				ParamSet.Add4Sql("@showline", SqlDbType.Char, 1, showLine)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessActivities", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					activityList = new Framework.Entities.Flow.ActivityList();

					while (dr.Read())
					{
						activity = new Framework.Entities.Flow.Activity();

						activity.ProcessID = processID;
						activity.ActivityID = dr["ActivityID"].ToString();
						activity.ParentActivityID = dr["ParentActivityID"].ToString();
						activity.Step = Convert.ToInt16(dr["Step"]);
						activity.SubStep = Convert.ToInt16(dr["SubStep"]);
						activity.Random = dr["Random"].ToString();
						activity.Inline = dr["Inline"].ToString();
						activity.BizRole = dr["BizRole"].ToString();
						activity.ActRole = dr["ActRole"].ToString();
						activity.DisplayName = dr["DisplayName"].ToString();
						activity.Progress = dr["Progress"].ToString();
						activity.PartType = dr["PartType"].ToString();
						activity.Limited = dr["Limited"].ToString();
						activity.Review = dr["Review"].ToString();
						activity.ShowLine = dr["ShowLine"].ToString();
						activity.ScopeLine = dr["ScopeLine"].ToString();
						activity.Mandatory = dr["Mandatory"].ToString();
						activity.EvalChart = Convert.ToInt32(dr["EvalChart"]);
						activity.ActivityPreCond = dr["ActivityPreCond"].ToString();
						activity.ActivityPostCond = dr["ActivityPostCond"].ToString();
						activity.WorkItemPreCond = dr["WorkItemPreCond"].ToString();
						activity.WorkItemPostCond = dr["WorkItemPostCond"].ToString();
						activity.Description = dr["Description"].ToString();
						activityList.Add(activity);
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return activityList;
		}

		/// <summary>
		/// 프로세스에 정의된 Activity들을 가져오기
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="parentActivityID"></param>
		/// <param name="showLine"></param>
		/// <returns></returns>
		public DataSet SelectProcessActivities(int processID, string parentActivityID, string showLine)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@parent_activityid", SqlDbType.VarChar, 33, parentActivityID),
				ParamSet.Add4Sql("@showline", SqlDbType.Char, 1, showLine)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessActivities", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Activity Schema 가져오기
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="currentActID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.SchemaList GetProcessActivitySchema(int processID, string currentActID)
		{
			SqlDataReader dr = null;
			Framework.Entities.Flow.SchemaList schemaList = null;
			Framework.Entities.Flow.Schema schema = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@parent_activityid", SqlDbType.VarChar, 33, currentActID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessActivitySchema", parameters);
			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					schemaList = new Framework.Entities.Flow.SchemaList();

					while (dr.Read())
					{
						schema = new Framework.Entities.Flow.Schema();
						schema.SetEntities(dr["ActivityID"].ToString()
									, dr["ParentActivityID"].ToString()
									, Convert.ToInt16(dr["Step"])
									, Convert.ToInt16(dr["SubStep"])
									, dr["Random"].ToString()
									, dr["Inline"].ToString()
									, dr["BizRole"].ToString()
									, dr["ActRole"].ToString()
									, dr["DisplayName"].ToString()
									, dr["Progress"].ToString()
									, dr["PartType"].ToString()
									, dr["ScopeLine"].ToString()
									, dr["Mandatory"].ToString()
									, Convert.ToInt32(dr["EvalChart"]));
						schemaList.Add(schema);
					}
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return schemaList;
		}

		/// <summary>
		/// 프로세스 단계 정의
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="processID"></param>
		/// <param name="parentActivityID"></param>
		/// <param name="step"></param>
		/// <param name="subStep"></param>
		/// <param name="random"></param>
		/// <param name="inLine"></param>
		/// <param name="bizRole"></param>
		/// <param name="actRole"></param>
		/// <param name="displayName"></param>
		/// <param name="progress"></param>
		/// <param name="partType"></param>
		/// <param name="limited"></param>
		/// <param name="review"></param>
		/// <param name="showLine"></param>
		/// <param name="scopeLine"></param>
		/// <param name="mandatory"></param>
		/// <param name="evalChart"></param>
		/// <param name="actPreCond"></param>
		/// <param name="actPostCond"></param>
		/// <param name="wiPreCond"></param>
		/// <param name="wiPostCond"></param>
		/// <param name="description"></param>
		public void InsertProcessActivity(string activityID, int processID, string parentActivityID, int step, int subStep, string random, string inLine, string bizRole
							, string actRole, string displayName, string progress, string partType, string limited, string review, string showLine, string scopeLine
							, string mandatory, int evalChart, string actPreCond, string actPostCond, string wiPreCond, string wiPostCond, string description)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@parent_activityid", SqlDbType.VarChar, 33, parentActivityID),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@random", SqlDbType.Char, 1, random),
				ParamSet.Add4Sql("@inline", SqlDbType.Char, 1, inLine),
				ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 100, displayName),
				ParamSet.Add4Sql("@progress", SqlDbType.VarChar, 10, progress),
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 5, partType),
				ParamSet.Add4Sql("@limited", SqlDbType.VarChar, 63, limited),
				ParamSet.Add4Sql("@review", SqlDbType.Char, 1, review),
				ParamSet.Add4Sql("@showline", SqlDbType.Char, 1, showLine),
				ParamSet.Add4Sql("@scopeline", SqlDbType.VarChar, 33, scopeLine),
				ParamSet.Add4Sql("@mandatory", SqlDbType.Char, 1, mandatory),
				ParamSet.Add4Sql("@evalchart", SqlDbType.Int, 4, evalChart),
				ParamSet.Add4Sql("@act_precond", SqlDbType.Char, 1, actPreCond),
				ParamSet.Add4Sql("@act_postcond", SqlDbType.Char, 1, actPostCond),
				ParamSet.Add4Sql("@wi_precond", SqlDbType.Char, 1, wiPreCond),
				ParamSet.Add4Sql("@wi_postcond", SqlDbType.Char, 1, wiPostCond),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description)
			};

			ParamData pData = new ParamData("admin.ph_up_BFInsertProcessActivity", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 단계 정보 변경
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="parentActivityID"></param>
		/// <param name="step"></param>
		/// <param name="subStep"></param>
		/// <param name="random"></param>
		/// <param name="inLine"></param>
		/// <param name="bizRole"></param>
		/// <param name="actRole"></param>
		/// <param name="displayName"></param>
		/// <param name="progress"></param>
		/// <param name="partType"></param>
		/// <param name="limited"></param>
		/// <param name="review"></param>
		/// <param name="showLine"></param>
		/// <param name="scopeLine"></param>
		/// <param name="mandatory"></param>
		/// <param name="evalChart"></param>
		/// <param name="actPreCond"></param>
		/// <param name="actPostCond"></param>
		/// <param name="wiPreCond"></param>
		/// <param name="wiPostCond"></param>
		/// <param name="description"></param>
		/// <returns></returns>
		public void UpdateProcessActivity(string activityID, string parentActivityID, int step, int subStep, string random, string inLine, string bizRole, string actRole
							, string displayName, string progress, string partType, string limited, string review, string showLine, string scopeLine, string mandatory
							, int evalChart, string actPreCond, string actPostCond, string wiPreCond, string wiPostCond, string description)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@parent_activityid", SqlDbType.VarChar, 33, parentActivityID),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@substep", SqlDbType.Int, 4, subStep),
				ParamSet.Add4Sql("@random", SqlDbType.Char, 1, random),
				ParamSet.Add4Sql("@inline", SqlDbType.Char, 1, inLine),
				ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 100, displayName),
				ParamSet.Add4Sql("@progress", SqlDbType.VarChar, 10, progress),
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 5, partType),
				ParamSet.Add4Sql("@limited", SqlDbType.VarChar, 63, limited),
				ParamSet.Add4Sql("@review", SqlDbType.Char, 1, review),
				ParamSet.Add4Sql("@showline", SqlDbType.Char, 1, showLine),
				ParamSet.Add4Sql("@scopeline", SqlDbType.VarChar, 33, scopeLine),
				ParamSet.Add4Sql("@mandatory", SqlDbType.Char, 1, mandatory),
				ParamSet.Add4Sql("@evalchart", SqlDbType.Int, 4, evalChart),
				ParamSet.Add4Sql("@act_precond", SqlDbType.Char, 1, actPreCond),
				ParamSet.Add4Sql("@act_postcond", SqlDbType.Char, 1, actPostCond),
				ParamSet.Add4Sql("@wi_precond", SqlDbType.Char, 1, wiPreCond),
				ParamSet.Add4Sql("@wi_postcond", SqlDbType.Char, 1, wiPostCond),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description)
			};

			ParamData pData = new ParamData("admin.ph_up_BFUpdateProcessActivity", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 단계에 참여자 정의
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="seq"></param>
		/// <param name="partID"></param>
		/// <param name="partName"></param>
		/// <param name="partDeptCode"></param>
		public void InsertProcessParticipant(string activityID, int seq, string partID, string partName, string partDeptCode)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@part_id", SqlDbType.VarChar, 63, partID),
				ParamSet.Add4Sql("@part_name", SqlDbType.NVarChar, 200, partName),
				ParamSet.Add4Sql("@part_deptcode", SqlDbType.VarChar, 63, partDeptCode)
			};

			ParamData pData = new ParamData("admin.ph_up_BFInsertProcessParticipant", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 단계에 참여자 정의 - 배치
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="xmlInfo"></param>
		public void InsertProcessParticipant(string activityID, string xmlInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@part_name", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BFInsertProcessParticipantBatch", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 단계에 정의된 참여자 삭제
		/// </summary>
		/// <param name="activityID"></param>
		public void RemoveProcessParticipant(string activityID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFRemoveProcessParticipant", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 정의의 순번/하위 순번을 변경
		/// </summary>
		/// <param name="stepJson"></param>
		/// <returns></returns>
		public int ChangeProcessActivityStep(string stepJson)
		{
			int iReturn = -1;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@stepjson", SqlDbType.NVarChar, stepJson)
			};

			ParamData pData = new ParamData("admin.ph_up_BFChangeProcessActivityStep", "", parameters);

			using (DbBase db = new DbBase())
			{
				string strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				iReturn = (string.IsNullOrWhiteSpace(strReturn)) ? 0 : -1;
			}

			return iReturn;
		}

		/// <summary>
		/// Activity 일반 정보 수정
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="displayName"></param>
		/// <param name="bizRole"></param>
		/// <param name="actRole"></param>
		/// <param name="review"></param>
		/// <param name="progress"></param>
		/// <param name="random"></param>
		/// <param name="showLine"></param>
		/// <param name="mandatory"></param>
		/// <returns></returns>
		public int UpdateProcessActivityGeneral(string activityID, string displayName, string bizRole, string actRole, string review, string progress, string random, string showLine, string mandatory)
		{
			int iReturn = -1;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 100, displayName),
				ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
				ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
				ParamSet.Add4Sql("@review", SqlDbType.Char, 1, review),
				ParamSet.Add4Sql("@progress", SqlDbType.VarChar, 10, progress),
				ParamSet.Add4Sql("@random", SqlDbType.Char, 1, random),
				ParamSet.Add4Sql("@showline", SqlDbType.Char, 1, showLine),
				ParamSet.Add4Sql("@mandatory", SqlDbType.Char, 1, mandatory)
			};

			ParamData pData = new ParamData("admin.ph_up_BFChangeProcessActivityGeneral", "", parameters);

			using (DbBase db = new DbBase())
			{
				string strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				iReturn = (string.IsNullOrWhiteSpace(strReturn)) ? 0 : -1;
			}

			return iReturn;
		}

		/// <summary>
		/// 특정 Activity에 미리 지정된 참여자 정보 가져오기
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="oID"></param>
		/// <param name="parentWID"></param>
		/// <param name="type"></param>
		/// <param name="subType"></param>
		/// <param name="subKey"></param>
		/// <param name="optionValue"></param>
		/// <returns></returns>
		public DataTable GetActivityParticipants(string activityID, int oID, string parentWID, string type, string subType, string subKey, string optionValue)
        {
			DataSet ds = null;
			DataTable dtReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@pwid", SqlDbType.VarChar, 33, parentWID),
				ParamSet.Add4Sql("@type", SqlDbType.Char, 1, type),
				ParamSet.Add4Sql("@subtype", SqlDbType.Char, 1, subType),
				ParamSet.Add4Sql("@subkey", SqlDbType.Char, 1, subKey),
				ParamSet.Add4Sql("@option", SqlDbType.NVarChar, 500, optionValue)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessActivityParticipants", parameters);

            try
            {
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables["ActivityParticipants"];
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
		/// Activity 참여 정보 조회
		/// </summary>
		/// <param name="activityID"></param>
		/// <param name="actType"></param>
		/// <param name="subType"></param>
		/// <returns></returns>
		public DataSet GetBFProcessActivityParticipantForAdmin(string activityID, string actType, string subType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@acttype", SqlDbType.Char, 1, actType),
				ParamSet.Add4Sql("@subtype", SqlDbType.Char, 1, subType)
			};

			ParamData pData = new ParamData("admin.ph_up_BFGetProcessActivityParticipantForAdmin", "", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
		#endregion

		#region [프로세스 속성 관련]
		/// <summary>
		/// 프로세스 인스턴스 속성 생성
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="activityID"></param>
		/// <param name="attribute"></param>
		/// <param name="display"></param>
		/// <param name="value"></param>
		/// <param name="dataType"></param>
		/// <param name="relFlag"></param>
		public void CreateProcessInstanceAttribute(int oID, string activityID, string attribute, string display, string value, string dataType, string relFlag)
        {
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@attribute", SqlDbType.VarChar, 50, attribute),
				ParamSet.Add4Sql("@display", SqlDbType.NText, display),
				ParamSet.Add4Sql("@value", SqlDbType.NText, value),
				ParamSet.Add4Sql("@datatype", SqlDbType.Char, 1, dataType),
				ParamSet.Add4Sql("@relflag", SqlDbType.Char, 1, relFlag)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessInstanceAttribute", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 인스턴스 속성 배치 생성
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="attInfo"></param>
		/// <returns></returns>
		public void CreateProcessInstanceAttributeBatch(int oID, string attInfo)
        {
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, attInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessInstanceAttributeBatch", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 인스턴스 속성 삭제
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="activityID"></param>
		public void DeleteProcessInstanceAttributes(int oID, string activityID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFDeleteProcessInstanceAttributes", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 인스턴스 속성 가져오기
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="activityID"></param>
		/// <returns></returns>
		public DataTable SelectProcessInstanceAttribute(int oID, string activityID)
		{
			DataSet ds = null;
			DataTable dtReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oID", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectProcessInstanceAttribute", parameters);

			try
			{
				using (DbBase db = new DbBase())
				{
					ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables["ActivityParticipants"];
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
		/// 프로세스 인스턴스 속성 가져오기
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="activityID"></param>
		/// <param name="attributeValue"></param>
		/// <returns></returns>
		public string SelectProcessInstanceAttribute(int oID, string activityID, out string attributeValue)
		{
			SqlDataReader dr = null;
			string strOut = "";
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oID", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectProcessInstanceAttribute", parameters);

            try
            {
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					while (dr.Read())
					{
						strOut = dr["attribute"].ToString();
						strReturn = dr["Value"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			attributeValue = strOut;
			return strReturn;
		}

		/// <summary>
		/// 프로세스 속성 정보들을 가져온다.
		/// </summary>
		/// <param name="attributeID"></param>
		/// <param name="processID"></param>
		/// <param name="activityID"></param>
		/// <returns></returns>
		public Framework.Entities.Flow.Attributes SelectProcessAttribute(int attributeID, int processID, string activityID)
        {
			SqlDataReader dr = null;
			Framework.Entities.Flow.Attributes attList = null;
			Framework.Entities.Flow.Attribute att = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@attributeid", SqlDbType.Int, 4, attributeID),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectProcessAttribute", parameters);

			try
			{
				using (DbBase db = new DbBase())
				{
					dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				}

				if (dr.HasRows)
				{
					attList = new Framework.Entities.Flow.Attributes();

					while (dr.Read())
					{
						att = new Framework.Entities.Flow.Attribute();

						att.AttributeID = Convert.ToInt32(dr["AttributeID"]);
						att.ProcessID = Convert.ToInt32(dr["ProcessID"]);
						att.Pos = dr["Pos"].ToString();
						att.ActivityID = dr["ActivityID"].ToString();
						att.Condition = dr["Condition"].ToString();
						att.Attribute1 = dr["Attribute"].ToString();
						att.Status = Convert.ToInt32(dr["Status"]);
						att.Description = dr["Description"].ToString();
						att.Item1 = dr["Item1"].ToString();
						att.Item2 = dr["Item2"].ToString();
						att.Item3 = dr["Item3"].ToString();
						att.Item4 = dr["Item4"].ToString();
						att.Item5 = dr["Item5"].ToString();

						attList.Add(att);
					
					}
					dr.Close();
				}
			}
			catch (Exception ex)
			{
				Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}
			finally
			{
				if (dr != null) dr.Dispose();
			}

			return attList;
		}

		/// <summary>
		/// Activity 속성 정보 조회
		/// </summary>
		/// <param name="attributeID"></param>
		/// <param name="processID"></param>
		/// <param name="activityID"></param>
		/// <returns></returns>
		public DataSet GetProcessAttribute(int attributeID, int processID, string activityID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@attributeid", SqlDbType.Int, 4, attributeID),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectProcessAttribute", "", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 프로세스 속성 생성
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="pos"></param>
		/// <param name="activityID"></param>
		/// <param name="condition"></param>
		/// <param name="attribute"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <param name="item3"></param>
		/// <param name="item4"></param>
		/// <param name="item5"></param>
		public void CreateProcessAttribute(int processID, string pos, string activityID, string condition, string attribute, int status
										, string description, string item1, string item2, string item3, string item4, string item5)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@pos", SqlDbType.Char, 1, pos),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@condition", SqlDbType.VarChar, 5, condition),
				ParamSet.Add4Sql("@attribute", SqlDbType.VarChar, 20, attribute),
				ParamSet.Add4Sql("@status", SqlDbType.Int, 4, status),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 200, description),
				ParamSet.Add4Sql("@item1", SqlDbType.NVarChar, 200, item1),
				ParamSet.Add4Sql("@item2", SqlDbType.NVarChar, 200, item2),
				ParamSet.Add4Sql("@item3", SqlDbType.NVarChar, 200, item3),
				ParamSet.Add4Sql("@item4", SqlDbType.NVarChar, 200, item4),
				ParamSet.Add4Sql("@item5", SqlDbType.NVarChar, 200, item5)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessAttribute", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 속성 삭제
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="targetID"></param>
		public void DeleteProcessAttribute(string mode, string targetID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@targetid", SqlDbType.VarChar, 33, targetID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFDeleteProcessAttribute", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 속성 변경 (processid는 변경 속성은 아니다)
		/// </summary>
		/// <param name="attributeID"></param>
		/// <param name="pos"></param>
		/// <param name="activityID"></param>
		/// <param name="condition"></param>
		/// <param name="attribute"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <param name="item3"></param>
		/// <param name="item4"></param>
		/// <param name="item5"></param>
		public void UpdateProcessAttribute(int attributeID, string pos, string activityID, string condition, string attribute, int status
										, string description, string item1, string item2, string item3, string item4, string item5)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@attributeid", SqlDbType.Int, 4, attributeID),
				ParamSet.Add4Sql("@pos", SqlDbType.Char, 1, pos),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@condition", SqlDbType.VarChar, 5, condition),
				ParamSet.Add4Sql("@attribute", SqlDbType.VarChar, 20, attribute),
				ParamSet.Add4Sql("@status", SqlDbType.Int, 4, status),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 200, description),
				ParamSet.Add4Sql("@item1", SqlDbType.NVarChar, 200, item1),
				ParamSet.Add4Sql("@item2", SqlDbType.NVarChar, 200, item2),
				ParamSet.Add4Sql("@item3", SqlDbType.NVarChar, 200, item3),
				ParamSet.Add4Sql("@item4", SqlDbType.NVarChar, 200, item4),
				ParamSet.Add4Sql("@item5", SqlDbType.NVarChar, 200, item5)
			};

			ParamData pData = new ParamData("admin.ph_up_BFUpdateProcessAttribute", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
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
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetListPerMenu(string mode, string admin, string formId, int defId, int viewer, int state, int page, int count
								, string baseSortCol, string sortCol, string sortType, string searchCol, string searchText, string searchDate, out int totalMessage)
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
				totalMessage = Convert.ToInt32(pData.GetParamValue("@total_cnt").ToString());
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

		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="defID"></param>
		/// <param name="domainID"></param>
		/// <param name="processName"></param>
		/// <param name="defXml"></param>
		/// <param name="actXml"></param>
		/// <param name="attXml"></param>
		/// <param name="partXml"></param>
		/// <returns></returns>
		public int CreateBFProcessWithImport(string mode, int defID, int domainID, string processName, string defXml, string actXml, string attXml, string partXml)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@defid", SqlDbType.Int, 4, defID),
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 200, processName),
				ParamSet.Add4Sql("@def", SqlDbType.NVarChar, -1, defXml),
				ParamSet.Add4Sql("@act", SqlDbType.NVarChar, -1, actXml),
				ParamSet.Add4Sql("@att", SqlDbType.NVarChar, -1, attXml),
				ParamSet.Add4Sql("@part", SqlDbType.NVarChar, -1, partXml)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcessWithImport", "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return iReturn;
		}

		/// <summary>
		/// 회사 서비스 모드 가져오기
		/// </summary>
		/// <param name="companyCode"></param>
		/// <returns></returns>
		public DataSet GetBFServiceCompanyDetail(string companyCode)
		{
			DataSet dsReturn = null;
			string strQuery = "SELECT BFFlowPreAppMode, BFFlowSvcMode, BFFlowSvcInterval FROM admin.PH_SERVICE_COMPANY_DETAIL (NOLOCK) WHERE CompanyCode = @companycode";
			
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companyCode)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 전체 사용자 서비스 모드 설정 가져오기
		/// </summary>
		/// <returns></returns>
		public DataSet GetBFServiceMemberDetail()
		{
			DataSet dsReturn = null;
			string strQuery = @"SELECT UserID, EmpID, DisplayName, Grade1, GroupName, FlowPreAppMode, FlowSvcMode, FlowSvcInterval FROM admin.ph_view_OBJECT_UR_LIST (NOLOCK) WHERE GRType='D' AND Role='regular' ORDER BY FlowPreAppMode DESC, FlowSvcMode DESC, GroupName, DisplayName";

			SqlParameter[] parameters = new SqlParameter[]
			{
				
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 서비스/선결 모드 변경
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="svcMode"></param>
		/// <param name="interval"></param>
		/// <param name="preAppMode"></param>
		/// <returns></returns>
		public int ChangeUserServiceMode(int userID, string svcMode, int interval, string preAppMode)
		{
			int iReturn = 0;
			string strQuery = "UPDATE admin.PH_USER_CONFIGURATION WITH (ROWLOCK) SET UseBFFlowPreAppMode=@preapp, UseBFFlowSvcMode=@svcmode, BFFlowSvcInterval=@svcinterval WHERE UserID=@urId";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@urId", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@svcmode", SqlDbType.Char, 1, svcMode),
				ParamSet.Add4Sql("@svcinterval", SqlDbType.TinyInt, 4, interval),
				ParamSet.Add4Sql("@preapp", SqlDbType.Char, 1, preAppMode)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return iReturn;
		}
	

		

		/// <summary>
		/// 특정 프로세스에 해당하는 양식들 가져오기
		/// </summary>
		/// <param name="defID"></param>
		/// <returns></returns>
		public DataSet GetBFProcessFormList(int defID)
		{
			DataSet dsReturn = null;
			string strQuery = @"SELECT ISNULL(b.ClassName,'') AS ClassName, a.FormID, a.DocName, a.MainTable
					FROM admin.PH_EA_FORMS a (NOLOCK) LEFT OUTER JOIN admin.PH_EA_CLASS b (NOLOCK) ON a.ClassID = b.ClassID
					WHERE ProcessID = @defId ORDER BY a.DocName";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@defId", SqlDbType.Int, 4, defID)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="processID"></param>
		/// <param name="activityID"></param>
		/// <returns></returns>
		public int DeleteProcessActivity(string mode, int processID, string activityID)
		{
			int iReturn = -1;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFDeleteProcessActivity", "", parameters);

			using (DbBase db = new DbBase())
			{
				string strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				iReturn = (string.IsNullOrWhiteSpace(strReturn)) ? 0 : -1;
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="activityID"></param>
		/// <param name="xmlInfo"></param>
		/// <returns></returns>
		public int InsertProcessAttributeBatch(int processID, string activityID, string xmlInfo)
		{
			int iReturn = -1;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@activityid", SqlDbType.VarChar, 33, activityID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BFCreateProcesAttributeBatch", "", parameters);

			using (DbBase db = new DbBase())
			{
				string strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				iReturn = (string.IsNullOrWhiteSpace(strReturn)) ? 0 : -1;
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="updateText"></param>
		/// <param name="whereText"></param>
		/// <returns></returns>
		public int ChangeActivityField(string updateText, string whereText)
		{
			int iReturn = -1;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@updatetext", SqlDbType.VarChar, 200, updateText),
				ParamSet.Add4Sql("@wheretext", SqlDbType.VarChar, 200, whereText)
			};

			ParamData pData = new ParamData("admin.ph_up_BFChangeActivityField", "", parameters);

			using (DbBase db = new DbBase())
			{
				string strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				iReturn = (string.IsNullOrWhiteSpace(strReturn)) ? 0 : -1;
			}

			return iReturn;
		}

		

		

		
	}
}
