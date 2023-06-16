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
	public class RelationDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public RelationDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public RelationDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 댓글 암호 조회
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="seqID"></param>
		/// <returns></returns>
		public string GetCommentMessagePassword(string messageID, string xfAlias, int seqID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@seqID", SqlDbType.Int, 4, seqID),
			};

			ParamData pData = new ParamData("admin.ph_up_AnonyGetCommentPwd", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 게시물에 대한 답글의 목록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="seqID"></param>
		/// <returns></returns>
		public DataSet GetReplyMessageList(int folderID, string xfAlias, int seqID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@seqID", SqlDbType.Int, 4, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetReplyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 지식관리 게시물 답글 목록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="seqID"></param>
		/// <returns></returns>
		public DataSet GetKmsReplyList(int folderID, string xfAlias, int seqID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@seqID", SqlDbType.Int, 4, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_KmsGetReplyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 익명 게시판의 답글 리스트
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="seqID"></param>
		/// <returns></returns>
		public DataSet GetAnonymousReplyMessageList(int folderID, string xfAlias, int seqID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@seqID", SqlDbType.Int, 4, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonyGetReplyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 게시물에 작성된 덧글의 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="seqID"></param>
		/// <returns></returns>
		public void DeleteCommentMessage(string xfAlias, string messageID, string seqID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 2000, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 2000, messageID),
				ParamSet.Add4Sql("@seqid", SqlDbType.VarChar, 2000, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgCommentRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 게시물에 대한 덧글의 목록
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetCommentMessageList(string messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetCommentList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

        /// <summary>
        /// 익명게시판 덧글 입력
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="messageID"></param>
        /// <param name="creator"></param>
        /// <param name="password"></param>
        /// <param name="comment"></param>
        /// <param name="seqID"></param>
        public void CreateAnonymousCommentMessage(string xfAlias, string messageID, string creator, string password, string comment, int seqID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 30, messageID),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 50, creator),
				ParamSet.Add4Sql("@password", SqlDbType.NVarChar, 100, password),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@seqid", SqlDbType.Int, 4, seqID)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonyCommentSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 게시물에 대한 덧글 입력
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="seqID"></param>
		/// <param name="userID"></param>
		/// <param name="userName"></param>
		/// <param name="comment"></param>
		/// <param name="deleteDate"></param>
		public void CreateCommentMessage(string xfAlias, int messageID, int seqID, string userID, string userName, string comment, string deleteDate)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@SeqID", SqlDbType.Int, 4, seqID),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 50, userName),
				ParamSet.Add4Sql("@Body", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@DeleteDate", SqlDbType.VarChar, 10, deleteDate)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgCommentSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 댓글 작성
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="seqID"></param>
		/// <param name="userID"></param>
		/// <param name="userName"></param>
		/// <param name="comment"></param>
		/// <param name="eval"></param>
		/// <param name="point"></param>
		/// <param name="deleteDate"></param>
		/// <returns></returns>
		public string CreateProcessCommentMessage(string xfAlias, int messageID, int seqID, string userID
							, string userName, string comment, string eval, int point, string deleteDate)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@SeqID", SqlDbType.Int, 4, seqID),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 50, userName),
				ParamSet.Add4Sql("@Body", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@Eval", SqlDbType.NVarChar, 10, eval),
				ParamSet.Add4Sql("@Point", SqlDbType.Int, 4, point),
				ParamSet.Add4Sql("@DeleteDate", SqlDbType.VarChar, 10, deleteDate),
				ParamSet.Add4Sql("@Result", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCommentSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//strReturn = pData.GetParamValue("@Result").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 게시물이 소유한 첨부파일 목록
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAttachFile(int domainID, int messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_GetAttachFile", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 첨부 파일, 본문 이미지의 저장 경로 및 첨부 파일, 본문 이미지의 Prefix 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetAttachPath(int domainID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetAttachPath", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 현재 게시물에 대한 이전, 다음 MessageID
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="returnPrevMessageID"></param>
		/// <param name="returnNextMessageID"></param>
		public void GetPrevNextMessageNumber(int domainID, int categoryID, int folderID, int userID, int messageID, string isAdmin
								, string parentACL, string sortColumn, string sortType, string searchColumn, string searchText
								, string searchStartDate, string searchEndDate, out string returnPrevMessageID, out string returnNextMessageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.VarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@prevMessageID", SqlDbType.Int, 4, ParameterDirection.Output),
				ParamSet.Add4Sql("@nextMessageID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetListPrevNext", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				returnPrevMessageID = pData.GetParamValue("@prevMessageID").ToString();
				returnNextMessageID = pData.GetParamValue("@nextMessageID").ToString();
			}
		}

		/// <summary>
		/// 현재 익명 게시물에 대한 이전, 다음 MessageID
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="returnPrevMessageID"></param>
		/// <param name="returnNextMessageID"></param>
		public void GetPrevNextAnonyNumber(int domainID, int categoryID, int folderID, int userID, int messageID, string isAdmin
						, string parentACL, string sortColumn, string sortType, string searchColumn, string searchText
						, string searchStartDate, string searchEndDate, out string returnPrevMessageID, out string returnNextMessageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.VarChar, 200, searchText),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@prevMessageID", SqlDbType.Int, 4, ParameterDirection.Output),
				ParamSet.Add4Sql("@nextMessageID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonyGetListPrevNext", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				returnPrevMessageID = pData.GetParamValue("@prevMessageID").ToString();
				returnNextMessageID = pData.GetParamValue("@nextMessageID").ToString();
			}
		}

		/// <summary>
		/// 현재 앨범에 이전 다음 MessageID
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="parentACL"></param>
		/// <param name="returnPrevMessageID"></param>
		/// <param name="returnNextMessageID"></param>
		public void GetPrevNextAlbumNumber(int domainID, int categoryID, int folderID, int userID, int messageID, string isAdmin
									, string parentACL, out string returnPrevMessageID, out string returnNextMessageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@isAdmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentACL", SqlDbType.VarChar, 20, parentACL),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, "SeqID"),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, "DESC"),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, ""),
				ParamSet.Add4Sql("@searchText", SqlDbType.VarChar, 200, ""),
				ParamSet.Add4Sql("@searchSDate", SqlDbType.VarChar, 10, ""),
				ParamSet.Add4Sql("@searchEDate", SqlDbType.VarChar, 10, ""),
				ParamSet.Add4Sql("@prevMessageID", SqlDbType.Int, 4, ParameterDirection.Output),
				ParamSet.Add4Sql("@nextMessageID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AlbumGetListPrevNext", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				returnPrevMessageID = pData.GetParamValue("@prevMessageID").ToString();
				returnNextMessageID = pData.GetParamValue("@nextMessageID").ToString();
			}
		}

		/// <summary>
		/// LocID값을 가져옴
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="fileOrImg"></param>
		/// <returns>locID</returns>
		public int GetMessageLocID(int domainID, string xfAlias, string fileOrImg)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@FileOrImg", SqlDbType.VarChar, 6, fileOrImg),
				ParamSet.Add4Sql("@LocID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetLocID", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//locID = Convert.ToInt32(pData.GetParamValue("@LocID").ToString());
			}

			return iReturn;
		}

        /// <summary>
		/// 조회 기록 입력 및 조회수 수정
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="ipAddress"></param>
		public void CreateViewingNumber(string xfAlias, int folderID, int userID, int messageID, string ipAddress)
        {
			CreateViewingNumber(xfAlias, folderID, userID, messageID, ipAddress, "");
        }

        /// <summary>
		/// 조회 기록 입력 및 조회수 수정
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="ipAddress"></param>
		/// <param name="viewer"></param>
        public void CreateViewingNumber(string xfAlias, int folderID, int userID, int messageID, string ipAddress, string viewer)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@IP", SqlDbType.VarChar, 30, ipAddress),
                ParamSet.Add4Sql("@viewer", SqlDbType.VarChar, 50, viewer)
            };

			ParamData pData = new ParamData("admin.ph_up_MsgSetViewEvent", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

        /// <summary>
		/// 게시물 좋아요 기록
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="actor"></param>
		/// <param name="point"></param>
		public void SetMsgLikeEvent(string xfAlias, int messageID, string actor, int point)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),                  
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@actor", SqlDbType.VarChar, 50, actor),
                ParamSet.Add4Sql("@point", SqlDbType.Int, 4, point)
            };

            ParamData pData = new ParamData("admin.ph_up_MsgSetLikeEvent", parameters);

            using (DbBase db = new DbBase())
            {
                string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
            }
        }

        /// <summary>
        /// 지직관리 공유자 읽음 설정
        /// </summary>
        /// <param name="xfAlias"></param>
        /// <param name="folderID"></param>
        /// <param name="userID"></param>
        /// <param name="messageID"></param>
        public void SetKmsState(string xfAlias, int folderID, int userID, string messageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 33, messageID),
			};

			ParamData pData = new ParamData("admin.ph_up_KmsSetState", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 파일 및 이미지에 대한 PH_FILE_REPOSITORY의 PREFIX값을 변경
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="xfAlias"></param>
		/// <param name="isFile"></param>
		public void ChangePrefixNumber(string prefix, string xfAlias, string isFile)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@Prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgChangePrefixNumber", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 게시물에 첨부된 파일을 삭제
		/// </summary>
		/// <param name="attachID"></param>
		/// <param name="xfAlias"></param>
		public void DeleteAttachFile(int attachID, string xfAlias)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@AttachID", SqlDbType.Int, 4, attachID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 20, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DelAttachFile", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 문서의 첨부파일 정보
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocAttachFile(int folderID, int messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetFileInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통보 메일 수신 여부 알아오기
		/// </summary>
		/// <param name="sendID"></param>
		/// <param name="sendType"></param>
		/// <param name="userID"></param>
		/// <param name="isGroup"></param>
		/// <param name="processID"></param>
		/// <param name="mailAccount"></param>
		/// <param name="subject"></param>
		/// <param name="sendAccount"></param>
		public void GetSendMailAccount(string sendID, string sendType, string userID, string isGroup, int processID
								, out string mailAccount, out string subject, out string sendAccount)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@sendid", SqlDbType.VarChar, 64, sendID),
				ParamSet.Add4Sql("@sendtype", SqlDbType.Char, 1, sendType),
				ParamSet.Add4Sql("@userid", SqlDbType.VarChar, 2000, userID),
				ParamSet.Add4Sql("@isgroup", SqlDbType.VarChar, 500, isGroup),
				ParamSet.Add4Sql("@pid", SqlDbType.Int, 4, processID),
				ParamSet.Add4Sql("@mailaccount", SqlDbType.VarChar, 2000, ParameterDirection.Output),
				ParamSet.Add4Sql("@subject", SqlDbType.VarChar, 500, ParameterDirection.Output),
				ParamSet.Add4Sql("@sendaccount", SqlDbType.VarChar, 64, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetSendMailAccount", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);

				mailAccount = pData.GetParamValue("@mailaccount").ToString();
				subject = pData.GetParamValue("@subject").ToString();
				sendAccount = pData.GetParamValue("@sendaccount").ToString();
			}
		}

		/// <summary>
		/// 사용가능한 모든 XFAlias를 가져온다.
		/// </summary>
		/// <returns></returns>
		public DataSet GetXfAliasList()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.ph_up_GetXFAlias", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 우편번호 검색 리스트를 가져온다.
		/// </summary>
		/// <param name="searchDong"></param>
		/// <returns></returns>
		public DataSet SearchZipCodeList(string searchDong)
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
		/// 본문 정보를 가져온다.
		/// </summary>
		/// <param name="contextID"></param>
		/// <returns></returns>
		public DataSet GetMessageContext(int contextID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ContextID", SqlDbType.Int, 4, contextID)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetContext", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 공지사항 목록
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetMessagePopupMessageList(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetPopupNotice", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#region 구독관련

		/// <summary>
		/// 구독 신청하기
		/// </summary>
		/// <param name="fdID"></param>
		/// <param name="userID"></param>
		/// <returns>실행결과</returns>
		public string CreateSubscription(int fdID, int userID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@Verification", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SubscriptionRegist", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//regist = pData.GetParamValue("@Verification").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 구독 해지하기
		/// </summary>
		/// <param name="fdID"></param>
		/// <param name="userID"></param>
		public void DeleteSubscription(int fdID, int userID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_SubscriptionDeleteRegist", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 자신의 구독 목록 / 게시판에 대한 권한 가져오기
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetMySubscription(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_SubscriptionGetMine", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 폴더에 대한 구독자 목록 / 권한 가져오기
		/// </summary>
		/// <param name="fdID"></param>
		/// <param name="inUse"></param>
		/// <returns></returns>
		public DataSet GetSubscriber(int fdID, out string inUse)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@InUse", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SubscriptionGetSubscriber", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				inUse = pData.GetParamValue("@InUse").ToString();
			}

			return dsReturn;
		}

		/// <summary>
		/// 자신이 구독을 신청했는지에 대한 여부
		/// </summary>
		/// <param name="fdID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public string VerifySubscription(int fdID, int userID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@Verification", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SubscriptionVerification", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//strReturn = pData.GetParamValue("@Verification").ToString();
			}

			return strReturn;
		}

		#endregion
	}
}
