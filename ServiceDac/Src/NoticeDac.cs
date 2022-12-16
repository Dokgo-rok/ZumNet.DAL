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
	/// 알림 및 쪽지 관리
	/// </summary>
	public class NoticeDac : DacBase
    {
		/// <summary>
		/// 
		/// </summary>
		public NoticeDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public NoticeDac(SqlConnection connection) : base(connection)
		{

		}

		#region [알림 : 트랜잭션 참여 안함]
		/// <summary>
		/// 알림 생성
		/// </summary>
		/// <param name="tgtId"></param>
		/// <param name="noticeClass"></param>
		/// <param name="contents"></param>
		/// <param name="linkInfo"></param>
		public void CreateNotice(int tgtId, string noticeClass, string contents, string linkInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@tgtid", SqlDbType.Int, 4, tgtId),
				ParamSet.Add4Sql("@noticls", SqlDbType.NVarChar, 50, noticeClass),
				ParamSet.Add4Sql("@contents", SqlDbType.NVarChar, 1000, contents),
				ParamSet.Add4Sql("@linkinfo", SqlDbType.NVarChar, 1000, linkInfo)
			};

			ParamData pData = new ParamData("dbo.zp_BT_insertNOTICE", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 알림 삭제설정, 읽음설정
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="regId"></param>
		/// <param name="tgtId"></param>
		public void UpdateNotice(string mode, long regId, int tgtId)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 8, regId),
				ParamSet.Add4Sql("@tgtid", SqlDbType.Int, 4, tgtId)
			};

			ParamData pData = new ParamData("dbo.zp_BT_updateNOTICE", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 알림 완전 삭제
		/// </summary>
		/// <param name="regId"></param>
		/// <param name="tgtId"></param>
		public void DeleteNotice(long regId, int tgtId)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@regid", SqlDbType.BigInt, 8, regId),
				ParamSet.Add4Sql("@tgtid", SqlDbType.Int, 4, tgtId)
			};

			ParamData pData = new ParamData("dbo.zp_BT_deleteNOTICE", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 알림 조회
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="tgtId"></param>
		/// <returns></returns>
		public DataSet SelectNotice(string mode, int tgtId)
		{
			DataSet ds = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@tgtid", SqlDbType.Int, 4, tgtId)
			};

			ParamData pData = new ParamData("dbo.zp_BT_selectNOTICE", parameters);

			using (DbBase db = new DbBase())
			{
				ds = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return ds;
		}

		/// <summary>
		/// 알림 갯수 조회
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="tgtId"></param>
		/// <returns></returns>
		public int SelectNoticeCount(string mode, int tgtId)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@tgtid", SqlDbType.Int, 4, tgtId)
			};

			ParamData pData = new ParamData("dbo.zp_BT_selectNOTICECOUNT", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}
        #endregion
    }
}
