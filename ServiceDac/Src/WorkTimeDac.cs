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
	public class WorkTimeDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public WorkTimeDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public WorkTimeDac(SqlConnection connection) : base(connection)
		{

		}

        #region [부서장 권한 관련]
        /// <summary>
        /// 부서장 권한 여부 확인
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string CheckChiefAcl(int userId)
        {
            string strReturn = "";
            string strQuery = "SELECT CASE WHEN Reserved1 = 'Y' THEN 'Y' ELSE 'N' END FROM admin.PH_USER_DETAIL (NOLOCK) WHERE UserID = @userid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 권한 정보 가져오기
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetToDoAcl(int userId)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, ""),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetAcl", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [근무시간, 휴일 정보 가져오기]
        /// <summary>
        /// 근무일, 최소/최대/최대연장 근무시간 가져오기
        /// </summary>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetMonthWorkHour(string viewDate)
        {
            DataSet dsReturn = null;
            string strQuery = "SELECT * FROM admin.ph_fn_GetMonthWorkHour(@viewdate)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 월 휴일 가져오기
        /// </summary>
        /// <param name="reqMonth"></param>
        /// <returns></returns>
        public DataSet GetMonthHoliday(string reqMonth)
        {
            DataSet dsReturn = null;
            string strQuery = "SELECT ItemSubHandler, Item1, Item2 FROM admin.PH_CODE_DESCRIPTION (NOLOCK) WHERE ItemKey='system' AND ItemSubKey='holiday' AND ItemSubHandler LIKE '" + reqMonth + "%'";

            SqlParameter[] parameters = null;

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [근무 상태 이벤트 관련]
        /// <summary>
        /// 현 근무 상태 파악
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <param name="ip"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        public string CheckWorkTimeStatus(string mode, int userId, string workDate, string ip, string browser)
        {
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate),
                ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 20, ip),
                ParamSet.Add4Sql("@browser", SqlDbType.VarChar, 30, browser)
            };

            ParamData pData = new ParamData("admin.ph_up_CheckWorkTimeStatus", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 현 근무 상태 파악
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeStatus(int userId, string workDate)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, "Y"),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate)
            };

            ParamData pData = new ParamData("admin.ph_up_CheckWorkTimeStatus", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 현 근무시간 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeStatus(string mode, int userId, string workDate)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate),
                ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 20, ""),
                ParamSet.Add4Sql("@browser", SqlDbType.VarChar, 30, "")
            };

            ParamData pData = new ParamData("admin.ph_up_CheckWorkTimeStatus", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 근무조정 요청 사항 조회
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <param name="reqId"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeStatus(int userId, string workDate, int reqId)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, "R"),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate),
                ParamSet.Add4Sql("@reqid", SqlDbType.Int, 4, reqId)
            };

            ParamData pData = new ParamData("admin.ph_up_CheckWorkTimeStatus", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }
        #endregion
    }
}
