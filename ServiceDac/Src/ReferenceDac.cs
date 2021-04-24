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
	public class ReferenceDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ReferenceDac(string connectionString = "") : base(connectionString)
		{

		}


		/// <summary>
		/// 관리툴에서 게시물 쿼리
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetAdminMessageList(int domainID, int categoryID, int folderID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리툴에서의 해당 게시물 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="messgaeID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAdminMessageInfo(int domainID, int categoryID, int folderID, int messgaeID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@categoryID", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messgaeID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetMsgInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 링크싸이트 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="isScope"></param>
		/// <param name="parentACL"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetLinkSiteMessageList(int domainID, int folderID, int userID, string isAdmin, string isMain, string isScope, string parentACL, int pageIndex, int pageCount, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@isMain", SqlDbType.Char, 1, isMain),
				ParamSet.Add4Sql("@isscope", SqlDbType.Char, 1, isScope),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkSiteGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 하위폴더의 링크싸이트 리스트를 가져온다.
		/// </summary>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetLinkSiteSubMessageList(int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkSiteGetSubList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 링크싸이트 검색
		/// </summary>
		/// <param name="searchText"></param>
		/// <returns></returns>
		public DataSet SearchLinkSite(string searchText)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 100, searchText)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkSiteSearch", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 앨범 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="isScope"></param>
		/// <param name="parentACL"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetAlbumMessageList(int domainID, int folderID, int userID, string isAdmin, string isScope, string parentACL, int pageIndex, int pageCount, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@isscope", SqlDbType.Char, 1, isScope),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AlbumGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 앨범 조회
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAlbumMessage(int userID, int folderID, int messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetAlbumView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 익명 게시 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetAnonymousMessageList(int domainID, int categoryID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonymousGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 익명 게시물 정보
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAnonymousMessage(int folderID, string messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonymousGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 게시 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetBoardMessageList(int domainID, int categoryID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 톱라인이 글이 추가된 게시 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetBoardMessageListAddTopLine(int domainID, int categoryID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetListAddTopLine", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 게시물에 대한 정보
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetBoardMessage(int userID, int folderID, string messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 토론 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetDiscussList(int domainID, int categoryID, int parentFolderID, int groupID, int userID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@parentfdid", SqlDbType.Int, 4, parentFolderID),
				ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 토론 세부 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetDiscussMessageList(int domainID, int categoryID, int folderID, int groupID, int userID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussGetMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 토론 게시물 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetDiscussMessage(int domainID, int categoryID, int folderID, int groupID, int userID, string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussGetMsgView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 토론 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetDiscussInfo(int domainID, int categoryID, int userID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 최근 게시물 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="isNotice"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <returns></returns>
		public DataSet GetRecentlyMessageList(int domainID, int folderID, string expectText, string xfAlias, int categoryID, int userID, int items, string isNotice, string isAdmin, string parentACL)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@expecttext", SqlDbType.VarChar, 500, expectText),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@categoryID", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@items", SqlDbType.Int, 4, items),
				ParamSet.Add4Sql("@isnotice", SqlDbType.VarChar, 1, isNotice),
				ParamSet.Add4Sql("@isadmin", SqlDbType.VarChar, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetRecentlyListView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 클럽 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="parentID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="inUse"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetClubList(int domainID, int parentID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string inUse, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@parentid", SqlDbType.Int, 4, parentID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@inUse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetCommList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetDocMessageList(int domainID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// XFORM 별 메세지 리스트 조회
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetProcessMessageList(int domainID, int folderID, string xfAlias, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// XFORM 별 상위메세지 리스트(하위폴더포함) 조회
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetProcessParentMessageList(int domainID, string folderID, string xfAlias, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.NVarChar, 500, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetParentMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// XFAlias별 프로세스 메세지리스트 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetProcessMyMessageList(int domainID, string xfAlias, int userID, int state, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetMyMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 나의 답변 QnA 리스트 
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="state"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetProcessAnswerMessageList(int domainID, string xfAlias, int userID, int state, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetAnswerMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리를 위한(상태,폴더별) 프로세스 메세지리스트 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="state"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetProcessAdminMessageList(int domainID, string folderID, string xfAlias, int state, string taskActivity, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.NVarChar, 100, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@taskactivity", SqlDbType.VarChar, 33, taskActivity),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetAdminMsgList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		// <summary>
		/// 문서 등록정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="scope"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocMessageProperty(int domainID, int folderID, int userID, int messageID, string scope, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetProperty", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 웹 파트 가져오기
		/// </summary>
		/// <param name="opID"></param>
		/// <returns></returns>
		public DataSet GetWebParts(int opID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@op_id", SqlDbType.Int, 4, opID)
			};

			ParamData pData = new ParamData("admin.ph_up_OpGetWebParts", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 제안 읽기
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetSuggestMessage(int userID, int folderID, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UR_ID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_SuggestGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 질의 내용 조회
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetQnAMessage(int userID, int folderID, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UR_ID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_QnAGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 목록 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="inputDate"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public DataSet GetScheduleList(int domainID, int groupID, int userID, string xfAlias, string inputDate, string mode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@inputDate", SqlDbType.Char, 10, inputDate),
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 목록 가져오기2 - 검색 조건으로
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="objectType"></param>
		/// <param name="partID"></param>
		/// <param name="taskID"></param>
		/// <param name="state"></param>
		/// <param name="scheduleType"></param>
		/// <param name="mode"></param>
		/// <param name="inputDate"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetScheduleSearchList(string actionKind, string objectType, int objectID, int state, string scheduleType, string mode, string inputDate, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.VarChar, 15, actionKind),
				ParamSet.Add4Sql("@objectType", SqlDbType.Char, 2, objectType),
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
				ParamSet.Add4Sql("@schType", SqlDbType.Char, 2, scheduleType),
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@inputDate", SqlDbType.Char, 10, inputDate),
				ParamSet.Add4Sql("@rangeSDate", SqlDbType.Char, 10, startDate),
				ParamSet.Add4Sql("@rangeEDate", SqlDbType.Char, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetSearchList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 보기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetScheduleView(int domainID, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 목록 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="pollType"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="parentACL"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetPollMessageList(int domainID, string pollType, int categoryID, int folderID, int groupID, int userID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string parentACL, string targetType, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@polltype", SqlDbType.VarChar, 20, pollType),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@parentAcl", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@TabType", SqlDbType.VarChar, 20, targetType),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 개요가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetPollMessage(int domainID, int folderID, string messageID, int groupID, int userID, int state)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@msg_id", SqlDbType.VarChar, 10, messageID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetSummary", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 문항
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public DataSet GetPollQuestions(string messageID, string mode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msg_id", SqlDbType.VarChar, 10, messageID),
				ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 1, mode)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetQuestions", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="SeqID"></param>
		/// <returns></returns>
		public DataSet GetPollItems(string messageID, int SeqID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msg_id", SqlDbType.VarChar, 10, messageID),
				ParamSet.Add4Sql("@seq_id", SqlDbType.Int, 4, SeqID)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetItems", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msgType"></param>
		/// <param name="fdID"></param>
		/// <returns></returns>
		public DataSet GetPollLiveMessageList(string msgType, int fdID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgType", SqlDbType.Char, 1, msgType),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetLiveList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		///  설문 그룹화 가져오기
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="parentClass"></param>
		/// <param name="ClassCode"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public DataSet GetPollClassList(string msgID, string mode, int type, int topClassCode, int middleClassCode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgID", SqlDbType.VarChar, 10, msgID),
				ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 20, mode),
				ParamSet.Add4Sql("@type", SqlDbType.Int, 4, type),
				ParamSet.Add4Sql("@topclassCode", SqlDbType.Int, 4, topClassCode),
				ParamSet.Add4Sql("@middleclassCode", SqlDbType.Int, 4, middleClassCode)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetClassList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문결과보기에서 기타 항목이나 주관식 문항에대한 리스트가져오기
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="itemType"></param>
		/// <param name="exampleType"></param>
		/// <returns></returns>
		public DataSet GetPollExampleItemList(int msgID, int itemType, int exampleType, int seqID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgID", SqlDbType.Int, 4, msgID),
				ParamSet.Add4Sql("@itemType", SqlDbType.Int, 4, itemType),
				ParamSet.Add4Sql("@exampleType", SqlDbType.Int, 4, exampleType),
				ParamSet.Add4Sql("@seqID", SqlDbType.Int, 4, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_PollExampleItemList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 자신이 가입한 커뮤니티 리스트 가져오기 
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetOwnerClubList(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetMyCommList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 자신이 가입한 커뮤니티의 팝업 공지사항만 가져오기 
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetOwnerClubPopupMessageList(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetMyPopupNotice", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹에 속한 모든(bbs, file, notice, album, linksite) 게시물을 가져온다.
		/// </summary>
		/// <param name="grID"></param>
		/// <param name="sortColum"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColum"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <returns></returns>
		public DataSet GetClubAllMessageList(int grID, int pageIndex, int pageCount, string sortColum, string sortType, string searchColum, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, grID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColum),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColum),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetMessageList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 내 문서함의 모든(bbs, file, album, linksite) 게시물을 가져온다.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColum"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColum"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalCount"></param>
		/// <returns></returns>
		public DataSet GetMyDocAllMessageList(int userID, int pageIndex, int pageCount, string sortColum, string sortType, string searchColum, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColum),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColum),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_PersonDocGetMessageList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 최근 지식 문서 중 상위 5개의 목록을 가져온다.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocRecentlyList(string num, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, num),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetRecentList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 자신이 등록한 웹파트의 높이를 가져온다. 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		public DataSet GetWebPartSize(int userID, int opID, int seqID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@opid", SqlDbType.Int, 4, opID),
				ParamSet.Add4Sql("@seqid", SqlDbType.Int, 4, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_OpGetSize", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함 리스트를 가져온다.
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="domainID"></param>
		/// <param name="scope"></param>
		/// <param name="cardClass"></param>
		/// <param name="userID"></param>
		/// <param name="departmentID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="seekText"></param>
		/// <param name="totalCount"></param>
		/// <returns></returns>
		public DataSet GetAddressCardList(string actionKind, int domainID, string scope, string cardClass, int userID, int departmentID, string targetType, int targetID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string seekText, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@class", SqlDbType.NVarChar, 500, cardClass),
				ParamSet.Add4Sql("@creatorID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@creatorDeptID", SqlDbType.Int, 4, departmentID),
				ParamSet.Add4Sql("@targetType", SqlDbType.Char, 2, targetType),
				ParamSet.Add4Sql("@targetID", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@count", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@seekSearchText", SqlDbType.NVarChar, 50, seekText),
				ParamSet.Add4Sql("@searchStartDate", SqlDbType.VarChar, 10, ""),
				ParamSet.Add4Sql("@searchEndDate", SqlDbType.VarChar, 10, ""),
				ParamSet.Add4Sql("@totalMessages", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressGetAddressCardList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMessages").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함 정보를 가져온다.
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetAddressCardInfo(int domainID, int cardID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@cardid", SqlDbType.Int, 4, cardID)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressGetAddressCardInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함의 회사 리스트를 가져온다
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="domainID"></param>
		/// <param name="scope"></param>
		/// <param name="cardClass"></param>
		/// <param name="userID"></param>
		/// <param name="departmentID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="seekText"></param>
		/// <param name="totalCount"></param>
		/// <returns></returns>
		public DataSet GetAddressClientList(string actionKind, int domainID, string scope, string cardClass, int userID, int departmentID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string seekText, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@class", SqlDbType.NVarChar, 500, cardClass),
				ParamSet.Add4Sql("@creatorID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@creatorDeptID", SqlDbType.Int, 4, departmentID),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@count", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@seekSearchText", SqlDbType.NVarChar, 50, seekText),
				ParamSet.Add4Sql("@searchStartDate", SqlDbType.VarChar, 10, ""),
				ParamSet.Add4Sql("@searchEndDate", SqlDbType.VarChar, 10, ""),
				ParamSet.Add4Sql("@totalMessages", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressGetAddressClientList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMessages").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함의 회사정보를 자져온다
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="clientID"></param>
		/// <returns></returns>
		public DataSet GetAddressClientInfo(int domainID, int clientID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@clientid", SqlDbType.Int, 4, clientID)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressGetAddressClientInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함의 범주 정보를 가져온다
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="scope"></param>
		/// <param name="userID"></param>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetAddressClass(int domainID, string scope, int userID, int groupID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressGetAddressClass", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 국가코드를 가져온다.
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="scope"></param>
		/// <param name="userID"></param>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetAddressLocale()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				
			};

			ParamData pData = new ParamData("admin.ph_up_AddressGetLocale", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 우편번호 검색
		/// </summary>
		/// <param name="searchDong"></param>
		/// <returns></returns>
		public DataSet GetZipCodeSearch(string searchDong)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@SearchDong", SqlDbType.NVarChar, 50, searchDong)
			};

			ParamData pData = new ParamData("admin.ph_up_GetZipCodeSearch", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 포탈의 해당하는 폴더에 속한 메세지 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="num"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <returns></returns>
		public DataSet GetLastestMessageList(int domainID, int folderID, string xfAlias, int userID, int num, string isAdmin, string parentACL)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@num", SqlDbType.Int, 4, num),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetListInPortal", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <param name="pageIndex"></param>
		/// <param name="PageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="isTotal"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetBookList(int domainID, int categoryID, int folderID, int userID, string xfAlias, string isAdmin, string parentACL, int pageIndex, int PageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string isTotal, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@xfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, PageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.VarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@isTotal", SqlDbType.Char, 1, isTotal),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BookGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetBookView(int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@MessageId", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_BookGetView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 관련문서(규정,메뉴얼) 리스트를 가져온다.
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetLinkedDocList(string xfAlias, string messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageID", SqlDbType.VarChar, 33, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkedDocGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// QnA의 가장 최신 질의 글을 가져온다.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetProcessRecentlyList(string num, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, num),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetRecentList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// QnA의 가장 최신 완료된 질의 글을 가져온다.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetProcessCompleteList(string num, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, num),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetCompleteList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당하는 달의 월간 최다 등록자 5명의 목록을 가져온다.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetProcessMonthlyCreateUser(int num, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, num),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetMonthlyCreateUser", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		///  해당하는 달의 월간 최다 조회 문서 5개의 목록을 가져온다.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetProcessMonthlyViewList(int num, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, num),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			string strSP = "";

			if (xfAlias == "qna")
				strSP = "admin.ph_up_ProcessGetMonthlyViewList";
			else if (xfAlias == "suggest")
				strSP = "admin.ph_up_ProcessGetMonthlyBestSuggestList";

			ParamData pData = new ParamData(strSP, parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당하는 달의 월간 최다 추천자 5명의 목록을 가져온다.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetProcessMonthlyEvalUser(int num, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, num),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetMonthlyEvalUser", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리툴에서의 문서 반출입 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetAdminCheckInOutList(int domainID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn
		, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetCheckInOutList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColum"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColum"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="totalCount"></param>
		/// <returns></returns>
		public DataSet GetRecentlyMessageListOfCT(int ctID, int pageIndex, int pageCount, string sortColum, string sortType, string searchColum, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, ctID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColum),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColum),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetRecentlyMessageListOfCT", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}
	}
}
