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

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
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

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
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

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식 특정 필드 값 가져오기 - reserved2 정보를 가져오기 위함
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string SelectEAFormField(string formId, string field)
        {
            string strReturn = "";
            string strQuery = "SELECT " + field + " FROM admin.PH_EA_FORMS (NOLOCK) WHERE FormID = @formid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formId)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
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

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
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

            ParamData pData = new ParamData("admin.ph_up_SelectRecordData", "", "RecordData", 30, parameters);

            using (DbBase db = new DbBase())
            {
                rowList = db.ExecuteListNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
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
        public DataSet SelectExternalKeyMsg(string dbName, string ekpDB, string formTable, string externalKey1)
        {
            DataSet dsReturn = null;

            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_SelectExternalKeyMsg";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.NVarChar, 50, ekpDB),
                ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 100, formTable),
                ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 255, externalKey1)
            };

            ParamData pData = new ParamData(strSP, "", "ExternalKeyMsg", 30, parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
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

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
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

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식에서 미리 정의된 표기 또는 내용 등을 가져오기
        /// </summary>
        /// <param name="dnId"></param>
        /// <param name="formTable"></param>
        /// <returns></returns>
        public DataTable GetFormOptionInfo(int dnId, string formTable)
        {
            return GetFormOptionInfo(dnId, formTable, "");
        }

        /// <summary>
        /// 양식에서 미리 정의된 표기 또는 내용 등을 가져오기
        /// </summary>
        /// <param name="dnId"></param>
        /// <param name="formTable"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable GetFormOptionInfo(int dnId, string formTable, string option)
        {
            DataSet ds = null;
            DataTable dtReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dnid", SqlDbType.TinyInt, 1, dnId),
                ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 50, formTable),
                ParamSet.Add4Sql("@option", SqlDbType.NVarChar, 500, option)
            };

            ParamData pData = new ParamData("admin.ph_up_BFGetEAFormOptionInfo", "", parameters);

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
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, MethodBase.GetCurrentMethod(), "", "GetFormOptionInfo");
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
        /// 최초 PH_XF_EA 테이블 값 저장
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="DocName"></param>
        /// <param name="MsgType"></param>
        /// <param name="Inherited"></param>
        /// <param name="Priority"></param>
        /// <param name="Secret"></param>
        /// <param name="DocStatus"></param>
        /// <param name="DocNumber"></param>
        /// <param name="DocLevel"></param>
        /// <param name="KeepYear"></param>
        /// <param name="Subject"></param>
        /// <param name="Creator"></param>
        /// <param name="CreatorID"></param>
        /// <param name="CreatorCN"></param>
        /// <param name="CreatorEmpNo"></param>
        /// <param name="CreatorPhone"></param>
        /// <param name="CreatorGrade"></param>
        /// <param name="CreatorDept"></param>
        /// <param name="CreatorDeptID"></param>
        /// <param name="CreatorDeptCode"></param>
        /// <param name="PublishDate"></param>
        /// <param name="ExpiredDate"></param>
        /// <param name="HasAttachFile"></param>
        /// <param name="LinkedMsg"></param>
        /// <param name="Transfer"></param>
        /// <param name="TMS"></param>
        /// <param name="prevWork"></param>
        /// <param name="nextWork"></param>
        /// <param name="ExternalKey1"></param>
        /// <param name="ExternalKey2"></param>
        /// <param name="Reserved1"></param>
        /// <param name="Reserved2"></param>
        /// <returns></returns>
        public int InsertXFEA(string FormID, string DocName, string MsgType, string Inherited, string Priority, string Secret, string DocStatus
                    , string DocNumber, int DocLevel, int KeepYear, string Subject, string Creator, int CreatorID, string CreatorCN, string CreatorEmpNo, string CreatorPhone
                    , string CreatorGrade, string CreatorDept, int CreatorDeptID, string CreatorDeptCode, string PublishDate, string ExpiredDate, string HasAttachFile
                    , string LinkedMsg, string Transfer, string TMS, long prevWork, string nextWork, string ExternalKey1, string ExternalKey2, string Reserved1, string Reserved2)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formid", SqlDbType.NVarChar, 33, FormID),
                ParamSet.Add4Sql("@docname", SqlDbType.NVarChar, 100, DocName),
                ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, MsgType),
                ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, Inherited),
                ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, Priority),
                ParamSet.Add4Sql("@secret", SqlDbType.Char, 1, Secret),
                ParamSet.Add4Sql("@docstatus", SqlDbType.Char, 3, DocStatus),
                ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, DocNumber),
                ParamSet.Add4Sql("@doclevel", SqlDbType.Int, 4, DocLevel),
                ParamSet.Add4Sql("@keepyear", SqlDbType.Int, 4, KeepYear),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, Subject),
                ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, Creator),
                ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, CreatorID),
                ParamSet.Add4Sql("@creatorcn", SqlDbType.VarChar, 63, CreatorCN),
                ParamSet.Add4Sql("@creatorempno", SqlDbType.VarChar, 63, CreatorEmpNo),
                ParamSet.Add4Sql("@creatorphone", SqlDbType.VarChar, 30, CreatorPhone),
                ParamSet.Add4Sql("@creatorgrade", SqlDbType.NVarChar, 50, CreatorGrade),
                ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, CreatorDept),
                ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, CreatorDeptID),
                ParamSet.Add4Sql("@creatordeptcode", SqlDbType.VarChar, 63, CreatorDeptCode),
                ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, PublishDate),
                ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, ExpiredDate),
                ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, HasAttachFile),
                ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, LinkedMsg),
                ParamSet.Add4Sql("@transfer", SqlDbType.Char, 1, Transfer),
                ParamSet.Add4Sql("@tms", SqlDbType.VarChar, 50, TMS),
                ParamSet.Add4Sql("@prevwork", SqlDbType.BigInt, prevWork),
                ParamSet.Add4Sql("@nextwork", SqlDbType.Char, 1, nextWork),
                ParamSet.Add4Sql("@externalkey1", SqlDbType.NVarChar, 255, ExternalKey1),
                ParamSet.Add4Sql("@externalkey2", SqlDbType.NVarChar, 255, ExternalKey2),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, Reserved1),
                ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, Reserved2),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertXFEA", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// PH_XF_EA 테이블 값 변경(임시저장함에서 수정저장)
        /// </summary>
        /// <param name="MessageID"></param>
        /// <param name="MsgType"></param>
        /// <param name="Inherited"></param>
        /// <param name="Priority"></param>
        /// <param name="Secret"></param>
        /// <param name="docStatus"></param>
        /// <param name="DocLevel"></param>
        /// <param name="KeepYear"></param>
        /// <param name="Subject"></param>
        /// <param name="PublishDate"></param>
        /// <param name="ExpiredDate"></param>
        /// <param name="HasAttachFile"></param>
        /// <param name="LinkedMsg"></param>
        /// <param name="Transfer"></param>
        /// <param name="TMS"></param>
        /// <param name="DataLogging"></param>
        /// <param name="prevWork"></param>
        /// <param name="nextWork"></param>
        /// <param name="ExternalKey1"></param>
        /// <param name="ExternalKey2"></param>
        /// <param name="Reserved1"></param>
        /// <param name="Reserved2"></param>
        public void UpdateXFEA(int MessageID, string MsgType, string Inherited, string Priority, string Secret, string docStatus
            , int DocLevel, int KeepYear, string Subject, string PublishDate, string ExpiredDate, string HasAttachFile, string LinkedMsg, string Transfer
            , string TMS, string DataLogging, long prevWork, string nextWork, string ExternalKey1, string ExternalKey2, string Reserved1, string Reserved2)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, MessageID),
                ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, MsgType),
                ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, Inherited),
                ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, Priority),
                ParamSet.Add4Sql("@secret", SqlDbType.Char, 1, Secret),
                ParamSet.Add4Sql("@docstatus", SqlDbType.Char, 3, docStatus),
                ParamSet.Add4Sql("@doclevel", SqlDbType.Int, 4, DocLevel),
                ParamSet.Add4Sql("@keepyear", SqlDbType.Int, 4, KeepYear),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, Subject),
                ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, PublishDate),
                ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, ExpiredDate),
                ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, HasAttachFile),
                ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, LinkedMsg),
                ParamSet.Add4Sql("@transfer", SqlDbType.Char, 1, Transfer),
                ParamSet.Add4Sql("@tms", SqlDbType.VarChar, 50, TMS),
                ParamSet.Add4Sql("@datalogging", SqlDbType.Char, 1, DataLogging),
                ParamSet.Add4Sql("@prevwork", SqlDbType.BigInt, prevWork),
                ParamSet.Add4Sql("@nextwork", SqlDbType.Char, 1, nextWork),
                ParamSet.Add4Sql("@externalkey1", SqlDbType.NVarChar, 255, ExternalKey1),
                ParamSet.Add4Sql("@externalkey2", SqlDbType.NVarChar, 255, ExternalKey2),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, Reserved1),
                ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, Reserved2)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateXFEA", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 양식별 데이타 저장
        /// </summary>
        /// <param name="dbName">양식 데이타베이스</param>
        /// <param name="messageID">양식 식별자</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="version">버전명</param>
        /// <param name="subTableCount">하위테이블 수</param>
        /// <param name="tableDef">메인테이블 필드 정의</param>
        /// <param name="subTableDef">하위테이블 필드 정의</param>
        /// <param name="mainField">메인테이블 저장 XML 데이타</param>
        /// <param name="subField">하위테이블  저장 XML 데이타</param>
        public void InsertFormData(string dbName, int messageID, string tableName, int version, int subTableCount
                                , string tableDef, string subTableDef, string mainField, string subField)
        {
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_InsertFormData";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@subtable_cnt", SqlDbType.Int, 4, subTableCount),
                ParamSet.Add4Sql("@table_def", SqlDbType.NText, tableDef),
                ParamSet.Add4Sql("@subtable_def", SqlDbType.NText, subTableDef),
                ParamSet.Add4Sql("@main_field", SqlDbType.NText, mainField),
                ParamSet.Add4Sql("@sub_field", SqlDbType.NText, subField)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 양식별 필드 값 변경
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="msgID"></param>
        /// <param name="tableName"></param>
        /// <param name="actorKey"></param>
        /// <param name="actor"></param>
        /// <param name="actorID"></param>
        /// <param name="field"></param>
        /// <param name="fieldLabel"></param>
        /// <param name="fieldValue"></param>
        public void UpdateFormMainData(string dbName, int msgID, string tableName, string actorKey, string actor
                                    , int actorID, string field, string fieldLabel, string fieldValue)
        {
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_UpdateFormMainData";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@actorkey", SqlDbType.VarChar, 33, actorKey),
                ParamSet.Add4Sql("@actor", SqlDbType.NVarChar, 100, actor),
                ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
                ParamSet.Add4Sql("@field", SqlDbType.VarChar, 100, field),
                ParamSet.Add4Sql("@field_label", SqlDbType.NVarChar, 100, fieldLabel),
                ParamSet.Add4Sql("@field_value", SqlDbType.NText, fieldValue)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 양식별 데이타 삭제
        /// </summary>
        /// <param name="dbName">양식 데이타베이스</param>
        /// <param name="messageID">양식 식별자</param>
        /// <param name="tableName">테이블명</param>
		/// <param name="version">버전명</param>
		/// <param name="subTableCount">하위테이블 수</param>
        public void DeleteFormData(string dbName, int messageID, string tableName, int version, int subTableCount)
        {
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_DeleteFormData";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@subtable_cnt", SqlDbType.Int, 4, subTableCount)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 자동 양식 생성을 위한 데이타 가져오기
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="formTable"></param>
        /// <param name="ver"></param>
        /// <param name="subCnt"></param>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="key3"></param>
        /// <param name="key4"></param>
        /// <param name="key5"></param>
        /// <returns></returns>
        public DataSet SelectFormDataForDraft(string dbName, string formTable, int ver, int subCnt
                                    , string key1, string key2, string key3, string key4, string key5)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, formTable),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, ver),
                ParamSet.Add4Sql("@subtable_cnt", SqlDbType.Int, 4, subCnt),
                ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 50, key1),
                ParamSet.Add4Sql("@key2", SqlDbType.NVarChar, 50, key2),
                ParamSet.Add4Sql("@key3", SqlDbType.NVarChar, 50, key3),
                ParamSet.Add4Sql("@key4", SqlDbType.NVarChar, 50, key4),
                ParamSet.Add4Sql("@key5", SqlDbType.NVarChar, 50, key5)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectFormDataForDraft", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [관련문서 및 타시스템 이관 관련]
        /// <summary>
        /// 폴더에 양식 등록
        /// </summary>
        /// <param name="fdID"></param>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="parentMsgID"></param>
        /// <param name="attType"></param>
        /// <param name="state"></param>
        /// <param name="taskActivity"></param>
        /// <param name="reserved1"></param>
        public void InsertXFormFD(int fdID, string xfAlias, string messageID, int parentMsgID, string attType, int state, string taskActivity, string reserved1)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@parentmsgid", SqlDbType.Int, 4, parentMsgID),
                ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType),
                ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
                ParamSet.Add4Sql("@taskactivity", SqlDbType.VarChar, 33, taskActivity),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertXFormFD", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 양식 폴더 등록 정보 변경
        /// </summary>
        /// <param name="fdID"></param>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="attType"></param>
        /// <param name="taskActivity"></param>
        /// <param name="reserved1"></param>
        public void UpdateXFormFD(int fdID, string xfAlias, string messageID, string attType, string taskActivity, string reserved1)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType),
                ParamSet.Add4Sql("@taskactivity", SqlDbType.VarChar, 33, taskActivity),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateXFormFD", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 관련문서 정보 저장하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="linkedXfAlias"></param>
        /// <param name="linkedMsgID"></param>
        /// <param name="linkedSubject"></param>
        /// <param name="linkedReserved1"></param>
        /// <param name="linkedReserved2"></param>
        public void InsertLinkedDoc(string xfAlias, string messageID, string linkedXfAlias, string linkedMsgID
                                , string linkedSubject, string linkedReserved1, string linkedReserved2)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@linkedxfalias", SqlDbType.VarChar, 30, linkedXfAlias),
                ParamSet.Add4Sql("@linkedmsgid", SqlDbType.VarChar, 33, linkedMsgID),
                ParamSet.Add4Sql("@linkedsubject", SqlDbType.NVarChar, 200, linkedSubject),
                ParamSet.Add4Sql("@linkedreserved1", SqlDbType.NVarChar, 255, linkedReserved1),
                ParamSet.Add4Sql("@linkedreserved2", SqlDbType.NVarChar, 500, linkedReserved2)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertLinkedDoc", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 관련문서 정보 배치 저장하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="xmlData"></param>
        public void InsertLinkedDoc(string xfAlias, string messageID, string xmlData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertLinkedDocBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 관련문서 정보 배치 변경하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="xmlData"></param>
        public void UpdateLinkedDocBatch(string xfAlias, string messageID, string xmlData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateLinkedDocBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 관련문서 정보 가져오기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public DataSet SelectLinkedDoc(string xfAlias, string messageID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectLinkedDoc", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 이관정보 저장하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="targetXfAlias"></param>
        /// <param name="attType"></param>
        /// <param name="targetFolder"></param>
        /// <param name="targetDesc"></param>
        /// <param name="reserved1"></param>
        public void InsertXFormTransfer(string xfAlias, string messageID, string targetXfAlias, string attType
                                    , int targetFolder, string targetDesc, string reserved1)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@targetxfalias", SqlDbType.VarChar, 30, targetXfAlias),
                ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType),
                ParamSet.Add4Sql("@targetfolder", SqlDbType.Int, 4, targetFolder),
                ParamSet.Add4Sql("@targetdesc", SqlDbType.NVarChar, 200, targetDesc),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 500, reserved1)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertXFormTransfer", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 이관정보 배치 저장하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="xmlData"></param>
        public void InsertXFormTransfer(string xfAlias, string messageID, string xmlData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertXFormTransferBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 이관 정보 가져오기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public DataSet SelectXFormTransfer(string xfAlias, string messageID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectXFormTransfer", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [공동등록자]
        /// <summary>
        /// 공동 등록자 생성
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="actorID"></param>
        /// <param name="actType"></param>
        /// <param name="actor"></param>
        /// <param name="actorCN"></param>
        /// <param name="actorGrade"></param>
        /// <param name="actorDept"></param>
        /// <param name="actorDeptID"></param>
        /// <param name="actorDeptCode"></param>
        /// <param name="reserved1"></param>
        public void InsertXFormRegistrant( string xfAlias, int messageID, int actorID, string actType, string actor, string actorCN
                                    , string actorGrade, string actorDept, int actorDeptID, string actorDeptCode, string reserved1)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
                ParamSet.Add4Sql("@acttype", SqlDbType.Char, 1, actType),
                ParamSet.Add4Sql("@actor", SqlDbType.NVarChar, 100, actor),
                ParamSet.Add4Sql("@actorcn", SqlDbType.VarChar, 30, actorCN),
                ParamSet.Add4Sql("@actorgrade", SqlDbType.NVarChar, 50, actorGrade),
                ParamSet.Add4Sql("@actordept", SqlDbType.NVarChar, 200, actorDept),
                ParamSet.Add4Sql("@actordeptid", SqlDbType.Int, 4, actorDeptID),
                ParamSet.Add4Sql("@actordeptcode", SqlDbType.VarChar, 30, actorDeptCode),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertXFormRegistrant", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 공동등록자 배치 저장하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="xmlData"></param>
        public void InsertXFormRegistrantBatch(string xfAlias, int messageID, string xmlData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertXFormRegistrantBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 공동등록자 배치 변경하기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="xmlData"></param>
        public void UpdateXFormRegistrantBatch(string xfAlias, int messageID, string xmlData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateXFormRegistrantBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 공동 등록자 가져오기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public DataSet SelectXFormRegistrant(string xfAlias, string messageID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectXFormRegistrant", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [이벤트 기록]
        /// <summary>
        /// 최초 PH_EVENT_REGISTRATION 테이블 값 저장
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="actorID"></param>
        /// <param name="actType"></param>
        /// <param name="actDate"></param>
        public void InsertEventRegistration(string xfAlias, int messageID, int actorID, string actType, string actDate)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actorID),
                ParamSet.Add4Sql("@acttype", SqlDbType.Char, 1, actType),
                ParamSet.Add4Sql("@actdate", SqlDbType.VarChar, 20, actDate),
            };

            ParamData pData = new ParamData("admin.ph_up_InsertEventRegistration", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 최초 PH_EVENT_EDIT 테이블 값 저장
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="actorID"></param>
        public void InsertEventEdit(string xfAlias, int messageID, int actorID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actorID)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertEventEdit", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 조회 로그 저장
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="fdID"></param>
        /// <param name="actorID"></param>
        /// <param name="messageID"></param>
        /// <param name="ip"></param>
        public void InsertEventView(string xfAlias, int fdID, int actorID, int messageID, string ip)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, fdID),
                ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, actorID),
                ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@IP", SqlDbType.VarChar, 30, ip),
            };

            ParamData pData = new ParamData("admin.ph_up_MsgSetViewEvent", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 조회 기록 설정
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="viewer"></param>
        public void SetEventView(string mode, string xfAlias, int messageID, int viewer)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 30, mode),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@viewer", SqlDbType.Int, 4, viewer)
            };

            ParamData pData = new ParamData("admin.ph_up_SetEventView", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [첨부 파일 관련]
        /// <summary>
        /// 스토리지 LocID값을 가져옴
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="xfAlias"></param>
        /// <param name="fileOrImg"></param>
        /// <returns></returns>
        public int GetMessageLocID(int domainID, string xfAlias, string fileOrImg)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID),
                ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@FileOrImg", SqlDbType.VarChar, 6, fileOrImg),
                ParamSet.Add4Sql("@LocID", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_MsgGetLocID", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 첨부파일이 저장될 경로 가져오기
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="xfAlias"></param>
        /// <returns></returns>
        public DataSet GetAttachPath(int domainID, string xfAlias)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
            };

            ParamData pData = new ParamData("admin.ph_up_MsgGetAttachPath", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 최초 PH_FILE_ATTACH 테이블 값 저장
        /// </summary>
        public int InsertFileAttach(string xfAlias, int messageID, string isFile, string fileName, string savedName
                        , string fileSize, string fileType, string prefix, string locationFolder, string storageFolder)
        {
            return InsertFileAttach(xfAlias, messageID, isFile, fileName, savedName, fileSize, fileType, prefix, locationFolder, storageFolder, "", 0);
        }

        /// <summary>
        /// 최초 PH_FILE_ATTACH 테이블 값 저장
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="isFile"></param>
        /// <param name="fileName"></param>
        /// <param name="savedName"></param>
        /// <param name="fileSize"></param>
        /// <param name="fileType"></param>
        /// <param name="prefix"></param>
        /// <param name="locationFolder"></param>
        /// <param name="storageFolder"></param>
        /// <param name="drm"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public int InsertFileAttach(string xfAlias, int messageID, string isFile, string fileName, string savedName, string fileSize
                                , string fileType, string prefix, string locationFolder, string storageFolder, string drm, int seq)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@isfile", SqlDbType.Char, 1, isFile),
                ParamSet.Add4Sql("@filename   ", SqlDbType.NVarChar, 100, fileName),
                ParamSet.Add4Sql("@savedname", SqlDbType.NVarChar, 100, savedName),
                ParamSet.Add4Sql("@filesize", SqlDbType.VarChar, 20, fileSize),
                ParamSet.Add4Sql("@filetype", SqlDbType.VarChar, 10, fileType),
                ParamSet.Add4Sql("@prefix", SqlDbType.VarChar, 15, prefix),
                ParamSet.Add4Sql("@locationfolder", SqlDbType.VarChar, 15, locationFolder),
                ParamSet.Add4Sql("@storagefolder", SqlDbType.NVarChar, 250, storageFolder),
                ParamSet.Add4Sql("@drm", SqlDbType.VarChar, 20, drm),
                ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertFileAttach", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 최초 PH_FILE_ATTACH 테이블 값 배치로 저장
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="fileInfo"></param>
        public void InsertFileAttach(string xfAlias, string messageID, string fileInfo)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
                ParamSet.Add4Sql("@fileinfo", SqlDbType.NText, fileInfo)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertFileAttachBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 첨부파일 쿼리
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public DataSet SelectAttachFile(int domainID, string xfAlias, int messageID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectFileAttach", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 저장된 파일명으로 해당 파일 경로 가져오기
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="savedName"></param>
        /// <returns></returns>
        public DataSet GetAttachedFileInfo(string xfAlias, string savedName)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@savedname", SqlDbType.NVarChar, 100, savedName)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectFileAttachWithSavedName", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 첨부파일 삭제
        /// </summary>
        /// <param name="attachID"></param>
        /// <param name="xfAlias"></param>
        public void DeleteAttachFile(int attachID, string xfAlias)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@attachid", SqlDbType.Int, 4, attachID),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
            };

            ParamData pData = new ParamData("admin.ph_up_DelAttachFile", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 최초 PH_FILE_ATTACH 테이블 값 배치로 저장
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        public void UpdateXFFileInfo(string xfAlias, int messageID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateXFFileInfo", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 파일 크기 정보 변경
        /// </summary>
        /// <param name="attachId"></param>
        /// <param name="fileSize"></param>
        public void UpdateFileSize(int attachId, string fileSize)
        {
            string strQuery = "UPDATE admin.PH_FILE_ATTACH WITH (ROWLOCK) SET FileSize = @filesize WHERE AttachID = @attachid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@attachid", SqlDbType.Int, 4, attachId),
                ParamSet.Add4Sql("@filesize", SqlDbType.VarChar, 20, fileSize)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 첨부파일 조회 로그 기록
        /// </summary>
        /// <param name="attachId"></param>
        /// <param name="fileName"></param>
        /// <param name="savedName"></param>
        /// <param name="realPath"></param>
        /// <param name="userId"></param>
        /// <param name="logonId"></param>
        /// <param name="userName"></param>
        /// <param name="ip"></param>
        /// <param name="browser"></param>
        /// <param name="disableSecu"></param>
        public void InsertEventFileView(int attachId, string fileName, string savedName, string realPath, int userId
                                    , string logonId, string userName, string ip, string browser, string disableSecu)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@attachid", SqlDbType.Int, 4, attachId),
                ParamSet.Add4Sql("@filename", SqlDbType.NVarChar, 100, fileName),
                ParamSet.Add4Sql("@savedname", SqlDbType.NVarChar, 100, savedName),
                ParamSet.Add4Sql("@realpath", SqlDbType.NVarChar, 250, realPath),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId),
                ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 30, logonId),
                ParamSet.Add4Sql("@username", SqlDbType.NVarChar, 100, userName),
                ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 30, ip),
                ParamSet.Add4Sql("@browser", SqlDbType.VarChar, 200, browser),
                ParamSet.Add4Sql("@disablesecu", SqlDbType.Char, 1, disableSecu)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertEventFileView", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 첨부파일 조회 기록 불러오기
        /// </summary>
        /// <param name="attachId"></param>
        /// <param name="realPath"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet SelectEventFileView(int attachId, string realPath, int userId)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@attachid", SqlDbType.Int, 4, attachId),
                ParamSet.Add4Sql("@realpath", SqlDbType.NVarChar, 250, realPath),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectEventFileView", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [외부시스템 연동 관련]
        /// <summary>
        /// 연동 시스템에 보낼 데이타 가져오기
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="moduleID"></param>
        /// <param name="ekpDB"></param>
        /// <param name="formID"></param>
        /// <param name="wID"></param>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <param name="msgID"></param>
        /// <param name="oID"></param>
        /// <returns></returns>
        public string GetExtraDataForInterface(string dbName, string moduleID, string ekpDB, string formID
                                        , string wID, string tableName, int version, int msgID, int oID)
        {
            string strReturn = "";
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_GetExtraDataForInterface";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
                ParamSet.Add4Sql("@ekp_db", SqlDbType.NVarChar, 200, ekpDB),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@wid", SqlDbType.VarChar, 33, wID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgID),
                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID)
            };

            ParamData pData = new ParamData(strSP, "", "ExtraData", 30, parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 연동 시스템에 받은 데이타로 양식 필드들 변경
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="moduleID"></param>
        /// <param name="dnAlias"></param>
        /// <param name="formID"></param>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <param name="msgID"></param>
        /// <param name="interfaceValue"></param>
        public void UpdateFormMainFieldForInterface(string dbName, string moduleID, string dnAlias, string formID
                                            , string tableName, int version, int msgID, string interfaceValue)
        {
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_UpdateFormMainFieldForInterface";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
                ParamSet.Add4Sql("@dnalias", SqlDbType.VarChar, 63, dnAlias),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgID),
                ParamSet.Add4Sql("@f_value", SqlDbType.NVarChar, 4000, interfaceValue)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 메일(외부로 보내는 경우) 시스템으로 보낼 데이타 가져오기
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="moduleID"></param>
        /// <param name="ekpDB"></param>
        /// <param name="mailDomain"></param>
        /// <param name="formID"></param>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <param name="msgID"></param>
        /// <param name="oID"></param>
        /// <returns></returns>
        public DataSet GetExtraDataForMail(string dbName, string moduleID, string ekpDB, string mailDomain
                                        , string formID, string tableName, int version, int msgID, int oID)
        {
            DataSet dsReturn = null;
            if (dbName != "") dbName += ".";
            string strSP = dbName + "admin.ph_up_GetExtraDataForMail";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@moduleid", SqlDbType.VarChar, 50, moduleID),
                ParamSet.Add4Sql("@ekp_db", SqlDbType.NVarChar, 200, ekpDB),
                ParamSet.Add4Sql("@maildomain", SqlDbType.VarChar, 100, mailDomain),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@table_name", SqlDbType.VarChar, 100, tableName),
                ParamSet.Add4Sql("@version", SqlDbType.Int, 4, version),
                ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, msgID),
                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oID),

                ParamSet.Add4Sql("@return_notice", SqlDbType.Char, 2, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [개인 결재선 관련]
        /// <summary>
        /// 개인 결재선 가져오기
        /// </summary>
        /// <param name="lineID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet SelectEAPersonLine(int lineID, int userID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@lineid", SqlDbType.Int, 4, lineID),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectEAPersonLine", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 개인 결재선 상세정보 가져오기
        /// </summary>
        /// <param name="plID"></param>
        /// <param name="lineID"></param>
        /// <returns></returns>
        public DataSet SelectEAPersonLineDetail(int plID, int lineID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@plid", SqlDbType.Int, 4, plID),
                ParamSet.Add4Sql("@lineid", SqlDbType.Int, 4, lineID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectEAPersonLineDetail", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 개인 결재선 생성
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="processID"></param>
        /// <param name="formID"></param>
        /// <param name="prior"></param>
        /// <param name="lineName"></param>
        /// <param name="description"></param>
        /// <param name="reserved1"></param>
        /// <returns></returns>
        public int InsertEAPersonLine(int userID, int processID, string formID, string prior
                                , string lineName, string description, string reserved1)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
                ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@prior", SqlDbType.Char, 1, prior),
                ParamSet.Add4Sql("@linename", SqlDbType.NVarChar, 200, lineName),
                ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, description),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertEAPersonLine", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 개인 결재선 상세정보 생성
        /// </summary>
        /// <param name="lineID"></param>
        /// <param name="step"></param>
        /// <param name="subStep"></param>
        /// <param name="bizRole"></param>
        /// <param name="actRole"></param>
        /// <param name="partType"></param>
        /// <param name="partID"></param>
        /// <param name="partName"></param>
        /// <param name="partDeptCode"></param>
        /// <param name="part1"></param>
        /// <param name="reserved1"></param>
        /// <returns></returns>
        public int InsertEAPersonLineDetail(int lineID, int step, int subStep, string bizRole, string actRole, string partType
                                        , string partID, string partName, string partDeptCode, string part1, string reserved1)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@lineid", SqlDbType.Int, 4, lineID),
                ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
                ParamSet.Add4Sql("@substgep", SqlDbType.Int, 4, subStep),
                ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
                ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
                ParamSet.Add4Sql("@parttype", SqlDbType.Char, 5, partType),
                ParamSet.Add4Sql("@partid", SqlDbType.VarChar, 63, partID),
                ParamSet.Add4Sql("@partname", SqlDbType.NVarChar, 200, partName),
                ParamSet.Add4Sql("@partdeptcode", SqlDbType.VarChar, 33, partDeptCode),
                ParamSet.Add4Sql("@partinfo1", SqlDbType.NVarChar, 200, part1),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertEAPersonLineDetail", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 개인결재선 상세 정보 배치 저장
        /// </summary>
        /// <param name="lineID"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public void InsertEAPersonLineDetail(int lineID, string xmlData)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@lineid", SqlDbType.Int, 4, lineID),
                ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
            };

            ParamData pData = new ParamData("admin.ph_up_InsertEAPersonLineDetailBatch", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개인 결재선 변경
        /// </summary>
        /// <param name="lineID"></param>
        /// <param name="userID"></param>
        /// <param name="processID"></param>
        /// <param name="formID"></param>
        /// <param name="prior"></param>
        /// <param name="lineName"></param>
        /// <param name="description"></param>
        /// <param name="reserved1"></param>
        /// <returns></returns>
        public void UpdateEAPersonLine(int lineID, int userID, int processID, string formID
                            , string prior, string lineName, string description, string reserved1)
        {
           SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@lineid", SqlDbType.Int, 4, lineID),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
                ParamSet.Add4Sql("@processid", SqlDbType.Int, 4, processID),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@prior", SqlDbType.Char, 1, prior),
                ParamSet.Add4Sql("@linename", SqlDbType.NVarChar, 200, lineName),
                ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, description),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateEAPersonLine", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개인 결재선 상세정보 변경
        /// </summary>
        /// <param name="plID"></param>
        /// <param name="step"></param>
        /// <param name="subStep"></param>
        /// <param name="bizRole"></param>
        /// <param name="actRole"></param>
        /// <param name="partType"></param>
        /// <param name="partID"></param>
        /// <param name="partName"></param>
        /// <param name="partDeptCode"></param>
        /// <param name="part1"></param>
        /// <param name="reserved1"></param>
        public void UpdateEAPersonLineDetail(int plID, int step, int subStep, string bizRole, string actRole, string partType
                                        , string partID, string partName, string partDeptCode, string part1, string reserved1)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@plid", SqlDbType.Int, 4, plID),
                ParamSet.Add4Sql("@step", SqlDbType.Int, 4, step),
                ParamSet.Add4Sql("@substgep", SqlDbType.Int, 4, subStep),
                ParamSet.Add4Sql("@bizrole", SqlDbType.VarChar, 30, bizRole),
                ParamSet.Add4Sql("@actrole", SqlDbType.VarChar, 30, actRole),
                ParamSet.Add4Sql("@parttype", SqlDbType.Char, 5, partType),
                ParamSet.Add4Sql("@partid", SqlDbType.VarChar, 63, partID),
                ParamSet.Add4Sql("@partname", SqlDbType.NVarChar, 200, partName),
                ParamSet.Add4Sql("@partdeptcode", SqlDbType.VarChar, 33, partDeptCode),
                ParamSet.Add4Sql("@partinfo1", SqlDbType.NVarChar, 200, part1),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateEAPersonLineDetail", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개인 결재선 삭제, 복구, 완전 제거
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="lineID"></param>
        /// <param name="userID"></param>
        public void DeleteEAPersonLine(string mode, string lineID, int userID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@lineid", SqlDbType.VarChar, 3500, lineID),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
            };

            ParamData pData = new ParamData("admin.ph_up_DeleteEAPersonLine", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개인 결재선 삭제, 복구, 완전 제거
        /// </summary>
        /// <param name="plID"></param>
        /// <param name="lineID"></param>
        public void DeleteEAPersonLineDetail(int plID, int lineID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@plid", SqlDbType.Int, 4, plID),
                ParamSet.Add4Sql("@lineid", SqlDbType.Int, 4, lineID)
            };

            ParamData pData = new ParamData("admin.ph_up_DeleteEAPersonLineDetail", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [결재 이외 양식 생성 및 변경, 조회]
        /// <summary>
        /// 결재 이외 양식 가져오기
        /// </summary>
        /// <param name="fiid"></param>
        /// <param name="xfalias"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public DataSet SelectFormDataNotEA(string fiid, string xfalias, string formId)
        {
            DataSet dsReturn = null;

            string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
            string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);

            if (strFormDB != "") strFormDB += ".";
            string strSP = strFormDB + "admin.ph_up_SelectFormDataNotEA";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
                ParamSet.Add4Sql("@fiid", SqlDbType.NVarChar, 50, fiid),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 100, xfalias),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formId)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 결재 이외의 양식 저장
        /// </summary>
        /// <param name="xfalias"></param>
        /// <param name="formId"></param>
        /// <param name="mainField"></param>
        /// <param name="subField"></param>
        /// <returns></returns>
        public int InsertFormDataNotEA(string xfalias, string formId, string mainField, string subField)
        {
            int iReturn = 0;
            string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
            string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);

            if (strFormDB != "") strFormDB += ".";
            string strSP = strFormDB + "admin.ph_up_SelectFormDataNotEA";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfalias),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 100, formId),
                ParamSet.Add4Sql("@mainfield", SqlDbType.NVarChar, -1, mainField),
                ParamSet.Add4Sql("@subfield", SqlDbType.NVarChar, -1, subField),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 결재양식 이외의 양식 수정
        /// </summary>
        /// <param name="fiid"></param>
        /// <param name="xfalias"></param>
        /// <param name="formTable"></param>
        /// <param name="mainField"></param>
        /// <param name="subField"></param>
        /// <returns></returns>
        public int UpdateFormDataNotEA(string fiid, string xfalias, string formTable, string mainField, string subField)
        {
            return UpdateFormDataNotEA(fiid, xfalias, formTable, 0, mainField, subField, "", "", 0, "");
        }

        /// <summary>
        /// 결재양식 이외의 양식 수정 - 이력관리 포함
        /// </summary>
        /// <param name="fiid"></param>
        /// <param name="xfalias"></param>
        /// <param name="formTable"></param>
        /// <param name="subTableCnt"></param>
        /// <param name="mainField"></param>
        /// <param name="subField"></param>
        /// <param name="actorKey"></param>
        /// <param name="actor"></param>
        /// <param name="actorId"></param>
        /// <param name="reserved1"></param>
        /// <returns></returns>
        public int UpdateFormDataNotEA(string fiid, string xfalias, string formTable, int subTableCnt, string mainField
                                    , string subField, string actorKey, string actor, int actorId, string reserved1)
        {
            int iReturn = 0;
            string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
            string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);

            if (strFormDB != "") strFormDB += ".";
            string strSP = strFormDB + "admin.ph_up_UpdateFormDataNotEA";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
                ParamSet.Add4Sql("@fiid", SqlDbType.VarChar, 50, fiid),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfalias),
                ParamSet.Add4Sql("@formtable", SqlDbType.VarChar, 50, formTable),
                ParamSet.Add4Sql("@subtablecnt", SqlDbType.Int, 4, subTableCnt),
                ParamSet.Add4Sql("@mainfield", SqlDbType.NVarChar, -1, mainField),
                ParamSet.Add4Sql("@subfield", SqlDbType.NVarChar, -1, subField),
                ParamSet.Add4Sql("@actorkey", SqlDbType.NVarChar, 33, actorKey),
                ParamSet.Add4Sql("@actor", SqlDbType.NVarChar, 100, actor),
                ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorId),
                ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 결재양식 이외의 양식 데이타 삭제
        /// </summary>
        /// <param name="fiid"></param>
        /// <param name="xfalias"></param>
        /// <param name="formId"></param>
        public void DeleteFormDataNotEA(string fiid, string xfalias, string formId)
        {
            string strEkpDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_BASE);
            string strFormDB = Framework.Configuration.ConfigINI.GetValue(Framework.Configuration.Sections.SECTION_DBNAME, Framework.Configuration.Property.INIKEY_DB_FORM);

            if (strFormDB != "") strFormDB += ".";
            string strSP = strFormDB + "admin.ph_up_DeleteFormDataNotEA";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ekpdb", SqlDbType.VarChar, 50, strEkpDB),
                ParamSet.Add4Sql("@fiid", SqlDbType.VarChar, 50, fiid),
                ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfalias),
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 100, formId)
            };

            ParamData pData = new ParamData(strSP, "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [결재 비밀번호 사용 여부]
        /// <summary>
        /// 개인별 결재 비밀번호 사용 여부 가져오기
        /// </summary>
        /// <param name="urId"></param>
        /// <returns></returns>
        public bool UseEApprovalPassword(int urId)
        {
            bool bReturn = true;
            string strRt = "";

            string strQuery = "SELECT ISNULL(UseEAPwd, 'N') AS UseEAPwd FROM admin.PH_USER_CONFIGURATION (NOLOCK) WHERE UserID = @urid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, urId)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                strRt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            if (strRt == "N") bReturn = false;
            return bReturn;
        }

        /// <summary>
        /// 개인 결재 비밀번호 사용 여부 설정
        /// </summary>
        /// <param name="urId"></param>
        /// <param name="usePwd"></param>
        public void SetEApprovalPassword(int urId, string usePwd)
        {
            string strQuery = "UPDATE admin.PH_USER_CONFIGURATION WITH (ROWLOCK) SET UseEAPwd = @usepwd WHERE UserID = @urid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, urId),
                ParamSet.Add4Sql("@usepwd", SqlDbType.Char, 1, usePwd)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [통합검색, 보존년한 및 문서등급]
        /// <summary>
        /// 통합검색용 검색리스트
        /// </summary>
        /// <param name="dnId"></param>
        /// <param name="fdId"></param>
        /// <param name="actId"></param>
        /// <param name="parentAcl"></param>
        /// <param name="viewer"></param>
        /// <param name="ctId"></param>
        /// <param name="admin"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="sortCol"></param>
        /// <param name="sortType"></param>
        /// <param name="searchCol"></param>
        /// <param name="searchText"></param>
        /// <param name="searchSDate"></param>
        /// <param name="searchEDate"></param>
        /// <returns></returns>
        public DataSet SearchTotalXFormList(int dnId, int fdId, int actId, string parentAcl, int viewer, int ctId, string admin, int page, int count
                                        , string sortCol, string sortType, string searchCol, string searchText, string searchSDate, string searchEDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, dnId),
                ParamSet.Add4Sql("@fd_id", SqlDbType.Int, fdId),
                ParamSet.Add4Sql("@activity_id", SqlDbType.Int, actId),
                ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentAcl),
                ParamSet.Add4Sql("@viewer", SqlDbType.Int, viewer),
                ParamSet.Add4Sql("@ctid", SqlDbType.Int, ctId),
                ParamSet.Add4Sql("@bAdminTool", SqlDbType.Char, 1, admin),
                ParamSet.Add4Sql("@page", SqlDbType.Int, page),
                ParamSet.Add4Sql("@count", SqlDbType.Int, count),
                ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortCol),
                ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
                ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchCol),
                ParamSet.Add4Sql("@searchText", SqlDbType.VarChar, 200, searchText),
                ParamSet.Add4Sql("@searchStartDate", SqlDbType.VarChar, 10, searchSDate),
                ParamSet.Add4Sql("@searchEndDate", SqlDbType.VarChar, 10, searchEDate),

                ParamSet.Add4Sql("@totalMessages", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData("admin.ph_up_TotalSearchGetXFormList", "", "TotalSearch", 30, parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식에 설정된 보존년한, 문서등급 정의 가져오기
        /// </summary>
        /// <param name="dnID"></param>
        /// <param name="docLevel"></param>
        /// <param name="keepYear"></param>
        /// <returns></returns>
        public DataSet RetrieveDocData(int dnID, int docLevel, int keepYear)
        {
            DataSet dsReturn = null;
            string strQuery = @"SELECT a.DisplayName AS DocLevel, b.DisplayName AS KeepYear
                        FROM admin.PH_DOC_LEVEL a (NOLOCK), admin.PH_DOC_KEEPYEAR b (NOLOCK)
                        WHERE a.DN_ID = @dnid AND b.DN_ID = @dnid AND a.DocLevel = @doclevel AND b.KeepYear = @keepyear";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dnid", SqlDbType.SmallInt, 2, dnID),
                ParamSet.Add4Sql("@doclevel", SqlDbType.SmallInt, 2, docLevel),
                ParamSet.Add4Sql("@keepyear", SqlDbType.SmallInt, 2, keepYear)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [업무관리 연관, 담당 양식, 부서 수신담당자 여부, 공유 문서함, 겸직부서]
        /// <summary>
        /// 결재문서에 연결된 업무 정보 가져오기
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="actID"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public DataSet RetrieveRelatedTaskInfo(string taskID, string actID, string step)
        {
            DataSet dsReturn = null;

            if (taskID == "") return dsReturn;
            if (actID == "") actID = "0";
            if (step == "") step = "0";

            string strQuery = @"SELECT DisplayName AS Task
                    , (SELECT Subject FROM admin.PH_XF_SCHEDULE (NOLOCK) WHERE MessageID = @actid) AS Activity
                    , (SELECT DisplayName FROM admin.PH_STATE_CHART (NOLOCK) WHERE Pos='SF' AND State = @state) AS ActivityStep
                    FROM admin.PH_OBJECT_FD (NOLOCK) WHERE FD_ID = @taskid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@taskid", SqlDbType.Int, 4, Convert.ToInt32(taskID)),
                ParamSet.Add4Sql("@actid", SqlDbType.Int, 4, Convert.ToInt32(actID)),
                ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, Convert.ToInt16(step))
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 담당 양식 존재 여부
        /// </summary>
        /// <param name="chargeId"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        public bool ExistChargedForm(int chargeId, string objType)
        {
            bool bReturn = false;
            int iCount = 0;

            string strQuery = "SELECT COUNT(FormID) FROM admin.PH_EA_FORM_CHARGE (NOLOCK) WHERE ChargeID = @chargeid AND ObjectType = @objtype";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@chargeid", SqlDbType.Int, 4, chargeId),
                ParamSet.Add4Sql("@objtype", SqlDbType.Char, 2, chargeId)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                iCount = Convert.ToInt32(db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            if (iCount > 0) bReturn = true;
            return bReturn;
        }

        /// <summary>
        /// 부서 수신담당자 인지(부서의 특정 사용자가 특정 역할을 가졌는지 여부)
        /// </summary>
        /// <param name="grId"></param>
        /// <param name="urId"></param>
        /// <param name="auAlias"></param>
        /// <returns></returns>
        public bool IsRcvManager(int grId, int urId, string auAlias)
        {
            bool bReturn = false;
            int iCount = 0;

            string strQuery = "SELECT COUNT(TargetID) FROM admin.PH_AUTHORITY (NOLOCK) WHERE ObjectID = @objectid AND ObjectType = 'GR' AND TargetID = @targetid AND TargetType = 'UR' AND AUAlias = @aualias";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, grId),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, urId),
                ParamSet.Add4Sql("@aualias", SqlDbType.VarChar, 20, auAlias)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                iCount = Convert.ToInt32(db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            if (iCount > 0) bReturn = true;
            return bReturn;
        }

        /// <summary>
        /// 공유된 타부서 문서함 가져오기
        /// </summary>
        /// <param name="grId"></param>
        /// <returns></returns>
        public DataSet SelectEAFolderView(int grId)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, grId)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectEAFolderView", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

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

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 현 사용자의 사업장 정보 가져 오기
        /// </summary>
        /// <param name="opID"></param>
        /// <returns></returns>
        public DataSet RetrieveCorpInfo(int opID)
        {
            DataSet dsReturn = null;

            string strQuery = @"
SELECT Logo, Address, CEO, RepresentPhone, Domain, HomePage, Logo_Small, CompanyName, CompanyCode
FROM admin.PH_OBJECT_OP (NOLOCK) WHERE OP_ID = @opid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@opid", SqlDbType.Int, 4, opID)
            };

            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [문서명 검색, 사용자 이름 검색]
        /// <summary>
        /// 문서명 검색
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public DataSet RetrieveDocList(string searchText)
        {
            DataSet dsReturn = null;
            string strQuery = @"SELECT FormID, DocName AS Name, COUNT(FormID) AS DocCount FROM admin.PH_VIEW_BF_WORKLIST (NOLOCK) " + searchText + " GROUP BY FormID, DocName ORDER BY DocCount DESC, Name"; ;

            SqlParameter[] parameters = null;
            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 사용자 검색
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public DataSet RetrieveUserList(string searchText)
        {
            DataSet dsReturn = null;
            string strQuery = @"SELECT UserID, LogonID, DisplayName AS Name FROM admin.ph_VIEW_OBJECT_UR_LIST (NOLOCK) WHERE GRAlias <> 'A4210' AND (Role = 'chief' OR Role = 'regular') AND DisplayName LIKE '" + searchText + "%' ORDER BY DisplayName";

            SqlParameter[] parameters = null;
            ParamData pData = new ParamData(strQuery, "text", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [양식 분류 관리]

        /// <summary>
        /// 양식 분류 관리
        /// </summary>
        /// <param name="dnID"></param>
        /// <returns></returns>
        public DataSet GetEAFormClass(int dnID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@domainid", SqlDbType.Int, 4, dnID)
            };

            ParamData pData = new ParamData("admin.ph_up_BFSelectEAFormClass", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 결재 양식 분류 관리
        /// </summary>
        /// <param name="command"></param>
        /// <param name="classid"></param>
        /// <param name="domainid"></param>
        /// <param name="formname"></param>
        /// <param name="formseqno"></param>
        public void HandleEAFormClass(string command, int classid, int domainid, string formname, int formseqno)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@command", SqlDbType.VarChar, 6, command),
                ParamSet.Add4Sql("@classid", SqlDbType.Int, classid),
                ParamSet.Add4Sql("@domainid", SqlDbType.Int, domainid),
                ParamSet.Add4Sql("@formname", SqlDbType.NVarChar, 100, formname),
                ParamSet.Add4Sql("@formseqno", SqlDbType.Int, formseqno)
            };

            ParamData pData = new ParamData("admin.ph_up_BFHandleEAFormKind", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 양식 조회(등록되어 있는 모든 양식)
        /// </summary>
        /// <param name="dnID"></param>
        /// <returns></returns>
        public DataSet GetEAFormList(int dnID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@domainid", SqlDbType.Int, 4, dnID)
            };

            ParamData pData = new ParamData("admin.ph_up_BFGetEAFormList", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        #endregion

        #region [ 양식 문서 정보 ]

        /// <summary>
        /// 문서에 대한 전체 정보 조회
        /// </summary>
        /// <param name="dnID"></param>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public DataSet GetEADocumentTotalData(int dnID, int messageID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, dnID),
                ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID)
            };

            ParamData pData = new ParamData("admin.ph_up_BFGetProcessTotalDataForAdmin", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        #endregion

        #region [ 양식 담당 관리 ]

        /// <summary>
        /// 양식 담당 관리 변경 - JSON 이용
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="chargejson"></param>
        public void UpdateEAFormChargeJson(string formID, string chargejson)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@chargejson", SqlDbType.NVarChar, chargejson)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateEAFormChargeJson", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        #endregion

        #region [ 양식 부가 설정 ]

        /// <summary>
        /// 양식폼 필드 업데이트
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="targetField"></param>
        /// <param name="targetValue"></param>
        public void UpdateEAFormFieldValue(string formID, string targetField, string targetValue)
        {
            string query = $"UPDATE admin.PH_EA_FORMS WITH (ROWLOCK) SET {targetField} = @value WHERE FormID = @formid";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@value", SqlDbType.NVarChar, 500, targetValue)
            };

            ParamData pData = new ParamData(query, "text", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        #endregion

        #region [ 양식 알림 설정 ]

        /// <summary>
        /// 양식 알림 설정 정보 조회
        /// </summary>
        /// <param name="formID"></param>
        /// <returns></returns>
        public DataSet GetEAFormNotice(string formID)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formID", SqlDbType.VarChar, 33, formID)
            };

            ParamData pData = new ParamData("admin.ph_up_SelectEAFormNotice", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 양식 알림 설정 업데이트
        /// </summary>
        /// <param name="formID"></param>
        /// <param name="period"></param>
        /// <param name="field"></param>
        /// <param name="deferment"></param>
        /// <param name="mailuse"></param>
        public void UpdateEAFormNoticeFormSet(string formID, int period, string field, int deferment, string mailuse)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID),
                ParamSet.Add4Sql("@period", SqlDbType.SmallInt, period),
                ParamSet.Add4Sql("@field", SqlDbType.NVarChar, 30, field),
                ParamSet.Add4Sql("@deferment", SqlDbType.SmallInt, deferment),
                ParamSet.Add4Sql("@mailuse", SqlDbType.Char, 1, mailuse)
            };

            ParamData pData = new ParamData("admin.ph_up_UpdateEAFormNoticeFormSet", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 양식 알림 설정 삭제
        /// </summary>
        /// <param name="formID"></param>
        public void DeleteEAFormNoticeFormSet(string formID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@formid", SqlDbType.VarChar, 33, formID)
            };

            ParamData pData = new ParamData("admin.ph_up_DeleteEAFormNoticeFormSet", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        #endregion
    }
}
