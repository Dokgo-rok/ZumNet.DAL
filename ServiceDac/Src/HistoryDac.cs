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
	public class HistoryDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public HistoryDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public HistoryDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 문서 보존년한
		/// </summary>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public DataSet GetDocKeepYear(int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetDocKeepYear", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서 등급
		/// </summary>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public DataSet GetDocLevel(int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetDocLevel", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 자원 폴더 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetScheduleResource(int domainID, int groupID, int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetResource", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서의 체크아웃 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="messageID"></param>
		/// <param name="attachID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocCheckOutInfo(int domainID, int messageID, int attachID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, attachID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetCheckOutInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 체크아웃 정보 기록
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="attachID"></param>
		/// <param name="coUserID"></param>
		/// <param name="coExpectedDate"></param>
		/// <param name="coReason"></param>
		public void CreateDocCheckOutInfo(int messageID, int attachID, int coUserID, string coExpectedDate, string coReason)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, attachID),
				ParamSet.Add4Sql("@couserID", SqlDbType.Int, 4, coUserID),
				ParamSet.Add4Sql("@coexpecteddate", SqlDbType.VarChar, 10, coExpectedDate),
				ParamSet.Add4Sql("@coreason", SqlDbType.NVarChar, 200, coReason)
			};

			ParamData pData = new ParamData("admin.ph_up_DocSetCheckOutInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 체크인 정보 기록
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="attachID"></param>
		/// <param name="parentAttachID"></param>
		/// <param name="groupID"></param>
		/// <param name="regUserID"></param>
		/// <param name="isChange"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="fileName"></param>
		/// <param name="savedName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="prefix"></param>
		/// <param name="location"></param>
		/// <param name="autoDeleted"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		public void CreateDocCheckInInfo(int domainID, int attachID, int parentAttachID, int groupID, int regUserID, string isChange, string xfAlias, int messageID
					, string fileName, string savedName, string fileSize, string fileType, string prefix, string location, string autoDeleted, int docLevel, int keepYear)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, attachID),
				ParamSet.Add4Sql("@parentattachID", SqlDbType.Int, 4, parentAttachID),
				ParamSet.Add4Sql("@groupID", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@reguserID", SqlDbType.Int, 4, regUserID),
				ParamSet.Add4Sql("@ischange", SqlDbType.Char, 1, isChange),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@filename", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@savedname", SqlDbType.VarChar, 100, savedName),
				ParamSet.Add4Sql("@filesize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@filetype", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 15, location),
				ParamSet.Add4Sql("@autodeleted", SqlDbType.Char, 1, autoDeleted),
				ParamSet.Add4Sql("@doclevel", SqlDbType.TinyInt, 1, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.SmallInt, 2, keepYear)
			};

			ParamData pData = new ParamData("admin.ph_up_DocSetCheckInInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 첨부 파일의 버전 정보 변경
		/// </summary>
		/// <param name="oldAttachID"></param>
		/// <param name="newAttachID"></param>
		public void ChangeVersionInfo(int oldAttachID, int newAttachID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oldattachID", SqlDbType.Int, 4, oldAttachID),
				ParamSet.Add4Sql("@newattachID", SqlDbType.Int, 4, newAttachID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocChangeVersionInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 버전 정보
		/// </summary>
		/// <param name="attachID"></param>
		/// <returns></returns>
		public DataSet GetDocVersionInfo(int attachID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, attachID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetVersionInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 파일 반출입의 로그 정보
		/// </summary>
		/// <param name="attachID"></param>
		/// <returns></returns>
		public DataSet GetDocCheckInOutLogInfo(int attachID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, attachID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetCheckInOutLog", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 방문 회수 및 시간 기록(동호회 방문회수)
		/// </summary>
		/// <param name="grID"></param>
		/// <param name="userID"></param>
		public void CreateGroupVisitCount(int grID, int userID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, grID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_VisitEventWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 관리툴에서의 문서 반입 상태 변경
		/// </summary>
		/// <param name="strCheckInInfo"></param>
		public void ChangeAdminCheckInDoc(string strCheckInInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@checkin_info", SqlDbType.NText, strCheckInInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeCheckInDoc", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
	}
}
