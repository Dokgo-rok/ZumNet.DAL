using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

using ZumNet.Framework.Common;
using ZumNet.DAL.Base;

namespace ZumNet.DAL.Sample
{
    /// <summary>
    /// 
    /// </summary>
    public class SampleManager : global::System.MarshalByRefObject, System.IDisposable
    {
        //private ZumNet.Framework.Logger.TimeStamp _timeStamp = null;

        /// <summary>
        /// 
        /// </summary>
        public SampleManager()
        {
            //_timeStamp = new ZumNet.Framework.Logger.TimeStamp();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //if (_timeStamp != null) _timeStamp = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="userId"></param>
        /// <param name="grId"></param>
        /// <returns></returns>
        public DataSet GetUserInfo(object dbConn, int userId, int grId)
        {
            //_timeStamp.Prepare();

            //DbConnect.GetString 호출 30~50ms 소요
            //string strConnectString = (dbConn.Length > 0) ? dbConn : DbConnect.GetString("", DbConnect.INITIAL_CATALOG.INIT_CAT_MAIN);
            if (dbConn.GetType() == "".GetType() && (string)dbConn == "") dbConn = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_BASE);
            DataSet ds = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, userId),
                ParamSet.Add4Sql("@gr_id", SqlDbType.Int, grId)
            };
            //parameters[0] = ParamSet.Add4Sql("@userid", SqlDbType.Int, userId);
            //parameters[1] = ParamSet.Add4Sql("@gr_id", SqlDbType.Int, grId);

            ParamData pData = new ParamData("admin.ph_up_ObjectGetUserInfo", parameters);

            using (DbBase db = new DbBase())
            {
                ds = db.ExecuteDatasetNTx(dbConn, MethodInfo.GetCurrentMethod(), pData);
            }
            //_timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), System.Reflection.MethodBase.GetCurrentMethod());

            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="alias"></param>
        /// <param name="oType"></param>
        /// <returns></returns>
        public string CheckBaseDoubleAlias(object dbConn, string alias, string oType)
        {
            //_timeStamp.Prepare();

            //DbConnect.GetString 호출 20~70ms 소요
            //string strConnectString = (dbConn.Length > 0) ? dbConn : DbConnect.GetString("", DbConnect.INITIAL_CATALOG.INIT_CAT_MAIN);
            if (dbConn.GetType() == "".GetType() && (string)dbConn == "") dbConn = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_BASE);
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@object_alias", SqlDbType.VarChar, 30, alias),
                ParamSet.Add4Sql("@object_type", SqlDbType.VarChar, 20, oType),
                ParamSet.Add4Sql("@result", SqlDbType.Char, 1, ParameterDirection.Output)
            };
            //parameters[0] = ParamSet.Add4Sql("@object_alias", SqlDbType.VarChar, 30, alias);
            //parameters[1] = ParamSet.Add4Sql("@object_type", SqlDbType.VarChar, 20, oType);
            //parameters[2] = ParamSet.Add4Sql("@result", SqlDbType.Char, 1, ParameterDirection.Output);

            ParamData pData = new ParamData("admin.ph_up_BaseAliasDblCheck", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteNonQueryNTx(dbConn, MethodInfo.GetCurrentMethod(), pData);
            }
            //_timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), System.Reflection.MethodBase.GetCurrentMethod());

            return strReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="userid"></param>
        /// <param name="personNo1"></param>
        /// <param name="personNo2"></param>
        /// <param name="birth"></param>
        /// <param name="sortKey"></param>
        /// <param name="careerSubject"></param>
        /// <returns></returns>
        public string UpdateUserInfo(object dbConn, int userid, string personNo1, string personNo2, string birth, string sortKey, string careerSubject)
        {
            //_timeStamp.Prepare();

            //DbConnect.GetString 호출 20~70ms 소요
            //string strConnectString = (dbConn.Length > 0) ? dbConn : DbConnect.GetString("", DbConnect.INITIAL_CATALOG.INIT_CAT_MAIN);
            if (dbConn.GetType() == "".GetType() && (string)dbConn == "") dbConn = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_BASE);
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, userid),
                ParamSet.Add4Sql("@person1", SqlDbType.VarChar, 20, personNo1),
                ParamSet.Add4Sql("@person2", SqlDbType.VarChar, 20, personNo2),
                ParamSet.Add4Sql("@birth", SqlDbType.VarChar, 20, birth),
                ParamSet.Add4Sql("@sortkey", SqlDbType.VarChar, 10, sortKey),
                ParamSet.Add4Sql("@career", SqlDbType.VarChar, 100, careerSubject)
            };

            string strQuery = @"
UPDATE admin.PH_USER_DETAIL
SET PersonNo1 = @person1, PersonNo2 = @person2, Birth = @birth
WHERE UserID = @userid

INSERT INTO admin.PH_USER_CAREER (UserID, SortKey, Subject)
VALUES (@userid, @sortkey, @career) -- raise error
";

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteNonQueryTx(dbConn, MethodInfo.GetCurrentMethod(), pData);
            }
            //_timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), System.Reflection.MethodBase.GetCurrentMethod());

            return strReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConn"></param>
        /// <param name="msgId"></param>
        /// <param name="userId"></param>
        /// <param name="subject"></param>
        /// <param name="description"></param>
        /// <param name="anniDate"></param>
        /// <param name="anniDateType"></param>
        /// <param name="alarmdate"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public string SetUserAnniversary(object dbConn, int msgId, int userId, string subject, string description
                        , string anniDate, string anniDateType, string alarmdate, string priority)
        {
            //_timeStamp.Prepare();

            //DbConnect.GetString 호출 20~70ms 소요
            //string strConnectString = (dbConn.Length > 0) ? dbConn : DbConnect.GetString(dbConn, DbConnect.INITIAL_CATALOG.INIT_CAT_MAIN);
            if (dbConn.GetType() == "".GetType() && (string)dbConn == "") dbConn = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_BASE);
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, msgId),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, userId),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 50, subject),
                ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
                ParamSet.Add4Sql("@annidate", SqlDbType.VarChar, 10, anniDate),
                ParamSet.Add4Sql("@annidatetype", SqlDbType.Char, 1, anniDateType),
                ParamSet.Add4Sql("@alarmdate", SqlDbType.VarChar, 10, alarmdate),
                ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority)
            };

            ParamData pData = new ParamData("admin.ph_up_DirectorySetUserAnniversary", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteNonQueryTx(dbConn, MethodInfo.GetCurrentMethod(), pData);
            }
            //_timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), System.Reflection.MethodBase.GetCurrentMethod());

            return strReturn;
        }
    }
}
