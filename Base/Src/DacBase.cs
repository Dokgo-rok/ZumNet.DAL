using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZumNet.Framework.Core;

namespace ZumNet.DAL.Base
{
	/// <summary>
	/// 
	/// </summary>
	public class DacBase
	{
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
		public DacBase() : this("")
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public DacBase(string connectionString)
		{
			this._connectString = connectionString;

			if (String.IsNullOrWhiteSpace(connectionString))
			{
				this._connectString = DbConnect.GetString(DbConnect.INITIAL_CATALOG.INIT_CAT_BASE);
			}
		}
	}
}
