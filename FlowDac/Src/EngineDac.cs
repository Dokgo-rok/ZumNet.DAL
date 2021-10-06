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

namespace ZumNet.DAL.FlowDac
{
	/// <summary>
	/// 
	/// </summary>
	public class EngineDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public EngineDac(string connectionString = "") : base(connectionString)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public EngineDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 특정 프로세스에 해당하는 참여자들 정보를 가져온다.
		/// </summary>
		/// <param name="oID"></param>
		/// <returns></returns>
		public DataSet RetrieveParticipantList(int oID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oID", SqlDbType.Int, 4, oID)
			};

			ParamData pData = new ParamData("admin.ph_up_BFSelectWorkItemOID", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
