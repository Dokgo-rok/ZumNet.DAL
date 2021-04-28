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
	public class CompetencyDac : DacBase
	{
		/// <summary>
		/// 
		/// </summary>
		public CompetencyDac(string connectionString = "") : base(connectionString)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public CompetencyDac(SqlConnection connection) : base(connection)
		{

		}

		/// <summary>
		/// 포인트 정보 입력
		/// </summary>
		/// <param name="pointInfo"></param>
		/// <returns></returns>
		public int CreateEvaluationPoint(string mode, string pointInfo)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@mode", SqlDbType.Char, 1, mode),
				ParamSet.Add4Sql("@strpoint", SqlDbType.NText, pointInfo)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessPoint", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 평가표 목록 리스트
		/// </summary>
		/// <param name="domainID"></param>
		/// <param name="isAdmin"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortColumn"></param>
		/// <param name="sortType"></param>
		/// <param name="searchColumn"></param>
		/// <param name="searchText"></param>
		/// <param name="totalMessage"></param>
		/// <returns></returns>
		public DataSet GetCompetencyList(int domainID, string isAdmin, int pageIndex, int pageCount, string sortColumn, string sortType, string searchColumn, string searchText, out int totalMessage)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@pageIdx", SqlDbType.Int, 4, pageIndex),
				ParamSet.Add4Sql("@pageCnt", SqlDbType.Int, 4, pageCount),
				ParamSet.Add4Sql("@admin", SqlDbType.Char, 1, isAdmin),
				ParamSet.Add4Sql("@sortCol", SqlDbType.VarChar, 20, sortColumn),
				ParamSet.Add4Sql("@sortType", SqlDbType.VarChar, 20, sortType),
				ParamSet.Add4Sql("@searchCol", SqlDbType.NVarChar, 20, searchColumn),
				ParamSet.Add4Sql("@searchText", SqlDbType.NVarChar, 200, searchText),
				ParamSet.Add4Sql("@TotalMsg", SqlDbType.Int, 4, ParameterDirection.Output)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetCompetencyList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
				totalMessage = int.Parse(pData.GetParamValue("@TotalMsg").ToString());
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당 평가표의 내용과 중분류/소분류 리스트를 조회한다
		/// </summary>
		/// <param name="competencyID"></param>
		/// <returns></returns>
		public DataSet GetCompetencyInstance(int competencyID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetCompetencyIns", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 해당 평가표의 중분류내용과 중분류 리스트를 조회한다
		/// </summary>
		/// <param name="competencyID"></param>
		/// <param name="classID"></param>
		/// <param name="reqID"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public DataSet GetCompetencyClassRequest(int competencyID, int classID, int reqID, string mode)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyID),
				ParamSet.Add4Sql("@classcode", SqlDbType.Int, 4, classID),
				ParamSet.Add4Sql("@reqcode", SqlDbType.Int, 4, reqID),
				ParamSet.Add4Sql("@mode", SqlDbType.VarChar, 33, mode)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetCompetencyClassRequest", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 평가표 (Competency,CompetencyInstance) 등록/수정
		/// </summary>
		/// <param name="competencyCode"></param>
		/// <param name="domainID"></param>
		/// <param name="usage"></param>
		/// <param name="subject"></param>
		/// <param name="useFrom"></param>
		/// <param name="useTo"></param>
		/// <param name="selectKind"></param>
		/// <param name="restriction1"></param>
		/// <param name="restriction2"></param>
		/// <param name="restriction3"></param>
		/// <param name="description"></param>
		/// <param name="isUsed"></param>
		/// <param name="parentCode"></param>
		/// <param name="xmlData"></param>
		/// <returns></returns>
		public int SetCompetencyInstance(int competencyCode, int domainID, string usage, string subject, string useFrom, string useTo, string selectKind, string restriction1, string restriction2, string restriction3, string description, string isUsed, string parentCode, string xmlData)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyCode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@usage", SqlDbType.VarChar, 10, usage),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@usefrom", SqlDbType.Char, 10, useFrom),
				ParamSet.Add4Sql("@useto", SqlDbType.Char, 10, useTo),
				ParamSet.Add4Sql("@selectkind", SqlDbType.Char, 1, selectKind),
				ParamSet.Add4Sql("@restriction1", SqlDbType.VarChar, 20, restriction1),
				ParamSet.Add4Sql("@restriction2", SqlDbType.VarChar, 20, restriction2),
				ParamSet.Add4Sql("@restriction3", SqlDbType.VarChar, 20, restriction3),
				ParamSet.Add4Sql("@description", SqlDbType.NVarChar, 500, description),
				ParamSet.Add4Sql("@isused", SqlDbType.Char, 1, isUsed),
				ParamSet.Add4Sql("@parentcode", SqlDbType.VarChar, 10, parentCode),
				ParamSet.Add4Sql("@xmldata", SqlDbType.NText, xmlData)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCompetencySetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 평가표의 중항목을 등록/수정한다.
		/// </summary>
		/// <param name="classCode"></param>
		/// <param name="domainID"></param>
		/// <param name="subject"></param>
		/// <param name="startPoint"></param>
		/// <param name="endPoint"></param>
		/// <param name="unitPoint"></param>
		/// <param name="rating"></param>
		/// <param name="comment1"></param>
		/// <param name="comment2"></param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public int SetCompetencyClass(int classCode, int domainID, string subject, string startPoint, string endPoint, string unitPoint, string rating, string comment1, string comment2, string alias)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@classcode", SqlDbType.Int, 4, classCode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@startpoint", SqlDbType.Decimal, Convert.ToDecimal(startPoint)),
				ParamSet.Add4Sql("@endpoint", SqlDbType.Decimal, Convert.ToDecimal(endPoint)),
				ParamSet.Add4Sql("@unitpoint", SqlDbType.Decimal, Convert.ToDecimal(unitPoint)),
				ParamSet.Add4Sql("@rating", SqlDbType.Decimal, Convert.ToDecimal(rating)),
				ParamSet.Add4Sql("@comment1", SqlDbType.NVarChar, 1000, comment1),
				ParamSet.Add4Sql("@comment2", SqlDbType.NVarChar, 1000, comment2),
				ParamSet.Add4Sql("@alias", SqlDbType.VarChar, 20, alias)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCompetencyClassSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 평가표의 소항목을 등록/수정한다.
		/// </summary>
		/// <param name="reqCode"></param>
		/// <param name="domainID"></param>
		/// <param name="subject"></param>
		/// <param name="startPoint"></param>
		/// <param name="endPoint"></param>
		/// <param name="unitPoint"></param>
		/// <param name="rating"></param>
		/// <param name="comment1"></param>
		/// <param name="comment2"></param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public int SetCompetencyRequest(int reqCode, int domainID, string subject, string startPoint, string endPoint, string unitPoint, string rating, string comment1, string comment2, string alias)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@reqcode", SqlDbType.Int, 4, reqCode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@subject", SqlDbType.NVarChar, 200, subject),
				ParamSet.Add4Sql("@startpoint", SqlDbType.Decimal, Convert.ToDecimal(startPoint)),
				ParamSet.Add4Sql("@endpoint", SqlDbType.Decimal, Convert.ToDecimal(endPoint)),
				ParamSet.Add4Sql("@unitpoint", SqlDbType.Decimal, Convert.ToDecimal(unitPoint)),
				ParamSet.Add4Sql("@rating", SqlDbType.Decimal, Convert.ToDecimal(rating)),
				ParamSet.Add4Sql("@comment1", SqlDbType.NVarChar, 1000, comment1),
				ParamSet.Add4Sql("@comment2", SqlDbType.NVarChar, 1000, comment2),
				ParamSet.Add4Sql("@alias", SqlDbType.VarChar, 20, alias)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCompetencyRequestSetWrite", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 평가표와 평가표인스턴스를 삭제한다.
		/// </summary>
		/// <param name="competencyCode"></param>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public int DeleteCompetency(int competencyCode, int domainID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@competencycode", SqlDbType.Int, 4, competencyCode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCompetencyDelete", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		///  평가표 중항목을 삭제한다.
		/// </summary>
		/// <param name="competencyCode"></param>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public int DeleteCompetencyClass(int classCode, int domainID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@classcode", SqlDbType.Int, 4, classCode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCompetencyClassDelete", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		///  평가표 소항목을 삭제한다.
		/// </summary>
		/// <param name="reqCode"></param>
		/// <param name="domainID"></param>
		/// <returns></returns>
		public int DeleteCompetencyRequest(int reqCode, int domainID)
		{
			int iReturn = 0;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@reqcode", SqlDbType.Int, 4, reqCode),
				ParamSet.Add4Sql("@dn_id", SqlDbType.Int, 4, domainID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessCompetencyRequestDelete", parameters);

			using (DbBase db = new DbBase())
			{
				iReturn = int.Parse(db.ExecuteNonQueryNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData));
			}

			return iReturn;
		}

		/// <summary>
		/// 평가자 리스트 및 평가 정보 가져오기
		/// </summary>
		/// <param name="competencyID"></param>
		/// <param name="classID"></param>
		/// <param name="reqID"></param>
		/// <returns></returns>
		public DataSet GetEvalList(int domainID, string xfAlias, int OID)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@domainID", SqlDbType.Int, 4, domainID),
				ParamSet.Add4Sql("@XFAlias", SqlDbType.VarChar, 20, xfAlias),
				ParamSet.Add4Sql("@OID", SqlDbType.Int, 4, OID)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetEvalList", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}

		/// <summary>
		/// 워크아이템 점수와 전체점수를 조회한다.
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="wid"></param>
		/// <returns></returns>
		public DataSet GetEvaluationPoint(int oid, int wid)
		{
			DataSet dsReturn = null;

			SqlParameter[] parameters = new SqlParameter[]
			{
				ParamSet.Add4Sql("@oid", SqlDbType.Int, 4, oid),
				ParamSet.Add4Sql("@wid", SqlDbType.Int, 4, wid)
			};

			ParamData pData = new ParamData("admin.ph_up_ProcessGetEvaluationPoint", parameters);

			using (DbBase db = new DbBase())
			{
				dsReturn = db.ExecuteDatasetNTx(this.ConnectionString, MethodInfo.GetCurrentMethod(), pData);
			}

			return dsReturn;
		}
	}
}
