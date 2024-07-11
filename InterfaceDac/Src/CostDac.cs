using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

using ZumNet.Framework.Base;
using ZumNet.Framework.Configuration;
using ZumNet.Framework.Data;

namespace ZumNet.DAL.InterfaceDac
{
    /// <summary>
    /// 
    /// </summary>
    public class CostDac : DacBase
    {
        #region [생성자]

        private string _formDB = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public CostDac(string connectionString = "") : base(connectionString)
		{
            _formDB = ConfigINI.GetValue(Sections.SECTION_DBNAME, Property.INIKEY_DB_FORM);
        }

		/// <summary>
		/// 
		/// </summary>
		public CostDac(SqlConnection connection) : base(connection)
		{
            _formDB = ConfigINI.GetValue(Sections.SECTION_DBNAME, Property.INIKEY_DB_FORM);
        }
        #endregion

        #region [기본 폴더, 기준정보 조회]
        /// <summary>
        /// 메뉴 권한 가져오기
        /// </summary>
        /// <param name="ctId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetMenuAcl(int ctId, int userId)
        {
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[]
            {
                ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, ctId),
                ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userId)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_MENU_ACL", "", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }

        /// <summary>
        /// 기준일자 가져오기
        /// </summary>
        /// <param name="ceId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetSTDDAY(int ceId, string viewDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_STDDAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
           
            return dsReturn;
        }

