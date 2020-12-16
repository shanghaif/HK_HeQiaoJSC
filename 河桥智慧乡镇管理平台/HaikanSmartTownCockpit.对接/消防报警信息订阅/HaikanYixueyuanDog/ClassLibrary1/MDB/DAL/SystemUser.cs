using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:SystemUser
	/// </summary>
	public partial class SystemUser
	{
		public SystemUser()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSql.GetMaxId("ID", "SystemUser"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid SystemUserUUID,int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SystemUser");
			strSql.Append(" where SystemUserUUID=@SystemUserUUID and ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemUserUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = SystemUserUUID;
			parameters[1].Value = ID;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ClassLibrary1.Model.SystemUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SystemUser(");
			strSql.Append("SystemUserUUID,LoginName,RealName,UserIdCard,PassWord,UserType,SchoolUUID,AddTime,AddPeople,IsDeleted,ManageDepartment,ZaiGang,Phone,Email,Sex,InTime,Types,Address,SystemRoleUUid,Remarks,StaffNum,Wechat,Topic,Content,Train,Main,Job,HealthCertificate,IsStaff,IsCreEdu,PersonnelType,ReviewStatus,CardNumber,IsBlacklist)");
			strSql.Append(" values (");
			strSql.Append("@SystemUserUUID,@LoginName,@RealName,@UserIdCard,@PassWord,@UserType,@SchoolUUID,@AddTime,@AddPeople,@IsDeleted,@ManageDepartment,@ZaiGang,@Phone,@Email,@Sex,@InTime,@Types,@Address,@SystemRoleUUid,@Remarks,@StaffNum,@Wechat,@Topic,@Content,@Train,@Main,@Job,@HealthCertificate,@IsStaff,@IsCreEdu,@PersonnelType,@ReviewStatus,@CardNumber,@IsBlacklist)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemUserUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LoginName", SqlDbType.NVarChar,255),
					new SqlParameter("@RealName", SqlDbType.NVarChar,255),
					new SqlParameter("@UserIdCard", SqlDbType.NVarChar,255),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,255),
					new SqlParameter("@UserType", SqlDbType.Int,4),
					new SqlParameter("@SchoolUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AddTime", SqlDbType.NVarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@ManageDepartment", SqlDbType.Text),
					new SqlParameter("@ZaiGang", SqlDbType.VarChar,255),
					new SqlParameter("@Phone", SqlDbType.VarChar,255),
					new SqlParameter("@Email", SqlDbType.VarChar,255),
					new SqlParameter("@Sex", SqlDbType.VarChar,255),
					new SqlParameter("@InTime", SqlDbType.VarChar,20),
					new SqlParameter("@Types", SqlDbType.VarChar,255),
					new SqlParameter("@Address", SqlDbType.VarChar,255),
					new SqlParameter("@SystemRoleUUid", SqlDbType.VarChar,255),
					new SqlParameter("@Remarks", SqlDbType.VarChar,255),
					new SqlParameter("@StaffNum", SqlDbType.NVarChar,255),
					new SqlParameter("@Wechat", SqlDbType.VarChar,255),
					new SqlParameter("@Topic", SqlDbType.VarChar,255),
					new SqlParameter("@Content", SqlDbType.VarChar,255),
					new SqlParameter("@Train", SqlDbType.VarChar,255),
					new SqlParameter("@Main", SqlDbType.VarChar,255),
					new SqlParameter("@Job", SqlDbType.NVarChar,255),
					new SqlParameter("@HealthCertificate", SqlDbType.Int,4),
					new SqlParameter("@IsStaff", SqlDbType.Int,4),
					new SqlParameter("@IsCreEdu", SqlDbType.Int,4),
					new SqlParameter("@PersonnelType", SqlDbType.VarChar,255),
					new SqlParameter("@ReviewStatus", SqlDbType.VarChar,255),
					new SqlParameter("@CardNumber", SqlDbType.VarChar,255),
					new SqlParameter("@IsBlacklist", SqlDbType.VarChar,255)};
			parameters[0].Value = model.SystemUserUUID;
			parameters[1].Value = model.LoginName;
			parameters[2].Value = model.RealName;
			parameters[3].Value = model.UserIdCard;
			parameters[4].Value = model.PassWord;
			parameters[5].Value = model.UserType;
			parameters[6].Value = model.SchoolUUID;
			parameters[7].Value = model.AddTime;
			parameters[8].Value = model.AddPeople;
			parameters[9].Value = model.IsDeleted;
			parameters[10].Value = model.ManageDepartment;
			parameters[11].Value = model.ZaiGang;
			parameters[12].Value = model.Phone;
			parameters[13].Value = model.Email;
			parameters[14].Value = model.Sex;
			parameters[15].Value = model.InTime;
			parameters[16].Value = model.Types;
			parameters[17].Value = model.Address;
			parameters[18].Value = model.SystemRoleUUid;
			parameters[19].Value = model.Remarks;
			parameters[20].Value = model.StaffNum;
			parameters[21].Value = model.Wechat;
			parameters[22].Value = model.Topic;
			parameters[23].Value = model.Content;
			parameters[24].Value = model.Train;
			parameters[25].Value = model.Main;
			parameters[26].Value = model.Job;
			parameters[27].Value = model.HealthCertificate;
			parameters[28].Value = model.IsStaff;
			parameters[29].Value = model.IsCreEdu;
			parameters[30].Value = model.PersonnelType;
			parameters[31].Value = model.ReviewStatus;
			parameters[32].Value = model.CardNumber;
			parameters[33].Value = model.IsBlacklist;

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
		public bool Update(ClassLibrary1.Model.SystemUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SystemUser set ");
			strSql.Append("LoginName=@LoginName,");
			strSql.Append("RealName=@RealName,");
			strSql.Append("UserIdCard=@UserIdCard,");
			strSql.Append("PassWord=@PassWord,");
			strSql.Append("UserType=@UserType,");
			strSql.Append("SchoolUUID=@SchoolUUID,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddPeople=@AddPeople,");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("ManageDepartment=@ManageDepartment,");
			strSql.Append("ZaiGang=@ZaiGang,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("Email=@Email,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("InTime=@InTime,");
			strSql.Append("Types=@Types,");
			strSql.Append("Address=@Address,");
			strSql.Append("SystemRoleUUid=@SystemRoleUUid,");
			strSql.Append("Remarks=@Remarks,");
			strSql.Append("StaffNum=@StaffNum,");
			strSql.Append("Wechat=@Wechat,");
			strSql.Append("Topic=@Topic,");
			strSql.Append("Content=@Content,");
			strSql.Append("Train=@Train,");
			strSql.Append("Main=@Main,");
			strSql.Append("Job=@Job,");
			strSql.Append("HealthCertificate=@HealthCertificate,");
			strSql.Append("IsStaff=@IsStaff,");
			strSql.Append("IsCreEdu=@IsCreEdu,");
			strSql.Append("PersonnelType=@PersonnelType,");
			strSql.Append("ReviewStatus=@ReviewStatus,");
			strSql.Append("CardNumber=@CardNumber,");
			strSql.Append("IsBlacklist=@IsBlacklist");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@LoginName", SqlDbType.NVarChar,255),
					new SqlParameter("@RealName", SqlDbType.NVarChar,255),
					new SqlParameter("@UserIdCard", SqlDbType.NVarChar,255),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,255),
					new SqlParameter("@UserType", SqlDbType.Int,4),
					new SqlParameter("@SchoolUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AddTime", SqlDbType.NVarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@ManageDepartment", SqlDbType.Text),
					new SqlParameter("@ZaiGang", SqlDbType.VarChar,255),
					new SqlParameter("@Phone", SqlDbType.VarChar,255),
					new SqlParameter("@Email", SqlDbType.VarChar,255),
					new SqlParameter("@Sex", SqlDbType.VarChar,255),
					new SqlParameter("@InTime", SqlDbType.VarChar,20),
					new SqlParameter("@Types", SqlDbType.VarChar,255),
					new SqlParameter("@Address", SqlDbType.VarChar,255),
					new SqlParameter("@SystemRoleUUid", SqlDbType.VarChar,255),
					new SqlParameter("@Remarks", SqlDbType.VarChar,255),
					new SqlParameter("@StaffNum", SqlDbType.NVarChar,255),
					new SqlParameter("@Wechat", SqlDbType.VarChar,255),
					new SqlParameter("@Topic", SqlDbType.VarChar,255),
					new SqlParameter("@Content", SqlDbType.VarChar,255),
					new SqlParameter("@Train", SqlDbType.VarChar,255),
					new SqlParameter("@Main", SqlDbType.VarChar,255),
					new SqlParameter("@Job", SqlDbType.NVarChar,255),
					new SqlParameter("@HealthCertificate", SqlDbType.Int,4),
					new SqlParameter("@IsStaff", SqlDbType.Int,4),
					new SqlParameter("@IsCreEdu", SqlDbType.Int,4),
					new SqlParameter("@PersonnelType", SqlDbType.VarChar,255),
					new SqlParameter("@ReviewStatus", SqlDbType.VarChar,255),
					new SqlParameter("@CardNumber", SqlDbType.VarChar,255),
					new SqlParameter("@IsBlacklist", SqlDbType.VarChar,255),
					new SqlParameter("@SystemUserUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.LoginName;
			parameters[1].Value = model.RealName;
			parameters[2].Value = model.UserIdCard;
			parameters[3].Value = model.PassWord;
			parameters[4].Value = model.UserType;
			parameters[5].Value = model.SchoolUUID;
			parameters[6].Value = model.AddTime;
			parameters[7].Value = model.AddPeople;
			parameters[8].Value = model.IsDeleted;
			parameters[9].Value = model.ManageDepartment;
			parameters[10].Value = model.ZaiGang;
			parameters[11].Value = model.Phone;
			parameters[12].Value = model.Email;
			parameters[13].Value = model.Sex;
			parameters[14].Value = model.InTime;
			parameters[15].Value = model.Types;
			parameters[16].Value = model.Address;
			parameters[17].Value = model.SystemRoleUUid;
			parameters[18].Value = model.Remarks;
			parameters[19].Value = model.StaffNum;
			parameters[20].Value = model.Wechat;
			parameters[21].Value = model.Topic;
			parameters[22].Value = model.Content;
			parameters[23].Value = model.Train;
			parameters[24].Value = model.Main;
			parameters[25].Value = model.Job;
			parameters[26].Value = model.HealthCertificate;
			parameters[27].Value = model.IsStaff;
			parameters[28].Value = model.IsCreEdu;
			parameters[29].Value = model.PersonnelType;
			parameters[30].Value = model.ReviewStatus;
			parameters[31].Value = model.CardNumber;
			parameters[32].Value = model.IsBlacklist;
			parameters[33].Value = model.SystemUserUUID;
			parameters[34].Value = model.ID;

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
			strSql.Append("delete from SystemUser ");
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
		public bool Delete(Guid SystemUserUUID,int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SystemUser ");
			strSql.Append(" where SystemUserUUID=@SystemUserUUID and ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@SystemUserUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = SystemUserUUID;
			parameters[1].Value = ID;

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
			strSql.Append("delete from SystemUser ");
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
		public ClassLibrary1.Model.SystemUser GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SystemUserUUID,LoginName,RealName,UserIdCard,PassWord,UserType,SchoolUUID,AddTime,AddPeople,IsDeleted,ManageDepartment,ID,ZaiGang,Phone,Email,Sex,InTime,Types,Address,SystemRoleUUid,Remarks,StaffNum,Wechat,Topic,Content,Train,Main,Job,HealthCertificate,IsStaff,IsCreEdu,PersonnelType,ReviewStatus,CardNumber,IsBlacklist from SystemUser ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ClassLibrary1.Model.SystemUser model=new ClassLibrary1.Model.SystemUser();
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
		public ClassLibrary1.Model.SystemUser DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.SystemUser model=new ClassLibrary1.Model.SystemUser();
			if (row != null)
			{
				if(row["SystemUserUUID"]!=null && row["SystemUserUUID"].ToString()!="")
				{
					model.SystemUserUUID= new Guid(row["SystemUserUUID"].ToString());
				}
				if(row["LoginName"]!=null)
				{
					model.LoginName=row["LoginName"].ToString();
				}
				if(row["RealName"]!=null)
				{
					model.RealName=row["RealName"].ToString();
				}
				if(row["UserIdCard"]!=null)
				{
					model.UserIdCard=row["UserIdCard"].ToString();
				}
				if(row["PassWord"]!=null)
				{
					model.PassWord=row["PassWord"].ToString();
				}
				if(row["UserType"]!=null && row["UserType"].ToString()!="")
				{
					model.UserType=int.Parse(row["UserType"].ToString());
				}
				if(row["SchoolUUID"]!=null && row["SchoolUUID"].ToString()!="")
				{
					model.SchoolUUID= new Guid(row["SchoolUUID"].ToString());
				}
				if(row["AddTime"]!=null)
				{
					model.AddTime=row["AddTime"].ToString();
				}
				if(row["AddPeople"]!=null)
				{
					model.AddPeople=row["AddPeople"].ToString();
				}
				if(row["IsDeleted"]!=null && row["IsDeleted"].ToString()!="")
				{
					model.IsDeleted=int.Parse(row["IsDeleted"].ToString());
				}
				if(row["ManageDepartment"]!=null)
				{
					model.ManageDepartment=row["ManageDepartment"].ToString();
				}
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["ZaiGang"]!=null)
				{
					model.ZaiGang=row["ZaiGang"].ToString();
				}
				if(row["Phone"]!=null)
				{
					model.Phone=row["Phone"].ToString();
				}
				if(row["Email"]!=null)
				{
					model.Email=row["Email"].ToString();
				}
				if(row["Sex"]!=null)
				{
					model.Sex=row["Sex"].ToString();
				}
				if(row["InTime"]!=null)
				{
					model.InTime=row["InTime"].ToString();
				}
				if(row["Types"]!=null)
				{
					model.Types=row["Types"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["SystemRoleUUid"]!=null)
				{
					model.SystemRoleUUid=row["SystemRoleUUid"].ToString();
				}
				if(row["Remarks"]!=null)
				{
					model.Remarks=row["Remarks"].ToString();
				}
				if(row["StaffNum"]!=null)
				{
					model.StaffNum=row["StaffNum"].ToString();
				}
				if(row["Wechat"]!=null)
				{
					model.Wechat=row["Wechat"].ToString();
				}
				if(row["Topic"]!=null)
				{
					model.Topic=row["Topic"].ToString();
				}
				if(row["Content"]!=null)
				{
					model.Content=row["Content"].ToString();
				}
				if(row["Train"]!=null)
				{
					model.Train=row["Train"].ToString();
				}
				if(row["Main"]!=null)
				{
					model.Main=row["Main"].ToString();
				}
				if(row["Job"]!=null)
				{
					model.Job=row["Job"].ToString();
				}
				if(row["HealthCertificate"]!=null && row["HealthCertificate"].ToString()!="")
				{
					model.HealthCertificate=int.Parse(row["HealthCertificate"].ToString());
				}
				if(row["IsStaff"]!=null && row["IsStaff"].ToString()!="")
				{
					model.IsStaff=int.Parse(row["IsStaff"].ToString());
				}
				if(row["IsCreEdu"]!=null && row["IsCreEdu"].ToString()!="")
				{
					model.IsCreEdu=int.Parse(row["IsCreEdu"].ToString());
				}
				if(row["PersonnelType"]!=null)
				{
					model.PersonnelType=row["PersonnelType"].ToString();
				}
				if(row["ReviewStatus"]!=null)
				{
					model.ReviewStatus=row["ReviewStatus"].ToString();
				}
				if(row["CardNumber"]!=null)
				{
					model.CardNumber=row["CardNumber"].ToString();
				}
				if(row["IsBlacklist"]!=null)
				{
					model.IsBlacklist=row["IsBlacklist"].ToString();
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
			strSql.Append("select SystemUserUUID,LoginName,RealName,UserIdCard,PassWord,UserType,SchoolUUID,AddTime,AddPeople,IsDeleted,ManageDepartment,ID,ZaiGang,Phone,Email,Sex,InTime,Types,Address,SystemRoleUUid,Remarks,StaffNum,Wechat,Topic,Content,Train,Main,Job,HealthCertificate,IsStaff,IsCreEdu,PersonnelType,ReviewStatus,CardNumber,IsBlacklist ");
			strSql.Append(" FROM SystemUser ");
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
			strSql.Append(" SystemUserUUID,LoginName,RealName,UserIdCard,PassWord,UserType,SchoolUUID,AddTime,AddPeople,IsDeleted,ManageDepartment,ID,ZaiGang,Phone,Email,Sex,InTime,Types,Address,SystemRoleUUid,Remarks,StaffNum,Wechat,Topic,Content,Train,Main,Job,HealthCertificate,IsStaff,IsCreEdu,PersonnelType,ReviewStatus,CardNumber,IsBlacklist ");
			strSql.Append(" FROM SystemUser ");
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
			strSql.Append("select count(1) FROM SystemUser ");
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
			strSql.Append(")AS Row, T.*  from SystemUser T ");
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
			parameters[0].Value = "SystemUser";
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

