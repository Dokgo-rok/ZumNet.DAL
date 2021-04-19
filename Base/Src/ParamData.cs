using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace ZumNet.DAL.Base
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ParamData : ISerializable
    {
        #region [멤버변수]
        private string _queryString = string.Empty;
        private string _commandType = string.Empty; // "" :  CommandType.StoredProcedure, "text" : CommandType.Text, "table" : CommandType.TableDirect;
        private string _srcTable = string.Empty;
        private int _commandTimeout = 15; // 기본
        private SqlParameter[] _sqlParams = null;
        

        /// <summary>
        /// 쿼리문 또는 프로시저명
        /// </summary>
        public string QueryString { get => _queryString; set => _queryString = value; }

        /// <summary>
        /// 쿼리 종류
        /// </summary>
        public string CommandType { get => _commandType; set => _commandType = value; }

        /// <summary>
        /// 반환테이블명
        /// </summary>
        public string SrcTable { get => _srcTable; set => _srcTable = value; }

        /// <summary>
        /// 실행제한시간
        /// </summary>
        public int CommandTimeout { get => _commandTimeout; set => _commandTimeout = value; }
        
        /// <summary>
        /// 파라미터
        /// </summary>
        public SqlParameter[] SqlParams { get => _sqlParams; set => _sqlParams = value; }
        

        private string[] _nameList;
        private DbType[] _dbTypeList;
        private int[] _sizeList;
        private object[] _valueList;
        private ParameterDirection[] _dirList;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">프로시저 또는 쿼리문</param>
        public ParamData(string query)
        {
            this._queryString = query;
            this._commandType = "";
            this._srcTable = "ResultTable";
            this._commandTimeout = 15;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">프로시저 또는 쿼리문</param>
        /// <param name="sqlParams">SqlParameters</param>
        public ParamData(string query, SqlParameter[] sqlParams)
        {
            this._queryString = query;
            this._commandType = "";
            this._srcTable = "ResultTable";
            this._commandTimeout = 15;
            this._sqlParams = sqlParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">프로시저 또는 쿼리문</param>
        /// <param name="cmdType">쿼리 종류</param>
        /// <param name="sqlParams">SqlParameters</param>
        public ParamData(string query, string cmdType, SqlParameter[] sqlParams)
        {
            this._queryString = query;
            this._commandType = cmdType;
            this._srcTable = "ResultTable";
            this._commandTimeout = 15;
            this._sqlParams = sqlParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">프로시저 또는 쿼리문</param>
        /// <param name="cmdType">쿼리 종류</param>
        /// <param name="cmdTimeout">실행제한시간</param>
        /// <param name="sqlParams">SqlParameters</param>
        public ParamData(string query, string cmdType, int cmdTimeout, SqlParameter[] sqlParams)
        {
            this._queryString = query;
            this._commandType = cmdType;
            this._srcTable = "ResultTable";
            this._commandTimeout = cmdTimeout;
            this._sqlParams = sqlParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">프로시저 또는 쿼리문</param>
        /// <param name="cmdType">쿼리 종류</param>
        /// <param name="srcTable">반환 테이블명</param>
        /// <param name="cmdTimeout">실행제한시간</param>
        /// <param name="sqlParams">SqlParameters</param>
        public ParamData(string query, string cmdType, string srcTable, int cmdTimeout, SqlParameter[] sqlParams)
        {
            this._queryString = query;
            this._commandType = cmdType;
            this._srcTable = srcTable;
            this._commandTimeout = cmdTimeout;
            this._sqlParams = sqlParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="si"></param>
        /// <param name="context"></param>
        protected ParamData(SerializationInfo si, StreamingContext context)
        {
            if (si.GetBoolean("Exist"))
            {
                this._nameList = (string[])si.GetValue("NameList", typeof(string[]));
                this._dbTypeList = (DbType[])si.GetValue("DbTypeList", typeof(DbType[]));
                this._sizeList = (int[])si.GetValue("SizeList", typeof(int[]));
                this._valueList = (object[])si.GetValue("ValueList", typeof(object[]));
                this._dirList = (ParameterDirection[])si.GetValue("DirectionList", typeof(ParameterDirection[]));

                this._sqlParams = new SqlParameter[this._dbTypeList.Length];
                for (int i = 0; i < this._dbTypeList.Length; i++)
                {
                    this._sqlParams[i] = new SqlParameter();
                    this._sqlParams[i].ParameterName = this._nameList[i];
                    this._sqlParams[i].DbType = this._dbTypeList[i];
                    this._sqlParams[i].Size = this._sizeList[i];
                    this._sqlParams[i].Value = this._valueList[i];
                    this._sqlParams[i].Direction = this._dirList[i];
                }
            }

            this.QueryString = (string)si.GetValue("Query", typeof(string));
            this._commandType = (string)si.GetValue("CommandType", typeof(string));
        }

        void System.Runtime.Serialization.ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
        {
            bool bExist = false;
            if (this._sqlParams != null)
            {
                int iLen = this._sqlParams.Length;

                this._nameList = new string[iLen];
                this._dbTypeList = new DbType[iLen];
                this._sizeList = new int[iLen];
                this._valueList = new object[iLen];
                this._dirList = new ParameterDirection[iLen];

                for (int i = 0; i < iLen; i++)
                {
                    this._nameList[i] = this._sqlParams[i].ParameterName;
                    this._dbTypeList[i] = this._sqlParams[i].DbType;
                    this._sizeList[i] = this._sqlParams[i].Size;
                    this._valueList[i] = this._sqlParams[i].Value;
                    this._dirList[i] = this._sqlParams[i].Direction;
                }
                si.AddValue("NameList", this._nameList);
                si.AddValue("DbTypeList", this._dbTypeList);
                si.AddValue("SizeList", this._sizeList);
                si.AddValue("ValueList", this._valueList);
                si.AddValue("DirectionList", this._dirList);
                bExist = true;
            }
            si.AddValue("Exist", bExist);
            si.AddValue("Query", this.QueryString);
            si.AddValue("CommandType", this._commandType);
        }

        /// <summary>
        /// Parameter에 설정된 Value값 찾기
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public object GetParamValue(string parameterName)
        {
			if (this._sqlParams != null)
			{
				foreach (var sqlParam in this._sqlParams)
				{
					if (String.Compare(sqlParam.ParameterName, parameterName, true) == 0)
					{
                        return sqlParam.Value;
					}
				}
			}

            return null;
        }
    }
}
