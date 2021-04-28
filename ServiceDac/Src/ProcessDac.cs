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
		/// WorkItem 생성
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="isGroup"></param>
		/// <param name="priority"></param>
		/// <param name="step"></param>
		/// <param name="seq"></param>
		/// <param name="state"></param>
		/// <param name="signStatus"></param>
		/// <param name="role"></param>
		/// <param name="participantID"></param>
		/// <param name="participantName"></param>
		/// <param name="partType"></param>
		/// <param name="receivedDate"></param>
		/// <param name="competency"></param>
		/// <param name="workID"></param>
		/// <returns></returns>
		public int CreateProcessWorkItem(int oID, string isGroup, int priority, int step, int seq, int state, int signStatus, string role, string participantID, string participantName, string partType, DateTime receivedDate, int competency, out int workID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@isgroup", SqlDbType.Char, 1, isGroup),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@participantrole", SqlDbType.VarChar, 30, role),
				ParamSet.Add4Sql("@participantid", SqlDbType.VarChar, 63, participantID),
				ParamSet.Add4Sql("@participantname", SqlDbType.NVarChar, 63, participantName),
				ParamSet.Add4Sql("@parttype", SqlDbType.Char, 1, partType),
				ParamSet.Add4Sql("@createdate", SqlDbType.DateTime, receivedDate),
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competency),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessWorkItemCreate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				workID = int.Parse(pData.GetParamValue("@wid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 결재정보 생성
		/// </summary>
		/// <param name="oID"></param>
		/// <param name="xmlData"></param>
		/// <returns></returns>
		public int CreateProcessSignInfo(int oID, string xmlData)
		{
			int iReturn = 0;
			
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),
				ParamSet.Add4Sql("@signInform", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessSignInformCreate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 인스턴스 생성
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="xmlData"></param>
		/// <param name="oID"></param>
		/// <returns></returns>
		public int CreateProcessInstance(int messageID, string xmlData, out int oID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@processins", SqlDbType.NText, xmlData),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessInstanceCreate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oID = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// WorkItem State 변경
		/// </summary>
		/// <param name="workitemID"></param>
		/// <param name="state"></param>
		/// <param name="signStatus"></param>
		/// <param name="point"></param>
		/// <param name="completedDate"></param>
		/// <param name="comment"></param>
		/// <returns></returns>
		public int ModifyWorkItemState(int workitemID, int state, int signStatus, decimal point, DateTime completedDate, string comment)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@workitemid", SqlDbType.Int, 4, workitemID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@point", SqlDbType.Decimal, point),
				ParamSet.Add4Sql("@completeddate", SqlDbType.DateTime, completedDate),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 500, comment)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessWorkItemState", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 인스턴스의 상태 변경
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public int ModifyProcessInstanceState(int processID, int state)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessInstanceState", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 중단[거절]
		/// </summary>
		/// <param name="processID"></param>
		public int RejectProcessWorkItem(int processID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessWorkItemRejectAll", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 버전정보 변경
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="workitemID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="role"></param>
		/// <param name="signStatus"></param>
		/// <param name="point"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public int ModifyProcessState(int processID, int workitemID, int messageID, string xfAlias, string role, int signStatus, decimal point, out int state)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@pid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, workitemID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@role", SqlDbType.VarChar, 30, role),
				ParamSet.Add4Sql("@signstatus", SqlDbType.Int, 4, signStatus),
				ParamSet.Add4Sql("@point", SqlDbType.Decimal, point),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, ParameterDirection.Output),
				ParamSet.Add4Sql("@postcond", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessMsgVersionMgr", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				state = int.Parse(pData.GetParamValue("@state").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// WorkItem 에러
		/// </summary>
		/// <param name="workitemID"></param>
		/// <returns></returns>
		public int ModifyStateRaisedErrorWorkItem(string workitemID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@workitemid", SqlDbType.Int, 4, workitemID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessWorkItemErrorState", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// WorkItem 삭제
		/// </summary>
		/// <param name="workitemID"></param>
		/// <returns></returns>
		public int RemoveWorkItemInfo(int workitemID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@workitemid", SqlDbType.Int, 4, workitemID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessWorkItemDelete", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 버전 생성
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="messageID"></param>
		/// <param name="role"></param>
		/// <param name="xfAlias"></param>
		/// <param name="state"></param>
		/// <param name="isLast"></param>
		/// <returns></returns>
		public int CreateProcessVersion(int processID, int messageID, string role, string xfAlias, int state, string isLast)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@pid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@role", SqlDbType.VarChar, 30, role),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@islastapp", SqlDbType.Char, 1, isLast)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessMsgCreateVersion", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 결재 정보 알아오기
		/// </summary>
		/// <param name="oID"></param>
		/// <returns></returns>
		public string GetProcessSignInfo(int oID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetSignInform", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn?.Tables?[0]?.Rows?[0][0].ToString() ?? "";
		}

		/// <summary>
		/// 프로세스 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetProcessListByFolderID(int domainID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetListByFolderID", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 프로세스 정보
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public DataSet GetProcessDetailInfo(int processID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetDefinition", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 프로세스 워크아이템 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="creatorID"></param>
		/// <param name="piState"></param>
		/// <param name="state"></param>
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
		public DataSet GetProcessGetEvalList(int domainID, string xfAlias, int participantID, int piState, int state, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@participantID", SqlDbType.Int, 4, participantID),
				ParamSet.Add4Sql("@piState", SqlDbType.Int, 4, piState),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetEvalMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 프로세스 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="admin"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetProcessList(int domainID, int pageIndex, int pageCount, string admin, string sortColumn, string sortType
		, string searchColumn, string searchText, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, admin),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}
	}
}
