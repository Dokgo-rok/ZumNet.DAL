using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZumNet.DAL.Base;

namespace ServiceDac
{
	public class GroupDac : DacBase
	{
		/// <summary>
		/// 구성원,소속 및 하위그룹 관리
		/// </summary>		
		public int ChangeBaseGroupNestedInfo(int domainID, int groupID, string groupType, string changedInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.TinyInt, 1, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@change_Info", SqlDbType.NText, changedInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeGroupNestedInfo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 그룹 정보 생성 : 기본 그룹 생성 및 포함 관계 정보 생성
		/// </summary>		
		public int CreateBaseGroup(int domainID, string groupType, string groupAlias, string parentAlias, string groupName, string groupShortName, int sortKey, string pdmgrcode, out int groupID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@gralias", SqlDbType.VarChar, 30, groupAlias),
				ParamSet.Add4Sql("@parentalias", SqlDbType.VarChar, 30, parentAlias),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, groupName),
				ParamSet.Add4Sql("@shortname", SqlDbType.NVarChar, 100, groupShortName),
				ParamSet.Add4Sql("@sortkey", SqlDbType.SmallInt, 2, sortKey),
				ParamSet.Add4Sql("@pdmgralias", SqlDbType.VarChar, 30, pdmgrcode),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseCreateGR", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				groupID = int.Parse(pData.GetParamValue("@gr_id").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 동일그룹유형에 이미 속해진 사용자 반환
		/// </summary>		
		public DataSet GetBaseExcludedGroupMembers(string memeberInfo, string groupType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@addmember_info", SqlDbType.NText, memeberInfo),
				ParamSet.Add4Sql("@grType", SqlDbType.Char, 1, groupType)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetExcludedGRMember", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 정보 DB Query (관리)
		/// </summary>		
		public DataSet GetBaseGroupInfo(int groupID, string viewDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@groupid", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetGroupInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 소속원 가져오기
		/// </summary>		
		public DataSet GetGroupMembers(string domainID, string groupID, string viewDate, string sort, string order, string admin)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
				ParamSet.Add4Sql("@sort", SqlDbType.VarChar, 10, sort),
				ParamSet.Add4Sql("@order", SqlDbType.VarChar, 5, order),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, admin)
			};

			ParamData pData = new ParamData("admin.ph_up_OrgGetGroupMembers", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹유형 DB Query
		/// </summary>		
		public DataSet GetBaseGroupType()
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetGRType", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 삭제시 구성원과 하위 그룹의 가지고 있는 DB Query (관리)
		/// </summary>		
		public DataSet GetBaseIrremovableGroups(string groupInfo)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@group_Info", SqlDbType.NText, groupInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetIrremovableGroups", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 트리 구조 만들기
		/// </summary>	
		public DataSet GetOrgGroupTree(int domainID, int memberOf, string groupType, int groupID, string viewDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@memberof", SqlDbType.Int, 4, memberOf),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, "")
			};

			ParamData pData = new ParamData("admin.ph_up_OrgGetGroupTree", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 동일 그룹 유형의 상위 그룹 Alias 쿼리
		/// </summary>		
		public DataSet GetBaseParentGroupAlias(string actionKind, int groupID, string groupType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 1, actionKind),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@grType", SqlDbType.Char, 1, groupType)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetParentGRAlias", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 생성 및 수정(관리)
		/// </summary>		
		public int HandleBaseGroup(string actionKind, string groupInfo, string changeInfo, string ownershipInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 10, actionKind),
				ParamSet.Add4Sql("@group_Info", SqlDbType.NText, groupInfo),
				ParamSet.Add4Sql("@change_Info", SqlDbType.NText, changeInfo),
				ParamSet.Add4Sql("@ownership_info", SqlDbType.NText, ownershipInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGRHandler", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 삭제 부서 List Query
		/// </summary>		
		public DataSet GetBaseDeletedGroups(string domainID, string groupType, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.VarChar, 3, domainID),
				ParamSet.Add4Sql("@grType", SqlDbType.Char, 1, groupType),
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

			ParamData pData = new ParamData("admin.ph_up_BaseGetDeletedGroupList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMessages").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 도메인 그룹 찾아오기
		/// </summary>		
		public DataSet SearchDomainGroups(string domainID, string memberOf, string groupType, string sortColumn, string sortType, string searchText, string admin)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.VarChar, 3, domainID),
				ParamSet.Add4Sql("@memberof", SqlDbType.VarChar, 20, memberOf),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 50, sortColumn),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 1000, searchText),
				ParamSet.Add4Sql("@admin_tool", SqlDbType.Char, 1, admin)
			};

