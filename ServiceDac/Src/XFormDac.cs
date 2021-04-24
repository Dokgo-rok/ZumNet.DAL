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
	public class XFormDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public XFormDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 양식정보 쿼리
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
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
		public int InsertXfKnowledge(string msgType, string inherited, string priority
					, string subject, string bodyText, string creator, int creatorID
					, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID
					, string creatorDeptCode, string coRegistrant, string createDate, string publishDate
					, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
					, string replyMail, string docNumber, string docLevel, string keepYear
					, string reserved1, string reserved2, out int oid)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// PH_XF_KNOWLEDGE 테이블 값 변경(임저장함에서 수정저장)
		/// </summary>
		public int UpdateXfKnowledge(int messageID, string msgType, string priority
					, string subject, string bodyText, string coRegistrant, string publishDate
					, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
					, string replyMail, string docNumber, string docLevel, string keepYear
					, string reserved1, string reserved2)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XF_SUGGEST 테이블 값 저장
		/// </summary>
		public int InsertXfSuggest(string msgType, string inherited, string priority
					, string subject, string bodyText, string creator, int creatorID
					, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID
					, string creatorDeptCode, string coRegistrant, string createDate, string publishDate
					, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
					, string replyMail, string docNumber, string docLevel, string keepYear
					, string reserved1, string reserved2, out int oid)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// PH_XF_SUGGEST 테이블 값 변경(임저장함에서 수정저장)
		/// </summary>
		public int UpdateXfSuggest(int messageID, string msgType, string priority
					, string subject, string bodyText, string coRegistrant, string publishDate
					, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
					, string replyMail, string docNumber, string docLevel, string keepYear
					, string reserved1, string reserved2)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XF_INFORMATION 테이블 값 저장
		/// </summary>
		public int InsertXfInformation(string msgType, string inherited, string priority
					, string subject, string bodyText, string creator, int creatorID
					, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID
					, string creatorDeptCode, string coRegistrant, string createDate, string publishDate
					, string expiredDate, string infoGetDate, string infoGetRout, string infoSource
					, string hasAttachFile, string isPublic, string linkedMsg, string topLine
					, string replyMail, string docNumber, string docLevel, string keepYear
					, string reserved1, string reserved2, out int oid)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// PH_XF_INFORMATION 테이블 값 변경(임저장함에서 수정저장)
		/// </summary>
		public int UpdateXfInformation(int messageID, string msgType, string priority
					, string subject, string bodyText, string coRegistrant, string publishDate
					, string expiredDate, string infoGetDate, string infoGetRout, string infoSource
					, string hasAttachFile, string isPublic, string linkedMsg, string topLine
					, string replyMail, string docNumber, string docLevel, string keepYear
					, string reserved1, string reserved2)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}


		/// <summary>
		/// 최초 PH_XF_QNA 테이블 값 저장
		/// </summary>
		public int InsertXfQnA(string msgType, string inherited, string priority
					, string subject, string bodyText, string creator, int creatorID
					, string creatorCN, string creatorGrade, string creatorDept, int creatorDeptID
					, string creatorDeptCode, string createDate, string publishDate
					, string expiredDate, string hasAttachFile, string linkedMsg, string topLine
					, string replyMail, string reserved1, string reserved2, out int oid)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XF_QNA 테이블 값 변경
		/// </summary>
		public int UpdateXfQnA(int messageID, string msgType, string priority
					, string subject, string bodyText, string publishDate, string expiredDate
					, string hasAttachFile, string linkedMsg, string topLine
					, string replyMail, string reserved1, string reserved2)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XFORM_CONTEXT 테이블 값 저장
		/// </summary>
		public int InsertXFormContext(string xfAlias, int messageID, string body, string drm, string reserved1, out int oid)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XFORM_CONTEXT 테이블 값 변경
		/// </summary>
		public int UpdateXFormContext(string xfAlias, int messageID, string body, string drm, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_FILE_ATTACH 테이블 값 저장
		/// </summary>
		public int InsertFileAttach(string xfAlias, int messageID, string isFile
					, string fileName, string savedName, string fileSize, string fileType
					, string prefix, string locationFolder, string drm, out int oid)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				oid = int.Parse(pData.GetParamValue("@oid").ToString());
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_FILE_ATTACH 테이블 값 변경(DRM 정보만 변경)
		/// </summary>
		public int UpdateFileAttach(int attachId, string xfAlias, int messageID, string drm)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_FILE_ATTACH 테이블 값 배치로 저장
		/// </summary>
		public int InsertFileAttach(string xfAlias, int messageID, string fileInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@fileinfo", SqlDbType.NText, fileInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertFileAttachBatch", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XFORM_FD 테이블 값 저장
		/// </summary>
		public int InsertXFormFD(int fdID, string xfAlias, string messageID, int parentMsgID
					, string attType, Int16 state, string taskActivity, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XFORM_FD 테이블 값 변경(분류 변경시)
		/// </summary>
		public int UpdateXFormFD(int fdID, string xfAlias, string messageID, string attType, string taskActivity, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XFORM_TEMPORARY 테이블 값 저장
		/// </summary>
		public int InsertXFormTemporary(int ctID, string actor, int actorID, string actorCN
				, string xfAlias, string messageID, string subject, string reason, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XFORM_TEMPORARY 테이블 값 변경(임시 저장함)
		/// </summary>
		public int UpdateXFormTemporary(int ctID, string xfAlias, string messageID, string subject, string reason, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XFORM_REGISTRANT 테이블 값 저장
		/// </summary>
		public int InsertXFormRegistrant(string xfAlias, int messageID, int actorID, string actType
						, string actor, string actorCN, string actorGrade, string actorDept
						, string actorDeptID, string actorDeptCode, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_XFORM_REGISTRANT 테이블 값 배치로 저장
		/// </summary>
		public int InsertXFormRegistrant(string xfAlias, string messageID, string xmlInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xmlinfo   ", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertXFormRegistrantBatch", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 admin.PH_XFORM_DL 테이블 값 저장
		/// </summary>
		public int InsertXFormDL(string xfAlias, string messageID, string step, string dl, string description, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 admin.PH_XFORM_DL 테이블 값 변경
		/// </summary>
		public int UpdateXFormDL(string xfAlias, string messageID, string step, string dl, string description, string reserved1)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 삭제 admin.PH_XFORM_DL 테이블 값 변경
		/// </summary>
		public int DeleteXFormDL(string xfAlias, string messageID, string step)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@step", SqlDbType.VarChar, 30, step)
			};

			ParamData pData = new ParamData("admin.ph_up_DeleteXFormDL", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_XFORM_REGISTRANT 테이블 값 배치로 변경
		/// </summary>
		public int UpdateXFormRegistrant(string xfAlias, string messageID, string xmlInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xmlinfo   ", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateXFormRegistrantBatch", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_LINKED_DOC 테이블 값 저장
		/// </summary>
		public int InsertLinkedDoc(string xfAlias, string messageID, string linkedXFAlias
				, string linkedMessageID, string linkedSubject, string linkedReserved1, string linkedReserved2)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_LINKED_DOC 테이블 값 배치로 저장
		/// </summary>
		public int InsertLinkedDoc(string xfAlias, string messageID, string xmlInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertLinkedDocBatch", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 수정 PH_LINKED_DOC 테이블 값 배치로 변경
		/// </summary>
		public int UpdateLinkedDoc(string xfAlias, string messageID, string xmlInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@xmlinfo", SqlDbType.NText, xmlInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_UpdateLinkedDocBatch", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_EVENT_REGISTRATION 테이블 값 저장
		/// </summary>
		public int InsertEventRegistration(string xfAlias, int messageID, int actorID, string actType, string actDate)
		{
			int iReturn = 0;

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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 최초 PH_EVENT_EDIT 테이블 값 저장
		/// </summary>
		public int InsertEventEdit(string xfAlias, int messageID, int actorID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@editor", SqlDbType.Int, 4, actorID)
			};

			ParamData pData = new ParamData("admin.ph_up_InsertEventEdit", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
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
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
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
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias)
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
		/// 질문/답변 (답변채택으로 질문 완료처리)
		/// </summary>
		public int SetQnaSelectAnswer(int messageID, string xfAlias, int parentID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@msgid", SqlDbType.Int, 4, messageID),
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@parentid", SqlDbType.Int, 4, parentID)
			};

			ParamData pData = new ParamData("admin.ph_up_SetQnaSelectAnswer", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 정보 공개여부 변경
		/// </summary>
		/// <param name="xfAlias"></param>
		/// <param name="messageID"></param>
		/// <param name="isPublic"></param>
		/// <returns></returns>
		public int ChangeXFormIsPublic(string xfAlias, string messageID, string isPublic)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@xfalias", SqlDbType.VarChar, 30, xfAlias),
				ParamSet.Add4Sql("@messageid", SqlDbType.VarChar, 33, messageID),
				ParamSet.Add4Sql("@ispublic", SqlDbType.Char, 1, isPublic)
			};

			ParamData pData = new ParamData("admin.ph_up_ChangeXFormIsPublic", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		#region 지식관리 메인 관련

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
				totalMessage = int.Parse(pData.GetParamValue("@totalmessage").ToString());
			}

			return dsReturn;
		}
		#endregion

		#region 지식관리 통계 관련

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


		public int GetStatisticsTotalData(int userID, int deptID, string isSection, string startDate, string endDate
				, out double userTotalPoint, out double userSearchPoint, out double deptTotalPoint, out double deptSearchPoint)
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
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
				userTotalPoint = int.Parse(pData.GetParamValue("@usertotalpoint").ToString());
				userSearchPoint = int.Parse(pData.GetParamValue("@usersearchpoint").ToString());
				deptTotalPoint = int.Parse(pData.GetParamValue("@depttotalpoint").ToString());
				deptSearchPoint = int.Parse(pData.GetParamValue("@deptsearchpoint").ToString());
			}

			return iReturn;
		}

		#endregion


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
	}
}
