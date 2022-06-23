using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ZumNet.Framework.Base;
using ZumNet.Framework.Data;

namespace ZumNet.DAL.InterfaceDac
{
    /// <summary>
    /// 쿼리문, 프로시저, 파라미터 외부에서 받아 처리
    /// </summary>
    public class ExecuteDac : DacBase
    {
        #region [생성자]
        /// <summary>
        /// 
        /// </summary>
        public ExecuteDac(string connectionString = "") : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ExecuteDac(SqlConnection connection) : base(connection)
        {
        }
        #endregion

        #region [데이터셋 반환]
        /// <summary>
        /// 데이터셋 반환 - 프로시저
        /// </summary>
        /// <param name="txRquest"></param>
        /// <param name="sp"></param>
        /// <param name="tableName"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteProcedure(bool txRquest, string sp, string tableName, int timeout, SqlParameter[] parameters)
		{
			DataSet dsReturn = null;
			ParamData pData = new ParamData(sp, "", tableName, timeout, parameters);

			using (DbBase db = new DbBase())
			{
				if (txRquest) dsReturn = db.ExecuteDatasetTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                else dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
			return dsReturn;
		}

        /// <summary>
        /// 데이터셋 반환 - 쿼리스트링
        /// </summary>
        /// <param name="txRquest"></param>
        /// <param name="query"></param>
        /// <param name="tableName"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
		public DataSet ExecuteQuery(bool txRquest, string query, string tableName, int timeout, SqlParameter[] parameters)
		{
			DataSet dsReturn = null;
			ParamData pData = new ParamData(query, "text", tableName, timeout, parameters);

			using (DbBase db = new DbBase())
			{
                if (txRquest) dsReturn = db.ExecuteDatasetTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                else dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
			return dsReturn;
		}
        #endregion

        #region [문자열 반환]
        /// <summary>
        /// 문자열 반환 - 프로시저
        /// </summary>
        /// <param name="txRquest"></param>
        /// <param name="sp"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string ExecuteScalarProcedure(bool txRquest, string sp, int timeout, SqlParameter[] parameters)
        {
            string strReturn = "";
            ParamData pData = new ParamData(sp, "", timeout, parameters);

            using (DbBase db = new DbBase())
            {
                if (txRquest) strReturn = db.ExecuteScalarTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                else strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
            return strReturn;
        }

        /// <summary>
        /// 문자열 반환 - 쿼리스트링
        /// </summary>
        /// <param name="txRquest"></param>
        /// <param name="query"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string ExecuteScalarQuery(bool txRquest, string query, int timeout, SqlParameter[] parameters)
        {
            string strReturn = "";
            ParamData pData = new ParamData(query, "text", timeout, parameters);

            using (DbBase db = new DbBase())
            {
                if (txRquest) strReturn = db.ExecuteScalarTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                else strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
            return strReturn;
        }
        #endregion

        #region [NonQuery 실행]
        /// <summary>
        /// NonQuery 실행 - 프로시저
        /// </summary>
        /// <param name="txRquest"></param>
        /// <param name="sp"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns>Output Parameter 값</returns>
        public string ExecuteProcedure(bool txRquest, string sp, int timeout, SqlParameter[] parameters)
        {
            string dsReturn = null;
            ParamData pData = new ParamData(sp, "", timeout, parameters);

            using (DbBase db = new DbBase())
            {
                if (txRquest) dsReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                else dsReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
            return dsReturn;
        }

        /// <summary>
        /// NonQuery 실행 - 쿼리스트링
        /// </summary>
        /// <param name="txRquest"></param>
        /// <param name="query"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns>Output Parameter 값</returns>
        public string ExecuteQuery(bool txRquest, string query, int timeout, SqlParameter[] parameters)
        {
            string dsReturn = null;
            ParamData pData = new ParamData(query, "text", timeout, parameters);

            using (DbBase db = new DbBase())
            {
                if (txRquest) dsReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                else dsReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
            return dsReturn;
        }
        #endregion

    }
}
