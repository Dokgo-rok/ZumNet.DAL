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
	public class FolderDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public FolderDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public FolderDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// FD DisplayName 변경
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="newName"></param>
		public void ChangeBaseFolderName(int folderID, string newName)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, newName)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeFDDisplayName", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 부서폴더 생성
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="createInfo"></param>
		public void CreateBaseDepartFolder(int domainID, int categoryID, int parentFolderID, string createInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@parentFolderID", SqlDbType.Int, 4, parentFolderID),
				ParamSet.Add4Sql("@create_info", SqlDbType.NText, createInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseCreateDepartFolder", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// FD Batch 생성
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="parentID"></param>
		/// <param name="uploadPath"></param>
		/// <returns>실행결과</returns>
		public string CreateBaseFolderFromExcel(int domainID, int categoryID, int parentID, string uploadPath)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID),
				ParamSet.Add4Sql("@parentFolderID", SqlDbType.Int, 4, parentID),
				ParamSet.Add4Sql("@path", SqlDbType.NVarChar, 255, uploadPath),
				ParamSet.Add4Sql("@result", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseCreateFolderFromExcel", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//result = pData.GetParamValue("@result").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 폴더 DeleteDate Update
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="attType"></param>
		/// <returns>실행결과</returns>
		public string DeleteBaseFolder(int folderID, int parentFolderID, string objectType, int objectID, string attType)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@parentFD_id", SqlDbType.Int, 4, parentFolderID),
				ParamSet.Add4Sql("@object_type", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@object_id", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@att_type", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@return", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseDeleteFD", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//result = pData.GetParamValue("@return").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 부서폴더 리스트 쿼리
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="memberOf"></param>
		/// <param name="viewDate"></param>
		/// <returns></returns>
		public DataSet GetBaseDepartFolderList(int domainID, int categoryID, int memberOf, string viewDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@memberof", SqlDbType.Int, 4, memberOf),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetDepartFolderList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 파일유형 DB Query
		/// </summary>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetFolderAttribute(int objectType, int objectID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@object_type", SqlDbType.Int, 4, objectType),
				ParamSet.Add4Sql("@object_id", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetFolderAttribute", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Folder 에 할당된 Ownership 쿼리
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetBaseFolderOwnerShip(int domainID, string objectType, int objectID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@objectType", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@objectID", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetFDOwnership", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Folder 에 할당된 Process 쿼리
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetBaseFolderProcess(int domainID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetFDProcess", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 파일유형 DB Query
		/// </summary>
		/// <param name="categoryID"></param>
		/// <param name="isAdmin"></param>
		/// <returns></returns>
		public DataSet GetFolderType(int categoryID, string isAdmin)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, isAdmin)
			};

			ParamData pData = new ParamData("admin.ph_up_GetFolderType", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 상위 폴더 정보 얻어오기
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="attType"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		public DataSet GetParentFolderAttribute(int folderID, string objectType, int objectID, string attType, int level)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@object_type", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@object_id", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@att_type", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@level", SqlDbType.Int, 4, level)
			};

			ParamData pData = new ParamData("admin.ph_up_GetParentFolderAttribute", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}		

		/// <summary>
		/// 공유 문서함 트리노드 검색
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="selectedID"></param>
		/// <param name="selectedType"></param>
		/// <param name="expandedLevel"></param>
		/// <param name="userID"></param>
		/// <param name="openFolderInfo"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="searchCol">user, group</param>
		/// <param name="searchText"></param>
		/// <returns></returns>
		public DataSet GetTreeObject_Shared(int domainID, int categoryID, string selectedID, string selectedType, int expandedLevel
						, int userID, string openFolderInfo, string isAdmin, string permission, string searchCol, string searchText)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@select_id", SqlDbType.VarChar, 63, selectedID),
				ParamSet.Add4Sql("@select_type", SqlDbType.VarChar, 2, selectedType),
				ParamSet.Add4Sql("@expandlevel", SqlDbType.SmallInt, 2, expandedLevel),
				ParamSet.Add4Sql("@cur_userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@open_fd", SqlDbType.VarChar, 63, openFolderInfo),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@permission", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 30, searchCol),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText)
			};

			ParamData pData = new ParamData("admin.ph_up_GetTreeObject_Shared", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// FD 생성 및 수정, 삭제
		/// </summary>
		/// <param name="actionKind"></param>
		/// <param name="folderInfo"></param>
		/// <param name="aclInfo"></param>
		/// <param name="ownershipInfo"></param>
		/// <param name="processInfo"></param>
		/// <param name="pointInfo"></param>
		public void HandleBaseFolder(string actionKind, string folderInfo, string aclInfo, string ownershipInfo, string processInfo, string pointInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 10, actionKind),
				ParamSet.Add4Sql("@folder_Info", SqlDbType.NText, folderInfo),
				ParamSet.Add4Sql("@acl_info", SqlDbType.NText, aclInfo),
				ParamSet.Add4Sql("@ownership_info", SqlDbType.NText, ownershipInfo),
				ParamSet.Add4Sql("@process_info", SqlDbType.NText, processInfo),
				ParamSet.Add4Sql("@point_info", SqlDbType.NText, pointInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseFDHandler", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// FD 이동 
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="attType"></param>
		/// <param name="targetParentFolderID"></param>
		public void MoveBaseFolder(int folderID, string objectType, int objectID, string attType, int targetParentFolderID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@object_type", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@object_id", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@att_type", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@target_parentFD_id", SqlDbType.Int, 4, targetParentFolderID)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeFDInstanceForMove", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 폴더 DB 삭제
		/// </summary>
		/// <param name="folderID"></param>
		/// <returns>실행결과</returns>
		public string RemoveBaseFolder(int folderID)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@return", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseRemoveFD", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//result = pData.GetParamValue("@return").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// Folder 검색
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="searchText"></param>
		/// <param name="isAll"></param>
		/// <returns></returns>
		public DataSet GetBaseSearchFolder(int domainID, int categoryID, string searchText, string isAll)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 1000, searchText),
				ParamSet.Add4Sql("@isAll", SqlDbType.Char, 1, isAll)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseSearchFolders", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Folder Alias에 해당하는 폴더 찾기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderAlias"></param>
		/// <returns></returns>
		public DataSet SearchFolderByAlias(int domainID, string folderAlias)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fdalias", SqlDbType.VarChar, 63, folderAlias)
			};

			ParamData pData = new ParamData("admin.ph_up_SearchFolderByAlias", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 하위 Folder List
		/// </summary>
		/// <param name="categoryID"></param>
		/// <returns></returns>
		public DataSet GetObjectXFormByCategory(int categoryID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetXformCT", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 하위 Folder List
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetObjectXFormInFolder(int domainID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@FD_ID", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetXFormInFD", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 폴더 정보 변경
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="folderType"></param>
		/// <param name="folderAlias"></param>
		/// <param name="xfAlias"></param>
		/// <param name="description"></param>
		/// <param name="inherited"></param>
		/// <param name="displayName"></param>
		/// <param name="expiredDate"></param>
		public void ChangeObjectFolder(int folderID, string folderType, string folderAlias, string xfAlias
							, string description, string inherited, string displayName, string expiredDate)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@fdtype", SqlDbType.Char, 1, folderType),
				ParamSet.Add4Sql("@fdalias", SqlDbType.VarChar, 63, folderAlias),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 200, description),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, displayName),
				ParamSet.Add4Sql("@expireddate", SqlDbType.Char, 10, expiredDate)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeFD", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 폴더 인스턴스 정보 변경
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="sortKey"></param>
		/// <param name="attType"></param>
		/// <param name="comment"></param>
		public void ChangeObjectFolderInstance(int domainID, int folderID, int groupID, int userID
								, int parentFolderID, int sortKey, string attType, string comment)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@parentfolderid", SqlDbType.Int, 4, parentFolderID),
				ParamSet.Add4Sql("@sortkey", SqlDbType.Int, 4, sortKey),
				ParamSet.Add4Sql("@attype", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 100, comment)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeFDInstance", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 폴더 삭제
		/// </summary>
		/// <param name="folderID"></param>
		public void DeleteObjectFolder(int folderID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@FolderID", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectRemoveFD", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 폴더 생성
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderType"></param>
		/// <param name="folderAlias"></param>
		/// <param name="xfAlias"></param>
		/// <param name="displayName"></param>
		/// <param name="description"></param>
		/// <param name="inherited"></param>
		/// <param name="inUse"></param>
		/// <param name="password"></param>
		/// <param name="creatorID"></param>
		/// <param name="expiredDate"></param>
		/// <param name="reserved1"></param>
		/// <returns>FolerID</returns>
		public int CreateObjectFolder(int domainID, int categoryID, string folderType, string folderAlias
							, string xfAlias, string displayName, string description, string inherited, string inUse
							, string password, int creatorID, string expiredDate, string reserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.SmallInt, 2, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID),
				ParamSet.Add4Sql("@foldertype", SqlDbType.Char, 1, folderType),
				ParamSet.Add4Sql("@folderalias", SqlDbType.VarChar, 64, folderAlias),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, displayName),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@password", SqlDbType.NVarChar, 10, password),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@expireddate", SqlDbType.Char, 10, expiredDate),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 256, reserved1),
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewFD", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//folderID = int.Parse(pData.GetParamValue("@folderid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 폴더 인스턴스 입력
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="objectType"></param>
		/// <param name="folderID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="hasSubFolder"></param>
		/// <param name="attType"></param>
		/// <param name="comment"></param>
		/// <param name="reserved1"></param>
		public void CreateObjectFolderInstance(int objectID, string objectType, int folderID, int parentFolderID
									, string hasSubFolder, string attType, string comment, string reserved1)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@parentfolderid", SqlDbType.Int, 4, parentFolderID),
				ParamSet.Add4Sql("@hassubfolder", SqlDbType.Char, 1, hasSubFolder),
				ParamSet.Add4Sql("@atttype", SqlDbType.Char, 1, attType),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 100, comment),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 256, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewFDInstance", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 폴더 소유 정보 변경(폴더 AttType 변경)	
		/// </summary>
		/// <param name="targetType"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="folderID"></param>
		/// <param name="ownerShipInfo"></param>
		public void ChangeBaseFolderAttribute(string targetType, string objectType, int objectID, int folderID, string ownerShipInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@targetType", SqlDbType.Char, 2, targetType),
				ParamSet.Add4Sql("@objectType", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@ownership_info", SqlDbType.NText, ownerShipInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeFDOwnerShip", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 폴더에 할당된 프로세스를 추가, 수정, 삭제 한다.
		/// </summary>
		/// <param name="folderID"></param>
		/// <param name="processInfo"></param>
		public void HandleBaseFolderProcess(int folderID, string processInfo)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@process_info", SqlDbType.NText, processInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeFDProcess", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// PH_OBJECT_FD 테이블, PH_FOLDER_INSTANCE 테이블 입력(CreateBaseFolderInstance에서 처리하는 부분을 현재 메소드에서 처리)
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderType"></param>
		/// <param name="folderAlias"></param>
		/// <param name="xfAlias"></param>
		/// <param name="displayName"></param>
		/// <param name="description"></param>
		/// <param name="inHerited"></param>
		/// <param name="shared"></param>
		/// <param name="inUse"></param>
		/// <param name="count"></param>
		/// <param name="password"></param>
		/// <param name="usePoint"></param>
		/// <param name="useProcess"></param>
		/// <param name="creatorID"></param>
		/// <param name="createDate"></param>
		/// <param name="expiredDate"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="parentFolderID"></param>
		/// <param name="sortKey"></param>
		/// <param name="comment"></param>
		/// <param name="reserved1"></param>
		/// <param name="reserved2"></param>
		/// <returns>FolderID</returns>
		public int CreateBaseFolder(int domainID, int categoryID, string folderType, string folderAlias, string xfAlias
							, string displayName, string description, string inHerited, string shared, string inUse, string count
							, string password, string usePoint, string useProcess, int creatorID, string createDate, string expiredDate
							, string objectType, int objectID, int parentFolderID, int sortKey, string comment, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID),
				ParamSet.Add4Sql("@foldertype", SqlDbType.Char, 1, folderType),
				ParamSet.Add4Sql("@folderalias", SqlDbType.VarChar, 64, folderAlias),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, displayName),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inHerited),
				ParamSet.Add4Sql("@shared", SqlDbType.Char, 1, shared),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@count", SqlDbType.VarChar, 10, count),
				ParamSet.Add4Sql("@password", SqlDbType.VarChar, 10, password),
				ParamSet.Add4Sql("@usepoint", SqlDbType.Char, 1, usePoint),
				ParamSet.Add4Sql("@useprocess", SqlDbType.Char, 1, useProcess),
				ParamSet.Add4Sql("@creatorid", SqlDbType.Int, 4, creatorID),
				ParamSet.Add4Sql("@create_date", SqlDbType.VarChar, 10, createDate),
				ParamSet.Add4Sql("@expired_date", SqlDbType.VarChar, 10, expiredDate),
				ParamSet.Add4Sql("@object_type", SqlDbType.Char, 1, objectType),
				ParamSet.Add4Sql("@object_id", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@parentFD_id", SqlDbType.Int, 4, parentFolderID),
				ParamSet.Add4Sql("@sortkey", SqlDbType.SmallInt, 2, sortKey),
				ParamSet.Add4Sql("@comment", SqlDbType.NVarChar, 100, comment),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 255, reserved2),
				ParamSet.Add4Sql("@folderid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseSetNewFD", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//folderID = int.Parse(pData.GetParamValue("@folderid").ToString());
			}

			return iReturn;
		}

		#region 폴더 환경설정 정보 쿼리

		/// <summary>
		/// 폴더 환경설정 정보 쿼리(PH_FOLDER_SCHEMA 테이블)
		/// </summary>
		/// <param name="folderID"> 폴더 아이디 </param>
		/// <param name="inheritedOptioFolderID"> 옵션을 상속한 폴더 아이뒤 </param>
		/// <param name="inheritedDesignFolderID"> 디자인을 상속한 폴더 아이뒤 </param>
		/// <returns> 속성(Field)과, 속성값(FieldValue) </returns>
		public DataSet GetFolderEnvironmentInfo(int folderID, int inheritedOptioFolderID, int inheritedDesignFolderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@inheritedOptioFolderID", SqlDbType.Int, 4, inheritedOptioFolderID),
				ParamSet.Add4Sql("@inheritedDesignFolderID", SqlDbType.Int, 4, inheritedDesignFolderID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetFolderEnvironmentAllOfAttribute", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#endregion

		#region Option | Desing을 상속해주는 폴더 ID 가져오기

		/// <summary>
		///Option | Desing을 상속해주는 폴더 ID 가져오기(PH_FOLDER_SCHEMA 테이블,PH_FOLDER_INSTANCE 테이블)
		/// </summary>
		/// <param name="folderID"> 폴더 아이디 </param>
		/// <param name="inheritedType"> 상속 종류("Option" | "Design") </param>
		/// <returns>상속 폴더 아이디</returns>
		public int GetInheritedEnvironmentFolderID(int folderID, string inheritedType)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@InheritedKind", SqlDbType.VarChar, 10, inheritedType),
				ParamSet.Add4Sql("@ParentID", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetInheritedEnvironmentFolderID", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = Convert.ToInt32(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				//parentID = int.Parse(pData.GetParamValue("@ParentID").ToString());
			}

			return iReturn;
		}

		#endregion

		#region 폴더의 특정 환경설정 속성 가져오기
		
		/// <summary>
		/// 폴더의 특정 환경설정 속성 가져오기
		/// </summary>
		/// <param name="folderID"> 폴더 아이디 </param>
		/// <param name="fieldName"> 속성 필드 네임(DB-Field) </param>
		/// <returns> 속성값(FieldValue) </returns>
		public string GetFolderEnvironmentProperty(int folderID, string fieldName)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@fieldName", SqlDbType.VarChar, 100, fieldName),
				ParamSet.Add4Sql("@outParam", SqlDbType.VarChar, 1000, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_GetInheritedEnvironmentFolderID", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				//strReturn = pData.GetParamValue("@outParam").ToString();
			}

			return strReturn;
		}

		#endregion

		#region 폴더 환경설정 정보 생성 및 변경

		/// <summary>
		/// 폴더 환경설정 정보 생성 및 변경
		/// </summary>
		/// <param name="folderID"> 폴더 아이디 </param>
		/// <param name="fieldName"> 속성 필드 네임(DB-Field) </param>
		/// <param name="fieldValue"> 속성 필드 값(DB-FieldValue) </param>
		public void SetFolderEnvironmentProperty(int folderID, string fieldName, string fieldValue)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@fieldName", SqlDbType.VarChar, 30, fieldName),
				ParamSet.Add4Sql("@fieldValue", SqlDbType.VarChar, 1000, fieldValue)
			};

			ParamData pData = new ParamData("admin.ph_up_SetFolderEnvironmentAttribute", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		#endregion

		/// <summary>
		/// 즐겨찾기 폴더 제어
		/// </summary>
		/// <param name="type"></param>
		/// <param name="userID"></param>
		/// <param name="categoryID"></param>
		/// <param name="folderID"></param>
		/// <param name="parentID"></param>
		/// <param name="folderName"></param>
		/// <param name="sortKey"></param>
		public void HandleFavoriteFolder(string type, int userID, int categoryID, int folderID, int parentID, string folderName, int sortKey)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@type", SqlDbType.Char, 1, type),
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@categoryID", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@folderID", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@parentID", SqlDbType.Int, 4, parentID),
				ParamSet.Add4Sql("@foldername", SqlDbType.NVarChar, 100, folderName),
				ParamSet.Add4Sql("@sortkey", SqlDbType.Int, 4, sortKey)
			};

			ParamData pData = new ParamData("admin.ph_up_HandleFavoriteFolder", parameters);

			using (DbBase db = new DbBase())
			{
				string rt = db.ExecuteNonQueryTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}
		}

		/// <summary>
		/// 위치(Navigation)가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="selectedID"></param>
		/// <returns></returns>
		public DataSet GetFolderNavigationUrl(int domainID, int categoryID, string selectedID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@select_id", SqlDbType.VarChar, 63, selectedID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetFolderNavagation", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 트리 구조 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="selectedID"></param>
		/// <param name="selectedType"></param>
		/// <param name="expandedLevel"></param>
		/// <param name="userID"></param>
		/// <param name="openFolderInfo"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="objectType"></param>
		/// <param name="objectID"></param>
		/// <param name="extraInfo"></param>
		/// <returns></returns>
		public DataSet GetTreeObject(int domainID, int categoryID, string selectedID, string selectedType, int expandedLevel, int userID
								, string openFolderInfo, string isAdmin, string permission, int objectType, int objectID, string extraInfo)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@select_id", SqlDbType.VarChar, 63, selectedID),
				ParamSet.Add4Sql("@select_type", SqlDbType.VarChar, 2, selectedType),
				ParamSet.Add4Sql("@expandlevel", SqlDbType.SmallInt, 2, expandedLevel),
				ParamSet.Add4Sql("@cur_userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@open_fd", SqlDbType.VarChar, 63, openFolderInfo),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@permission", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@object_type", SqlDbType.Int, 4, objectType),
				ParamSet.Add4Sql("@object_id", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@extra_info", SqlDbType.VarChar, 50, extraInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_GetTreeObject", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 즐겨찾기 폴더
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="selectedID"></param>
		/// <param name="selectedType"></param>
		/// <param name="expandedLevel"></param>
		/// <param name="userID"></param>
		/// <param name="openFolderInfo"></param>
		/// <param name="isAdmin"></param>
		/// <param name="permission"></param>
		/// <param name="extraInfo"></param>
		/// <returns></returns>
		public DataSet GetFavoriteTreeObject(int domainID, int categoryID, string selectedID, string selectedType, int expandedLevel
										, int userID, string openFolderInfo, string isAdmin, string permission, string extraInfo)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@select_id", SqlDbType.VarChar, 63, selectedID),
				ParamSet.Add4Sql("@select_type", SqlDbType.VarChar, 2, selectedType),
				ParamSet.Add4Sql("@expandlevel", SqlDbType.SmallInt, 2, expandedLevel),
				ParamSet.Add4Sql("@cur_userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@open_fd", SqlDbType.VarChar, 63, openFolderInfo),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@permission", SqlDbType.VarChar, 20, permission),
				ParamSet.Add4Sql("@extra_info", SqlDbType.VarChar, 50, extraInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_GetTreeObject_Favorite", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
