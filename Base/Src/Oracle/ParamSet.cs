using System;
using System.Data;
using System.Data.OracleClient;

namespace ZumNet.DAL.Base.Oracle
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
        public static OracleParameter Add4Sql(string paramName, object paramValue)
        {
            OracleParameter param = new OracleParameter();
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
        public static OracleParameter Add4Sql(string paramName, OracleType dbType, object paramValue)
        {
            OracleParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.OracleType = dbType;
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
        public static OracleParameter Add4Sql(string paramName, OracleType dbType, ParameterDirection paramDirection, object paramValue)
        {
            OracleParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.OracleType = dbType;
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
        public static OracleParameter Add4Sql(string paramName, OracleType dbType, int size, ParameterDirection paramDirection, object paramValue)
        {
            OracleParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.OracleType = dbType;
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
        public static OracleParameter Add4Sql(string paramName, OracleType dbType, int size, ParameterDirection paramDirection)
        {
            OracleParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.OracleType = dbType;
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
        public static OracleParameter Add4Sql(string paramName, OracleType dbType, int size, object paramValue)
        {
            OracleParameter param = new OracleParameter();
            param.ParameterName = paramName;
            param.OracleType = dbType;
            param.Size = size;
            param.Value = paramValue;
            return param;
        }

        #endregion
    }
}
