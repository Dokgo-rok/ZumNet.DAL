using Oracle.ManagedDataAccess.Client;

using System.Collections;
using System;
using System.Data;
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
    }
}
