using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZumNet.DAL.Base;

namespace ZumNet.DAL.ServiceDac
{
	public class ReferenceDacc : DacBase
	{
		/// <summary>
		/// 관리툴에서 게시물 쿼리
		/// </summary>
		/// <param name="connect"></param>
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
		public DataSet GetAdminMessageList(object connect, int domainID, int categoryID, int folderID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
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
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="messgaeID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAdminMessageInfo(object connect, int domainID, int categoryID, int folderID, int messgaeID, string xfAlias)
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
		/// <param name="connect"></param>
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
		public DataSet GetLinkSiteMessageList(object connect, int domainID, int folderID, int userID, string isAdmin, string isMain, string isScope, string parentACL, int pageIndex, int pageCount, out int totalMessage)
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
		/// <param name="connect"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetLinkSiteSubMessageList(object connect, int folderID)
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
		/// <param name="connect"></param>
		/// <param name="searchText"></param>
		/// <returns></returns>
		public DataSet SearchLinkSite(object connect, string searchText)
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
		/// <param name="connect"></param>
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
		public DataSet GetAlbumMessageList(object connect, int domainID, int folderID, int userID, string isAdmin, string isScope, string parentACL, int pageIndex, int pageCount, out int totalMessage)
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
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAlbumMessage(object connect, int userID, int folderID, int messageID, string xfAlias)
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
		/// <param name="connect"></param>
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
		public DataSet GetAnonymousMessageList(object connect, int domainID, int categoryID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
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
		/// <param name="connect"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAnonymousMessage(object connect, int folderID, string messageID, string xfAlias)
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
		/// <param name="connect"></param>
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
		public DataSet GetBoardMessageList(object connect, int domainID, int categoryID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
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
		/// <param name="connect"></param>
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
		public DataSet GetBoardMessageListAddTopLine(object connect, int domainID, int categoryID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
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
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetBoardMessage(object connect, int userID, int folderID, string messageID, string xfAlias)
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
		/// <param name="connect"></param>
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
		public DataSet GetDiscussList(object connect, int domainID, int categoryID, int parentFolderID, int groupID, int userID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
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
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetDiscussMessageList(object connect, int domainID, int categoryID, int folderID, int groupID, int userID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_DiscussGetMsgList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@ct_id", SqlDbType.Int, 4, categoryID);
			SqlParameter param3 = Utility.AddSqlParameters("@fd_id", SqlDbType.Int, 4, folderID);
			SqlParameter param4 = Utility.AddSqlParameters("@gr_id", SqlDbType.Int, 4, groupID);
			SqlParameter param5 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);
			SqlParameter param6 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param7 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param8 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param9 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param10 = Utility.AddSqlParameters("@searchCol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param11 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param12 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param13 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetDiscussMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetDiscussMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 토론 게시물 정보
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetDiscussMessage(object connect, int domainID, int categoryID, int folderID, int groupID, int userID, string xfAlias, int messageID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_DiscussGetMsgView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dnid", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@ctid", SqlDbType.Int, 4, categoryID);
			SqlParameter param3 = Utility.AddSqlParameters("@fdid", SqlDbType.Int, 4, folderID);
			SqlParameter param4 = Utility.AddSqlParameters("@grid", SqlDbType.Int, 4, groupID);
			SqlParameter param5 = Utility.AddSqlParameters("@urid", SqlDbType.Int, 4, userID);
			SqlParameter param6 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 30, xfAlias);
			SqlParameter param7 = Utility.AddSqlParameters("@msgid", SqlDbType.Int, 4, messageID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetDiscussMessage", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetDiscussMessage", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 토론 정보
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetDiscussInfo(object connect, int domainID, int categoryID, int userID, int folderID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_DiscussGetView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dnid", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@ctid", SqlDbType.Int, 4, categoryID);
			SqlParameter param3 = Utility.AddSqlParameters("@urid", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@fdid", SqlDbType.Int, 4, folderID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetDiscussInfo", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetDiscussInfo", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 최근 게시물 정보
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="isNotice"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetRecentlyMessageList(object connect, int domainID, int folderID, string expectText, string xfAlias, int categoryID, int userID, int items, string isNotice, string isAdmin, string parentACL)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_MsgGetRecentlyListView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@expecttext", SqlDbType.VarChar, 500, expectText);
			SqlParameter param4 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 30, xfAlias);
			SqlParameter param5 = Utility.AddSqlParameters("@categoryID", SqlDbType.Int, 4, categoryID);
			SqlParameter param6 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param7 = Utility.AddSqlParameters("@items", SqlDbType.Int, 4, items);
			SqlParameter param8 = Utility.AddSqlParameters("@isnotice", SqlDbType.VarChar, 1, isNotice);
			SqlParameter param9 = Utility.AddSqlParameters("@isadmin", SqlDbType.VarChar, 1, isAdmin);
			SqlParameter param10 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, parentACL);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetRecentlyMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetRecentlyMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 클럽 리스트
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetClubList(object connect, int domainID, int parentID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string inUse, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ClubGetCommList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@parentid", SqlDbType.Int, 4, parentID);
			SqlParameter param3 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param4 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param5 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param6 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param7 = Utility.AddSqlParameters("@searchCol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param8 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param9 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param10 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter param11 = Utility.AddSqlParameters("@inUse", SqlDbType.Char, 1, inUse);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetClubList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetClubList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 문서 리스트
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetDocMessageList(object connect, int domainID, int folderID, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_DocGetList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param5 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, permission);
			SqlParameter param6 = Utility.AddSqlParameters("@pageno", SqlDbType.Int, 4, pageIndex);
			SqlParameter param7 = Utility.AddSqlParameters("@pagecount", SqlDbType.Int, 4, pageCount);
			SqlParameter param8 = Utility.AddSqlParameters("@sortcol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param9 = Utility.AddSqlParameters("@sorttype", SqlDbType.VarChar, 20, sortType);
			SqlParameter param10 = Utility.AddSqlParameters("@searchcol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param11 = Utility.AddSqlParameters("@searchtext", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param12 = Utility.AddSqlParameters("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param13 = Utility.AddSqlParameters("@searchenddate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetDocMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetDocMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}


		/// <summary>
		/// XFORM 별 메세지 리스트 조회
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetProcessMessageList(object connect, int domainID, int folderID, string xfAlias, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetMsgList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@XFAlias", SqlDbType.VarChar, 20, xfAlias);
			SqlParameter param4 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param5 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param6 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, permission);
			SqlParameter param7 = Utility.AddSqlParameters("@pageno", SqlDbType.Int, 4, pageIndex);
			SqlParameter param8 = Utility.AddSqlParameters("@pagecount", SqlDbType.Int, 4, pageCount);
			SqlParameter param9 = Utility.AddSqlParameters("@sortcol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param10 = Utility.AddSqlParameters("@sorttype", SqlDbType.VarChar, 20, sortType);
			SqlParameter param11 = Utility.AddSqlParameters("@searchcol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@searchtext", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param13 = Utility.AddSqlParameters("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param14 = Utility.AddSqlParameters("@searchenddate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// XFORM 별 상위메세지 리스트(하위폴더포함) 조회
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetProcessParentMessageList(object connect, int domainID, string folderID, string xfAlias, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetParentMsgList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.NVarChar, 500, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@XFAlias", SqlDbType.VarChar, 20, xfAlias);
			SqlParameter param4 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param5 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param6 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, permission);
			SqlParameter param7 = Utility.AddSqlParameters("@pageno", SqlDbType.Int, 4, pageIndex);
			SqlParameter param8 = Utility.AddSqlParameters("@pagecount", SqlDbType.Int, 4, pageCount);
			SqlParameter param9 = Utility.AddSqlParameters("@sortcol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param10 = Utility.AddSqlParameters("@sorttype", SqlDbType.VarChar, 20, sortType);
			SqlParameter param11 = Utility.AddSqlParameters("@searchcol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@searchtext", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param13 = Utility.AddSqlParameters("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param14 = Utility.AddSqlParameters("@searchenddate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessParentMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessParentMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}
		/// <summary>
		/// XFAlias별 프로세스 메세지리스트 가져오기
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetProcessMyMessageList(object connect, int domainID, string xfAlias, int userID, int state, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetMyMsgList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@XFAlias", SqlDbType.VarChar, 20, xfAlias);
			SqlParameter param3 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@state", SqlDbType.Int, 4, state);
			SqlParameter param5 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param6 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, permission);
			SqlParameter param7 = Utility.AddSqlParameters("@pageno", SqlDbType.Int, 4, pageIndex);
			SqlParameter param8 = Utility.AddSqlParameters("@pagecount", SqlDbType.Int, 4, pageCount);
			SqlParameter param9 = Utility.AddSqlParameters("@sortcol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param10 = Utility.AddSqlParameters("@sorttype", SqlDbType.VarChar, 20, sortType);
			SqlParameter param11 = Utility.AddSqlParameters("@searchcol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@searchtext", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param13 = Utility.AddSqlParameters("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param14 = Utility.AddSqlParameters("@searchenddate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessMyMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessMyMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 나의 답변 QnA 리스트 
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetProcessAnswerMessageList(object connect, int domainID, string xfAlias, int userID, int state, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetAnswerMsgList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@XFAlias", SqlDbType.VarChar, 20, xfAlias);
			SqlParameter param3 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@state", SqlDbType.Int, 4, state);
			SqlParameter param5 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param6 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, permission);
			SqlParameter param7 = Utility.AddSqlParameters("@pageno", SqlDbType.Int, 4, pageIndex);
			SqlParameter param8 = Utility.AddSqlParameters("@pagecount", SqlDbType.Int, 4, pageCount);
			SqlParameter param9 = Utility.AddSqlParameters("@sortcol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param10 = Utility.AddSqlParameters("@sorttype", SqlDbType.VarChar, 20, sortType);
			SqlParameter param11 = Utility.AddSqlParameters("@searchcol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@searchtext", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param13 = Utility.AddSqlParameters("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param14 = Utility.AddSqlParameters("@searchenddate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessAnswerMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessAnswerMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 관리를 위한(상태,폴더별) 프로세스 메세지리스트 가져오기
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetProcessAdminMessageList(object connect, int domainID, string folderID, string xfAlias, int state, string taskActivity, int userID, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetAdminMsgList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.NVarChar, 100, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@XFAlias", SqlDbType.VarChar, 20, xfAlias);
			SqlParameter param4 = Utility.AddSqlParameters("@state", SqlDbType.Int, 4, state);
			SqlParameter param5 = Utility.AddSqlParameters("@taskactivity", SqlDbType.VarChar, 33, taskActivity);
			SqlParameter param6 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param7 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param8 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, permission);
			SqlParameter param9 = Utility.AddSqlParameters("@pageno", SqlDbType.Int, 4, pageIndex);
			SqlParameter param10 = Utility.AddSqlParameters("@pagecount", SqlDbType.Int, 4, pageCount);
			SqlParameter param11 = Utility.AddSqlParameters("@sortcol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@sorttype", SqlDbType.VarChar, 20, sortType);
			SqlParameter param13 = Utility.AddSqlParameters("@searchcol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param14 = Utility.AddSqlParameters("@searchtext", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param15 = Utility.AddSqlParameters("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param16 = Utility.AddSqlParameters("@searchenddate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessAdminMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessAdminMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		// <summary>
		/// 문서 등록정보
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="scope"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetDocMessageProperty(object connect, int domainID, int folderID, int userID, int messageID, string scope, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_DocGetProperty";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@messageID", SqlDbType.Int, 4, messageID);
			SqlParameter param5 = Utility.AddSqlParameters("@scope", SqlDbType.Char, 1, scope);
			SqlParameter param6 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 30, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetDocMessageProperty", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetDocMessageProperty", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 웹 파트 가져오기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="opID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetWebParts(object connect, int opID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_OpGetWebParts";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@op_id", SqlDbType.Int, 4, opID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetWebParts", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetWebParts", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 제안 읽기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetSuggestMessage(object connect, int userID, int folderID, int messageID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_SuggestGetView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@UR_ID", SqlDbType.Int, 4, userID);
			SqlParameter param2 = Utility.AddSqlParameters("@FD_ID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@MessageID", SqlDbType.Int, 4, messageID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetSuggestMessage", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetSuggestMessage", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 질의 내용 조회
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetQnAMessage(object connect, int userID, int folderID, int messageID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_QnAGetView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@UR_ID", SqlDbType.Int, 4, userID);
			SqlParameter param2 = Utility.AddSqlParameters("@FD_ID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@MessageID", SqlDbType.Int, 4, messageID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetSuggestMessage", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetSuggestMessage", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}


		/// <summary>
		/// 일정 목록 가져오기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="inputDate"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetScheduleList(object connect, int domainID, int groupID, int userID, string xfAlias, string inputDate, string mode)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ScheduleGetList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@gr_id", SqlDbType.Int, 4, groupID);
			SqlParameter param3 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 30, xfAlias);
			SqlParameter param5 = Utility.AddSqlParameters("@inputDate", SqlDbType.Char, 10, inputDate);
			SqlParameter param6 = Utility.AddSqlParameters("@mode", SqlDbType.Char, 1, mode);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetScheduleList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetScheduleList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 목록 가져오기2 - 검색 조건으로
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetScheduleSearchList(object connect, string actionKind, string objectType, int objectID, int state, string scheduleType, string mode, string inputDate, string startDate, string endDate)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ScheduleGetSearchList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@action_kind", SqlDbType.VarChar, 15, actionKind);
			SqlParameter param2 = Utility.AddSqlParameters("@objectType", SqlDbType.Char, 2, objectType);
			SqlParameter param3 = Utility.AddSqlParameters("@objectid", SqlDbType.Int, 4, objectID);
			SqlParameter param4 = Utility.AddSqlParameters("@state", SqlDbType.SmallInt, 2, state);
			SqlParameter param5 = Utility.AddSqlParameters("@schType", SqlDbType.Char, 2, scheduleType);
			SqlParameter param6 = Utility.AddSqlParameters("@mode", SqlDbType.Char, 1, mode);
			SqlParameter param7 = Utility.AddSqlParameters("@inputDate", SqlDbType.Char, 10, inputDate);
			SqlParameter param8 = Utility.AddSqlParameters("@rangeSDate", SqlDbType.Char, 10, startDate);
			SqlParameter param9 = Utility.AddSqlParameters("@rangeEDate", SqlDbType.Char, 10, endDate);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetScheduleSearchList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetScheduleSearchList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 보기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetScheduleView(object connect, int domainID, int messageID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ScheduleGetView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@msgid", SqlDbType.Int, 4, messageID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetScheduleView", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetScheduleView", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 목록 가져오기
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetPollMessageList(object connect, int domainID, string pollType, int categoryID, int folderID, int groupID, int userID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string parentACL, string targetType, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollGetList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@polltype", SqlDbType.VarChar, 20, pollType);
			SqlParameter param3 = Utility.AddSqlParameters("@ct_id", SqlDbType.Int, 4, categoryID);
			SqlParameter param4 = Utility.AddSqlParameters("@fd_id", SqlDbType.Int, 4, folderID);
			SqlParameter param5 = Utility.AddSqlParameters("@gr_id", SqlDbType.Int, 4, groupID);
			SqlParameter param6 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);
			SqlParameter param7 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param8 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param9 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param10 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param11 = Utility.AddSqlParameters("@searchCol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param13 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param14 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter param15 = Utility.AddSqlParameters("@parentAcl", SqlDbType.VarChar, 20, parentACL);
			SqlParameter param16 = Utility.AddSqlParameters("@TabType", SqlDbType.VarChar, 20, targetType);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 설문 개요가져오기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetPollMessage(object connect, int domainID, int folderID, string messageID, int groupID, int userID, int state)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollGetSummary";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@fd_id", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@msg_id", SqlDbType.VarChar, 10, messageID);
			SqlParameter param4 = Utility.AddSqlParameters("@gr_id", SqlDbType.Int, 4, groupID);
			SqlParameter param5 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);
			SqlParameter param6 = Utility.AddSqlParameters("@state", SqlDbType.Int, 4, state);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollView", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollView", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 문항
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="messageID"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetPollQuestions(object connect, string messageID, string mode)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollGetQuestions";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@msg_id", SqlDbType.VarChar, 10, messageID);
			SqlParameter param2 = Utility.AddSqlParameters("@mode", SqlDbType.VarChar, 1, mode);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollQuestions", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollQuestions", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		[AutoComplete]
		public DataSet GetPollItems(object connect, string messageID, int SeqID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollGetItems";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@msg_id", SqlDbType.VarChar, 10, messageID);
			SqlParameter param2 = Utility.AddSqlParameters("@seq_id", SqlDbType.Int, 4, SeqID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollItems", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollItems", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		[AutoComplete]
		public DataSet GetPollLiveMessageList(object connect, string msgType, int fdID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollGetLiveList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@msgType", SqlDbType.Char, 1, msgType);
			SqlParameter param2 = Utility.AddSqlParameters("@fdid", SqlDbType.Int, 4, fdID);


			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollLiveMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollLiveMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		///  설문 그룹화 가져오기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="msgID"></param>
		/// <param name="parentClass"></param>
		/// <param name="ClassCode"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetPollClassList(object connect, string msgID, string mode, int type, int topClassCode, int middleClassCode)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollGetClassList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@msgID", SqlDbType.VarChar, 10, msgID);
			SqlParameter param2 = Utility.AddSqlParameters("@mode", SqlDbType.VarChar, 20, mode);
			SqlParameter param3 = Utility.AddSqlParameters("@type", SqlDbType.Int, 4, type);
			SqlParameter param4 = Utility.AddSqlParameters("@topclassCode", SqlDbType.Int, 4, topClassCode);
			SqlParameter param5 = Utility.AddSqlParameters("@middleclassCode", SqlDbType.Int, 4, middleClassCode);


			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollClassList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollClassList", sqlParameters);
				}
				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;

		}

		/// <summary>
		/// 설문결과보기에서 기타 항목이나 주관식 문항에대한 리스트가져오기
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="msgID"></param>
		/// <param name="itemType"></param>
		/// <param name="exampleType"></param>
		/// <returns></returns>
		public DataSet GetPollExampleItemList(object connect, int msgID, int itemType, int exampleType, int seqID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PollExampleItemList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@msgID", SqlDbType.Int, 4, msgID);
			SqlParameter param2 = Utility.AddSqlParameters("@itemType", SqlDbType.Int, 4, itemType);
			SqlParameter param3 = Utility.AddSqlParameters("@exampleType", SqlDbType.Int, 4, exampleType);
			SqlParameter param4 = Utility.AddSqlParameters("@seqID", SqlDbType.Int, 4, seqID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetPollExampleItemList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetPollExampleItemList", sqlParameters);
				}
				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;

		}

		/// <summary>
		/// 자신이 가입한 커뮤니티 리스트 가져오기 
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetOwnerClubList(object connect, int userID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ClubGetMyCommList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetOwnerClubList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetOwnerClubList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 자신이 가입한 커뮤니티의 팝업 공지사항만 가져오기 
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetOwnerClubPopupMessageList(object connect, int userID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ClubGetMyPopupNotice";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetOwnerClubPopupMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetOwnerClubPopupMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹에 속한 모든(bbs, file, notice, album, linksite) 게시물을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="grID"></param>
		/// <param name="sortColum"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColum"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetClubAllMessageList(object connect, int grID, int pageIndex, int pageCount, string sortColum, string sortType, string searchColum, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ClubGetMessageList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@gr_id", SqlDbType.Int, 4, grID);
			SqlParameter param2 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param3 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param4 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColum);
			SqlParameter param5 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param6 = Utility.AddSqlParameters("@searchCol", SqlDbType.VarChar, 20, searchColum);
			SqlParameter param7 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param8 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param9 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetClubGetMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetClubGetMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalCount = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 내 문서함의 모든(bbs, file, album, linksite) 게시물을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetMyDocAllMessageList(object connect, int userID, int pageIndex, int pageCount, string sortColum, string sortType, string searchColum, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_PersonDocGetMessageList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@userid", SqlDbType.Int, 4, userID);
			SqlParameter param2 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param3 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param4 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColum);
			SqlParameter param5 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param6 = Utility.AddSqlParameters("@searchCol", SqlDbType.VarChar, 20, searchColum);
			SqlParameter param7 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param8 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param9 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "PersonDocGetMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "PersonDocGetMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalCount = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 최근 지식 문서 중 상위 5개의 목록을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetDocRecentlyList(object connect, string num, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_DocGetRecentList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@num", SqlDbType.VarChar, 12, num);
			SqlParameter param2 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 20, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetDocRecentlyList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetDocRecentlyList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 자신이 등록한 웹파트의 높이를 가져온다. 
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetWebPartSize(object connect, int userID, int opID, int seqID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_OpGetSize";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@urid", SqlDbType.Int, 4, userID);
			SqlParameter param2 = Utility.AddSqlParameters("@opid", SqlDbType.Int, 4, opID);
			SqlParameter param3 = Utility.AddSqlParameters("@seqid", SqlDbType.Int, 4, seqID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetWebPartSize", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetWebPartSize", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함 리스트를 가져온다.
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetAddressCardList(object connect, string actionKind, int domainID, string scope, string cardClass, int userID, int departmentID, string targetType, int targetID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string seekText, out int totalCount)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_AddressGetAddressCardList";
			string searchStartDate = "";
			string searchEndDate = "";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@action_kind", SqlDbType.Char, 1, actionKind);
			SqlParameter param2 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param3 = Utility.AddSqlParameters("@scope", SqlDbType.Char, 1, scope);
			SqlParameter param4 = Utility.AddSqlParameters("@class", SqlDbType.NVarChar, 500, cardClass);
			SqlParameter param5 = Utility.AddSqlParameters("@creatorID", SqlDbType.Int, 4, userID);
			SqlParameter param6 = Utility.AddSqlParameters("@creatorDeptID", SqlDbType.Int, 4, departmentID);
			SqlParameter param7 = Utility.AddSqlParameters("@targetType", SqlDbType.Char, 2, targetType);
			SqlParameter param8 = Utility.AddSqlParameters("@targetID", SqlDbType.Int, 4, targetID);
			SqlParameter param9 = Utility.AddSqlParameters("@page", SqlDbType.Int, 4, pageIndex);
			SqlParameter param10 = Utility.AddSqlParameters("@count", SqlDbType.Int, 4, pageCount);
			SqlParameter param11 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param13 = Utility.AddSqlParameters("@searchCol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param14 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param15 = Utility.AddSqlParameters("@seekSearchText", SqlDbType.NVarChar, 50, seekText);
			SqlParameter param16 = Utility.AddSqlParameters("@searchStartDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param17 = Utility.AddSqlParameters("@searchEndDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMessages", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, param17, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAddressCardList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAddressCardList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalCount = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 명함 정보를 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetAddressCardInfo(object connect, int domainID, int cardID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_AddressGetAddressCardInfo";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dnid", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@cardid", SqlDbType.Int, 4, cardID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAddressCardInfo", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAddressCardInfo", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함의 회사 리스트를 가져온다
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetAddressClientList(object connect, string actionKind, int domainID, string scope, string cardClass, int userID, int departmentID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string seekText, out int totalCount)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_AddressGetAddressClientList";
			string searchStartDate = "";
			string searchEndDate = "";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@action_kind", SqlDbType.Char, 1, actionKind);
			SqlParameter param2 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param3 = Utility.AddSqlParameters("@scope", SqlDbType.Char, 1, scope);
			SqlParameter param4 = Utility.AddSqlParameters("@class", SqlDbType.NVarChar, 500, cardClass);
			SqlParameter param5 = Utility.AddSqlParameters("@creatorID", SqlDbType.Int, 4, userID);
			SqlParameter param6 = Utility.AddSqlParameters("@creatorDeptID", SqlDbType.Int, 4, departmentID);
			SqlParameter param7 = Utility.AddSqlParameters("@page", SqlDbType.Int, 4, pageIndex);
			SqlParameter param8 = Utility.AddSqlParameters("@count", SqlDbType.Int, 4, pageCount);
			SqlParameter param9 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param10 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param11 = Utility.AddSqlParameters("@searchCol", SqlDbType.NVarChar, 20, searchColumn);
			SqlParameter param12 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param13 = Utility.AddSqlParameters("@seekSearchText", SqlDbType.NVarChar, 50, seekText);
			SqlParameter param14 = Utility.AddSqlParameters("@searchStartDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param15 = Utility.AddSqlParameters("@searchEndDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMessages", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAddressClientList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAddressClientList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalCount = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		/// <summary>
		/// 명함의 회사정보를 자져온다
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="clientID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetAddressClientInfo(object connect, int domainID, int clientID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_AddressGetAddressClientInfo";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dnid", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@clientid", SqlDbType.Int, 4, clientID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAddressClientInfo", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAddressClientInfo", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 명함의 범주 정보를 가져온다
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="scope"></param>
		/// <param name="userID"></param>
		/// <param name="groupID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetAddressClass(object connect, int domainID, string scope, int userID, int groupID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_AddressGetAddressClass";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dnid", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@scope", SqlDbType.Char, 1, scope);
			SqlParameter param3 = Utility.AddSqlParameters("@userid", SqlDbType.Int, 4, userID);
			SqlParameter param4 = Utility.AddSqlParameters("@grid", SqlDbType.Int, 4, groupID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAddressClass", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAddressClass", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 국가코드를 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="scope"></param>
		/// <param name="userID"></param>
		/// <param name="groupID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetAddressLocale(object connect)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_AddressGetLocale";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter[] sqlParameters = null;

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAddressLocale", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAddressLocale", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 우편번호 검색
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="searchDong"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetZipCodeSearch(object connect, string searchDong)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_GetZipCodeSearch";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@SearchDong", SqlDbType.NVarChar, 50, searchDong);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetZipCodeSearch", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetZipCodeSearch", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 포탈의 해당하는 폴더에 속한 메세지 리스트
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="num"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetLastestMessageList(object connect, int domainID, int folderID, string xfAlias, int userID, int num, string isAdmin, string parentACL)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_MsgGetListInPortal";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@domainID", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@folderID", SqlDbType.Int, 4, folderID);
			SqlParameter param3 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 30, xfAlias);
			SqlParameter param4 = Utility.AddSqlParameters("@userID", SqlDbType.Int, 4, userID);
			SqlParameter param5 = Utility.AddSqlParameters("@num", SqlDbType.Int, 4, num);
			SqlParameter param6 = Utility.AddSqlParameters("@isadmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param7 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, parentACL);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetLastestMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetLastestMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		[AutoComplete]
		public DataSet GetBookList(object connect, int domainID, int categoryID, int folderID, int userID, string xfAlias, string isAdmin, string parentACL, int pageIndex, int PageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string isTotal, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_BookGetList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@ct_id", SqlDbType.Int, 4, categoryID);
			SqlParameter param3 = Utility.AddSqlParameters("@fd_id", SqlDbType.Int, 4, folderID);
			SqlParameter param4 = Utility.AddSqlParameters("@ur_id", SqlDbType.Int, 4, userID);
			SqlParameter param5 = Utility.AddSqlParameters("@xfAlias", SqlDbType.VarChar, 30, xfAlias);
			SqlParameter param6 = Utility.AddSqlParameters("@isAdmin", SqlDbType.Char, 1, isAdmin);
			SqlParameter param7 = Utility.AddSqlParameters("@parentACL", SqlDbType.VarChar, 20, parentACL);
			SqlParameter param8 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param9 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, PageCount);
			SqlParameter param10 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param11 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param12 = Utility.AddSqlParameters("@searchCol", SqlDbType.VarChar, 20, searchColumn);
			SqlParameter param13 = Utility.AddSqlParameters("@searchText", SqlDbType.VarChar, 200, searchText);
			SqlParameter param14 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param15 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter param16 = Utility.AddSqlParameters("@isTotal", SqlDbType.Char, 1, isTotal);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);


			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetBookList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetBookList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		[AutoComplete]
		public DataSet GetBookView(object connect, int messageID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_BookGetView";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@MessageId", SqlDbType.Int, 4, messageID);


			SqlParameter[] sqlParameters = new SqlParameter[] { param1 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetBookView", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetBookView", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 관련문서(규정,메뉴얼) 리스트를 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetLinkedDocList(object connect, string xfAlias, string messageID)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_LinkedDocGetList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 30, xfAlias);
			SqlParameter param2 = Utility.AddSqlParameters("@messageID", SqlDbType.VarChar, 33, messageID);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetLinkedDocList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetLinkedDocList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// QnA의 가장 최신 질의 글을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetProcessRecentlyList(object connect, string num, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetRecentList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@num", SqlDbType.VarChar, 12, num);
			SqlParameter param2 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 20, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessRecentlyList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessRecentlyList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// QnA의 가장 최신 완료된 질의 글을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetProcessCompleteList(object connect, string num, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetCompleteList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@num", SqlDbType.VarChar, 12, num);
			SqlParameter param2 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 20, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessCompleteList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessCompleteList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당하는 달의 월간 최다 등록자 5명의 목록을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetProcessMonthlyCreateUser(object connect, int num, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetMonthlyCreateUser";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@num", SqlDbType.Int, 4, num);
			SqlParameter param2 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 20, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessMonthlyCreateUser", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessMonthlyCreateUser", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		///  해당하는 달의 월간 최다 조회 문서 5개의 목록을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetProcessMonthlyViewList(object connect, int num, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "";

			if (xfAlias == "qna")
				strSP = "admin.ph_up_ProcessGetMonthlyViewList";
			else if (xfAlias == "suggest")
				strSP = "admin.ph_up_ProcessGetMonthlyBestSuggestList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@num", SqlDbType.Int, 4, num);
			SqlParameter param2 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 20, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessMonthlyViewList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessMonthlyViewList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당하는 달의 월간 최다 추천자 5명의 목록을 가져온다.
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="num"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		[AutoComplete]
		public DataSet GetProcessMonthlyEvalUser(object connect, int num, string xfAlias)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_ProcessGetMonthlyEvalUser";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@num", SqlDbType.Int, 4, num);
			SqlParameter param2 = Utility.AddSqlParameters("@xfalias", SqlDbType.VarChar, 20, xfAlias);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2 };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetProcessMonthlyEvalUser", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetProcessMonthlyEvalUser", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리툴에서의 문서 반출입 정보
		/// </summary>
		/// <param name="connect"></param>
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
		[AutoComplete]
		public DataSet GetAdminCheckInOutList(object connect, int domainID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn
		, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_BaseGetCheckInOutList";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@dn_id", SqlDbType.Int, 4, domainID);
			SqlParameter param2 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param3 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param4 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColumn);
			SqlParameter param5 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param6 = Utility.AddSqlParameters("@searchCol", SqlDbType.VarChar, 20, searchColumn);
			SqlParameter param7 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param8 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param9 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetAdminCheckInOutList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetAdminCheckInOutList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalMessage = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}

		[AutoComplete]
		public DataSet GetRecentlyMessageListOfCT(object connect, int ctID, int pageIndex, int pageCount, string sortColum, string sortType, string searchColum, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DBHelperSQL comDH = null;
			DataSet dsReturn = null;
			string strSP = "admin.ph_up_MsgGetRecentlyMessageListOfCT";

			//실행 시간 준비는 변수 선언 다음에 한다.
			_executionTimeLog.Prepare();

			SqlParameter param1 = Utility.AddSqlParameters("@ct_id", SqlDbType.Int, 4, ctID);
			SqlParameter param2 = Utility.AddSqlParameters("@pageIdx", SqlDbType.Int, 4, pageIndex);
			SqlParameter param3 = Utility.AddSqlParameters("@pageCnt", SqlDbType.Int, 4, pageCount);
			SqlParameter param4 = Utility.AddSqlParameters("@sortCol", SqlDbType.VarChar, 20, sortColum);
			SqlParameter param5 = Utility.AddSqlParameters("@sortType", SqlDbType.VarChar, 20, sortType);
			SqlParameter param6 = Utility.AddSqlParameters("@searchCol", SqlDbType.VarChar, 20, searchColum);
			SqlParameter param7 = Utility.AddSqlParameters("@searchText", SqlDbType.NVarChar, 200, searchText);
			SqlParameter param8 = Utility.AddSqlParameters("@searchSDate", SqlDbType.VarChar, 10, searchStartDate);
			SqlParameter param9 = Utility.AddSqlParameters("@searchEDate", SqlDbType.VarChar, 10, searchEndDate);
			SqlParameter outParam = Utility.AddSqlParameters("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output);

			SqlParameter[] sqlParameters = new SqlParameter[] { param1, param2, param3, param4, param5, param6, param7, param8, param9, outParam };

			try
			{
				comDH = new DBHelperSQL();

				if (connect.GetType() == "".GetType())
				{
					dsReturn = comDH.ExecuteDataset((string)connect, "", strSP, 15, "GetRecentlyMessageList", sqlParameters);
				}
				else
				{
					dsReturn = comDH.ExecuteDataset((SqlConnection)connect, "", strSP, 15, "GetRecentlyMessageList", sqlParameters);
				}

				comDH.Dispose();

				//실행 시간 실행은 로직 마지막에 위치한다.
				_executionTimeLog.MeasureExecutionTime(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()
													, System.Reflection.MethodInfo.GetCurrentMethod());
			}
			catch (Exception ex)
			{
				if (comDH != null)
				{
					comDH.Dispose();
				}

				ExceptionManager.ThrowException(ex, System.Reflection.MethodInfo.GetCurrentMethod(), "", "");
			}

			totalCount = int.Parse(outParam.Value.ToString());
			return dsReturn;
		}
	}
}
