using Oracle.ManagedDataAccess.Client;

using System.Collections;
using System;
using System.Data;
using System.Reflection;

using ZumNet.Framework.Core;
using ZumNet.Framework.Oracle.Base;
using ZumNet.Framework.Oracle.Data;

namespace ZumNet.DAL.ExternalDac
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
    }
}
