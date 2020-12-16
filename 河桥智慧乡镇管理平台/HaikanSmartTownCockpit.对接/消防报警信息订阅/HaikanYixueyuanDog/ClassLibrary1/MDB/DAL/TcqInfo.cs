using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Haikan.DBHelper;
namespace ClassLibrary1.DAL
{
	/// <summary>
	/// 数据访问类:TcqInfo
	/// </summary>
	public partial class TcqInfo
	{
		public TcqInfo()
		{}
		#region  基本方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid Guid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TcqInfo");
			strSql.Append(" where Guid=@Guid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = Guid;

			return DbHelperSql.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ClassLibrary1.Model.TcqInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into TcqInfo(");
			strSql.Append("Guid,detectorCode,detectorName,mainframeId,buildingId,detectorAddr,orgId,detectorType,ifcsSystemType,partunitloopCode,channelNo,hardwareVersion,softwareVersion,personId,minalarmValue,maxalarmValue,deviceRange,basalArea,maxHeight,registerTime,registerStatus,detectorRunBigStatus,detectorRunSmallStatus,producerId,produceDate,runDate,serviceLimit,mapType,gpsX3d,gpsY3d,gpsZ3d,planId,planX,planY,platformCode,recordCode,communicationId,hostSim,protocolType,firePartition,systemAddress,analogValueTypeId)");
			strSql.Append(" values (");
			strSql.Append("@Guid,@detectorCode,@detectorName,@mainframeId,@buildingId,@detectorAddr,@orgId,@detectorType,@ifcsSystemType,@partunitloopCode,@channelNo,@hardwareVersion,@softwareVersion,@personId,@minalarmValue,@maxalarmValue,@deviceRange,@basalArea,@maxHeight,@registerTime,@registerStatus,@detectorRunBigStatus,@detectorRunSmallStatus,@producerId,@produceDate,@runDate,@serviceLimit,@mapType,@gpsX3d,@gpsY3d,@gpsZ3d,@planId,@planX,@planY,@platformCode,@recordCode,@communicationId,@hostSim,@protocolType,@firePartition,@systemAddress,@analogValueTypeId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@detectorCode", SqlDbType.VarChar,255),
					new SqlParameter("@detectorName", SqlDbType.VarChar,255),
					new SqlParameter("@mainframeId", SqlDbType.VarChar,255),
					new SqlParameter("@buildingId", SqlDbType.VarChar,255),
					new SqlParameter("@detectorAddr", SqlDbType.VarChar,255),
					new SqlParameter("@orgId", SqlDbType.VarChar,255),
					new SqlParameter("@detectorType", SqlDbType.VarChar,255),
					new SqlParameter("@ifcsSystemType", SqlDbType.VarChar,255),
					new SqlParameter("@partunitloopCode", SqlDbType.VarChar,255),
					new SqlParameter("@channelNo", SqlDbType.VarChar,255),
					new SqlParameter("@hardwareVersion", SqlDbType.VarChar,255),
					new SqlParameter("@softwareVersion", SqlDbType.VarChar,255),
					new SqlParameter("@personId", SqlDbType.VarChar,255),
					new SqlParameter("@minalarmValue", SqlDbType.VarChar,255),
					new SqlParameter("@maxalarmValue", SqlDbType.VarChar,255),
					new SqlParameter("@deviceRange", SqlDbType.VarChar,255),
					new SqlParameter("@basalArea", SqlDbType.VarChar,255),
					new SqlParameter("@maxHeight", SqlDbType.VarChar,255),
					new SqlParameter("@registerTime", SqlDbType.VarChar,255),
					new SqlParameter("@registerStatus", SqlDbType.VarChar,255),
					new SqlParameter("@detectorRunBigStatus", SqlDbType.VarChar,255),
					new SqlParameter("@detectorRunSmallStatus", SqlDbType.VarChar,255),
					new SqlParameter("@producerId", SqlDbType.VarChar,255),
					new SqlParameter("@produceDate", SqlDbType.VarChar,255),
					new SqlParameter("@runDate", SqlDbType.VarChar,255),
					new SqlParameter("@serviceLimit", SqlDbType.VarChar,255),
					new SqlParameter("@mapType", SqlDbType.VarChar,255),
					new SqlParameter("@gpsX3d", SqlDbType.VarChar,255),
					new SqlParameter("@gpsY3d", SqlDbType.VarChar,255),
					new SqlParameter("@gpsZ3d", SqlDbType.VarChar,255),
					new SqlParameter("@planId", SqlDbType.VarChar,255),
					new SqlParameter("@planX", SqlDbType.VarChar,255),
					new SqlParameter("@planY", SqlDbType.VarChar,255),
					new SqlParameter("@platformCode", SqlDbType.VarChar,255),
					new SqlParameter("@recordCode", SqlDbType.VarChar,255),
					new SqlParameter("@communicationId", SqlDbType.VarChar,255),
					new SqlParameter("@hostSim", SqlDbType.VarChar,255),
					new SqlParameter("@protocolType", SqlDbType.VarChar,255),
					new SqlParameter("@firePartition", SqlDbType.VarChar,255),
					new SqlParameter("@systemAddress", SqlDbType.VarChar,255),
					new SqlParameter("@analogValueTypeId", SqlDbType.VarChar,1)};
			parameters[0].Value = Guid.NewGuid();
			parameters[1].Value = model.detectorCode;
			parameters[2].Value = model.detectorName;
			parameters[3].Value = model.mainframeId;
			parameters[4].Value = model.buildingId;
			parameters[5].Value = model.detectorAddr;
			parameters[6].Value = model.orgId;
			parameters[7].Value = model.detectorType;
			parameters[8].Value = model.ifcsSystemType;
			parameters[9].Value = model.partunitloopCode;
			parameters[10].Value = model.channelNo;
			parameters[11].Value = model.hardwareVersion;
			parameters[12].Value = model.softwareVersion;
			parameters[13].Value = model.personId;
			parameters[14].Value = model.minalarmValue;
			parameters[15].Value = model.maxalarmValue;
			parameters[16].Value = model.deviceRange;
			parameters[17].Value = model.basalArea;
			parameters[18].Value = model.maxHeight;
			parameters[19].Value = model.registerTime;
			parameters[20].Value = model.registerStatus;
			parameters[21].Value = model.detectorRunBigStatus;
			parameters[22].Value = model.detectorRunSmallStatus;
			parameters[23].Value = model.producerId;
			parameters[24].Value = model.produceDate;
			parameters[25].Value = model.runDate;
			parameters[26].Value = model.serviceLimit;
			parameters[27].Value = model.mapType;
			parameters[28].Value = model.gpsX3d;
			parameters[29].Value = model.gpsY3d;
			parameters[30].Value = model.gpsZ3d;
			parameters[31].Value = model.planId;
			parameters[32].Value = model.planX;
			parameters[33].Value = model.planY;
			parameters[34].Value = model.platformCode;
			parameters[35].Value = model.recordCode;
			parameters[36].Value = model.communicationId;
			parameters[37].Value = model.hostSim;
			parameters[38].Value = model.protocolType;
			parameters[39].Value = model.firePartition;
			parameters[40].Value = model.systemAddress;
			parameters[41].Value = model.analogValueTypeId;
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
		public bool Update(ClassLibrary1.Model.TcqInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update TcqInfo set ");
			strSql.Append("detectorCode=@detectorCode,");
			strSql.Append("detectorName=@detectorName,");
			strSql.Append("mainframeId=@mainframeId,");
			strSql.Append("buildingId=@buildingId,");
			strSql.Append("detectorAddr=@detectorAddr,");
			strSql.Append("orgId=@orgId,");
			strSql.Append("detectorType=@detectorType,");
			strSql.Append("ifcsSystemType=@ifcsSystemType,");
			strSql.Append("partunitloopCode=@partunitloopCode,");
			strSql.Append("channelNo=@channelNo,");
			strSql.Append("hardwareVersion=@hardwareVersion,");
			strSql.Append("softwareVersion=@softwareVersion,");
			strSql.Append("personId=@personId,");
			strSql.Append("minalarmValue=@minalarmValue,");
			strSql.Append("maxalarmValue=@maxalarmValue,");
			strSql.Append("deviceRange=@deviceRange,");
			strSql.Append("basalArea=@basalArea,");
			strSql.Append("maxHeight=@maxHeight,");
			strSql.Append("registerTime=@registerTime,");
			strSql.Append("registerStatus=@registerStatus,");
			strSql.Append("detectorRunBigStatus=@detectorRunBigStatus,");
			strSql.Append("detectorRunSmallStatus=@detectorRunSmallStatus,");
			strSql.Append("producerId=@producerId,");
			strSql.Append("produceDate=@produceDate,");
			strSql.Append("runDate=@runDate,");
			strSql.Append("serviceLimit=@serviceLimit,");
			strSql.Append("mapType=@mapType,");
			strSql.Append("gpsX3d=@gpsX3d,");
			strSql.Append("gpsY3d=@gpsY3d,");
			strSql.Append("gpsZ3d=@gpsZ3d,");
			strSql.Append("planId=@planId,");
			strSql.Append("planX=@planX,");
			strSql.Append("planY=@planY,");
			strSql.Append("platformCode=@platformCode,");
			strSql.Append("recordCode=@recordCode,");
			strSql.Append("communicationId=@communicationId,");
			strSql.Append("hostSim=@hostSim,");
			strSql.Append("protocolType=@protocolType,");
			strSql.Append("firePartition=@firePartition,");
			strSql.Append("systemAddress=@systemAddress,");
			strSql.Append("analogValueTypeId=@analogValueTypeId");
			strSql.Append(" where Guid=@Guid ");
			SqlParameter[] parameters = {
					new SqlParameter("@detectorCode", SqlDbType.VarChar,255),
					new SqlParameter("@detectorName", SqlDbType.VarChar,255),
					new SqlParameter("@mainframeId", SqlDbType.VarChar,255),
					new SqlParameter("@buildingId", SqlDbType.VarChar,255),
					new SqlParameter("@detectorAddr", SqlDbType.VarChar,255),
					new SqlParameter("@orgId", SqlDbType.VarChar,255),
					new SqlParameter("@detectorType", SqlDbType.VarChar,255),
					new SqlParameter("@ifcsSystemType", SqlDbType.VarChar,255),
					new SqlParameter("@partunitloopCode", SqlDbType.VarChar,255),
					new SqlParameter("@channelNo", SqlDbType.VarChar,255),
					new SqlParameter("@hardwareVersion", SqlDbType.VarChar,255),
					new SqlParameter("@softwareVersion", SqlDbType.VarChar,255),
					new SqlParameter("@personId", SqlDbType.VarChar,255),
					new SqlParameter("@minalarmValue", SqlDbType.VarChar,255),
					new SqlParameter("@maxalarmValue", SqlDbType.VarChar,255),
					new SqlParameter("@deviceRange", SqlDbType.VarChar,255),
					new SqlParameter("@basalArea", SqlDbType.VarChar,255),
					new SqlParameter("@maxHeight", SqlDbType.VarChar,255),
					new SqlParameter("@registerTime", SqlDbType.VarChar,255),
					new SqlParameter("@registerStatus", SqlDbType.VarChar,255),
					new SqlParameter("@detectorRunBigStatus", SqlDbType.VarChar,255),
					new SqlParameter("@detectorRunSmallStatus", SqlDbType.VarChar,255),
					new SqlParameter("@producerId", SqlDbType.VarChar,255),
					new SqlParameter("@produceDate", SqlDbType.VarChar,255),
					new SqlParameter("@runDate", SqlDbType.VarChar,255),
					new SqlParameter("@serviceLimit", SqlDbType.VarChar,255),
					new SqlParameter("@mapType", SqlDbType.VarChar,255),
					new SqlParameter("@gpsX3d", SqlDbType.VarChar,255),
					new SqlParameter("@gpsY3d", SqlDbType.VarChar,255),
					new SqlParameter("@gpsZ3d", SqlDbType.VarChar,255),
					new SqlParameter("@planId", SqlDbType.VarChar,255),
					new SqlParameter("@planX", SqlDbType.VarChar,255),
					new SqlParameter("@planY", SqlDbType.VarChar,255),
					new SqlParameter("@platformCode", SqlDbType.VarChar,255),
					new SqlParameter("@recordCode", SqlDbType.VarChar,255),
					new SqlParameter("@communicationId", SqlDbType.VarChar,255),
					new SqlParameter("@hostSim", SqlDbType.VarChar,255),
					new SqlParameter("@protocolType", SqlDbType.VarChar,255),
					new SqlParameter("@firePartition", SqlDbType.VarChar,255),
					new SqlParameter("@systemAddress", SqlDbType.VarChar,255),
					new SqlParameter("@analogValueTypeId", SqlDbType.VarChar,1),
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.detectorCode;
			parameters[1].Value = model.detectorName;
			parameters[2].Value = model.mainframeId;
			parameters[3].Value = model.buildingId;
			parameters[4].Value = model.detectorAddr;
			parameters[5].Value = model.orgId;
			parameters[6].Value = model.detectorType;
			parameters[7].Value = model.ifcsSystemType;
			parameters[8].Value = model.partunitloopCode;
			parameters[9].Value = model.channelNo;
			parameters[10].Value = model.hardwareVersion;
			parameters[11].Value = model.softwareVersion;
			parameters[12].Value = model.personId;
			parameters[13].Value = model.minalarmValue;
			parameters[14].Value = model.maxalarmValue;
			parameters[15].Value = model.deviceRange;
			parameters[16].Value = model.basalArea;
			parameters[17].Value = model.maxHeight;
			parameters[18].Value = model.registerTime;
			parameters[19].Value = model.registerStatus;
			parameters[20].Value = model.detectorRunBigStatus;
			parameters[21].Value = model.detectorRunSmallStatus;
			parameters[22].Value = model.producerId;
			parameters[23].Value = model.produceDate;
			parameters[24].Value = model.runDate;
			parameters[25].Value = model.serviceLimit;
			parameters[26].Value = model.mapType;
			parameters[27].Value = model.gpsX3d;
			parameters[28].Value = model.gpsY3d;
			parameters[29].Value = model.gpsZ3d;
			parameters[30].Value = model.planId;
			parameters[31].Value = model.planX;
			parameters[32].Value = model.planY;
			parameters[33].Value = model.platformCode;
			parameters[34].Value = model.recordCode;
			parameters[35].Value = model.communicationId;
			parameters[36].Value = model.hostSim;
			parameters[37].Value = model.protocolType;
			parameters[38].Value = model.firePartition;
			parameters[39].Value = model.systemAddress;
			parameters[40].Value = model.analogValueTypeId;
			parameters[41].Value = model.Guid;

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
		public bool Delete(Guid Guid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TcqInfo ");
			strSql.Append(" where Guid=@Guid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = Guid;

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
		public bool DeleteList(string Guidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TcqInfo ");
			strSql.Append(" where Guid in ("+Guidlist + ")  ");
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
		public ClassLibrary1.Model.TcqInfo GetModel(Guid Guid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Guid,detectorCode,detectorName,mainframeId,buildingId,detectorAddr,orgId,detectorType,ifcsSystemType,partunitloopCode,channelNo,hardwareVersion,softwareVersion,personId,minalarmValue,maxalarmValue,deviceRange,basalArea,maxHeight,registerTime,registerStatus,detectorRunBigStatus,detectorRunSmallStatus,producerId,produceDate,runDate,serviceLimit,mapType,gpsX3d,gpsY3d,gpsZ3d,planId,planX,planY,platformCode,recordCode,communicationId,hostSim,protocolType,firePartition,systemAddress,analogValueTypeId from TcqInfo ");
			strSql.Append(" where Guid=@Guid ");
			SqlParameter[] parameters = {
					new SqlParameter("@Guid", SqlDbType.UniqueIdentifier,16)			};
			parameters[0].Value = Guid;

			ClassLibrary1.Model.TcqInfo model=new ClassLibrary1.Model.TcqInfo();
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
		public ClassLibrary1.Model.TcqInfo DataRowToModel(DataRow row)
		{
			ClassLibrary1.Model.TcqInfo model=new ClassLibrary1.Model.TcqInfo();
			if (row != null)
			{
				if(row["Guid"]!=null && row["Guid"].ToString()!="")
				{
					model.Guid= new Guid(row["Guid"].ToString());
				}
				if(row["detectorCode"]!=null)
				{
					model.detectorCode=row["detectorCode"].ToString();
				}
				if(row["detectorName"]!=null)
				{
					model.detectorName=row["detectorName"].ToString();
				}
				if(row["mainframeId"]!=null)
				{
					model.mainframeId=row["mainframeId"].ToString();
				}
				if(row["buildingId"]!=null)
				{
					model.buildingId=row["buildingId"].ToString();
				}
				if(row["detectorAddr"]!=null)
				{
					model.detectorAddr=row["detectorAddr"].ToString();
				}
				if(row["orgId"]!=null)
				{
					model.orgId=row["orgId"].ToString();
				}
				if(row["detectorType"]!=null)
				{
					model.detectorType=row["detectorType"].ToString();
				}
				if(row["ifcsSystemType"]!=null)
				{
					model.ifcsSystemType=row["ifcsSystemType"].ToString();
				}
				if(row["partunitloopCode"]!=null)
				{
					model.partunitloopCode=row["partunitloopCode"].ToString();
				}
				if(row["channelNo"]!=null)
				{
					model.channelNo=row["channelNo"].ToString();
				}
				if(row["hardwareVersion"]!=null)
				{
					model.hardwareVersion=row["hardwareVersion"].ToString();
				}
				if(row["softwareVersion"]!=null)
				{
					model.softwareVersion=row["softwareVersion"].ToString();
				}
				if(row["personId"]!=null)
				{
					model.personId=row["personId"].ToString();
				}
				if(row["minalarmValue"]!=null)
				{
					model.minalarmValue=row["minalarmValue"].ToString();
				}
				if(row["maxalarmValue"]!=null)
				{
					model.maxalarmValue=row["maxalarmValue"].ToString();
				}
				if(row["deviceRange"]!=null)
				{
					model.deviceRange=row["deviceRange"].ToString();
				}
				if(row["basalArea"]!=null)
				{
					model.basalArea=row["basalArea"].ToString();
				}
				if(row["maxHeight"]!=null)
				{
					model.maxHeight=row["maxHeight"].ToString();
				}
				if(row["registerTime"]!=null)
				{
					model.registerTime=row["registerTime"].ToString();
				}
				if(row["registerStatus"]!=null)
				{
					model.registerStatus=row["registerStatus"].ToString();
				}
				if(row["detectorRunBigStatus"]!=null)
				{
					model.detectorRunBigStatus=row["detectorRunBigStatus"].ToString();
				}
				if(row["detectorRunSmallStatus"]!=null)
				{
					model.detectorRunSmallStatus=row["detectorRunSmallStatus"].ToString();
				}
				if(row["producerId"]!=null)
				{
					model.producerId=row["producerId"].ToString();
				}
				if(row["produceDate"]!=null)
				{
					model.produceDate=row["produceDate"].ToString();
				}
				if(row["runDate"]!=null)
				{
					model.runDate=row["runDate"].ToString();
				}
				if(row["serviceLimit"]!=null)
				{
					model.serviceLimit=row["serviceLimit"].ToString();
				}
				if(row["mapType"]!=null)
				{
					model.mapType=row["mapType"].ToString();
				}
				if(row["gpsX3d"]!=null)
				{
					model.gpsX3d=row["gpsX3d"].ToString();
				}
				if(row["gpsY3d"]!=null)
				{
					model.gpsY3d=row["gpsY3d"].ToString();
				}
				if(row["gpsZ3d"]!=null)
				{
					model.gpsZ3d=row["gpsZ3d"].ToString();
				}
				if(row["planId"]!=null)
				{
					model.planId=row["planId"].ToString();
				}
				if(row["planX"]!=null)
				{
					model.planX=row["planX"].ToString();
				}
				if(row["planY"]!=null)
				{
					model.planY=row["planY"].ToString();
				}
				if(row["platformCode"]!=null)
				{
					model.platformCode=row["platformCode"].ToString();
				}
				if(row["recordCode"]!=null)
				{
					model.recordCode=row["recordCode"].ToString();
				}
				if(row["communicationId"]!=null)
				{
					model.communicationId=row["communicationId"].ToString();
				}
				if(row["hostSim"]!=null)
				{
					model.hostSim=row["hostSim"].ToString();
				}
				if(row["protocolType"]!=null)
				{
					model.protocolType=row["protocolType"].ToString();
				}
				if(row["firePartition"]!=null)
				{
					model.firePartition=row["firePartition"].ToString();
				}
				if(row["systemAddress"]!=null)
				{
					model.systemAddress=row["systemAddress"].ToString();
				}
				if(row["analogValueTypeId"]!=null)
				{
					model.analogValueTypeId=row["analogValueTypeId"].ToString();
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
			strSql.Append("select Guid,detectorCode,detectorName,mainframeId,buildingId,detectorAddr,orgId,detectorType,ifcsSystemType,partunitloopCode,channelNo,hardwareVersion,softwareVersion,personId,minalarmValue,maxalarmValue,deviceRange,basalArea,maxHeight,registerTime,registerStatus,detectorRunBigStatus,detectorRunSmallStatus,producerId,produceDate,runDate,serviceLimit,mapType,gpsX3d,gpsY3d,gpsZ3d,planId,planX,planY,platformCode,recordCode,communicationId,hostSim,protocolType,firePartition,systemAddress,analogValueTypeId ");
			strSql.Append(" FROM TcqInfo ");
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
			strSql.Append(" Guid,detectorCode,detectorName,mainframeId,buildingId,detectorAddr,orgId,detectorType,ifcsSystemType,partunitloopCode,channelNo,hardwareVersion,softwareVersion,personId,minalarmValue,maxalarmValue,deviceRange,basalArea,maxHeight,registerTime,registerStatus,detectorRunBigStatus,detectorRunSmallStatus,producerId,produceDate,runDate,serviceLimit,mapType,gpsX3d,gpsY3d,gpsZ3d,planId,planX,planY,platformCode,recordCode,communicationId,hostSim,protocolType,firePartition,systemAddress,analogValueTypeId ");
			strSql.Append(" FROM TcqInfo ");
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
			strSql.Append("select count(1) FROM TcqInfo ");
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
				strSql.Append("order by T.Guid desc");
			}
			strSql.Append(")AS Row, T.*  from TcqInfo T ");
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
			parameters[0].Value = "TcqInfo";
			parameters[1].Value = "Guid";
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

