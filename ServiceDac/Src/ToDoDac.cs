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
    public class ToDoDac : DacBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ToDoDac(string connectionString = "") : base(connectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public ToDoDac(SqlConnection connection) : base(connection)
        {

        }

        /// <summary>
        /// 할일 신규 생성
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectID"></param>
        /// <param name="scheduleType"></param>
        /// <param name="taskID"></param>
        /// <param name="inherited"></param>
        /// <param name="subject"></param>
        /// <param name="bodyData"></param>
        /// <param name="location"></param>
        /// <param name="state"></param>
        /// <param name="priority"></param>
        /// <param name="periodFrom"></param>
        /// <param name="startTime"></param>
        /// <param name="periodTo"></param>
        /// <param name="endTime"></param>
        /// <param name="term"></param>
        /// <param name="repeatType"></param>
        /// <param name="alarm"></param>
        /// <param name="creatorID"></param>
        /// <param name="creatorDeptName"></param>
        /// <param name="creatorDeptID"></param>
        /// <param name="isFile"></param>
        /// <param name="fileInfo"></param>
        /// <param name="strTaskActivity"></param>
        /// <param name="option"></param>
        /// <param name="orgMsgId"></param>
        /// <param name="targetDate"></param>
        /// <returns>신규 MessageID</returns>
        public int CreateScheduleMain(string objectType, int objectID, string scheduleType, int taskID
                , string inherited, string subject, string bodyData, string location, int state, string priority, string periodFrom
                , string startTime, string periodTo, string endTime, int term, string repeatType, string alarm, int creatorID
                , string creatorDeptName, int creatorDeptID, string isFile, string fileInfo, string strTaskActivity
                , string option, int orgMsgId, string targetDate)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
                ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
                ParamSet.Add4Sql("@schType", SqlDbType.Char, 2, scheduleType),
                ParamSet.Add4Sql("@taskid", SqlDbType.Int, 4, taskID),
                ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
                ParamSet.Add4Sql("@body", SqlDbType.NText, bodyData),
                ParamSet.Add4Sql("@location", SqlDbType.NVarChar, 100, location),
                ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
                ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
                ParamSet.Add4Sql("@periodFrom", SqlDbType.Char, 10, periodFrom),
                ParamSet.Add4Sql("@startTime", SqlDbType.Char, 5, startTime),
                ParamSet.Add4Sql("@periodTo", SqlDbType.Char, 10, periodTo),
                ParamSet.Add4Sql("@endTime", SqlDbType.Char, 5, endTime),
                ParamSet.Add4Sql("@term", SqlDbType.Int, 4, term),
                ParamSet.Add4Sql("@repeatType", SqlDbType.Char, 1, repeatType),
                ParamSet.Add4Sql("@alarm", SqlDbType.VarChar, 5, alarm),
                ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
                ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDeptName),
                ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
                ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
                ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo),
                ParamSet.Add4Sql("@taskActivity", SqlDbType.NVarChar, 30, strTaskActivity),
                ParamSet.Add4Sql("@option", SqlDbType.VarChar, 10, option),
                ParamSet.Add4Sql("@orgmsgid", SqlDbType.Int, 4, orgMsgId),
                ParamSet.Add4Sql("@tdate", SqlDbType.VarChar, 10, targetDate),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.up_ToDoCreateMain", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 할일 수정
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="actor"></param>
        /// <param name="scheduleType"></param>
        /// <param name="inherited"></param>
        /// <param name="subject"></param>
        /// <param name="bodyData"></param>
        /// <param name="location"></param>
        /// <param name="state"></param>
        /// <param name="priority"></param>
        /// <param name="periodFrom"></param>
        /// <param name="startTime"></param>
        /// <param name="periodTo"></param>
        /// <param name="endTime"></param>
        /// <param name="term"></param>
        /// <param name="alarm"></param>
        /// <param name="isFile"></param>
        /// <param name="fileInfo"></param>
        public void ModifyToDoContents(int messageID, int actor, string scheduleType, string inherited, string subject, string bodyData
                                , string location, int state, string priority, string periodFrom, string startTime, string periodTo, string endTime
                                , int term, string alarm, string isFile, string fileInfo)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actor),
                ParamSet.Add4Sql("@schType", SqlDbType.Char, 2, scheduleType),
                ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
                ParamSet.Add4Sql("@body", SqlDbType.NText, bodyData),
                ParamSet.Add4Sql("@location", SqlDbType.NVarChar, 100, location),
                ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
                ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
                ParamSet.Add4Sql("@periodFrom", SqlDbType.Char, 10, periodFrom),
                ParamSet.Add4Sql("@startTime", SqlDbType.Char, 5, startTime),
                ParamSet.Add4Sql("@periodTo", SqlDbType.Char, 10, periodTo),
                ParamSet.Add4Sql("@endTime", SqlDbType.Char, 5, endTime),
                ParamSet.Add4Sql("@term", SqlDbType.Int, 4, term),
                ParamSet.Add4Sql("@alarm", SqlDbType.VarChar, 5, alarm),
                ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
                ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo)
            };

            ParamData pData = new ParamData("admin.up_ToDoGeModifyContents", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 할일 반복 설정
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="deleteDate"></param>
        /// <param name="repeatType"></param>
        /// <param name="periodFrom"></param>
        /// <param name="repeatEnd"></param>
        /// <param name="repeatCount"></param>
        /// <param name="intervalType"></param>
        /// <param name="interval"></param>
        /// <param name="condDay"></param>
        /// <param name="condWeek"></param>
        /// <param name="condDate"></param>
        /// <param name="option"></param>
        public void WorkToDoRepeat(int messageID, string deleteDate, string repeatType, string periodFrom, string repeatEnd
                                , string repeatCount, string intervalType, string interval, string condDay, string condWeek, string condDate, string option)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@deletedate", SqlDbType.Char, 10, deleteDate),
                ParamSet.Add4Sql("@repeatType", SqlDbType.Char, 1, repeatType),
                ParamSet.Add4Sql("@periodFrom", SqlDbType.Char, 10, periodFrom),
                ParamSet.Add4Sql("@repeatEnd", SqlDbType.VarChar, 10, repeatEnd),
                ParamSet.Add4Sql("@repeatCount", SqlDbType.VarChar, 10, repeatCount),
                ParamSet.Add4Sql("@intervalType", SqlDbType.Char, 1, intervalType),
                ParamSet.Add4Sql("@interval", SqlDbType.Char, 2, interval),
                ParamSet.Add4Sql("@con_Day", SqlDbType.Char, 7, condDay),
                ParamSet.Add4Sql("@con_Week", SqlDbType.Char, 1, condWeek),
                ParamSet.Add4Sql("@con_Date", SqlDbType.Char, 2, condDate),
                ParamSet.Add4Sql("@option", SqlDbType.VarChar, 10, option)
            };

            ParamData pData = new ParamData("admin.up_ToDoWorkRepeat", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 할일 메뉴에 표시될 부서원 목록
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="grID"></param>
        /// <returns></returns>
        public DataSet GetToDoMenuList(int userID, int grID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
                ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, grID)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetMenuList", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 할일 대상에 대한 권한 가져오기
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="grID"></param>
        /// <param name="targetID"></param>
        /// <returns></returns>
        public string GetToDoAclOfTarget(int userID, int grID, int targetID)
        {
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
                ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, grID),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetAclOfTarget", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 할일 갯수 조회
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectID"></param>
        /// <param name="mode"></param>
        /// <param name="date"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetToDoCount(string objectType, int objectID, string mode, string date, string fromDate, string toDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@objectType", SqlDbType.Char, 2, objectType),
                ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@date", SqlDbType.Char, 10, date),
                ParamSet.Add4Sql("@fromdate", SqlDbType.Char, 10, fromDate),
                ParamSet.Add4Sql("@todate", SqlDbType.Char, 10, toDate)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetCount", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 할일 조회 목록
        /// </summary>
        /// <param name="actionKind"></param>
        /// <param name="objectType"></param>
        /// <param name="objectID"></param>
        /// <param name="state"></param>
        /// <param name="mode"></param>
        /// <param name="date"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetToDoList(string actionKind, string objectType, int objectID, int state, string mode, string date, string fromDate, string toDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@action_kind", SqlDbType.VarChar, 15, actionKind),
                ParamSet.Add4Sql("@objectType", SqlDbType.Char, 2, objectType),
                ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
                ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@date", SqlDbType.Char, 10, date),
                ParamSet.Add4Sql("@fromdate", SqlDbType.Char, 10, fromDate),
                ParamSet.Add4Sql("@todate", SqlDbType.Char, 10, toDate)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetList", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 할일 정보 불러오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="domainID"></param>
        /// <param name="messageID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetToDoView(string mode, int domainID, int messageID, string date)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 1, mode),
                ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@date", SqlDbType.Char, 10, date)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetView", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 할일 요약정보 조회
        /// </summary>
        /// <param name="actionKind"></param>
        /// <param name="userId"></param>
        /// <param name="targetUR"></param>
        /// <param name="targetGR"></param>
        /// <param name="state"></param>
        /// <param name="mode"></param>
        /// <param name="date"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataSet GetToDoSummary(string actionKind, int userId, int targetUR, int targetGR, int state, string mode, string date, string fromDate, string toDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@action_kind", SqlDbType.VarChar, 15, actionKind),
                ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@targetur", SqlDbType.Int, 4, targetUR),
                ParamSet.Add4Sql("@targetgr", SqlDbType.Int, 4, targetGR),
                ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@date", SqlDbType.Char, 10, date),
                ParamSet.Add4Sql("@fromdate", SqlDbType.Char, 10, fromDate),
                ParamSet.Add4Sql("@todate", SqlDbType.Char, 10, toDate)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetSummary", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 상태, 중요도 변경
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="repeat"></param>
        /// <param name="date"></param>
        /// <param name="actorId"></param>
        /// <param name="field"></param>
        /// <param name="vlu"></param>
        public void UpdateToDoState(int messageId, string repeat, string date, int actorId, string field, string vlu)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageId),
                ParamSet.Add4Sql("@repeat", SqlDbType.Char, 1, repeat),
                ParamSet.Add4Sql("@date", SqlDbType.Char, 10, date),
                ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actorId),
                ParamSet.Add4Sql("@field", SqlDbType.VarChar, 50, field),
                ParamSet.Add4Sql("@value", SqlDbType.VarChar, 10, vlu)
            };

            ParamData pData = new ParamData("admin.up_ToDoUpdateState", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 할일 삭제
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="objectType"></param>
        /// <param name="objectID"></param>
        /// <param name="msgID"></param>
        /// <param name="actor"></param>
        /// <param name="option"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string DeleteToDo(string mode, string objectType, int objectID, int msgID, int actor, string option, string date)
        {
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 2, mode),
                ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
                ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
                ParamSet.Add4Sql("@msgID", SqlDbType.Int, 4, msgID),
                ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actor),
                ParamSet.Add4Sql("@option", SqlDbType.VarChar, 10, option),
                ParamSet.Add4Sql("@date", SqlDbType.VarChar, 10, date),
                ParamSet.Add4Sql("@rt", SqlDbType.Char, 1, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.up_ToDoDelete", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 할일 댓글 추가, 변경
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="seqID"></param>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        /// <param name="comment"></param>
        /// <param name="deleteDate"></param>
        public void SetToDoComment(string xfAlias, int messageID, int seqID, int userID, string userName, string comment, string deleteDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@SeqID", SqlDbType.Int, 4, seqID),
                ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, userID),
                ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 50, userName),
                ParamSet.Add4Sql("@Body", SqlDbType.NVarChar, 1000, comment),
                ParamSet.Add4Sql("@DeleteDate", SqlDbType.VarChar, 10, deleteDate)
            };

            ParamData pData = new ParamData("admin.ph_up_MsgCommentSetWrite", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 특정 댓글 가져오기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageId"></param>
        /// <param name="seqId"></param>
        /// <returns></returns>
        public DataSet SelectComment(string xfAlias, int messageId, int seqId)
        {
            DataSet dsReturn = null;
            string strQuery = "SELECT * FROM admin.PH_XF_COMMENT (NOLOCK) WHERE XFAlias = @xfalias AND MessageID = @msgid AND SeqID = @seqid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageId),
                ParamSet.Add4Sql("@seqid", SqlDbType.Int, 4, seqId)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 특정 댓글 삭제
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageId"></param>
        /// <param name="seqId"></param>
        public void DeleteComment(string xfAlias, int messageId, int seqId)
        {
            string strQuery = @"
UPDATE admin.PH_XF_COMMENT SET DeleteDate = GETDATE() WHERE XFAlias = @xfalias AND MessageID = @msgid AND SeqID = @seqid

DECLARE @cnt INT
SET @cnt = 0

SELECT @cnt = ISNULL(COUNt(MessageID), 0) FROM admin.PH_XF_COMMENT (NOLOCK) WHERE XFAlias = @xfalias AND MessageID = @msgid AND DeleteDate IS NULL

UPDATE admin.PH_XF_SCHEDULE SET CommentCount = @cnt WHERE MessageID = @msgid
";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageId),
                ParamSet.Add4Sql("@seqid", SqlDbType.Int, 4, seqId)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 할일 집계 통계
        /// </summary>
        /// <param name="actionKind"></param>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="targetUR"></param>
        /// <param name="targetGR"></param>
        /// <returns></returns>
        public DataSet GetToDoReport(string actionKind, string year, int week, int targetUR, int targetGR)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@action_kind", SqlDbType.VarChar, 10, actionKind),
                ParamSet.Add4Sql("@year", SqlDbType.Char, 4, year),
                ParamSet.Add4Sql("@week", SqlDbType.Int, 4, week),
                ParamSet.Add4Sql("@targetur", SqlDbType.Int, 4, targetUR),
                ParamSet.Add4Sql("@targetgr", SqlDbType.Int, 4, targetGR)
            };

            ParamData pData = new ParamData("admin.up_ToDoGetReport", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
    }
}
