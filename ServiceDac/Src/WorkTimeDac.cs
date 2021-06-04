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
	public class WorkTimeDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public WorkTimeDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public WorkTimeDac(SqlConnection connection) : base(connection)
		{

		}


	}
}
