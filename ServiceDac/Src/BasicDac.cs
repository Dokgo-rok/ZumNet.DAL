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
	public class BasicDac : DacBase
	{
		/// <summary>
		/// 게시물 이동
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="moveInfo"></param>
		/// <returns></returns>
		public int MoveBaseMessage(int userID, string moveInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@move_info", SqlDbType.NText, moveInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseMoveMsg", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 게시물 DB삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int RemoveBaseMessage(string xfAlias, string messageID, out string returnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = pData.GetParamValue("@RetMessageID").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 게시물 복구
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int RestoreBaseMessage(string xfAlias, int userID, string messageID, out string returnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = pData.GetParamValue("@RetMessageID").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 익명 게시물 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int DeleteAnonymousMessage(int folderID, string xfAlias, string messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 2000, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.VarChar, 2000, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_AnonymousRemove", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 게시물 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public int DeleteBoardMessage(int folderID, string xfAlias, string messageID, int userID)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
		/// <returns></returns>
		public int CreateLinkSiteMessage(int folderID, string xfAlias, int messageID, int parentMessageID, int creatorID, string name, string url, int sortKey, int priority, string inherited)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 앨범 등록 / 수정
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
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
		/// <returns></returns>
		public int CreateAlbumMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string creator, int creatorID, int creatorDeptID, string creatorDeptName, string subject, string bodyText, string inherited, string expiredDate, int imgID, string isFile, string fileName, string savedName, string fileSize, string fileType, string prefix, string location)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 게시물 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
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
		/// <param name="isFile"></param>
		/// <param name="fileInfo"></param>
		/// <param name="isHot"></param>
		/// <param name="reserved1"></param>
		/// <returns></returns>
		public int CreateBoardMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string creator, int creatorID, int creatorDeptID, string creatorDeptName, string messageType, string subject, string body, string bodyText, string inherited, string expiredDate, string popupDate, string publishDate, string fileCount, string fileInfo, string isHot, string reserved1, string isPopup, string replyMail, string topLine, string taskid, string taskactivity, int salesstep)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 토론 게시물 삭제
		/// </summary>
		/// <param name="creatorID"></param>
		/// <param name="messageID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public int DeleteDiscussMessage(int creatorID, int messageID, int folderID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_DiscussSetMsgDelete", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 익명게시물 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="creatorName"></param>
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
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int CreateAnonymousMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string creatorName, int creatorID, string password, string subject, string body, string bodyText, string inherited, string publishDate, string Priority, string isFile, string topLine, string fileInfo, out int returnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = int.Parse(pData.GetParamValue("@ReturnMsgID").ToString());
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
		/// <returns></returns>
		public int CreateDiscussMessage(int folderID, int messageID, string xfAlias, string subject, string body, int creatorID, int creatorDeptID, string isFile, string fileInfo)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 문서 게시물의 등록정보 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="folderId"></param>
		/// <param name="messageID"></param>
		/// <param name="registeredFile"></param>
		/// <param name="subject"></param>
		/// <param name="comment"></param>
		/// <param name="keyword1"></param>
		/// <param name="keyword2"></param>
		/// <param name="oldKnowledgeFolderID"></param>
		/// <param name="newKnowledgeFolderID"></param>
		/// <param name="knowledgeMode"></param>
		/// <param name="knowledgeAlias"></param>
		/// <param name="newFolderID"></param>
		/// <param name="newAlias"></param>
		/// <param name="oldFolderID"></param>
		/// <param name="keepYear"></param>
		/// <param name="isChange"></param>
		/// <param name="fileItem"></param>
		/// <returns></returns>
		public int ModifyDocProperty(string xfAlias, string inherited, int folderID, int messageID, string registeredFile, string subject, string comment, string keyword1, string keyword2, int newFolderID, int oldFolderID, int keepYear, string isChange, string fileItem)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
		/// <returns></returns>
		public int CreateDocFile(int domainID, string xfAlias, int messageID, int creatorID, int groupID, string isFile, string fileName, string savedName, string fileSize, string fileType, string prefix, string location, string autoDeleted, int docLevel, int keepYear, int docType, string doc_number)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 문서 작성
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="parentMessageID"></param>
		/// <param name="inherited"></param>
		/// <param name="registeredFile"></param>
		/// <param name="msgType"></param>
		/// <param name="subject"></param>
		/// <param name="comment"></param>
		/// <param name="expiredDate"></param>
		/// <param name="bodyText"></param>
		/// <param name="keyword1"></param>
		/// <param name="keyword2"></param>
		/// <param name="creatorID"></param>
		/// <param name="creatorDeptID"></param>
		/// <param name="creatorDept"></param>
		/// <param name="coCreatorID"></param>
		/// <param name="linkedMessageExist"></param>
		/// <param name="linkedMessageXfAlias"></param>
		/// <param name="linkedMessageID"></param>
		/// <param name="relationCategoryID"></param>
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int CreateDocMessage(string xfAlias, int folderID, int messageID, string inherited, string registeredFile
			, string subject, string comment, string keyword1, string keyword2, int creatorID, int creatorDeptID, string creatorDept
			, int taskID, string taskAcitivity, int salesStep, out int returnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = int.Parse(pData.GetParamValue("@ReturnMsgID").ToString());
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
		/// <returns></returns>
		public int MoveDocMessage(int messageID, int oldFolderID, int newFolderID, string xfAlias, string attType)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 문서, 지식관리의 게시물 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int DeleteDocMessage(string xfAlias, int messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocMsgRemove", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
		/// <returns></returns>
		public int HandleOfficePortal(string actKind, int domainID, int opID, int categoryID, string opName, int userID, string expiredDate, string xmlData)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
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
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int SetSuggestMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string inherited, string subject, string messageType, string expiredDate, string expectedDate, string registeredFile, int creatorID, int creatorDeptID, string creatorDept, int coCreatorID, string bodyData, string bodyText, int relationCategoryID, string isFile, string fileInfo, int hasprocess, string hasLinkedDoc, string linkedDocInfo, out int returnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = int.Parse(pData.GetParamValue("@ReturnMsgID").ToString());
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
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int SetQnAMessage(int folderID, string xfAlias, int messageID, int parentMessageID, string inherited, string priority, string subject, string messageType, string topLine, string expiredDate, string expectedDate, string hasAttachFile, string creator, int creatorID, int creatorDeptID, string creatorDept, string replyMail, string bodyData, string bodyText, int relationCategoryID, string isFile, string fileInfo, int hasprocess, string hasLinkedDoc, string linkedDocInfo, out int returnMessageID)
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
				ParamSet.Add4Sql("@Creator", SqlDbType.NVarChar, 100, creatorID),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = int.Parse(pData.GetParamValue("@ReturnMsgID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 일정 작성
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
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
		/// <param name="returnMessageID"></param>
		/// <returns></returns>
		public int CreateScheduleMain(string objectType, int objectID, string scheduleType, int taskID
			, string inherited, string subject, string bodyData, string location, int state, string priority, string periodFrom
			, string startTime, string periodTo, string endTime, int term, string repeatType, string alarm, int creatorID
			, string creatorDeptName, int creatorDeptID, string isFile, string fileInfo, string strTaskActivity, out int returnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				returnMessageID = int.Parse(pData.GetParamValue("@msgid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 일정 삭제 - 원본이면 삭제일 기록, 아니면 인스턴스 삭제
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int DeleteSchedule(string objectType, int objectID, int messageID, string attType)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
		/// <returns></returns>
		public int ModifyScheduleContents(int messageID, int actor, string scheduleType, string inherited, string subject, string bodyData, string location, string priority, string periodFrom, string startTime, string periodTo, string endTime, int term, string alarm, string isFile, string fileInfo)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="mode"></param>
		/// <param name="classCode"></param>
		/// <param name="className"></param>
		/// <param name="subClass"></param>
		/// <param name="items"></param>
		/// <returns></returns>
		public int CreatePollClassMessage(int msgID, string mode, int classCode, string className, string subClass, string items)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 설문 작성
		/// </summary>
		/// <param name="xmlData"></param>
		/// <returns></returns>
		public int CreatePollMessage(string xmlData)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_PollCreate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn; 
		}

		/// <summary>
		/// 설문 삭제
		/// </summary>
		/// <param name="msgID"></param>
		/// <returns></returns>
		public int DeletePollMessage(string msgID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 2000, msgID)
			};

			ParamData pData = new ParamData("admin.ph_up_PollDelete", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 설문 수정
		/// </summary>
		/// <param name="xmlData"></param>
		/// <returns></returns>
		public int ModifyPollMessage(string xmlData, out int ReturnMessageID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				ReturnMessageID = int.Parse(pData.GetParamValue("@msgid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 설문 참여 대상자 등록, 및 등록된 대상자 삭제
		/// </summary>
		/// <param name="mode">등록 : 'reg', 삭제 : 'del'</param>
		/// <param name="messageID"></param>
		/// <param name="participant"></param>
		/// <returns></returns>
		public int SetPollParticipant(int dnid, string mode, int messageID, string participant)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		///  설문 기간연장
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="fdID"></param>
		/// <param name="periodDate"></param>
		/// <returns></returns>
		public int ChangePollPeriodToDate(int messageID, int fdID, string periodTo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@fdid", SqlDbType.Int, 4, fdID),
				ParamSet.Add4Sql("@periodto", SqlDbType.VarChar, 10, periodTo)
			};

			ParamData pData = new ParamData("admin.ph_up_PollExtensionPeriodTo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 설문 참여
		/// </summary>
		/// <param name="xmlData"></param>
		/// <returns></returns>
		public int CreatePollParticipant(string xmlData)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xmlData", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_PollParticipate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		///  설문 그룹화 수정/삭제
		/// </summary>
		/// <param name="msgID"></param>
		/// <param name="mode"></param>
		/// <param name="items"></param>
		/// <param name="classCode"></param>
		/// <returns></returns>
		public int ModifyPollClassMessage(int msgID, int mode, string items, string className, string subClass, int classCode)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 도서 등록
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xFAlias"></param>
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
		/// <returns></returns>
		public int CreateLibraryMessage(int folderID, string xFAlias, string Inherited, int creatorID, string creatorName, int managedID, string managedName, string subject, string bodyText, string author, string publisher, int price, string hasBookImg, string fileName, string savedName, string fileSize, string fileType, string prefix, string messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xFAlias),
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
				ParamSet.Add4Sql("@Prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, int.Parse(messageID))
			};

			ParamData pData = new ParamData("admin.ph_up_BookSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// OP 저장
		/// </summary>
		/// <param name="oldOpID"></param>
		/// <param name="categoryID"></param>
		/// <param name="displayName"></param>
		/// <param name="userID"></param>
		/// <param name="xmlData"></param>
		/// <returns></returns>
		public int CreateOfficePortal(int oldOpID, int categoryID, string displayName, int userID, string xmlData)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// H_XFORM_FD  State 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public int ModifyProcessXFormState(string xfAlias, int messageID, int state, string taskActivity)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="folderID"></param>
		/// <param name="attType"></param>
		/// <returns></returns>
		public int ModifyProcessXFormCategory(string xfAlias, int messageID, int folderID, string attType)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 명함 회사정보 등록/수정
		/// </summary>
		/// <param name="command"></param>
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
		/// <param name="clientID"></param>
		/// <returns></returns>
		public int SetAddressClient(string actionKind, int clientID, int domainID, int folderID, string clientAlias, string shortName, string comName, string locale, string comZipCode, string comAddr, string comDetailAddr, string comPhone, string comFax, string homePage, string ceo, string ceoPhone, string live, string comDesc, string comInherited, int comCreator, string comReserved1, out int outClientID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				outClientID = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 명함 등록/수정
		/// </summary>
		/// <param name="command"></param>
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
		/// <param name="outCardID"></param>
		/// <returns></returns>
		public int SetAddressCard(string actionKind, int cardID, int domainID, int clientID, string cardName, string department, string grade, string scope, string cardClass, string mobile, string officePhone, string officeFax, string homePhone, string mail, string homeZipCode, string homeAddr, string homeDetailAddr, string cardDescription, string cardInherited, int cardCreator, int creatorDeptID, string cardReserved1, out int outCardID)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				outCardID = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 명함 삭제
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="cardID"></param>
		/// <param name="clientID"></param>
		/// <returns></returns>
		public int DeleteAddress(string actionKind, string cardID, string clientID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@cardID", SqlDbType.VarChar, 2000, cardID),
				ParamSet.Add4Sql("@clientID", SqlDbType.VarChar, 2000, clientID)
			};

			ParamData pData = new ParamData("admin.ph_up_AddressDeleteAddress", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 관련문서(규정,메뉴얼) 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int DeleteLinkedDoc(string xfAlias, int messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_LinkedDocRemove", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
		/// <returns></returns>
		public int ChangeBaseMessage(int domainID, int folderID, int categoryID, int messageID, int creatorID, int deptID, string xfAlias
		, string hasFileCount, string hasNewFile, string subject, string deleteDate, string expiredDate, string fileInfo)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 관리툴에서의 본문 정보 수정
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="bodyInfo"></param>
		/// <returns></returns>
		public int ChangeBaseMessageBody(int domainID, int folderID, int messageID, string xfAlias, string bodyInfo)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 제안의 시행일자 등록
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="applyDate"></param>
		/// <returns></returns>
		public int SetSuggestApplyDate(int messageID, string applyDate)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@ApplyDate", SqlDbType.VarChar, 10, applyDate)
			};

			ParamData pData = new ParamData("admin.ph_up_SuggestSetApplyDate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 프로세스 인스턴스 삭제
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int DeleteProcessInstance(string xfAlias, int messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessInstanceRemove", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}
	}
}
