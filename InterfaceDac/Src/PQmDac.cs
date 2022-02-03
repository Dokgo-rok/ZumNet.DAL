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
	}
}
