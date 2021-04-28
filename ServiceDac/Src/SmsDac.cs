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
	public class SmsDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public SmsDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public SmsDac(SqlConnection connection) : base(connection)
		{

		}

		#region SMS 메시지 관리
		/// <summary>
		/// SMS  Message 발송 기록 / 예약 등록
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="userName"></param>
		/// <param name="logonID"></param>
		/// <param name="deptAlias"></param>
		/// <param name="deptName"></param>
		/// <param name="sendDate"></param>
		/// <param name="reservedDate"></param>
		/// <param name="reciverPhone"></param>
		/// <param name="callbackPhone"></param>
		/// <param name="senderPhone"></param>
		/// <param name="message"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public int SetSmsMessage(int userID, string userName, string logonID, string deptAlias, string deptName, DateTime sendDate, DateTime reservedDate, string reciverPhone, string callbackPhone, string senderPhone, string message, string state, string cmpID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@username", SqlDbType.NVarChar, 30, userName),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 10, logonID),
				ParamSet.Add4Sql("@deptalias", SqlDbType.VarChar, 3, deptAlias),
				ParamSet.Add4Sql("@deptname", SqlDbType.NVarChar, 30, deptName),
				ParamSet.Add4Sql("@senddate", SqlDbType.DateTime, sendDate),
				ParamSet.Add4Sql("@reseveddate", SqlDbType.DateTime, reservedDate),
				ParamSet.Add4Sql("@reciverphone", SqlDbType.VarChar, 11, reciverPhone),
				ParamSet.Add4Sql("@callbackphone", SqlDbType.VarChar, 11, callbackPhone),
				ParamSet.Add4Sql("@senderphone", SqlDbType.VarChar, 11, senderPhone),
				ParamSet.Add4Sql("@message", SqlDbType.NVarChar, 80, message),
				ParamSet.Add4Sql("@state", SqlDbType.VarChar, 10, state),
				ParamSet.Add4Sql("@cmpid", SqlDbType.VarChar, 5, cmpID)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsSetSmsMessage", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 메시지 전송 등록
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="userName"></param>
		/// <param name="logonID"></param>
		/// <param name="deptAlias"></param>
		/// <param name="deptName"></param>
		/// <param name="sendDate"></param>
		/// <param name="reservedDate"></param>
		/// <param name="reciverPhone"></param>
		/// <param name="callbackPhone"></param>
		/// <param name="senderPhone"></param>
		/// <param name="message"></param>
		/// <param name="state"></param>
		/// <param name="cmpID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int SetSmsMessage(int userID, string userName, string logonID, string deptAlias, string deptName, DateTime sendDate, DateTime reservedDate, string reciverPhone, string callbackPhone, string senderPhone, string message, string state, string cmpID, out int returnMessageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@username", SqlDbType.NVarChar, 30, userName),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 10, logonID),
				ParamSet.Add4Sql("@deptalias", SqlDbType.VarChar, 3, deptAlias),
				ParamSet.Add4Sql("@deptname", SqlDbType.NVarChar, 30, deptName),
				ParamSet.Add4Sql("@senddate", SqlDbType.DateTime, sendDate),
				ParamSet.Add4Sql("@reseveddate", SqlDbType.DateTime, reservedDate),
				ParamSet.Add4Sql("@reciverphone", SqlDbType.VarChar, 11, reciverPhone),
				ParamSet.Add4Sql("@callbackphone", SqlDbType.VarChar, 11, callbackPhone),
				ParamSet.Add4Sql("@senderphone", SqlDbType.VarChar, 11, senderPhone),
				ParamSet.Add4Sql("@message", SqlDbType.NVarChar, 80, message),
				ParamSet.Add4Sql("@state", SqlDbType.VarChar, 10, state),
				ParamSet.Add4Sql("@cmpid", SqlDbType.VarChar, 5, cmpID),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 2000, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsRegMessage", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = int.Parse(pData.GetParamValue("@messageid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// SMS 메시지 상태 변경
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public int ChangeSmsMessageState(int messageID, string state)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@state", SqlDbType.VarChar, 10, state)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsChangeSmsState", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// SMS (발송/등록) 목록
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="mode"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="inUse"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetSmsMessageList(int userID, string mode, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 10, mode),
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

			ParamData pData = new ParamData("admin.ph_up_SmsGetMessageList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// SMS 발송 예약 목록
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public DataSet GetSmsReservedMessageList(int userID, string mode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 10, mode)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsGetReservedMessageList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#endregion

		#region SMS 한도 관리

		/// <summary>
		/// SMS 한도 설정
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="size"></param>
		/// <param name="setMode"></param>
		/// <returns></returns>
		public int SetSmsCapacity(int userID, string size, string setMode)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@size", SqlDbType.VarChar, 30, size),
				ParamSet.Add4Sql("@setmode", SqlDbType.VarChar, 10, setMode)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsSetCapacity", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 사용자 SMS 한도 정보
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public DataSet GetSmsUserCapacity(int userID, int size)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@size", SqlDbType.Int, 4, size)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsGetUserCapacity", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#endregion

		#region SMS 서버 관리

		/// <summary>
		///  SMS 서버 등록 / 수정
		/// </summary>
		/// <param name="serverID"></param>
		/// <param name="serverIP"></param>
		/// <param name="port"></param>
		/// <param name="priority"></param>
		/// <param name="inUse"></param>
		/// <returns></returns>
		public int SetSmsServer(int serverID, string serverIP, int port, int priority, string inUse)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@serverid", SqlDbType.Int, 4, serverID),
				ParamSet.Add4Sql("@serverip", SqlDbType.VarChar, 15, serverIP),
				ParamSet.Add4Sql("@port", SqlDbType.Int, 4, port),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@inuse", SqlDbType.VarChar, 1, inUse)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsSetServerIP", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// SMS 서비스 등록 서버 제거
		/// </summary>
		/// <param name="serverID"></param>
		/// <returns></returns>
		public int DeleteSmsServerIP(int serverID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@serverid", SqlDbType.Int, 4, serverID)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsDeleteServerIP", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// SMS 서비스 서버IP 목록
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		public DataSet GetSmsServerIP(string mode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 1, mode)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsGetServerIP", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#endregion

		/// <summary>
		/// 사용자 핸드폰 번호 가져오기
		/// </summary>
		/// <param name="logonID"></param>
		/// <returns></returns>
		public int GetUserMobile(string logonID, out string mobileNumber)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 10, logonID),
				ParamSet.Add4Sql("@mobile", SqlDbType.VarChar, 15, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsGetMobile", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				mobileNumber = pData.GetParamValue("@mobile").ToString();
			}

			return iReturn;
		}
	}
}
