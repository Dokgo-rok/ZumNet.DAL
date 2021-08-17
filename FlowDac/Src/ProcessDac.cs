using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZumNet.Framework.Base;
using ZumNet.Framework.Data;

namespace ZumNet.DAL.FlowDac
{
    public class ProcessDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public ProcessDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public ProcessDac(SqlConnection connection) : base(connection)
		{

		}
	}
}
