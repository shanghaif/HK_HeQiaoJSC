using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan2.DBHelper;
namespace big_data.DAL
{
	/// <summary>
	/// 数据访问类:Town
	/// </summary>
	public partial class Town
	{
		public Town()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid TownUuid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Town");
			strSql.Append(" where TownUuid=@TownUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@TownUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TownUuid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(big_data.Model.Town model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Town(");
			strSql.Append("TownUuid,IsDeleted,TownName,TownPeople,PartyMember,Geographical,Company,SjTownUuid,TownGrade,Lon,Lat,AddTime,AddPeople)");
			strSql.Append(" values (");
			strSql.Append("@TownUuid,@IsDeleted,@TownName,@TownPeople,@PartyMember,@Geographical,@Company,@SjTownUuid,@TownGrade,@Lon,@Lat,@AddTime,@AddPeople)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@TownUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@TownName", SqlDbType.NVarChar,-1),
					new SqlParameter("@TownPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@PartyMember", SqlDbType.NVarChar,255),
					new SqlParameter("@Geographical", SqlDbType.NVarChar,255),
					new SqlParameter("@Company", SqlDbType.NVarChar,255),
					new SqlParameter("@SjTownUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TownGrade", SqlDbType.NVarChar,255),
					new SqlParameter("@Lon", SqlDbType.NVarChar,255),
					new SqlParameter("@Lat", SqlDbType.NVarChar,255),
					new SqlParameter("@AddTime", SqlDbType.NVarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.NVarChar,255)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.IsDeleted;
			parameters[2].Value = model.TownName;
			parameters[3].Value = model.TownPeople;
			parameters[4].Value = model.PartyMember;
			parameters[5].Value = model.Geographical;
			parameters[6].Value = model.Company;
			parameters[7].Value = Guid.NewGuid();
			parameters[8].Value = model.TownGrade;
			parameters[9].Value = model.Lon;
			parameters[10].Value = model.Lat;
			parameters[11].Value = model.AddTime;
			parameters[12].Value = model.AddPeople;

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
		public bool Update(big_data.Model.Town model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Town set ");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("TownName=@TownName,");
			strSql.Append("TownPeople=@TownPeople,");
			strSql.Append("PartyMember=@PartyMember,");
			strSql.Append("Geographical=@Geographical,");
			strSql.Append("Company=@Company,");
			strSql.Append("SjTownUuid=@SjTownUuid,");
			strSql.Append("TownGrade=@TownGrade,");
			strSql.Append("Lon=@Lon,");
			strSql.Append("Lat=@Lat,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddPeople=@AddPeople");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@TownName", SqlDbType.NVarChar,-1),
					new SqlParameter("@TownPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@PartyMember", SqlDbType.NVarChar,255),
					new SqlParameter("@Geographical", SqlDbType.NVarChar,255),
					new SqlParameter("@Company", SqlDbType.NVarChar,255),
					new SqlParameter("@SjTownUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TownGrade", SqlDbType.NVarChar,255),
					new SqlParameter("@Lon", SqlDbType.NVarChar,255),
					new SqlParameter("@Lat", SqlDbType.NVarChar,255),
					new SqlParameter("@AddTime", SqlDbType.NVarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@TownUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.IsDeleted;
			parameters[1].Value = model.TownName;
			parameters[2].Value = model.TownPeople;
			parameters[3].Value = model.PartyMember;
			parameters[4].Value = model.Geographical;
			parameters[5].Value = model.Company;
			parameters[6].Value = model.SjTownUuid;
			parameters[7].Value = model.TownGrade;
			parameters[8].Value = model.Lon;
			parameters[9].Value = model.Lat;
			parameters[10].Value = model.AddTime;
			parameters[11].Value = model.AddPeople;
			parameters[12].Value = model.TownUuid;
			parameters[13].Value = model.ID;

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
			strSql.Append("delete from Town ");
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
		public bool Delete(Guid TownUuid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Town ");
			strSql.Append(" where TownUuid=@TownUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@TownUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TownUuid;

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
			strSql.Append("delete from Town ");
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
		public big_data.Model.Town GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TownUuid,ID,IsDeleted,TownName,TownPeople,PartyMember,Geographical,Company,SjTownUuid,TownGrade,Lon,Lat,AddTime,AddPeople from Town ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			big_data.Model.Town model=new big_data.Model.Town();
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
		public big_data.Model.Town DataRowToModel(DataRow row)
		{
			big_data.Model.Town model=new big_data.Model.Town();
			if (row != null)
			{
				if(row["TownUuid"]!=null && row["TownUuid"].ToString()!="")
				{
					model.TownUuid= new Guid(row["TownUuid"].ToString());
				}
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["IsDeleted"]!=null && row["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=int.Parse(row["IsDeleted"].ToString());
				}
				if(row["TownName"]!=null)
				{
					model.TownName=row["TownName"].ToString();
				}
				if(row["TownPeople"]!=null)
				{
					model.TownPeople=row["TownPeople"].ToString();
				}
				if(row["PartyMember"]!=null)
				{
					model.PartyMember=row["PartyMember"].ToString();
				}
				if(row["Geographical"]!=null)
				{
					model.Geographical=row["Geographical"].ToString();
				}
				if(row["Company"]!=null)
				{
					model.Company=row["Company"].ToString();
				}
				if(row["SjTownUuid"]!=null && row["SjTownUuid"].ToString()!="")
				{
					model.SjTownUuid= new Guid(row["SjTownUuid"].ToString());
				}
				if(row["TownGrade"]!=null)
				{
					model.TownGrade=row["TownGrade"].ToString();
				}
				if(row["Lon"]!=null)
				{
					model.Lon=row["Lon"].ToString();
				}
				if(row["Lat"]!=null)
				{
					model.Lat=row["Lat"].ToString();
				}
				if(row["AddTime"]!=null)
				{
					model.AddTime=row["AddTime"].ToString();
				}
				if(row["AddPeople"]!=null)
				{
					model.AddPeople=row["AddPeople"].ToString();
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
			strSql.Append("select TownUuid,ID,IsDeleted,TownName,TownPeople,PartyMember,Geographical,Company,SjTownUuid,TownGrade,Lon,Lat,AddTime,AddPeople ");
			strSql.Append(" FROM Town ");
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
			strSql.Append(" TownUuid,ID,IsDeleted,TownName,TownPeople,PartyMember,Geographical,Company,SjTownUuid,TownGrade,Lon,Lat,AddTime,AddPeople ");
			strSql.Append(" FROM Town ");
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
			strSql.Append("select count(1) FROM Town ");
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
			strSql.Append(")AS Row, T.*  from Town T ");
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
			parameters[0].Value = "Town";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSql.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

