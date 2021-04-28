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
	public class UserDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public UserDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public UserDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// Check Duble Alias Check
		/// </summary>		
		public int CheckBaseDoubleAlias(string objectAlias, string objectType, out string strReturn)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@object_alias", SqlDbType.VarChar, 30, objectAlias),
				ParamSet.Add4Sql("@object_type", SqlDbType.VarChar, 20, objectType),
				ParamSet.Add4Sql("@result", SqlDbType.Char, 1, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseAliasDblCheck", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				strReturn = pData.GetParamValue("@result").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userid"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public int SetPasswordChange(string userid, string password)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, Convert.ToInt32(userid)),
				ParamSet.Add4Sql("@password", SqlDbType.VarChar, 100, password)
			};

			ParamData pData = new ParamData("admin.ph_up_SetPWDate", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 계정 생성 : 필수 데이타와 그룹멤버로 생성(Execl로 배치작업시 사용)
		/// </summary>		
		public int CreateBaseUser(string logonID, string mailAccount, string domainAlias, string displayName, string employeeID, string isLive, string mailServer, string ouName, string longName, string firstName, string lastName, string joinDate, string groupAlias, string groupID, string role, string gradeCode1, string gradeCode2, string gradeCode3, string gradeCode4, string gradeCode5, string personNo1, string personNo2, string birth, string birthType, string mobile, string telephone, string fax, string homePhone, string homePage, string zipCode1, string address1, string detailAddress1, string company, string zipCode2, string address2, string detailAddress2, string themePath, string keyword1, string keyword2, string keyword3, string keyword4, string keyword5, string keyword6, string keyword7, string isInsa, string secondMail, string isgw, string ispdm, string iserp, string ismsg, out int userID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 30, logonID),
				ParamSet.Add4Sql("@mailaccount", SqlDbType.VarChar, 30, mailAccount),
				ParamSet.Add4Sql("@dnalias", SqlDbType.VarChar, 63, domainAlias),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 100, displayName),
				ParamSet.Add4Sql("@empid", SqlDbType.VarChar, 30, employeeID),
				ParamSet.Add4Sql("@islive", SqlDbType.Char, 1, isLive),
				ParamSet.Add4Sql("@mailserver", SqlDbType.VarChar, 50, mailServer),
				ParamSet.Add4Sql("@ouname", SqlDbType.NVarChar, 50, ouName),
				ParamSet.Add4Sql("@longname", SqlDbType.NVarChar, 100, longName),
				ParamSet.Add4Sql("@fname", SqlDbType.NVarChar, 50, firstName),
				ParamSet.Add4Sql("@lname", SqlDbType.NVarChar, 50, lastName),
				ParamSet.Add4Sql("@indate", SqlDbType.Char, 10, joinDate),
				ParamSet.Add4Sql("@gralias", SqlDbType.VarChar, 30, groupAlias),
				ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@role", SqlDbType.VarChar, 20, role),
				ParamSet.Add4Sql("@gradecode1", SqlDbType.VarChar, 10, gradeCode1),
				ParamSet.Add4Sql("@gradecode2", SqlDbType.VarChar, 10, gradeCode2),
				ParamSet.Add4Sql("@gradecode3", SqlDbType.VarChar, 10, gradeCode3),
				ParamSet.Add4Sql("@gradecode4", SqlDbType.VarChar, 10, gradeCode4),
				ParamSet.Add4Sql("@gradecode5", SqlDbType.VarChar, 10, gradeCode5),
				ParamSet.Add4Sql("@personno1", SqlDbType.VarChar, 20, personNo1),
				ParamSet.Add4Sql("@personno2", SqlDbType.VarChar, 63, personNo2),
				ParamSet.Add4Sql("@birth", SqlDbType.Char, 10, birth),
				ParamSet.Add4Sql("@birthtype", SqlDbType.Char, 1, birthType),
				ParamSet.Add4Sql("@mobile", SqlDbType.VarChar, 30, mobile),
				ParamSet.Add4Sql("@telephone", SqlDbType.VarChar, 30, telephone),
				ParamSet.Add4Sql("@fax", SqlDbType.VarChar, 30, fax),
				ParamSet.Add4Sql("@homephone", SqlDbType.VarChar, 30, homePhone),
				ParamSet.Add4Sql("@homepage", SqlDbType.NVarChar, 255, homePage),
				ParamSet.Add4Sql("@zipcode1", SqlDbType.VarChar, 10, zipCode1),
				ParamSet.Add4Sql("@address1", SqlDbType.NVarChar, 100, address1),
				ParamSet.Add4Sql("@detailaddress1", SqlDbType.NVarChar, 100, detailAddress1),
				ParamSet.Add4Sql("@company", SqlDbType.NVarChar, 100, company),
				ParamSet.Add4Sql("@zipcode2", SqlDbType.VarChar, 10, zipCode2),
				ParamSet.Add4Sql("@address2", SqlDbType.NVarChar, 100, address2),
				ParamSet.Add4Sql("@detailaddress2", SqlDbType.NVarChar, 100, detailAddress2),
				ParamSet.Add4Sql("@themepath", SqlDbType.TinyInt, 1, themePath),
				ParamSet.Add4Sql("@keyword1", SqlDbType.NVarChar, 100, keyword1),
				ParamSet.Add4Sql("@keyword2", SqlDbType.NVarChar, 100, keyword2),
				ParamSet.Add4Sql("@keyword3", SqlDbType.NVarChar, 100, keyword3),
				ParamSet.Add4Sql("@keyword4", SqlDbType.NVarChar, 100, keyword4),
				ParamSet.Add4Sql("@keyword5", SqlDbType.NVarChar, 100, keyword5),
				ParamSet.Add4Sql("@keyword6", SqlDbType.NVarChar, 100, keyword6),
				ParamSet.Add4Sql("@keyword7", SqlDbType.NVarChar, 100, keyword7),
				ParamSet.Add4Sql("@isInsa", SqlDbType.Char, 1, isInsa),
				ParamSet.Add4Sql("@secondmail", SqlDbType.VarChar, 30, secondMail),
				ParamSet.Add4Sql("@sort_key", SqlDbType.VarChar, 100, "0"),
				ParamSet.Add4Sql("@isgw", SqlDbType.Char, 1, isgw),
				ParamSet.Add4Sql("@ispdm", SqlDbType.Char, 1, ispdm),
				ParamSet.Add4Sql("@iserp", SqlDbType.Char, 1, iserp),
				ParamSet.Add4Sql("@ismsg", SqlDbType.Char, 1, ismsg),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseCreateUR", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				userID = int.Parse(pData.GetParamValue("@userid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 구성원 이동시 이동대상 그룹에 존재하는 구성원 DB Query (관리)
		/// </summary>		
		public DataSet GetImmovableUsers(string moveUserInfo)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@move_info", SqlDbType.NText, moveUserInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetImmovableUsers", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 선택한 그룹에서의 사용자 정보 - 상태에 따른 역할 구분을 위한
		/// </summary>		
		public DataSet GetMemberStateInfo(int userID, int groupID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetUserInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자, 그룹 batch생성을 위해 Excel로 부터 생성정보 읽기
		/// </summary>		
		public DataSet GetBaseObjectInfoFromExcel(string objectKind, string uploadPath)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@objectKind", SqlDbType.VarChar, 5, objectKind),
				ParamSet.Add4Sql("@path", SqlDbType.NVarChar, 255, uploadPath)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetObjectInfoFromExcel", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자 정보 DB Query (관리)
		/// </summary>		
		public DataSet GetBaseUserInfo(int userID, string viewDate)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetUserInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자 생성 및 수정(관리)
		/// </summary>		
		public int HandleBaseUser(string actionKind, string userInfo, string groupInfo, string ownerShipInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@action_kind", SqlDbType.Char, 10, actionKind),
				ParamSet.Add4Sql("@user_Info", SqlDbType.NText, userInfo),
				ParamSet.Add4Sql("@group_Info", SqlDbType.NText, groupInfo),
				ParamSet.Add4Sql("@ownership_info", SqlDbType.NText, ownerShipInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseURHandler", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 도메인 사용자 찾아오기
		/// </summary>		
		public DataSet SearchDomainUsers(string domainID, string groupID, string groupType, int pageIndex, int pageCount, string sortColumn, string sortType, string searchText, string admin, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.VarChar, 3, domainID),
				ParamSet.Add4Sql("@gr_id", SqlDbType.VarChar, 20, groupID),
				ParamSet.Add4Sql("@grtype", SqlDbType.Char, 1, groupType),
				ParamSet.Add4Sql("@page", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@count", SqlDbType.SmallInt, 2, pageCount),
				ParamSet.Add4Sql("@sort_col", SqlDbType.VarChar, 50, sortColumn),
				ParamSet.Add4Sql("@sort_type", SqlDbType.VarChar, 5, sortType),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 1000, searchText),
				ParamSet.Add4Sql("@admin_tool", SqlDbType.Char, 1, admin),
				ParamSet.Add4Sql("@total_cnt", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DomainSearchUsers", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@total_cnt").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// User Search for User Manage
		/// </summary>		
		public DataSet SearchBaseDomainUsers(int domainID, string gradeCode1, string searchText, string isAll)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@gradecode1", SqlDbType.VarChar, 10, gradeCode1),
				ParamSet.Add4Sql("@search_text", SqlDbType.NVarChar, 1000, searchText),
				ParamSet.Add4Sql("@isAll", SqlDbType.Char, 1, isAll)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseSearchUsers", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 퇴직 사용자 List Query(관리툴용)
		/// </summary>		
		public DataSet SearchBaseRetiredUsers(string domainID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.VarChar, 3, domainID),
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

			ParamData pData = new ParamData("admin.ph_up_BaseGetRetiredUserList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMessages").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 퇴직 사용자 List Query(조직검색용)
		/// </summary>		
		public DataSet GetOrgSearchRetiredUsers(string domainID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, string searchStartDate, string searchEndDate, out int totalCount)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.VarChar, 3, domainID),
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

			ParamData pData = new ParamData("admin.ph_up_GetUserRetiredList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalCount = int.Parse(pData.GetParamValue("@totalMessages").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자 상태 정보
		/// </summary>
		/// <param name="connect"></param>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetObjectGroupUserStatus(int groupID, int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetGrUserStatus", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자 상태 변경
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="role"></param>
		/// <param name="nickName"></param>
		/// <param name="introduction"></param>
		/// <param name="status"></param>
		/// <param name="reserved1"></param>
		/// <returns></returns>
		public int ChangeObjectGroupMember(int groupID, int userID, string role, string nickName, string introduction, string status, string reserved1)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@role", SqlDbType.VarChar, 20, role),
				ParamSet.Add4Sql("@nickname", SqlDbType.NVarChar, 20, nickName),
				ParamSet.Add4Sql("@introduction", SqlDbType.NVarChar, 500, introduction),
				ParamSet.Add4Sql("@Staus", SqlDbType.Char, 1, status),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectChangeGRMember", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹에서 사용자 삭제
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public int RemoveObjectGroupMember(int groupID, int userID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectRemoveGRMember", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹에 사용자 추가
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="userID"></param>
		/// <param name="role"></param>
		/// <param name="nickName"></param>
		/// <param name="introduction"></param>
		/// <param name="status"></param>
		/// <param name="reserved1"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public int CreateObjectGroupMember(int groupID, int userID, string role, string nickName, string introduction, string status, string reserved1, out string message)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@gr_id", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@role", SqlDbType.VarChar, 20, role),
				ParamSet.Add4Sql("@nickname", SqlDbType.NVarChar, 20, nickName),
				ParamSet.Add4Sql("@introduction", SqlDbType.NVarChar, 500, introduction),
				ParamSet.Add4Sql("@Staus", SqlDbType.VarChar, 1, status),
				ParamSet.Add4Sql("@reserved1", SqlDbType.NVarChar, 255, reserved1),
				ParamSet.Add4Sql("@msg", SqlDbType.VarChar, 2, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewGRMember", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				message = pData.GetParamValue("@msg").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 멤버 추가(XML이용)
		/// </summary>
		/// <param name="groupID"></param>
		/// <param name="groupMemeber"></param>
		/// <returns></returns>
		public int CreateObjectGroupMemberByXML(int groupID, string groupMemeber)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@GroupID", SqlDbType.Int, 4, groupID),
				ParamSet.Add4Sql("@GroupMember", SqlDbType.NText, groupMemeber)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetNewGRMemberXML", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 그룹 검색 멤버 가져오기
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
		public DataSet GetGroupSearchMembers(string domainID, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.VarChar, 3, domainID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.VarChar, 20, searchColumn),
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
		/// 사용자 이미지 가져오기
		/// </summary>
		public string GetBasePersonImage(int userID, string viewDate, string imgType, string mode)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@viewdate", SqlDbType.Char, 10, viewDate),
				ParamSet.Add4Sql("@imgtype", SqlDbType.VarChar, 20, imgType),
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseGetPersonImage", parameters);

			using (DbBase db = new DbBase())
			{
				strReturn = db.ExecuteScalarNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return strReturn;
		}

		/// <summary>
		/// 사용자 정보 조회
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetOrgPersonInfo(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectoryGetInfoView", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자 Configuration 정보 조회
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetOrgPersonConfigInfo(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectoryGetUserConfigInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 사용자 보조 메일 변경
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="secondMail"></param>
		/// <returns>"OK" OR "EXIST"</returns>
		public string SetSecondEmail(int userID, string secondMail)
		{
			string strReturn = "";

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@secondmail", SqlDbType.VarChar, 30, secondMail),
				ParamSet.Add4Sql("@result", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectorySetSecondMail", parameters);

			using (DbBase db = new DbBase())
			{
				db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);

				strReturn = pData.GetParamValue("@result").ToString();
			}

			return strReturn;
		}

		/// <summary>
		/// 사용자 신상 정보 변경
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="PersonName2"></param>
		/// <param name="PersonName3"></param>
		/// <param name="Birthday"></param>
		/// <param name="BirthdayType"></param>
		/// <param name="HandPhone"></param>
		/// <param name="InterPhone"></param>
		/// <param name="MarriDate"></param>
		/// <param name="NickName"></param>
		/// <param name="DirectPhone"></param>
		/// <param name="Fax"></param>
		/// <param name="Address"></param>
		/// <param name="AddressDetail"></param>
		/// <param name="ZipCode"></param>
		/// <param name="Homepage"></param>
		/// <param name="Introduction"></param>
		/// <returns></returns>
		public int CreateOrgPersonInfo(int userID, string PersonName2, string PersonName3, string Birthday, string BirthdayType, string HandPhone, string InterPhone, string MarriDate, string NickName, string DirectPhone, string Fax, string Address, string AddressDetail, string ZipCode, string Homepage, string Introduction)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@regionname1", SqlDbType.NVarChar, 100, PersonName2),
				ParamSet.Add4Sql("@regionname2", SqlDbType.NVarChar, 100, PersonName3),
				ParamSet.Add4Sql("@birth", SqlDbType.Char, 10, Birthday),
				ParamSet.Add4Sql("@birthType", SqlDbType.Char, 1, BirthdayType),
				ParamSet.Add4Sql("@mobile", SqlDbType.VarChar, 30, HandPhone),
				ParamSet.Add4Sql("@telephone2", SqlDbType.VarChar, 30, InterPhone),
				ParamSet.Add4Sql("@homepage", SqlDbType.NVarChar, 255, Homepage),
				ParamSet.Add4Sql("@marrieddate", SqlDbType.VarChar, 10, MarriDate),
				ParamSet.Add4Sql("@nickname", SqlDbType.NVarChar, 100, NickName),
				ParamSet.Add4Sql("@telephone", SqlDbType.VarChar, 30, DirectPhone),
				ParamSet.Add4Sql("@fax", SqlDbType.VarChar, 30, Fax),
				ParamSet.Add4Sql("@address1", SqlDbType.NVarChar, 100, Address),
				ParamSet.Add4Sql("@detailaddress1", SqlDbType.NVarChar, 100, AddressDetail),
				ParamSet.Add4Sql("@zipcode1", SqlDbType.VarChar, 10, ZipCode),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, Introduction),
				ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 100, ""),
				ParamSet.Add4Sql("@key2", SqlDbType.NVarChar, 100, ""),
				ParamSet.Add4Sql("@key3", SqlDbType.NVarChar, 100, ""),
				ParamSet.Add4Sql("@key4", SqlDbType.NVarChar, 100, ""),
				ParamSet.Add4Sql("@key5", SqlDbType.NVarChar, 100, ""),
				ParamSet.Add4Sql("@key6", SqlDbType.NVarChar, 100, "")
			};

			ParamData pData = new ParamData("admin.ph_up_DirectorySetInfoView", parameters);
			
			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="PersonName2"></param>
		/// <param name="PersonName3"></param>
		/// <param name="Birthday"></param>
		/// <param name="BirthdayType"></param>
		/// <param name="HandPhone"></param>
		/// <param name="InterPhone"></param>
		/// <param name="MarriDate"></param>
		/// <param name="NickName"></param>
		/// <param name="DirectPhone"></param>
		/// <param name="Fax"></param>
		/// <param name="Address"></param>
		/// <param name="AddressDetail"></param>
		/// <param name="ZipCode"></param>
		/// <param name="Homepage"></param>
		/// <param name="Introduction"></param>
		/// <param name="key1"></param>
		/// <param name="key2"></param>
		/// <param name="key3"></param>
		/// <param name="key4"></param>
		/// <param name="key5"></param>
		/// <param name="key6"></param>
		/// <returns></returns>
		public int CreateOrgPersonInfo(int userID, string PersonName2, string PersonName3, string Birthday, string BirthdayType, string HandPhone, string InterPhone, string MarriDate, string NickName, string DirectPhone, string Fax, string Address, string AddressDetail, string ZipCode, string Homepage, string Introduction, string key1, string key2, string key3, string key4, string key5, string key6)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@regionname1", SqlDbType.NVarChar, 100, PersonName2),
				ParamSet.Add4Sql("@regionname2", SqlDbType.NVarChar, 100, PersonName3),
				ParamSet.Add4Sql("@birth", SqlDbType.Char, 10, Birthday),
				ParamSet.Add4Sql("@birthType", SqlDbType.Char, 1, BirthdayType),
				ParamSet.Add4Sql("@mobile", SqlDbType.VarChar, 30, HandPhone),
				ParamSet.Add4Sql("@telephone2", SqlDbType.VarChar, 30, InterPhone),
				ParamSet.Add4Sql("@homepage", SqlDbType.NVarChar, 255, Homepage),
				ParamSet.Add4Sql("@marrieddate", SqlDbType.VarChar, 10, MarriDate),
				ParamSet.Add4Sql("@nickname", SqlDbType.NVarChar, 100, NickName),
				ParamSet.Add4Sql("@telephone", SqlDbType.VarChar, 30, DirectPhone),
				ParamSet.Add4Sql("@fax", SqlDbType.VarChar, 30, Fax),
				ParamSet.Add4Sql("@address1", SqlDbType.NVarChar, 100, Address),
				ParamSet.Add4Sql("@detailaddress1", SqlDbType.NVarChar, 100, AddressDetail),
				ParamSet.Add4Sql("@zipcode1", SqlDbType.VarChar, 10, ZipCode),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 1000, Introduction),
				ParamSet.Add4Sql("@key1", SqlDbType.NVarChar, 100, key1),
				ParamSet.Add4Sql("@key2", SqlDbType.NVarChar, 100, key2),
				ParamSet.Add4Sql("@key3", SqlDbType.NVarChar, 100, key3),
				ParamSet.Add4Sql("@key4", SqlDbType.NVarChar, 100, key4),
				ParamSet.Add4Sql("@key5", SqlDbType.NVarChar, 100, key5),
				ParamSet.Add4Sql("@key6", SqlDbType.NVarChar, 100, key6)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectorySetInfoView", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 사용자 Configuration정보 등록 / 변경
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="Absent"></param>
		/// <param name="AbsentStartDate"></param>
		/// <param name="AbsentEndDate"></param>
		/// <param name="AbsentType"></param>
		/// <param name="AbsentReason"></param>
		/// <param name="UseRepresentation"></param>
		/// <param name="RepresentorID"></param>
		/// <returns></returns>
		public int SetOrgPersonConfigInfo(int userID, string logonID, string Absent, string AbsentStartDate, string AbsentEndDate
									, string AbsentType, string AbsentReason, string UseRepresentation, string RepresentorID, string RepresentorName)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@logonid", SqlDbType.VarChar, 30, logonID),
				ParamSet.Add4Sql("@absent", SqlDbType.Char, 1, Absent),
				ParamSet.Add4Sql("@absentstartdate", SqlDbType.Char, 10, AbsentStartDate),
				ParamSet.Add4Sql("@absentenddate", SqlDbType.Char, 10, AbsentEndDate),
				ParamSet.Add4Sql("@absenttype", SqlDbType.Char, 1, AbsentType),
				ParamSet.Add4Sql("@absentreason", SqlDbType.NVarChar, 128, AbsentReason),
				ParamSet.Add4Sql("@userepresentation", SqlDbType.Char, 1, UseRepresentation),
				ParamSet.Add4Sql("@representorid", SqlDbType.VarChar, 30, RepresentorID),
				ParamSet.Add4Sql("@representorname", SqlDbType.NVarChar, 30, RepresentorName)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectorySetUserConfigInfo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 로그아웃 시간 기록
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public int SetLogout(int userID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_LogOutEventWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 사용자 정보중 소속부서 관리.(관리툴에서 사용)
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="displayName"></param>
		/// <param name="groupInfo"></param>
		/// <returns></returns>
		public int ChangeBaseUserGroupInfo(int userID, string displayName, string groupInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@displayname", SqlDbType.NVarChar, 100, displayName),
				ParamSet.Add4Sql("@group_Info", SqlDbType.NText, groupInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseChangeUserGroupInfo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 사용자 삭제
		/// </summary>
		/// <param name="userInfo"></param>
		/// <returns></returns>
		public int DeleteBaseUsers(string userInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@user_Info", SqlDbType.NText, userInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_BaseDeleteURs", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// alias를 입력 받아 UserID를 반환
		/// </summary>
		/// <param name="alias"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public int GetUserIDFromLogonID(string alias, out int userID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@alias", SqlDbType.VarChar, 30, alias),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_getUserID", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				userID = int.Parse(pData.GetParamValue("@userid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// GR_ID를 입력 받아, 부서장 정보를 반환
		/// </summary>
		/// <param name="groupID"></param>
		/// <returns></returns>
		public DataSet GetGroupChiefByGroupID(int groupID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@grid", SqlDbType.Int, 4, groupID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetGroupChief", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// User Career 등록
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="sortKey"></param>
		/// <param name="fromCareer"></param>
		/// <param name="toCareer"></param>
		/// <param name="subject"></param>
		/// <param name="description"></param>
		/// <returns></returns>
		public int CreateUserCareer(int userID, int sortKey, string fromCareer, string toCareer, string subject, string description)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@sortkey", SqlDbType.SmallInt, 2, sortKey),
				ParamSet.Add4Sql("@fromCareer", SqlDbType.VarChar, 7, fromCareer),
				ParamSet.Add4Sql("@toCareer", SqlDbType.VarChar, 7, toCareer),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 100, subject),
				ParamSet.Add4Sql("@description", SqlDbType.NText, description)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetUserCareer", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 사용자 경력사항 정보 가져오기
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetUserCareer(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetUserCareer", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		///  User Qualification 등록
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="sortKey"></param>
		/// <param name="qualifiedDate"></param>
		/// <param name="qualifiedName"></param>
		/// <param name="qualifiedGrade"></param>
		/// <param name="qualifiedCompany"></param>
		/// <returns></returns>
		public int CreateUserQualification(int userID, int sortKey, string qualifiedDate, string qualifiedName, string qualifiedGrade, string qualifiedCompany)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userID", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@sortkey", SqlDbType.SmallInt, 2, sortKey),
				ParamSet.Add4Sql("@QualifiedDate", SqlDbType.VarChar, 10, qualifiedDate),
				ParamSet.Add4Sql("@QualifiedName", SqlDbType.VarChar, 50, qualifiedName),
				ParamSet.Add4Sql("@QualifiedGrade", SqlDbType.VarChar, 10, qualifiedGrade),
				ParamSet.Add4Sql("@QualifiedCompany", SqlDbType.VarChar, 50, qualifiedCompany)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectSetUserQualification", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 사용자 기념일 등록
		/// </summary>
		/// <param name="messageID"></param>
		/// <param name="userID"></param>
		/// <param name="subject"></param>
		/// <param name="description"></param>
		/// <param name="anniDate"></param>
		/// <param name="anniDateType"></param>
		/// <param name="alarmDate"></param>
		/// <param name="priority"></param>
		/// <returns></returns>
		public int SetUserAnniversary(int messageID, int userID, string subject, string description, string anniDate, string anniDateType, string alarmDate, string priority)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 50, subject),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@annidate", SqlDbType.VarChar, 10, anniDate),
				ParamSet.Add4Sql("@annidatetype", SqlDbType.Char, 1, anniDateType),
				ParamSet.Add4Sql("@alarmdate", SqlDbType.VarChar, 10, alarmDate),
				ParamSet.Add4Sql("@priority", SqlDbType.Char, 1, priority)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectorySetUserAnniversary", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		///  기념일 엑셀 파일로 추가
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="path"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public int SetUserAnniversaryExcel(int userID, string path, out string result)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@path", SqlDbType.NVarChar, 255, path),
				ParamSet.Add4Sql("@result", SqlDbType.VarChar, 10, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectorySetUserAnniversaryExcel", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				result = pData.GetParamValue("@result").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 기념일 정보 쿼리
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="lunarToday"></param>
		/// <param name="getMode">All, Today</param>
		/// <returns></returns>
		public DataSet GetUserAnniversary(int userID, string lunarToday, string getMode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@lunartoday", SqlDbType.VarChar, 10, lunarToday),
				ParamSet.Add4Sql("@getmode", SqlDbType.VarChar, 10, getMode)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectoryGetUserAnniversary", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 기념일 정보 삭제
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="messageID"></param>
		/// <returns></returns>
		public int DeleteUserAnniversary(int messageID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@messageID", SqlDbType.Int, 4, messageID)
			};

			ParamData pData = new ParamData("admin.ph_up_DirectoryDeleteUserAnniversary", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 개인 환경 설정 => 현재는 테마 설정만 함
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="theme"></param>
		/// <returns></returns>
		public int HandleUserConfiguration(int userID, int theme)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@ur_id", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@theme", SqlDbType.TinyInt, 1, theme)
			};

			ParamData pData = new ParamData("admin.ph_up_OpSetPersonConfig", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// UserID를 입력받아 부서장 정보를 확인
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetGroupChiefByUserID(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@partiid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_ObjectGetPersonChief", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#region 사용자 리스트 뷰 설정 쿼리

		/// <summary>
		/// 사용자 리스트 뷰 설정 쿼리(미리보기 설정 정보, 리스트 목록 수 조회) 
		/// </summary>
		/// <param name="userid">사용자 아이디</param>
		/// <returns></returns>
		public DataSet GetUserListViewSettingInfo(int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 7, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_GetUserListViewSettingInfo", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		#endregion

		#region 사용자 리스트 뷰 설정 정보 저장

		/// <summary>
		/// 사용자 리스트 뷰 설정 정보 저장
		/// </summary>
		/// <param name="UserID">사용자 ID</param>
		/// <param name="Preview">미리보기 설정</param>
		/// <param name="ListCount">리스트 목록 수</param>
		/// <returns></returns>
		public int SetBoardUserListViewSettingInfo(int userID, string preview, int listcount)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@UserID", SqlDbType.Int, userID),
				ParamSet.Add4Sql("@Preview", SqlDbType.VarChar, 10, preview),
				ParamSet.Add4Sql("@ListCount", SqlDbType.Int, listcount)
			};

			ParamData pData = new ParamData("admin.ph_up_SetUserListViewSettingInfo", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 내 문서함 최상위 폴더 가져오기
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="userID"></param>
		/// <param name="rootFolderID"></param>
		/// <returns></returns>
		public int GetPersonDocRootFolder(int dnID, int userID, out string rootFolderID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID),
				ParamSet.Add4Sql("@rootfolderid", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_PersonDocGetRootFolder", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				rootFolderID = pData.GetParamValue("@rootfolderid").ToString();
			}

			return iReturn;
		}

		/// <summary>
		/// 개인 문서함 용량 설정
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="size"></param>
		/// <param name="setMode"></param>
		/// <returns></returns>
		public int SetPersonDocCapacity(int userID, string size, string setMode)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, userID),
				ParamSet.Add4Sql("@size", SqlDbType.VarChar, 30, size),
				ParamSet.Add4Sql("@setmode", SqlDbType.VarChar, 10, setMode)
			};

			ParamData pData = new ParamData("admin.ph_up_PersonDocSetCapacity", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 개인 문서함 용량 정보
		/// </summary>
		/// <param name="dnID"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet GetPersonDocCapacity(int dnID, int userID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dnid", SqlDbType.Int, 4, dnID),
				ParamSet.Add4Sql("@userid", SqlDbType.Int, 4, userID)
			};

			ParamData pData = new ParamData("admin.ph_up_PersonDocGetMyCapacity", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// SMS 한도 설정
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="size"></param>
		/// <param name="setMode"></param>
		/// <returns></returns>
		public int SetSmsCapacity(int userID, string size, string setMode)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@userid", SqlDbType.Int, userID),
				ParamSet.Add4Sql("@size", SqlDbType.VarChar, 30, size),
				ParamSet.Add4Sql("@setmode", SqlDbType.VarChar, 10, setMode)
			};

			ParamData pData = new ParamData("admin.ph_up_SmsSetCapacity", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		#endregion

		/// <summary>
		/// 인사 DB에 있는 사용자 정보
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
	}
}
