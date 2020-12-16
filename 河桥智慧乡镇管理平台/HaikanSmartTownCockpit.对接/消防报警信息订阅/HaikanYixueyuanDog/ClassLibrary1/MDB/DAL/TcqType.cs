using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:TcqType
	/// </summary>
	public partial class TcqType
	{
		public TcqType()
		{}
		#region  基本方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid Tcqguid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TcqType");
			strSql.Append(" where Tcqguid=@Tcqguid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tcqguid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = Tcqguid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ClassLibrary1.Model.TcqType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TcqType(");
			strSql.Append("Tcqguid,Tcqid,TcqName)");
			strSql.Append(" values (");
			strSql.Append("@Tcqguid,@Tcqid,@TcqName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Tcqguid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Tcqid", SqlDbType.Int,4),
					new SqlParameter("@TcqName", SqlDbType.VarChar,255)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.Tcqid;
			parameters[2].Value = model.TcqName;
			return DbHelperSql.Exists(strSql.ToString(), parameters);
			//string rows=DbHelperSql.GetSingleGUID(strSql.ToString(),parameters);
			//if (rows == "")
			//{
			//	return Guid.Empty;
			//}
			//else
			//{
			//	return Guid.Parse(rows.ToString());
			//}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ClassLibrary1.Model.TcqType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TcqType set ");
			strSql.Append("Tcqid=@Tcqid,");
			strSql.Append("TcqName=@TcqName");
			strSql.Append(" where Tcqguid=@Tcqguid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tcqid", SqlDbType.Int,4),
					new SqlParameter("@TcqName", SqlDbType.VarChar,255),
					new SqlParameter("@Tcqguid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.Tcqid;
			parameters[1].Value = model.TcqName;
			parameters[2].Value = model.Tcqguid;

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
		public bool Delete(Guid Tcqguid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TcqType ");
			strSql.Append(" where Tcqguid=@Tcqguid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tcqguid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = Tcqguid;

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
		public bool DeleteList(string Tcqguidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TcqType ");
			strSql.Append(" where Tcqguid in ("+Tcqguidlist + ")  ");
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
		public ClassLibrary1.Model.TcqType GetModel(Guid Tcqguid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Tcqguid,Tcqid,TcqName from TcqType ");
			strSql.Append(" where Tcqguid=@Tcqguid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Tcqguid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = Tcqguid;

			ClassLibrary1.Model.TcqType model=new ClassLibrary1.Model.TcqType();
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
		public ClassLibrary1.Model.TcqType DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.TcqType model=new ClassLibrary1.Model.TcqType();
			if (row != null)
			{
				if(row["Tcqguid"]!=null && row["Tcqguid"].ToString()!="")
				{
					model.Tcqguid= new Guid(row["Tcqguid"].ToString());
				}
				if(row["Tcqid"]!=null && row["Tcqid"].ToString()!="")
				{
					model.Tcqid=int.Parse(row["Tcqid"].ToString());
				}
				if(row["TcqName"]!=null)
				{
					model.TcqName=row["TcqName"].ToString();
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
			strSql.Append("select Tcqguid,Tcqid,TcqName ");
			strSql.Append(" FROM TcqType ");
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
			strSql.Append(" Tcqguid,Tcqid,TcqName ");
			strSql.Append(" FROM TcqType ");
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
			strSql.Append("select count(1) FROM TcqType ");
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
				strSql.Append("order by T.Tcqguid desc");
			}
			strSql.Append(")AS Row, T.*  from TcqType T ");
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
			parameters[0].Value = "TcqType";
			parameters[1].Value = "Tcqguid";
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

