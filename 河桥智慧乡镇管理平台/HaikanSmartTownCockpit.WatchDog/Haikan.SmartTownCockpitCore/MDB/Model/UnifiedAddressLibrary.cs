using System;
namespace big_data.Model
{
	/// <summary>
	/// 统一地址库
	/// </summary>
	[Serializable]
	public partial class UnifiedAddressLibrary
	{
		public UnifiedAddressLibrary()
		{}
		#region Model
		private Guid _unifiedaddresslibraryuuid;
		private int _id;
		private string _oid;
		private string _sourceaddress;
		private string _city;
		private string _county;
		private string _town;
		private string _community;
		private string _squad;
		private string _village;
		private string _szone;
		private string _street;
		private string _door;
		private string _resregion;
		private string _building;
		private string _building_num;
		private string _unit;
		private string _floor;
		private string _room;
		private string _grid_code;
		private string _building_code;
		private string _house_code;
		private string _code;
		private string _createtime;
		private string _updatetime;
		private int? _isvalid;
		private string _from_status;
		private string _to_status;
		private string _building_path;
		private string _datasource;
		private string _reverse1;
		private string _reverse2;
		private string _lon;
		private string _lat;
		private string _z;
		private string _room_floor;
		private string _addrtype;
		private string _guid;
		private string _inserttime;
		private string _systemid;
		private int? _isdelete;
		private string _belong_building;
		private string _address_type;
		private string _remark;
		private string _addtime;
		/// <summary>
		/// 
		/// </summary>
		public Guid UnifiedAddressLibraryUUID
		{
			set{ _unifiedaddresslibraryuuid=value;}
			get{return _unifiedaddresslibraryuuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string oid
		{
			set{ _oid=value;}
			get{return _oid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SOURCEADDRESS
		{
			set{ _sourceaddress=value;}
			get{return _sourceaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CITY
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string COUNTY
		{
			set{ _county=value;}
			get{return _county;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TOWN
		{
			set{ _town=value;}
			get{return _town;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string COMMUNITY
		{
			set{ _community=value;}
			get{return _community;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SQUAD
		{
			set{ _squad=value;}
			get{return _squad;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VILLAGE
		{
			set{ _village=value;}
			get{return _village;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SZONE
		{
			set{ _szone=value;}
			get{return _szone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string STREET
		{
			set{ _street=value;}
			get{return _street;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DOOR
		{
			set{ _door=value;}
			get{return _door;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RESREGION
		{
			set{ _resregion=value;}
			get{return _resregion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BUILDING
		{
			set{ _building=value;}
			get{return _building;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BUILDING_NUM
		{
			set{ _building_num=value;}
			get{return _building_num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UNIT
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FLOOR
		{
			set{ _floor=value;}
			get{return _floor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ROOM
		{
			set{ _room=value;}
			get{return _room;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GRID_CODE
		{
			set{ _grid_code=value;}
			get{return _grid_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BUILDING_CODE
		{
			set{ _building_code=value;}
			get{return _building_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HOUSE_CODE
		{
			set{ _house_code=value;}
			get{return _house_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CODE
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATETIME
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UPDATETIME
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ISVALID
		{
			set{ _isvalid=value;}
			get{return _isvalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FROM_STATUS
		{
			set{ _from_status=value;}
			get{return _from_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TO_STATUS
		{
			set{ _to_status=value;}
			get{return _to_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BUILDING_PATH
		{
			set{ _building_path=value;}
			get{return _building_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DATASOURCE
		{
			set{ _datasource=value;}
			get{return _datasource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REVERSE1
		{
			set{ _reverse1=value;}
			get{return _reverse1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REVERSE2
		{
			set{ _reverse2=value;}
			get{return _reverse2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LON
		{
			set{ _lon=value;}
			get{return _lon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LAT
		{
			set{ _lat=value;}
			get{return _lat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Z
		{
			set{ _z=value;}
			get{return _z;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ROOM_FLOOR
		{
			set{ _room_floor=value;}
			get{return _room_floor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADDRTYPE
		{
			set{ _addrtype=value;}
			get{return _addrtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUID
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string INSERTTIME
		{
			set{ _inserttime=value;}
			get{return _inserttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SYSTEMID
		{
			set{ _systemid=value;}
			get{return _systemid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ISDELETE
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BELONG_BUILDING
		{
			set{ _belong_building=value;}
			get{return _belong_building;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADDRESS_TYPE
		{
			set{ _address_type=value;}
			get{return _address_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REMARK
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public string AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

