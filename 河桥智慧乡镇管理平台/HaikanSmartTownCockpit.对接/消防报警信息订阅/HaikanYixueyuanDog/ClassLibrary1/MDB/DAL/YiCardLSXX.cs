using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:YiCardLSXX
	/// </summary>
	public partial class YiCardLSXX
	{
		public YiCardLSXX()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid YicardUuid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from YiCardLSXX");
			strSql.Append(" where YicardUuid=@YicardUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@YicardUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = YicardUuid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ClassLibrary1.Model.YiCardLSXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YiCardLSXX(");
			strSql.Append("YicardUuid,YicardName,Possessor,ConsumType,ExpenseCal,Balance,IsDeleted,AddTime,AddPeople,ConsumTime,Address,SiteName,DepName)");
			strSql.Append(" values (");
			strSql.Append("@YicardUuid,@YicardName,@Possessor,@ConsumType,@ExpenseCal,@Balance,@IsDeleted,@AddTime,@AddPeople,@ConsumTime,@Address,@SiteName,@DepName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@YicardUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@YicardName", SqlDbType.VarChar,255),
					new SqlParameter("@Possessor", SqlDbType.VarChar,255),
					new SqlParameter("@ConsumType", SqlDbType.VarChar,255),
					new SqlParameter("@ExpenseCal", SqlDbType.VarChar,255),
					new SqlParameter("@Balance", SqlDbType.VarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.VarChar,255),
					new SqlParameter("@ConsumTime", SqlDbType.VarChar,255),
					new SqlParameter("@Address", SqlDbType.VarChar,255),
					new SqlParameter("@SiteName", SqlDbType.VarChar,255),
					new SqlParameter("@DepName", SqlDbType.VarChar,255)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.YicardName;
			parameters[2].Value = model.Possessor;
			parameters[3].Value = model.ConsumType;
			parameters[4].Value = model.ExpenseCal;
			parameters[5].Value = model.Balance;
			parameters[6].Value = model.IsDeleted;
			parameters[7].Value = model.AddTime;
			parameters[8].Value = model.AddPeople;
			parameters[9].Value = model.ConsumTime;
			parameters[10].Value = model.Address;
			parameters[11].Value = model.SiteName;
			parameters[12].Value = model.DepName;

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
		public bool Update(ClassLibrary1.Model.YiCardLSXX model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YiCardLSXX set ");
			strSql.Append("YicardName=@YicardName,");
			strSql.Append("Possessor=@Possessor,");
			strSql.Append("ConsumType=@ConsumType,");
			strSql.Append("ExpenseCal=@ExpenseCal,");
			strSql.Append("Balance=@Balance,");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddPeople=@AddPeople,");
			strSql.Append("ConsumTime=@ConsumTime,");
			strSql.Append("Address=@Address,");
			strSql.Append("SiteName=@SiteName,");
			strSql.Append("DepName=@DepName");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@YicardName", SqlDbType.VarChar,255),
					new SqlParameter("@Possessor", SqlDbType.VarChar,255),
					new SqlParameter("@ConsumType", SqlDbType.VarChar,255),
					new SqlParameter("@ExpenseCal", SqlDbType.VarChar,255),
					new SqlParameter("@Balance", SqlDbType.VarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.VarChar,255),
					new SqlParameter("@ConsumTime", SqlDbType.VarChar,255),
					new SqlParameter("@Address", SqlDbType.VarChar,255),
					new SqlParameter("@SiteName", SqlDbType.VarChar,255),
					new SqlParameter("@DepName", SqlDbType.VarChar,255),
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@YicardUuid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.YicardName;
			parameters[1].Value = model.Possessor;
			parameters[2].Value = model.ConsumType;
			parameters[3].Value = model.ExpenseCal;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.IsDeleted;
			parameters[6].Value = model.AddTime;
			parameters[7].Value = model.AddPeople;
			parameters[8].Value = model.ConsumTime;
			parameters[9].Value = model.Address;
			parameters[10].Value = model.SiteName;
			parameters[11].Value = model.DepName;
			parameters[12].Value = model.Id;
			parameters[13].Value = model.YicardUuid;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YiCardLSXX ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool Delete(Guid YicardUuid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YiCardLSXX ");
			strSql.Append(" where YicardUuid=@YicardUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@YicardUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = YicardUuid;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from YiCardLSXX ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public ClassLibrary1.Model.YiCardLSXX GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,YicardUuid,YicardName,Possessor,ConsumType,ExpenseCal,Balance,IsDeleted,AddTime,AddPeople,ConsumTime,Address,SiteName,DepName from YiCardLSXX ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			ClassLibrary1.Model.YiCardLSXX model=new ClassLibrary1.Model.YiCardLSXX();
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
		public ClassLibrary1.Model.YiCardLSXX DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.YiCardLSXX model=new ClassLibrary1.Model.YiCardLSXX();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["YicardUuid"]!=null && row["YicardUuid"].ToString()!="")
				{
					model.YicardUuid= new Guid(row["YicardUuid"].ToString());
				}
				if(row["YicardName"]!=null)
				{
					model.YicardName=row["YicardName"].ToString();
				}
				if(row["Possessor"]!=null)
				{
					model.Possessor=row["Possessor"].ToString();
				}
				if(row["ConsumType"]!=null)
				{
					model.ConsumType=row["ConsumType"].ToString();
				}
				if(row["ExpenseCal"]!=null)
				{
					model.ExpenseCal=row["ExpenseCal"].ToString();
				}
				if(row["Balance"]!=null)
				{
					model.Balance=row["Balance"].ToString();
				}
				if(row["IsDeleted"]!=null && row["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=int.Parse(row["IsDeleted"].ToString());
				}
				if(row["AddTime"]!=null)
				{
					model.AddTime=row["AddTime"].ToString();
				}
				if(row["AddPeople"]!=null)
				{
					model.AddPeople=row["AddPeople"].ToString();
				}
				if(row["ConsumTime"]!=null)
				{
					model.ConsumTime=row["ConsumTime"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["SiteName"]!=null)
				{
					model.SiteName=row["SiteName"].ToString();
				}
				if(row["DepName"]!=null)
				{
					model.DepName=row["DepName"].ToString();
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
			strSql.Append("select Id,YicardUuid,YicardName,Possessor,ConsumType,ExpenseCal,Balance,IsDeleted,AddTime,AddPeople,ConsumTime,Address,SiteName,DepName ");
			strSql.Append(" FROM YiCardLSXX ");
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
			strSql.Append(" Id,YicardUuid,YicardName,Possessor,ConsumType,ExpenseCal,Balance,IsDeleted,AddTime,AddPeople,ConsumTime,Address,SiteName,DepName ");
			strSql.Append(" FROM YiCardLSXX ");
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
			strSql.Append("select count(1) FROM YiCardLSXX ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from YiCardLSXX T ");
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
			parameters[0].Value = "YiCardLSXX";
			parameters[1].Value = "Id";
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

