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
    public class ToDoDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public ToDoDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public ToDoDac(SqlConnection connection) : base(connection)
		{

		}
	}
}
