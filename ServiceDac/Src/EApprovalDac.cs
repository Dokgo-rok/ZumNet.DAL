using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ZumNet.Framework.Base;
using ZumNet.Framework.Data;

namespace ZumNet.DAL.ServiceDac
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

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
		#endregion
	}
}
