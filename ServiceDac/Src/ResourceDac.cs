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
	public class ResourceDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ResourceDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public ResourceDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 일정 참여자 가져오기
		/// </summary>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetScheduleParticipants(int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetParticipants", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 선택한 자원 사용 가능 여부 판단
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="messageID"></param>
		/// <param name="resource"></param>
		/// <param name="resType"></param>
		/// <param name="resApp"></param>
		/// <param name="rangeSDate"></param>
		/// <param name="rangeEDate"></param>
		/// <param name="rangeSTime"></param>
		/// <param name="rangeETime"></param>
		/// <returns>bUsable</returns>
		public string CheckScheduleUsingResource(int domainID, int messageID, int resource, string resType, string resApp
									, string rangeSDate, string rangeEDate, string rangeSTime, string rangeETime)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@resource", SqlDbType.Int, 4, resource),
				ParamSet.Add4Sql("@resType", SqlDbType.Char, 2, resType),
				ParamSet.Add4Sql("@resApp", SqlDbType.Char, 1, resApp),
				ParamSet.Add4Sql("@rangeSDate", SqlDbType.Char, 10, rangeSDate),
				ParamSet.Add4Sql("@rangeEDate", SqlDbType.Char, 10, rangeEDate),
				ParamSet.Add4Sql("@rangeSTime", SqlDbType.Char, 5, rangeSTime),
				ParamSet.Add4Sql("@rangeETime", SqlDbType.Char, 5, rangeETime),
				ParamSet.Add4Sql("@bUsable", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleCheckUsingResource", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//bUsable = pData.GetParamValue("@bUsable").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 일정 자원 정보 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="partID"></param>
		/// <param name="partType"></param>
		/// <returns></returns>
		public DataSet GetScheduleResourceInfo(int domainID, int partID, string partType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@part_id", SqlDbType.Int, 4, partID),
				ParamSet.Add4Sql("@partType", SqlDbType.VarChar, 30, partType)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetResourceInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 참여자의 확인
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="partID"></param>
		/// <param name="objectType"></param>
		/// <param name="state"></param>
		public void ConfirmScheduleByParticipant(int messageID, int partID, string objectType, int state)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@part_id", SqlDbType.Int, 4, partID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleConfirmedByParticipant", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 작업 영역 만들기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="messageID"></param>
		/// <param name="actor"></param>
		public void CreateScheduleWorkArea(int domainID, int messageID, int actor)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actor)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleCreateWorkArea", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 일정 참여자 정보 생성,변경,삭제
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="messageID"></param>
		/// <param name="objectType"></param>
		/// <param name="partID"></param>
		/// <param name="partType"></param>
		/// <param name="sendMail"></param>
		/// <param name="note"></param>
		/// <param name="state"></param>
		/// <param name="confirmed"></param>
		public void SetScheduleParticipant(string mode, int messageID, string objectType, int partID, string partType
								, string sendMail, string note, int state, string confirmed)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
				ParamSet.Add4Sql("@part_id", SqlDbType.Int, 4, partID),
				ParamSet.Add4Sql("@part_type", SqlDbType.Char, 1, partType),
				ParamSet.Add4Sql("@sendmail", SqlDbType.Char, 1, sendMail),
				ParamSet.Add4Sql("@note", SqlDbType.NVarChar, 255, note),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 1, state),
				ParamSet.Add4Sql("@confirmed", SqlDbType.Char, 1, confirmed)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleSetParticipant", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 일정 참여자 정보 일괄 작업 (전체 삭제 후 생성)
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="messageID"></param>
		/// <param name="xmlData"></param>
		public void SetScheduleParticipant(string mode, int messageID, string xmlData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleSetParticipants", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
	}
}
