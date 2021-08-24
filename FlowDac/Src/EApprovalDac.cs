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

namespace ZumNet.DAL.FlowDac
{
    /// <summary>
    /// 
    /// </summary>
    public class EApprovalDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public EApprovalDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public EApprovalDac(SqlConnection connection) : base(connection)
		{

		}

        #region [양식 관리]
        /// <summary>
        /// 양식 분류 관리
        /// </summary>
        /// <param name="dnID"></param>
        /// <param name="userID"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public DataSet GetEAFormSelect(int dnID, int userID, string admin)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, dnID),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
                ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, admin)
            };

            ParamData pData = new ParamData("admin.ph_up_BFGetEAFormSelect", "", "FormList", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch(Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetEAFormSelect");
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식 정의 가져오기
        /// </summary>
        /// <param name="dnID"></param>
        /// <param name="formID"></param>
        /// <returns></returns>
        public ZumNet.Framework.Entities.Flow.XFormDefinition GetEAFormData(int dnID, string formID)
        {
            ZumNet.Framework.Entities.Flow.XFormDefinition xfDef = null;
            SqlDataReader dr = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@domainid", SqlDbType.Int, 4, dnID),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID)
            };

            ParamData pData = new ParamData("admin.ph_up_BFGetEAFormData", "", parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }

                if (dr.HasRows)
                {
                    xfDef = new ZumNet.Framework.Entities.Flow.XFormDefinition();
                    while (dr.Read())
                    {
                        xfDef.DnID = dnID;
                        xfDef.FormID = formID;
                        xfDef.ClassID = Convert.ToInt32(dr["ClassID"]);
                        xfDef.ProcessID = Convert.ToInt32(dr["ProcessID"]);
                        xfDef.DocName = dr["DocName"].ToString();
                        xfDef.Description = dr["Description"].ToString();
                        xfDef.MainTable = dr["MainTable"].ToString();
                        xfDef.MainTableDef = dr["MainTableDef"].ToString();
                        xfDef.SubTableCount = Convert.ToInt32(dr["SubTableCount"]);
                        xfDef.SubTableDef = dr["SubtableDef"].ToString();
                        xfDef.XslName = dr["XslName"].ToString();
                        xfDef.CssName = dr["CssName"].ToString();
                        xfDef.JsName = dr["JsName"].ToString();
                        xfDef.Version = Convert.ToInt32(dr["Version"]);
                        xfDef.Usage = dr["Usage"].ToString();
                        xfDef.IsCommon = dr["IsCommon"].ToString();
                        xfDef.Selectable = dr["Selectable"].ToString();
                        xfDef.WebEditor = dr["WebEditor"].ToString();
                        xfDef.HtmlFile = dr["HtmlFile"].ToString();
                        xfDef.Limited = dr["Limited"].ToString();
                        xfDef.ProcessNameString = dr["ProcessNameString"].ToString();
                        xfDef.Validation = dr["Validation"].ToString();
                        xfDef.Reserved1 = dr["Reserved1"].ToString();
                        xfDef.Reserved2 = dr["Reserved2"].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetEAFormData");
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }

            return xfDef;
        }

        /// <summary>
        /// 양식 정의 가져오기
        /// </summary>
        /// <param name="dnID"></param>
        /// <param name="formID"></param>
        /// <param name="formTable"></param>
        /// <returns></returns>
        public ZumNet.Framework.Entities.Flow.XFormDefinition GetEAFormData(int dnID, string formID, string formTable)
        {
            ZumNet.Framework.Entities.Flow.XFormDefinition xfDef = null;
             SqlDataReader dr = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@domainid", SqlDbType.Int, 4, dnID),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 100, formTable)
            };

            ParamData pData = new ParamData("admin.ph_up_BFGetEAFormData", "", parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }

                if (dr.HasRows)
                {
                    xfDef = new ZumNet.Framework.Entities.Flow.XFormDefinition();
                    while (dr.Read())
                    {
                        xfDef.DnID = dnID;
                        xfDef.FormID = dr["FormID"].ToString();
                        xfDef.ClassID = Convert.ToInt32(dr["ClassID"]);
                        xfDef.ProcessID = Convert.ToInt32(dr["ProcessID"]);
                        xfDef.DocName = dr["DocName"].ToString();
                        xfDef.Description = dr["Description"].ToString();
                        xfDef.MainTable = dr["MainTable"].ToString();
                        xfDef.MainTableDef = dr["MainTableDef"].ToString();
                        xfDef.SubTableCount = Convert.ToInt32(dr["SubTableCount"]);
                        xfDef.SubTableDef = dr["SubtableDef"].ToString();
                        xfDef.XslName = dr["XslName"].ToString();
                        xfDef.CssName = dr["CssName"].ToString();
                        xfDef.JsName = dr["JsName"].ToString();
                        xfDef.Version = Convert.ToInt32(dr["Version"]);
                        xfDef.Usage = dr["Usage"].ToString();
                        xfDef.IsCommon = dr["IsCommon"].ToString();
                        xfDef.Selectable = dr["Selectable"].ToString();
                        xfDef.WebEditor = dr["WebEditor"].ToString();
                        xfDef.HtmlFile = dr["HtmlFile"].ToString();
                        xfDef.Limited = dr["Limited"].ToString();
                        xfDef.ProcessNameString = dr["ProcessNameString"].ToString();
                        xfDef.Validation = dr["Validation"].ToString();
                        xfDef.Reserved1 = dr["Reserved1"].ToString();
                        xfDef.Reserved2 = dr["Reserved2"].ToString();
                    }
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetEAFormData");
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }

            return xfDef;
        }
        #endregion

        #region [문서 생성 및 변경, 조회]
        /// <summary>
        /// xfalias와 messageid에 해당하는 게시물을 조회
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public DataSet SelectXFMainData(string xfAlias, int messageID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectXFMainData", "", "XFMainData", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectXFMainData");
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식 공통 테이블 정보 가져오기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public ZumNet.Framework.Entities.Flow.XFormInstance SelectXFMainEntity(string xfAlias, int messageID)
        {
            ZumNet.Framework.Entities.Flow.XFormInstance xfInst = null;
            SqlDataReader dr = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectXFMainData", "", parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dr = db.ExecuteReaderNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }

                if (dr.HasRows)
                {
                    xfInst = new ZumNet.Framework.Entities.Flow.XFormInstance();
                    while (dr.Read())
                    {
                        xfInst.MessageID = messageID;
                        xfInst.FormID = dr["FormID"].ToString();
                        xfInst.DocName = dr["DocName"].ToString();
                        xfInst.MsgType = dr["MsgType"].ToString();
                        xfInst.Inherited = dr["Inherited"].ToString();
                        xfInst.Priority = dr["Priority"].ToString();
                        xfInst.Secret = dr["Secret"].ToString();
                        xfInst.DocStatus = dr["DocStatus"].ToString();
                        xfInst.DocNumber = dr["DocNumber"].ToString();
                        xfInst.FormID = dr["FormID"].ToString();
                        xfInst.DocLevel = (dr.IsDBNull(9)) ? 0 : Convert.ToInt32(dr[9]);
                        xfInst.KeepYear = (dr.IsDBNull(10)) ? 0 : Convert.ToInt32(dr[10]);
                        xfInst.Subject = dr["Subject"].ToString();
                        xfInst.Creator = dr["Creator"].ToString();
                        xfInst.CreatorID = Convert.ToInt32(dr["CreatorID"]);
                        xfInst.CreatorCN = dr["CreatorCN"].ToString();
                        xfInst.CreatorEmpNo = dr["CreatorEmpNo"].ToString();
                        xfInst.CreatorPhone = dr["CreatorPhone"].ToString();
                        xfInst.CreatorGrade = dr["CreatorGrade"].ToString();
                        xfInst.CreatorDept = dr["CreatorDept"].ToString();
                        xfInst.CreatorDeptID = Convert.ToInt32(dr["CreatorDeptID"]);
                        xfInst.CreatorDeptCode = dr["CreatorDeptCode"].ToString();
                        xfInst.CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm");
                        xfInst.PublishDate = Convert.ToDateTime(dr["PublishDate"]).ToString("yyyy-MM-dd HH:mm");
                        xfInst.ExpiredDate = (dr.IsDBNull(24)) ? "" : Convert.ToDateTime(dr[24]).ToString("yyyy-MM-dd HH:mm");
                        xfInst.HasAttachFile = dr["HasAttachFile"].ToString();
                        xfInst.LinkedMsg = dr["LinkedMsg"].ToString();
                        xfInst.CommentCount = Convert.ToInt32(dr["CommentCount"]);
                        xfInst.ViewCount = Convert.ToInt32(dr["ViewCount"]);
                        xfInst.EvalCount = Convert.ToInt32(dr["EvalCount"]);
                        xfInst.Transfer = dr["Transfer"].ToString();
                        xfInst.Tms = dr["TMS"].ToString();
                        xfInst.DataLogging = dr["DataLogging"].ToString();
                        xfInst.PrevWork = long.Parse(dr["PrevWork"].ToString());
                        xfInst.NextWork = dr["NextWork"].ToString();
                        xfInst.ExternalKey1 = dr["ExternalKey1"].ToString();
                        xfInst.ExternalKey2 = dr["ExternalKey2"].ToString();
                        xfInst.Reserved1 = dr["Reserved1"].ToString();
                        xfInst.Reserved2 = dr["Reserved2"].ToString();
                    }
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectXFMainEntity");
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }

            return xfInst;
        }

        /// <summary>
        /// 양식별 데이터 가져오기
        /// </summary>
        /// <param name="dbName">양식 데이타베이스</param>
        /// <param name="messageID">양식 식별자</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="version">버전명</param>
        /// <param name="subTableCount">하위테이블 수</param>
        /// <returns></returns>
        public DataSet SelectFormData(string dbName, int messageID, string tableName, int version, int subTableCount)
        {
            DataSet dsReturn = null;
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_SelectFormData";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@subtable_cnt", SqlDbType.Int, 4, subTableCount)
            };

            ParamData pData = new ParamData(strSP, "", "FormData", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectFormData");
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식별 특정 필드 데이타 가져오기
        /// </summary>
        /// <param name="dbName">양식 데이타베이스</param>
        /// <param name="messageID">양식 식별자</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="version">버전명</param>
        /// <param name="fieldName">가져올 필드명</param>
        /// <returns></returns>
        public string GetMainFormSpecificData(string dbName, int messageID, string tableName, int version, string fieldName)
        {
            string strReturn = "";
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_GetMainFormSpecificData";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@field", SqlDbType.VarChar, 100, fieldName)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetMainFormSpecificData");
            }

            return strReturn;
        }

        /// <summary>
        /// 특정 외부키에 해당하는 게시물 식별자를 가져오기
        /// </summary>
        /// <param name="externalKey1"></param>
        /// <param name="xfAlias"></param>
        /// <param name="formID"></param>
        /// <returns></returns>
        public ArrayList SelectRecordData(string externalKey1, string xfAlias, string formID)
        {
            ArrayList rowList = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 255, externalKey1),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID)
            };

            ParamData pData = new ParamData("ph_up_SelectRecordData", "", "RecordData", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    rowList = db.ExecuteListNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectRecordData");
            }

            return rowList;
        }

        /// <summary>
        /// 특정 외부키에 해당하는 양식 기본 정보를 가져오기 (MessageID, TableName)
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="ekpDB"></param>
        /// <param name="formTable"></param>
        /// <param name="externalKey1"></param>
        /// <returns></returns>
        public DataTable SelectExternalKeyMsg(string dbName, string ekpDB, string formTable, string externalKey1)
        {
            DataSet ds = null;
            DataTable dtReturn = null;

            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_SelectExternalKeyMsg";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.NVarChar, 50, ekpDB),
                ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 100, formTable),
                ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 255, externalKey1)
            };

            ParamData pData = new ParamData(strSP, "", "ExternalKeyMsg", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
                if (ds != null && ds.Tables.Count > 0) dtReturn = ds.Tables[0];
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectExternalKeyMsg");
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return dtReturn;
        }

        /// <summary>
        /// 특정 외부키에 해당하는 양식 정보를 가져오기
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="ekpDB"></param>
        /// <param name="messageId"></param>
        /// <param name="oId"></param>
        /// <param name="formID"></param>
        /// <param name="formTable"></param>
        /// <param name="version"></param>
        /// <param name="externalKey1"></param>
        /// <returns></returns>
        public DataSet SelectExternalData(string dbName, string ekpDB, int messageId, int oId, string formID, string formTable, int version, string externalKey1)
        {
            DataSet dsReturn = null;
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_SelectExternalData";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.NVarChar, 50, ekpDB),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageId),
                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oId),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 100, formTable),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 255, externalKey1)
            };

            ParamData pData = new ParamData(strSP, "", "ExternalData", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectExternalData");
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식 데이터 이력 가져오기
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="messageID"></param>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public DataSet SelectHistoryData(string dbName, int messageID, string tableName, int version)
        {
            DataSet dsReturn = null;
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_SelectHistory";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version)
            };

            ParamData pData = new ParamData(strSP, "", "HistoryData", 30, parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "SelectHistoryData");
            }

            return dsReturn;
        }
        #endregion

        #region [겸직부서 가져오기]

        /// <summary>
        /// 겸직부서 가져오기
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet RetrieveDeptInfo(int userId)
        {
			DataSet dsReturn = null;
			string strQuery = "SELECT GR_ID AS DeptID, GRAlias AS DeptAlias, GroupName AS DeptName, Role, Grade1, Grade2 FROM admin.ph_VIEW_OBJECT_UR_LIST (NOLOCK) WHERE UserID = @urid";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userId)
			};

			ParamData pData = new ParamData(strQuery, "text", parameters);

            try
            {
                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "RetrieveDeptInfo");
            }

            return dsReturn;
		}
		#endregion
	}
}
