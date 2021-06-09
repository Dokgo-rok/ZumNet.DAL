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
	public class BaseDac : DacBase
	{
        /// <summary>
        /// 
        /// </summary>
        public BaseDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public BaseDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 게시물 이동
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="moveInfo"></param>
		public void MoveBaseMessage(int userID, string moveInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@move_info", SqlDbType.NText, moveInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseMoveMsg", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 게시물 DB삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns>returnMessageID</returns>
		public int RemoveBaseMessage(string xfAlias, string messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 2000, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.VarChar, 2000, messageID),
				ParamSet.Add4Sql("@RetMessageID", SqlDbType.VarChar, 2000, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseRemoveMsg", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = pData.GetParamValue("@RetMessageID").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 게시물 복구
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <returns>returnMessageID</returns>
		public int RestoreBaseMessage(string xfAlias, int userID, string messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 2000, xfAlias),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.VarChar, 2000, messageID),
				ParamSet.Add4Sql("@RetMessageID", SqlDbType.VarChar, 2000, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseRestoreMsg", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = pData.GetParamValue("@RetMessageID").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 익명 게시물 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		public void DeleteAnonymousMessage(int folderID, string xfAlias, string messageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 2000, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.VarChar, 2000, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonymousRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 게시물 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="userID"></param>
		public void DeleteBoardMessage(int folderID, string xfAlias, string messageID, int userID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 2000, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.VarChar, 2000, messageID),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 링크싸이트 등록 수정
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="creatorID"></param>
		/// <param name="name"></param>
		/// <param name="url"></param>
		/// <param name="sortKey"></param>
		/// <param name="priority"></param>
		/// <param name="inherited"></param>
		public void CreateLinkSiteMessage(int folderID, string xfAlias, int messageID, int parentMessageID, int creatorID, string name, string url, int sortKey, int priority, string inherited)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@parentmsgid", SqlDbType.Int, 4, parentMessageID),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 100, name),
				ParamSet.Add4Sql("@url", SqlDbType.NVarChar, 1000, url),
				ParamSet.Add4Sql("@priority", SqlDbType.Int, 4, priority),
				ParamSet.Add4Sql("@sortkey", SqlDbType.Int, 4, sortKey),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkSiteSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 앨범 등록 / 수정
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDeptName"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="inherited"></param>
		/// <param name="expiredDate"></param>
		/// <param name="imgID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileName"></param>
		/// <param name="savedName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="prefix"></param>
		/// <param name="location"></param>
		public void CreateAlbumMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string creator, int creatorID, int creatorDeptID, string creatorDeptName, string subject, string bodyText, string inherited, string expiredDate, int imgID, string isFile, string fileName, string savedName, string fileSize, string fileType, string prefix, string location)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@parentmsgid", SqlDbType.Int, 4, parentMessageID),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDeptName),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@imgid", SqlDbType.Int, 4, imgID),
				ParamSet.Add4Sql("@isfile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@filename", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@savedname", SqlDbType.NVarChar, 100, savedName),
				ParamSet.Add4Sql("@filesize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@filetype", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@locationfolder", SqlDbType.VarChar, 15, location)
			};

			ParamData pData = new ParamData("admin.ph_up_AlbumSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 게시물 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDeptName"></param>
		/// <param name="messageType"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="bodyText"></param>
		/// <param name="inherited"></param>
		/// <param name="expiredDate"></param>
		/// <param name="popupDate"></param>
		/// <param name="publishDate"></param>
		/// <param name="fileCount"></param>
		/// <param name="fileInfo"></param>
		/// <param name="isHot"></param>
		/// <param name="reserved1"></param>
		/// <param name="isPopup"></param>
		/// <param name="replyMail"></param>
		/// <param name="topLine"></param>
		/// <param name="taskid"></param>
		/// <param name="taskactivity"></param>
		/// <param name="salesstep"></param>
		public void CreateBoardMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string creator, int creatorID, int creatorDeptID, string creatorDeptName, string messageType, string subject, string body, string bodyText, string inherited, string expiredDate, string popupDate, string publishDate, string fileCount, string fileInfo, string isHot, string reserved1, string isPopup, string replyMail, string topLine, string taskid, string taskactivity, int salesstep)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@parentmsgid", SqlDbType.Int, 4, parentMessageID),
				ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDeptName),
				ParamSet.Add4Sql("@MsgType", SqlDbType.NVarChar, 30, messageType),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@Body", SqlDbType.NText, body),
				ParamSet.Add4Sql("@BodyText", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@Inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@ExpiredDate", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@PopUpDate", SqlDbType.VarChar, 10, popupDate),
				ParamSet.Add4Sql("@PublishDate", SqlDbType.VarChar, 10, publishDate),
				ParamSet.Add4Sql("@IsPopup", SqlDbType.Char, 1, isPopup),
				ParamSet.Add4Sql("@ReplyMail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@TopLine", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@AttachFile", SqlDbType.Char, 1, fileCount),
				ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo),
				ParamSet.Add4Sql("@IsHot", SqlDbType.Char, 1, isHot),
				ParamSet.Add4Sql("@Reserved1", SqlDbType.NVarChar, 100, reserved1),
				ParamSet.Add4Sql("@TaskID", SqlDbType.NVarChar, 30, taskid),
				ParamSet.Add4Sql("@TaskActivity", SqlDbType.NVarChar, 30, taskactivity),
				ParamSet.Add4Sql("@SalesStep", SqlDbType.SmallInt, 2, salesstep)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 토론 게시물 삭제
		/// </summary>
		/// <param name="creatorID"></param>
		/// <param name="messageID"></param>
		/// <param name="folderID"></param>
		public void DeleteDiscussMessage(int creatorID, int messageID, int folderID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussSetMsgDelete", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 익명게시물 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="creatorName"></param>
		/// <param name="creatorID"></param>
		/// <param name="password"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="bodyText"></param>
		/// <param name="inherited"></param>
		/// <param name="publishDate"></param>
		/// <param name="Priority"></param>
		/// <param name="isFile"></param>
		/// <param name="topLine"></param>
		/// <param name="fileInfo"></param>
		/// <returns>returnMessageID</returns>
		public int CreateAnonymousMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string creatorName, int creatorID, string password, string subject, string body, string bodyText, string inherited, string publishDate, string Priority, string isFile, string topLine, string fileInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@parentmsgid", SqlDbType.Int, 4, parentMessageID),
				ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 50, creatorName),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@password", SqlDbType.VarChar, 10, password),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@Body", SqlDbType.NText, body),
				ParamSet.Add4Sql("@BodyText", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@Inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@PublishDate", SqlDbType.VarChar, 10, publishDate),
				ParamSet.Add4Sql("@Priority", SqlDbType.Char, 1, Priority),
				ParamSet.Add4Sql("@TopLine", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo),
				ParamSet.Add4Sql("@ReturnMsgID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonymousSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = Convert.ToInt32(pData.GetParamValue("@ReturnMsgID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 토론 게시물 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		public void CreateDiscussMessage(int folderID, int messageID, string xfAlias, string subject, string body, int creatorID, int creatorDeptID, string isFile, string fileInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@body", SqlDbType.NText, body),
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@deptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@attachfileinfo", SqlDbType.NText, fileInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussSetMsgWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 문서 게시물의 등록정보 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="inherited"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="registeredFile"></param>
		/// <param name="subject"></param>
		/// <param name="comment"></param>
		/// <param name="keyword1"></param>
		/// <param name="keyword2"></param>
		/// <param name="newFolderID"></param>
		/// <param name="oldFolderID"></param>
		/// <param name="keepYear"></param>
		/// <param name="isChange"></param>
		/// <param name="fileItem"></param>
		public void ModifyDocProperty(string xfAlias, string inherited, int folderID, int messageID, string registeredFile, string subject, string comment, string keyword1, string keyword2, int newFolderID, int oldFolderID, int keepYear, string isChange, string fileItem)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@RegisteredFile", SqlDbType.Char, 1, registeredFile),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@Comment", SqlDbType.NText, comment),
				ParamSet.Add4Sql("@Keyword1", SqlDbType.NVarChar, 20, keyword1),
				ParamSet.Add4Sql("@Keyword2", SqlDbType.NVarChar, 20, keyword2),
				ParamSet.Add4Sql("@NewFolderID", SqlDbType.Int, 4, newFolderID),
				ParamSet.Add4Sql("@OldFolderID", SqlDbType.Int, 4, oldFolderID),
				ParamSet.Add4Sql("@KeepYear", SqlDbType.SmallInt, 2, keepYear),
				ParamSet.Add4Sql("@IsChange", SqlDbType.Char, 1, isChange),
				ParamSet.Add4Sql("@FileItem", SqlDbType.NText, fileItem)
			};

			ParamData pData = new ParamData("admin.ph_up_DocChangePropertyInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 문서 작성시의 파일 정보 등록
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="creatorID"></param>
		/// <param name="groupID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileName"></param>
		/// <param name="savedName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="prefix"></param>
		/// <param name="location"></param>
		/// <param name="autoDeleted"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		/// <param name="docType"></param>
		/// <param name="doc_number"></param>
		public void CreateDocFile(int domainID, string xfAlias, int messageID, int creatorID, int groupID, string isFile, string fileName, string savedName, string fileSize, string fileType, string prefix, string location, string autoDeleted, int docLevel, int keepYear, int docType, string doc_number)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@GroupID", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@FileName", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@SavedName", SqlDbType.NVarChar, 100, savedName),
				ParamSet.Add4Sql("@FileSize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@FileType", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@Prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@Location", SqlDbType.VarChar, 15, location),
				ParamSet.Add4Sql("@AutoDeleted", SqlDbType.Char, 1, autoDeleted),
				ParamSet.Add4Sql("@DocLevel", SqlDbType.TinyInt, 1, docLevel),
				ParamSet.Add4Sql("@KeepYear", SqlDbType.SmallInt, 2, keepYear),
				ParamSet.Add4Sql("@DocType", SqlDbType.TinyInt, 1, docType),
				ParamSet.Add4Sql("@DocNumber", SqlDbType.NVarChar, 63, doc_number)
			};

			ParamData pData = new ParamData("admin.ph_up_DocSetFileInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 문서 작성
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="inherited"></param>
		/// <param name="registeredFile"></param>
		/// <param name="subject"></param>
		/// <param name="comment"></param>
		/// <param name="keyword1"></param>
		/// <param name="keyword2"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDept"></param>
		/// <param name="taskID"></param>
		/// <param name="taskAcitivity"></param>
		/// <param name="salesStep"></param>
		/// <returns>returnMessageID</returns>
		public int CreateDocMessage(string xfAlias, int folderID, int messageID, string inherited, string registeredFile
			, string subject, string comment, string keyword1, string keyword2, int creatorID, int creatorDeptID, string creatorDept
			, int taskID, string taskAcitivity, int salesStep)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@Inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@RegisteredFile", SqlDbType.Char, 1, registeredFile),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@Comment", SqlDbType.NText, comment),
				ParamSet.Add4Sql("@Keyword1", SqlDbType.NVarChar, 20, keyword1),
				ParamSet.Add4Sql("@Keyword2", SqlDbType.NVarChar, 20, keyword2),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@CreateDeptID", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@CreateDept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@TaskID", SqlDbType.Int, 4, taskID),
				ParamSet.Add4Sql("@TaskActivity", SqlDbType.VarChar, 33, taskAcitivity),
				ParamSet.Add4Sql("@SalesStep", SqlDbType.SmallInt, 2, salesStep),
				ParamSet.Add4Sql("@ReturnMsgID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DocSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = Convert.ToInt32(pData.GetParamValue("@ReturnMsgID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 문서, 지식관리의 게시물 이동
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="oldFolderID"></param>
		/// <param name="newFolderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="attType"></param>
		public void MoveDocMessage(int messageID, int oldFolderID, int newFolderID, string xfAlias, string attType)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@OldFolderID", SqlDbType.Int, 4, oldFolderID),
				ParamSet.Add4Sql("@NewFolderID", SqlDbType.Int, 4, newFolderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@AttType", SqlDbType.Char, 1, attType)
			};

			ParamData pData = new ParamData("admin.ph_up_DocMoveMessage", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 문서, 지식관리의 게시물 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		public void DeleteDocMessage(string xfAlias, int messageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocMsgRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 포탈 저장, 삭제, 제거
		/// </summary>
		/// <param name="actKind"></param>
		/// <param name="domainID"></param>
		/// <param name="opID"></param>
		/// <param name="categoryID"></param>
		/// <param name="opName"></param>
		/// <param name="userID"></param>
		/// <param name="expiredDate"></param>
		/// <param name="xmlData"></param>
		public void HandleOfficePortal(string actKind, int domainID, int opID, int categoryID, string opName, int userID, string expiredDate, string xmlData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@act_kind", SqlDbType.Char, 1, actKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@op_id", SqlDbType.Int, 4, opID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@op_name", SqlDbType.NVarChar, 100, opName),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@expired", SqlDbType.Char, 10, expiredDate),
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_OpSetting", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 제안 작성 / 수정
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="inherited"></param>
		/// <param name="subject"></param>
		/// <param name="messageType"></param>
		/// <param name="expiredDate"></param>
		/// <param name="expectedDate"></param>
		/// <param name="registeredFile"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDept"></param>
		/// <param name="coCreatorID"></param>
		/// <param name="bodyData"></param>
		/// <param name="bodyText"></param>
		/// <param name="relationCategoryID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		/// <param name="hasprocess"></param>
		/// <param name="hasLinkedDoc"></param>
		/// <param name="linkedDocInfo"></param>
		/// <returns>returnMessageID</returns>
		public int SetSuggestMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string inherited, string subject
							, string messageType, string expiredDate, string expectedDate, string registeredFile, int creatorID, int creatorDeptID
							, string creatorDept, int coCreatorID, string bodyData, string bodyText, int relationCategoryID
							, string isFile, string fileInfo, int hasprocess, string hasLinkedDoc, string linkedDocInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@Parentmsgid", SqlDbType.Int, 4, parentMessageID),
				ParamSet.Add4Sql("@Inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@MsgType", SqlDbType.NVarChar, 30, messageType),
				ParamSet.Add4Sql("@ExpiredDate", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@ExpectedDate", SqlDbType.VarChar, 10, expectedDate),
				ParamSet.Add4Sql("@RegisteredFile", SqlDbType.Char, 1, registeredFile),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@CreateDeptID", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@CreateDept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@CoCreateGR", SqlDbType.Int, 4, coCreatorID),
				ParamSet.Add4Sql("@Body", SqlDbType.NText, bodyData),
				ParamSet.Add4Sql("@BodyText", SqlDbType.NVarChar, 100, bodyText),
				ParamSet.Add4Sql("@RelationCategoryID", SqlDbType.Int, 4, relationCategoryID),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo),
				ParamSet.Add4Sql("@hasprocess", SqlDbType.Int, 4, hasprocess),
				ParamSet.Add4Sql("@hasLinkedDoc", SqlDbType.Char, 1, hasLinkedDoc),
				ParamSet.Add4Sql("@LinkedDocInfo", SqlDbType.NText, linkedDocInfo),
				ParamSet.Add4Sql("@ReturnMsgID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SuggestSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = Convert.ToInt32(pData.GetParamValue("@ReturnMsgID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 질의 작성
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="inherited"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="messageType"></param>
		/// <param name="topLine"></param>
		/// <param name="expiredDate"></param>
		/// <param name="expectedDate"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDept"></param>
		/// <param name="replyMail"></param>
		/// <param name="bodyData"></param>
		/// <param name="bodyText"></param>
		/// <param name="relationCategoryID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		/// <param name="hasprocess"></param>
		/// <param name="hasLinkedDoc"></param>
		/// <param name="linkedDocInfo"></param>
		/// <returns></returns>
		public int SetQnAMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string inherited, string priority
						, string subject, string messageType, string topLine, string expiredDate, string expectedDate, string hasAttachFile
						, string creator, int creatorID, int creatorDeptID, string creatorDept, string replyMail, string bodyData, string bodyText
						, int relationCategoryID, string isFile, string fileInfo, int hasprocess, string hasLinkedDoc, string linkedDocInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@Parentmsgid", SqlDbType.Int, 4, parentMessageID),
				ParamSet.Add4Sql("@Inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@Priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@MsgType", SqlDbType.NVarChar, 30, messageType),
				ParamSet.Add4Sql("@TopLine", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@ExpiredDate", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@ExpectedDate", SqlDbType.VarChar, 10, expectedDate),
				ParamSet.Add4Sql("@HasAttachFile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 50, creator),
				ParamSet.Add4Sql("@CreateDeptID", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@CreateDept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@ReplyMail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@Body", SqlDbType.NText, bodyData),
				ParamSet.Add4Sql("@BodyText", SqlDbType.NVarChar, 100, bodyText),
				ParamSet.Add4Sql("@RelationCategoryID", SqlDbType.Int, 4, relationCategoryID),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo),
				ParamSet.Add4Sql("@hasprocess", SqlDbType.Int, 4, hasprocess),
				ParamSet.Add4Sql("@hasLinkedDoc", SqlDbType.Char, 1, hasLinkedDoc),
				ParamSet.Add4Sql("@LinkedDocInfo", SqlDbType.NText, linkedDocInfo),
				ParamSet.Add4Sql("@ReturnMsgID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_QnASetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = Convert.ToInt32(pData.GetParamValue("@ReturnMsgID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 일정 작성
		/// </summary>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="scheduleType"></param>
		/// <param name="taskID"></param>
		/// <param name="inherited"></param>
		/// <param name="subject"></param>
		/// <param name="bodyData"></param>
		/// <param name="location"></param>
		/// <param name="state"></param>
		/// <param name="priority"></param>
		/// <param name="periodFrom"></param>
		/// <param name="startTime"></param>
		/// <param name="periodTo"></param>
		/// <param name="endTime"></param>
		/// <param name="term"></param>
		/// <param name="repeatType"></param>
		/// <param name="alarm"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptName"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		/// <param name="strTaskActivity"></param>
		/// <returns>returnMessageID</returns>
		public int CreateScheduleMain(string objectType, int objectID, string scheduleType, int taskID
				, string inherited, string subject, string bodyData, string location, int state, string priority, string periodFrom
				, string startTime, string periodTo, string endTime, int term, string repeatType, string alarm, int creatorID
				, string creatorDeptName, int creatorDeptID, string isFile, string fileInfo, string strTaskActivity)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@schType", SqlDbType.Char, 2, scheduleType),
				ParamSet.Add4Sql("@taskid", SqlDbType.Int, 4, taskID),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@body", SqlDbType.NText, bodyData),
				ParamSet.Add4Sql("@location", SqlDbType.NVarChar, 100, location),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@periodFrom", SqlDbType.Char, 10, periodFrom),
				ParamSet.Add4Sql("@startTime", SqlDbType.Char, 5, startTime),
				ParamSet.Add4Sql("@periodTo", SqlDbType.Char, 10, periodTo),
				ParamSet.Add4Sql("@endTime", SqlDbType.Char, 5, endTime),
				ParamSet.Add4Sql("@term", SqlDbType.Int, 4, term),
				ParamSet.Add4Sql("@repeatType", SqlDbType.Char, 1, repeatType),
				ParamSet.Add4Sql("@alarm", SqlDbType.VarChar, 5, alarm),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDeptName),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo),
				ParamSet.Add4Sql("@taskActivity", SqlDbType.NVarChar, 30, strTaskActivity),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleCreateMain", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//returnMessageID = Convert.ToInt32(pData.GetParamValue("@msgid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 일정 삭제 - 원본이면 삭제일 기록, 아니면 인스턴스 삭제
		/// </summary>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="messageID"></param>
		/// <param name="attType"></param>
		public void DeleteSchedule(string objectType, int objectID, int messageID, string attType)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleDelete", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 일정 내용 변경 (내용, 일정 시간, 첨부)
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="actor"></param>
		/// <param name="scheduleType"></param>
		/// <param name="inherited"></param>
		/// <param name="subject"></param>
		/// <param name="bodyData"></param>
		/// <param name="location"></param>
		/// <param name="priority"></param>
		/// <param name="periodFrom"></param>
		/// <param name="startTime"></param>
		/// <param name="periodTo"></param>
		/// <param name="endTime"></param>
		/// <param name="term"></param>
		/// <param name="alarm"></param>
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		public void ModifyScheduleContents(int messageID, int actor, string scheduleType, string inherited, string subject
								, string bodyData, string location, string priority, string periodFrom, string startTime
								, string periodTo, string endTime, int term, string alarm, string isFile, string fileInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actor),
				ParamSet.Add4Sql("@schType", SqlDbType.Char, 2, scheduleType),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@body", SqlDbType.NText, bodyData),
				ParamSet.Add4Sql("@location", SqlDbType.NVarChar, 100, location),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@periodFrom", SqlDbType.Char, 10, periodFrom),
				ParamSet.Add4Sql("@startTime", SqlDbType.Char, 5, startTime),
				ParamSet.Add4Sql("@periodTo", SqlDbType.Char, 10, periodTo),
				ParamSet.Add4Sql("@endTime", SqlDbType.Char, 5, endTime),
				ParamSet.Add4Sql("@term", SqlDbType.Int, 4, term),
				ParamSet.Add4Sql("@alarm", SqlDbType.VarChar, 5, alarm),
				ParamSet.Add4Sql("@IsFile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@FileInfo", SqlDbType.NText, fileInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleModifyContents", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 분류 생성
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="mode"></param>
		/// <param name="classCode"></param>
		/// <param name="className"></param>
		/// <param name="subClass"></param>
		/// <param name="items"></param>
		public void CreatePollClassMessage(int msgID, string mode, int classCode, string className, string subClass, string items)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgID", SqlDbType.Int, 4, msgID),
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@classCode", SqlDbType.Int, 4, classCode),
				ParamSet.Add4Sql("@className", SqlDbType.VarChar, 100, className),
				ParamSet.Add4Sql("@subClass", SqlDbType.VarChar, 100, subClass),
				ParamSet.Add4Sql("@items", SqlDbType.NVarChar, 1000, items)
			};

			ParamData pData = new ParamData("admin.ph_up_PollClassCreate", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 작성
		/// </summary>
		/// <param name="xmlData"></param>
		public void CreatePollMessage(string xmlData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_PollCreate", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 삭제
		/// </summary>
		/// <param name="msgID"></param>
		public void DeletePollMessage(string msgID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 2000, msgID)
			};

			ParamData pData = new ParamData("admin.ph_up_PollDelete", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 수정
		/// </summary>
		/// <param name="xmlData"></param>
		/// <returns>ReturnMessageID</returns>
		public int ModifyPollMessage(string xmlData)
		{
			int iReturn = 0;

            SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_PollEdit", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//ReturnMessageID = Convert.ToInt32(pData.GetParamValue("@msgid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 설문 참여 대상자 등록, 및 등록된 대상자 삭제
		/// </summary>
		/// <param name="dnid"></param>
		/// <param name="mode"></param>
		/// <param name="messageID"></param>
		/// <param name="participant"></param>
		public void SetPollParticipant(int dnid, string mode, int messageID, string participant)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, dnid),
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 3, mode),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@participant", SqlDbType.NVarChar, 4000, participant)
			};

			ParamData pData = new ParamData("admin.ph_up_PollParticipantRegistAndDelete", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 기간연장
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="fdID"></param>
		/// <param name="periodTo"></param>
		public void ChangePollPeriodToDate(int messageID, int fdID, string periodTo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@periodto", SqlDbType.VarChar, 10, periodTo)
			};

			ParamData pData = new ParamData("admin.ph_up_PollExtensionPeriodTo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 참여
		/// </summary>
		/// <param name="xmlData"></param>
		public void CreatePollParticipant(string xmlData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_PollParticipate", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 설문 그룹화 수정/삭제
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="mode"></param>
		/// <param name="items"></param>
		/// <param name="className"></param>
		/// <param name="subClass"></param>
		/// <param name="classCode"></param>
		public void ModifyPollClassMessage(int msgID, int mode, string items, string className, string subClass, int classCode)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgID", SqlDbType.Int, 4, msgID),
				ParamSet.Add4Sql("@mode", SqlDbType.Int, 4, mode),
				ParamSet.Add4Sql("@items", SqlDbType.NVarChar, 1000, items),
				ParamSet.Add4Sql("@className", SqlDbType.VarChar, 100, className),
				ParamSet.Add4Sql("@subClass", SqlDbType.VarChar, 100, subClass),
				ParamSet.Add4Sql("@classCode", SqlDbType.Int, 4, classCode)
			};

			ParamData pData = new ParamData("admin.ph_up_PollClassEdit", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 도서 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xFAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="Inherited"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorName"></param>
		/// <param name="managedID"></param>
		/// <param name="managedName"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="price"></param>
		/// <param name="hasBookImg"></param>
		/// <param name="fileName"></param>
		/// <param name="savedName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="prefix"></param>
		public void CreateLibraryMessage(int folderID, string xFAlias, int messageID, string Inherited, int creatorID, string creatorName
								, int managedID, string managedName, string subject, string bodyText, string author, string publisher
								, int price, string hasBookImg, string fileName, string savedName, string fileSize, string fileType, string prefix)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xFAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@Inherited", SqlDbType.Char, 1, Inherited),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@CreatorName", SqlDbType.NVarChar, 100, creatorName),
				ParamSet.Add4Sql("@ManagedID", SqlDbType.Int, 4, managedID),
				ParamSet.Add4Sql("@ManagedName", SqlDbType.NVarChar, 200, managedName),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@BodyText", SqlDbType.NText, bodyText),
				ParamSet.Add4Sql("@Author", SqlDbType.NVarChar, 200, author),
				ParamSet.Add4Sql("@Publisher", SqlDbType.NVarChar, 200, publisher),
				ParamSet.Add4Sql("@Price", SqlDbType.Int, 4, price),
				ParamSet.Add4Sql("@HasBookImg", SqlDbType.Char, 1, hasBookImg),
				ParamSet.Add4Sql("@FileName", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@SavedName", SqlDbType.VarChar, 100, savedName),
				ParamSet.Add4Sql("@FileSize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@FileType", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@Prefix", SqlDbType.VarChar, 15, prefix)
			};

			ParamData pData = new ParamData("admin.ph_up_BookSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// OP 저장
		/// </summary>
		/// <param name="oldOpID"></param>
		/// <param name="categoryID"></param>
		/// <param name="displayName"></param>
		/// <param name="userID"></param>
		/// <param name="xmlData"></param>
		public void CreateOfficePortal(int oldOpID, int categoryID, string displayName, int userID, string xmlData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@OLD_OP_ID", SqlDbType.Int, 4, oldOpID),
				ParamSet.Add4Sql("@CT_ID", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@DisplayName", SqlDbType.NText, displayName),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_OpSave", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		///  H_XFORM_FD  State 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="state"></param>
		/// <param name="taskActivity"></param>
		public void ModifyProcessXFormState(string xfAlias, int messageID, int state, string taskActivity)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@state", SqlDbType.Int, 4, state),
				ParamSet.Add4Sql("@taskactivity", SqlDbType.VarChar, 33, taskActivity)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessXFormState", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// Category 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="folderID"></param>
		/// <param name="attType"></param>
		public void ModifyProcessXFormCategory(string xfAlias, int messageID, int folderID, string attType)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessModifyXFormCategory", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 명함 회사정보 등록/수정
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="clientID"></param>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="clientAlias"></param>
		/// <param name="shortName"></param>
		/// <param name="comName"></param>
		/// <param name="locale"></param>
		/// <param name="comZipCode"></param>
		/// <param name="comAddr"></param>
		/// <param name="comDetailAddr"></param>
		/// <param name="comPhone"></param>
		/// <param name="comFax"></param>
		/// <param name="homePage"></param>
		/// <param name="ceo"></param>
		/// <param name="ceoPhone"></param>
		/// <param name="live"></param>
		/// <param name="comDesc"></param>
		/// <param name="comInherited"></param>
		/// <param name="comCreator"></param>
		/// <param name="comReserved1"></param>
		/// <returns>ClientID</returns>
		public int SetAddressClient(string actionKind, int clientID, int domainID, int folderID, string clientAlias, string shortName
						, string comName, string locale, string comZipCode, string comAddr, string comDetailAddr, string comPhone, string comFax
						, string homePage, string ceo, string ceoPhone, string live, string comDesc, string comInherited, int comCreator, string comReserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@command", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@clientid", SqlDbType.Int, 4, clientID),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@clientalias", SqlDbType.VarChar, 63, clientAlias),
				ParamSet.Add4Sql("@shortname", SqlDbType.NVarChar, 100, shortName),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, comName),
				ParamSet.Add4Sql("@locale", SqlDbType.VarChar, 5, locale),
				ParamSet.Add4Sql("@zipcode", SqlDbType.VarChar, 10, comZipCode),
				ParamSet.Add4Sql("@address", SqlDbType.NVarChar, 255, comAddr),
				ParamSet.Add4Sql("@detailaddr", SqlDbType.NVarChar, 255, comDetailAddr),
				ParamSet.Add4Sql("@phone", SqlDbType.VarChar, 30, comPhone),
				ParamSet.Add4Sql("@fax", SqlDbType.VarChar, 30, comFax),
				ParamSet.Add4Sql("@homepage", SqlDbType.NVarChar, 255, homePage),
				ParamSet.Add4Sql("@ceo", SqlDbType.NVarChar, 63, ceo),
				ParamSet.Add4Sql("@ceo_phone", SqlDbType.VarChar, 30, ceoPhone),
				ParamSet.Add4Sql("@live", SqlDbType.Char, 1, live),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, comDesc),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, comInherited),
				ParamSet.Add4Sql("@creator", SqlDbType.Int, 4, comCreator),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, comReserved1),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressSetAddrClient", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//outClientID = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 명함 등록/수정
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="cardID"></param>
		/// <param name="domainID"></param>
		/// <param name="clientID"></param>
		/// <param name="cardName"></param>
		/// <param name="department"></param>
		/// <param name="grade"></param>
		/// <param name="scope"></param>
		/// <param name="cardClass"></param>
		/// <param name="mobile"></param>
		/// <param name="officePhone"></param>
		/// <param name="officeFax"></param>
		/// <param name="homePhone"></param>
		/// <param name="mail"></param>
		/// <param name="homeZipCode"></param>
		/// <param name="homeAddr"></param>
		/// <param name="homeDetailAddr"></param>
		/// <param name="cardDescription"></param>
		/// <param name="cardInherited"></param>
		/// <param name="cardCreator"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="cardReserved1"></param>
		/// <returns>CardID</returns>
		public int SetAddressCard(string actionKind, int cardID, int domainID, int clientID, string cardName, string department
							, string grade, string scope, string cardClass, string mobile, string officePhone, string officeFax
							, string homePhone, string mail, string homeZipCode, string homeAddr, string homeDetailAddr
							, string cardDescription, string cardInherited, int cardCreator, int creatorDeptID, string cardReserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@command", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@cardid", SqlDbType.Int, 4, cardID),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@clientid", SqlDbType.Int, 4, clientID),
				ParamSet.Add4Sql("@name", SqlDbType.NVarChar, 63, cardName),
				ParamSet.Add4Sql("@department", SqlDbType.NVarChar, 63, department),
				ParamSet.Add4Sql("@grade", SqlDbType.NVarChar, 63, grade),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@class", SqlDbType.NVarChar, 500, cardClass),
				ParamSet.Add4Sql("@mobile", SqlDbType.VarChar, 30, mobile),
				ParamSet.Add4Sql("@phone", SqlDbType.VarChar, 30, officePhone),
				ParamSet.Add4Sql("@fax", SqlDbType.VarChar, 30, officeFax),
				ParamSet.Add4Sql("@home_tel", SqlDbType.VarChar, 30, homePhone),
				ParamSet.Add4Sql("@mail", SqlDbType.NVarChar, 63, mail),
				ParamSet.Add4Sql("@zipcode", SqlDbType.VarChar, 10, homeZipCode),
				ParamSet.Add4Sql("@address", SqlDbType.NVarChar, 63, homeAddr),
				ParamSet.Add4Sql("@detailaddr", SqlDbType.NVarChar, 63, homeDetailAddr),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, cardDescription),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, cardInherited),
				ParamSet.Add4Sql("@creator", SqlDbType.Int, 4, cardCreator),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 100, cardReserved1),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressSetAddrCard", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//outCardID = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 명함 삭제
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="cardID"></param>
		/// <param name="clientID"></param>
		public void DeleteAddress(string actionKind, string cardID, string clientID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@cardID", SqlDbType.VarChar, 2000, cardID),
				ParamSet.Add4Sql("@clientID", SqlDbType.VarChar, 2000, clientID)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressDeleteAddress", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 관련문서(규정,메뉴얼) 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		public void DeleteLinkedDoc(string xfAlias, int messageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkedDocRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 관리툴에서의 문서 등록정보 수정
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="categoryID"></param>
		/// <param name="messageID"></param>
		/// <param name="creatorID"></param>
		/// <param name="deptID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="hasFileCount"></param>
		/// <param name="hasNewFile"></param>
		/// <param name="subject"></param>
		/// <param name="deleteDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="fileInfo"></param>
		public void ChangeBaseMessage(int domainID, int folderID, int categoryID, int messageID, int creatorID, int deptID, string xfAlias
						, string hasFileCount, string hasNewFile, string subject, string deleteDate, string expiredDate, string fileInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@categoryID", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@creatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@deptID", SqlDbType.Int, 4, deptID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@hasfilecount", SqlDbType.Char, 1, hasFileCount),
				ParamSet.Add4Sql("@hasnewfile", SqlDbType.Char, 1, hasNewFile),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@deletedate", SqlDbType.VarChar, 10, deleteDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NText, fileInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeMessage", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 관리툴에서의 본문 정보 수정
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="bodyInfo"></param>
		public void ChangeBaseMessageBody(int domainID, int folderID, int messageID, string xfAlias, string bodyInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@bodyinfo", SqlDbType.NText, bodyInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeMessageBody", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 제안의 시행일자 등록
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="applyDate"></param>
		public void SetSuggestApplyDate(int messageID, string applyDate)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@ApplyDate", SqlDbType.VarChar, 10, applyDate)
			};

			ParamData pData = new ParamData("admin.ph_up_SuggestSetApplyDate", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 프로세스 인스턴스 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		public void DeleteProcessInstance(string xfAlias, int messageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessInstanceRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 양식정보 쿼리
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="ctID"></param>
		/// <param name="fdID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SearchKMDocInfo(int dnID, int ctID, int fdID, int userID, int messageID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, ctID),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SearchKMDOCInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 최초 PH_XF_KNOWLEDGE 테이블 값 저장
		/// </summary>
		/// <param name="msgType"></param>
		/// <param name="inherited"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorCN"></param>
		/// <param name="creatorGrade"></param>
		/// <param name="creatorDept"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDeptCode"></param>
		/// <param name="coRegistrant"></param>
		/// <param name="createDate"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="docNumber"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		/// <returns>oid</returns>
		public int InsertXFKnowledge(string msgType, string inherited, string priority, string subject, string bodyText, string creator, int creatorID
								, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID, string creatorDeptCode, string coRegistrant
								, string createDate, string publishDate, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
								, string replyMail, string docNumber, string docLevel, string keepYear, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatorcn", SqlDbType.VarChar, 30, creatorCN),
				ParamSet.Add4Sql("@creatorgrade", SqlDbType.NVarChar, 50, creatorGrade),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@creatordeptcode", SqlDbType.VarChar, 30, creatorDeptCode),
				ParamSet.Add4Sql("@coregistrant", SqlDbType.NVarChar, 1000, coRegistrant),
				ParamSet.Add4Sql("@createdate", SqlDbType.VarChar, 20, createDate),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, docNumber),
				ParamSet.Add4Sql("@doclevel", SqlDbType.VarChar, 5, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.VarChar, 5, keepYear),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFKnowledge", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// PH_XF_KNOWLEDGE 테이블 값 변경(임저장함에서 수정저장)
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="msgType"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="coRegistrant"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="docNumber"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		public void UpdateXFKnowledge(int messageID, string msgType, string priority, string subject, string bodyText, string coRegistrant
							, string publishDate, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
							, string replyMail, string docNumber, string docLevel, string keepYear, string reserved1, string reserved2)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@coregistrant", SqlDbType.NVarChar, 1000, coRegistrant),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, docNumber),
				ParamSet.Add4Sql("@doclevel", SqlDbType.VarChar, 5, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.VarChar, 5, keepYear),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFKnowledge", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XF_SUGGEST 테이블 값 저장
		/// </summary>
		/// <param name="msgType"></param>
		/// <param name="inherited"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorCN"></param>
		/// <param name="creatorGrade"></param>
		/// <param name="creatorDept"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDeptCode"></param>
		/// <param name="coRegistrant"></param>
		/// <param name="createDate"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="docNumber"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		/// <returns>oid</returns>
		public int InsertXFSuggest(string msgType, string inherited, string priority, string subject, string bodyText, string creator, int creatorID
							, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID, string creatorDeptCode, string coRegistrant
							, string createDate, string publishDate, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
							, string replyMail, string docNumber, string docLevel, string keepYear, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatorcn", SqlDbType.VarChar, 30, creatorCN),
				ParamSet.Add4Sql("@creatorgrade", SqlDbType.NVarChar, 50, creatorGrade),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@creatordeptcode", SqlDbType.VarChar, 30, creatorDeptCode),
				ParamSet.Add4Sql("@coregistrant", SqlDbType.NVarChar, 1000, coRegistrant),
				ParamSet.Add4Sql("@createdate", SqlDbType.VarChar, 20, createDate),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, docNumber),
				ParamSet.Add4Sql("@doclevel", SqlDbType.VarChar, 5, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.VarChar, 5, keepYear),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFSuggest", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//oid = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// PH_XF_SUGGEST 테이블 값 변경(임저장함에서 수정저장)
		/// </summary>
		public void UpdateXFSuggest(int messageID, string msgType, string priority, string subject, string bodyText, string coRegistrant
							, string publishDate, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
							, string replyMail, string docNumber, string docLevel, string keepYear, string reserved1, string reserved2)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@coregistrant", SqlDbType.NVarChar, 1000, coRegistrant),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, docNumber),
				ParamSet.Add4Sql("@doclevel", SqlDbType.VarChar, 5, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.VarChar, 5, keepYear),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFSuggest", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XF_INFORMATION 테이블 값 저장
		/// </summary>
		/// <param name="msgType"></param>
		/// <param name="inherited"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorCN"></param>
		/// <param name="creatorGrade"></param>
		/// <param name="creatorDept"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDeptCode"></param>
		/// <param name="coRegistrant"></param>
		/// <param name="createDate"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="infoGetDate"></param>
		/// <param name="infoGetRout"></param>
		/// <param name="infoSource"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="isPublic"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="docNumber"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		/// <returns></returns>
		public int InsertXFInformation(string msgType, string inherited, string priority, string subject, string bodyText, string creator, int creatorID
								, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID, string creatorDeptCode, string coRegistrant
								, string createDate, string publishDate, string expiredDate, string infoGetDate, string infoGetRout, string infoSource
								, string hasAttachFile, string isPublic, string linkedMsg, string topLine, string replyMail, string docNumber, string docLevel
								, string keepYear, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatorcn", SqlDbType.VarChar, 30, creatorCN),
				ParamSet.Add4Sql("@creatorgrade", SqlDbType.NVarChar, 50, creatorGrade),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@creatordeptcode", SqlDbType.VarChar, 30, creatorDeptCode),
				ParamSet.Add4Sql("@coregistrant", SqlDbType.NVarChar, 1000, coRegistrant),
				ParamSet.Add4Sql("@createdate", SqlDbType.VarChar, 20, createDate),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@infogetdate", SqlDbType.VarChar, 10, infoGetDate),
				ParamSet.Add4Sql("@infogetroute", SqlDbType.NVarChar, 100, infoGetRout),
				ParamSet.Add4Sql("@infosource", SqlDbType.NVarChar, 100, infoSource),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@ispublic", SqlDbType.Char, 1, isPublic),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, docNumber),
				ParamSet.Add4Sql("@doclevel", SqlDbType.VarChar, 5, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.VarChar, 5, keepYear),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFInformation", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//oid = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// PH_XF_INFORMATION 테이블 값 변경(임저장함에서 수정저장)
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="msgType"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="coRegistrant"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="infoGetDate"></param>
		/// <param name="infoGetRout"></param>
		/// <param name="infoSource"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="isPublic"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="docNumber"></param>
		/// <param name="docLevel"></param>
		/// <param name="keepYear"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		public void UpdateXFInformation(int messageID, string msgType, string priority, string subject, string bodyText, string coRegistrant
								, string publishDate, string expiredDate, string infoGetDate, string infoGetRout, string infoSource
								, string hasAttachFile, string isPublic, string linkedMsg, string topLine, string replyMail, string docNumber
								, string docLevel, string keepYear, string reserved1, string reserved2)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@coregistrant", SqlDbType.NVarChar, 1000, coRegistrant),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@infogetdate", SqlDbType.VarChar, 10, infoGetDate),
				ParamSet.Add4Sql("@infogetroute", SqlDbType.NVarChar, 100, infoGetRout),
				ParamSet.Add4Sql("@infosource", SqlDbType.NVarChar, 100, infoSource),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@ispublic", SqlDbType.Char, 1, isPublic),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@docnumber", SqlDbType.NVarChar, 63, docNumber),
				ParamSet.Add4Sql("@doclevel", SqlDbType.VarChar, 5, docLevel),
				ParamSet.Add4Sql("@keepyear", SqlDbType.VarChar, 5, keepYear),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFInformation", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XF_QNA 테이블 값 저장
		/// </summary>
		/// <param name="msgType"></param>
		/// <param name="inherited"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorCN"></param>
		/// <param name="creatorGrade"></param>
		/// <param name="creatorDept"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDeptCode"></param>
		/// <param name="createDate"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		/// <returns>oid</returns>
		public int InsertXFQnA(string msgType, string inherited, string priority, string subject, string bodyText, string creator
						, int creatorID, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID, string creatorDeptCode
						, string createDate, string publishDate, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
						, string replyMail, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 100, creator),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@creatorcn", SqlDbType.VarChar, 30, creatorCN),
				ParamSet.Add4Sql("@creatorgrade", SqlDbType.NVarChar, 50, creatorGrade),
				ParamSet.Add4Sql("@creatordept", SqlDbType.NVarChar, 200, creatorDept),
				ParamSet.Add4Sql("@creatordeptid", SqlDbType.Int, 4, creatorDeptID),
				ParamSet.Add4Sql("@creatordeptcode", SqlDbType.VarChar, 30, creatorDeptCode),
				ParamSet.Add4Sql("@createdate", SqlDbType.VarChar, 20, createDate),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFQnA", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//oid = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XF_QNA 테이블 값 변경
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="msgType"></param>
		/// <param name="priority"></param>
		/// <param name="subject"></param>
		/// <param name="bodyText"></param>
		/// <param name="publishDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="hasAttachFile"></param>
		/// <param name="linkedMsg"></param>
		/// <param name="topLine"></param>
		/// <param name="replyMail"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		public void UpdateXFQnA(int messageID, string msgType, string priority, string subject, string bodyText, string publishDate, string expiredDate
							, string hasAttachFile, string linkedMsg, string topLine, string replyMail, string reserved1, string reserved2)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@msgtype", SqlDbType.NVarChar, 30, msgType),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@bodytext", SqlDbType.NVarChar, 1000, bodyText),
				ParamSet.Add4Sql("@publishdate", SqlDbType.VarChar, 20, publishDate),
				ParamSet.Add4Sql("@expireddate", SqlDbType.VarChar, 20, expiredDate),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasAttachFile),
				ParamSet.Add4Sql("@linkedmsg", SqlDbType.Char, 1, linkedMsg),
				ParamSet.Add4Sql("@topline", SqlDbType.Char, 1, topLine),
				ParamSet.Add4Sql("@replymail", SqlDbType.Char, 1, replyMail),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 500, reserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFQnA", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XFORM_CONTEXT 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="body"></param>
		/// <param name="drm"></param>
		/// <param name="reserved1"></param>
		/// <returns></returns>
		public int InsertXFormContext(string xfAlias, int messageID, string body, string drm, string reserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@body", SqlDbType.NText, body),
				ParamSet.Add4Sql("@drm", SqlDbType.Char, 1, drm),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormContext", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//oid = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XFORM_CONTEXT 테이블 값 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="body"></param>
		/// <param name="drm"></param>
		/// <param name="reserved1"></param>
		public void UpdateXFormContext(string xfAlias, int messageID, string body, string drm, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@body", SqlDbType.NText, body),
				ParamSet.Add4Sql("@drm", SqlDbType.Char, 1, drm),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFormContext", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_FILE_ATTACH 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileName"></param>
		/// <param name="savedName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="prefix"></param>
		/// <param name="locationFolder"></param>
		/// <param name="drm"></param>
		/// <returns>oid</returns>
		public int InsertFileAttach(string xfAlias, int messageID, string isFile, string fileName, string savedName
								, string fileSize, string fileType, string prefix, string locationFolder, string drm)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@isfile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@filename   ", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@savedname", SqlDbType.NVarChar, 100, savedName),
				ParamSet.Add4Sql("@filesize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@filetype", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@locationfolder", SqlDbType.VarChar, 15, locationFolder),
				ParamSet.Add4Sql("@drm", SqlDbType.VarChar, 7, drm),
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertFileAttach", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//oid = Convert.ToInt32(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_FILE_ATTACH 테이블 값 변경(DRM 정보만 변경)
		/// </summary>
		/// <param name="attachId"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="drm"></param>
		public void UpdateFileAttach(int attachId, string xfAlias, int messageID, string drm)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@attachid", SqlDbType.Int, 4, attachId),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@drm", SqlDbType.VarChar, 7, drm)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateFileAttach", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_FILE_ATTACH 테이블 값 배치로 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="fileInfo"></param>
		public void InsertFileAttach(string xfAlias, int messageID, string fileInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NText, fileInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertFileAttachBatch", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XFORM_FD 테이블 값 저장
		/// </summary>
		/// <param name="fdID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMsgID"></param>
		/// <param name="attType"></param>
		/// <param name="state"></param>
		/// <param name="taskActivity"></param>
		/// <param name="reserved1"></param>
		public void InsertXFormFD(int fdID, string xfAlias, string messageID, int parentMsgID, string attType, Int16 state, string taskActivity, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@parentmsgid", SqlDbType.Int, 4, parentMsgID),
				ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
				ParamSet.Add4Sql("@taskactivity", SqlDbType.VarChar, 33, taskActivity),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormFD", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 수정 PH_XFORM_FD 테이블 값 변경(분류 변경시)
		/// </summary>
		/// <param name="fdID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="attType"></param>
		/// <param name="taskActivity"></param>
		/// <param name="reserved1"></param>
		public void UpdateXFormFD(int fdID, string xfAlias, string messageID, string attType, string taskActivity, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@taskactivity", SqlDbType.VarChar, 33, taskActivity),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFormFD", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XFORM_TEMPORARY 테이블 값 저장
		/// </summary>
		/// <param name="ctID"></param>
		/// <param name="actor"></param>
		/// <param name="actorID"></param>
		/// <param name="actorCN"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="subject"></param>
		/// <param name="reason"></param>
		/// <param name="reserved1"></param>
		public void InsertXFormTemporary(int ctID, string actor, int actorID, string actorCN, string xfAlias
								, string messageID, string subject, string reason, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, ctID),
				ParamSet.Add4Sql("@actor", SqlDbType.NVarChar, 100, actor),
				ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
				ParamSet.Add4Sql("@actorcn", SqlDbType.VarChar, 30, actorCN),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, 1000, reason),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormTemporary", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 수정 PH_XFORM_TEMPORARY 테이블 값 변경(임시 저장함)
		/// </summary>
		/// <param name="ctID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="subject"></param>
		/// <param name="reason"></param>
		/// <param name="reserved1"></param>
		public void UpdateXFormTemporary(int ctID, string xfAlias, string messageID, string subject, string reason, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, ctID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@reason", SqlDbType.NVarChar, 1000, reason),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFormTemporary", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XFORM_REGISTRANT 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="actorID"></param>
		/// <param name="actType"></param>
		/// <param name="actor"></param>
		/// <param name="actorCN"></param>
		/// <param name="actorGrade"></param>
		/// <param name="actorDept"></param>
		/// <param name="actorDeptID"></param>
		/// <param name="actorDeptCode"></param>
		/// <param name="reserved1"></param>
		public void InsertXFormRegistrant(string xfAlias, int messageID, int actorID, string actType, string actor, string actorCN
								, string actorGrade, string actorDept, string actorDeptID, string actorDeptCode, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@actorid", SqlDbType.Int, 4, actorID),
				ParamSet.Add4Sql("@acttype", SqlDbType.Char, 1, actType),
				ParamSet.Add4Sql("@actor", SqlDbType.NVarChar, 100, actor),
				ParamSet.Add4Sql("@actorcn", SqlDbType.VarChar, 30, actorCN),
				ParamSet.Add4Sql("@actorgrade", SqlDbType.NVarChar, 50, actorGrade),
				ParamSet.Add4Sql("@actordept", SqlDbType.NVarChar, 200, actorDept),
				ParamSet.Add4Sql("@actordeptid", SqlDbType.Int, 4, actorDeptID),
				ParamSet.Add4Sql("@actordeptcode", SqlDbType.VarChar, 30, actorDeptCode),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormRegistrant", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_XFORM_REGISTRANT 테이블 값 배치로 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="xmlInfo"></param>
		public void InsertXFormRegistrant(string xfAlias, string messageID, string xmlInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormRegistrantBatch", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 admin.PH_XFORM_DL 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="step"></param>
		/// <param name="dl"></param>
		/// <param name="description"></param>
		/// <param name="reserved1"></param>
		public void InsertXFormDL(string xfAlias, string messageID, string step, string dl, string description, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@step", SqlDbType.VarChar, 30, step),
				ParamSet.Add4Sql("@dl", SqlDbType.NText, dl),
				ParamSet.Add4Sql("@description", SqlDbType.NText, description),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormDL", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 수정 admin.PH_XFORM_DL 테이블 값 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="step"></param>
		/// <param name="dl"></param>
		/// <param name="description"></param>
		/// <param name="reserved1"></param>
		public void UpdateXFormDL(string xfAlias, string messageID, string step, string dl, string description, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@step", SqlDbType.VarChar, 30, step),
				ParamSet.Add4Sql("@dl", SqlDbType.NText, dl),
				ParamSet.Add4Sql("@description", SqlDbType.NText, description),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFormDL", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 삭제 admin.PH_XFORM_DL 테이블 값 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="step"></param>
		public void DeleteXFormDL(string xfAlias, string messageID, string step)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@step", SqlDbType.VarChar, 30, step)
			};

			ParamData pData = new ParamData("admin.ph_up_DeleteXFormDL", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 수정 PH_XFORM_REGISTRANT 테이블 값 배치로 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="xmlInfo"></param>
		public void UpdateXFormRegistrant(string xfAlias, string messageID, string xmlInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFormRegistrantBatch", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_LINKED_DOC 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="linkedXFAlias"></param>
		/// <param name="linkedMessageID"></param>
		/// <param name="linkedSubject"></param>
		/// <param name="linkedReserved1"></param>
		/// <param name="linkedReserved2"></param>
		public void InsertLinkedDoc(string xfAlias, string messageID, string linkedXFAlias, string linkedMessageID
								, string linkedSubject, string linkedReserved1, string linkedReserved2)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@linked_xfalias", SqlDbType.VarChar, 30, linkedXFAlias),
				ParamSet.Add4Sql("@linked_messageid", SqlDbType.VarChar, 33, linkedMessageID),
				ParamSet.Add4Sql("@linked_subject", SqlDbType.NVarChar, 200, linkedSubject),
				ParamSet.Add4Sql("@linked_reserved1", SqlDbType.NVarChar, 255, linkedReserved1),
				ParamSet.Add4Sql("@linked_reserved2", SqlDbType.NVarChar, 500, linkedReserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertLinkedDoc", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_LINKED_DOC 테이블 값 배치로 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="xmlInfo"></param>
		public void InsertLinkedDoc(string xfAlias, string messageID, string xmlInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertLinkedDocBatch", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 수정 PH_LINKED_DOC 테이블 값 배치로 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="xmlInfo"></param>
		public void UpdateLinkedDoc(string xfAlias, string messageID, string xmlInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateLinkedDocBatch", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_EVENT_REGISTRATION 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="actorID"></param>
		/// <param name="actType"></param>
		/// <param name="actDate"></param>
		public void InsertEventRegistration(string xfAlias, int messageID, int actorID, string actType, string actDate)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actorID),
				ParamSet.Add4Sql("@acttype", SqlDbType.Char, 1, actType),
				ParamSet.Add4Sql("@actdate", SqlDbType.VarChar, 20, actDate)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertEventRegistration", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 최초 PH_EVENT_EDIT 테이블 값 저장
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="actorID"></param>
		public void InsertEventEdit(string xfAlias, int messageID, int actorID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@editor", SqlDbType.Int, 4, actorID)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertEventEdit", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// XFormMainData 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectXFMainData(string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectXFMainData", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// XFormMain의 본문 정보 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectXFormContext(string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectXFormContext", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 덧글 정보 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectXFormComment(string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_MsgGetCommentList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 답변글 리스트 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectXFormReply(string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectXFormReplyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 첨부파일 쿼리
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectAttachFile(int domainID, string xfAlias, int messageID)
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
		/// 공동 등록자 정보 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectXFormRegistrant(string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectXFormRegistrant", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 관련문서
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectLinkedDoc(string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkedDocGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 배포대상자 목록 정보 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="step"></param>
		/// <param name="dl"></param>
		/// <param name="description"></param>
		/// <param name="reserved1"></param>
		/// <returns></returns>
		public DataSet SelectXFormDL(string xfAlias, string messageID, string step, string dl, string description, string reserved1)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@step", SqlDbType.VarChar, 30, step),
				ParamSet.Add4Sql("@dl", SqlDbType.NText, dl),
				ParamSet.Add4Sql("@description", SqlDbType.NText, description),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectXFormDL", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 분류 정보 쿼리
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet SelectCategoryInfo(int domainID, int categoryID, string xfAlias, int messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ctid", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectXFormCategoryInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 질문/답변 (답변채택으로 질문 완료처리)
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="parentID"></param>
		public void SetQnaSelectAnswer(int messageID, string xfAlias, int parentID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@parentid", SqlDbType.Int, 4, parentID)
			};

			ParamData pData = new ParamData("admin.ph_up_SetQnaSelectAnswer", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 정보 공개여부 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="isPublic"></param>
		public void ChangeXFormIsPublic(string xfAlias, string messageID, string isPublic)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@ispublic", SqlDbType.Char, 1, isPublic)
			};

			ParamData pData = new ParamData("admin.ph_up_ChangeXFormIsPublic", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		#region 지식관리 메인 관련

		/// <summary>
		/// QnaList
		/// </summary>
		/// <param name="strNum"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectQnaListWithoutAnswer(string strNum, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, strNum),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectQnaListWithoutAnswer", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strNum"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectKnowledgeOrderByViewCount(string strNum, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, strNum),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectKnowledgeOrderByViewCount", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strNum"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectKnowledgeRecommendList(string strNum, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, strNum),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectKnowledgeRecommendList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strNum"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectKnowledgeNewList(string strNum, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, strNum),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectKnowledgeNewList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strNum"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectQnaCompleteListAtToday(string strNum, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, strNum),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectQnaCompleteListAtToday", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strNum"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectPointListOrderPoint(string strNum, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.VarChar, 12, strNum),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectPointListOrderPoint", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet SelectPointDuringMonth(string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SelectPointDuringMonth", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#endregion

		#region 지식관리 리스트 관련

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="userID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="type"></param>
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
		public DataSet GetKnowledgeData(int domainID, int folderID, int userID, string xfAlias, string type, string isAdmin, string permission, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@urid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@type", SqlDbType.VarChar, 15, type),
				ParamSet.Add4Sql("@isadmin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@parentacl", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@pageindex", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetBasicKMList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = Convert.ToInt32(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		#endregion

		#region 지식관리 통계 관련
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="isSection"></param>
		/// <param name="isView"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetStatisticsData(int userID, string isSection, string isView, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, isSection),
				ParamSet.Add4Sql("@isview", SqlDbType.VarChar, 10, isView),
				ParamSet.Add4Sql("@startdate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@enddate", SqlDbType.VarChar, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetPerson", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="deptID"></param>
		/// <param name="isSection"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="userTotalPoint"></param>
		/// <param name="userSearchPoint"></param>
		/// <param name="deptTotalPoint"></param>
		/// <param name="deptSearchPoint"></param>
		/// <returns></returns>
		public int GetStatisticsTotalData(int userID, int deptID, string isSection, string startDate, string endDate, out double userTotalPoint, out double userSearchPoint, out double deptTotalPoint, out double deptSearchPoint)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@deptid", SqlDbType.Int, 4, deptID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, isSection),
				ParamSet.Add4Sql("@startdate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@enddate", SqlDbType.VarChar, 10, endDate),
				ParamSet.Add4Sql("@usertotalpoint", SqlDbType.VarChar, 10, ParameterDirection.Output),
				ParamSet.Add4Sql("@usersearchpoint", SqlDbType.VarChar, 10, ParameterDirection.Output),
				ParamSet.Add4Sql("@depttotalpoint", SqlDbType.VarChar, 10, ParameterDirection.Output),
				ParamSet.Add4Sql("@deptsearchpoint", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetPersonTotal", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));

				userTotalPoint = double.Parse(pData.GetParamValue("@usertotalpoint").ToString());
				userSearchPoint = double.Parse(pData.GetParamValue("@usersearchpoint").ToString());
				deptTotalPoint = double.Parse(pData.GetParamValue("@depttotalpoint").ToString());
				deptSearchPoint = double.Parse(pData.GetParamValue("@deptsearchpoint").ToString());
			}

			return iReturn;
		}

		#endregion
	}
}
