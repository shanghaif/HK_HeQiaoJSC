using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan2.DBHelper;
namespace big_data.DAL
{
	/// <summary>
	/// 数据访问类:UnifiedAddressLibrary
	/// </summary>
	public partial class UnifiedAddressLibrary
	{
		public UnifiedAddressLibrary()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(big_data.Model.UnifiedAddressLibrary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into UnifiedAddressLibrary(");
			strSql.Append("UnifiedAddressLibraryUUID,oid,SOURCEADDRESS,CITY,COUNTY,TOWN,COMMUNITY,SQUAD,VILLAGE,SZONE,STREET,DOOR,RESREGION,BUILDING,BUILDING_NUM,UNIT,FLOOR,ROOM,GRID_CODE,BUILDING_CODE,HOUSE_CODE,CODE,CREATETIME,UPDATETIME,ISVALID,FROM_STATUS,TO_STATUS,BUILDING_PATH,DATASOURCE,REVERSE1,REVERSE2,LON,LAT,Z,ROOM_FLOOR,ADDRTYPE,GUID,INSERTTIME,SYSTEMID,ISDELETE,BELONG_BUILDING,ADDRESS_TYPE,REMARK,AddTime)");
			strSql.Append(" values (");
			strSql.Append("@UnifiedAddressLibraryUUID,@oid,@SOURCEADDRESS,@CITY,@COUNTY,@TOWN,@COMMUNITY,@SQUAD,@VILLAGE,@SZONE,@STREET,@DOOR,@RESREGION,@BUILDING,@BUILDING_NUM,@UNIT,@FLOOR,@ROOM,@GRID_CODE,@BUILDING_CODE,@HOUSE_CODE,@CODE,@CREATETIME,@UPDATETIME,@ISVALID,@FROM_STATUS,@TO_STATUS,@BUILDING_PATH,@DATASOURCE,@REVERSE1,@REVERSE2,@LON,@LAT,@Z,@ROOM_FLOOR,@ADDRTYPE,@GUID,@INSERTTIME,@SYSTEMID,@ISDELETE,@BELONG_BUILDING,@ADDRESS_TYPE,@REMARK,@AddTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UnifiedAddressLibraryUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@oid", SqlDbType.VarChar,255),
					new SqlParameter("@SOURCEADDRESS", SqlDbType.VarChar,255),
					new SqlParameter("@CITY", SqlDbType.VarChar,255),
					new SqlParameter("@COUNTY", SqlDbType.VarChar,255),
					new SqlParameter("@TOWN", SqlDbType.VarChar,255),
					new SqlParameter("@COMMUNITY", SqlDbType.VarChar,255),
					new SqlParameter("@SQUAD", SqlDbType.VarChar,255),
					new SqlParameter("@VILLAGE", SqlDbType.VarChar,255),
					new SqlParameter("@SZONE", SqlDbType.VarChar,255),
					new SqlParameter("@STREET", SqlDbType.VarChar,255),
					new SqlParameter("@DOOR", SqlDbType.VarChar,255),
					new SqlParameter("@RESREGION", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING_NUM", SqlDbType.VarChar,255),
					new SqlParameter("@UNIT", SqlDbType.VarChar,255),
					new SqlParameter("@FLOOR", SqlDbType.VarChar,255),
					new SqlParameter("@ROOM", SqlDbType.VarChar,255),
					new SqlParameter("@GRID_CODE", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING_CODE", SqlDbType.VarChar,255),
					new SqlParameter("@HOUSE_CODE", SqlDbType.VarChar,255),
					new SqlParameter("@CODE", SqlDbType.VarChar,255),
					new SqlParameter("@CREATETIME", SqlDbType.VarChar,255),
					new SqlParameter("@UPDATETIME", SqlDbType.VarChar,255),
					new SqlParameter("@ISVALID", SqlDbType.Int,4),
					new SqlParameter("@FROM_STATUS", SqlDbType.VarChar,255),
					new SqlParameter("@TO_STATUS", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING_PATH", SqlDbType.VarChar,255),
					new SqlParameter("@DATASOURCE", SqlDbType.VarChar,255),
					new SqlParameter("@REVERSE1", SqlDbType.VarChar,255),
					new SqlParameter("@REVERSE2", SqlDbType.VarChar,255),
					new SqlParameter("@LON", SqlDbType.VarChar,255),
					new SqlParameter("@LAT", SqlDbType.VarChar,255),
					new SqlParameter("@Z", SqlDbType.VarChar,255),
					new SqlParameter("@ROOM_FLOOR", SqlDbType.VarChar,255),
					new SqlParameter("@ADDRTYPE", SqlDbType.VarChar,255),
					new SqlParameter("@GUID", SqlDbType.VarChar,255),
					new SqlParameter("@INSERTTIME", SqlDbType.VarChar,255),
					new SqlParameter("@SYSTEMID", SqlDbType.VarChar,255),
					new SqlParameter("@ISDELETE", SqlDbType.Int,4),
					new SqlParameter("@BELONG_BUILDING", SqlDbType.VarChar,255),
					new SqlParameter("@ADDRESS_TYPE", SqlDbType.VarChar,255),
					new SqlParameter("@REMARK", SqlDbType.VarChar,255),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.oid;
			parameters[2].Value = model.SOURCEADDRESS;
			parameters[3].Value = model.CITY;
			parameters[4].Value = model.COUNTY;
			parameters[5].Value = model.TOWN;
			parameters[6].Value = model.COMMUNITY;
			parameters[7].Value = model.SQUAD;
			parameters[8].Value = model.VILLAGE;
			parameters[9].Value = model.SZONE;
			parameters[10].Value = model.STREET;
			parameters[11].Value = model.DOOR;
			parameters[12].Value = model.RESREGION;
			parameters[13].Value = model.BUILDING;
			parameters[14].Value = model.BUILDING_NUM;
			parameters[15].Value = model.UNIT;
			parameters[16].Value = model.FLOOR;
			parameters[17].Value = model.ROOM;
			parameters[18].Value = model.GRID_CODE;
			parameters[19].Value = model.BUILDING_CODE;
			parameters[20].Value = model.HOUSE_CODE;
			parameters[21].Value = model.CODE;
			parameters[22].Value = model.CREATETIME;
			parameters[23].Value = model.UPDATETIME;
			parameters[24].Value = model.ISVALID;
			parameters[25].Value = model.FROM_STATUS;
			parameters[26].Value = model.TO_STATUS;
			parameters[27].Value = model.BUILDING_PATH;
			parameters[28].Value = model.DATASOURCE;
			parameters[29].Value = model.REVERSE1;
			parameters[30].Value = model.REVERSE2;
			parameters[31].Value = model.LON;
			parameters[32].Value = model.LAT;
			parameters[33].Value = model.Z;
			parameters[34].Value = model.ROOM_FLOOR;
			parameters[35].Value = model.ADDRTYPE;
			parameters[36].Value = model.GUID;
			parameters[37].Value = model.INSERTTIME;
			parameters[38].Value = model.SYSTEMID;
			parameters[39].Value = model.ISDELETE;
			parameters[40].Value = model.BELONG_BUILDING;
			parameters[41].Value = model.ADDRESS_TYPE;
			parameters[42].Value = model.REMARK;
			parameters[43].Value = model.AddTime;

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
		public bool Update(big_data.Model.UnifiedAddressLibrary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update UnifiedAddressLibrary set ");
			strSql.Append("UnifiedAddressLibraryUUID=@UnifiedAddressLibraryUUID,");
			strSql.Append("oid=@oid,");
			strSql.Append("SOURCEADDRESS=@SOURCEADDRESS,");
			strSql.Append("CITY=@CITY,");
			strSql.Append("COUNTY=@COUNTY,");
			strSql.Append("TOWN=@TOWN,");
			strSql.Append("COMMUNITY=@COMMUNITY,");
			strSql.Append("SQUAD=@SQUAD,");
			strSql.Append("VILLAGE=@VILLAGE,");
			strSql.Append("SZONE=@SZONE,");
			strSql.Append("STREET=@STREET,");
			strSql.Append("DOOR=@DOOR,");
			strSql.Append("RESREGION=@RESREGION,");
			strSql.Append("BUILDING=@BUILDING,");
			strSql.Append("BUILDING_NUM=@BUILDING_NUM,");
			strSql.Append("UNIT=@UNIT,");
			strSql.Append("FLOOR=@FLOOR,");
			strSql.Append("ROOM=@ROOM,");
			strSql.Append("GRID_CODE=@GRID_CODE,");
			strSql.Append("BUILDING_CODE=@BUILDING_CODE,");
			strSql.Append("HOUSE_CODE=@HOUSE_CODE,");
			strSql.Append("CODE=@CODE,");
			strSql.Append("CREATETIME=@CREATETIME,");
			strSql.Append("UPDATETIME=@UPDATETIME,");
			strSql.Append("ISVALID=@ISVALID,");
			strSql.Append("FROM_STATUS=@FROM_STATUS,");
			strSql.Append("TO_STATUS=@TO_STATUS,");
			strSql.Append("BUILDING_PATH=@BUILDING_PATH,");
			strSql.Append("DATASOURCE=@DATASOURCE,");
			strSql.Append("REVERSE1=@REVERSE1,");
			strSql.Append("REVERSE2=@REVERSE2,");
			strSql.Append("LON=@LON,");
			strSql.Append("LAT=@LAT,");
			strSql.Append("Z=@Z,");
			strSql.Append("ROOM_FLOOR=@ROOM_FLOOR,");
			strSql.Append("ADDRTYPE=@ADDRTYPE,");
			strSql.Append("GUID=@GUID,");
			strSql.Append("INSERTTIME=@INSERTTIME,");
			strSql.Append("SYSTEMID=@SYSTEMID,");
			strSql.Append("ISDELETE=@ISDELETE,");
			strSql.Append("BELONG_BUILDING=@BELONG_BUILDING,");
			strSql.Append("ADDRESS_TYPE=@ADDRESS_TYPE,");
			strSql.Append("REMARK=@REMARK,");
			strSql.Append("AddTime=@AddTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@UnifiedAddressLibraryUUID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@oid", SqlDbType.VarChar,255),
					new SqlParameter("@SOURCEADDRESS", SqlDbType.VarChar,255),
					new SqlParameter("@CITY", SqlDbType.VarChar,255),
					new SqlParameter("@COUNTY", SqlDbType.VarChar,255),
					new SqlParameter("@TOWN", SqlDbType.VarChar,255),
					new SqlParameter("@COMMUNITY", SqlDbType.VarChar,255),
					new SqlParameter("@SQUAD", SqlDbType.VarChar,255),
					new SqlParameter("@VILLAGE", SqlDbType.VarChar,255),
					new SqlParameter("@SZONE", SqlDbType.VarChar,255),
					new SqlParameter("@STREET", SqlDbType.VarChar,255),
					new SqlParameter("@DOOR", SqlDbType.VarChar,255),
					new SqlParameter("@RESREGION", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING_NUM", SqlDbType.VarChar,255),
					new SqlParameter("@UNIT", SqlDbType.VarChar,255),
					new SqlParameter("@FLOOR", SqlDbType.VarChar,255),
					new SqlParameter("@ROOM", SqlDbType.VarChar,255),
					new SqlParameter("@GRID_CODE", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING_CODE", SqlDbType.VarChar,255),
					new SqlParameter("@HOUSE_CODE", SqlDbType.VarChar,255),
					new SqlParameter("@CODE", SqlDbType.VarChar,255),
					new SqlParameter("@CREATETIME", SqlDbType.VarChar,255),
					new SqlParameter("@UPDATETIME", SqlDbType.VarChar,255),
					new SqlParameter("@ISVALID", SqlDbType.Int,4),
					new SqlParameter("@FROM_STATUS", SqlDbType.VarChar,255),
					new SqlParameter("@TO_STATUS", SqlDbType.VarChar,255),
					new SqlParameter("@BUILDING_PATH", SqlDbType.VarChar,255),
					new SqlParameter("@DATASOURCE", SqlDbType.VarChar,255),
					new SqlParameter("@REVERSE1", SqlDbType.VarChar,255),
					new SqlParameter("@REVERSE2", SqlDbType.VarChar,255),
					new SqlParameter("@LON", SqlDbType.VarChar,255),
					new SqlParameter("@LAT", SqlDbType.VarChar,255),
					new SqlParameter("@Z", SqlDbType.VarChar,255),
					new SqlParameter("@ROOM_FLOOR", SqlDbType.VarChar,255),
					new SqlParameter("@ADDRTYPE", SqlDbType.VarChar,255),
					new SqlParameter("@GUID", SqlDbType.VarChar,255),
					new SqlParameter("@INSERTTIME", SqlDbType.VarChar,255),
					new SqlParameter("@SYSTEMID", SqlDbType.VarChar,255),
					new SqlParameter("@ISDELETE", SqlDbType.Int,4),
					new SqlParameter("@BELONG_BUILDING", SqlDbType.VarChar,255),
					new SqlParameter("@ADDRESS_TYPE", SqlDbType.VarChar,255),
					new SqlParameter("@REMARK", SqlDbType.VarChar,255),
					new SqlParameter("@AddTime", SqlDbType.VarChar,255),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.UnifiedAddressLibraryUUID;
			parameters[1].Value = model.oid;
			parameters[2].Value = model.SOURCEADDRESS;
			parameters[3].Value = model.CITY;
			parameters[4].Value = model.COUNTY;
			parameters[5].Value = model.TOWN;
			parameters[6].Value = model.COMMUNITY;
			parameters[7].Value = model.SQUAD;
			parameters[8].Value = model.VILLAGE;
			parameters[9].Value = model.SZONE;
			parameters[10].Value = model.STREET;
			parameters[11].Value = model.DOOR;
			parameters[12].Value = model.RESREGION;
			parameters[13].Value = model.BUILDING;
			parameters[14].Value = model.BUILDING_NUM;
			parameters[15].Value = model.UNIT;
			parameters[16].Value = model.FLOOR;
			parameters[17].Value = model.ROOM;
			parameters[18].Value = model.GRID_CODE;
			parameters[19].Value = model.BUILDING_CODE;
			parameters[20].Value = model.HOUSE_CODE;
			parameters[21].Value = model.CODE;
			parameters[22].Value = model.CREATETIME;
			parameters[23].Value = model.UPDATETIME;
			parameters[24].Value = model.ISVALID;
			parameters[25].Value = model.FROM_STATUS;
			parameters[26].Value = model.TO_STATUS;
			parameters[27].Value = model.BUILDING_PATH;
			parameters[28].Value = model.DATASOURCE;
			parameters[29].Value = model.REVERSE1;
			parameters[30].Value = model.REVERSE2;
			parameters[31].Value = model.LON;
			parameters[32].Value = model.LAT;
			parameters[33].Value = model.Z;
			parameters[34].Value = model.ROOM_FLOOR;
			parameters[35].Value = model.ADDRTYPE;
			parameters[36].Value = model.GUID;
			parameters[37].Value = model.INSERTTIME;
			parameters[38].Value = model.SYSTEMID;
			parameters[39].Value = model.ISDELETE;
			parameters[40].Value = model.BELONG_BUILDING;
			parameters[41].Value = model.ADDRESS_TYPE;
			parameters[42].Value = model.REMARK;
			parameters[43].Value = model.AddTime;
			parameters[44].Value = model.ID;

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
			strSql.Append("delete from UnifiedAddressLibrary ");
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from UnifiedAddressLibrary ");
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
		public big_data.Model.UnifiedAddressLibrary GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 UnifiedAddressLibraryUUID,ID,oid,SOURCEADDRESS,CITY,COUNTY,TOWN,COMMUNITY,SQUAD,VILLAGE,SZONE,STREET,DOOR,RESREGION,BUILDING,BUILDING_NUM,UNIT,FLOOR,ROOM,GRID_CODE,BUILDING_CODE,HOUSE_CODE,CODE,CREATETIME,UPDATETIME,ISVALID,FROM_STATUS,TO_STATUS,BUILDING_PATH,DATASOURCE,REVERSE1,REVERSE2,LON,LAT,Z,ROOM_FLOOR,ADDRTYPE,GUID,INSERTTIME,SYSTEMID,ISDELETE,BELONG_BUILDING,ADDRESS_TYPE,REMARK,AddTime from UnifiedAddressLibrary ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			big_data.Model.UnifiedAddressLibrary model=new big_data.Model.UnifiedAddressLibrary();
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
		public big_data.Model.UnifiedAddressLibrary DataRowToModel(DataRow row)
		{
			big_data.Model.UnifiedAddressLibrary model=new big_data.Model.UnifiedAddressLibrary();
			if (row != null)
			{
				if(row["UnifiedAddressLibraryUUID"]!=null && row["UnifiedAddressLibraryUUID"].ToString()!="")
				{
					model.UnifiedAddressLibraryUUID= new Guid(row["UnifiedAddressLibraryUUID"].ToString());
				}
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["oid"]!=null)
				{
					model.oid=row["oid"].ToString();
				}
				if(row["SOURCEADDRESS"]!=null)
				{
					model.SOURCEADDRESS=row["SOURCEADDRESS"].ToString();
				}
				if(row["CITY"]!=null)
				{
					model.CITY=row["CITY"].ToString();
				}
				if(row["COUNTY"]!=null)
				{
					model.COUNTY=row["COUNTY"].ToString();
				}
				if(row["TOWN"]!=null)
				{
					model.TOWN=row["TOWN"].ToString();
				}
				if(row["COMMUNITY"]!=null)
				{
					model.COMMUNITY=row["COMMUNITY"].ToString();
				}
				if(row["SQUAD"]!=null)
				{
					model.SQUAD=row["SQUAD"].ToString();
				}
				if(row["VILLAGE"]!=null)
				{
					model.VILLAGE=row["VILLAGE"].ToString();
				}
				if(row["SZONE"]!=null)
				{
					model.SZONE=row["SZONE"].ToString();
				}
				if(row["STREET"]!=null)
				{
					model.STREET=row["STREET"].ToString();
				}
				if(row["DOOR"]!=null)
				{
					model.DOOR=row["DOOR"].ToString();
				}
				if(row["RESREGION"]!=null)
				{
					model.RESREGION=row["RESREGION"].ToString();
				}
				if(row["BUILDING"]!=null)
				{
					model.BUILDING=row["BUILDING"].ToString();
				}
				if(row["BUILDING_NUM"]!=null)
				{
					model.BUILDING_NUM=row["BUILDING_NUM"].ToString();
				}
				if(row["UNIT"]!=null)
				{
					model.UNIT=row["UNIT"].ToString();
				}
				if(row["FLOOR"]!=null)
				{
					model.FLOOR=row["FLOOR"].ToString();
				}
				if(row["ROOM"]!=null)
				{
					model.ROOM=row["ROOM"].ToString();
				}
				if(row["GRID_CODE"]!=null)
				{
					model.GRID_CODE=row["GRID_CODE"].ToString();
				}
				if(row["BUILDING_CODE"]!=null)
				{
					model.BUILDING_CODE=row["BUILDING_CODE"].ToString();
				}
				if(row["HOUSE_CODE"]!=null)
				{
					model.HOUSE_CODE=row["HOUSE_CODE"].ToString();
				}
				if(row["CODE"]!=null)
				{
					model.CODE=row["CODE"].ToString();
				}
				if(row["CREATETIME"]!=null)
				{
					model.CREATETIME=row["CREATETIME"].ToString();
				}
				if(row["UPDATETIME"]!=null)
				{
					model.UPDATETIME=row["UPDATETIME"].ToString();
				}
				if(row["ISVALID"]!=null && row["ISVALID"].ToString()!="")
				{
					model.ISVALID=int.Parse(row["ISVALID"].ToString());
				}
				if(row["FROM_STATUS"]!=null)
				{
					model.FROM_STATUS=row["FROM_STATUS"].ToString();
				}
				if(row["TO_STATUS"]!=null)
				{
					model.TO_STATUS=row["TO_STATUS"].ToString();
				}
				if(row["BUILDING_PATH"]!=null)
				{
					model.BUILDING_PATH=row["BUILDING_PATH"].ToString();
				}
				if(row["DATASOURCE"]!=null)
				{
					model.DATASOURCE=row["DATASOURCE"].ToString();
				}
				if(row["REVERSE1"]!=null)
				{
					model.REVERSE1=row["REVERSE1"].ToString();
				}
				if(row["REVERSE2"]!=null)
				{
					model.REVERSE2=row["REVERSE2"].ToString();
				}
				if(row["LON"]!=null)
				{
					model.LON=row["LON"].ToString();
				}
				if(row["LAT"]!=null)
				{
					model.LAT=row["LAT"].ToString();
				}
				if(row["Z"]!=null)
				{
					model.Z=row["Z"].ToString();
				}
				if(row["ROOM_FLOOR"]!=null)
				{
					model.ROOM_FLOOR=row["ROOM_FLOOR"].ToString();
				}
				if(row["ADDRTYPE"]!=null)
				{
					model.ADDRTYPE=row["ADDRTYPE"].ToString();
				}
				if(row["GUID"]!=null)
				{
					model.GUID=row["GUID"].ToString();
				}
				if(row["INSERTTIME"]!=null)
				{
					model.INSERTTIME=row["INSERTTIME"].ToString();
				}
				if(row["SYSTEMID"]!=null)
				{
					model.SYSTEMID=row["SYSTEMID"].ToString();
				}
				if(row["ISDELETE"]!=null && row["ISDELETE"].ToString()!="")
				{
					model.ISDELETE=int.Parse(row["ISDELETE"].ToString());
				}
				if(row["BELONG_BUILDING"]!=null)
				{
					model.BELONG_BUILDING=row["BELONG_BUILDING"].ToString();
				}
				if(row["ADDRESS_TYPE"]!=null)
				{
					model.ADDRESS_TYPE=row["ADDRESS_TYPE"].ToString();
				}
				if(row["REMARK"]!=null)
				{
					model.REMARK=row["REMARK"].ToString();
				}
				if(row["AddTime"]!=null)
				{
					model.AddTime=row["AddTime"].ToString();
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
			strSql.Append("select UnifiedAddressLibraryUUID,ID,oid,SOURCEADDRESS,CITY,COUNTY,TOWN,COMMUNITY,SQUAD,VILLAGE,SZONE,STREET,DOOR,RESREGION,BUILDING,BUILDING_NUM,UNIT,FLOOR,ROOM,GRID_CODE,BUILDING_CODE,HOUSE_CODE,CODE,CREATETIME,UPDATETIME,ISVALID,FROM_STATUS,TO_STATUS,BUILDING_PATH,DATASOURCE,REVERSE1,REVERSE2,LON,LAT,Z,ROOM_FLOOR,ADDRTYPE,GUID,INSERTTIME,SYSTEMID,ISDELETE,BELONG_BUILDING,ADDRESS_TYPE,REMARK,AddTime ");
			strSql.Append(" FROM UnifiedAddressLibrary ");
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
			strSql.Append(" UnifiedAddressLibraryUUID,ID,oid,SOURCEADDRESS,CITY,COUNTY,TOWN,COMMUNITY,SQUAD,VILLAGE,SZONE,STREET,DOOR,RESREGION,BUILDING,BUILDING_NUM,UNIT,FLOOR,ROOM,GRID_CODE,BUILDING_CODE,HOUSE_CODE,CODE,CREATETIME,UPDATETIME,ISVALID,FROM_STATUS,TO_STATUS,BUILDING_PATH,DATASOURCE,REVERSE1,REVERSE2,LON,LAT,Z,ROOM_FLOOR,ADDRTYPE,GUID,INSERTTIME,SYSTEMID,ISDELETE,BELONG_BUILDING,ADDRESS_TYPE,REMARK,AddTime ");
			strSql.Append(" FROM UnifiedAddressLibrary ");
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
			strSql.Append("select count(1) FROM UnifiedAddressLibrary ");
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
			strSql.Append(")AS Row, T.*  from UnifiedAddressLibrary T ");
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
			parameters[0].Value = "UnifiedAddressLibrary";
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

