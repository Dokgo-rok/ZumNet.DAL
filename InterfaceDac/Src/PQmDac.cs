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
	public class PQmDac : DacBase
    {
		#region [생성자]
		/// <summary>
		/// 
		/// </summary>
		public PQmDac(string connectionString = "") : base(connectionString)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public PQmDac(SqlConnection connection) : base(connection)
		{
		}
        #endregion

        /// <summary>
        /// ERP KPI 지표현황 입고기준
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="current"></param>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        public DataSet Get_INV_NEW_KPI(string mode, string current, string year, int week)
		{
			string sDBName = ConfigINI.GetValue(Sections.SECTION_DBNAME, Property.INIKEY_DB_FORM);
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@current", SqlDbType.Char, 10, current),
				ParamSet.Add4Sql("@year", SqlDbType.Char, 4, year),
				ParamSet.Add4Sql("@week", SqlDbType.Int, 4, week)
			};

			ParamData pData = new ParamData(sDBName + ".dbo.up_GET_INV_NEW_KPI", "", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// ERP KPI 지표현황 재고기준
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="current"></param>
		/// <param name="year"></param>
		/// <param name="week"></param>
		/// <returns></returns>
		public DataSet Get_INV_NEW_KPI_S(string mode, string current, string year, int week)
		{
			string sDBName = ConfigINI.GetValue(Sections.SECTION_DBNAME, Property.INIKEY_DB_FORM);
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@current", SqlDbType.Char, 10, current),
				ParamSet.Add4Sql("@year", SqlDbType.Char, 4, year),
				ParamSet.Add4Sql("@week", SqlDbType.Int, 4, week)
			};

			ParamData pData = new ParamData(sDBName + ".dbo.up_GET_INV_NEW_KPI_S", "", 30, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 재고지표현황 법인 주차 종합 설정
		/// </summary>
		/// <param name="corp"></param>
		/// <param name="year"></param>
		/// <param name="week"></param>
		/// <param name="kpiA"></param>
		/// <param name="kpiB"></param>
		/// <param name="kpiC"></param>
		/// <param name="kpiD"></param>
		/// <param name="kpiE"></param>
		/// <param name="kpiF"></param>
		/// <param name="kpiG"></param>
		/// <returns></returns>
		public string Set_INV_NEW_KPI_HDR(string corp, int year, int week
				, string kpiA, string kpiB, string kpiC, string kpiD, string kpiE, string kpiF, string kpiG)
		{
			string sDBName = ConfigINI.GetValue(Sections.SECTION_DBNAME, Property.INIKEY_DB_FORM);
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@corp", SqlDbType.VarChar, 10, corp),
				ParamSet.Add4Sql("@iyear", SqlDbType.Int, 4, year),
				ParamSet.Add4Sql("@iweek", SqlDbType.Int, 4, week),
				ParamSet.Add4Sql("@kpia", SqlDbType.VarChar, 20, kpiA),
				ParamSet.Add4Sql("@kpib", SqlDbType.VarChar, 20, kpiB),
				ParamSet.Add4Sql("@kpic", SqlDbType.VarChar, 20, kpiC),
				ParamSet.Add4Sql("@kpid", SqlDbType.VarChar, 20, kpiD),
				ParamSet.Add4Sql("@kpie", SqlDbType.VarChar, 20, kpiE),
				ParamSet.Add4Sql("@kpif", SqlDbType.VarChar, 20, kpiF),
				ParamSet.Add4Sql("@kpig", SqlDbType.VarChar, 20, kpiG)
			};

			ParamData pData = new ParamData(sDBName + ".dbo.up_SET_INV_NEW_KPI_HDR", "NEW_KPI_HDR", 30, parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteScalarTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 재고지표현황 품목별 지표 설정
		/// </summary>
		/// <param name="corp"></param>
		/// <param name="year"></param>
		/// <param name="week"></param>
		/// <param name="invCat"></param>
		/// <param name="desc"></param>
		/// <param name="kpiA"></param>
		/// <param name="kpiB"></param>
		/// <param name="kpiC"></param>
		/// <param name="kpiD"></param>
		/// <param name="kpiE"></param>
		/// <param name="kpiF"></param>
		/// <param name="kpiG"></param>
		/// <returns></returns>
		public string Set_INV_NEW_KPI_DTL(string corp, int year, int week, string invCat, string desc
				, string kpiA, string kpiB, string kpiC, string kpiD, string kpiE, string kpiF, string kpiG)
		{
			string sDBName = ConfigINI.GetValue(Sections.SECTION_DBNAME, Property.INIKEY_DB_FORM);
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@corp", SqlDbType.VarChar, 10, corp),
				ParamSet.Add4Sql("@iyear", SqlDbType.Int, 4, year),
				ParamSet.Add4Sql("@iweek", SqlDbType.Int, 4, week),
				ParamSet.Add4Sql("@invcat", SqlDbType.VarChar, 30, invCat),
				ParamSet.Add4Sql("@desc", SqlDbType.NVarChar, 50, desc),
				ParamSet.Add4Sql("@kpia", SqlDbType.VarChar, 20, kpiA),
				ParamSet.Add4Sql("@kpib", SqlDbType.VarChar, 20, kpiB),
				ParamSet.Add4Sql("@kpic", SqlDbType.VarChar, 20, kpiC),
				ParamSet.Add4Sql("@kpid", SqlDbType.VarChar, 20, kpiD),
				ParamSet.Add4Sql("@kpie", SqlDbType.VarChar, 20, kpiE),
				ParamSet.Add4Sql("@kpif", SqlDbType.VarChar, 20, kpiF),
				ParamSet.Add4Sql("@kpig", SqlDbType.VarChar, 20, kpiG)
			};

			ParamData pData = new ParamData(sDBName + ".dbo.up_SET_INV_NEW_KPI_DTL", "NEW_KPI_DTL", 30, parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteScalarTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 생관계획 저장
		/// </summary>
		/// <param name="corp"></param>
		/// <param name="stdDate"></param>
		/// <param name="plan"></param>
		public void SetRegisterPQMPRODCONTPLAN(string corp, string stdDate, string plan)
		{
			string strSP = "product.dbo.product_SetRegisterPRODCONTPLAN";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@corp", SqlDbType.VarChar, 20, corp),
				ParamSet.Add4Sql("@stddate", SqlDbType.NVarChar, 10, stdDate),
				ParamSet.Add4Sql("@plan", SqlDbType.NVarChar, 20, plan)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 품질목표 저장
		/// </summary>
		/// <param name="stdDate"></param>
		/// <param name="partNO"></param>
		/// <param name="corp"></param>
		/// <param name="goalCnt"></param>
		public void SetRegisterPQMQUALITYGOAL(string stdDate, string partNO, string corp, string goalCnt)
		{
			string strSP = "product.dbo.product_SetRegisterQUALITYGOAL";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@stddate", SqlDbType.NVarChar, 20, stdDate),
				ParamSet.Add4Sql("@partno", SqlDbType.NVarChar, 20, partNO),
				ParamSet.Add4Sql("@corp", SqlDbType.NVarChar, 20, corp),
				ParamSet.Add4Sql("@goalcnt", SqlDbType.NVarChar, 20, goalCnt)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 표준공수 저장
		/// </summary>
		/// <param name="corp"></param>
		/// <param name="modelNM"></param>
		/// <param name="stdDate"></param>
		/// <param name="manHour"></param>
		/// <param name="remarks"></param>
		public void SetRegisterPQMSTANDWORK(string corp, string modelNM, string stdDate, string manHour, string remarks)
		{
			string strSP = "product.dbo.product_SetRegisterSTANDWORK";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@corp", SqlDbType.NVarChar, 10, corp),
				ParamSet.Add4Sql("@modelnm", SqlDbType.NVarChar, 50, modelNM),
				ParamSet.Add4Sql("@stddate", SqlDbType.NVarChar, 10, stdDate),
				ParamSet.Add4Sql("@manhour", SqlDbType.NVarChar, 20, manHour),
				ParamSet.Add4Sql("@remarks", SqlDbType.NVarChar, 400, remarks)
			};

			ParamData pData = new ParamData(strSP, "", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}
	}
}
