using System;
using System.Data;
using System.Data.SqlClient;

namespace ZumNet.DAL.Base
{
    /// <summary>
    /// SQL 파라미터 설정
    /// </summary>
    public class ParamSet
    {
        /// <summary>
        /// 
        /// </summary>
        public ParamSet()
        {
        }

        #region Add4Sql[SqlParameter 정의]

        /// <summary>
        /// SqlParameter 정의
        /// </summary>
        /// <param name="paramName">Parameter 이름</param>
        /// <param name="paramValue">Parameter 입력값</param>
        /// <returns></returns>
        public static SqlParameter Add4Sql(string paramName, object paramValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            return param;
        }

        /// <summary>
        /// SqlParameter 정의
        /// </summary>
        /// <param name="paramName">Parameter 이름</param>
        /// <param name="dbType">Parameter 데이터형식</param>
        /// <param name="paramValue">Parameter 입력값</param>
        /// <returns></returns>
        public static SqlParameter Add4Sql(string paramName, SqlDbType dbType, object paramValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.SqlDbType = dbType;
            param.Value = paramValue;
            return param;
        }

        /// <summary>
        /// SqlParameter 정의
        /// </summary>
        /// <param name="paramName">Parameter 이름</param>
        /// <param name="dbType">Parameter 데이터형식</param>
        /// <param name="paramDirection">Parameter 형식</param>
        /// <param name="paramValue">Parameter 입력값</param>
        /// <returns></returns>
        public static SqlParameter Add4Sql(string paramName, SqlDbType dbType, ParameterDirection paramDirection, object paramValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.SqlDbType = dbType;
            param.Direction = paramDirection;
            param.Value = paramValue;
            return param;
        }

        /// <summary>
        /// SqlParameter 정의
        /// </summary>
        /// <param name="paramName">Parameter 이름</param>
        /// <param name="dbType">Parameter 데이터형식</param>
        /// <param name="size">Parameter 길이</param>
        /// <param name="paramDirection">Parameter 형식</param>
        /// <param name="paramValue">Parameter 입력값</param>
        /// <returns></returns>
        public static SqlParameter Add4Sql(string paramName, SqlDbType dbType, int size, ParameterDirection paramDirection, object paramValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.SqlDbType = dbType;
            param.Size = size;
            param.Direction = paramDirection;
            param.Value = paramValue;
            return param;
        }

        /// <summary>
        /// SqlParameter 정의
        /// </summary>
        /// <param name="paramName">Parameter 이름</param>
        /// <param name="dbType">Parameter 데이터형식</param>
        /// <param name="size">Parameter 길이</param>
        /// <param name="paramDirection">Parameter 형식</param>
        /// <returns></returns>
        public static SqlParameter Add4Sql(string paramName, SqlDbType dbType, int size, ParameterDirection paramDirection)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.SqlDbType = dbType;
            param.Size = size;
            param.Direction = paramDirection;
            return param;
        }

        /// <summary>
        /// SqlParameter 정의
        /// </summary>
        /// <param name="paramName">Parameter 이름</param>
        /// <param name="dbType">Parameter 데이터형식</param>
        /// <param name="size">Parameter 길이</param>
        /// <param name="paramValue">Parameter 입력값</param>
        /// <returns></returns>
        public static SqlParameter Add4Sql(string paramName, SqlDbType dbType, int size, object paramValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.SqlDbType = dbType;
            param.Size = size;
            param.Value = paramValue;
            return param;
        }

        #endregion
    }
}
