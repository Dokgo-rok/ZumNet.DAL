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
	public class SpecificationDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public SpecificationDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public SpecificationDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 커버 스토리 정보 가져오기
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="coverID"></param>
		/// <returns></returns>
		public DataSet GetClubCoverStoryInfo(int folderID, int coverID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@CoverID", SqlDbType.Int, 4, coverID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetCoverStory", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 클럽 타입 별 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <returns></returns>
		public DataSet GetClubTreeAllList(int domainID, int categoryID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetTreeAll", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 커버스토리 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetCoverStoryList(int domainID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetCoverList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 마스터 변경시 ACL 변경
		/// </summary>
		/// <param name="newUserID"></param>
		/// <param name="changeACLXML"></param>
		public void ChangeClubACLByXML(int newUserID, string changeACLXML)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@NewSysopID", SqlDbType.Int, 4, newUserID),
				ParamSet.Add4Sql("@ChangeAclXML", SqlDbType.NText, changeACLXML)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubChangeAclByXML", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 마스터 변경
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="folderID"></param>
		/// <param name="newUserID"></param>
		/// <param name="oldUserID"></param>
		public void ChangeClubSysops(int groupID, int folderID, int newUserID, int oldUserID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@GR_ID", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@NewID", SqlDbType.Int, 4, newUserID),
				ParamSet.Add4Sql("@OldID", SqlDbType.Int, 4, oldUserID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubChangeSysops", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 클럽 마스터 변경시 하위의 FD들의 CreatorID를 변경
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="newUserID"></param>
		public void ChangeFolderCreatorID(int folderID, int newUserID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@NewID", SqlDbType.Int, 4, newUserID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubChangeCreatorID", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 커버 스토리 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="coverID"></param>
		public void DeleteClubCoverStory(int folderID, int coverID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@coverid", SqlDbType.Int, 4, coverID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubRemoveCoverStory", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 커버 스토리 덧글 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="coverID"></param>
		/// <param name="seqID"></param>
		/// <param name="creatorID"></param>
		public void DeleteClubCoverStoryCommentMessage(int folderID, int coverID, int seqID, string creatorID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fdID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@coverID", SqlDbType.Int, 4, coverID),
				ParamSet.Add4Sql("@seqID", SqlDbType.SmallInt, 2, seqID),
				ParamSet.Add4Sql("@creatorID", SqlDbType.NVarChar, 50, creatorID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubCoverCommentRemove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 동호회 커버스토리 덧글 작성
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="coverID"></param>
		/// <param name="comment"></param>
		/// <param name="creator"></param>
		/// <param name="creatorID"></param>
		public void CreateClubCoverStoryCommentMessage(int folderID, int coverID, string comment, string creator, string creatorID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fdID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@coverID", SqlDbType.Int, 4, coverID),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 1000, comment),
				ParamSet.Add4Sql("@creator", SqlDbType.NVarChar, 50, creator),
				ParamSet.Add4Sql("@creatorID", SqlDbType.Int, 4, creatorID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubCoverCommentSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 커버 스토리 작성
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="coverID"></param>
		/// <param name="bodyText"></param>
		/// <param name="creatorID"></param>
		/// <param name="isType"></param>
		/// <param name="xfAlias"></param>
		/// <param name="fileName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="newFileName"></param>
		/// <param name="imgUpdate"></param>
		public void SetClubCoverStory(int folderID, int coverID, string bodyText, int creatorID, string isType, string xfAlias
								, string fileName, string fileSize, string fileType, string newFileName, string imgUpdate)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@CoverID", SqlDbType.Int, 4, coverID),
				ParamSet.Add4Sql("@BodyText", SqlDbType.NText, bodyText),
				ParamSet.Add4Sql("@CreatorID", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@IsType", SqlDbType.Char, 1, isType),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@FileName", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@FileSize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@FileType", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@SavedName", SqlDbType.VarChar, 100, newFileName),
				ParamSet.Add4Sql("@FileUpdate", SqlDbType.Char, 1, imgUpdate)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubSetCoverStory", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 동호회 환경 설정 정보 / 생성 / 수정
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="titleBGcolor"></param>
		/// <param name="titleBorderColor"></param>
		/// <param name="titleFontColor"></param>
		/// <param name="titleContent"></param>
		/// <param name="titleContentType"></param>
		/// <param name="titleContentSize"></param>
		/// <param name="useDefaultDesign"></param>
		/// <param name="coverBGColor"></param>
		/// <param name="coverBorderColor"></param>
		/// <param name="coverFontColor"></param>
		/// <param name="clubBGColor"></param>
		/// <param name="clubBorderColor"></param>
		/// <param name="clubFontColor"></param>
		/// <param name="menuColor"></param>
		/// <param name="treeType"></param>
		/// <param name="totalSize"></param>
		public void SetClubEnvironment(int folderID, string titleBGcolor, string titleBorderColor, string titleFontColor, string titleContent
					, string titleContentType, string titleContentSize, string useDefaultDesign, string coverBGColor, string coverBorderColor
					, string coverFontColor, string clubBGColor, string clubBorderColor, string clubFontColor, string menuColor, string treeType, string totalSize)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@TitleBGcolor", SqlDbType.VarChar, 20, titleBGcolor),
				ParamSet.Add4Sql("@TitleBorderColor", SqlDbType.VarChar, 20, titleBorderColor),
				ParamSet.Add4Sql("@TitleFontColor", SqlDbType.VarChar, 20, titleFontColor),
				ParamSet.Add4Sql("@TitleContent", SqlDbType.NVarChar, 1000, titleContent),
				ParamSet.Add4Sql("@TitleContentType", SqlDbType.VarChar, 20, titleContentType),
				ParamSet.Add4Sql("@TitleContentSize", SqlDbType.VarChar, 20, titleContentSize),
				ParamSet.Add4Sql("@UseDefaultDesign", SqlDbType.Char, 1, useDefaultDesign),
				ParamSet.Add4Sql("@CoverBGColor", SqlDbType.VarChar, 20, coverBGColor),
				ParamSet.Add4Sql("@CoverBorderColor", SqlDbType.VarChar, 20, coverBorderColor),
				ParamSet.Add4Sql("@CoverFontColor", SqlDbType.VarChar, 20, coverFontColor),
				ParamSet.Add4Sql("@ClubBGColor", SqlDbType.VarChar, 20, clubBGColor),
				ParamSet.Add4Sql("@ClubBorderColor", SqlDbType.VarChar, 20, clubBorderColor),
				ParamSet.Add4Sql("@ClubFontColor", SqlDbType.VarChar, 20, clubFontColor),
				ParamSet.Add4Sql("@MenuColor", SqlDbType.VarChar, 20, menuColor),
				ParamSet.Add4Sql("@TreeType", SqlDbType.VarChar, 20, treeType),
				ParamSet.Add4Sql("@TotalSize", SqlDbType.VarChar, 100, totalSize)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubSetEnvironment", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		///  동호회 환경 설정 정보 가져오기
		/// </summary>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetClubEnvironment(int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetEnvironment", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 동호회 전체 통계 정보 가져오기
		/// </summary>
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
		public DataSet GetAllClubStatistic(int pageIndex, int pageCount, string sortColum, string sortType, string searchColum
							, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
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

			ParamData pData = new ParamData("admin.ph_up_ClubGetStatistic", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = Convert.ToInt32(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서 작업리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="userID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetDocWorkList(int domainID, int userID, int pageIndex, int pageCount, string startDate, string endDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@UR_ID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@PageNo", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@PageCount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@StartDate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@EndDate", SqlDbType.VarChar, 10, endDate),
				ParamSet.Add4Sql("@TotalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetWorkList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = Convert.ToInt32(pData.GetParamValue("@TotalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 보안그룹 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public DataSet GetSecureGroupInfo(int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetSecureGroupList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 문서, 지식관리의 게시물 이름 변경
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="xfAlias"></param>
		/// <param name="subject"></param>
		public void ChangeDocReName(int messageID, string xfAlias, string subject)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@MessageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@XfAlias", SqlDbType.NVarChar, 30, xfAlias),
				ParamSet.Add4Sql("@Subject", SqlDbType.NVarChar, 50, subject)
			};

			ParamData pData = new ParamData("admin.ph_up_DocChangeDocReName", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 작업목록 삭제
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public void DeleteDocWorkList(string startDate, string endDate, int userID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@StartDate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@EndDate", SqlDbType.VarChar, 10, endDate),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_DocDelWorkList", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 웹 파트 폴더 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public DataSet GetFolderIncludedWebParts(int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_OpGetFolderWebParts", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당 포탈 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="categoryAlias"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet SearchCurrentOfficePortal(int domainID, int categoryID, string categoryAlias, int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@ctalias", SqlDbType.VarChar, 30, categoryAlias),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_OpSearchCurrent", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// OP 속성변경
		/// </summary>
		/// <param name="opID"></param>
		/// <param name="deleteDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="opName"></param>
		public void ChangeOfficePortalAttribute(int opID, string deleteDate, string expiredDate, string opName)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@op_id", SqlDbType.Int, 4, opID),
				ParamSet.Add4Sql("@deleted_date", SqlDbType.VarChar, 10, deleteDate),
				ParamSet.Add4Sql("@expired_date", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@op_name", SqlDbType.NVarChar, 100, opName)
			};

			ParamData pData = new ParamData("admin.ph_up_OpChangeAttribute", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 특정 참여자(참여자, 자원) 일정 검색 리스트 => 일정 리스트 + 일정에 따른 참여자 목록
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="participants"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetScheduleSearchListParts(int domainID, string participants, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@participants", SqlDbType.VarChar, 1000, participants),
				ParamSet.Add4Sql("@rangeSDate", SqlDbType.Char, 10, startDate),
				ParamSet.Add4Sql("@rangeEDate", SqlDbType.Char, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetSearchListParts", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 메뉴에 따른 일정 종류 가져오기
		/// </summary>
		/// <param name="categoryID"></param>
		/// <returns></returns>
		public DataSet GetScheduleType(int categoryID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetType", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 일정 공유 설정
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="mode"></param>
		/// <param name="xmlData"></param>
		public void CreateScheduleShare(int messageID, string mode, string xmlData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleSetShare", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 일정 상태 변경 (메인 테이블의 상태값과 일자를 이력 테이블에 옮기고 새 상태값을 반영한다.)
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="state"></param>
		/// <param name="actor"></param>
		public void CreateScheduleStateHistory(int messageID, int state, int actor)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@state", SqlDbType.SmallInt, 2, state),
				ParamSet.Add4Sql("@actor", SqlDbType.Int, 4, actor)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleSetStateHistory", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 일정 반복 작업 (생성, 수정, 삭제)
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="deleteDate"></param>
		/// <param name="repeatType"></param>
		/// <param name="periodFrom"></param>
		/// <param name="repeatEnd"></param>
		/// <param name="repeatCount"></param>
		/// <param name="intervalType"></param>
		/// <param name="interval"></param>
		/// <param name="condDay"></param>
		/// <param name="condWeek"></param>
		/// <param name="condDate"></param>
		public void HandleScheduleRepeat(int messageID, string deleteDate, string repeatType, string periodFrom, string repeatEnd
						, string repeatCount, string intervalType, string interval, string condDay, string condWeek, string condDate)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@deletedate", SqlDbType.Char, 10, deleteDate),
				ParamSet.Add4Sql("@repeatType", SqlDbType.Char, 1, repeatType),
				ParamSet.Add4Sql("@periodFrom", SqlDbType.Char, 10, periodFrom),
				ParamSet.Add4Sql("@repeatEnd", SqlDbType.VarChar, 10, repeatEnd),
				ParamSet.Add4Sql("@repeatCount", SqlDbType.VarChar, 10, repeatCount),
				ParamSet.Add4Sql("@intervalType", SqlDbType.Char, 1, intervalType),
				ParamSet.Add4Sql("@interval", SqlDbType.Char, 2, interval),
				ParamSet.Add4Sql("@con_Day", SqlDbType.Char, 7, condDay),
				ParamSet.Add4Sql("@con_Week", SqlDbType.Char, 1, condWeek),
				ParamSet.Add4Sql("@con_Date", SqlDbType.Char, 2, condDate)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleWorkRepeat", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 포탈에서 라이브 폴 보기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="partYN"></param>
		/// <returns></returns>
		public DataSet GetPollInfoInOfficePortal(int domainID, int groupID, int userID, out string partYN)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@partiYN", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetOPView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				partYN = pData.GetParamValue("@partiYN").ToString();
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 통계
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="flag"></param>
		/// <returns></returns>
		public DataSet GetPollStatistics(int messageID, string flag)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msg_id", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@flag", SqlDbType.Char, 1, flag)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetStatistics", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 설문 미참여자
		/// </summary>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public DataSet GetNotPartUser(string messageID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.VarChar, 33, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_PollGetNotPartiUser", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 동호회의 개설 승인
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		public void ChangeMadeClubApproval(int folderID, int groupID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeGrApproval", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 동호회 운영자의 정보를 가지고 온다.
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetClubSysopsInfo(int groupID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@grID", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetGrSysopsInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 월 최다 조회 문서 목록
		/// </summary>
		/// <param name="listCount"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocMonthViewDocument(int listCount, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.Int, 4, listCount),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetMonthlyViewDocument", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 월 최다 조회자 목록
		/// </summary>
		/// <param name="listCount"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocMonthViewUser(int listCount, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.Int, 4, listCount),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetMonthlyViewUser", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 월 최다 등록자 목록
		/// </summary>
		/// <param name="listCount"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocMonthCreateUser(int listCount, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.Int, 4, listCount),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetMonthlyCreateUser", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당 월의 부서의 등록 문서 정보 
		/// </summary>
		/// <param name="listCount"></param>
		/// <param name="deptID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet GetDocMonthCreateDept(int listCount, int deptID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@num", SqlDbType.Int, 4, listCount),
				ParamSet.Add4Sql("@deptID", SqlDbType.Int, 4, deptID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_DocGetMonthlyCreateDept", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 포탈 화면의 게시판 구조 정보
		/// </summary>
		/// <returns></returns>
		public DataSet GetPortalSetting()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.ph_up_OPGetPortalSetting", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// OP 세팅된 항목 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		public void DeleteOPPortalSetting(int folderID, string xfAlias)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.NVarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_OPDelPortalSetting", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 포탈 변경 사항 저장
		/// </summary>
		/// <param name="portalData"></param>
		public void ChangeOPPortalSetting(string portalData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@portaldata", SqlDbType.NText, portalData)
			};

			ParamData pData = new ParamData("admin.ph_up_OPChangePortalSetting", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 대용량 메일 정보 입력
		/// </summary>
		/// <param name="senderID"></param>
		/// <param name="subject"></param>
		/// <param name="period"></param>
		/// <param name="hasattachfile"></param>
		/// <returns>messageID</returns>
		public int SetGMailInfo(int senderID, string subject, int period, string hasattachfile)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@senderID", SqlDbType.Int, 4, senderID),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@period", SqlDbType.SmallInt, 2, period),
				ParamSet.Add4Sql("@hasattachfile", SqlDbType.Char, 1, hasattachfile),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SetGMailInfo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//messageID = Convert.ToInt32(pData.GetParamValue("@messageID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		///  대용량 메일의 첨부 파일 정보 입력
		/// </summary>
		/// <param name="xfalias"></param>
		/// <param name="messageID"></param>
		/// <param name="isFile"></param>
		/// <param name="fileName"></param>
		/// <param name="savedName"></param>
		/// <param name="fileSize"></param>
		/// <param name="fileType"></param>
		/// <param name="prefix"></param>
		/// <param name="location"></param>
		/// <returns>attachID</returns>
		public int SetGMailFileInfo(string xfalias, int messageID, string isFile, string fileName, string savedName
								, string fileSize, string fileType, string prefix, string location)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.NVarChar, 30, xfalias),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@isfile", SqlDbType.Char, 1, isFile),
				ParamSet.Add4Sql("@filename", SqlDbType.NVarChar, 100, fileName),
				ParamSet.Add4Sql("@savedname", SqlDbType.VarChar, 100, savedName),
				ParamSet.Add4Sql("@filesize", SqlDbType.VarChar, 20, fileSize),
				ParamSet.Add4Sql("@filetype", SqlDbType.VarChar, 10, fileType),
				ParamSet.Add4Sql("@prefix", SqlDbType.VarChar, 15, prefix),
				ParamSet.Add4Sql("@location", SqlDbType.VarChar, 15, location),
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_SetGMailFileInfo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//attachID = Convert.ToInt32(pData.GetParamValue("@attachID").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 대용량 메일 정보
		/// </summary>
		/// <returns></returns>
		public DataSet GetBaseMailCapacity()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.up_GetConfiguration", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 대용량 메일 설정
		/// </summary>
		/// <param name="externalCapacity"></param>
		/// <param name="capacity"></param>
		/// <param name="period"></param>
		/// <param name="maxCapacity"></param>
		public void ChangeBaseMailCapacity(string externalCapacity, string capacity, int period, string maxCapacity)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@capacity", SqlDbType.VarChar, 50, capacity),
				ParamSet.Add4Sql("@period", SqlDbType.Int, 4, period),
				ParamSet.Add4Sql("@maxcapacity", SqlDbType.VarChar, 50, maxCapacity),
				ParamSet.Add4Sql("@externalcapacity", SqlDbType.VarChar, 50, externalCapacity)
			};

			ParamData pData = new ParamData("admin.up_SetConfiguration", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 마스터 패스워드 정보
		/// </summary>
		/// <returns>password</returns>
		public string GetMasterPassword()
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@password", SqlDbType.VarChar, 50, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetMasterPassword", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//password = pData.GetParamValue("@password").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 사용자 비밀번호 정보
		/// </summary>
		/// <param name="logonID"></param>
		/// <returns></returns>
		public string GetUserPassword(string logonID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@logonID", SqlDbType.VarChar, 30, logonID),
				ParamSet.Add4Sql("@password", SqlDbType.VarChar, 100, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetUserPassword", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 보고서의 양식 분류 정보 변경
		/// </summary>
		/// <param name="companycode"></param>
		/// <param name="formInfo"></param>
		public void ChangeReportFormInfo(string companycode, string formInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@form_info", SqlDbType.NText, formInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeReportFormClass", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 해당 양식에 대한 기본 정보
		/// </summary>
		/// <param name="companycode"></param>
		/// <param name="classID"></param>
		/// <param name="formID"></param>
		/// <returns></returns>
		public DataSet GetReportFormsInfo(string companycode, int classID, string formID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@classID", SqlDbType.Int, 4, classID),
				ParamSet.Add4Sql("@formID", SqlDbType.VarChar, 63, formID)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetReportForms", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 보고서 양식 수정 및 생성
		/// </summary>
		/// <param name="companycode"></param>
		/// <param name="reportType"></param>
		/// <param name="classID"></param>
		/// <param name="formID"></param>
		/// <param name="displayName"></param>
		/// <param name="seq"></param>
		/// <param name="inuse"></param>
		/// <param name="useeditor"></param>
		/// <param name="subTableName1"></param>
		/// <param name="subTableSort1"></param>
		/// <param name="subTableName2"></param>
		/// <param name="subTableSort2"></param>
		/// <param name="subTableName3"></param>
		/// <param name="subTableSort3"></param>
		/// <param name="subTableName4"></param>
		/// <param name="subTableSort4"></param>
		public void HandleReportForm(string companycode, string reportType, int classID, string formID, string displayName, int seq
							, string inuse, string useeditor, string subTableName1, string subTableSort1, string subTableName2
							, string subTableSort2, string subTableName3, string subTableSort3, string subTableName4, string subTableSort4)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@reporttype", SqlDbType.Char, 1, reportType),
				ParamSet.Add4Sql("@classID", SqlDbType.Int, 4, classID),
				ParamSet.Add4Sql("@formID", SqlDbType.VarChar, 63, formID),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 100, displayName),
				ParamSet.Add4Sql("@seq", SqlDbType.Int, 4, seq),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inuse),
				ParamSet.Add4Sql("@useeditor", SqlDbType.Char, 1, useeditor),
				ParamSet.Add4Sql("@subtablename1", SqlDbType.VarChar, 100, subTableName1),
				ParamSet.Add4Sql("@subtablesort1", SqlDbType.VarChar, 100, subTableSort1),
				ParamSet.Add4Sql("@subtablename2", SqlDbType.VarChar, 100, subTableName2),
				ParamSet.Add4Sql("@subtablesort2", SqlDbType.VarChar, 100, subTableSort2),
				ParamSet.Add4Sql("@subtablename3", SqlDbType.VarChar, 100, subTableName3),
				ParamSet.Add4Sql("@subtablesort3", SqlDbType.VarChar, 100, subTableSort3),
				ParamSet.Add4Sql("@subtablename4", SqlDbType.VarChar, 100, subTableName4),
				ParamSet.Add4Sql("@subtablesort4", SqlDbType.VarChar, 100, subTableSort4)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseReportFormsHandler", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 통계 : 개인 통계정보(등록/답변/조회/수정/추천/피추천/평가)
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="isKind"></param>
		/// <param name="isView"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetStatisticsPersonal(int userID, string isKind, string isView, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@IsKind", SqlDbType.VarChar, 10, isKind),
				ParamSet.Add4Sql("@IsView", SqlDbType.VarChar, 10, isView),
				ParamSet.Add4Sql("@StartDate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@EndDate", SqlDbType.VarChar, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetPersonalInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : XFAlias별 개인 통계정보(등록/답변/조회/수정/추천/피추천/평가)
		/// </summary>
		/// <param name="xfalias"></param>
		/// <param name="userID"></param>
		/// <param name="isKind"></param>
		/// <param name="isView"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetStatisticsPersonalByXFAlias(string xfalias, int userID, string isKind, string isView, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfalias),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@IsKind", SqlDbType.VarChar, 10, isKind),
				ParamSet.Add4Sql("@IsView", SqlDbType.VarChar, 10, isView),
				ParamSet.Add4Sql("@StartDate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@EndDate", SqlDbType.VarChar, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetPersonalInfoByXFAlias", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : 부서원 통계정보(등록/답변/조회/수정/추천/피추천/평가)
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="deptID"></param>
		/// <param name="isKind"></param>
		/// <returns></returns>
		public DataSet GetStatisticsDeptPerson(int userID, int deptID, string isKind)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@DeptID", SqlDbType.Int, 4, deptID),
				ParamSet.Add4Sql("@IsKind", SqlDbType.VarChar, 10, isKind)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetDeptMemberInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 :  XFAlias별 부서원 통계정보(등록/답변/조회/수정/추천/피추천/평가)
		/// </summary>
		/// <param name="xfalias"></param>
		/// <param name="userID"></param>
		/// <param name="deptID"></param>
		/// <param name="isKind"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetStatisticsDeptPersonByXFAlias(string xfalias, int userID, int deptID, string isKind, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfalias),
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@DeptID", SqlDbType.Int, 4, deptID),
				ParamSet.Add4Sql("@IsKind", SqlDbType.VarChar, 10, isKind),
				ParamSet.Add4Sql("@StartDate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@EndDate", SqlDbType.VarChar, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetDeptMemberInfoByXFAlias", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : 부서 통계정보(등록/답변/조회/수정/추천/피추천/평가)
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="isKind"></param>
		/// <returns></returns>
		public DataSet GetStatisticsDept(int dnID, string isKind)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@IsKind", SqlDbType.VarChar, 10, isKind)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetDeptInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : XFAlias별 부서 통계정보(등록/답변/조회/수정/추천/피추천/평가)
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="xfalias"></param>
		/// <param name="isKind"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet GetStatisticsDeptByXFAlias(int dnID, string xfalias, string isKind, string startDate, string endDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfalias),
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@IsKind", SqlDbType.VarChar, 10, isKind),
				ParamSet.Add4Sql("@StartDate", SqlDbType.VarChar, 10, startDate),
				ParamSet.Add4Sql("@EndDate", SqlDbType.VarChar, 10, endDate)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetDeptInfoByXFAlias", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : 부서원 폴더별 조회 통계정보
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet StatisticsViewVSPerson(int dnID, string folderID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.VarChar, 63, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetViewVSPerson", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : 해당 부서 폴더별 조회 통계정보
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet StatisticsViewVSDept(int dnID, string folderID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.VarChar, 63, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetViewVSDept", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : 개인 폴더별 작성 통계정보
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet StatisticsCreateVSPerson(int dnID, string folderID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.VarChar, 63, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetCreateVSPerson", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 통계 : 부서 폴더별 작성 통계정보
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="folderID"></param>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public DataSet StatisticsCreateVSDept(int dnID, string folderID, string xfAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.VarChar, 63, folderID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.NVarChar, 30, xfAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_StatisticsGetCreateVSDept", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// MSN 알람 카운터 가져오기(결재, 보고/집계, 제안)
		/// </summary>
		/// <param name="logonID"></param>
		/// <returns>linkageCnt</returns>
		public string GetMsnLinkageCount(string logonID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 10, logonID),
				ParamSet.Add4Sql("@linkageCnt", SqlDbType.VarChar, 50, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetMsnCount", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//strReturn = pData.GetParamValue("@linkageCnt").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 인사 사용자 정보
		/// </summary>
		/// <param name="logonID"></param>
		/// <returns></returns>
		public DataSet GetUserInsaInfo(string logonID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@logonID", SqlDbType.VarChar, 30, logonID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetUserInsaInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 제안 전문가 정보
		/// </summary>
		/// <returns></returns>
		public DataSet GetSuggestExpertInfo()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = null;

			ParamData pData = new ParamData("admin.ph_up_GetSuggestExpertInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 제안 전문가 정보 제어
		/// </summary>
		/// <param name="suggestInfo"></param>
		public void SetSuggestExpertInfo(string suggestInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@suggestinfo", SqlDbType.NText, suggestInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_SetSuggestExpertInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 대용량 메일 첨부 파일 정보
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="messageID"></param>
		/// <param name="attachID"></param>
		/// <returns></returns>
		public DataSet GetGMailFileInfo(int domainID, int messageID, int attachID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@attachID", SqlDbType.Int, 4, attachID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetGMailFileInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 팀점 문서함 리스트
		/// </summary>
		/// <param name="logonID"></param>
		/// <param name="groupAlias"></param>
		/// <param name="pageNo"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortCol"></param>
		/// <param name="sortType"></param>
		/// <param name="searchCol"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="searchType"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetTeamDocList(string logonID, string groupAlias, int pageNo, int pageCount, string sortCol, string sortType
					, string searchCol, string searchText, string searchStartDate, string searchEndDate, string searchType, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@logonID", SqlDbType.VarChar, 10, logonID),
				ParamSet.Add4Sql("@groupalias", SqlDbType.NVarChar, 3, groupAlias),
				ParamSet.Add4Sql("@pageno", SqlDbType.Int, 4, pageNo),
				ParamSet.Add4Sql("@pagecount", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortcol", SqlDbType.VarChar, 20, sortCol),
				ParamSet.Add4Sql("@sorttype", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchcol", SqlDbType.VarChar, 20, searchCol),
				ParamSet.Add4Sql("@searchtext", SqlDbType.NVarChar, 100, searchText),
				ParamSet.Add4Sql("@searchstartdate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchenddate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@searchtype", SqlDbType.Char, 1, searchType),
				ParamSet.Add4Sql("@totalmessage", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_TeamGetDocList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = Convert.ToInt32(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 팀점 문서함 문서 정보
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="messageType"></param>
		/// <param name="messageYear"></param>
		/// <param name="messageKind"></param>
		/// <returns></returns>
		public DataSet GetTeamDocMessageInfo(int messageID, string messageType, string messageYear, string messageKind)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@messagetype", SqlDbType.VarChar, 3, messageType),
				ParamSet.Add4Sql("@messageyear", SqlDbType.VarChar, 4, messageYear),
				ParamSet.Add4Sql("@messagekind", SqlDbType.Char, 1, messageKind)
			};

			ParamData pData = new ParamData("admin.ph_up_TeamGetDocViewContent", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 팀점 문서함 조회수 기록
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="logonID"></param>
		public void SetTeamDocView(int messageID, string logonID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@logonID", SqlDbType.VarChar, 10, logonID)
			};

			ParamData pData = new ParamData("admin.ph_up_TeamSetViewContent", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 포탈에서의 팀점 문서함 리스트
		/// </summary>
		/// <param name="listCount"></param>
		/// <param name="messageYear"></param>
		/// <param name="groupAlias"></param>
		/// <returns></returns>
		public DataSet GetTeamPortalInfo(int listCount, string messageYear, string groupAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@listcount", SqlDbType.Int, 4, listCount),
				ParamSet.Add4Sql("@messageyear", SqlDbType.VarChar, 4, messageYear),
				ParamSet.Add4Sql("@groupalias", SqlDbType.VarChar, 3, groupAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_TeamGetDocListInPortal", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 포탈 화면 정보 수정
		/// </summary>
		/// <param name="strPortalData"></param>
		public void UpdatePortalInfo(string strPortalData)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@portalinfo", SqlDbType.NText, strPortalData)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdatePortalInfo", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 포탈 설정 정보 조회
		/// </summary>
		/// <param name="opid"></param>
		/// <returns></returns>
		public DataSet GetPortalConfigData(string opid)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@op_id", SqlDbType.Int, Convert.ToInt32(opid))
			};

			ParamData pData = new ParamData("admin.ph_up_SelectPortalConfiguration", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 자원 신청 또는 예약 받은 목록 검색
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="creatorID"></param>
		/// <param name="objectType"></param>
		/// <param name="partID"></param>
		/// <param name="searchCol"></param>
		/// <param name="searchText"></param>
		/// <param name="searchDate"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <returns></returns>
		public DataSet GetSchedulePartsList(string mode, int creatorID, string objectType, int partID, string searchCol
								, string searchText, string searchDate, string searchStartDate, string searchEndDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, objectType),
				ParamSet.Add4Sql("@partid", SqlDbType.Int, 4, partID),
				ParamSet.Add4Sql("@search_col", SqlDbType.VarChar, 100, searchCol),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 100, searchText),
				ParamSet.Add4Sql("@search_date", SqlDbType.VarChar, 100, searchDate),
				ParamSet.Add4Sql("@search_start", SqlDbType.Char, 10, searchStartDate),
				ParamSet.Add4Sql("@search_end", SqlDbType.Char, 10, searchEndDate)
			};

			ParamData pData = new ParamData("admin.ph_up_ScheduleGetPartsList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
