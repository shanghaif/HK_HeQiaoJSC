using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 消防建筑物信息
	/// </summary>
	[Serializable]
	public partial class xf_Building
	{
		public xf_Building()
		{}
		#region Model
		private int _id;
		private DateTime _createtime= DateTime.Now;
		private Guid _buildinguuid;
		private string _buildingname;
		private string _buildingtype;
		private string _buildingusenature;
		private string _firedanger;
		private string _fireresistantlevel;
		private string _structuretype;
		private decimal? _buildingheight;
		private string _buildingaddr;
		private string _regioncode;
		private string _buildingstate;
		private string _buildingtime;
		private string _buildingfinishtime;
		private string _propertyright;
		private decimal? _buildingarea;
		private decimal? _occupyarea;
		private decimal? _standardfloorarea;
		private int? _overfloornum;
		private decimal? _overfloorarea;
		private int? _underfloornum;
		private decimal? _underfloorarea;
		private decimal? _tunnelheight;
		private decimal? _tunnellength;
		private int? _controlroomcondition;
		private string _controlroomposition;
		private int? _workerdailynum;
		private int? _buildinggalleryful;
		private int? _fireelevatornum;
		private string _fireelevatorposition;
		private decimal? _fireelevatorcarrery;
		private decimal? _shelterfloornum;
		private decimal? _shelterfloorarea;
		private string _shelterfloorposition;
		private int? _exitnum;
		private string _exitposition;
		private string _exitform;
		private string _storagename;
		private int? _storagenum;
		private string _storagenature;
		private string _storageshape;
		private decimal? _storagesize;
		private string _mainmaterial;
		private string _mainproduct;
		private int? _enterorgnum;
		private string _orgabutsituation;
		private string _belongorgid;
		private string _managerorgid;
		private int? _unitnum;
		private int? _floornum;
		private int? _maptype;
		private decimal? _gpsx3d;
		private decimal? _gpsy3d;
		private decimal? _gpsz3d;
		private int? _ismezzanine;
		private string _memo;
		private string _platformcode;
		private string _recordcode;
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
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid BuildingUuid
		{
			set{ _buildinguuid=value;}
			get{return _buildinguuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingName
		{
			set{ _buildingname=value;}
			get{return _buildingname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingType
		{
			set{ _buildingtype=value;}
			get{return _buildingtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingUseNature
		{
			set{ _buildingusenature=value;}
			get{return _buildingusenature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fireDanger
		{
			set{ _firedanger=value;}
			get{return _firedanger;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fireResistantLevel
		{
			set{ _fireresistantlevel=value;}
			get{return _fireresistantlevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string structureType
		{
			set{ _structuretype=value;}
			get{return _structuretype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? buildingHeight
		{
			set{ _buildingheight=value;}
			get{return _buildingheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingAddr
		{
			set{ _buildingaddr=value;}
			get{return _buildingaddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string regionCode
		{
			set{ _regioncode=value;}
			get{return _regioncode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingState
		{
			set{ _buildingstate=value;}
			get{return _buildingstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingTime
		{
			set{ _buildingtime=value;}
			get{return _buildingtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingFinishTime
		{
			set{ _buildingfinishtime=value;}
			get{return _buildingfinishtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string propertyRight
		{
			set{ _propertyright=value;}
			get{return _propertyright;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? buildingArea
		{
			set{ _buildingarea=value;}
			get{return _buildingarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? occupyArea
		{
			set{ _occupyarea=value;}
			get{return _occupyarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? standardFloorArea
		{
			set{ _standardfloorarea=value;}
			get{return _standardfloorarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? overFloorNum
		{
			set{ _overfloornum=value;}
			get{return _overfloornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? overFloorArea
		{
			set{ _overfloorarea=value;}
			get{return _overfloorarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? underFloorNum
		{
			set{ _underfloornum=value;}
			get{return _underfloornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? underFloorArea
		{
			set{ _underfloorarea=value;}
			get{return _underfloorarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? tunnelHeight
		{
			set{ _tunnelheight=value;}
			get{return _tunnelheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? tunnelLength
		{
			set{ _tunnellength=value;}
			get{return _tunnellength;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? controlRoomCondition
		{
			set{ _controlroomcondition=value;}
			get{return _controlroomcondition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlRoomPosition
		{
			set{ _controlroomposition=value;}
			get{return _controlroomposition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? workerDailyNum
		{
			set{ _workerdailynum=value;}
			get{return _workerdailynum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? buildingGalleryful
		{
			set{ _buildinggalleryful=value;}
			get{return _buildinggalleryful;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fireElevatorNum
		{
			set{ _fireelevatornum=value;}
			get{return _fireelevatornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fireElevatorPosition
		{
			set{ _fireelevatorposition=value;}
			get{return _fireelevatorposition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? fireElevatorCarrery
		{
			set{ _fireelevatorcarrery=value;}
			get{return _fireelevatorcarrery;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? shelterFloorNum
		{
			set{ _shelterfloornum=value;}
			get{return _shelterfloornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? shelterFloorArea
		{
			set{ _shelterfloorarea=value;}
			get{return _shelterfloorarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shelterFloorPosition
		{
			set{ _shelterfloorposition=value;}
			get{return _shelterfloorposition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? exitNum
		{
			set{ _exitnum=value;}
			get{return _exitnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string exitPosition
		{
			set{ _exitposition=value;}
			get{return _exitposition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string exitForm
		{
			set{ _exitform=value;}
			get{return _exitform;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storageName
		{
			set{ _storagename=value;}
			get{return _storagename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? storageNum
		{
			set{ _storagenum=value;}
			get{return _storagenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storageNature
		{
			set{ _storagenature=value;}
			get{return _storagenature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storageShape
		{
			set{ _storageshape=value;}
			get{return _storageshape;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? storageSize
		{
			set{ _storagesize=value;}
			get{return _storagesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mainMaterial
		{
			set{ _mainmaterial=value;}
			get{return _mainmaterial;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mainProduct
		{
			set{ _mainproduct=value;}
			get{return _mainproduct;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? enterOrgNum
		{
			set{ _enterorgnum=value;}
			get{return _enterorgnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string orgAbutSituation
		{
			set{ _orgabutsituation=value;}
			get{return _orgabutsituation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string belongOrgId
		{
			set{ _belongorgid=value;}
			get{return _belongorgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string managerOrgId
		{
			set{ _managerorgid=value;}
			get{return _managerorgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? unitNum
		{
			set{ _unitnum=value;}
			get{return _unitnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? floorNum
		{
			set{ _floornum=value;}
			get{return _floornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? mapType
		{
			set{ _maptype=value;}
			get{return _maptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? gpsX3d
		{
			set{ _gpsx3d=value;}
			get{return _gpsx3d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? gpsY3d
		{
			set{ _gpsy3d=value;}
			get{return _gpsy3d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? gpsZ3d
		{
			set{ _gpsz3d=value;}
			get{return _gpsz3d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isMezzanine
		{
			set{ _ismezzanine=value;}
			get{return _ismezzanine;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platformCode
		{
			set{ _platformcode=value;}
			get{return _platformcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string recordCode
		{
			set{ _recordcode=value;}
			get{return _recordcode;}
		}
		#endregion Model

	}
}

