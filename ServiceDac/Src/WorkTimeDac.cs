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
                ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userId)
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
        /// 현 근무 상태 파악
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <param name="ip"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        public DataSet CheckWorkTimeStatus(string mode, int userId, string workDate, string ip, string browser)
        {
            DataSet ds = null;

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
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 현 근무 상태 파악
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <returns></returns>
        public DataSet CheckWorkTimeStatus(int userId, string workDate)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, "V"),
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
        /// 근무조정 요청 사항 조회
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <param name="reqId"></param>
        /// <returns></returns>
        public DataSet CheckWorkTimeStatus(int userId, string workDate, int reqId)
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

        ///// <summary>
        ///// 현 근무 상태 파악(IP정보)
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="workDate"></param>
        ///// <param name="ip"></param>
        ///// <param name="browser"></param>
        ///// <returns></returns>
        //public Dictionary<string, string> CheckWorkTimeStatus(int userId, string workDate, string ip, string browser)
        //{
        //    Dictionary<string, string> dicReturn = new Dictionary<string, string>();
        //    DataSet ds = null;

        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, ""),
        //        ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
        //        ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate),
        //        ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 20, ip),
        //        ParamSet.Add4Sql("@browser", SqlDbType.VarChar, 30, browser)
        //    };

        //    ParamData pData = new ParamData("admin.ph_up_CheckWorkTimeStatus", parameters);

        //    using (DbBase db = new DbBase())
        //    {
        //        ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
        //    }

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow dr = ds.Tables[0].Rows[0];

        //        foreach (DataColumn col in ds.Tables[0].Columns)
        //        {
        //            dicReturn.Add(col.ColumnName, dr[col.ColumnName].ToString());
        //        }
        //    }

        //    return dicReturn;
        //}

        /// <summary>
        /// 근무형태에 따른 시간 기록
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <param name="status"></param>
        /// <param name="ip"></param>
        /// <param name="browser"></param>
        public void InsertWorkTimeStatus(int userId, string workDate, string status, string ip, string browser)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate),
                ParamSet.Add4Sql("@status", SqlDbType.Char, 1, status),
                ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 20, ip),
                ParamSet.Add4Sql("@browser", SqlDbType.VarChar, 30, browser)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertWorkTimeStatus", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [근무 집계 관련]
        /// <summary>
        /// 월 일일집계 현황 가져오기
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeMonth(int userId, string from, string to)
        {
            DataSet dsReturn = null;
            string strQuery = @"
SELECT WorkDate, HolidayType, WorkType, InTime, OutTime, InType, OutType, BizTrip, Leave, OutWork, OutLcm, HolidayWork, TotalWork, RealWork, NightWork
FROM admin.PH_WORKTIME_DAILY (NOLOCK)
WHERE UserID = @userid AND WorkDate >= @from AND WorkDate <= @to
    AND HolidayType = 'N'
ORDER BY WorkDate
";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@from", SqlDbType.Char, 10, from),
                ParamSet.Add4Sql("@to", SqlDbType.Char, 10, to)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 월 총 근무시간 가져오기
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public string GetWorkTimeMonthTotal(int userId, string from, string to)
        {
            string strReturn = "";
            string strQuery = @"
SELECT SUM(RealWork)
FROM admin.PH_WORKTIME_DAILY (NOLOCK)
WHERE UserID = @userid AND WorkDate >= @from AND WorkDate <= @to
";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@from", SqlDbType.Char, 10, from),
                ParamSet.Add4Sql("@to", SqlDbType.Char, 10, to)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 월 총 근무시간 가져오기
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeMonthTotal(int targetId, string viewDate)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeMonthTotal", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }
        #endregion

        #region [근무 현황, 계획]
        /// <summary>
        /// 월 근무현황 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeDaily(string mode, int targetId, string viewDate)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeDaily", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 부서원 근무현황 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeDaily(string mode, int targetId, string viewDate, string member)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
                ParamSet.Add4Sql("@member", SqlDbType.VarChar, 2000, member)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeDaily", "", 30, parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 전사 근무현황
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <param name="member"></param>
        /// <param name="sortCol"></param>
        /// <param name="sortType"></param>
        /// <param name="searchCol"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeDaily(string mode, int targetId, string viewDate, string member
                                , string sortCol, string sortType, string searchCol, string searchText)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
                ParamSet.Add4Sql("@member", SqlDbType.VarChar, 2000, member),
                ParamSet.Add4Sql("@sortcol", SqlDbType.NVarChar, 50, sortCol),
                ParamSet.Add4Sql("@sorttype", SqlDbType.NVarChar, 5, sortType),
                ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 50, searchCol),
                ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 100, searchText)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeDaily", "", 30, parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 월 근무계획 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetWorkTimePlan(string mode, int targetId, string viewDate)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimePlan", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 부서원 월 근무계획 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public DataSet GetWorkTimePlan(string mode, int targetId, string viewDate, string member)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
                ParamSet.Add4Sql("@member", SqlDbType.VarChar, 2000, member)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimePlan", "", 30, parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }

        /// <summary>
        /// 근무계획 저장
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="userId"></param>
        /// <param name="userDn"></param>
        /// <param name="userDeptId"></param>
        /// <param name="userDept"></param>
        /// <param name="userGrade"></param>
        /// <param name="planDate"></param>
        /// <param name="planInTime"></param>
        /// <param name="planOutTime"></param>
        /// <param name="planType"></param>
        /// <param name="reason"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public long InsertWorkTimePlan(long regId, int userId, string userDn, int userDeptId, string userDept, string userGrade
                            , string planDate, string planInTime, string planOutTime, string planType, string reason, string status)
        {
            long iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 8, regId),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.Int, 4, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),
                ParamSet.Add4Sql("@usergrade", SqlDbType.NVarChar, 50, userGrade),
                ParamSet.Add4Sql("@plandate", SqlDbType.Char, 10, planDate),
                ParamSet.Add4Sql("@planintime", SqlDbType.VarChar, 20, planInTime),
                ParamSet.Add4Sql("@planouttime", SqlDbType.VarChar, 20, planOutTime),
                ParamSet.Add4Sql("@plantype", SqlDbType.NVarChar, 10, planType),
                ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, 200, reason),
                ParamSet.Add4Sql("@status", SqlDbType.Char, 1, status),

                ParamSet.Add4Sql("@oid", SqlDbType.BigInt, 8, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertWorkTimePlan", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt64(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 근무계획 승인, 재검토요청
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="userId"></param>
        /// <param name="userDn"></param>
        /// <param name="userDeptId"></param>
        /// <param name="userDept"></param>
        /// <param name="userGrade"></param>
        /// <param name="status"></param>
        /// <param name="comment"></param>
        public void SetWorkTimePlan(long regId, int userId, string userDn, int userDeptId
                                , string userDept, string userGrade, string status, string comment)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 8, regId),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.Int, 4, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),
                ParamSet.Add4Sql("@usergrade", SqlDbType.NVarChar, 50, userGrade),
                ParamSet.Add4Sql("@status", SqlDbType.Char, 1, status),
                ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 200, comment)
            };

            ParamData pData = new ParamData("admin.ph_up_SetWorkTimePlan", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [근무조정 신청 승인 관련]
        /// <summary>
        /// 근무조정 신청
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workDate"></param>
        /// <param name="reqSubject"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public long InsertWorkTimeRequest(int userId, string workDate, string reqSubject, string reason)
        {
            long iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@workdate", SqlDbType.Char, 10, workDate),
                ParamSet.Add4Sql("@reqsubject", SqlDbType.NVarChar, 200, reqSubject),
                ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, 500, reason),

                ParamSet.Add4Sql("@oid", SqlDbType.BigInt, 8, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertWorkTimeRequest", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt64(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 근무조정 신청 항목 저장
        /// </summary>
        /// <param name="reqId"></param>
        /// <param name="seq"></param>
        /// <param name="regId"></param>
        /// <param name="workStatus"></param>
        public void InsertWorkTimeRequestHistory(int reqId, int seq, long regId, string workStatus)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@reqid", SqlDbType.Int, 4, reqId),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq),
                ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 8, regId),
                ParamSet.Add4Sql("@workstatus", SqlDbType.Char, 1, workStatus)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertWorkTimeRequestHistory", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 근무조정신청 승인
        /// </summary>
        /// <param name="step"></param>
        /// <param name="reqId"></param>
        /// <param name="userId"></param>
        /// <param name="userDn"></param>
        /// <param name="userDeptId"></param>
        /// <param name="userDept"></param>
        /// <param name="userGrade"></param>
        /// <param name="status"></param>
        /// <param name="comment"></param>
        public void SetWorkTimeRequest(string step, int reqId, int userId, string userDn, int userDeptId
                                    , string userDept, string userGrade, string status, string comment)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@step", SqlDbType.Char, 1, step),
                ParamSet.Add4Sql("@reqid", SqlDbType.Int, 4, reqId),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.Int, 4, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),
                ParamSet.Add4Sql("@usergrade", SqlDbType.NVarChar, 50, userGrade),
                ParamSet.Add4Sql("@status", SqlDbType.Char, 1, status),
                ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 200, comment)
            };

            ParamData pData = new ParamData("admin.ph_up_SetWorkTimeRequest", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 근무조정신청 항목별 이력 수정
        /// </summary>
        /// <param name="reqId"></param>
        /// <param name="seq"></param>
        /// <param name="regId"></param>
        /// <param name="resetStatus"></param>
        /// <param name="resetTime"></param>
        public void SetWorkTimeRequestHistory(int reqId, int seq, long regId, string resetStatus, string resetTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@reqid", SqlDbType.Int, 4, reqId),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq),
                ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 8, regId),
                ParamSet.Add4Sql("@resetstatus", SqlDbType.Char, 1, resetStatus),
                ParamSet.Add4Sql("@resettime", SqlDbType.VarChar, 20, resetTime)
            };

            ParamData pData = new ParamData("admin.ph_up_SetWorkTimeRequestHistory", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 근무조정 카운트 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="viewer"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public string GetWorkTimeRequestCount(string mode, int viewer, string member)
        {
            string strReturn = "";
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@viewer", SqlDbType.Int, 4, viewer),
                ParamSet.Add4Sql("@member", SqlDbType.VarChar, 2000, member)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeRequestCount", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 근무조정 신청 목록 보기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="targetId"></param>
        /// <param name="location"></param>
        /// <param name="viewDate"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeRequest(string mode, int targetId, string location, string viewDate, string member)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@location", SqlDbType.VarChar, 20, location),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
                ParamSet.Add4Sql("@member", SqlDbType.VarChar, 2000, member)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeRequest", "", 30, parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }
        #endregion

        #region [대장]
        /// <summary>
        /// 야간/특근대장, 전사급여대장
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="viewDate"></param>
        /// <param name="targetId"></param>
        /// <param name="member"></param>
        /// <param name="sortCol"></param>
        /// <param name="sortType"></param>
        /// <param name="searchCol"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public DataSet GetWorkTimeLedger(string mode, string viewDate, int targetId, string member
                                , string sortCol, string sortType, string searchCol, string searchText)
        {
            DataSet ds = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@member", SqlDbType.VarChar, 2000, member),
                ParamSet.Add4Sql("@sortcol", SqlDbType.NVarChar, 50, sortCol),
                ParamSet.Add4Sql("@sorttype", SqlDbType.NVarChar, 5, sortType),
                ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 50, searchCol),
                ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 100, searchText)
            };

            ParamData pData = new ParamData("admin.ph_up_GetWorkTimeLedger", "", 30, parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return ds;
        }
        #endregion
    }
}
