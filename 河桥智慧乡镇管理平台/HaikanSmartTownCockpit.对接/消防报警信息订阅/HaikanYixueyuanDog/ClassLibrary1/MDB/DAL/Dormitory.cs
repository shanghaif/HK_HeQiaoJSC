using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:Dormitory
	/// </summary>
	public partial class Dormitory
	{
		public Dormitory()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid DormitoryUuid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Dormitory");
			strSql.Append(" where DormitoryUuid=@DormitoryUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@DormitoryUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = DormitoryUuid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ClassLibrary1.Model.Dormitory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Dormitory(");
			strSql.Append("CreateTime,DormitoryUuid,DormitoryName,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid)");
			strSql.Append(" values (");
			strSql.Append("@CreateTime,@DormitoryUuid,@DormitoryName,@IsDeleted,@AddTime,@AddPeople,@SchoolDistrictUuid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@DormitoryUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@DormitoryName", SqlDbType.VarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.VarChar,255),
					new SqlParameter("@SchoolDistrictUuid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.DormitoryUuid;
			parameters[2].Value = model.DormitoryName;
			parameters[3].Value = model.IsDeleted;
			parameters[4].Value = model.AddTime;
			parameters[5].Value = model.AddPeople;
			parameters[6].Value = model.SchoolDistrictUuid;

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
		public bool Update(ClassLibrary1.Model.Dormitory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Dormitory set ");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("DormitoryName=@DormitoryName,");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddPeople=@AddPeople,");
			strSql.Append("SchoolDistrictUuid=@SchoolDistrictUuid");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@DormitoryName", SqlDbType.VarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.VarChar,255),
					new SqlParameter("@SchoolDistrictUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@DormitoryUuid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.DormitoryName;
			parameters[2].Value = model.IsDeleted;
			parameters[3].Value = model.AddTime;
			parameters[4].Value = model.AddPeople;
			parameters[5].Value = model.SchoolDistrictUuid;
			parameters[6].Value = model.ID;
			parameters[7].Value = model.DormitoryUuid;

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
			strSql.Append("delete from Dormitory ");
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
		public bool Delete(Guid DormitoryUuid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Dormitory ");
			strSql.Append(" where DormitoryUuid=@DormitoryUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@DormitoryUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = DormitoryUuid;

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
			strSql.Append("delete from Dormitory ");
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
		public ClassLibrary1.Model.Dormitory GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CreateTime,DormitoryUuid,DormitoryName,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid from Dormitory ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ClassLibrary1.Model.Dormitory model=new ClassLibrary1.Model.Dormitory();
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
		public ClassLibrary1.Model.Dormitory DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.Dormitory model=new ClassLibrary1.Model.Dormitory();
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
				if(row["DormitoryUuid"]!=null && row["DormitoryUuid"].ToString()!="")
				{
					model.DormitoryUuid= new Guid(row["DormitoryUuid"].ToString());
				}
				if(row["DormitoryName"]!=null)
				{
					model.DormitoryName=row["DormitoryName"].ToString();
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
				if(row["SchoolDistrictUuid"]!=null && row["SchoolDistrictUuid"].ToString()!="")
				{
					model.SchoolDistrictUuid= new Guid(row["SchoolDistrictUuid"].ToString());
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
			strSql.Append("select ID,CreateTime,DormitoryUuid,DormitoryName,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid ");
			strSql.Append(" FROM Dormitory ");
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
			strSql.Append(" ID,CreateTime,DormitoryUuid,DormitoryName,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid ");
			strSql.Append(" FROM Dormitory ");
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
			strSql.Append("select count(1) FROM Dormitory ");
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
			strSql.Append(")AS Row, T.*  from Dormitory T ");
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
			parameters[0].Value = "Dormitory";
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

