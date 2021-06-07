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
	public class DomainDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public DomainDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public DomainDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// XML 형식으로 Authority 변경
		/// </summary>
		/// <param name="actionKind">Authority Type</param>
		/// <param name="authorityInfo">Authority Info</param>
		/// <returns></returns>
		public int ChangeBaseAuthority(string actionKind, string authorityInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@authority_info", SqlDbType.NText, authorityInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeAuthority", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// XML 형식으로 ACL 변경 
		/// </summary>	
		/// <param name="objectID">Object ID</param>
		/// <param name="objectType">Object Type</param>
		/// <param name="aclInfo">ACL Information</param>
		/// <returns></returns>
		public int ChangeBaseACL(int objectID, string objectType, string aclInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectID", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objectType", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@acl_info", SqlDbType.NText, aclInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeObjectACL", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// ACL 변경
		/// </summary>	
		/// <param name="objectID">Object ID</param>
		/// <param name="objectType">Object Type</param>
		/// <param name="targetID">Target ID</param>
		/// <param name="targetType">Target Type</param>
		/// <param name="scope">Scope</param>
		/// <param name="aclKind">ACL Kind Information</param>
		/// <param name="description">Description</param>
		/// <returns></returns>
		public int ChangeSpecialACL(int objectID, string objectType, int targetID, string targetType, string scope, string aclKind, string description)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@targettype", SqlDbType.Char, 2, targetType),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@aclkind", SqlDbType.VarChar, 20, aclKind),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 100, description)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeAcl", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// Grade Code Batch 일괄 생성
		/// </summary>	
		/// <param name="domainID">Domain ID</param>
		/// <param name="path">Excel File Location</param>
		/// <param name="retResult">Process Result(OK, MISSING, NOTUNIQUE, EXIST)</param>
		/// <returns></returns>
		public int CreateBaseGradeCodeFromExcel(int domainID, string path, out string retResult)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.SmallInt, 2, domainID),
				ParamSet.Add4Sql("@path", SqlDbType.NVarChar, 255, path),
				ParamSet.Add4Sql("@result", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseCreateGradeFromExcel", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				retResult = pData.GetParamValue("@result").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 관리자 설정을 위해 PH_AUTHORITY TABLE에 할당된 모든 권한 반환
		/// </summary>	
		/// <param name="actionKind"></param>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public DataSet GetBaseAuthority(string actionKind, int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetAuthority", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Auth Type DB Query
		/// </summary>	
		/// <param name="actionKind">Authority Type</param>
		/// <param name="scope">Scope</param>
		/// <returns></returns>
		public DataSet GetBaseAuthType(string actionKind, string scope)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetAuthType", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// CN 정보 가져오기
		/// </summary>		
		/// <param name="actionKind">CN Type</param>
		/// <param name="containerID">Container ID</param>
		/// <returns></returns>
		public DataSet GetBaseContainer(string actionKind, int containerID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@cn_id", SqlDbType.SmallInt, 2, containerID)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetCN", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Domain CT 정보 가져오기
		/// </summary>	
		/// <param name="actionKind">DomainContainer Type</param>
		/// <param name="domainID">Domain ID</param>
		/// <param name="categoryID">Category ID</param>
		/// <returns></returns>
		public DataSet GetDomainCategoryInfo(string actionKind, int domainID, int categoryID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID)
			};

			ParamData pData = new ParamData("admin.ph_up_DomainGetCT", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Grade Code DB Query
		/// </summary>	
		/// <param name="actionKind">Grade Type</param>
		/// <param name="domainID">Domain ID</param>
		/// <param name="codeType">CodeType</param>
		/// <returns></returns>
		public DataSet GetBaseGradeCode(string actionKind, int domainID, string codeType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@codetype", SqlDbType.Char, 1, codeType)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetGradeCode", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 설정된 ACL 정보 가져오기
		/// </summary>	
		/// <param name="domainID">Domain ID</param>
		/// <param name="inherited">Inherited</param>
		/// <param name="objectID">Object ID</param>
		/// <param name="objectType">Object Type</param>
		/// <param name="scope">Scope</param>
		/// <returns></returns>
		public DataSet GetObjectACL(int domainID, string inherited, int objectID, string objectType, string scope)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope)
			};

			ParamData pData = new ParamData("admin.ph_up_GetObjectACL", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 지정된 Target, AUAlias에 대한 Authority 지정 Object 정보 가져오기
		/// </summary>	
		/// <param name="domainID">Domain ID</param>
		/// <param name="auth">Auth</param>
		/// <param name="targetID">Target ID</param>
		/// <param name="targetType">Target Type</param>
		/// <returns></returns>
		public DataSet GetObjectAuthority(int domainID, string auth, int targetID, string targetType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@auth", SqlDbType.NVarChar, 500, auth),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@targettype", SqlDbType.Char, 2, targetType)
			};

			ParamData pData = new ParamData("admin.ph_up_GetObjectAuthority", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// Object CT가져오기
		/// </summary>	
		/// <param name="actionKind">Category Type</param>
		/// <returns></returns>
		public DataSet GetBaseCategory(string actionKind)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetCT", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 접근하려는 객체에 대한 권한을 가져오기
		/// </summary>
		/// <param name="domainID">Domain ID</param>
		/// <param name="userID">User ID</param>
		/// <param name="objectID">Object ID</param>
		/// <param name="objectType">Object Type</param>
		/// <param name="scope">Scope</param>
		/// <returns></returns>
		public string GetObjectPermission(int domainID, int userID, int objectID, string objectType, string scope)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@permission", SqlDbType.VarChar, 20, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetPermission", parameters);

			using (DbBase db = new DbBase())
			{
				db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				strReturn = pData.GetParamValue("@permission").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// Grade Code 생성(단일), 수정, 삭제
		/// </summary>	
		/// <param name="actionKind">Code Type</param>
		/// <param name="domainID">Domain ID</param>
		/// <param name="type">Old Grade Type</param>
		/// <param name="code">Old Grade Code</param>
		/// <param name="newType">New Grade Type</param>
		/// <param name="newCode">New Grade Code</param>
		/// <param name="codeName">Grade Code Name</param>
		/// <param name="inUse">Using[Y/N]</param>
		/// <param name="removeInfo">Remove Info</param>
		/// <returns></returns>
		public int HandleBaseGradeCode(string actionKind, int domainID, string type, string code, string newType, string newCode
		, string codeName, string inUse, string removeInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 10, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.SmallInt, 2, domainID),
				ParamSet.Add4Sql("@type", SqlDbType.Char, 1, type),
				ParamSet.Add4Sql("@code", SqlDbType.VarChar, 3, code),
				ParamSet.Add4Sql("@new_type", SqlDbType.Char, 1, newType),
				ParamSet.Add4Sql("@new_code", SqlDbType.VarChar, 3, newCode),
				ParamSet.Add4Sql("@codename", SqlDbType.NVarChar, 50, codeName),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@remove_info", SqlDbType.NText, removeInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGradeCodeHandler", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// ACL 제거
		/// </summary>	
		/// <param name="objectID">Object ID</param>
		/// <param name="objectType">Object Type</param>
		/// <param name="targetID">Target ID</param>
		/// <param name="targetType">Target Type</param>
		/// <returns></returns>
		public int RemoveObjectACL(int objectID, string objectType, int targetID, string targetType)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@targettype", SqlDbType.Char, 2, targetType)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectRemoveAcl", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// DomainCT 생성, 변경, 삭제
		/// </summary>
		/// <param name="actionKind">Category Type</param>
		/// <param name="domainID">Domain ID</param>
		/// <param name="categoryID">Category ID</param>
		/// <param name="prevPosition">Old Position</param>
		/// <param name="position">New Position</param>
		/// <param name="memberOf">Member Of</param>
		/// <param name="displayName">Display Name</param>
		/// <param name="regionName">Region Name</param>
		/// <param name="inUse">Using</param>
		/// <param name="inherited">Inherited</param>
		/// <param name="sortKey">SortKey</param>
		/// <param name="reserved1">Reserved Field</param>
		/// <param name="reserved2">Reserved Field</param>
		/// <returns></returns>
		public int CreateDomainCategory(string actionKind, int domainID, int categoryID, string prevPosition, string position, int memberOf
		, string displayName, string jadisplayName, string endisplayName, string chdisplayName, string regionName, string inUse, string inherited, int sortKey, string reserved1, string reserved2)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 10, actionKind),
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID),
				ParamSet.Add4Sql("@prev_position", SqlDbType.Char, 1, prevPosition),
				ParamSet.Add4Sql("@position", SqlDbType.Char, 1, position),
				ParamSet.Add4Sql("@memberof", SqlDbType.SmallInt, 2, memberOf),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 50, displayName),
				ParamSet.Add4Sql("@jadisplayname", SqlDbType.NVarChar, 50, jadisplayName),
				ParamSet.Add4Sql("@endisplayname", SqlDbType.NVarChar, 50, endisplayName),
				ParamSet.Add4Sql("@chdisplayname", SqlDbType.NVarChar, 50, chdisplayName),
				ParamSet.Add4Sql("@regionname", SqlDbType.NVarChar, 50, regionName),
				ParamSet.Add4Sql("@inuse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@inherited", SqlDbType.Char, 1, inherited),
				ParamSet.Add4Sql("@sortkey", SqlDbType.TinyInt, 1, sortKey),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 100, reserved1),
				ParamSet.Add4Sql("@reserved2", SqlDbType.NVarChar, 255, reserved2)
			};

			ParamData pData = new ParamData("admin.ph_up_DomainSetCT", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// ACL 설정 - batch로
		/// </summary>	
		/// <param name="xmlData">Acl Info By XML</param>
		/// <returns></returns>
		public int CreateObjectACLByXML(string xmlData)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewAcl", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// CN 생성, 변경, 삭제
		/// </summary>
		/// <param name="actionKind">Container Type</param>
		/// <param name="containerID">Container ID</param>
		/// <param name="memberOf">Member Of</param>
		/// <param name="displayName">DisplayName</param>
		/// <param name="sortKey">SortKey</param>
		/// <param name="command">Command</param>
		/// <param name="description">Description</param>
		/// <param name="outContainerID">ContainerID</param>
		/// <returns></returns>
		public int CreateBaseContainer(string actionKind, int containerID, int memberOf, string displayName, int sortKey, string command, string description, out int outContainerID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@cn_id", SqlDbType.SmallInt, 2, containerID),
				ParamSet.Add4Sql("@memberof", SqlDbType.SmallInt, 2, memberOf),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 20, displayName),
				ParamSet.Add4Sql("@sortkey", SqlDbType.TinyInt, 1, sortKey),
				ParamSet.Add4Sql("@command", SqlDbType.NVarChar, 255, command),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 255, description),
				ParamSet.Add4Sql("@new_id", SqlDbType.SmallInt, 2, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseSetCN", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				outContainerID = int.Parse(pData.GetParamValue("@new_id").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// Object CT 생성, 수정
		/// </summary>
		/// <param name="ctID">수정할 메뉴 아이디(0:새 매뉴 생성)</param>
		/// <param name="ctAlias">매뉴 별칭</param>
		/// <param name="description">매뉴 설명</param>
		/// <param name="url">경로</param>
		/// <param name="reserved1">업무대상</param>
		/// <returns></returns>
		public int SetObjectCT(int ctID, string ctAlias, string description, string url, string reserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, ctID),
				ParamSet.Add4Sql("@ctalias", SqlDbType.VarChar, 30, ctAlias),
				ParamSet.Add4Sql("@url", SqlDbType.NVarChar, 255, url),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 100, description),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseSetObjectCT", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}


		/// <summary>
		/// Object CT 삭제
		/// </summary>
		/// <param name="ctID">메뉴 아이디</param>
		/// <returns></returns>
		public int RemoveObjectCT(int ctID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, ctID)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseRemoveObjectCT", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// Domain CT 생성 및 수정, 삭제
		/// </summary>	
		/// <param name="actionKind"></param>
		/// <param name="categoryInfo"></param>
		/// <param name="aclInfo"></param>
		/// <param name="adminInfo"></param>
		/// <returns></returns>
		public int HandleBaseDomainCategory(string actionKind, string categoryInfo, string aclInfo, string adminInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 10, actionKind),
				ParamSet.Add4Sql("@ct_Info", SqlDbType.NText, categoryInfo),
				ParamSet.Add4Sql("@acl_info", SqlDbType.NText, aclInfo),
				ParamSet.Add4Sql("@admin_Info", SqlDbType.NText, adminInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseDomainCTHandler", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// CT 생성, 변경, 삭제
		/// </summary>	
		/// <param name="actionKind">Category Type</param>
		/// <param name="categoryID">Category ID</param>
		/// <param name="categoryAlias">Category Alias</param>
		/// <param name="url">URL</param>
		/// <param name="description">Description</param>
		/// <param name="reserved1">Reserved Field</param>
		/// <param name="outCategoryID">Category ID</param>
		/// <returns></returns>
		public int CreateBaseCategory(string actionKind, int categoryID, string categoryAlias, string url, string description, string reserved1, out int outCategoryID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@ct_id", SqlDbType.SmallInt, 2, categoryID),
				ParamSet.Add4Sql("@ctalias", SqlDbType.VarChar, 30, categoryAlias),
				ParamSet.Add4Sql("@url", SqlDbType.NVarChar, 255, url),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 100, description),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@new_id", SqlDbType.SmallInt, 2, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseSetCT", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				outCategoryID = int.Parse(pData.GetParamValue("@new_id").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 로그온 정보 가져오기
		/// </summary>
		/// <param name="logonID"></param>
		/// <param name="logonIP"></param>
		/// <param name="logonBrowser"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public DataSet GetLogonUserInfo(string logonID, string logonIP, string logonBrowser, string password)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@urAccount", SqlDbType.VarChar, 30, logonID),
				ParamSet.Add4Sql("@ip", SqlDbType.VarChar, 30, logonIP),
				ParamSet.Add4Sql("@browser", SqlDbType.VarChar, 200, logonBrowser),
				ParamSet.Add4Sql("@password", SqlDbType.VarChar, 100, password)
			};

			ParamData pData = new ParamData("admin.ph_up_LogonGetUserInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 관리툴에서 이벤트 로그(로그온 정보, 문서등록로그, 문서조회로그) 쿼리
		/// </summary>
		/// <param name="eventViewName">Event Name</param>
		/// <param name="pageIndex">PageNo</param>
		/// <param name="pageCount">PageCount</param>
		/// <param name="sortColumn">Sort Column</param>
		/// <param name="sortType">Sort Type</param>
		/// <param name="searchColumn">Search Column</param>
		/// <param name="searchText">Search Text</param>
		/// <param name="searchStartDate">SearchStartDate</param>
		/// <param name="searchEndDate">SearchEndDate</param>
		/// <param name="totalMsg">TotalMessage</param>
		/// <returns></returns>
		public DataSet GetBaseEventLogList(string eventViewName, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMsg)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@eventViewName", SqlDbType.VarChar, 50, eventViewName),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@count", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@searchStartDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchEndDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@totalMessages", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetEventLogList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMsg = int.Parse(pData.GetParamValue("@totalMessages").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// Schedule에서의 권한 설정
		/// </summary>
		/// <param name="mode">Acl Type</param>
		/// <param name="objectID">Object ID</param>
		/// <param name="objectType">Object Type</param>
		/// <param name="scope">Scope</param>
		/// <param name="xmlData">Acl Info</param>
		/// <returns></returns>
		public int CreateObjectSingleACL(string mode, int objectID, string objectType, string scope, string xmlData)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@objectID", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objectType", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@scope", SqlDbType.Char, 1, scope),
				ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSingleSetACL", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 권한 추가, 삭제, 수정을 수행한다.
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="objectType"></param>
		/// <param name="aclInfo"></param>
		/// <returns></returns>
		public int ChangeObjectACL(int objectID, string objectType, string aclInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectID", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objectType", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@acl_info", SqlDbType.NText, aclInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeObjectACL", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// XML 정보를 각기 구성하여 저장
		/// </summary>
		/// <param name="hasNewAcl">Has New Acl</param>
		/// <param name="newAclXML">New Acl Info By XML</param>
		/// <param name="hasChangeAcl">Has Change Acl</param>
		/// <param name="changeAclXML">Change Acl Info By XML</param>
		/// <param name="hasDeleteAcl">Has Delete Acl</param>
		/// <param name="DeleteAclXML">Delete Acl Info By XML</param>
		/// <returns></returns>
		public int ChangeObjectACLByXML(string hasNewAcl, string newAclXML, string hasChangeAcl, string changeAclXML, string hasDeleteAcl, string DeleteAclXML)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@HasNewAcl", SqlDbType.Char, 1, hasNewAcl),
				ParamSet.Add4Sql("@NewAclXML", SqlDbType.NText, newAclXML),
				ParamSet.Add4Sql("@HasChangeAcl", SqlDbType.Char, 1, hasChangeAcl),
				ParamSet.Add4Sql("@ChangeAclXML", SqlDbType.NText, changeAclXML),
				ParamSet.Add4Sql("@HasDeleteAcl", SqlDbType.Char, 1, hasDeleteAcl),
				ParamSet.Add4Sql("@DeleteAclXML", SqlDbType.NText, DeleteAclXML)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeAclByXML", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 도메인별 디렉터리 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetDirectoryListByDomain(int domainID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.NVarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.NVarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 300, searchText),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectoryGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// Authority 변경
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="objectType"></param>
		/// <param name="targetID"></param>
		/// <param name="targetType"></param>
		/// <param name="auAlias"></param>
		/// <param name="description"></param>
		/// <returns></returns>
		public int ChangeObjectAuthority(int objectID, string objectType, int targetID, string targetType, string auAlias, string description)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@targettype", SqlDbType.Char, 2, targetType),
				ParamSet.Add4Sql("@aualias", SqlDbType.VarChar, 20, auAlias),
				ParamSet.Add4Sql("@description", SqlDbType.VarChar, 100, description)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeAuthority", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// Authority 삭제
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="objectType"></param>
		/// <param name="targetID"></param>
		/// <param name="targetType"></param>
		/// <returns></returns>
		public int DeleteObjectAuthority(int objectID, string objectType, int targetID, string targetType)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@targettype", SqlDbType.Char, 2, targetType)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectRemoveAuthority", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// Authority 생성
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="objectType"></param>
		/// <param name="targetID"></param>
		/// <param name="targetType"></param>
		/// <param name="auAlias"></param>
		/// <param name="description"></param>
		/// <returns></returns>
		public int CreateObjectAuthority(int objectID, string objectType, int targetID, string targetType, string auAlias, string description)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectid", SqlDbType.Int, 4, objectID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.VarChar, 30, objectType),
				ParamSet.Add4Sql("@targetid", SqlDbType.Int, 4, targetID),
				ParamSet.Add4Sql("@targettype", SqlDbType.Char, 2, targetType),
				ParamSet.Add4Sql("@aualias", SqlDbType.VarChar, 20, auAlias),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 100, description)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewAuthority", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 관리툴에서 고객사 리스트 쿼리
		/// </summary>
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
		public DataSet GetASPCompanyList(int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
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

			ParamData pData = new ParamData("admin.ph_up_GetServiceCompanyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// XML 형식으로 ASP 고객사 등록, 변경
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="ComInfoXMLText"></param>
		/// <returns></returns>
		public int SetCompanyInfo(string cmd, string ComInfoXMLText)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@cmd", SqlDbType.VarChar, 30, cmd),
				ParamSet.Add4Sql("@com_info", SqlDbType.NText, ComInfoXMLText)
			};

			ParamData pData = new ParamData("admin.ph_up_HDCompany", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companycode"></param>
		/// <returns></returns>
		public DataSet GetASPRegistCount(string companycode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode)
			};

			ParamData pData = new ParamData("admin.ph_up_GetPerLogonCount", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="companycode"></param>
		/// <returns></returns>
		public DataSet GetASPCompanyAtt(string companycode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode)
			};

			ParamData pData = new ParamData("admin.ph_up_GetServiceCompanyAtt", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
		
		/// <summary>
		/// ASP Server DB Query
		/// </summary>	
		/// <param name="domainID">Domain ID</param>
		/// <returns></returns>
		public DataSet GetServiceServerName(int domainID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domain", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetServiceServerName", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 고객사 코드 중복체크
		/// </summary>	
		/// <param name="domainID">CompanyCode</param>
		/// <param name="retResult">Process Result(OK, EXIST)</param>
		/// <returns></returns>
		public int DuplicationChk(string companycode, out string retResult)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode),
				ParamSet.Add4Sql("@result", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DupCompanyCode", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				retResult = pData.GetParamValue("@result").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// ASP Server DB Query
		/// </summary>	
		/// <param name="domainID">Domain ID</param>
		/// <returns></returns>
		public DataSet GetReSessionInfo(string companycode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@companycode", SqlDbType.VarChar, 30, companycode)
			};

			ParamData pData = new ParamData("admin.ph_up_ReSession", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="domainID"></param>
		/// <param name="fieldName"></param>
		/// <returns></returns>
		public DataSet GetDomainConfiguration(string mode, int domainID, string fieldName)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 3, mode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@fieldname", SqlDbType.VarChar, 30, fieldName)
			};

			ParamData pData = new ParamData("admin.ph_up_DomainGetConfiguration", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public DataSet GetPasswordPolicy()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				
			};

			ParamData pData = new ParamData("admin.ph_up_DomainGetPasswordPolicy", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="changeitem"></param>
		/// <param name="changevalue"></param>
		/// <returns></returns>
		public int SetPasswordPolicy(string changeitem, string changevalue)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@changeitem", SqlDbType.VarChar, 30, changeitem),
				ParamSet.Add4Sql("@changevalue", SqlDbType.VarChar, 2, changevalue)
			};

			ParamData pData = new ParamData("admin.ph_up_SetPasswordPolicy", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// CN, CT 트리 구조 가져오기
		/// </summary>		
		/// <param name="domainID"></param>
		/// <param name="selectedType"></param>
		/// <param name="expandedLevel"></param>
		/// <param name="selectedID"></param>
		/// <returns></returns>
		public DataSet GetBaseTreeObject(int domainID, string selectedType, int expandedLevel, int selectedID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@objecttype", SqlDbType.Char, 2, selectedType),
				ParamSet.Add4Sql("@expandlevel", SqlDbType.SmallInt, 2, expandedLevel),
				ParamSet.Add4Sql("@selected", SqlDbType.Int, 4, selectedID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetTreeObjectForAdmin", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 메뉴 가져오기
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="categoryID"></param>
		/// <param name="userID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="position"></param>
		/// <param name="language"></param>
		/// <returns></returns>
		public DataSet GetMenuList(int domainID, int categoryID, int userID, string isAdmin, string position, string language)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@ct_id", SqlDbType.Int, 4, categoryID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@pos", SqlDbType.Char, 1, position),
				ParamSet.Add4Sql("@language", SqlDbType.VarChar, 2, language)
			};

			ParamData pData = new ParamData("admin.ph_up_MenuGetList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
