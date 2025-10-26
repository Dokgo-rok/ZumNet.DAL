//using Oracle.ManagedDataAccess.Client;

using System.Collections;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Reflection;
using System.Text;

using ZumNet.Framework.Core;
using ZumNet.Framework.Oracle.Base;
using ZumNet.Framework.Oracle.Data;

namespace ZumNet.DAL.InterfaceDac
{
    /// <summary>
    /// 
    /// </summary>
    public class OracleERP : global::System.MarshalByRefObject, System.IDisposable
    {
        #region [생성자 및 멤버변수]
        /// <summary>
        /// 
        /// </summary>
        private string _connectString = "";

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectString; }
            set { this._connectString = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public OracleERP()
        {
            this._connectString = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_EXTERNAL1);
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public OracleERP(string connectionString)
        {
            this._connectString = connectionString;

            if (String.IsNullOrWhiteSpace(connectionString))
            {
                this._connectString = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_EXTERNAL1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
        #endregion

        /// <summary>
        /// 크레신 - 품번, 업체 ID 및 코드 가져오기
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="itemNo"></param>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public Hashtable Cresyn_Get_MTL_SYSTEM_ITEMS_PO_VENDORS(string orgId, string itemNo, string vendor)
        {
            DataSet ds = null;
            DataSet ds2 = null;
            DataRow row = null;
            Hashtable ht = null;

            string strQuery = "SELECT MSI.SEGMENT1, MSI.DESCRIPTION, MSI.INVENTORY_ITEM_ID FROM MTL_SYSTEM_ITEMS_B MSI WHERE MSI.ORGANIZATION_ID ='" + orgId
                            + "' AND MSI.ITEM_TYPE = 'PUR' AND MSI.INVENTORY_ITEM_STATUS_CODE = 'Active' AND MSI.SEGMENT1 = '" + itemNo + "'";

            string strQuery2 = "SELECT PV.SEGMENT1,PV.VENDOR_NAME,PV.VENDOR_ID FROM PO_VENDORS PV WHERE PV.VENDOR_NAME = '" + vendor + "'";

            try
            {
                ht = new Hashtable();

                ht.Add("ITEMID", "");
                ht.Add("ITEMNO", "");
                ht.Add("ITEMNM", "");

                ht.Add("VENDORID", "");
                ht.Add("VENDORCODE", "");
                ht.Add("VENDOR", "");

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery, "text", "SYSTEM_ITEMS", 60, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "PO_VENDORS", 60, parameters);

                using (DbBase db = new DbBase())
                {
                    if (orgId != "" && itemNo != "") ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    if (vendor != "") ds2 = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    row = ds.Tables[0].Rows[0];
                    ht["ITEMID"] = row["INVENTORY_ITEM_ID"].ToString();
                    ht["ITEMNO"] = row["SEGMENT1"].ToString();
                    ht["ITEMNM"] = row["DESCRIPTION"].ToString();
                }

                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    row = ds2.Tables[0].Rows[0];
                    ht["VENDORID"] = row["VENDOR_ID"].ToString();
                    ht["VENDORCODE"] = row["SEGMENT1"].ToString();
                    ht["VENDOR"] = row["VENDOR_NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            finally
            {
                if (ds != null) ds.Dispose();
                if (ds2 != null) ds2.Dispose();
            }
            return ht;
        }

        /// <summary>
        /// 크레신 - 품번,모델명 등 가져오기
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_MTL_SYSTEM_ITEMS_B(string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM MTL_SYSTEM_ITEMS_B MSI WHERE MSI.ORGANIZATION_ID = 107 AND MSI.SEGMENT1 LIKE '" + search + "%'";

                string strQuery2 = @"SELECT * FROM (SELECT MSI.SEGMENT1, MSI.DESCRIPTION, ROWNUM RN FROM MTL_SYSTEM_ITEMS_B MSI
     WHERE MSI.ORGANIZATION_ID = 107 AND MSI.SEGMENT1 LIKE '" + search + "%' ORDER BY 1) WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 외주관련 품번,모델명 등 가져오기
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orgId"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_MTL_SYSTEM_ITEMS_C(string search, int pageSize, int page, string orgId, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = @"SELECT COUNT(*) FROM (SELECT MSI.SEGMENT1
                    , MSI.DESCRIPTION
                    , ROWNUM RN 
                 FROM MTL_SYSTEM_ITEMS_B MSI
                    , CBO_NEW_ORG_V      ORG
                WHERE MSI.ORGANIZATION_ID = ORG.ORGANIZATION_ID
                  AND MSI.INVENTORY_ITEM_STATUS_CODE = 'Active'
                  AND MSI.OUTSIDE_OPERATION_FLAG = 'Y'
                  AND MSI.OUTSIDE_OPERATION_UOM_TYPE = 'ASSEMBLY'  
                  AND ORG.ORGANIZATION_CODE = '" + orgId + "' AND MSI.SEGMENT1 LIKE '" + search + "%')";

                string strQuery2 = @"SELECT * FROM (SELECT MSI.SEGMENT1
                    , MSI.DESCRIPTION
                    , ROWNUM RN 
                 FROM MTL_SYSTEM_ITEMS_B MSI
                    , CBO_NEW_ORG_V      ORG
                WHERE MSI.ORGANIZATION_ID = ORG.ORGANIZATION_ID
                  AND MSI.INVENTORY_ITEM_STATUS_CODE = 'Active'
                  AND MSI.OUTSIDE_OPERATION_FLAG = 'Y'
                  AND MSI.OUTSIDE_OPERATION_UOM_TYPE = 'ASSEMBLY'                
                  AND ORG.ORGANIZATION_CODE = '" + orgId + "' AND MSI.SEGMENT1 LIKE '" + search + "%' ORDER BY 1) WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 품번 품명 검색 시 법인명 추가토록 기능, 20-08-03 INVENTORY_ITEM_ID 반환
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orgId"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_MTL_SYSTEM_ITEMS_D(string search, int pageSize, int page, string orgId, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM MTL_SYSTEM_ITEMS_B MSI WHERE MSI.ORGANIZATION_ID ='" + orgId + "' AND MSI.ITEM_TYPE = 'PUR' AND MSI.INVENTORY_ITEM_STATUS_CODE = 'Active' AND MSI.SEGMENT1 LIKE '" + search + "%' ";

                string strQuery2 = @"SELECT * FROM (SELECT MSI.SEGMENT1, MSI.DESCRIPTION, MSI.INVENTORY_ITEM_ID, ROWNUM RN FROM MTL_SYSTEM_ITEMS_B MSI
     WHERE MSI.ORGANIZATION_ID =  '" + orgId + "' AND MSI.SEGMENT1 LIKE '" + search + "%' AND MSI.ITEM_TYPE = 'PUR' AND MSI.INVENTORY_ITEM_STATUS_CODE = 'Active' ORDER BY 1) WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 품번 품명 검색(구매단가결정서 양식 사용, 2020-12-16)
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orgId"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_BPA_ITEM_V(string search, int pageSize, int page, string orgId, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM  CBO_EKP_BPA_ITEM_V  MSI WHERE MSI.ORGANIZATION_ID ='" + orgId + "' AND MSI.SEGMENT1 LIKE '" + search + "%' ";

                string strQuery2 = @"SELECT * FROM (SELECT MSI.SEGMENT1, MSI.DESCRIPTION, MSI.INVENTORY_ITEM_ID, ROWNUM RN FROM  CBO_EKP_BPA_ITEM_V  MSI
     WHERE MSI.ORGANIZATION_ID =  '" + orgId + "' AND MSI.SEGMENT1 LIKE '" + search + "%' ORDER BY 1) WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - BOM 가져오기
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="modelNo"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_BOM_ITEMS(string orgId, string modelNo)
        {
            DataSet dsReturn = null;
            string rt = "";

            StringBuilder sbQuery = new StringBuilder();
            StringBuilder sbQuery2 = new StringBuilder();

            sbQuery.Append("DECLARE x_error_message varchar2(2000); x_error_code  number; v_source_type   varchar2(2000); v_souring_org varchar2(2000); v_bpa_no   varchar2(2000); ");
            sbQuery.Append(" v_unit_price  varchar2(2000); v_currency_code varchar2(2000); i_group_id  number := 0; i_sequence_id   number := 0;");
            sbQuery.Append(" BEGIN FOR c1 IN ( SELECT msi.organization_id, msi.inventory_item_id,msi.segment1  FROM MTL_SYSTEM_ITEMS  MSI , MTL_PARAMETERS MPS ");
            sbQuery.AppendFormat(" WHERE msi.organization_id = {0} and msi.organization_id = mps.organization_id and msi.item_type in ('FGS', 'CGS') and msi.segment1 = '{1}' ) LOOP", orgId, modelNo);
            sbQuery.Append(" DELETE FROM CBO_BOM_GET_ITEM_TEMP; ");
            sbQuery.Append(" select BOM_EXPLOSION_TEMP_S.nextval into i_group_id from dual; ");
            sbQuery.Append(" select BOM_IMPLOSION_TEMP_S.nextval into i_sequence_id from dual; ");
            sbQuery.Append(" BOMPXINQ.EXPLODER_USEREXIT(0,C1.organization_id,1,i_group_id,0,100,1,1,2,3,2,0,2,1,c1.inventory_item_id,null,null,null,null,sysdate,1,2,2,x_error_message,x_error_code);");
            sbQuery.Append(" insert into CBO_BOM_GET_ITEM_TEMP(top_bill_sequence_id,bill_sequence_id,component_sequence_id,top_item_id,group_id,organization_id,inventory_item_id,item_code,item_name");
            sbQuery.Append(" ,plan_level,sort_by,customer_item,spec,item_type,item_status,qpa,extended_qpa,ind_qty,dep_qty,scrap,disable_date,osp_item_id,wip_supply_type,supply_subinventory,processing_lt");
            sbQuery.Append(" ,souring_type,souring_org_supplier,bpa_no,price,currency,item_seq,op_seq,last_update_date,last_updated_by,creation_date,created_by,last_update_login)");
            sbQuery.Append(" select bse.top_bill_sequence_id, bse.bill_sequence_id, bse.component_sequence_id, bse.top_item_id, bse.group_id, bse.organization_id, bse.component_item_id, msi.segment1");
            sbQuery.Append(" , msi.description, bse.plan_level, bse.sort_order, mci.customer_item_number, mde.element_value, msi.item_type, msi.inventory_item_status_code, component_quantity, bse.extended_quantity");
            sbQuery.Append(" , bse.attribute1, bse.attribute2, msi.shrinkage_rate, bse.disable_date, CBO_COMMON_PKG.GET_OSP_ITEM_ID(bse.organization_id,bse.component_item_id), ml.meaning, bse.supply_subinventory");
            sbQuery.Append(" , msi.full_lead_time, null, null, null, null, null, bse.item_num, bse.operation_seq_num, sysdate, -1, sysdate, -1, -1");
            sbQuery.Append(" from BOM_SMALL_EXPL_TEMP bse, MTL_SYSTEM_ITEMS_B msi, MFG_LOOKUPS ml, MTL_DESCR_ELEMENT_VALUES_V mde, MTL_CUSTOMER_ITEM_XREFS_V mci");
            sbQuery.Append(" where bse.organization_id = msi.organization_id and bse.component_item_id = msi.inventory_item_id and bse.wip_supply_type = ml.lookup_code(+) and ml.lookup_type (+)= 'WIP_SUPPLY'");
            sbQuery.Append(" and msi.item_catalog_group_id = mde.item_catalog_group_id(+) and msi.inventory_item_id = mde.inventory_item_id(+) and mde.element_name (+)= 'Specification' ");
            sbQuery.Append(" and msi.inventory_item_id = mci.inventory_item_id(+) AND MSI.ORGANIZATION_ID = C1.organization_id and msi.organization_id = mci.master_organization_id(+) and mci.rank (+)= 1");
            sbQuery.Append("	and mci.inactive_flag (+)= 'N' and bse.group_id = i_group_id ;");
            sbQuery.Append(" COMMIT; END LOOP; END;");

            sbQuery2.Append("SELECT MSI.ORGANIZATION_ID, MSI.SEGMENT1, lpad(CCH.PLAN_LEVEL,CCH.PLAN_LEVEL,'_')  PLAN_LEVEL,");
            sbQuery2.Append(" CCH.ITEM_CODE, CCH.ITEM_NAME, CCH.SPEC, CCH.ITEM_TYPE, CCH.ITEM_STATUS, CCH.QPA, CCH.EXTENDED_QPA,");
            sbQuery2.Append(" CCH.SCRAP, CCH.DISABLE_DATE, CCH.WIP_SUPPLY_TYPE, CCH.SUPPLY_SUBINVENTORY");
            sbQuery2.Append(" FROM CBO_BOM_GET_ITEM_TEMP CCH, MTL_SYSTEM_ITEMS_B MSI");
            sbQuery2.AppendFormat(" WHERE MSI.ORGANIZATION_ID ='{0}'", orgId);
            sbQuery2.Append(" AND MSI.INVENTORY_ITEM_ID = CCH.TOP_ITEM_ID");
            sbQuery2.Append(" ORDER BY MSI.ORGANIZATION_ID, GROUP_ID, SORT_BY");

            try
            {
                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(sbQuery.ToString(), "text", 30, parameters);
                ParamData pData2 = new ParamData(sbQuery2.ToString(), "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 업체명 가져오기, 20-07-30 VENDOR_ID 필드 추가
        /// </summary>
        /// <param name="searchCol"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PO_VENDORS(string searchCol, string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM PO_VENDORS PV WHERE PV." + searchCol + " LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (SELECT PV.SEGMENT1,PV.VENDOR_NAME,PV.VENDOR_ID, ROWNUM RN FROM PO_VENDORS PV WHERE PV." + searchCol + " LIKE '%" + search + "%') WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 업체명 가져오기
        /// </summary>
        /// <param name="searchCol"></param>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PO_VENDORS2(string searchCol, string search, string query, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM CBO_EKP_CUSTOMER_V PV WHERE PV.ORG_CODE  = '" + query + "' AND PV." + searchCol + " LIKE '%" + search + "%'";

                string strQuery2 = "SELECT CUSTOMER_NAME, CUSTOMER_NUMBER FROM (SELECT PV.CUSTOMER_NUMBER,PV.CUSTOMER_NAME, ROWNUM RN FROM CBO_EKP_CUSTOMER_V PV WHERE PV.ORG_CODE  = '" + query + "' AND  PV." + searchCol + " LIKE '%" + search + "%') WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchCol"></param>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PO_SALEITEMS(string searchCol, string search, string query, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM CBO_EKP_SYSTEM_ITEMS_V PV WHERE PV.ORG_CODE  = '" + query + "' AND PV." + searchCol + " LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (SELECT PV.SEGMENT1,PV.DESCRIPTION, ROWNUM RN FROM CBO_EKP_SYSTEM_ITEMS_V PV WHERE PV.ORG_CODE  = '" + query + "' AND  PV." + searchCol + " LIKE '%" + search + "%') WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 고객명 가져오기
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_RA_CUSTOMERS(string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM RA_CUSTOMERS RC WHERE RC.customer_name LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (SELECT RC.customer_name, ROWNUM RN FROM RA_CUSTOMERS RC WHERE RC.customer_name LIKE '%" + search + "%') WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 구매담당자 가져오기
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_BUYER(string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM CBO_EKP_BUYER WHERE FULL_NAME LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (SELECT FULL_NAME AS DESCRIPTION, BUYER_ID, ROWNUM RN FROM CBO_EKP_BUYER WHERE FULL_NAME LIKE '%" + search + "%' ORDER BY FULL_NAME) WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "SYSITEMS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - PO Type 가져오기
        /// </summary>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_PO_TYPE()
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT PO_TYPE FROM CBO_EKP_PO_TYPE";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "PO_TYPE", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - BPA NUM 가져오기
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="venderCode"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_PO_BPA(string orgCode, string venderCode, string currency)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT PO_NUM, VENDOR_NAME, APPROVED_FLAG FROM CBO_EKP_PO_BPA WHERE ORGANIZATION_CODE = '" + orgCode + "' AND VENDOR_CODE = '" + venderCode + "' AND CURRENCY_CODE = '" + currency + "'";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "PO_NUM", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_SALECENTER(string orgCode)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT SELL_ORG_CODE, VENDOR_NAME, VENDOR_CODE FROM CBO_EKP_SELL_PROD_ORG_V WHERE PROD_ORG_CODE = '" + orgCode + "'";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "SELL_ORG_CODE", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_PRICELIST(string orgCode, string Currency)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT PRICE_LIST_NAME, PRICE_LIST_DESC FROM CBO_EKP_PRICE_LIST_V WHERE ORG_CODE = '" + orgCode + "' AND CURRENCY_CODE = '" + Currency + "' ";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "PRICE_LIST_NAME", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="Currency"></param>
        /// <param name="VendCode"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_EKP_SALEBPANUM(string orgCode, string Currency, string VendCode)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT BPA_NUMBER, VENDOR_NAME FROM CBO_EKP_INTER_BPA_V WHERE ORG_CODE = '" + orgCode + "' AND CURRENCY_CODE = '" + Currency + "' AND VENDOR_CODE = '" + VendCode + "'  ";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "SALEBPANUM", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 본사 On-hand 조회
        /// </summary>
        /// <param name="itemNo"></param>
        /// <param name="inventoryCode"></param>
        /// <returns></returns>
        public string Cresyn_Get_MTL_ONHAND_TOTAL_MWB_V(string itemNo, string inventoryCode)
        {
            string strReturn = "";

            try
            {
                string strQuery = @"
    SELECT NVL(SUM(ON_HAND), 0)
	FROM MTL_ONHAND_TOTAL_MWB_V
	WHERE 1 = 1
		AND ORGANIZATION_ID    = 101      --본사
	--	AND INVENTORY_ITEM_ID  = 9517108  --ITEM_ID
		AND ITEM               = '" + itemNo + @"'  --ITEM_CODE
		AND SUBINVENTORY_CODE  = '" + inventoryCode + @"' --창고
        ";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return strReturn;
        }

        /// <summary>
        /// 크레신 - 본사 창고 조회
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_MTL_SECONDARY_INVENTORIES(string search)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = @"
    SELECT ORGANIZATION_ID, SECONDARY_INVENTORY_NAME, DESCRIPTION
    FROM MTL_SECONDARY_INVENTORIES
    WHERE 1=1 
        AND DISABLE_DATE IS NULL 
        AND ORGANIZATION_ID = 101 --본사
	--	AND SECONDARY_INVENTORY_NAME = 'HS-MDS'
        ";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "INVENTORIES", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        #region [개발원가견적, 모델별원가]
        /// <summary>
        /// 생산지, 업체에 따른 품목 단가 가져오기
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="itemId"></param>
        /// <param name="vendCode"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PLA_UNIT_PRICE(string orgId, string itemId, string vendCode)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = @"
    SELECT PHA.ORG_ID             AS ORG_ID
         , MSI.INVENTORY_ITEM_ID  AS ITEM_ID
         , MSI.SEGMENT1           AS ITEM_CODE
         , MSI.DESCRIPTION        AS ITEM_DESC
         , PVS.VENDOR_ID          AS VENDOR_ID
         , PVS.VENDOR_NAME        AS VENDOR_NAME
         , PVS.SEGMENT1           AS VENDOR_CODE
         , PHA.CURRENCY_CODE      AS CURRENCY_CODE
         , PLA.UNIT_PRICE         AS UNIT_PRICE          
      FROM PO_LINES_ALL              PLA
         , PO_HEADERS_ALL            PHA
         , MTL_SYSTEM_ITEMS_B        MSI
         , PO_VENDORS                PVS    
     WHERE PLA.PO_HEADER_ID        = PHA.PO_HEADER_ID
       AND PHA.VENDOR_ID           = PVS.VENDOR_ID
       AND PLA.ITEM_ID             = MSI.INVENTORY_ITEM_ID
       AND PLA.ORG_ID              = MSI.ORGANIZATION_ID
       AND PLA.PO_LINE_ID          = (
                                       SELECT MAX(POL.PO_LINE_ID)
                                         FROM PO_HEADERS_ALL              POH
                                            , PO_LINES_ALL                POL   
                                            , MTL_SYSTEM_ITEMS_B          MSI        
                                            , PO_VENDORS                  PVS                          
                                        WHERE POH.PO_HEADER_ID          = POL.PO_HEADER_ID
                                          AND NVL(POL.CANCEL_FLAG, 'N') = 'N'
                                          AND NVL(POH.CANCEL_FLAG, 'N') = 'N'
                                          AND POH.TYPE_LOOKUP_CODE      = 'BLANKET'
                                          AND POH.AUTHORIZATION_STATUS  = 'APPROVED'
                                          AND TRUNC(SYSDATE) BETWEEN NVL(POH.START_DATE, TRUNC(SYSDATE))
                                                                 AND NVL(POH.END_DATE, TRUNC(SYSDATE))
                                          AND NVL(POL.EXPIRATION_DATE, TRUNC(SYSDATE)) >= TRUNC(SYSDATE)                        
                                          AND POL.ITEM_ID               = MSI.INVENTORY_ITEM_ID
                                          AND POL.ORG_ID                = MSI.ORGANIZATION_ID
                                          AND PVS.VENDOR_ID             = POH.VENDOR_ID
                                          AND POL.ORG_ID                = " + orgId + @"        -- ORG_ID                                                       
                                          AND POL.ITEM_ID               = " + itemId + @"       -- INVENTORY_ITEM_ID
                                          AND PVS.SEGMENT1              = " + vendCode + @"     -- VENDOR_CODE   
                                       )
";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "PLA_UNIT_PRICE", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 품번에 해당되는 업체 및 단가 가져오기
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="itemNo"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PHA_VENDOR_UNIT_PRICE(string orgId, string itemNo)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = @"
    SELECT PHA.ORG_ID  
        , MSI.SEGMENT1     AS ITEM_CODE
        , PHA.SEGMENT1     AS BPA
        , PLA.UNIT_PRICE  
        , PHA.CURRENCY_CODE
        , PVS.VENDOR_NAME
    FROM PO_HEADERS_ALL       PHA
        , PO_LINES_ALL         PLA
        , MTL_SYSTEM_ITEMS_B   MSI
        , PO_VENDORS           PVS
    WHERE PHA.ORG_ID           = " + orgId + @"         -- ORG_ID
        AND PHA.ORG_ID           = MSI.ORGANIZATION_ID
        AND PHA.PO_HEADER_ID     = PLA.PO_HEADER_ID
        AND PHA.VENDOR_ID        = PVS.VENDOR_ID
        AND PHA.TYPE_LOOKUP_CODE = 'BLANKET'    
        AND PLA.ITEM_ID          = MSI.INVENTORY_ITEM_ID
        AND PLA.EXPIRATION_DATE IS NULL                 -- EXPIRE안되어있는것들
        AND MSI.SEGMENT1        =  '" + itemNo + @"'    --품번입력
";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "PLA_UNIT_PRICE", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 모델 BOM 가져오기
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="modelNo"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_BOM_EXPLODE_ORG(string orgId, string modelNo)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = @"
    SELECT CBO.TOP_ITEM_CODE    AS TOP_ITEM_CODE
        , CBO.PLAN_LEVEL_DESC   AS PLAN_LEVEL_DESC  -- 단계
        , CBO.COM_ITEM_CODE     AS COM_ITEM_CODE    -- (하위)품번NO
        , CBO.COM_ITEM_ID       AS COM_ITEM_ID      -- 품번ID
        , CBO.COM_ITEM_DESC     AS COM_ITEM_DESC    -- 품명
        , MDE.ELEMENT_VALUE     AS COM_ITEM_SPEC    -- 규격
    FROM CBO_BOM_EXPLODE_ORG CBO
        , MTL_SYSTEM_ITEMS_B MSI
        , MTL_DESCR_ELEMENT_VALUES_V MDE
    WHERE 1=1
        AND TRUNC(CBO.EFFECTIVITY_DATE) <= TRUNC(SYSDATE)
        AND CBO.DISABLE_DATE IS NULL
        AND CBO.ORGANIZATION_ID = MSI.ORGANIZATION_ID
        AND CBO.COM_ITEM_ID = MSI.INVENTORY_ITEM_ID
        AND MSI.ITEM_CATALOG_GROUP_ID = MDE.ITEM_CATALOG_GROUP_ID(+)
        AND MSI.INVENTORY_ITEM_ID = MDE.INVENTORY_ITEM_ID(+)
        AND MDE.ELEMENT_NAME (+) = 'Specification'
        AND CBO.ORGANIZATION_ID = " + orgId + @"
        AND CBO.TOP_ITEM_CODE = '" + modelNo + @"'
    ORDER BY
        CBO.SORT_ORDER
";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "BOM_EXPLODE_ORG", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 특정 일자 모델별 공수 가져오기 => OCI-22053: overflow error 가 자주 발생돼 프로시저 사용
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="modelNo"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_BOM_STANDARD_COST(string orgId, string modelNo, string searchDate)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = @"
    SELECT B.COMPONENT_ITEM_CODE,MSI.DESCRIPTION, B.STANDARD_TIME, B.MAN_HOUR, B.TRX_FROM_DATE
    FROM   CBO_BOM_STANDARD_COST_T_V2 B, MTL_SYSTEM_ITEMS_B MSI
    WHERE B.TRX_FROM_DATE = TO_DATE('" + searchDate + @"','YYYYMMDD')
    AND   B.ORGANIZATION_ID = MSI.ORGANIZATION_ID
    AND   B.COMPONENT_ITEM_ID = MSI.INVENTORY_ITEM_ID
    AND   B.MODEL_ITEM_CODE = '" + modelNo + @"'
    AND   B.COMPONENT_ITEM_TYPE IN ('FGS','SUB')
    AND   B.ORGANIZATION_ID = " + orgId + @"
    AND   B.PLANNER_CODE is null
    and   B.is_comp_org_flag is null
    and   B.outside_operation_flag = 'N'
    AND   B.IS_PUR_COMP_FLAG IS NULL
    and   (b.wip_supply_type is null or b.wip_supply_type <> '6')
";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "BOM_STANDARD_COST", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }
        #endregion

        #region [환율 관련]
        /// <summary>
        /// 크레신 - 환율 정보 가져오기
        /// </summary>
        /// <returns></returns>
        public DataSet Cresyn_Get_GL_DAILY_RATES()
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT 'CHQ_Corporate' AS CONVERSION_TYPE, CONVERSION_DATE, FROM_CURRENCY,TO_CURRENCY, ROUND(CONVERSION_RATE,10) FROM GL_DAILY_RATES WHERE CONVERSION_TYPE = 1000";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "CURRENCY", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCurrency"></param>
        /// <param name="searchSDate"></param>
        /// <param name="searchEDate"></param>
        /// <param name="conver"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_GL_DAILY_RATES(string toCurrency, string searchSDate, string searchEDate, string conver)
        {
            DataSet dsReturn = null;

            try
            {
                string strWhere = "";
                if (conver == "1005")
                {
                    strWhere = " WHERE CONVERSION_TYPE = 1005  AND TO_CURRENCY = '" + toCurrency + "'  AND (TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd')) = '" + searchSDate + "'";
                }
                else if (searchEDate == "")
                {
                    strWhere = " WHERE CONVERSION_TYPE = 1000  AND TO_CURRENCY = '" + toCurrency + "'  AND (TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd')) >= '" + searchSDate + "'";
                }
                else
                {
                    strWhere = " WHERE CONVERSION_TYPE = 1000  AND TO_CURRENCY = '" + toCurrency + "'  AND (TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd')) >= '" + searchSDate + "'  AND (TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd')) <= '" + searchEDate + "'";
                }
                string strQuery = "SELECT  CASE FROM_CURRENCY WHEN 'USD' THEN '1'WHEN 'EUR' THEN '2'WHEN 'JPY' THEN '3'WHEN 'CNY' THEN '4'WHEN 'HKD' THEN '5'WHEN 'IDR' THEN '6' WHEN 'VND' THEN '7' WHEN 'PHP' THEN '8' ELSE '9'  END AS SORT"
                       + ",'CHQ_Corporate' AS CONVERSION_TYPE, TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd') as CONVERSION_DATE, FROM_CURRENCY,TO_CURRENCY"
                       + ", CASE WHEN FROM_CURRENCY ='JPY' OR  FROM_CURRENCY ='IDR' OR  FROM_CURRENCY ='VND' THEN  ROUND(CONVERSION_RATE,10) *100  ELSE  ROUND(CONVERSION_RATE,10) END AS CONVERSION_RATE"
                       + " FROM GL_DAILY_RATES"
                       + strWhere
                       + " ORDER BY CONVERSION_DATE DESC, SORT,FROM_CURRENCY";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "CURRENCY", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toItem"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_GL_UNITJAEGO(string toItem)
        {
            DataSet dsReturn = null;

            try
            {
                string strWhere = "";
                if (toItem == "")
                {
                    strWhere = " AND  MSI.SEGMENT1 like '%'";
                }
                else
                {
                    strWhere = " AND  MSI.SEGMENT1 like '%" + toItem + "%' ";
                }

                string strQuery = "SELECT MSI.SEGMENT1, MSI.DESCRIPTION    "
            + "        , (  SELECT MAX(CBO.TOP_ITEM_CODE)                                                      "
            + "                  from CBO_BOM_MODEL_EXPLODE_ORG CBO                                           "
            + "                            WHERE CBO.RANK_CNT = 1                                    "
            + "                                     AND CBO.COM_ITEM_ID = MSI.INVENTORY_ITEM_ID                                         "
            + "                                            ) MODEL_CODE                    "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_INACTIVE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_NG_MAT  "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_DISUSE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_INACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_NG_MAT "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_DISUSE"
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_INACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_NG_MAT "
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_DISUSE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_INACTIVE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_NG_MAT  "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_DISUSE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_INACTIVE    "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_NG_MAT     "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_DISUSE                              "
    + "FROM MTL_SYSTEM_ITEMS_B          MSI   "
    + " , MTL_ONHAND_QUANTITIES       MOQ   "
    + " , MTL_SECONDARY_INVENTORIES   MIS   "
    + "    WHERE MSI.ORGANIZATION_ID = 107   "
    + " AND MSI.SEGMENT1 NOT LIKE '%OSP'   "
    + "    AND MSI.SEGMENT1 NOT LIKE '%IC'    "
    + " AND  (MSI.SEGMENT1 LIKE 'CER%'   "
    + "  OR MSI.SEGMENT1 LIKE 'CHR%'   "
    + "  OR MSI.SEGMENT1 LIKE 'CBR%'    "
+ "      OR MSI.SEGMENT1 LIKE 'CSR%'     "
+ "      OR MSI.SEGMENT1 LIKE 'CBQ%'      "
+ "      OR MSI.SEGMENT1 LIKE 'CRR%'      "
+ "      OR MSI.SEGMENT1 LIKE 'SA%' "
+ "      OR MSI.SEGMENT1 LIKE 'MSB%'    "
+ "      OR MSI.SEGMENT1 LIKE 'SPK%'    "
+ "      )                          "
   + "AND MOQ.INVENTORY_ITEM_ID = MSI.INVENTORY_ITEM_ID "
+ "   AND MOQ.SUBINVENTORY_CODE = MIS.SECONDARY_INVENTORY_NAME  "
   + "AND MOQ.ORGANIZATION_ID   = MIS.ORGANIZATION_ID   "
+ "   AND MOQ.ORGANIZATION_ID IN (102, 103, 104, 105, 108, 148) "
+ "   AND MOQ.TRANSACTION_QUANTITY <> 0                 "
+ strWhere
+ " GROUP BY    "
+ "       MSI.SEGMENT1  "
+ "     , MSI.DESCRIPTION   "
        + "     , MSI.INVENTORY_ITEM_ID    ";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "SEGMENT1", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toItem"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_GL_WIREJAEGO(string toItem)
        {
            DataSet dsReturn = null;

            try
            {
                string strWhere = "";
                if (toItem == "")
                {
                    strWhere = " AND  MSI.SEGMENT1 like '%'";
                }
                else
                {
                    strWhere = " AND  MSI.SEGMENT1 like '%" + toItem + "%' ";
                }

                string strQuery = "SELECT MSI.SEGMENT1, MSI.DESCRIPTION    "
                + "        , (  SELECT MAX(CBO.TOP_ITEM_CODE)                                                      "
            + "                  from CBO_BOM_MODEL_EXPLODE_ORG CBO                                           "
            + "                            WHERE CBO.RANK_CNT = 1                                    "
            + "                                     AND CBO.COM_ITEM_ID = MSI.INVENTORY_ITEM_ID                                         "
            + "                                            ) MODEL_CODE                    "
    + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_INACTIVE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_NG_MAT  "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 102 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CH_DISUSE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_INACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_NG_MAT "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 103 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CT_DISUSE"
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_INACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_NG_MAT "
  + "     , TO_CHAR(SUM(CASE WHEN (MOQ.ORGANIZATION_ID = 104 OR MOQ.ORGANIZATION_ID = 148) AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS CD_DISUSE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_INACTIVE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_NG_MAT  "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 105 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS IC_DISUSE "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '1' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_ACTIVE"
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '2' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_INACTIVE    "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '4' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_NG_MAT     "
  + "     , TO_CHAR(SUM(CASE WHEN MOQ.ORGANIZATION_ID = 108 AND MIS.ATTRIBUTE9 = '3' THEN MOQ.TRANSACTION_QUANTITY ELSE 0 END), 'fm999,999,999')    AS VH_DISUSE                              "
    + "FROM MTL_SYSTEM_ITEMS_B          MSI   "
    + " , MTL_ONHAND_QUANTITIES       MOQ   "
    + " , MTL_SECONDARY_INVENTORIES   MIS   "
    + "    WHERE MSI.ORGANIZATION_ID = 107   "
    + " AND MSI.SEGMENT1 NOT LIKE '%OSP'   "
    + "    AND MSI.SEGMENT1 NOT LIKE '%IC'    "
    + "  AND  (MSI.SEGMENT1 LIKE 'CJT%' "
 + "      OR MSI.SEGMENT1 LIKE 'CSA%'   "
 + "      OR MSI.SEGMENT1 LIKE 'CSB%'   "
 + "      OR MSI.SEGMENT1 LIKE 'CSD%'       "
 + "      OR MSI.SEGMENT1 LIKE 'CSK%'   "
 + "      OR MSI.SEGMENT1 LIKE 'CST%'   "
 + "      OR MSI.SEGMENT1 LIKE 'CSV%'       "
 + "      OR MSI.SEGMENT1 LIKE 'CSX%'      "
 + "      OR MSI.SEGMENT1 LIKE 'CTV%'       "
 + "      OR MSI.SEGMENT1 LIKE 'CTJ%'      "
 + "      OR MSI.SEGMENT1 LIKE 'CBA%'       "
 + "      OR MSI.SEGMENT1 LIKE 'SBA%'       "
+ "      )                          "
   + "AND MOQ.INVENTORY_ITEM_ID = MSI.INVENTORY_ITEM_ID "
+ "   AND MOQ.SUBINVENTORY_CODE = MIS.SECONDARY_INVENTORY_NAME  "
   + "AND MOQ.ORGANIZATION_ID   = MIS.ORGANIZATION_ID   "
+ "   AND MOQ.ORGANIZATION_ID IN (102, 103, 104, 105, 108, 148) "
+ "   AND MOQ.TRANSACTION_QUANTITY <> 0                 "
+ strWhere
+ " GROUP BY    "
+ "       MSI.SEGMENT1  "
+ "     , MSI.DESCRIPTION   "
        + "     , MSI.INVENTORY_ITEM_ID    ";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "SEGMENT1", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 기준 환율 가져오기(원화 -> ?)
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_GL_DAILY_RATES_V(string fromCurrency, string toCurrency, string searchDate)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT FROM_CURRENCY,TO_CURRENCY,CONVERSION_DATE,SHOW_CONVERSION_RATE AS FROM_TO_CON_RATE,SHOW_INVERSE_CON_RATE AS TO_FROM_CON_RATE"
                    + " FROM GL_DAILY_RATES_V WHERE status_code != 'D' AND (FROM_CURRENCY = '" + fromCurrency + "') AND (TO_CURRENCY = '" + toCurrency
                    + "') AND (USER_CONVERSION_TYPE LIKE 'CHQ_Corporate') AND (TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd') = '" + searchDate
                    + "') ORDER BY FROM_CURRENCY, TO_CURRENCY, CONVERSION_DATE DESC, USER_CONVERSION_TYPE";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "CURRENCY", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 기준 환율 가져오기
        /// </summary>
        /// <param name="fromCurrency"></param>
        /// <param name="toCurrency"></param>
        /// <param name="conversionType"></param>
        /// <param name="conversionDate"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_GL_DAILY_RATES_V(string fromCurrency, string toCurrency, string conversionType, string conversionDate)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT FROM_CURRENCY,TO_CURRENCY,CONVERSION_DATE,SHOW_CONVERSION_RATE AS CON_RATE,SHOW_INVERSE_CON_RATE AS INV_RATE"
                    + " FROM GL_DAILY_RATES_V WHERE status_code != 'D' AND FROM_CURRENCY = '" + fromCurrency + "' AND TO_CURRENCY = '" + toCurrency
                    + "' AND USER_CONVERSION_TYPE = '" + conversionType + "' AND TO_CHAR(CONVERSION_DATE,'yyyy-mm-dd') = '" + conversionDate + "'";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "CURRENCY", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }
        #endregion

        #region [불용제품처리기안에서 사용]
        /// <summary>
        /// 크레신 - 당월 불량 수량 가져오기  김승재
        /// </summary>
        /// <param name="statYear"></param>
        /// <param name="statMonth"></param>
        /// <param name="erpId"></param>
        /// <param name="erpSubId"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_MONTH_FAULTY(string statYear, string statMonth, string erpId, string erpSubId)
        {
            DataSet dsReturn = null;

            try
            {
                if (statMonth.Length == 1) statMonth = "0" + statMonth;
                string strDate = statYear + "-" + statMonth;

                string strQuery = @"
SELECT INA.TRAN_MM             
     , ROUND(NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
         FROM MTL_ONHAND_QUANTITIES   MOQ             
            , MTL_SECONDARY_INVENTORIES   MSV             
        WHERE MOQ.ORGANIZATION_ID       = INA.ORGANIZATION_ID             
          AND MOQ.ORGANIZATION_ID       = MSV.ORGANIZATION_ID
          AND MOQ.SUBINVENTORY_CODE       = MSV.SECONDARY_INVENTORY_NAME    
          AND MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WRB'              
          AND NVL(MSV.ATTRIBUTE5, 2)      = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
          
    ), 0) - INA.T_S_WRB)   작업불량시점재고               
    , DECODE(SIGN(당월작업불량입고수량), 1, 당월작업불량입고수량, 0)     AS 당월작업불량입고수량
    , NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
         FROM MTL_ONHAND_QUANTITIES   MOQ             
            , MTL_SECONDARY_INVENTORIES   MSV             
        WHERE MOQ.ORGANIZATION_ID       = INA.ORGANIZATION_ID             
          AND MOQ.ORGANIZATION_ID       = MSV.ORGANIZATION_ID
          AND MOQ.SUBINVENTORY_CODE       = MSV.SECONDARY_INVENTORY_NAME    
          AND (MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WAR' OR MSV.SECONDARY_INVENTORY_NAME LIKE 'S%DISUSE')               
          AND NVL(MSV.ATTRIBUTE5, 2)      = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
    ), 0) - INA.T_S_WAR   자재시점재고             
    , DECODE(SIGN(당월자재불량입고수량), 1, 당월자재불량입고수량, 0)     AS 당월자재불량입고수량
    , NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
        FROM MTL_ONHAND_QUANTITIES   MOQ             
            , MTL_SECONDARY_INVENTORIES   MSV             
        WHERE MOQ.ORGANIZATION_ID      = INA.ORGANIZATION_ID      
        AND MOQ.ORGANIZATION_ID        = MSV.ORGANIZATION_ID       
        AND MOQ.SUBINVENTORY_CODE      = MSV.SECONDARY_INVENTORY_NAME    
        AND MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WAS'              
        AND NVL(MSV.ATTRIBUTE5, 2)     = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
    ), 0) - INA.T_S_WAS   반제시점재고             
    , DECODE(SIGN(당월반제불량입고수량), 1, 당월반제불량입고수량, 0)     AS 당월반제불량입고수량
    , NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
        FROM MTL_ONHAND_QUANTITIES   MOQ             
            , MTL_SECONDARY_INVENTORIES   MSV             
        WHERE MOQ.ORGANIZATION_ID     = INA.ORGANIZATION_ID    
        AND MOQ.ORGANIZATION_ID       = MSV.ORGANIZATION_ID         
        AND MOQ.SUBINVENTORY_CODE     = MSV.SECONDARY_INVENTORY_NAME    
        AND MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WAF'              
        AND NVL(MSV.ATTRIBUTE5, 2)    = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
    ), 0) - INA.T_S_WAF 제품시점재고     
    , DECODE(SIGN(당월제품불량입고수량), 1, 당월제품불량입고수량, 0)     AS 당월제품불량입고수량     
FROM (             
        SELECT             
                '2018-06'  TRAN_MM             
            , MMT.ORGANIZATION_ID             
            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1 
                            AND MMT.SUBINVENTORY_CODE   LIKE 'S%WRB'                 
                            THEN MMT.TRANSACTION_QUANTITY                             
                            ELSE 0     END                                               
                    )         당월작업불량입고수량             
            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WRB' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WRB   
            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1          
                            AND ( MMT.SUBINVENTORY_CODE   LIKE 'S%WAR' OR MMT.SUBINVENTORY_CODE   LIKE 'S%DISUSE')                                                                                            
                            THEN MMT.TRANSACTION_QUANTITY                                                          
                            ELSE 0     END                                                                         
                    )         당월자재불량입고수량             
            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WAR' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WAR   
            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1          
                            AND MMT.SUBINVENTORY_CODE   LIKE 'S%WAS'                                              
                            THEN MMT.TRANSACTION_QUANTITY                                                          
                            ELSE 0     END                                                                           
                    )         당월반제불량입고수량             
            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WAS' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WAS   
            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1          
                            AND MMT.SUBINVENTORY_CODE   LIKE 'S%WAF'                                              
                            THEN MMT.TRANSACTION_QUANTITY                                                          
                            ELSE 0     END                                                                         
                    )         당월제품불량입고수량             
            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WAF' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WAF   
        FROM MTL_MATERIAL_TRANSACTIONS   MMT             
            , MTL_SECONDARY_INVENTORIES   MSV             
        WHERE MMT.ORGANIZATION_ID       = " + erpId + @"
          AND MMT.TRANSACTION_DATE   >= TO_DATE('" + strDate + @"', 'YYYY-MM')               
          AND MMT.TRANSACTION_DATE   <  SYSDATE + 1             
          AND MMT.ORGANIZATION_ID        = MSV.ORGANIZATION_ID             
          AND MMT.SUBINVENTORY_CODE      = MSV.SECONDARY_INVENTORY_NAME             
          AND MSV.ATTRIBUTE4             = 3                    
          AND NVL(MSV.ATTRIBUTE5, 2)  = DECODE(" + erpId + @", 103, " + erpSubId + @", 2)                       
        GROUP BY             
                MMT.ORGANIZATION_ID              
    )  INA";

//        SELECT INA.TRAN_MM             
//    , ROUND(NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
//        FROM MTL_ONHAND_QUANTITIES   MOQ             
//            , MTL_SECONDARY_INVENTORIES   MSV             
//        WHERE MOQ.ORGANIZATION_ID   = INA.ORGANIZATION_ID             
//        AND MOQ.SUBINVENTORY_CODE = MSV.SECONDARY_INVENTORY_NAME    
//        AND MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WRB'              
//        AND NVL(MSV.ATTRIBUTE5, 2)  = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
//    ), 0) - INA.T_S_WRB)   작업불량시점재고               
//    , DECODE(SIGN(당월작업불량입고수량), 1, 당월작업불량입고수량, 0)     AS 당월작업불량입고수량
//    , NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
//        FROM MTL_ONHAND_QUANTITIES   MOQ             
//            , MTL_SECONDARY_INVENTORIES   MSV             
//        WHERE MOQ.ORGANIZATION_ID   = INA.ORGANIZATION_ID             
//        AND MOQ.SUBINVENTORY_CODE = MSV.SECONDARY_INVENTORY_NAME    
//        AND (MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WAR' OR MSV.SECONDARY_INVENTORY_NAME LIKE 'S%DISUSE')               
//        AND NVL(MSV.ATTRIBUTE5, 2)  = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
//    ), 0) - INA.T_S_WAR   자재시점재고             
//    , DECODE(SIGN(당월자재불량입고수량), 1, 당월자재불량입고수량, 0)     AS 당월자재불량입고수량
//    , NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
//        FROM MTL_ONHAND_QUANTITIES   MOQ             
//            , MTL_SECONDARY_INVENTORIES   MSV             
//        WHERE MOQ.ORGANIZATION_ID   = INA.ORGANIZATION_ID             
//        AND MOQ.SUBINVENTORY_CODE = MSV.SECONDARY_INVENTORY_NAME    
//        AND MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WAS'              
//        AND NVL(MSV.ATTRIBUTE5, 2)  = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
//    ), 0) - INA.T_S_WAS   반제시점재고             
//    , DECODE(SIGN(당월반제불량입고수량), 1, 당월반제불량입고수량, 0)     AS 당월반제불량입고수량
//    , NVL((SELECT SUM(MOQ.TRANSACTION_QUANTITY)             
//        FROM MTL_ONHAND_QUANTITIES   MOQ             
//            , MTL_SECONDARY_INVENTORIES   MSV             
//        WHERE MOQ.ORGANIZATION_ID   = INA.ORGANIZATION_ID             
//        AND MOQ.SUBINVENTORY_CODE = MSV.SECONDARY_INVENTORY_NAME    
//        AND MSV.SECONDARY_INVENTORY_NAME LIKE 'S%WAF'              
//        AND NVL(MSV.ATTRIBUTE5, 2)  = DECODE(INA.ORGANIZATION_ID, 103, " + erpSubId + @", 2)          
//    ), 0) - INA.T_S_WAF 제품시점재고     
//    , DECODE(SIGN(당월제품불량입고수량), 1, 당월제품불량입고수량, 0)     AS 당월제품불량입고수량     
//FROM (             
//        SELECT             
//                '" + strDate + @"'  TRAN_MM             
//            , MMT.ORGANIZATION_ID             
//            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1 
//                            AND MMT.SUBINVENTORY_CODE   LIKE 'S%WRB'                 
//                            THEN MMT.TRANSACTION_QUANTITY                             
//                            ELSE 0     END                                               
//                    )         당월작업불량입고수량             
//            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WRB' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WRB   
//            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1          
//                            AND ( MMT.SUBINVENTORY_CODE   LIKE 'S%WAR' OR MMT.SUBINVENTORY_CODE   LIKE 'S%DISUSE')                                                                                            
//                            THEN MMT.TRANSACTION_QUANTITY                                                          
//                            ELSE 0     END                                                                         
//                    )         당월자재불량입고수량             
//            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WAR' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WAR   
//            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1          
//                            AND MMT.SUBINVENTORY_CODE   LIKE 'S%WAS'                                              
//                            THEN MMT.TRANSACTION_QUANTITY                                                          
//                            ELSE 0     END                                                                           
//                    )         당월반제불량입고수량             
//            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WAS' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WAS   
//            , SUM(CASE WHEN MMT.TRANSACTION_DATE <  LAST_DAY(TO_DATE('" + strDate + @"', 'YYYY-MM')) + 1          
//                            AND MMT.SUBINVENTORY_CODE   LIKE 'S%WAF'                                              
//                            THEN MMT.TRANSACTION_QUANTITY                                                          
//                            ELSE 0     END                                                                         
//                    )         당월제품불량입고수량             
//            , SUM( CASE WHEN MMT.SUBINVENTORY_CODE LIKE 'S%WAF' THEN MMT.TRANSACTION_QUANTITY ELSE 0 END) T_S_WAF   
//        FROM MTL_MATERIAL_TRANSACTIONS   MMT             
//            , MTL_SECONDARY_INVENTORIES   MSV             
//        WHERE MMT.ORGANIZATION_ID       = " + erpId + @"
//            AND MMT.TRANSACTION_DATE   >= TO_DATE('" + strDate + @"', 'YYYY-MM')               
//            AND MMT.TRANSACTION_DATE   <  SYSDATE + 1             
//            AND MMT.ORGANIZATION_ID        = MSV.ORGANIZATION_ID             
//            AND MMT.SUBINVENTORY_CODE      = MSV.SECONDARY_INVENTORY_NAME             
//            AND MSV.ATTRIBUTE4             = 3           
//            AND MSV.SECONDARY_INVENTORY_NAME NOT LIKE 'S%DIS%'           
//            AND NVL(MSV.ATTRIBUTE5, 2)  = DECODE(" + erpId + @", 103, " + erpSubId + @", 2)                       
//        GROUP BY             
//                MMT.ORGANIZATION_ID              
//    )  INA";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "FAULTY", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 크레신 - 불양제품처리기안 작업불량 공수 가져오기
        /// </summary>
        /// <param name="statYear"></param>
        /// <param name="statMonth"></param>
        /// <param name="erpId"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_MONTH_FAULTY_GONGSU(string statYear, string statMonth, string erpId)
        {
            DataSet dsReturn = null;

            try
            {
                int iLastDay = DateTime.DaysInMonth(Convert.ToInt32(statYear), Convert.ToInt32(statMonth));

                if (statMonth.Length == 1) statMonth = "0" + statMonth;
                string strFromDate = statYear + statMonth + "01";
                string strToDate = statYear + statMonth + iLastDay.ToString();

                string strQuery = @"
--inv org
--inv org
SELECT ORGANIZATION_ID
      ,ORGANIZATION_CODE
      ,TO_CHAR(SUM(GOOD_QTY)) GOOD_QTY
      ,TO_CHAR(SUM(NG_QTY)) NG_QTY
      ,TO_CHAR(SUM(GOOD_TIME)) GOOD_TIME
      ,TO_CHAR(SUM(NG_TIME)) NG_TIME
FROM ( SELECT 'NEW'
             ,CWT.ORGANIZATION_ID
             ,OOD.ORGANIZATION_CODE
             ,NULL ITEM_CODE
             ,SUM(NVL(CWT.REQUEST_QTY,0)) GOOD_QTY
             ,SUM(NVL(CWT.DEFECT_QTY,0)) NG_QTY
             ,SUM(NVL(CWT.REQUEST_QTY,0))/(SUM(NVL(CWT.REQUEST_QTY,0))+SUM(NVL(CWT.DEFECT_QTY,0)))*(SUM(CWV.A_UNIT)+SUM(CWV.F_UNIT)+SUM(CWV.D_UNIT)-SUM(CWV.C_UNIT)) GOOD_TIME
             ,SUM(NVL(CWT.DEFECT_QTY,0))/(SUM(NVL(CWT.REQUEST_QTY,0))+SUM(NVL(CWT.DEFECT_QTY,0)))*(SUM(CWV.A_UNIT)+SUM(CWV.F_UNIT)+SUM(CWV.D_UNIT)-SUM(CWV.C_UNIT)) NG_TIME
       FROM  (
              SELECT ORGANIZATION_ID
                    ,PARENT_FLAG
                    ,PRODUCTION_LINE_ID
                    ,WORK_DATE
                    ,ATTRIBUTE1
                    ,SUM(REQUEST_QTY) REQUEST_QTY
                    ,SUM(DEFECT_QTY) DEFECT_QTY
              FROM (
              SELECT CWT.ORGANIZATION_ID
                    ,CWT.PARENT_FLAG
                    ,CWT.PRODUCTION_LINE_ID
                    ,TRUNC(CWT.WORK_DATE) WORK_DATE
                    ,CWT.ITEM_CODE
                    ,CWT.ATTRIBUTE1
                    ,SUM(NVL(CWT.REQUEST_QTY,0)) REQUEST_QTY
                    ,SUM(NVL(CWT.DEFECT_QTY,0)) DEFECT_QTY
              FROM   CBO_WIP_WO_TRX_UPD_V CWT
                    ,WIP_DISCRETE_JOBS WDJ
                    ,CBO_WIP_MAN_HOUR_BY_WO_V CWH
              WHERE  CWT.WORK_DATE >= TO_DATE('" + strFromDate + @"','YYYYMMDD')
              AND    CWT.WORK_DATE <  TO_DATE('" + strToDate + @"','YYYYMMDD') + 1
              AND    CWT.ORGANIZATION_ID = WDJ.ORGANIZATION_ID
              AND    CWT.WIP_ENTITY_ID = WDJ.WIP_ENTITY_ID
              AND    CWH.ORGANIZATION_ID = CWT.ORGANIZATION_ID
              AND    CWH.PRODUCTION_LINE_ID = CWT.PRODUCTION_LINE_ID
              AND    CWH.SHIFT = CWT.ATTRIBUTE1
              AND    TO_CHAR(CWH.WORK_DATE, 'YYYYMMDD') = TO_CHAR(CWT.WORK_DATE, 'YYYYMMDD')
              AND    CWT.PARENT_FLAG = 'Y'
              GROUP BY
                     CWT.ORGANIZATION_ID
                    ,CWT.PARENT_FLAG
                    ,CWT.PRODUCTION_LINE_ID
                    ,TRUNC(CWT.WORK_DATE)
                    ,CWT.ITEM_CODE
                    ,CWT.ATTRIBUTE1
              HAVING MAX(WDJ.QUANTITY_COMPLETED) <> 0
              )
              GROUP BY
                     ORGANIZATION_ID
                    ,PARENT_FLAG
                    ,PRODUCTION_LINE_ID
                    ,WORK_DATE
                    ,ATTRIBUTE1) CWT
             ,CBO_WIP_MAN_HOUR_BY_WO_V CWV
             ,ORG_ORGANIZATION_DEFINITIONS OOD
       WHERE  CWT.ORGANIZATION_ID = CWV.ORGANIZATION_ID
       AND    CWT.ORGANIZATION_ID = " + erpId + @"
       AND    CWT.PARENT_FLAG = 'Y'
       AND    CWT.PRODUCTION_LINE_ID = CWV.PRODUCTION_LINE_ID
       AND    CWT.ORGANIZATION_ID = OOD.ORGANIZATION_ID
       AND    TO_CHAR(CWT.WORK_DATE, 'YYYYMMDD') = TO_CHAR(CWV.WORK_DATE, 'YYYYMMDD')
       AND    CWT.WORK_DATE >= TO_DATE('" + strFromDate + @"','YYYYMMDD')
       AND    CWT.WORK_DATE <  TO_DATE('" + strToDate + @"','YYYYMMDD') + 1
       AND    CWV.SHIFT = CWT.ATTRIBUTE1
       GROUP BY
              CWT.ORGANIZATION_ID
             ,OOD.ORGANIZATION_CODE
       UNION ALL
       --OLD
       SELECT 'OLD'
             ,ORGANIZATION_ID
             ,ORGANIZATION_CODE
             ,ITEM_CODE
             ,GOOD_QTY
             ,NG_QTY
             ,(GOOD_QTY/DECODE((GOOD_QTY+NG_QTY), 0, NULL, (GOOD_QTY+NG_QTY)))*(NORMAL_TIME+OVER_TIME+INPUT_TIME-OUTPUT_TIME) GOOD_TIME
             ,(NG_QTY/DECODE((GOOD_QTY+NG_QTY), 0, NULL, (GOOD_QTY+NG_QTY)))*(NORMAL_TIME+OVER_TIME+INPUT_TIME-OUTPUT_TIME) NG_TIME
       FROM ( SELECT ORGANIZATION_ID
                    ,ORGANIZATION_CODE
                    ,PRIMARY_ITEM_ID
                    ,ITEM_CODE
                    ,SUM(GOOD_QTY) GOOD_QTY
                    ,SUM(NORMAL_TIME) NORMAL_TIME
                    ,SUM(OVER_TIME) OVER_TIME
                    ,SUM(INPUT_TIME) INPUT_TIME
                    ,SUM(OUTPUT_TIME) OUTPUT_TIME
                    ,(SELECT NVL(SUM(MMT.TRANSACTION_QUANTITY*-1) ,0)
                      FROM   MTL_MATERIAL_TRANSACTIONS MMT
                      WHERE  MMT.TRANSACTION_TYPE_ID IN (2,64) --Subinventory Transfer / Move Order Transfer
                      AND    MMT.TRANSFER_SUBINVENTORY = 'W-NG'
                      AND    MMT.TRANSACTION_DATE >=  TO_DATE('" + strFromDate + @"','YYYYMMDD')
                      AND    MMT.TRANSACTION_DATE <   TO_DATE('" + strToDate + @"','YYYYMMDD') + 1
                      AND    MMT.ORGANIZATION_ID = ORGANIZATION_ID
                      AND    MMT.INVENTORY_ITEM_ID = PRIMARY_ITEM_ID
                      AND    MMT.TRANSACTION_QUANTITY < 0 ) NG_QTY
              FROM (SELECT OOD.ORGANIZATION_ID
                          ,OOD.ORGANIZATION_CODE
                          ,CWJ.WIP_ENTITY_ID
                          ,CWT.TRANSACTION_ID
                          ,CWJ.PRIMARY_ITEM_ID
                          ,MSI.SEGMENT1 ITEM_CODE
                          ,MAX(NVL(CWT.ACTUAL_QTY,0)) GOOD_QTY
                          ,SUM(CRT.NORMAL_TIME) NORMAL_TIME
                          ,SUM(CRT.OVER_TIME) OVER_TIME
                          ,SUM(CRT.INPUT_TIME) INPUT_TIME
                          ,SUM(CRT.OUTPUT_TIME) OUTPUT_TIME
                    FROM   WIP_DISCRETE_JOBS CWJ
                          ,CBO_WIP_MOVE_TRANSACTIONS CWT
                          ,(SELECT CRT.ORGANIZATION_ID
                                  ,CRT.WIP_ENTITY_ID
                                  ,CRT.TRANSACTION_ID
                                  ,NVL(CRT.ACTIVITY_TIME,0) NORMAL_TIME
                                  ,0 OVER_TIME
                                  ,0 INPUT_TIME
                                  ,0 OUTPUT_TIME
                            FROM   CBO_WIP_RSC_TRANSACTIONS CRT
                            WHERE  CRT.ACTIVITY_ID = 1003 --Normal
                            UNION ALL
                            SELECT CRT.ORGANIZATION_ID
                                  ,CRT.WIP_ENTITY_ID
                                  ,CRT.TRANSACTION_ID
                                  ,0 NORMAL_TIME
                                  ,NVL(CRT.ACTIVITY_TIME,0) OVER_TIME
                                  ,0 INPUT_TIME
                                  ,0 OUTPUT_TIME
                            FROM   CBO_WIP_RSC_TRANSACTIONS CRT
                            WHERE  CRT.ACTIVITY_ID = 1005 --Overtime
                            UNION ALL
                            SELECT CRT.ORGANIZATION_ID
                                  ,CRT.WIP_ENTITY_ID
                                  ,CRT.TRANSACTION_ID
                                  ,0 NORMAL_TIME
                                  ,0 OVER_TIME
                                  ,NVL(CRT.ACTIVITY_TIME,0) INPUT_TIME
                                  ,0 OUTPUT_TIME
                            FROM   CBO_WIP_RSC_TRANSACTIONS CRT
                            WHERE  CRT.ACTIVITY_ID = 1000 --Input
                            UNION ALL
                            SELECT CRT.ORGANIZATION_ID
                                  ,CRT.WIP_ENTITY_ID
                                  ,CRT.TRANSACTION_ID
                                  ,0 NORMAL_TIME
                                  ,0 OVER_TIME
                                  ,0 INPUT_TIME
                                  ,NVL(CRT.ACTIVITY_TIME,0) OUTPUT_TIME
                            FROM   CBO_WIP_RSC_TRANSACTIONS CRT
                            WHERE  CRT.ACTIVITY_ID = 1004 --Output
                           ) CRT
                          ,ORG_ORGANIZATION_DEFINITIONS OOD
                          ,MTL_SYSTEM_ITEMS_B MSI
                    WHERE  CWJ.ORGANIZATION_ID = CWT.ORGANIZATION_ID
                    AND    CWJ.WIP_ENTITY_ID = CWT.WIP_ENTITY_ID
                    AND    CWJ.ORGANIZATION_ID = CRT.ORGANIZATION_ID
                    AND    CWJ.WIP_ENTITY_ID = CRT.WIP_ENTITY_ID
                    AND    CWT.TRANSACTION_ID = CRT.TRANSACTION_ID
                    AND    CWJ.ORGANIZATION_ID = OOD.ORGANIZATION_ID
                    AND    CWJ.ORGANIZATION_ID = MSI.ORGANIZATION_ID
                    AND    CWJ.PRIMARY_ITEM_ID = MSI.INVENTORY_ITEM_ID
                    AND    CWJ.ORGANIZATION_ID IN (SELECT ORGANIZATION_ID
                                                   FROM   MTL_SECONDARY_INVENTORIES
                                                   WHERE  SECONDARY_INVENTORY_NAME = 'W-NG')
                    AND    CWJ.ORGANIZATION_ID = " + erpId + @"
                    AND    CWT.TRANSACTION_DATE >=  TO_DATE('" + strFromDate + @"','YYYYMMDD')
                    AND    CWT.TRANSACTION_DATE <   TO_DATE('" + strToDate + @"','YYYYMMDD') + 1
                    GROUP BY
                           OOD.ORGANIZATION_CODE
                          ,OOD.ORGANIZATION_ID
                          ,CWJ.WIP_ENTITY_ID
                          ,CWJ.PRIMARY_ITEM_ID
                          ,CWT.TRANSACTION_ID
                          ,MSI.SEGMENT1
                   )
              GROUP BY
                     ORGANIZATION_ID
                    ,ORGANIZATION_CODE
                    ,PRIMARY_ITEM_ID
                    ,ITEM_CODE
             )
      )
GROUP BY
       ORGANIZATION_ID
      ,ORGANIZATION_CODE
";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "GONGSU", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }
        #endregion

        #region [설계사양변경통보서에서 사용]
        /// <summary>
        /// 크레신 - 당월 불량 수량 가져오기  김승재
        /// </summary>
        /// <param name="statItem"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_SPEC_CHANGE(string statItem)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = @"

  SELECT MSI.SEGMENT1
       , SUM(DECODE(MOT.ORGANIZATION_ID , 148, NVL(MOT.ON_HAND, 0), 0))  AS CD_QTY
, SUM(DECODE(MOT.ORGANIZATION_ID , 102, NVL(MOT.ON_HAND, 0), 0))  AS CH_QTY
       , SUM(DECODE(MOT.ORGANIZATION_ID , 103, NVL(MOT.ON_HAND, 0), 0))  AS CT_QTY
, SUM(DECODE(MOT.ORGANIZATION_ID , 108, NVL(MOT.ON_HAND, 0), 0))  AS VH_QTY
       , SUM(DECODE(MOT.ORGANIZATION_ID , 105, NVL(MOT.ON_HAND, 0), 0))  AS IC_QTY       
       , SUM(DECODE(MOT.ORGANIZATION_ID , 128, NVL(MOT.ON_HAND, 0), 0))  AS IS_QTY                                                                         
   FROM  MTL_ONHAND_TOTAL_MWB_V    MOT
       , MTL_SYSTEM_ITEMS_B        MSI
   WHERE MOT.ORGANIZATION_ID     = MSI.ORGANIZATION_ID
     AND MOT.INVENTORY_ITEM_ID   = MSI.INVENTORY_ITEM_ID       
     AND MOT.ORGANIZATION_ID   IN (102, 103, 148, 105, 108, 128)               
     AND EXISTS  (SELECT MIS.SECONDARY_INVENTORY_NAME
                    FROM MTL_SECONDARY_INVENTORIES MIS     
                   WHERE MIS.ORGANIZATION_ID IN (102, 103, 148, 105, 108, 128)
                     AND MIS.ATTRIBUTE9      IN ('1', '2', '6')
                     AND MIS.SECONDARY_INVENTORY_NAME = MOT.SUBINVENTORY_CODE
                  )                                      
     AND MSI.SEGMENT1 = '" + statItem + @"'                                     
   GROUP BY 
         MSI.SEGMENT1";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "FAULTY", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }
        #endregion

        #region [금형에서 사용]
        /// <summary>
        /// 크레신 -제작업체 / 사출업체 / BUYER 가져오기 => orgid가 필요없는 경우
        /// </summary>
        /// <param name="type"></param>
        /// <param name="searchCol"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PO_VENDORS_CUSTOMERS(string type, string searchCol, string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            string strQuery1 = "";
            string strQuery2 = "";

            try
            {
                if (type == "BUYER")
                {
                    strQuery1 = "SELECT COUNT(*) FROM AR_CUSTOMERS_V WHERE " + searchCol + " LIKE '%" + search + "%'";

                    strQuery2 = @"SELECT a.* FROM (SELECT CUSTOMER_ID VENDOR_ID, CUSTOMER_NUMBER VENDOR_NUMBER, CUSTOMER_NAME VENDOR_NAME, 0 VENDOR_SITE_ID
FROM AR_CUSTOMERS_V WHERE " + searchCol + " LIKE '%" + search + "%' ORDER BY CUSTOMER_NAME) a WHERE ROWNUM BETWEEN " + iS + " AND " + iE;
                }
                else
                {
                    strQuery1 = @"SELECT COUNT(*) FROM PO_VENDORS A , PO_VENDOR_SITES_ALL  B
WHERE NVL(A.ENABLED_FLAG, 'N') = 'Y'  AND NVL(A.VENDOR_TYPE_LOOKUP_CODE, 'SUPPLIER') <> 'EMPLOYEE'  AND A.VENDOR_ID = B.VENDOR_ID  AND A." + searchCol + " LIKE '%" + search + "%'";

                    strQuery2 = @"SELECT a.* FROM (SELECT A.VENDOR_ID, A.SEGMENT1 VENDOR_NUMBER, A.VENDOR_NAME , B.VENDOR_SITE_ID
FROM PO_VENDORS A , PO_VENDOR_SITES_ALL  B
WHERE NVL(A.ENABLED_FLAG, 'N') = 'Y' AND NVL(A.VENDOR_TYPE_LOOKUP_CODE, 'SUPPLIER') <> 'EMPLOYEE'  AND A.VENDOR_ID = B.VENDOR_ID AND A." + searchCol + " LIKE '%" + search + "%' ORDER BY A.VENDOR_NAME) a WHERE ROWNUM BETWEEN " + iS + " AND " + iE;
                }

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "VENDORS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 크레신 -제작업체 / 사출업체 / BUYER 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <param name="orgId"></param>
        /// <param name="searchCol"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_PO_VENDORS_CUSTOMERS(string type, string orgId, string searchCol, string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            string strQuery1 = "";
            string strQuery2 = "";

            try
            {
                if (type == "BUYER")
                {
                    strQuery1 = "SELECT COUNT(*) FROM AR_CUSTOMERS_V WHERE " + searchCol + " LIKE '%" + search + "%'";

                    strQuery2 = @"SELECT a.* FROM (SELECT CUSTOMER_ID VENDOR_ID, CUSTOMER_NUMBER VENDOR_NUMBER, CUSTOMER_NAME VENDOR_NAME, 0 VENDOR_SITE_ID
FROM AR_CUSTOMERS_V WHERE " + searchCol + " LIKE '%" + search + "%' ORDER BY CUSTOMER_NAME) a WHERE ROWNUM BETWEEN " + iS + " AND " + iE;
                }
                else
                {
                    strQuery1 = @"SELECT COUNT(*) FROM PO_VENDORS A, PO_VENDOR_SITES_ALL B, MTL_PARAMETERS_VIEW C 
WHERE A.VENDOR_ID= B.VENDOR_ID AND B.ORG_ID= C.ORGANIZATION_ID
AND C.ORGANIZATION_ID= " + orgId + @"
AND A." + searchCol + " LIKE '%" + search + "%'";

                    strQuery2 = @"SELECT a.* FROM (SELECT A.VENDOR_ID, A.SEGMENT1 VENDOR_NUMBER, A.VENDOR_NAME, B.VENDOR_SITE_ID
FROM PO_VENDORS A, PO_VENDOR_SITES_ALL B, MTL_PARAMETERS_VIEW C
WHERE A.VENDOR_ID= B.VENDOR_ID AND B.ORG_ID= C.ORGANIZATION_ID AND C.ORGANIZATION_ID= " + orgId + @"
AND A." + searchCol + " LIKE '%" + search + "%' ORDER BY A.VENDOR_NAME) a WHERE ROWNUM BETWEEN " + iS + " AND " + iE;
                }

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "VENDORS", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }
        #endregion

        #region [전표에서 사용]
        /// <summary>
        /// 기본 정보 가져오기 (조직, 통화, 계정 등)
        /// </summary>
        /// <param name="gubun"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_AP_GWARE_INFO_V(string gubun, string orgId)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT * FROM CBO_AP_GWARE_INFO_V WHERE GUBUN = '" + gubun + "'";
                if (orgId != "") strQuery += " AND ORG_ID = '" + orgId + "'";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "APGWAREINFO", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gubun"></param>
        /// <param name="orgId"></param>
        /// <param name="searchCol"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_AP_GWARE_INFO_V(string gubun, string orgId, string searchCol, string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM CBO_AP_GWARE_INFO_V WHERE GUBUN = '" + gubun + "' AND ORG_ID = '" + orgId + "' AND " + searchCol + " LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (SELECT AG.*, ROWNUM RN FROM CBO_AP_GWARE_INFO_V AG WHERE AG.GUBUN = '" + gubun + "' AND AG.ORG_ID = '" + orgId + "' AND AG." + searchCol + " LIKE '%" + search + "%') WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "APGWAREINFOCNT", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 거래처 가져오기
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_AP_GWARE_VENDOR_INFO_V(string orgId, string search, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM CBO_AP_GWARE_VENDOR_INFO_V PV WHERE PV.ORG_ID = '" + orgId + "' AND PV.DESCRIPTION LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (SELECT PV.*, ROWNUM RN FROM CBO_AP_GWARE_VENDOR_INFO_V PV WHERE PV.ORG_ID = '" + orgId + "' AND PV.DESCRIPTION LIKE '%" + search + "%') WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "APVENDORINFO", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// VALUE SET 가져오기
        /// </summary>
        /// <param name="contextCode"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_AP_INVOICE_DFF_V(string contextCode)
        {
            DataSet dsReturn = null;

            try
            {
                string strQuery = "SELECT END_USER_COLUMN_NAME, APPLICATION_COLUMN_NAME, COLUMN_ENABLED_FLAG, COLUMN_REQUIRED_FLAG, FLEX_VALUE_SET_NAME, MAXIMUM_SIZE, FORMAT_TYPE, VALIDATION_TYPE FROM CBO_AP_INVOICE_DFF_V WHERE DESCRIPTIVE_FLEX_CONTEXT_CODE = '" + contextCode + "'";

                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(strQuery, "text", "APINVOICEDFF", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// VALUESET 실행 - 단순 권리
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_AP_VALUESET(string query)
        {
            DataSet dsReturn = null;

            try
            {
                OracleParameter[] parameters = null;
                ParamData pData = new ParamData(query, "text", "APVALUESET", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return dsReturn;
        }

        /// <summary>
        /// VALUESET 실행 - 페이지 권리
        /// </summary>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet Cresyn_Get_CBO_AP_VALUESET(string query1, string query2, int pageSize, int page, out int totalCount)
        {
            DataSet dsReturn = null;
            string rt = "";
            int iS = 1, iE = pageSize;

            if (page > 1)
            {
                iS = (pageSize * (page - 1)) + 1;
                iE = iS + (pageSize - 1);
            }

            try
            {
                string strQuery1 = "SELECT COUNT(*) FROM " + query1;//CBO_AP_GWARE_VENDOR_INFO_V PV WHERE PV.ORG_ID = '" + orgId + "' AND PV.DESCRIPTION LIKE '%" + search + "%'";

                string strQuery2 = "SELECT * FROM (" + query2 + ") WHERE RN BETWEEN " + iS + " AND " + iE;

                OracleParameter[] parameters = null;

                ParamData pData1 = new ParamData(strQuery1, "text", 30, parameters);
                ParamData pData2 = new ParamData(strQuery2, "text", "APVALUESET", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    rt = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData1);
                    dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData2);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            totalCount = Framework.Util.StringHelper.SafeInt(rt);
            return dsReturn;
        }

        /// <summary>
        /// 전표번호 발생
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="empId"></param>
        /// <param name="invClass"></param>
        /// <param name="doDate"></param>
        /// <returns></returns>
        public string Cresyn_Get_CBO_AP_GET_INV_NUM_FUNC(int orgId, int empId, string invClass, string doDate)
        {
            string strReturn = "";
            string strSP = "CBO_AP_GET_INV_NUM_FUNC";

            try
            {
                OracleParameter[] parameters = new OracleParameter[] {
                    ParamSet.Add4Sql("P_ORG_ID", OracleType.Number, orgId),
                    ParamSet.Add4Sql("P_USER_ID", OracleType.Number, empId),
                    ParamSet.Add4Sql("P_INV_CLASS", OracleType.NVarChar, invClass),
                    ParamSet.Add4Sql("P_DATE", OracleType.DateTime, doDate),
                    
                    ParamSet.Add4Sql("return_value", OracleType.NVarChar, 50, ParameterDirection.ReturnValue)
                };
                ParamData pData = new ParamData(strSP, "", 30, parameters);

                using (DbBase db = new DbBase())
                {
                    strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
                }
            }
            catch (Exception ex)
            {
                ZumNet.Framework.Exception.ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
            }
            return strReturn;
        }
        #endregion
    }
}
