using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:MenJin
	/// </summary>
	public partial class MenJin
	{
		public MenJin()
		{}
		#region  基本方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid MenJinUuid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from MenJin");
			strSql.Append(" where MenJinUuid=@MenJinUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@MenJinUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = MenJinUuid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ClassLibrary1.Model.MenJin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into MenJin(");
			strSql.Append("CreateTime,MenJinUuid,carNumber,cardStatus,cardType,channelCode,channelName,deptName,deviceCode,deviceName,enterOrExit,recordid,openResult,openType,paperNumber,personCode,personId,personName,recordImage,swingTime)");
			strSql.Append(" values (");
			strSql.Append("@CreateTime,@MenJinUuid,@carNumber,@cardStatus,@cardType,@channelCode,@channelName,@deptName,@deviceCode,@deviceName,@enterOrExit,@recordid,@openResult,@openType,@paperNumber,@personCode,@personId,@personName,@recordImage,@swingTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@MenJinUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@carNumber", SqlDbType.VarChar,255),
					new SqlParameter("@cardStatus", SqlDbType.Int,4),
					new SqlParameter("@cardType", SqlDbType.Int,4),
					new SqlParameter("@channelCode", SqlDbType.VarChar,255),
					new SqlParameter("@channelName", SqlDbType.VarChar,255),
					new SqlParameter("@deptName", SqlDbType.VarChar,255),
					new SqlParameter("@deviceCode", SqlDbType.VarChar,255),
					new SqlParameter("@deviceName", SqlDbType.VarChar,255),
					new SqlParameter("@enterOrExit", SqlDbType.Int,4),
					new SqlParameter("@recordid", SqlDbType.VarChar,255),
					new SqlParameter("@openResult", SqlDbType.Int,4),
					new SqlParameter("@openType", SqlDbType.Int,4),
					new SqlParameter("@paperNumber", SqlDbType.VarChar,255),
					new SqlParameter("@personCode", SqlDbType.VarChar,255),
					new SqlParameter("@personId", SqlDbType.Int,4),
					new SqlParameter("@personName", SqlDbType.VarChar,255),
					new SqlParameter("@recordImage", SqlDbType.VarChar,255),
					new SqlParameter("@swingTime", SqlDbType.VarChar,255)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = Guid.NewGuid();
			parameters[2].Value = model.carNumber;
			parameters[3].Value = model.cardStatus;
			parameters[4].Value = model.cardType;
			parameters[5].Value = model.channelCode;
			parameters[6].Value = model.channelName;
			parameters[7].Value = model.deptName;
			parameters[8].Value = model.deviceCode;
			parameters[9].Value = model.deviceName;
			parameters[10].Value = model.enterOrExit;
			parameters[11].Value = model.recordid;
			parameters[12].Value = model.openResult;
			parameters[13].Value = model.openType;
			parameters[14].Value = model.paperNumber;
			parameters[15].Value = model.personCode;
			parameters[16].Value = model.personId;
			parameters[17].Value = model.personName;
			parameters[18].Value = model.recordImage;
			parameters[19].Value = model.swingTime;

			object obj = DbHelperSql.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ClassLibrary1.Model.MenJin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update MenJin set ");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("carNumber=@carNumber,");
			strSql.Append("cardStatus=@cardStatus,");
			strSql.Append("cardType=@cardType,");
			strSql.Append("channelCode=@channelCode,");
			strSql.Append("channelName=@channelName,");
			strSql.Append("deptName=@deptName,");
			strSql.Append("deviceCode=@deviceCode,");
			strSql.Append("deviceName=@deviceName,");
			strSql.Append("enterOrExit=@enterOrExit,");
			strSql.Append("recordid=@recordid,");
			strSql.Append("openResult=@openResult,");
			strSql.Append("openType=@openType,");
			strSql.Append("paperNumber=@paperNumber,");
			strSql.Append("personCode=@personCode,");
			strSql.Append("personId=@personId,");
			strSql.Append("personName=@personName,");
			strSql.Append("recordImage=@recordImage,");
			strSql.Append("swingTime=@swingTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@carNumber", SqlDbType.VarChar,255),
					new SqlParameter("@cardStatus", SqlDbType.Int,4),
					new SqlParameter("@cardType", SqlDbType.Int,4),
					new SqlParameter("@channelCode", SqlDbType.VarChar,255),
					new SqlParameter("@channelName", SqlDbType.VarChar,255),
					new SqlParameter("@deptName", SqlDbType.VarChar,255),
					new SqlParameter("@deviceCode", SqlDbType.VarChar,255),
					new SqlParameter("@deviceName", SqlDbType.VarChar,255),
					new SqlParameter("@enterOrExit", SqlDbType.Int,4),
					new SqlParameter("@recordid", SqlDbType.VarChar,255),
					new SqlParameter("@openResult", SqlDbType.Int,4),
					new SqlParameter("@openType", SqlDbType.Int,4),
					new SqlParameter("@paperNumber", SqlDbType.VarChar,255),
					new SqlParameter("@personCode", SqlDbType.VarChar,255),
					new SqlParameter("@personId", SqlDbType.Int,4),
					new SqlParameter("@personName", SqlDbType.VarChar,255),
					new SqlParameter("@recordImage", SqlDbType.VarChar,255),
					new SqlParameter("@swingTime", SqlDbType.VarChar,255),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@MenJinUuid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.carNumber;
			parameters[2].Value = model.cardStatus;
			parameters[3].Value = model.cardType;
			parameters[4].Value = model.channelCode;
			parameters[5].Value = model.channelName;
			parameters[6].Value = model.deptName;
			parameters[7].Value = model.deviceCode;
			parameters[8].Value = model.deviceName;
			parameters[9].Value = model.enterOrExit;
			parameters[10].Value = model.recordid;
			parameters[11].Value = model.openResult;
			parameters[12].Value = model.openType;
			parameters[13].Value = model.paperNumber;
			parameters[14].Value = model.personCode;
			parameters[15].Value = model.personId;
			parameters[16].Value = model.personName;
			parameters[17].Value = model.recordImage;
			parameters[18].Value = model.swingTime;
			parameters[19].Value = model.ID;
			parameters[20].Value = model.MenJinUuid;

			int rows=DbHelperSql.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MenJin ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSql.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid MenJinUuid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MenJin ");
			strSql.Append(" where MenJinUuid=@MenJinUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@MenJinUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = MenJinUuid;

			int rows=DbHelperSql.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from MenJin ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSql.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ClassLibrary1.Model.MenJin GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CreateTime,MenJinUuid,carNumber,cardStatus,cardType,channelCode,channelName,deptName,deviceCode,deviceName,enterOrExit,recordid,openResult,openType,paperNumber,personCode,personId,personName,recordImage,swingTime from MenJin ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ClassLibrary1.Model.MenJin model=new ClassLibrary1.Model.MenJin();
			DataSet ds=DbHelperSql.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ClassLibrary1.Model.MenJin DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.MenJin model=new ClassLibrary1.Model.MenJin();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["MenJinUuid"]!=null && row["MenJinUuid"].ToString()!="")
				{
					model.MenJinUuid= new Guid(row["MenJinUuid"].ToString());
				}
				if(row["carNumber"]!=null)
				{
					model.carNumber=row["carNumber"].ToString();
				}
				if(row["cardStatus"]!=null && row["cardStatus"].ToString()!="")
				{
					model.cardStatus=int.Parse(row["cardStatus"].ToString());
				}
				if(row["cardType"]!=null && row["cardType"].ToString()!="")
				{
					model.cardType=int.Parse(row["cardType"].ToString());
				}
				if(row["channelCode"]!=null)
				{
					model.channelCode=row["channelCode"].ToString();
				}
				if(row["channelName"]!=null)
				{
					model.channelName=row["channelName"].ToString();
				}
				if(row["deptName"]!=null)
				{
					model.deptName=row["deptName"].ToString();
				}
				if(row["deviceCode"]!=null)
				{
					model.deviceCode=row["deviceCode"].ToString();
				}
				if(row["deviceName"]!=null)
				{
					model.deviceName=row["deviceName"].ToString();
				}
				if(row["enterOrExit"]!=null && row["enterOrExit"].ToString()!="")
				{
					model.enterOrExit=int.Parse(row["enterOrExit"].ToString());
				}
				if(row["recordid"]!=null)
				{
					model.recordid=row["recordid"].ToString();
				}
				if(row["openResult"]!=null && row["openResult"].ToString()!="")
				{
					model.openResult=int.Parse(row["openResult"].ToString());
				}
				if(row["openType"]!=null && row["openType"].ToString()!="")
				{
					model.openType=int.Parse(row["openType"].ToString());
				}
				if(row["paperNumber"]!=null)
				{
					model.paperNumber=row["paperNumber"].ToString();
				}
				if(row["personCode"]!=null)
				{
					model.personCode=row["personCode"].ToString();
				}
				if(row["personId"]!=null && row["personId"].ToString()!="")
				{
					model.personId=int.Parse(row["personId"].ToString());
				}
				if(row["personName"]!=null)
				{
					model.personName=row["personName"].ToString();
				}
				if(row["recordImage"]!=null)
				{
					model.recordImage=row["recordImage"].ToString();
				}
				if(row["swingTime"]!=null)
				{
					model.swingTime=row["swingTime"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CreateTime,MenJinUuid,carNumber,cardStatus,cardType,channelCode,channelName,deptName,deviceCode,deviceName,enterOrExit,recordid,openResult,openType,paperNumber,personCode,personId,personName,recordImage,swingTime ");
			strSql.Append(" FROM MenJin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSql.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,CreateTime,MenJinUuid,carNumber,cardStatus,cardType,channelCode,channelName,deptName,deviceCode,deviceName,enterOrExit,recordid,openResult,openType,paperNumber,personCode,personId,personName,recordImage,swingTime ");
			strSql.Append(" FROM MenJin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSql.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM MenJin ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSql.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from MenJin T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSql.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "MenJin";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSql.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  基本方法
		#region  扩展方法

		#endregion  扩展方法
	}
}

