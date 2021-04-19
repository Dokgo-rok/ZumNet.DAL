using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Transactions;

namespace ZumNet.DAL.Base
{
    /// <summary>
    /// MSSQL DAL Base
    /// </summary>
    public class DbBase : System.IDisposable
    {
        #region [생성자]
        private ZumNet.Framework.Logger.TimeStamp _timeStamp = null;

        /// <summary>
        /// 
        /// </summary>
        public DbBase()
        {
            _timeStamp = new ZumNet.Framework.Logger.TimeStamp();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_timeStamp != null) _timeStamp = null;
        }
        #endregion

        #region [DataSet 반환]
        /// <summary>
        /// 데이터셋 반환, 트랜잭션 없음
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public DataSet ExecuteDatasetNTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted; //격리수준

            DataSet ds = null;

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            ds = dbHelper.ExecuteDataset((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SrcTable, paramData.SqlParams);
                        }
                        else
                        {
                            ds = dbHelper.ExecuteDataset((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SrcTable, paramData.SqlParams);
                        }
                    }
                    scope.Complete(); //형식을 맞추기 위해 Suppress 경우도 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteDatasetNTx");
            }
            finally
            {
            }
            return ds;
        }

        /// <summary>
        /// 데이터셋 반환, 트랜잭션 필수
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public DataSet ExecuteDatasetTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted; //격리수준

            DataSet ds = null;

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            ds = dbHelper.ExecuteDataset((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SrcTable, paramData.SqlParams);
                        }
                        else
                        {
                            ds = dbHelper.ExecuteDataset((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SrcTable, paramData.SqlParams);
                        }
                    }
                    scope.Complete(); //트랜잭션 필요 경우 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteDatasetTx");
            }
            finally
            {
            }
            return ds;
        }
        #endregion

        #region [DataReader 반환]
        /// <summary>
        /// 데이터리더 반환, 트랜잭션 없음
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReaderNTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted; //격리수준

            SqlDataReader dr = null;

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            dr = dbHelper.ExecuteReader((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                        else
                        {
                            dr = dbHelper.ExecuteReader((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                    }
                    scope.Complete(); //형식을 맞추기 위해 Suppress 경우도 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteReaderNTx");
            }
            finally
            {
            }
            return dr;
        }

        /// <summary>
        /// 데이터리더 반환, 트랜잭션 필수
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReaderTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted; //격리수준

            SqlDataReader dr = null;

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            dr = dbHelper.ExecuteReader((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                        else
                        {
                            dr = dbHelper.ExecuteReader((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                    }
                    scope.Complete(); //트랜잭션 필요 경우 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteReaderTx");
            }
            finally
            {
            }
            return dr;
        }
        #endregion

        #region [ArrayList 반환]
        /// <summary>
        /// 배열반환, 트랜잭션 없음
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public ArrayList ExecuteListNTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted; //격리수준

            ArrayList rowList = null;

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            rowList = dbHelper.ExecuteList((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                        else
                        {
                            rowList = dbHelper.ExecuteList((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                    }
                    scope.Complete(); //형식을 맞추기 위해 Suppress 경우도 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteListNTx");
            }
            finally
            {
            }
            return rowList;
        }

        /// <summary>
        /// 배열반환, 트랜잭션 필수
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public ArrayList ExecuteListTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted; //격리수준

            ArrayList rowList = null;

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            rowList = dbHelper.ExecuteList((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                        else
                        {
                            rowList = dbHelper.ExecuteList((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                    }
                    scope.Complete(); //트랜잭션 필요 경우 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteListTx");
            }
            finally
            {
            }
            return rowList;
        }
        #endregion

        #region [단일값(결과집합 첫행 첫열) 반환]
        /// <summary>
        /// 단일값(결과집합 첫행 첫열) 반환, 트랜잭션 없음
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public string ExecuteScalarNTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted; //격리수준

            string rt = "";

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            rt = Convert.ToString(dbHelper.ExecuteScalar((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams));
                        }
                        else
                        {
                            rt = Convert.ToString(dbHelper.ExecuteScalar((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams));
                        }
                    }
                    scope.Complete(); //형식을 맞추기 위해 Suppress 경우도 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteScalarNTx");
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// 단일값(결과집합 첫행 첫열) 반환, 트랜잭션 필수
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public string ExecuteScalarTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted; //격리수준

            string rt = "";

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            rt = Convert.ToString(dbHelper.ExecuteScalar((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams));
                        }
                        else
                        {
                            rt = Convert.ToString(dbHelper.ExecuteScalar((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams));
                        }
                    }
                    scope.Complete(); //트랜잭션 필요 경우 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteScalarTx");
            }
            finally
            {
            }
            return rt;
        }
        #endregion

        #region [NonQuery 실행]
        /// <summary>
        /// NonQuery 실행, 트랜잭션 없음
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public string ExecuteNonQueryNTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted; //격리수준

            string rt = "";

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            int i = dbHelper.ExecuteNonQuery((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                        else
                        {
                            int i = dbHelper.ExecuteNonQuery((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                    }

                    // Output Parameter Setting
                    int iCnt = 0;
                    char cDiv = (char)1;
                    foreach (SqlParameter p in paramData.SqlParams)
                    {
                        if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
                        {
                            if (iCnt > 0) rt += cDiv;
                            rt +=  p.Value.ToString();
                        }
                    }

                    scope.Complete(); //형식을 맞추기 위해 Suppress 경우도 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteNonQueryNTx");
            }
            finally
            {
            }
            return rt;
        }

        /// <summary>
        /// NonQuery 실행, 트랜잭션 필수
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="methodBase">상위 메소드 정보</param>
        /// <param name="paramData"></param>
        /// <returns></returns>
        public string ExecuteNonQueryTx(object connect, MethodBase methodBase, ParamData paramData)
        {
            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.FromSeconds(paramData.CommandTimeout);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted; //격리수준

            string rt = "";

            try
            {
                //실행시간 준비
                _timeStamp.Prepare();

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    using (ZumNet.Framework.Data.SQLHelper dbHelper = new ZumNet.Framework.Data.SQLHelper())
                    {
                        if (connect.GetType() == "".GetType())
                        {
                            int i = dbHelper.ExecuteNonQuery((string)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                        else
                        {
                            int i = dbHelper.ExecuteNonQuery((SqlConnection)connect, paramData.CommandType, paramData.QueryString, paramData.CommandTimeout, paramData.SqlParams);
                        }
                    }

                    // Output Parameter Setting
                    int iCnt = 0;
                    char cDiv = (char)1;
                    foreach (SqlParameter p in paramData.SqlParams)
                    {
                        if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
                        {
                            if (iCnt > 0) rt += cDiv;
                            rt += p.Value.ToString();
                        }
                    }

                    scope.Complete(); //트랜잭션 필요 경우 선언
                }

                //실행시간 기록
                _timeStamp.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString(), methodBase);
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, methodBase, "", "ExecuteNonQueryTx");
            }
            finally
            {
            }
            return rt;
        }
        #endregion
    }
}