        /// <summary>
        /// 설  명 : 환율, 임율 기준목록 가져오기
        /// </summary>
        /// <returns></returns>
        public DataSet GetSTDINFO()
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = null;

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_STDINFO", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 환율정보 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="regId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetXRATE(string mode, int regId, string viewDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_XRATE", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 외주임율 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="regId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetOUTPAY(string mode, int regId, string viewDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_OUTPAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 기준임율 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="regId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetSTDPAY(string mode, int regId, string viewDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_STDPAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 기준임율 계산표 반환
        /// </summary>
        /// <param name="stdId"></param>
        /// <param name="outId"></param>
        /// <param name="item"></param>
        /// <param name="buyer"></param>
        /// <param name="corp"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public DataSet GetSTDPAY_CALC(int stdId, int outId, string item, string buyer, string corp, double rate)
        {
            DataSet dsReturn = null;
            
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@stdid", SqlDbType.Int, 4, stdId),
                ParamSet.Add4Sql("@outid", SqlDbType.Int, 4, outId),
                ParamSet.Add4Sql("@item", SqlDbType.NVarChar, 20, item),
                ParamSet.Add4Sql("@buyer", SqlDbType.NVarChar, 20, buyer),
                ParamSet.Add4Sql("@corp", SqlDbType.NVarChar, 20, corp),
                ParamSet.Add4Sql("@rate", SqlDbType.Decimal, 18, rate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_STDPAY_CALC", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [기준정보 저장]
        /// <summary>
        /// 기준환율 저장
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="stdDt"></param>
        /// <param name="xcls"></param>
        /// <param name="state"></param>
        /// <param name="userId"></param>
        /// <param name="userDn"></param>
        /// <param name="userDeptId"></param>
        /// <param name="userDept"></param>
        /// <param name="usd_krw"></param>
        /// <param name="eur_krw"></param>
        /// <param name="cny_krw"></param>
        /// <param name="jpy_krw"></param>
        /// <param name="idr_krw"></param>
        /// <param name="vnd_krw"></param>
        /// <param name="hkd_krw"></param>
        /// <param name="usd_eur"></param>
        /// <param name="usd_cny"></param>
        /// <param name="usd_jpy"></param>
        /// <param name="usd_idr"></param>
        /// <param name="usd_vnd"></param>
        /// <param name="usd_hkd"></param>
        /// <param name="eur_usd"></param>
        /// <param name="hkd_cny"></param>
        /// <param name="s_usd_idr"></param>
        public int InsertXRATE(int regId, string stdDt, string xcls, string state, string userId, string userDn, string userDeptId, string userDept
                            , string usd_krw, string eur_krw, string cny_krw, string jpy_krw, string idr_krw, string vnd_krw, string hkd_krw
                            , string usd_eur, string usd_cny, string usd_jpy, string usd_idr, string usd_vnd, string usd_hkd, string eur_usd
                            , string hkd_cny, string s_usd_idr)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@stddt", SqlDbType.VarChar, 10, stdDt),
                ParamSet.Add4Sql("@xcls", SqlDbType.NVarChar, 10, xcls),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state),
                ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 20, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.VarChar, 20, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),
                ParamSet.Add4Sql("@usd_krw", SqlDbType.VarChar, 20, usd_krw),
                ParamSet.Add4Sql("@eur_krw", SqlDbType.VarChar, 20, eur_krw),
                ParamSet.Add4Sql("@cny_krw", SqlDbType.VarChar, 20, cny_krw),
                ParamSet.Add4Sql("@jpy_krw", SqlDbType.VarChar, 20, jpy_krw),
                ParamSet.Add4Sql("@idr_krw", SqlDbType.VarChar, 20, idr_krw),
                ParamSet.Add4Sql("@vnd_krw", SqlDbType.VarChar, 20, vnd_krw),
                ParamSet.Add4Sql("@hkd_krw", SqlDbType.VarChar, 20, hkd_krw),
                ParamSet.Add4Sql("@usd_eur", SqlDbType.VarChar, 20, usd_eur),
                ParamSet.Add4Sql("@usd_cny", SqlDbType.VarChar, 20, usd_cny),
                ParamSet.Add4Sql("@usd_jpy", SqlDbType.VarChar, 20, usd_jpy),
                ParamSet.Add4Sql("@usd_idr", SqlDbType.VarChar, 20, usd_idr),
                ParamSet.Add4Sql("@usd_vnd", SqlDbType.VarChar, 20, usd_vnd),
                ParamSet.Add4Sql("@usd_hkd", SqlDbType.VarChar, 20, usd_hkd),
                ParamSet.Add4Sql("@eur_usd", SqlDbType.VarChar, 20, eur_usd),
                ParamSet.Add4Sql("@hkd_cny", SqlDbType.VarChar, 20, hkd_cny),
                ParamSet.Add4Sql("@s_usd_idr", SqlDbType.VarChar, 20, s_usd_idr),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_XRATE", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 외주임율 저장
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="stdDt"></param>
        /// <param name="xcls"></param>
        /// <param name="state"></param>
        /// <param name="userId"></param>
        /// <param name="userDn"></param>
        /// <param name="userDeptId"></param>
        /// <param name="userDept"></param>
        /// <param name="corp_ct"></param>
        /// <param name="corp_ch"></param>
        /// <param name="corp_cd"></param>
        /// <param name="corp_cl"></param>
        /// <param name="corp_vh"></param>
        /// <param name="corp_ic"></param>
        /// <param name="corp_is"></param>
        /// <param name="corp_kh"></param>
        public int InsertOUTPAY(int regId, string stdDt, string xcls, string state, string userId, string userDn, string userDeptId, string userDept
                            , string corp_ct, string corp_ch, string corp_cd, string corp_cl, string corp_vh, string corp_ic, string corp_is, string corp_kh)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@stddt", SqlDbType.VarChar, 10, stdDt),
                ParamSet.Add4Sql("@xcls", SqlDbType.NVarChar, 10, xcls),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state),
                ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 20, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.VarChar, 20, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),
                ParamSet.Add4Sql("@corp_ct", SqlDbType.VarChar, 20, corp_ct),
                ParamSet.Add4Sql("@corp_ch", SqlDbType.VarChar, 20, corp_ch),
                ParamSet.Add4Sql("@corp_cd", SqlDbType.VarChar, 20, corp_cd),
                ParamSet.Add4Sql("@corp_cl", SqlDbType.VarChar, 20, corp_cl),
                ParamSet.Add4Sql("@corp_vh", SqlDbType.VarChar, 20, corp_vh),
                ParamSet.Add4Sql("@corp_ic", SqlDbType.VarChar, 20, corp_ic),
                ParamSet.Add4Sql("@corp_is", SqlDbType.VarChar, 20, corp_is),
                ParamSet.Add4Sql("@corp_kh", SqlDbType.VarChar, 20, corp_kh),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_OUTPAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 기준임율 저장
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="stdDt"></param>
        /// <param name="xcls"></param>
        /// <param name="state"></param>
        /// <param name="userId"></param>
        /// <param name="userDn"></param>
        /// <param name="userDeptId"></param>
        /// <param name="userDept"></param>
        public int InsertSTDPAY(int regId, string stdDt, string xcls, string state, string userId, string userDn, string userDeptId, string userDept)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@stddt", SqlDbType.VarChar, 10, stdDt),
                ParamSet.Add4Sql("@xcls", SqlDbType.NVarChar, 10, xcls),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state),
                ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 20, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.VarChar, 20, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_STDPAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 기준임율 항목 저장
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="corp"></param>
        /// <param name="buyer"></param>
        /// <param name="itemCls"></param>
        /// <param name="rowSeq"></param>
        /// <param name="currecny"></param>
        /// <param name="dir_cost"></param>
        /// <param name="smt_cost"></param>
        /// <param name="fl_cost"></param>
        /// <param name="idlb_cost"></param>
        /// <param name="idfx_cost"></param>
        /// <param name="im_rt"></param>
        /// <param name="cs_rt"></param>
        /// <param name="ind_fx"></param>
        /// <param name="ind_lb"></param>
        /// <param name="dpc_rt"></param>
        /// <param name="st_em"></param>
        /// <param name="corp_sga"></param>
        /// <param name="sl_lgt"></param>
        /// <param name="sgc_rt"></param>
        /// <param name="cn_sga"></param>
        /// <param name="fl_sga"></param>
        /// <param name="costrt"></param>
        public void InsertSTDPAYITEM(int regId, string corp, string buyer, string itemCls, int rowSeq, string currecny
                                , string dir_cost, string smt_cost, string fl_cost, string idlb_cost, string idfx_cost
                                , string im_rt, string cs_rt, string ind_fx, string ind_lb, string dpc_rt, string st_em
                                , string corp_sga, string sl_lgt, string sgc_rt, string cn_sga, string fl_sga, string costrt)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@corp", SqlDbType.VarChar, 10, corp),
                ParamSet.Add4Sql("@buyer", SqlDbType.VarChar, 20, buyer),
                ParamSet.Add4Sql("@itemcls", SqlDbType.VarChar, 20, itemCls),
                ParamSet.Add4Sql("@rowseq", SqlDbType.Int, 4, rowSeq),
                ParamSet.Add4Sql("@currency", SqlDbType.VarChar, 5, currecny),
                ParamSet.Add4Sql("@dir_cost", SqlDbType.VarChar, 20, dir_cost),
                ParamSet.Add4Sql("@smt_cost", SqlDbType.VarChar, 20, smt_cost),
                ParamSet.Add4Sql("@fl_cost", SqlDbType.VarChar, 20, fl_cost),
                ParamSet.Add4Sql("@idlb_cost", SqlDbType.VarChar, 20, idlb_cost),
                ParamSet.Add4Sql("@idfx_cost", SqlDbType.VarChar, 20, idfx_cost),
                ParamSet.Add4Sql("@im_rt", SqlDbType.VarChar, 20, im_rt),
                ParamSet.Add4Sql("@cs_rt", SqlDbType.VarChar, 20, cs_rt),
                //ParamSet.Add4Sql("@fx_em", SqlDbType.VarChar, 20, fx_em),
                ParamSet.Add4Sql("@ind_fx", SqlDbType.VarChar, 20, ind_fx),
                ParamSet.Add4Sql("@ind_lb", SqlDbType.VarChar, 20, ind_lb),
                ParamSet.Add4Sql("@dpc_rt", SqlDbType.VarChar, 20, dpc_rt),
                ParamSet.Add4Sql("@st_em", SqlDbType.VarChar, 20, st_em),
                ParamSet.Add4Sql("@corp_sga", SqlDbType.VarChar, 20, corp_sga),
                ParamSet.Add4Sql("@sl_lgt", SqlDbType.VarChar, 20, sl_lgt),
                ParamSet.Add4Sql("@sgc_rt", SqlDbType.VarChar, 20, sgc_rt),
                ParamSet.Add4Sql("@cn_sga", SqlDbType.VarChar, 20, cn_sga),
                ParamSet.Add4Sql("@fl_sga", SqlDbType.VarChar, 20, fl_sga),
                ParamSet.Add4Sql("@costrt", SqlDbType.VarChar, 20, costrt)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_STDPAY_ITEM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 기준임율 항목 삭제
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="rowSeq"></param>
        public void DeleteSTDPAYITEM(int regId, int rowSeq)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@rowseq", SqlDbType.Int, 4, rowSeq)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_DELETE_CE_STDPAY_ITEM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 기준정보 삭제, 복구, 상태변경
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="page"></param>
        /// <param name="regId"></param>
        /// <param name="state"></param>
        public void SetSTDINFO(string mode, string page, int regId, string state)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@page", SqlDbType.VarChar, 50, page),
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_SET_CE_STDINFO", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [견적표 조회, 집계]
        /// <summary>
        /// 개발원가견적 목록
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <param name="state"></param>
        /// <param name="sortCol"></param>
        /// <param name="sortType"></param>
        /// <param name="searchCol"></param>
        /// <param name="searchTetxt"></param>
        /// <returns></returns>
        public DataSet GetCELIST(string cmd, int targetId, string viewDate, string state, string sortCol, string sortType, string searchCol, string searchTetxt)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, cmd),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state),
                ParamSet.Add4Sql("@sortcol", SqlDbType.NVarChar, 50, sortCol),
                ParamSet.Add4Sql("@sorttype", SqlDbType.NVarChar, 5, sortType),
                ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 50, searchCol),
                ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 100, searchTetxt)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_LIST", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 견적표 기본 조회
        /// </summary>
        /// <param name="ceId"></param>
        /// <param name="reportId"></param>
        /// <param name="pntId"></param>
        /// <returns></returns>
        public DataSet GetCEMAIN(int ceId, string reportId, int pntId)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceId),
                ParamSet.Add4Sql("@reportid", SqlDbType.VarChar, 1000, reportId),
                ParamSet.Add4Sql("@pntceid", SqlDbType.Int, 4, pntId)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_MAIN", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 견적표 포함 및 요청/확인 상태 조회
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="ceId"></param>
        /// <returns></returns>
        public DataSet GetCEMAIN_NESTED(string mode, int ceId)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceId)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_MAIN_NESTED", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 집계 목록
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="viewer"></param>
        /// <param name="viewDate"></param>
        /// <param name="cond1"></param>
        /// <param name="cond2"></param>
        /// <param name="cond3"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sortCol"></param>
        /// <param name="sortType"></param>
        /// <param name="searchCol"></param>
        /// <param name="searchTetxt"></param>
        /// <returns></returns>
        public DataSet GetCEREPORT(string cmd, int viewer, string viewDate, string cond1, string cond2, string cond3
                            , string from, string to, string sortCol, string sortType, string searchCol, string searchTetxt)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, cmd),
                ParamSet.Add4Sql("@viewer", SqlDbType.Int, 4, viewer),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate),
                ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 50, cond1),
                ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 50, cond2),
                ParamSet.Add4Sql("@cond3", SqlDbType.NVarChar, 50, cond3),
                ParamSet.Add4Sql("@from", SqlDbType.VarChar, 10, from),
                ParamSet.Add4Sql("@to", SqlDbType.VarChar, 10, to),
                ParamSet.Add4Sql("@sortcol", SqlDbType.NVarChar, 50, sortCol),
                ParamSet.Add4Sql("@sorttype", SqlDbType.NVarChar, 5, sortType),
                ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 50, searchCol),
                ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 100, searchTetxt)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_REPORT", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }
        #endregion

        #region [견적표 저장]
        /// <summary>
        /// 견적표 저장
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="ceid"></param>
        /// <param name="xcls"></param>
        /// <param name="partcls"></param>
        /// <param name="ver"></param>
        /// <param name="subject"></param>
        /// <param name="dscpt"></param>
        /// <param name="urnm"></param>
        /// <param name="urid"></param>
        /// <param name="urdpt"></param>
        /// <param name="urdptid"></param>
        /// <param name="modelnm"></param>
        /// <param name="model"></param>
        /// <param name="itemcls"></param>
        /// <param name="submodel"></param>
        /// <param name="submodeldesc"></param>
        /// <param name="buyer"></param>
        /// <param name="buyercls"></param>
        /// <param name="corp"></param>
        /// <param name="corpid"></param>
        /// <param name="corpcurrency"></param>
        /// <param name="plancnt"></param>
        /// <param name="docnum"></param>
        /// <param name="xratedt"></param>
        /// <param name="stdpaydt"></param>
        /// <param name="outpaydt"></param>
        /// <param name="currency"></param>
        /// <param name="mtrcost"></param>
        /// <param name="mtrcostin"></param>
        /// <param name="mtrcostout"></param>
        /// <param name="mtrcostrt"></param>
        /// <param name="imcost"></param>
        /// <param name="imcostrt"></param>
        /// <param name="pccost"></param>
        /// <param name="pccostmpnt"></param>
        /// <param name="pccostrt"></param>
        /// <param name="splex"></param>
        /// <param name="splexrt"></param>
        /// <param name="varcost"></param>
        /// <param name="varcostrt"></param>
        /// <param name="fixcost"></param>
        /// <param name="fixcostrt"></param>
        /// <param name="prodcost"></param>
        /// <param name="prodcostrt"></param>
        /// <param name="sgacost"></param>
        /// <param name="sgacostrt"></param>
        /// <param name="totalcost"></param>
        /// <param name="exsaleprice"></param>
        /// <param name="exopprofit"></param>
        /// <param name="exopprofitrt"></param>
        /// <param name="exctbprofit"></param>
        /// <param name="exctbprofitrt"></param>
        /// <param name="ttl"></param>
        public int InsertCEMAIN(string cmd, int ceid, string xcls, string partcls, int ver, string subject, string dscpt, string urnm, string urid, string urdpt, string urdptid
                        , string modelnm, string model, string itemcls, string submodel, string submodeldesc, string buyer, string buyercls, string corp, string corpid
                        , string corpcurrency, string plancnt, string docnum, string xratedt, string stdpaydt, string outpaydt, string currency
                        , string mtrcost, string mtrcostin, string mtrcostout, string mtrcostrt, string imcost, string imcostrt
                        , string pccost, string pccostmpnt, string pccostrt, string splex, string splexrt, string varcost, string varcostrt
                        , string fixcost, string fixcostrt, string prodcost, string prodcostrt, string sgacost, string sgacostrt, string totalcost
                        , string exsaleprice, string exopprofit, string exopprofitrt, string exctbprofit, string exctbprofitrt, string ttl)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 10, cmd),
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@xcls", SqlDbType.NVarChar, 20, xcls),
                ParamSet.Add4Sql("@partcls", SqlDbType.NVarChar, 20, partcls),
                //ParamSet.Add4Sql("@orgceid", SqlDbType.Int, 4, orgceid),
                ParamSet.Add4Sql("@ver", SqlDbType.SmallInt, 2, ver),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 50, subject),
                ParamSet.Add4Sql("@dscpt", SqlDbType.NVarChar, 1000, dscpt),

                ParamSet.Add4Sql("@urnm", SqlDbType.NVarChar, 50,  urnm),
                ParamSet.Add4Sql("@urid", SqlDbType.VarChar, 20, urid),
                ParamSet.Add4Sql("@urdpt", SqlDbType.NVarChar, 50,  urdpt),
                ParamSet.Add4Sql("@urdptid", SqlDbType.VarChar, 20, urdptid),

                ParamSet.Add4Sql("@modelnm", SqlDbType.NVarChar, 50, modelnm),
                ParamSet.Add4Sql("@model", SqlDbType.VarChar, 50, model),
                ParamSet.Add4Sql("@itemcls", SqlDbType.NVarChar, 20, itemcls),
                ParamSet.Add4Sql("@submodel", SqlDbType.VarChar, 50, submodel),
                ParamSet.Add4Sql("@submodeldesc", SqlDbType.NVarChar, 100, submodeldesc),

                ParamSet.Add4Sql("@buyer", SqlDbType.NVarChar, 50, buyer),
                ParamSet.Add4Sql("@buyercls", SqlDbType.NVarChar, 20, buyercls),
                ParamSet.Add4Sql("@corp", SqlDbType.VarChar, 10, corp),
                ParamSet.Add4Sql("@corpid", SqlDbType.VarChar, 10, corpid),
                ParamSet.Add4Sql("@corpcurrency", SqlDbType.VarChar, 10, corpcurrency),
                ParamSet.Add4Sql("@plancnt", SqlDbType.VarChar, 50, plancnt),
                ParamSet.Add4Sql("@docnum", SqlDbType.NVarChar, 50, docnum),
                ParamSet.Add4Sql("@xratedt", SqlDbType.VarChar, 10, xratedt),
                ParamSet.Add4Sql("@stdpaydt", SqlDbType.VarChar, 10, stdpaydt),
                ParamSet.Add4Sql("@outpaydt", SqlDbType.VarChar, 10, outpaydt),

                ParamSet.Add4Sql("@currency", SqlDbType.VarChar, 10, currency),
                ParamSet.Add4Sql("@mtrcost", SqlDbType.VarChar, 20, mtrcost),
                ParamSet.Add4Sql("@mtrcostin", SqlDbType.VarChar, 20, mtrcostin),
                ParamSet.Add4Sql("@mtrcostout", SqlDbType.VarChar, 20, mtrcostout),
                ParamSet.Add4Sql("@mtrcostrt", SqlDbType.VarChar, 20, mtrcostrt),
                ParamSet.Add4Sql("@imcost", SqlDbType.VarChar, 20, imcost),
                ParamSet.Add4Sql("@imcostrt", SqlDbType.VarChar, 20, imcostrt),
                ParamSet.Add4Sql("@pccost", SqlDbType.VarChar, 20, pccost),
                ParamSet.Add4Sql("@pccostmpnt", SqlDbType.VarChar, 20, pccostmpnt),
                ParamSet.Add4Sql("@pccostrt", SqlDbType.VarChar, 20, pccostrt),
                ParamSet.Add4Sql("@splex", SqlDbType.VarChar, 20, splex),
                ParamSet.Add4Sql("@splexrt", SqlDbType.VarChar, 20, splexrt),
                ParamSet.Add4Sql("@varcost", SqlDbType.VarChar, 20, varcost),
                ParamSet.Add4Sql("@varcostrt", SqlDbType.VarChar, 20, varcostrt),
                ParamSet.Add4Sql("@fixcost", SqlDbType.VarChar, 20, fixcost),
                ParamSet.Add4Sql("@fixcostrt", SqlDbType.VarChar, 20, fixcostrt),
                ParamSet.Add4Sql("@prodcost", SqlDbType.VarChar, 20, prodcost),
                ParamSet.Add4Sql("@prodcostrt", SqlDbType.VarChar, 20, prodcostrt),
                ParamSet.Add4Sql("@sgacost", SqlDbType.VarChar, 20, sgacost),
                ParamSet.Add4Sql("@sgacostrt", SqlDbType.VarChar, 20, sgacostrt),
                ParamSet.Add4Sql("@totalcost", SqlDbType.VarChar, 20, totalcost),
                ParamSet.Add4Sql("@exsaleprice", SqlDbType.VarChar, 20, exsaleprice),
                ParamSet.Add4Sql("@exopprofit", SqlDbType.VarChar, 20, exopprofit),
                ParamSet.Add4Sql("@exopprofitrt", SqlDbType.VarChar, 20, exopprofitrt),
                ParamSet.Add4Sql("@exctbprofit", SqlDbType.VarChar, 20, exctbprofit),
                ParamSet.Add4Sql("@exctbprofitrt", SqlDbType.VarChar, 20, exctbprofitrt),
                ParamSet.Add4Sql("@ttl", SqlDbType.VarChar, 20, ttl),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_MAIN", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 개발원가견적표 복사
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="subject"></param>
        /// <param name="dscpt"></param>
        /// <param name="urnm"></param>
        /// <param name="urid"></param>
        /// <param name="urdpt"></param>
        /// <param name="urdptid"></param>
        /// <param name="modelnm"></param>
        /// <param name="model"></param>
        /// <param name="buyer"></param>
        /// <param name="buyercls"></param>
        public int CopyCEMAIN(int ceid, string subject, string dscpt, string urnm, string urid, string urdpt
                        , string urdptid, string modelnm, string model, string buyer, string buyercls)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 50, subject),
                ParamSet.Add4Sql("@dscpt", SqlDbType.NVarChar, 1000, dscpt),

                ParamSet.Add4Sql("@urnm", SqlDbType.NVarChar, 50,  urnm),
                ParamSet.Add4Sql("@urid", SqlDbType.VarChar, 20, urid),
                ParamSet.Add4Sql("@urdpt", SqlDbType.NVarChar, 50,  urdpt),
                ParamSet.Add4Sql("@urdptid", SqlDbType.VarChar, 20, urdptid),

                ParamSet.Add4Sql("@modelnm", SqlDbType.NVarChar, 50, modelnm),
                ParamSet.Add4Sql("@model", SqlDbType.VarChar, 50, model),
                ParamSet.Add4Sql("@buyer", SqlDbType.NVarChar, 50, buyer),
                ParamSet.Add4Sql("@buyercls", SqlDbType.NVarChar, 20, buyercls),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_COPY_CE_MAIN", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 개발원가견적표 품목 저장
        /// </summary>
        /// <param name="iid"></param>
        /// <param name="orgiid"></param>
        /// <param name="ver"></param>
        /// <param name="itemno"></param>
        /// <param name="itemid"></param>
        /// <param name="stddt"></param>
        /// <param name="itemnm"></param>
        /// <param name="cavity"></param>
        /// <param name="stddesc"></param>
        /// <param name="vendor"></param>
        /// <param name="vendorid"></param>
        /// <param name="vendorcode"></param>
        /// <param name="currency"></param>
        /// <param name="price"></param>
        /// <param name="desc"></param>
        public int InsertCEITEMHSTY(int iid, int orgiid, int ver, string itemno, string itemid, string stddt
                                , string itemnm, string cavity, string stddesc, string vendor, string vendorid, string vendorcode
                                , string currency, string price, string desc)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@iid", SqlDbType.Int, 4, iid),
                ParamSet.Add4Sql("@orgiid", SqlDbType.Int, 4, orgiid),
                ParamSet.Add4Sql("@ver", SqlDbType.SmallInt, 2, ver),
                ParamSet.Add4Sql("@itemno", SqlDbType.NVarChar, 50, itemno),
                ParamSet.Add4Sql("@itemid", SqlDbType.NVarChar, 20, itemid),
                ParamSet.Add4Sql("@stddt", SqlDbType.VarChar, 10, stddt),
                ParamSet.Add4Sql("@itemnm", SqlDbType.NVarChar, 100, itemnm),
                ParamSet.Add4Sql("@cavity", SqlDbType.NVarChar, 10, cavity),
                ParamSet.Add4Sql("@stddesc", SqlDbType.NVarChar, 100, stddesc),
                ParamSet.Add4Sql("@vendor", SqlDbType.NVarChar, 100, vendor),
                ParamSet.Add4Sql("@vendorid", SqlDbType.VarChar, 20, vendorid),
                ParamSet.Add4Sql("@vendorcode", SqlDbType.VarChar, 20, vendorcode),
                ParamSet.Add4Sql("@currency", SqlDbType.VarChar, 20, currency),
                ParamSet.Add4Sql("@price", SqlDbType.VarChar, 20, price),
                ParamSet.Add4Sql("@desc", SqlDbType.NVarChar, 200, desc),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_ITEM_HSTY", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 개발원가견적표 품목 저장
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="seq"></param>
        /// <param name="iid"></param>
        /// <param name="cls"></param>
        /// <param name="clscd"></param>
        /// <param name="clspos"></param>
        /// <param name="clsceid"></param>
        /// <param name="itemno"></param>
        /// <param name="itemid"></param>
        /// <param name="stddt"></param>
        /// <param name="itemnm"></param>
        /// <param name="cavity"></param>
        /// <param name="stddesc"></param>
        /// <param name="vendor"></param>
        /// <param name="vendorid"></param>
        /// <param name="vendorcode"></param>
        /// <param name="iocls"></param>
        /// <param name="desc"></param>
        /// <param name="currency"></param>
        /// <param name="price"></param>
        /// <param name="cnt"></param>
        /// <param name="sum"></param>
        /// <param name="usdex"></param>
        /// <param name="usdprice"></param>
        /// <param name="usdsum"></param>
        /// <param name="smtpnt"></param>
        /// <param name="smtttl"></param>
        /// <param name="ckflag"></param>
        public void InsertCEITEM(int ceid, int seq, int iid, string cls, string clscd, string clspos, int clsceid, string itemno, string itemid, string stddt
                                , string itemnm, string cavity, string stddesc, string vendor, string vendorid, string vendorcode, string iocls, string desc
                                , string currency, string price, string cnt, string sum, string usdex, string usdprice, string usdsum, string smtpnt, string smtttl, string ckflag)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
                ParamSet.Add4Sql("@iid", SqlDbType.Int, 4, iid),
                ParamSet.Add4Sql("@cls", SqlDbType.NVarChar, 20, cls),
                ParamSet.Add4Sql("@clscd", SqlDbType.NVarChar, 20, clscd),
                ParamSet.Add4Sql("@clspos", SqlDbType.NVarChar, 20, clspos),
                ParamSet.Add4Sql("@clsceid", SqlDbType.Int, 4, clsceid),
                ParamSet.Add4Sql("@itemno", SqlDbType.NVarChar, 50, itemno),
                ParamSet.Add4Sql("@itemid", SqlDbType.NVarChar, 20, itemid),
                ParamSet.Add4Sql("@stddt", SqlDbType.VarChar, 10, stddt),
                ParamSet.Add4Sql("@itemnm", SqlDbType.NVarChar, 100, itemnm),
                ParamSet.Add4Sql("@cavity", SqlDbType.NVarChar, 10, cavity),
                ParamSet.Add4Sql("@stddesc", SqlDbType.NVarChar, 100, stddesc),
                ParamSet.Add4Sql("@vendor", SqlDbType.NVarChar, 100, vendor),
                ParamSet.Add4Sql("@vendorid", SqlDbType.VarChar, 20, vendorid),
                ParamSet.Add4Sql("@vendorcode", SqlDbType.VarChar, 20, vendorcode),
                ParamSet.Add4Sql("@iocls", SqlDbType.NVarChar, 20, iocls),
                ParamSet.Add4Sql("@desc", SqlDbType.NVarChar, 200, desc),
                ParamSet.Add4Sql("@currency", SqlDbType.VarChar, 10, currency),
                ParamSet.Add4Sql("@price", SqlDbType.VarChar, 20, price),
                ParamSet.Add4Sql("@cnt", SqlDbType.VarChar, 20, cnt),
                ParamSet.Add4Sql("@sum", SqlDbType.VarChar, 20, sum),
                ParamSet.Add4Sql("@usdex", SqlDbType.VarChar, 20, usdex),
                ParamSet.Add4Sql("@usdprice", SqlDbType.VarChar, 20, usdprice),
                ParamSet.Add4Sql("@usdsum", SqlDbType.VarChar, 20, usdsum),
                ParamSet.Add4Sql("@smtpnt", SqlDbType.VarChar, 20, smtpnt),
                ParamSet.Add4Sql("@smtttl", SqlDbType.VarChar, 20, smtttl),
                ParamSet.Add4Sql("@ckflag", SqlDbType.Char, 1, ckflag)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_ITEM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개발원가견적표 재료비 저장
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="seq"></param>
        /// <param name="clsdesc"></param>
        /// <param name="incost"></param>
        /// <param name="outcost"></param>
        /// <param name="sum"></param>
        /// <param name="rate"></param>
        public void InsertCOSTMTR(int ceid, int seq, string clsdesc, string incost, string outcost, string sum, string rate)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq),
                ParamSet.Add4Sql("@clsdesc", SqlDbType.NVarChar, 50, clsdesc),
                ParamSet.Add4Sql("@incost", SqlDbType.VarChar, 20, incost),
                ParamSet.Add4Sql("@outcost", SqlDbType.VarChar, 20, outcost),
                ParamSet.Add4Sql("@sum", SqlDbType.VarChar, 20, sum),
                ParamSet.Add4Sql("@rate", SqlDbType.VarChar, 20, rate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_COST_MTR", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 수입경비
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="seq"></param>
        /// <param name="clsdesc"></param>
        /// <param name="stddesc"></param>
        /// <param name="imrate"></param>
        /// <param name="sum"></param>
        /// <param name="rate"></param>
        public void InsertCOSTIM(int ceid, int seq, string clsdesc, string stddesc, string imrate, string sum, string rate)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq),
                ParamSet.Add4Sql("@clsdesc", SqlDbType.NVarChar, 50, clsdesc),
                ParamSet.Add4Sql("@stddesc", SqlDbType.NVarChar, 50, stddesc),
                ParamSet.Add4Sql("@imrate", SqlDbType.VarChar, 20, imrate),
                ParamSet.Add4Sql("@sum", SqlDbType.VarChar, 20, sum),
                ParamSet.Add4Sql("@rate", SqlDbType.VarChar, 20, rate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_COST_IM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 가공비
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="seq"></param>
        /// <param name="clsdesc"></param>
        /// <param name="mpnt"></param>
        /// <param name="mpay"></param>
        /// <param name="sum"></param>
        /// <param name="rate"></param>
        public void InsertCOSTPC(int ceid, int seq, string clsdesc, string mpnt, string mpay, string sum, string rate)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq),
                ParamSet.Add4Sql("@clsdesc", SqlDbType.NVarChar, 50, clsdesc),
                ParamSet.Add4Sql("@mpnt", SqlDbType.VarChar, 20, mpnt),
                ParamSet.Add4Sql("@mpay", SqlDbType.VarChar, 20, mpay),
                ParamSet.Add4Sql("@sum", SqlDbType.VarChar, 20, sum),
                ParamSet.Add4Sql("@rate", SqlDbType.VarChar, 20, rate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_COST_PC", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 판관비
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="seq"></param>
        /// <param name="clsdesc"></param>
        /// <param name="stddesc"></param>
        /// <param name="sum"></param>
        /// <param name="rate"></param>
        public void InsertCOSTSGA(int ceid, int seq, string clsdesc, string stddesc, string sum, string rate)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq),
                ParamSet.Add4Sql("@clsdesc", SqlDbType.NVarChar, 50, clsdesc),
                ParamSet.Add4Sql("@stddesc", SqlDbType.NVarChar, 50, stddesc),
                ParamSet.Add4Sql("@sum", SqlDbType.VarChar, 20, sum),
                ParamSet.Add4Sql("@rate", SqlDbType.VarChar, 20, rate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_CE_COST_SGA", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개발원가견적 항목 삭제
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="iid"></param>
        public void DeleteCEITEM(int ceid, int iid)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@iid", SqlDbType.Int, 4, iid)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_DELETE_CE_ITEM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개발원가견적 하위테이블 항목 삭제
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="ceid"></param>
        /// <param name="seq"></param>
        public void DeleteCOSTTABLE(string tbl, int ceid, int seq)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@tbl", SqlDbType.NVarChar, 50, tbl),
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceid),
                ParamSet.Add4Sql("@seq", SqlDbType.SmallInt, 2, seq)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_DELETE_CE_COST_TABLE", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개발원가견적표 부문 포함 설정
        /// </summary>
        /// <param name="ceId"></param>
        /// <param name="pntCeid"></param>
        /// <param name="pntState"></param>
        public void SetMAINNESTED(int ceId, int pntCeid, string pntState)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceId),
                ParamSet.Add4Sql("@pntceid", SqlDbType.Int, 4, pntCeid),
                ParamSet.Add4Sql("@pntstate", SqlDbType.Char, 1, pntState)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_SET_CE_MAIN_NESTED", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 개발원가견적표 반려요청, 재검토요청, 재검토완료 상태 설정
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="ceId"></param>
        /// <param name="pntCeid"></param>
        /// <param name="state"></param>
        /// <param name="urId"></param>
        /// <param name="urNm"></param>
        /// <param name="urDptId"></param>
        /// <param name="urDpt"></param>
        /// <param name="reqCmnt"></param>
        public void UpdateMAINNESTED(string cmd, int ceId, int pntCeid, string state
                            , string urId, string urNm, string urDptId, string urDpt, string reqCmnt)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, cmd),
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceId),
                ParamSet.Add4Sql("@pntceid", SqlDbType.Int, 4, pntCeid),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state),
                ParamSet.Add4Sql("@urid", SqlDbType.VarChar, 20, urId),
                ParamSet.Add4Sql("@urnm", SqlDbType.NVarChar, 50, urNm),
                ParamSet.Add4Sql("@urdptid", SqlDbType.VarChar, 20, urDptId),
                ParamSet.Add4Sql("@urdpt", SqlDbType.NVarChar, 50, urDpt),
                ParamSet.Add4Sql("@reqcmnt", SqlDbType.NVarChar, 500, reqCmnt)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_UPDATE_CE_MAIN_NESTED", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }
        #endregion

        #region [기타]
        /// <summary>
        /// 모델번호 중복 체크
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ceId"></param>
        /// <returns></returns>
        public string GetCEMAIN_MODEL(string model, int ceId)
        {
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@model", SqlDbType.VarChar, 50, model),
                ParamSet.Add4Sql("@ceid", SqlDbType.Int, 4, ceId)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_CE_MAIN_MODEL", "", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return strReturn;
        }
        #endregion

        #region [모델별원가]
        /// <summary>
        /// 모델별원가 집계목록
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="sortCol"></param>
        /// <param name="sortType"></param>
        /// <param name="searchCol"></param>
        /// <param name="searchTetxt"></param>
        /// <param name="cond1"></param>
        /// <param name="cond2"></param>
        /// <param name="cond3"></param>
        /// <returns></returns>
        public DataSet GetMCSUMMARY(string cmd, int targetId, string viewDate, int page, int count
                                , string sortCol, string sortType, string searchCol, string searchTetxt
                                , string cond1, string cond2, string cond3)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, cmd),
                ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate),
                ParamSet.Add4Sql("@page", SqlDbType.Int, 4, page),
                ParamSet.Add4Sql("@count", SqlDbType.SmallInt, 2, count),
                ParamSet.Add4Sql("@sortcol", SqlDbType.NVarChar, 50, sortCol),
                ParamSet.Add4Sql("@sorttype", SqlDbType.NVarChar, 5, sortType),
                ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 50, searchCol),
                ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 100, searchTetxt),
                ParamSet.Add4Sql("@cond1", SqlDbType.NVarChar, 20, cond1),
                ParamSet.Add4Sql("@cond2", SqlDbType.NVarChar, 20, cond2),
                ParamSet.Add4Sql("@cond3", SqlDbType.NVarChar, 20, cond3),

                ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_MC_SUMMARY", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 특정 일자 모델별 공수 가져오기
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="targetId"></param>
        /// <param name="model"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetMCSTDTIME(string cmd, int targetId, string model, string viewDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, cmd),
                ParamSet.Add4Sql("@orgid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@model", SqlDbType.VarChar, 50, model),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_MC_STDTIME", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }


        /// <summary>
        /// 원가항목 조회
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="targetId"></param>
        /// <param name="viewDate"></param>
        /// <param name="field"></param>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public DataSet GetMCSUMMARYITEM(string cmd, int targetId, string viewDate, string field, string model, string item)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, cmd),
                ParamSet.Add4Sql("@orgid", SqlDbType.Int, 4, targetId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate),
                ParamSet.Add4Sql("@field", SqlDbType.VarChar, 50, field),
                ParamSet.Add4Sql("@model", SqlDbType.VarChar, 50, model),
                ParamSet.Add4Sql("@item", SqlDbType.VarChar, 50, item)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_MC_SUMMARY_ITEM", "", 60, parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 실적율 가져오기
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="regId"></param>
        /// <param name="viewDate"></param>
        /// <returns></returns>
        public DataSet GetMCSTDPAY(string mode, int regId, string viewDate)
        {
            DataSet dsReturn = null;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@viewdate", SqlDbType.VarChar, 10, viewDate)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_GET_MC_STDPAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }

            return dsReturn;
        }

        /// <summary>
        /// 실적율 저장
        /// </summary>
        public int InsertMCSTDPAY(int regId, string stdDt, string xcls, string state, string userId, string userDn, string userDeptId, string userDept)
        {
            int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@stddt", SqlDbType.VarChar, 10, stdDt),
                ParamSet.Add4Sql("@xcls", SqlDbType.NVarChar, 10, xcls),
                ParamSet.Add4Sql("@state", SqlDbType.Char, 1, state),
                ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 20, userId),
                ParamSet.Add4Sql("@userdn", SqlDbType.NVarChar, 50, userDn),
                ParamSet.Add4Sql("@userdeptid", SqlDbType.VarChar, 20, userDeptId),
                ParamSet.Add4Sql("@userdept", SqlDbType.NVarChar, 50, userDept),

                ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_MC_STDPAY", "", parameters);

            using (DbBase db = new DbBase())
            {
                iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
            }

            return iReturn;
        }

        /// <summary>
        /// 실적율 항목 저장
        /// </summary>
        public void InsertMCSTDPAYITEM(int regId, string corp, string buyer, string itemCls, int rowSeq, string currecny
                                , string flt_rt, string et_mtr, string spl_ex, string ind_fx, string ind_lb, string dpc_rt
                                , string corp_sga, string sl_lgt, string sgc_rt, string cn_sga, string fl_sga)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@corp", SqlDbType.VarChar, 10, corp),
                ParamSet.Add4Sql("@buyer", SqlDbType.VarChar, 20, buyer),
                ParamSet.Add4Sql("@itemcls", SqlDbType.VarChar, 20, itemCls),
                ParamSet.Add4Sql("@rowseq", SqlDbType.Int, 4, rowSeq),
                ParamSet.Add4Sql("@currency", SqlDbType.VarChar, 5, currecny),
                ParamSet.Add4Sql("@flt_rt", SqlDbType.VarChar, 20, flt_rt),
                ParamSet.Add4Sql("@et_mtr", SqlDbType.VarChar, 20, et_mtr),
                ParamSet.Add4Sql("@spl_ex", SqlDbType.VarChar, 20, spl_ex),
                //ParamSet.Add4Sql("@fx_cost", SqlDbType.VarChar, 20, fx_cost),
                ParamSet.Add4Sql("@ind_fx", SqlDbType.VarChar, 20, ind_fx),
                ParamSet.Add4Sql("@ind_lb", SqlDbType.VarChar, 20, ind_lb),
                ParamSet.Add4Sql("@dpc_rt", SqlDbType.VarChar, 20, dpc_rt),
                ParamSet.Add4Sql("@corp_sga", SqlDbType.VarChar, 20, corp_sga),
                ParamSet.Add4Sql("@sl_lgt", SqlDbType.VarChar, 20, sl_lgt),
                ParamSet.Add4Sql("@sgc_rt", SqlDbType.VarChar, 20, sgc_rt),
                ParamSet.Add4Sql("@cn_sga", SqlDbType.VarChar, 20, cn_sga),
                ParamSet.Add4Sql("@fl_sga", SqlDbType.VarChar, 20, fl_sga)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_MC_STDPAY_ITEM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 실적율 항목 삭제
        /// </summary>
        public void DeleteMCSTDPAYITEM(int regId, int rowSeq)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@regid", SqlDbType.Int, 4, regId),
                ParamSet.Add4Sql("@rowseq", SqlDbType.Int, 4, rowSeq)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_DELETE_MC_STDPAY_ITEM", "", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 모델별원가 개발원가견적/모델별원가실적 엑셀 저장
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="orgsubid"></param>
        /// <param name="orgcd"></param>
        /// <param name="periodNm"></param>
        /// <param name="model"></param>
        /// <param name="modelid"></param>
        /// <param name="xcls"></param>
        /// <param name="itemcls"></param>
        /// <param name="buyer"></param>
        /// <param name="xratedt"></param>
        /// <param name="stdpaydt"></param>
        /// <param name="outpaydt"></param>
        /// <param name="mtrcost"></param>
        /// <param name="mtrcostrt"></param>
        /// <param name="pccost"></param>
        /// <param name="pccostrt"></param>
        /// <param name="splex"></param>
        /// <param name="splexrt"></param>
        /// <param name="varcost"></param>
        /// <param name="varcostrt"></param>
        /// <param name="fixcost"></param>
        /// <param name="fixcostrt"></param>
        /// <param name="fixcostb"></param>
        /// <param name="fixcostbrt"></param>
        /// <param name="prodcost"></param>
        /// <param name="prodcostrt"></param>
        /// <param name="sgacost"></param>
        /// <param name="sgacostrt"></param>
        /// <param name="totalcost"></param>
        /// <param name="totalcostrt"></param>
        /// <param name="saleprice"></param>
        /// <param name="opprofit"></param>
        /// <param name="opprofitrt"></param>
        /// <param name="ctbprofit"></param>
        /// <param name="ctbprofitrt"></param>
        /// <param name="stdtime"></param>
        /// <param name="psales"></param>
        /// <returns></returns>
        public string InsertMCSUMMARY_EXCEL(int orgid, int orgsubid, string orgcd, string periodNm, string model, int modelid
                                , string xcls, string itemcls, string buyer, string xratedt, string stdpaydt, string outpaydt
                                , string mtrcost, string mtrcostrt, string pccost, string pccostrt, string splex, string splexrt
                                , string varcost, string varcostrt, string fixcost, string fixcostrt, string fixcostb, string fixcostbrt
                                , string prodcost, string prodcostrt, string sgacost, string sgacostrt, string totalcost, string totalcostrt
                                , string saleprice, string opprofit, string opprofitrt, string ctbprofit, string ctbprofitrt, string stdtime, string psales)
        {
            string strReturn = "";

            SqlParameter[] parameters = new SqlParameter[] {
                ParamSet.Add4Sql("@orgid", SqlDbType.Int, 4, orgid),
                ParamSet.Add4Sql("@orgsubid", SqlDbType.Int, 4, orgsubid),
                ParamSet.Add4Sql("@orgcd", SqlDbType.VarChar, 20, orgcd),
                ParamSet.Add4Sql("@periodnm", SqlDbType.VarChar, 10, periodNm),
                ParamSet.Add4Sql("@model", SqlDbType.VarChar, 50, model),
                ParamSet.Add4Sql("@modelid", SqlDbType.Int, 4, modelid),

                ParamSet.Add4Sql("@xcls", SqlDbType.Char, 1, xcls),
                ParamSet.Add4Sql("@itemcls", SqlDbType.NVarChar, 20, itemcls),
                ParamSet.Add4Sql("@buyer", SqlDbType.NVarChar, 50, buyer),
                ParamSet.Add4Sql("@xratedt", SqlDbType.VarChar, 10, xratedt),
                ParamSet.Add4Sql("@stdpaydt", SqlDbType.VarChar, 10, stdpaydt),
                ParamSet.Add4Sql("@outpaydt", SqlDbType.VarChar, 10, outpaydt),

                ParamSet.Add4Sql("@mtrcost", SqlDbType.VarChar, 20, mtrcost),
                ParamSet.Add4Sql("@mtrcostrt", SqlDbType.VarChar, 20, mtrcostrt),
                ParamSet.Add4Sql("@pccost", SqlDbType.VarChar, 20, pccost),
                ParamSet.Add4Sql("@pccostrt", SqlDbType.VarChar, 20, pccostrt),
                ParamSet.Add4Sql("@splex", SqlDbType.VarChar, 20, splex),
                ParamSet.Add4Sql("@splexrt", SqlDbType.VarChar, 20, splexrt),
                ParamSet.Add4Sql("@varcost", SqlDbType.VarChar, 20, varcost),
                ParamSet.Add4Sql("@varcostrt", SqlDbType.VarChar, 20, varcostrt),
                ParamSet.Add4Sql("@fixcost", SqlDbType.VarChar, 20, fixcost),
                ParamSet.Add4Sql("@fixcostrt", SqlDbType.VarChar, 20, fixcostrt),
                ParamSet.Add4Sql("@fixcostb", SqlDbType.VarChar, 20, fixcostb),
                ParamSet.Add4Sql("@fixcostbrt", SqlDbType.VarChar, 20, fixcostbrt),
                ParamSet.Add4Sql("@prodcost", SqlDbType.VarChar, 20, prodcost),
                ParamSet.Add4Sql("@prodcostrt", SqlDbType.VarChar, 20, prodcostrt),
                ParamSet.Add4Sql("@sgacost", SqlDbType.VarChar, 20, sgacost),
                ParamSet.Add4Sql("@sgacostrt", SqlDbType.VarChar, 20, sgacostrt),
                ParamSet.Add4Sql("@totalcost", SqlDbType.VarChar, 20, totalcost),
                ParamSet.Add4Sql("@totalcostrt", SqlDbType.VarChar, 20, totalcostrt),
                ParamSet.Add4Sql("@saleprice", SqlDbType.VarChar, 20, saleprice),
                ParamSet.Add4Sql("@opprofit", SqlDbType.VarChar, 20, opprofit),
                ParamSet.Add4Sql("@opprofitrt", SqlDbType.VarChar, 20, opprofitrt),
                ParamSet.Add4Sql("@ctbprofit", SqlDbType.VarChar, 20, ctbprofit),
                ParamSet.Add4Sql("@ctbprofitrt", SqlDbType.VarChar, 20, ctbprofitrt),
                ParamSet.Add4Sql("@stdtime", SqlDbType.VarChar, 20, stdtime),
                ParamSet.Add4Sql("@psales", SqlDbType.VarChar, 20, psales),

                ParamSet.Add4Sql("@out", SqlDbType.Char, 2, ParameterDirection.Output)
            };

            ParamData pData = new ParamData(this._formDB + ".dbo.up_INSERT_MC_SUMMARY_EXCEL", "", parameters);

            using (DbBase db = new DbBase())
            {
                strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
            return strReturn;
        }
        #endregion
    }
}
