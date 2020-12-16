using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan2.DBHelper;
namespace big_data.DAL
{
	/// <summary>
	/// 数据访问类:Userinfoty
	/// </summary>
	public partial class Userinfoty
	{
		public Userinfoty()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSql.GetMaxId("ID", "Userinfoty"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid UserInfoUUID,int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Userinfoty");
			strSql.Append(" where UserInfoUUID=@UserInfoUUID and ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserInfoUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = UserInfoUUID;
			parameters[1].Value = ID;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(big_data.Model.Userinfoty model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Userinfoty(");
			strSql.Append("UserInfoUUID,IsDeleted,AddTime,AddPeople,RealName,UserIdCard,Wechat,Phone,Email,Sex,Address,DepartmentUUID,Birth,IdentityCard,Residence,Domicile,Nation,Education,QianYiSTime,QianYiETime,Settled,Occupation,DyStaues,Politics,position,Evaluate,OrganizationUuid,JoinArmy,ArmyTime,SettledTime,TownUuid,Defense,Category,Partybranch,JoinDate,Work,Age,Household,Relation,HouseholderName)");
			strSql.Append(" values (");
			strSql.Append("@UserInfoUUID,@IsDeleted,@AddTime,@AddPeople,@RealName,@UserIdCard,@Wechat,@Phone,@Email,@Sex,@Address,@DepartmentUUID,@Birth,@IdentityCard,@Residence,@Domicile,@Nation,@Education,@QianYiSTime,@QianYiETime,@Settled,@Occupation,@DyStaues,@Politics,@position,@Evaluate,@OrganizationUuid,@JoinArmy,@ArmyTime,@SettledTime,@TownUuid,@Defense,@Category,@Partybranch,@JoinDate,@Work,@Age,@Household,@Relation,@HouseholderName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserInfoUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.NVarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@RealName", SqlDbType.NVarChar,255),
					new SqlParameter("@UserIdCard", SqlDbType.NVarChar,255),
					new SqlParameter("@Wechat", SqlDbType.NVarChar,255),
					new SqlParameter("@Phone", SqlDbType.NVarChar,255),
					new SqlParameter("@Email", SqlDbType.NVarChar,255),
					new SqlParameter("@Sex", SqlDbType.NVarChar,255),
					new SqlParameter("@Address", SqlDbType.NVarChar,255),
					new SqlParameter("@DepartmentUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Birth", SqlDbType.NVarChar,255),
					new SqlParameter("@IdentityCard", SqlDbType.NVarChar,255),
					new SqlParameter("@Residence", SqlDbType.NVarChar,255),
					new SqlParameter("@Domicile", SqlDbType.NVarChar,255),
					new SqlParameter("@Nation", SqlDbType.NVarChar,255),
					new SqlParameter("@Education", SqlDbType.NVarChar,255),
					new SqlParameter("@QianYiSTime", SqlDbType.NVarChar,255),
					new SqlParameter("@QianYiETime", SqlDbType.NVarChar,255),
					new SqlParameter("@Settled", SqlDbType.NVarChar,255),
					new SqlParameter("@Occupation", SqlDbType.NVarChar,255),
					new SqlParameter("@DyStaues", SqlDbType.NVarChar,255),
					new SqlParameter("@Politics", SqlDbType.NVarChar,255),
					new SqlParameter("@position", SqlDbType.NVarChar,255),
					new SqlParameter("@Evaluate", SqlDbType.NVarChar,255),
					new SqlParameter("@OrganizationUuid", SqlDbType.NVarChar,255),
					new SqlParameter("@JoinArmy", SqlDbType.NVarChar,255),
					new SqlParameter("@ArmyTime", SqlDbType.NVarChar,255),
					new SqlParameter("@SettledTime", SqlDbType.NVarChar,255),
					new SqlParameter("@TownUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Defense", SqlDbType.NVarChar,255),
					new SqlParameter("@Category", SqlDbType.NVarChar,255),
					new SqlParameter("@Partybranch", SqlDbType.NVarChar,255),
					new SqlParameter("@JoinDate", SqlDbType.NVarChar,255),
					new SqlParameter("@Work", SqlDbType.NVarChar,255),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Household", SqlDbType.NVarChar,255),
					new SqlParameter("@Relation", SqlDbType.NVarChar,100),
					new SqlParameter("@HouseholderName", SqlDbType.NVarChar,100)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.IsDeleted;
			parameters[2].Value = model.AddTime;
			parameters[3].Value = model.AddPeople;
			parameters[4].Value = model.RealName;
			parameters[5].Value = model.UserIdCard;
			parameters[6].Value = model.Wechat;
			parameters[7].Value = model.Phone;
			parameters[8].Value = model.Email;
			parameters[9].Value = model.Sex;
			parameters[10].Value = model.Address;
			parameters[11].Value = Guid.NewGuid();
			parameters[12].Value = model.Birth;
			parameters[13].Value = model.IdentityCard;
			parameters[14].Value = model.Residence;
			parameters[15].Value = model.Domicile;
			parameters[16].Value = model.Nation;
			parameters[17].Value = model.Education;
			parameters[18].Value = model.QianYiSTime;
			parameters[19].Value = model.QianYiETime;
			parameters[20].Value = model.Settled;
			parameters[21].Value = model.Occupation;
			parameters[22].Value = model.DyStaues;
			parameters[23].Value = model.Politics;
			parameters[24].Value = model.position;
			parameters[25].Value = model.Evaluate;
			parameters[26].Value = model.OrganizationUuid;
			parameters[27].Value = model.JoinArmy;
			parameters[28].Value = model.ArmyTime;
			parameters[29].Value = model.SettledTime;
			parameters[30].Value = Guid.NewGuid();
			parameters[31].Value = model.Defense;
			parameters[32].Value = model.Category;
			parameters[33].Value = model.Partybranch;
			parameters[34].Value = model.JoinDate;
			parameters[35].Value = model.Work;
			parameters[36].Value = model.Age;
			parameters[37].Value = model.Household;
			parameters[38].Value = model.Relation;
			parameters[39].Value = model.HouseholderName;

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
		public bool Update(big_data.Model.Userinfoty model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Userinfoty set ");
			strSql.Append("IsDeleted=@IsDeleted,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("AddPeople=@AddPeople,");
			strSql.Append("RealName=@RealName,");
			strSql.Append("UserIdCard=@UserIdCard,");
			strSql.Append("Wechat=@Wechat,");
			strSql.Append("Phone=@Phone,");
			strSql.Append("Email=@Email,");
			strSql.Append("Sex=@Sex,");
			strSql.Append("Address=@Address,");
			strSql.Append("DepartmentUUID=@DepartmentUUID,");
			strSql.Append("Birth=@Birth,");
			strSql.Append("IdentityCard=@IdentityCard,");
			strSql.Append("Residence=@Residence,");
			strSql.Append("Domicile=@Domicile,");
			strSql.Append("Nation=@Nation,");
			strSql.Append("Education=@Education,");
			strSql.Append("QianYiSTime=@QianYiSTime,");
			strSql.Append("QianYiETime=@QianYiETime,");
			strSql.Append("Settled=@Settled,");
			strSql.Append("Occupation=@Occupation,");
			strSql.Append("DyStaues=@DyStaues,");
			strSql.Append("Politics=@Politics,");
			strSql.Append("position=@position,");
			strSql.Append("Evaluate=@Evaluate,");
			strSql.Append("OrganizationUuid=@OrganizationUuid,");
			strSql.Append("JoinArmy=@JoinArmy,");
			strSql.Append("ArmyTime=@ArmyTime,");
			strSql.Append("SettledTime=@SettledTime,");
			strSql.Append("TownUuid=@TownUuid,");
			strSql.Append("Defense=@Defense,");
			strSql.Append("Category=@Category,");
			strSql.Append("Partybranch=@Partybranch,");
			strSql.Append("JoinDate=@JoinDate,");
			strSql.Append("Work=@Work,");
			strSql.Append("Age=@Age,");
			strSql.Append("Household=@Household,");
			strSql.Append("Relation=@Relation,");
			strSql.Append("HouseholderName=@HouseholderName");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@IsDeleted", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.NVarChar,255),
					new SqlParameter("@AddPeople", SqlDbType.NVarChar,255),
					new SqlParameter("@RealName", SqlDbType.NVarChar,255),
					new SqlParameter("@UserIdCard", SqlDbType.NVarChar,255),
					new SqlParameter("@Wechat", SqlDbType.NVarChar,255),
					new SqlParameter("@Phone", SqlDbType.NVarChar,255),
					new SqlParameter("@Email", SqlDbType.NVarChar,255),
					new SqlParameter("@Sex", SqlDbType.NVarChar,255),
					new SqlParameter("@Address", SqlDbType.NVarChar,255),
					new SqlParameter("@DepartmentUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Birth", SqlDbType.NVarChar,255),
					new SqlParameter("@IdentityCard", SqlDbType.NVarChar,255),
					new SqlParameter("@Residence", SqlDbType.NVarChar,255),
					new SqlParameter("@Domicile", SqlDbType.NVarChar,255),
					new SqlParameter("@Nation", SqlDbType.NVarChar,255),
					new SqlParameter("@Education", SqlDbType.NVarChar,255),
					new SqlParameter("@QianYiSTime", SqlDbType.NVarChar,255),
					new SqlParameter("@QianYiETime", SqlDbType.NVarChar,255),
					new SqlParameter("@Settled", SqlDbType.NVarChar,255),
					new SqlParameter("@Occupation", SqlDbType.NVarChar,255),
					new SqlParameter("@DyStaues", SqlDbType.NVarChar,255),
					new SqlParameter("@Politics", SqlDbType.NVarChar,255),
					new SqlParameter("@position", SqlDbType.NVarChar,255),
					new SqlParameter("@Evaluate", SqlDbType.NVarChar,255),
					new SqlParameter("@OrganizationUuid", SqlDbType.NVarChar,255),
					new SqlParameter("@JoinArmy", SqlDbType.NVarChar,255),
					new SqlParameter("@ArmyTime", SqlDbType.NVarChar,255),
					new SqlParameter("@SettledTime", SqlDbType.NVarChar,255),
					new SqlParameter("@TownUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Defense", SqlDbType.NVarChar,255),
					new SqlParameter("@Category", SqlDbType.NVarChar,255),
					new SqlParameter("@Partybranch", SqlDbType.NVarChar,255),
					new SqlParameter("@JoinDate", SqlDbType.NVarChar,255),
					new SqlParameter("@Work", SqlDbType.NVarChar,255),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Household", SqlDbType.NVarChar,255),
					new SqlParameter("@Relation", SqlDbType.NVarChar,100),
					new SqlParameter("@HouseholderName", SqlDbType.NVarChar,100),
					new SqlParameter("@UserInfoUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.IsDeleted;
			parameters[1].Value = model.AddTime;
			parameters[2].Value = model.AddPeople;
			parameters[3].Value = model.RealName;
			parameters[4].Value = model.UserIdCard;
			parameters[5].Value = model.Wechat;
			parameters[6].Value = model.Phone;
			parameters[7].Value = model.Email;
			parameters[8].Value = model.Sex;
			parameters[9].Value = model.Address;
			parameters[10].Value = model.DepartmentUUID;
			parameters[11].Value = model.Birth;
			parameters[12].Value = model.IdentityCard;
			parameters[13].Value = model.Residence;
			parameters[14].Value = model.Domicile;
			parameters[15].Value = model.Nation;
			parameters[16].Value = model.Education;
			parameters[17].Value = model.QianYiSTime;
			parameters[18].Value = model.QianYiETime;
			parameters[19].Value = model.Settled;
			parameters[20].Value = model.Occupation;
			parameters[21].Value = model.DyStaues;
			parameters[22].Value = model.Politics;
			parameters[23].Value = model.position;
			parameters[24].Value = model.Evaluate;
			parameters[25].Value = model.OrganizationUuid;
			parameters[26].Value = model.JoinArmy;
			parameters[27].Value = model.ArmyTime;
			parameters[28].Value = model.SettledTime;
			parameters[29].Value = model.TownUuid;
			parameters[30].Value = model.Defense;
			parameters[31].Value = model.Category;
			parameters[32].Value = model.Partybranch;
			parameters[33].Value = model.JoinDate;
			parameters[34].Value = model.Work;
			parameters[35].Value = model.Age;
			parameters[36].Value = model.Household;
			parameters[37].Value = model.Relation;
			parameters[38].Value = model.HouseholderName;
			parameters[39].Value = model.UserInfoUUID;
			parameters[40].Value = model.ID;

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
			strSql.Append("delete from Userinfoty ");
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
		public bool Delete(Guid UserInfoUUID,int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Userinfoty ");
			strSql.Append(" where UserInfoUUID=@UserInfoUUID and ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@UserInfoUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ID", SqlDbType.Int,4)			};
			parameters[0].Value = UserInfoUUID;
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
			strSql.Append("delete from Userinfoty ");
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
		public big_data.Model.Userinfoty GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UserInfoUUID,ID,IsDeleted,AddTime,AddPeople,RealName,UserIdCard,Wechat,Phone,Email,Sex,Address,DepartmentUUID,Birth,IdentityCard,Residence,Domicile,Nation,Education,QianYiSTime,QianYiETime,Settled,Occupation,DyStaues,Politics,position,Evaluate,OrganizationUuid,JoinArmy,ArmyTime,SettledTime,TownUuid,Defense,Category,Partybranch,JoinDate,Work,Age,Household,Relation,HouseholderName from Userinfoty ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			big_data.Model.Userinfoty model=new big_data.Model.Userinfoty();
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
		public big_data.Model.Userinfoty DataRowToModel(DataRow row)
		{
			big_data.Model.Userinfoty model=new big_data.Model.Userinfoty();
			if (row != null)
			{
				if(row["UserInfoUUID"]!=null && row["UserInfoUUID"].ToString()!="")
				{
					model.UserInfoUUID= new Guid(row["UserInfoUUID"].ToString());
				}
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
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
				if(row["RealName"]!=null)
				{
					model.RealName=row["RealName"].ToString();
				}
				if(row["UserIdCard"]!=null)
				{
					model.UserIdCard=row["UserIdCard"].ToString();
				}
				if(row["Wechat"]!=null)
				{
					model.Wechat=row["Wechat"].ToString();
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
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["DepartmentUUID"]!=null && row["DepartmentUUID"].ToString()!="")
				{
					model.DepartmentUUID= new Guid(row["DepartmentUUID"].ToString());
				}
				if(row["Birth"]!=null)
				{
					model.Birth=row["Birth"].ToString();
				}
				if(row["IdentityCard"]!=null)
				{
					model.IdentityCard=row["IdentityCard"].ToString();
				}
				if(row["Residence"]!=null)
				{
					model.Residence=row["Residence"].ToString();
				}
				if(row["Domicile"]!=null)
				{
					model.Domicile=row["Domicile"].ToString();
				}
				if(row["Nation"]!=null)
				{
					model.Nation=row["Nation"].ToString();
				}
				if(row["Education"]!=null)
				{
					model.Education=row["Education"].ToString();
				}
				if(row["QianYiSTime"]!=null)
				{
					model.QianYiSTime=row["QianYiSTime"].ToString();
				}
				if(row["QianYiETime"]!=null)
				{
					model.QianYiETime=row["QianYiETime"].ToString();
				}
				if(row["Settled"]!=null)
				{
					model.Settled=row["Settled"].ToString();
				}
				if(row["Occupation"]!=null)
				{
					model.Occupation=row["Occupation"].ToString();
				}
				if(row["DyStaues"]!=null)
				{
					model.DyStaues=row["DyStaues"].ToString();
				}
				if(row["Politics"]!=null)
				{
					model.Politics=row["Politics"].ToString();
				}
				if(row["position"]!=null)
				{
					model.position=row["position"].ToString();
				}
				if(row["Evaluate"]!=null)
				{
					model.Evaluate=row["Evaluate"].ToString();
				}
				if(row["OrganizationUuid"]!=null)
				{
					model.OrganizationUuid=row["OrganizationUuid"].ToString();
				}
				if(row["JoinArmy"]!=null)
				{
					model.JoinArmy=row["JoinArmy"].ToString();
				}
				if(row["ArmyTime"]!=null)
				{
					model.ArmyTime=row["ArmyTime"].ToString();
				}
				if(row["SettledTime"]!=null)
				{
					model.SettledTime=row["SettledTime"].ToString();
				}
				if(row["TownUuid"]!=null && row["TownUuid"].ToString()!="")
				{
					model.TownUuid= new Guid(row["TownUuid"].ToString());
				}
				if(row["Defense"]!=null)
				{
					model.Defense=row["Defense"].ToString();
				}
				if(row["Category"]!=null)
				{
					model.Category=row["Category"].ToString();
				}
				if(row["Partybranch"]!=null)
				{
					model.Partybranch=row["Partybranch"].ToString();
				}
				if(row["JoinDate"]!=null)
				{
					model.JoinDate=row["JoinDate"].ToString();
				}
				if(row["Work"]!=null)
				{
					model.Work=row["Work"].ToString();
				}
				if(row["Age"]!=null && row["Age"].ToString()!="")
				{
					model.Age=int.Parse(row["Age"].ToString());
				}
				if(row["Household"]!=null)
				{
					model.Household=row["Household"].ToString();
				}
				if(row["Relation"]!=null)
				{
					model.Relation=row["Relation"].ToString();
				}
				if(row["HouseholderName"]!=null)
				{
					model.HouseholderName=row["HouseholderName"].ToString();
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
			strSql.Append("select UserInfoUUID,ID,IsDeleted,AddTime,AddPeople,RealName,UserIdCard,Wechat,Phone,Email,Sex,Address,DepartmentUUID,Birth,IdentityCard,Residence,Domicile,Nation,Education,QianYiSTime,QianYiETime,Settled,Occupation,DyStaues,Politics,position,Evaluate,OrganizationUuid,JoinArmy,ArmyTime,SettledTime,TownUuid,Defense,Category,Partybranch,JoinDate,Work,Age,Household,Relation,HouseholderName ");
			strSql.Append(" FROM Userinfoty ");
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
			strSql.Append(" UserInfoUUID,ID,IsDeleted,AddTime,AddPeople,RealName,UserIdCard,Wechat,Phone,Email,Sex,Address,DepartmentUUID,Birth,IdentityCard,Residence,Domicile,Nation,Education,QianYiSTime,QianYiETime,Settled,Occupation,DyStaues,Politics,position,Evaluate,OrganizationUuid,JoinArmy,ArmyTime,SettledTime,TownUuid,Defense,Category,Partybranch,JoinDate,Work,Age,Household,Relation,HouseholderName ");
			strSql.Append(" FROM Userinfoty ");
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
			strSql.Append("select count(1) FROM Userinfoty ");
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
			strSql.Append(")AS Row, T.*  from Userinfoty T ");
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
			parameters[0].Value = "Userinfoty";
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

