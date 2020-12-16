using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:xf_Building
	/// </summary>
	public partial class xf_Building
	{
		public xf_Building()
		{}
		#region  基本方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid BuildingUuid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from xf_Building");
			strSql.Append(" where BuildingUuid=@BuildingUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@BuildingUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = BuildingUuid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ClassLibrary1.Model.xf_Building model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into xf_Building(");
			strSql.Append("CreateTime,BuildingUuid,buildingName,buildingType,buildingUseNature,fireDanger,fireResistantLevel,structureType,buildingHeight,buildingAddr,regionCode,buildingState,buildingTime,buildingFinishTime,propertyRight,buildingArea,occupyArea,standardFloorArea,overFloorNum,overFloorArea,underFloorNum,underFloorArea,tunnelHeight,tunnelLength,controlRoomCondition,controlRoomPosition,workerDailyNum,buildingGalleryful,fireElevatorNum,fireElevatorPosition,fireElevatorCarrery,shelterFloorNum,shelterFloorArea,shelterFloorPosition,exitNum,exitPosition,exitForm,storageName,storageNum,storageNature,storageShape,storageSize,mainMaterial,mainProduct,enterOrgNum,orgAbutSituation,belongOrgId,managerOrgId,unitNum,floorNum,mapType,gpsX3d,gpsY3d,gpsZ3d,isMezzanine,memo,platformCode,recordCode)");
			strSql.Append(" values (");
			strSql.Append("@CreateTime,@BuildingUuid,@buildingName,@buildingType,@buildingUseNature,@fireDanger,@fireResistantLevel,@structureType,@buildingHeight,@buildingAddr,@regionCode,@buildingState,@buildingTime,@buildingFinishTime,@propertyRight,@buildingArea,@occupyArea,@standardFloorArea,@overFloorNum,@overFloorArea,@underFloorNum,@underFloorArea,@tunnelHeight,@tunnelLength,@controlRoomCondition,@controlRoomPosition,@workerDailyNum,@buildingGalleryful,@fireElevatorNum,@fireElevatorPosition,@fireElevatorCarrery,@shelterFloorNum,@shelterFloorArea,@shelterFloorPosition,@exitNum,@exitPosition,@exitForm,@storageName,@storageNum,@storageNature,@storageShape,@storageSize,@mainMaterial,@mainProduct,@enterOrgNum,@orgAbutSituation,@belongOrgId,@managerOrgId,@unitNum,@floorNum,@mapType,@gpsX3d,@gpsY3d,@gpsZ3d,@isMezzanine,@memo,@platformCode,@recordCode)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@BuildingUuid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@buildingName", SqlDbType.VarChar,255),
					new SqlParameter("@buildingType", SqlDbType.VarChar,255),
					new SqlParameter("@buildingUseNature", SqlDbType.VarChar,255),
					new SqlParameter("@fireDanger", SqlDbType.VarChar,255),
					new SqlParameter("@fireResistantLevel", SqlDbType.VarChar,255),
					new SqlParameter("@structureType", SqlDbType.VarChar,255),
					new SqlParameter("@buildingHeight", SqlDbType.Float,8),
					new SqlParameter("@buildingAddr", SqlDbType.VarChar,255),
					new SqlParameter("@regionCode", SqlDbType.VarChar,255),
					new SqlParameter("@buildingState", SqlDbType.VarChar,255),
					new SqlParameter("@buildingTime", SqlDbType.VarChar,255),
					new SqlParameter("@buildingFinishTime", SqlDbType.VarChar,255),
					new SqlParameter("@propertyRight", SqlDbType.VarChar,255),
					new SqlParameter("@buildingArea", SqlDbType.Float,8),
					new SqlParameter("@occupyArea", SqlDbType.Float,8),
					new SqlParameter("@standardFloorArea", SqlDbType.Float,8),
					new SqlParameter("@overFloorNum", SqlDbType.Int,4),
					new SqlParameter("@overFloorArea", SqlDbType.Float,8),
					new SqlParameter("@underFloorNum", SqlDbType.Int,4),
					new SqlParameter("@underFloorArea", SqlDbType.Float,8),
					new SqlParameter("@tunnelHeight", SqlDbType.Float,8),
					new SqlParameter("@tunnelLength", SqlDbType.Float,8),
					new SqlParameter("@controlRoomCondition", SqlDbType.Int,4),
					new SqlParameter("@controlRoomPosition", SqlDbType.VarChar,255),
					new SqlParameter("@workerDailyNum", SqlDbType.Int,4),
					new SqlParameter("@buildingGalleryful", SqlDbType.Int,4),
					new SqlParameter("@fireElevatorNum", SqlDbType.Int,4),
					new SqlParameter("@fireElevatorPosition", SqlDbType.VarChar,255),
					new SqlParameter("@fireElevatorCarrery", SqlDbType.Float,8),
					new SqlParameter("@shelterFloorNum", SqlDbType.Float,8),
					new SqlParameter("@shelterFloorArea", SqlDbType.Float,8),
					new SqlParameter("@shelterFloorPosition", SqlDbType.VarChar,255),
					new SqlParameter("@exitNum", SqlDbType.Int,4),
					new SqlParameter("@exitPosition", SqlDbType.VarChar,255),
					new SqlParameter("@exitForm", SqlDbType.VarChar,255),
					new SqlParameter("@storageName", SqlDbType.VarChar,255),
					new SqlParameter("@storageNum", SqlDbType.Int,4),
					new SqlParameter("@storageNature", SqlDbType.VarChar,255),
					new SqlParameter("@storageShape", SqlDbType.VarChar,255),
					new SqlParameter("@storageSize", SqlDbType.Float,8),
					new SqlParameter("@mainMaterial", SqlDbType.VarChar,255),
					new SqlParameter("@mainProduct", SqlDbType.VarChar,255),
					new SqlParameter("@enterOrgNum", SqlDbType.Int,4),
					new SqlParameter("@orgAbutSituation", SqlDbType.VarChar,255),
					new SqlParameter("@belongOrgId", SqlDbType.VarChar,255),
					new SqlParameter("@managerOrgId", SqlDbType.VarChar,255),
					new SqlParameter("@unitNum", SqlDbType.Int,4),
					new SqlParameter("@floorNum", SqlDbType.Int,4),
					new SqlParameter("@mapType", SqlDbType.Int,4),
					new SqlParameter("@gpsX3d", SqlDbType.Float,8),
					new SqlParameter("@gpsY3d", SqlDbType.Float,8),
					new SqlParameter("@gpsZ3d", SqlDbType.Float,8),
					new SqlParameter("@isMezzanine", SqlDbType.Int,4),
					new SqlParameter("@memo", SqlDbType.VarChar,255),
					new SqlParameter("@platformCode", SqlDbType.VarChar,255),
					new SqlParameter("@recordCode", SqlDbType.VarChar,255)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = Guid.NewGuid();
			parameters[2].Value = model.buildingName;
			parameters[3].Value = model.buildingType;
			parameters[4].Value = model.buildingUseNature;
			parameters[5].Value = model.fireDanger;
			parameters[6].Value = model.fireResistantLevel;
			parameters[7].Value = model.structureType;
			parameters[8].Value = model.buildingHeight;
			parameters[9].Value = model.buildingAddr;
			parameters[10].Value = model.regionCode;
			parameters[11].Value = model.buildingState;
			parameters[12].Value = model.buildingTime;
			parameters[13].Value = model.buildingFinishTime;
			parameters[14].Value = model.propertyRight;
			parameters[15].Value = model.buildingArea;
			parameters[16].Value = model.occupyArea;
			parameters[17].Value = model.standardFloorArea;
			parameters[18].Value = model.overFloorNum;
			parameters[19].Value = model.overFloorArea;
			parameters[20].Value = model.underFloorNum;
			parameters[21].Value = model.underFloorArea;
			parameters[22].Value = model.tunnelHeight;
			parameters[23].Value = model.tunnelLength;
			parameters[24].Value = model.controlRoomCondition;
			parameters[25].Value = model.controlRoomPosition;
			parameters[26].Value = model.workerDailyNum;
			parameters[27].Value = model.buildingGalleryful;
			parameters[28].Value = model.fireElevatorNum;
			parameters[29].Value = model.fireElevatorPosition;
			parameters[30].Value = model.fireElevatorCarrery;
			parameters[31].Value = model.shelterFloorNum;
			parameters[32].Value = model.shelterFloorArea;
			parameters[33].Value = model.shelterFloorPosition;
			parameters[34].Value = model.exitNum;
			parameters[35].Value = model.exitPosition;
			parameters[36].Value = model.exitForm;
			parameters[37].Value = model.storageName;
			parameters[38].Value = model.storageNum;
			parameters[39].Value = model.storageNature;
			parameters[40].Value = model.storageShape;
			parameters[41].Value = model.storageSize;
			parameters[42].Value = model.mainMaterial;
			parameters[43].Value = model.mainProduct;
			parameters[44].Value = model.enterOrgNum;
			parameters[45].Value = model.orgAbutSituation;
			parameters[46].Value = model.belongOrgId;
			parameters[47].Value = model.managerOrgId;
			parameters[48].Value = model.unitNum;
			parameters[49].Value = model.floorNum;
			parameters[50].Value = model.mapType;
			parameters[51].Value = model.gpsX3d;
			parameters[52].Value = model.gpsY3d;
			parameters[53].Value = model.gpsZ3d;
			parameters[54].Value = model.isMezzanine;
			parameters[55].Value = model.memo;
			parameters[56].Value = model.platformCode;
			parameters[57].Value = model.recordCode;

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
		public bool Update(ClassLibrary1.Model.xf_Building model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update xf_Building set ");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("buildingName=@buildingName,");
			strSql.Append("buildingType=@buildingType,");
			strSql.Append("buildingUseNature=@buildingUseNature,");
			strSql.Append("fireDanger=@fireDanger,");
			strSql.Append("fireResistantLevel=@fireResistantLevel,");
			strSql.Append("structureType=@structureType,");
			strSql.Append("buildingHeight=@buildingHeight,");
			strSql.Append("buildingAddr=@buildingAddr,");
			strSql.Append("regionCode=@regionCode,");
			strSql.Append("buildingState=@buildingState,");
			strSql.Append("buildingTime=@buildingTime,");
			strSql.Append("buildingFinishTime=@buildingFinishTime,");
			strSql.Append("propertyRight=@propertyRight,");
			strSql.Append("buildingArea=@buildingArea,");
			strSql.Append("occupyArea=@occupyArea,");
			strSql.Append("standardFloorArea=@standardFloorArea,");
			strSql.Append("overFloorNum=@overFloorNum,");
			strSql.Append("overFloorArea=@overFloorArea,");
			strSql.Append("underFloorNum=@underFloorNum,");
			strSql.Append("underFloorArea=@underFloorArea,");
			strSql.Append("tunnelHeight=@tunnelHeight,");
			strSql.Append("tunnelLength=@tunnelLength,");
			strSql.Append("controlRoomCondition=@controlRoomCondition,");
			strSql.Append("controlRoomPosition=@controlRoomPosition,");
			strSql.Append("workerDailyNum=@workerDailyNum,");
			strSql.Append("buildingGalleryful=@buildingGalleryful,");
			strSql.Append("fireElevatorNum=@fireElevatorNum,");
			strSql.Append("fireElevatorPosition=@fireElevatorPosition,");
			strSql.Append("fireElevatorCarrery=@fireElevatorCarrery,");
			strSql.Append("shelterFloorNum=@shelterFloorNum,");
			strSql.Append("shelterFloorArea=@shelterFloorArea,");
			strSql.Append("shelterFloorPosition=@shelterFloorPosition,");
			strSql.Append("exitNum=@exitNum,");
			strSql.Append("exitPosition=@exitPosition,");
			strSql.Append("exitForm=@exitForm,");
			strSql.Append("storageName=@storageName,");
			strSql.Append("storageNum=@storageNum,");
			strSql.Append("storageNature=@storageNature,");
			strSql.Append("storageShape=@storageShape,");
			strSql.Append("storageSize=@storageSize,");
			strSql.Append("mainMaterial=@mainMaterial,");
			strSql.Append("mainProduct=@mainProduct,");
			strSql.Append("enterOrgNum=@enterOrgNum,");
			strSql.Append("orgAbutSituation=@orgAbutSituation,");
			strSql.Append("belongOrgId=@belongOrgId,");
			strSql.Append("managerOrgId=@managerOrgId,");
			strSql.Append("unitNum=@unitNum,");
			strSql.Append("floorNum=@floorNum,");
			strSql.Append("mapType=@mapType,");
			strSql.Append("gpsX3d=@gpsX3d,");
			strSql.Append("gpsY3d=@gpsY3d,");
			strSql.Append("gpsZ3d=@gpsZ3d,");
			strSql.Append("isMezzanine=@isMezzanine,");
			strSql.Append("memo=@memo,");
			strSql.Append("platformCode=@platformCode,");
			strSql.Append("recordCode=@recordCode");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@CreateTime", SqlDbType.DateTime2,8),
					new SqlParameter("@buildingName", SqlDbType.VarChar,255),
					new SqlParameter("@buildingType", SqlDbType.VarChar,255),
					new SqlParameter("@buildingUseNature", SqlDbType.VarChar,255),
					new SqlParameter("@fireDanger", SqlDbType.VarChar,255),
					new SqlParameter("@fireResistantLevel", SqlDbType.VarChar,255),
					new SqlParameter("@structureType", SqlDbType.VarChar,255),
					new SqlParameter("@buildingHeight", SqlDbType.Float,8),
					new SqlParameter("@buildingAddr", SqlDbType.VarChar,255),
					new SqlParameter("@regionCode", SqlDbType.VarChar,255),
					new SqlParameter("@buildingState", SqlDbType.VarChar,255),
					new SqlParameter("@buildingTime", SqlDbType.VarChar,255),
					new SqlParameter("@buildingFinishTime", SqlDbType.VarChar,255),
					new SqlParameter("@propertyRight", SqlDbType.VarChar,255),
					new SqlParameter("@buildingArea", SqlDbType.Float,8),
					new SqlParameter("@occupyArea", SqlDbType.Float,8),
					new SqlParameter("@standardFloorArea", SqlDbType.Float,8),
					new SqlParameter("@overFloorNum", SqlDbType.Int,4),
					new SqlParameter("@overFloorArea", SqlDbType.Float,8),
					new SqlParameter("@underFloorNum", SqlDbType.Int,4),
					new SqlParameter("@underFloorArea", SqlDbType.Float,8),
					new SqlParameter("@tunnelHeight", SqlDbType.Float,8),
					new SqlParameter("@tunnelLength", SqlDbType.Float,8),
					new SqlParameter("@controlRoomCondition", SqlDbType.Int,4),
					new SqlParameter("@controlRoomPosition", SqlDbType.VarChar,255),
					new SqlParameter("@workerDailyNum", SqlDbType.Int,4),
					new SqlParameter("@buildingGalleryful", SqlDbType.Int,4),
					new SqlParameter("@fireElevatorNum", SqlDbType.Int,4),
					new SqlParameter("@fireElevatorPosition", SqlDbType.VarChar,255),
					new SqlParameter("@fireElevatorCarrery", SqlDbType.Float,8),
					new SqlParameter("@shelterFloorNum", SqlDbType.Float,8),
					new SqlParameter("@shelterFloorArea", SqlDbType.Float,8),
					new SqlParameter("@shelterFloorPosition", SqlDbType.VarChar,255),
					new SqlParameter("@exitNum", SqlDbType.Int,4),
					new SqlParameter("@exitPosition", SqlDbType.VarChar,255),
					new SqlParameter("@exitForm", SqlDbType.VarChar,255),
					new SqlParameter("@storageName", SqlDbType.VarChar,255),
					new SqlParameter("@storageNum", SqlDbType.Int,4),
					new SqlParameter("@storageNature", SqlDbType.VarChar,255),
					new SqlParameter("@storageShape", SqlDbType.VarChar,255),
					new SqlParameter("@storageSize", SqlDbType.Float,8),
					new SqlParameter("@mainMaterial", SqlDbType.VarChar,255),
					new SqlParameter("@mainProduct", SqlDbType.VarChar,255),
					new SqlParameter("@enterOrgNum", SqlDbType.Int,4),
					new SqlParameter("@orgAbutSituation", SqlDbType.VarChar,255),
					new SqlParameter("@belongOrgId", SqlDbType.VarChar,255),
					new SqlParameter("@managerOrgId", SqlDbType.VarChar,255),
					new SqlParameter("@unitNum", SqlDbType.Int,4),
					new SqlParameter("@floorNum", SqlDbType.Int,4),
					new SqlParameter("@mapType", SqlDbType.Int,4),
					new SqlParameter("@gpsX3d", SqlDbType.Float,8),
					new SqlParameter("@gpsY3d", SqlDbType.Float,8),
					new SqlParameter("@gpsZ3d", SqlDbType.Float,8),
					new SqlParameter("@isMezzanine", SqlDbType.Int,4),
					new SqlParameter("@memo", SqlDbType.VarChar,255),
					new SqlParameter("@platformCode", SqlDbType.VarChar,255),
					new SqlParameter("@recordCode", SqlDbType.VarChar,255),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@BuildingUuid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.buildingName;
			parameters[2].Value = model.buildingType;
			parameters[3].Value = model.buildingUseNature;
			parameters[4].Value = model.fireDanger;
			parameters[5].Value = model.fireResistantLevel;
			parameters[6].Value = model.structureType;
			parameters[7].Value = model.buildingHeight;
			parameters[8].Value = model.buildingAddr;
			parameters[9].Value = model.regionCode;
			parameters[10].Value = model.buildingState;
			parameters[11].Value = model.buildingTime;
			parameters[12].Value = model.buildingFinishTime;
			parameters[13].Value = model.propertyRight;
			parameters[14].Value = model.buildingArea;
			parameters[15].Value = model.occupyArea;
			parameters[16].Value = model.standardFloorArea;
			parameters[17].Value = model.overFloorNum;
			parameters[18].Value = model.overFloorArea;
			parameters[19].Value = model.underFloorNum;
			parameters[20].Value = model.underFloorArea;
			parameters[21].Value = model.tunnelHeight;
			parameters[22].Value = model.tunnelLength;
			parameters[23].Value = model.controlRoomCondition;
			parameters[24].Value = model.controlRoomPosition;
			parameters[25].Value = model.workerDailyNum;
			parameters[26].Value = model.buildingGalleryful;
			parameters[27].Value = model.fireElevatorNum;
			parameters[28].Value = model.fireElevatorPosition;
			parameters[29].Value = model.fireElevatorCarrery;
			parameters[30].Value = model.shelterFloorNum;
			parameters[31].Value = model.shelterFloorArea;
			parameters[32].Value = model.shelterFloorPosition;
			parameters[33].Value = model.exitNum;
			parameters[34].Value = model.exitPosition;
			parameters[35].Value = model.exitForm;
			parameters[36].Value = model.storageName;
			parameters[37].Value = model.storageNum;
			parameters[38].Value = model.storageNature;
			parameters[39].Value = model.storageShape;
			parameters[40].Value = model.storageSize;
			parameters[41].Value = model.mainMaterial;
			parameters[42].Value = model.mainProduct;
			parameters[43].Value = model.enterOrgNum;
			parameters[44].Value = model.orgAbutSituation;
			parameters[45].Value = model.belongOrgId;
			parameters[46].Value = model.managerOrgId;
			parameters[47].Value = model.unitNum;
			parameters[48].Value = model.floorNum;
			parameters[49].Value = model.mapType;
			parameters[50].Value = model.gpsX3d;
			parameters[51].Value = model.gpsY3d;
			parameters[52].Value = model.gpsZ3d;
			parameters[53].Value = model.isMezzanine;
			parameters[54].Value = model.memo;
			parameters[55].Value = model.platformCode;
			parameters[56].Value = model.recordCode;
			parameters[57].Value = model.ID;
			parameters[58].Value = model.BuildingUuid;

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
			strSql.Append("delete from xf_Building ");
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
		public bool Delete(Guid BuildingUuid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from xf_Building ");
			strSql.Append(" where BuildingUuid=@BuildingUuid ");
			SqlParameter[] parameters = {
					new SqlParameter("@BuildingUuid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = BuildingUuid;

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
			strSql.Append("delete from xf_Building ");
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
		public ClassLibrary1.Model.xf_Building GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CreateTime,BuildingUuid,buildingName,buildingType,buildingUseNature,fireDanger,fireResistantLevel,structureType,buildingHeight,buildingAddr,regionCode,buildingState,buildingTime,buildingFinishTime,propertyRight,buildingArea,occupyArea,standardFloorArea,overFloorNum,overFloorArea,underFloorNum,underFloorArea,tunnelHeight,tunnelLength,controlRoomCondition,controlRoomPosition,workerDailyNum,buildingGalleryful,fireElevatorNum,fireElevatorPosition,fireElevatorCarrery,shelterFloorNum,shelterFloorArea,shelterFloorPosition,exitNum,exitPosition,exitForm,storageName,storageNum,storageNature,storageShape,storageSize,mainMaterial,mainProduct,enterOrgNum,orgAbutSituation,belongOrgId,managerOrgId,unitNum,floorNum,mapType,gpsX3d,gpsY3d,gpsZ3d,isMezzanine,memo,platformCode,recordCode from xf_Building ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ClassLibrary1.Model.xf_Building model=new ClassLibrary1.Model.xf_Building();
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
		public ClassLibrary1.Model.xf_Building DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.xf_Building model=new ClassLibrary1.Model.xf_Building();
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
				if(row["BuildingUuid"]!=null && row["BuildingUuid"].ToString()!="")
				{
					model.BuildingUuid= new Guid(row["BuildingUuid"].ToString());
				}
				if(row["buildingName"]!=null)
				{
					model.buildingName=row["buildingName"].ToString();
				}
				if(row["buildingType"]!=null)
				{
					model.buildingType=row["buildingType"].ToString();
				}
				if(row["buildingUseNature"]!=null)
				{
					model.buildingUseNature=row["buildingUseNature"].ToString();
				}
				if(row["fireDanger"]!=null)
				{
					model.fireDanger=row["fireDanger"].ToString();
				}
				if(row["fireResistantLevel"]!=null)
				{
					model.fireResistantLevel=row["fireResistantLevel"].ToString();
				}
				if(row["structureType"]!=null)
				{
					model.structureType=row["structureType"].ToString();
				}
				if(row["buildingHeight"]!=null && row["buildingHeight"].ToString()!="")
				{
					model.buildingHeight=decimal.Parse(row["buildingHeight"].ToString());
				}
				if(row["buildingAddr"]!=null)
				{
					model.buildingAddr=row["buildingAddr"].ToString();
				}
				if(row["regionCode"]!=null)
				{
					model.regionCode=row["regionCode"].ToString();
				}
				if(row["buildingState"]!=null)
				{
					model.buildingState=row["buildingState"].ToString();
				}
				if(row["buildingTime"]!=null)
				{
					model.buildingTime=row["buildingTime"].ToString();
				}
				if(row["buildingFinishTime"]!=null)
				{
					model.buildingFinishTime=row["buildingFinishTime"].ToString();
				}
				if(row["propertyRight"]!=null)
				{
					model.propertyRight=row["propertyRight"].ToString();
				}
				if(row["buildingArea"]!=null && row["buildingArea"].ToString()!="")
				{
					model.buildingArea=decimal.Parse(row["buildingArea"].ToString());
				}
				if(row["occupyArea"]!=null && row["occupyArea"].ToString()!="")
				{
					model.occupyArea=decimal.Parse(row["occupyArea"].ToString());
				}
				if(row["standardFloorArea"]!=null && row["standardFloorArea"].ToString()!="")
				{
					model.standardFloorArea=decimal.Parse(row["standardFloorArea"].ToString());
				}
				if(row["overFloorNum"]!=null && row["overFloorNum"].ToString()!="")
				{
					model.overFloorNum=int.Parse(row["overFloorNum"].ToString());
				}
				if(row["overFloorArea"]!=null && row["overFloorArea"].ToString()!="")
				{
					model.overFloorArea=decimal.Parse(row["overFloorArea"].ToString());
				}
				if(row["underFloorNum"]!=null && row["underFloorNum"].ToString()!="")
				{
					model.underFloorNum=int.Parse(row["underFloorNum"].ToString());
				}
				if(row["underFloorArea"]!=null && row["underFloorArea"].ToString()!="")
				{
					model.underFloorArea=decimal.Parse(row["underFloorArea"].ToString());
				}
				if(row["tunnelHeight"]!=null && row["tunnelHeight"].ToString()!="")
				{
					model.tunnelHeight=decimal.Parse(row["tunnelHeight"].ToString());
				}
				if(row["tunnelLength"]!=null && row["tunnelLength"].ToString()!="")
				{
					model.tunnelLength=decimal.Parse(row["tunnelLength"].ToString());
				}
				if(row["controlRoomCondition"]!=null && row["controlRoomCondition"].ToString()!="")
				{
					model.controlRoomCondition=int.Parse(row["controlRoomCondition"].ToString());
				}
				if(row["controlRoomPosition"]!=null)
				{
					model.controlRoomPosition=row["controlRoomPosition"].ToString();
				}
				if(row["workerDailyNum"]!=null && row["workerDailyNum"].ToString()!="")
				{
					model.workerDailyNum=int.Parse(row["workerDailyNum"].ToString());
				}
				if(row["buildingGalleryful"]!=null && row["buildingGalleryful"].ToString()!="")
				{
					model.buildingGalleryful=int.Parse(row["buildingGalleryful"].ToString());
				}
				if(row["fireElevatorNum"]!=null && row["fireElevatorNum"].ToString()!="")
				{
					model.fireElevatorNum=int.Parse(row["fireElevatorNum"].ToString());
				}
				if(row["fireElevatorPosition"]!=null)
				{
					model.fireElevatorPosition=row["fireElevatorPosition"].ToString();
				}
				if(row["fireElevatorCarrery"]!=null && row["fireElevatorCarrery"].ToString()!="")
				{
					model.fireElevatorCarrery=decimal.Parse(row["fireElevatorCarrery"].ToString());
				}
				if(row["shelterFloorNum"]!=null && row["shelterFloorNum"].ToString()!="")
				{
					model.shelterFloorNum=decimal.Parse(row["shelterFloorNum"].ToString());
				}
				if(row["shelterFloorArea"]!=null && row["shelterFloorArea"].ToString()!="")
				{
					model.shelterFloorArea=decimal.Parse(row["shelterFloorArea"].ToString());
				}
				if(row["shelterFloorPosition"]!=null)
				{
					model.shelterFloorPosition=row["shelterFloorPosition"].ToString();
				}
				if(row["exitNum"]!=null && row["exitNum"].ToString()!="")
				{
					model.exitNum=int.Parse(row["exitNum"].ToString());
				}
				if(row["exitPosition"]!=null)
				{
					model.exitPosition=row["exitPosition"].ToString();
				}
				if(row["exitForm"]!=null)
				{
					model.exitForm=row["exitForm"].ToString();
				}
				if(row["storageName"]!=null)
				{
					model.storageName=row["storageName"].ToString();
				}
				if(row["storageNum"]!=null && row["storageNum"].ToString()!="")
				{
					model.storageNum=int.Parse(row["storageNum"].ToString());
				}
				if(row["storageNature"]!=null)
				{
					model.storageNature=row["storageNature"].ToString();
				}
				if(row["storageShape"]!=null)
				{
					model.storageShape=row["storageShape"].ToString();
				}
				if(row["storageSize"]!=null && row["storageSize"].ToString()!="")
				{
					model.storageSize=decimal.Parse(row["storageSize"].ToString());
				}
				if(row["mainMaterial"]!=null)
				{
					model.mainMaterial=row["mainMaterial"].ToString();
				}
				if(row["mainProduct"]!=null)
				{
					model.mainProduct=row["mainProduct"].ToString();
				}
				if(row["enterOrgNum"]!=null && row["enterOrgNum"].ToString()!="")
				{
					model.enterOrgNum=int.Parse(row["enterOrgNum"].ToString());
				}
				if(row["orgAbutSituation"]!=null)
				{
					model.orgAbutSituation=row["orgAbutSituation"].ToString();
				}
				if(row["belongOrgId"]!=null)
				{
					model.belongOrgId=row["belongOrgId"].ToString();
				}
				if(row["managerOrgId"]!=null)
				{
					model.managerOrgId=row["managerOrgId"].ToString();
				}
				if(row["unitNum"]!=null && row["unitNum"].ToString()!="")
				{
					model.unitNum=int.Parse(row["unitNum"].ToString());
				}
				if(row["floorNum"]!=null && row["floorNum"].ToString()!="")
				{
					model.floorNum=int.Parse(row["floorNum"].ToString());
				}
				if(row["mapType"]!=null && row["mapType"].ToString()!="")
				{
					model.mapType=int.Parse(row["mapType"].ToString());
				}
				if(row["gpsX3d"]!=null && row["gpsX3d"].ToString()!="")
				{
					model.gpsX3d=decimal.Parse(row["gpsX3d"].ToString());
				}
				if(row["gpsY3d"]!=null && row["gpsY3d"].ToString()!="")
				{
					model.gpsY3d=decimal.Parse(row["gpsY3d"].ToString());
				}
				if(row["gpsZ3d"]!=null && row["gpsZ3d"].ToString()!="")
				{
					model.gpsZ3d=decimal.Parse(row["gpsZ3d"].ToString());
				}
				if(row["isMezzanine"]!=null && row["isMezzanine"].ToString()!="")
				{
					model.isMezzanine=int.Parse(row["isMezzanine"].ToString());
				}
				if(row["memo"]!=null)
				{
					model.memo=row["memo"].ToString();
				}
				if(row["platformCode"]!=null)
				{
					model.platformCode=row["platformCode"].ToString();
				}
				if(row["recordCode"]!=null)
				{
					model.recordCode=row["recordCode"].ToString();
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
			strSql.Append("select ID,CreateTime,BuildingUuid,buildingName,buildingType,buildingUseNature,fireDanger,fireResistantLevel,structureType,buildingHeight,buildingAddr,regionCode,buildingState,buildingTime,buildingFinishTime,propertyRight,buildingArea,occupyArea,standardFloorArea,overFloorNum,overFloorArea,underFloorNum,underFloorArea,tunnelHeight,tunnelLength,controlRoomCondition,controlRoomPosition,workerDailyNum,buildingGalleryful,fireElevatorNum,fireElevatorPosition,fireElevatorCarrery,shelterFloorNum,shelterFloorArea,shelterFloorPosition,exitNum,exitPosition,exitForm,storageName,storageNum,storageNature,storageShape,storageSize,mainMaterial,mainProduct,enterOrgNum,orgAbutSituation,belongOrgId,managerOrgId,unitNum,floorNum,mapType,gpsX3d,gpsY3d,gpsZ3d,isMezzanine,memo,platformCode,recordCode ");
			strSql.Append(" FROM xf_Building ");
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
			strSql.Append(" ID,CreateTime,BuildingUuid,buildingName,buildingType,buildingUseNature,fireDanger,fireResistantLevel,structureType,buildingHeight,buildingAddr,regionCode,buildingState,buildingTime,buildingFinishTime,propertyRight,buildingArea,occupyArea,standardFloorArea,overFloorNum,overFloorArea,underFloorNum,underFloorArea,tunnelHeight,tunnelLength,controlRoomCondition,controlRoomPosition,workerDailyNum,buildingGalleryful,fireElevatorNum,fireElevatorPosition,fireElevatorCarrery,shelterFloorNum,shelterFloorArea,shelterFloorPosition,exitNum,exitPosition,exitForm,storageName,storageNum,storageNature,storageShape,storageSize,mainMaterial,mainProduct,enterOrgNum,orgAbutSituation,belongOrgId,managerOrgId,unitNum,floorNum,mapType,gpsX3d,gpsY3d,gpsZ3d,isMezzanine,memo,platformCode,recordCode ");
			strSql.Append(" FROM xf_Building ");
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
			strSql.Append("select count(1) FROM xf_Building ");
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
			strSql.Append(")AS Row, T.*  from xf_Building T ");
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
			parameters[0].Value = "xf_Building";
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