			ParamData pData = new ParamData("admin.ph_up_DomainSearchGroups", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 정보 변경
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		/// <param name="memberOf"></param>
		/// <param name="groupType"></param>
		/// <param name="policy"></param>
		/// <param name="inUse"></param>
		/// <param name="displayName"></param>
		/// <param name="desciption"></param>
		/// <returns></returns>
		public int ChangeObjectGroup(int domainID, int folderID, int groupID, int memberOf, string groupType, string policy, string inUse, string displayName, string desciption)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@memberof", SqlDbType.Int, 4, memberOf),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@policy", SqlDbType.Char, 1, policy),
				ParamSet.Add4Sql("@InUse", SqlDbType.Char, 1, inUse),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, displayName),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 100, desciption)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeGr", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹 삭제
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupID"></param>
		/// <param name="memberOf"></param>
		/// <returns></returns>
		public int DeleteObjectGR(int domainID, int groupID, int memberOf)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@DN_ID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@MemberOf", SqlDbType.Int, 4, memberOf)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectRemoveGr", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 해당 Alias에 대한 중복 여부
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <returns></returns>
		public int GetObjectIntegrityGroup(string xfAlias, out int number)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@alias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@num", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetIntegrityGr", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				number = int.Parse(pData.GetParamValue("@num").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹 정보
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="folderID"></param>
		/// <returns></returns>
		public DataSet GetObjectGroupInfo(int groupID, int folderID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetGrInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹에 속한 사용자 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="folderID"></param>
		/// <param name="groupID"></param>
		/// <param name="parentACL"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="searchStartDate"></param>
		/// <param name="searchEndDate"></param>
		/// <param name="condition"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetObjectGroupMemberList(int domainID, int folderID, int groupID, string parentACL, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, string condition, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@fd_id", SqlDbType.Int, 4, folderID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@parentacl", SqlDbType.VarChar, 6, parentACL),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.VarChar, 200, searchText),
				ParamSet.Add4Sql("@searchstartDate", SqlDbType.VarChar, 10, searchStartDate),
				ParamSet.Add4Sql("@searchendDate", SqlDbType.VarChar, 10, searchEndDate),
				ParamSet.Add4Sql("@condition", SqlDbType.VarChar, 50, condition),
				ParamSet.Add4Sql("@totalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetGrMemberList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@totalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 특정그룹의 회원 활동현황 조회(동호회에서 동호회 회원 활동 현황 조회)
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <returns></returns>
		public DataSet GetObjectGroupMemberStatistic(int groupID, string sortColumn, string sortType)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType)
			};

			ParamData pData = new ParamData("admin.ph_up_ClubGetMemberStatistic", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetObjectGroupVisitCount(int groupID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_VisitGetCount", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹의 멤버수
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetObjectGroupUserCount(int groupID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetGrUserCount", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 생성
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="groupType"></param>
		/// <param name="memberOf"></param>
		/// <param name="groupAlias"></param>
		/// <param name="policy"></param>
		/// <param name="displayName"></param>
		/// <param name="description"></param>
		/// <param name="reserved1"></param>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public int CreateObjectGroup(int domainID, string groupType, int memberOf, string groupAlias, string policy, string displayName, string description, string reserved1, out int groupID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@memberof", SqlDbType.Int, 4, memberOf),
				ParamSet.Add4Sql("@gralias", SqlDbType.VarChar, 30, groupAlias),
				ParamSet.Add4Sql("@policy", SqlDbType.Char, 1, policy),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, displayName),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 100, description),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 256, reserved1),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewGR", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				groupID = int.Parse(pData.GetParamValue("@gr_id").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹 정책 변경
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="join"></param>
		/// <param name="leave"></param>
		/// <param name="isPublic"></param>
		/// <param name="description"></param>
		/// <param name="joinMessage"></param>
		/// <param name="leaveMessage"></param>
		/// <returns></returns>
		public int ChangeObjectGroupPolicy(int groupID, string join, string leave, string isPublic, string description, string joinMessage, string leaveMessage)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@Join", SqlDbType.Char, 1, join),
				ParamSet.Add4Sql("@Leave", SqlDbType.Char, 1, leave),
				ParamSet.Add4Sql("@Public", SqlDbType.Char, 1, isPublic),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 4000, description),
				ParamSet.Add4Sql("@JoinMsg", SqlDbType.NVarChar, 500, joinMessage),
				ParamSet.Add4Sql("@LeaveMsg", SqlDbType.NVarChar, 500, leaveMessage)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeGrPolicy", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹 정책 설정
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="join"></param>
		/// <param name="leave"></param>
		/// <param name="isPublic"></param>
		/// <param name="description"></param>
		/// <param name="joinMessage"></param>
		/// <param name="leaveMessage"></param>
		/// <param name="reserved1"></param>
		/// <returns></returns>
		public int CreateObjectGroupPolicy(int groupID, string join, string leave, string isPublic, string description, string joinMessage, string leaveMessage, string reserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@Join", SqlDbType.Char, 1, join),
				ParamSet.Add4Sql("@Leave", SqlDbType.Char, 1, leave),
				ParamSet.Add4Sql("@Public", SqlDbType.Char, 1, isPublic),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 4000, description),
				ParamSet.Add4Sql("@JoinMsg", SqlDbType.NVarChar, 500, joinMessage),
				ParamSet.Add4Sql("@LeaveMsg", SqlDbType.NVarChar, 500, leaveMessage),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 100, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewGrPolicy", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// PH_OBJECT_GR 테이블의 Display Name변경
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="groupType"></param>
		/// <param name="displayName"></param>
		/// <param name="shortName"></param>
		/// <returns></returns>
		public int ChangeBaseGroupDisplayName(int groupID, string groupType, string displayName, string shortName)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 200, displayName),
				ParamSet.Add4Sql("@shortname", SqlDbType.NVarChar, 100, shortName)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeGRDisplayName", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹 삭제
		/// </summary>
		/// <param name="groupInfo"></param>
		/// <returns></returns>
		public int DeleteBaseGroups(string groupInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@group_Info", SqlDbType.NText, groupInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseDeleteGRs", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 공동 작성자 그룹 리스트를 가져온다.
		/// </summary>
		/// <param name="searchText"></param>
		/// <param name="searchGroup"></param>
		/// <returns></returns>
		public DataSet GetObjectSharedGroup(string searchText, string searchGroup)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@SearchText", SqlDbType.VarChar, 20, searchText),
				ParamSet.Add4Sql("@SearchGroupType", SqlDbType.VarChar, 5, searchGroup)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetShardGroup", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹의 상위그룹라인 쿼리
		/// </summary>		
		public DataSet GetBaseGroupParentLine(int domainID, string groupType, int groupID, int parentID, string viewDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@parent_id", SqlDbType.Int, 4, parentID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetParentGroupLine", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 그룹 트리 구조 만들기
		/// </summary>		
		public DataSet GetOrgGroupTree(int domainID, int memberOf, string groupType, int groupID, string viewDate, string admin)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@memberof", SqlDbType.Int, 4, memberOf),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, admin)
			};

			ParamData pData = new ParamData("admin.ph_up_OrgGetGroupTree", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
