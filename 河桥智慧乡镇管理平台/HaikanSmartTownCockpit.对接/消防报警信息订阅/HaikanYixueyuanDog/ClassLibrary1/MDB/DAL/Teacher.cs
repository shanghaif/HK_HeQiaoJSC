using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:Teacher
	/// </summary>
	public partial class Teacher
	{
		public Teacher()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid TeacherUuid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Teacher");
			strSql.Append(" where TeacherUuid=@TeacherUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@TeacherUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TeacherUuid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ClassLibrary1.Model.Teacher model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Teacher(");
			strSql.Append("CreateTime,TeacherUuid,Name,Sex,Gh,DepartmentUuid,YiCard,DormitoryUuid,Picture,DormitoryNumUuid,Phone,IdCard,Email,Nation,Political,Religion,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid,ProfessionalTitle)");
			strSql.Append(" values (");
			strSql.Append("@CreateTime,@TeacherUuid,@Name,@Sex,@Gh,@DepartmentUuid,@YiCard,@DormitoryUuid,@Picture,@DormitoryNumUuid,@Phone,@IdCard,@Email,@Nation,@Political,@Religion,@IsDeleted,@AddTime,@AddPeople,@SchoolDistrictUuid,@ProfessionalTitle)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@TeacherUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Name", SqlDbType.VarChar,255),
					new SqlParameter("@Sex", SqlDbType.VarChar,255),
					new SqlParameter("@Gh", SqlDbType.VarChar,255),
					new SqlParameter("@DepartmentUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@YiCard", SqlDbType.VarChar,255),
					new SqlParameter("@DormitoryUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Picture", SqlDbType.VarChar,255),
					new SqlParameter("@DormitoryNumUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Phone", SqlDbType.VarChar,255),
					new SqlParameter("@IdCard", SqlDbType.VarChar,255),
					new SqlParameter("@Email", SqlDbType.VarChar,255),
					new SqlParameter("@Nation", SqlDbType.VarChar,255),
					new SqlParameter("@Political", SqlDbType.VarChar,255),
					new SqlParameter("@Religion", SqlDbType.VarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.VarChar,255),
					new SqlParameter("@SchoolDistrictUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ProfessionalTitle", SqlDbType.VarChar,255)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.TeacherUuid;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.Sex;
			parameters[4].Value = model.Gh;
			parameters[5].Value = model.DepartmentUuid;
			parameters[6].Value = model.YiCard;
			parameters[7].Value = model.DormitoryUuid;
			parameters[8].Value = model.Picture;
			parameters[9].Value = model.DormitoryNumUuid;
			parameters[10].Value = model.Phone;
			parameters[11].Value = model.IdCard;
			parameters[12].Value = model.Email;
			parameters[13].Value = model.Nation;
			parameters[14].Value = model.Political;
			parameters[15].Value = model.Religion;
			parameters[16].Value = model.IsDeleted;
			parameters[17].Value = model.AddTime;
			parameters[18].Value = model.AddPeople;
			parameters[19].Value = model.SchoolDistrictUuid;
			parameters[20].Value = model.ProfessionalTitle;

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
		public bool Update(ClassLibrary1.Model.Teacher model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Teacher set ");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("Name=@Name,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Gh=@Gh,");
			strSql.Append("DepartmentUuid=@DepartmentUuid,");
			strSql.Append("YiCard=@YiCard,");
			strSql.Append("DormitoryUuid=@DormitoryUuid,");
			strSql.Append("Picture=@Picture,");
			strSql.Append("DormitoryNumUuid=@DormitoryNumUuid,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("IdCard=@IdCard,");
			strSql.Append("Email=@Email,");
			strSql.Append("Nation=@Nation,");
			strSql.Append("Political=@Political,");
			strSql.Append("Religion=@Religion,");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddPeople=@AddPeople,");
			strSql.Append("SchoolDistrictUuid=@SchoolDistrictUuid,");
			strSql.Append("ProfessionalTitle=@ProfessionalTitle");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@Name", SqlDbType.VarChar,255),
					new SqlParameter("@Sex", SqlDbType.VarChar,255),
					new SqlParameter("@Gh", SqlDbType.VarChar,255),
					new SqlParameter("@DepartmentUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@YiCard", SqlDbType.VarChar,255),
					new SqlParameter("@DormitoryUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Picture", SqlDbType.VarChar,255),
					new SqlParameter("@DormitoryNumUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Phone", SqlDbType.VarChar,255),
					new SqlParameter("@IdCard", SqlDbType.VarChar,255),
					new SqlParameter("@Email", SqlDbType.VarChar,255),
					new SqlParameter("@Nation", SqlDbType.VarChar,255),
					new SqlParameter("@Political", SqlDbType.VarChar,255),
					new SqlParameter("@Religion", SqlDbType.VarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.VarChar,255),
					new SqlParameter("@SchoolDistrictUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ProfessionalTitle", SqlDbType.VarChar,255),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TeacherUuid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Sex;
			parameters[3].Value = model.Gh;
			parameters[4].Value = model.DepartmentUuid;
			parameters[5].Value = model.YiCard;
			parameters[6].Value = model.DormitoryUuid;
			parameters[7].Value = model.Picture;
			parameters[8].Value = model.DormitoryNumUuid;
			parameters[9].Value = model.Phone;
			parameters[10].Value = model.IdCard;
			parameters[11].Value = model.Email;
			parameters[12].Value = model.Nation;
			parameters[13].Value = model.Political;
			parameters[14].Value = model.Religion;
			parameters[15].Value = model.IsDeleted;
			parameters[16].Value = model.AddTime;
			parameters[17].Value = model.AddPeople;
			parameters[18].Value = model.SchoolDistrictUuid;
			parameters[19].Value = model.ProfessionalTitle;
			parameters[20].Value = model.ID;
			parameters[21].Value = model.TeacherUuid;

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
			strSql.Append("delete from Teacher ");
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
		public bool Delete(Guid TeacherUuid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Teacher ");
			strSql.Append(" where TeacherUuid=@TeacherUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@TeacherUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = TeacherUuid;

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
			strSql.Append("delete from Teacher ");
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
		public ClassLibrary1.Model.Teacher GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CreateTime,TeacherUuid,Name,Sex,Gh,DepartmentUuid,YiCard,DormitoryUuid,Picture,DormitoryNumUuid,Phone,IdCard,Email,Nation,Political,Religion,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid,ProfessionalTitle from Teacher ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ClassLibrary1.Model.Teacher model=new ClassLibrary1.Model.Teacher();
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
		public ClassLibrary1.Model.Teacher DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.Teacher model=new ClassLibrary1.Model.Teacher();
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
				if(row["TeacherUuid"]!=null && row["TeacherUuid"].ToString()!="")
				{
					model.TeacherUuid= new Guid(row["TeacherUuid"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Sex"]!=null)
				{
					model.Sex=row["Sex"].ToString();
				}
				if(row["Gh"]!=null)
				{
					model.Gh=row["Gh"].ToString();
				}
				if(row["DepartmentUuid"]!=null && row["DepartmentUuid"].ToString()!="")
				{
					model.DepartmentUuid= new Guid(row["DepartmentUuid"].ToString());
				}
				if(row["YiCard"]!=null)
				{
					model.YiCard=row["YiCard"].ToString();
				}
				if(row["DormitoryUuid"]!=null && row["DormitoryUuid"].ToString()!="")
				{
					model.DormitoryUuid= new Guid(row["DormitoryUuid"].ToString());
				}
				if(row["Picture"]!=null)
				{
					model.Picture=row["Picture"].ToString();
				}
				if(row["DormitoryNumUuid"]!=null && row["DormitoryNumUuid"].ToString()!="")
				{
					model.DormitoryNumUuid= new Guid(row["DormitoryNumUuid"].ToString());
				}
				if(row["Phone"]!=null)
				{
					model.Phone=row["Phone"].ToString();
				}
				if(row["IdCard"]!=null)
				{
					model.IdCard=row["IdCard"].ToString();
				}
				if(row["Email"]!=null)
				{
					model.Email=row["Email"].ToString();
				}
				if(row["Nation"]!=null)
				{
					model.Nation=row["Nation"].ToString();
				}
				if(row["Political"]!=null)
				{
					model.Political=row["Political"].ToString();
				}
				if(row["Religion"]!=null)
				{
					model.Religion=row["Religion"].ToString();
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
				if(row["ProfessionalTitle"]!=null)
				{
					model.ProfessionalTitle=row["ProfessionalTitle"].ToString();
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
			strSql.Append("select ID,CreateTime,TeacherUuid,Name,Sex,Gh,DepartmentUuid,YiCard,DormitoryUuid,Picture,DormitoryNumUuid,Phone,IdCard,Email,Nation,Political,Religion,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid,ProfessionalTitle ");
			strSql.Append(" FROM Teacher ");
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
			strSql.Append(" ID,CreateTime,TeacherUuid,Name,Sex,Gh,DepartmentUuid,YiCard,DormitoryUuid,Picture,DormitoryNumUuid,Phone,IdCard,Email,Nation,Political,Religion,IsDeleted,AddTime,AddPeople,SchoolDistrictUuid,ProfessionalTitle ");
			strSql.Append(" FROM Teacher ");
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
			strSql.Append("select count(1) FROM Teacher ");
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
			strSql.Append(")AS Row, T.*  from Teacher T ");
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
			parameters[0].Value = "Teacher";
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
		public  bool Yicard(string Gh,string ykt)
        {
			string sql = "update Teacher set YiCard='" + ykt + "' where Gh='" + Gh + "'";
			int rows = DbHelperSql.ExecuteSql(sql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		#endregion  ExtensionMethod
	}
}

