using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// TcqInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TcqInfo
	{
		public TcqInfo()
		{}
		#region Model
		private Guid _guid;
		private string _detectorcode;
		private string _detectorname;
		private string _mainframeid;
		private string _buildingid;
		private string _detectoraddr;
		private string _orgid;
		private string _detectortype;
		private string _ifcssystemtype;
		private string _partunitloopcode;
		private string _channelno;
		private string _hardwareversion;
		private string _softwareversion;
		private string _personid;
		private string _minalarmvalue;
		private string _maxalarmvalue;
		private string _devicerange;
		private string _basalarea;
		private string _maxheight;
		private string _registertime;
		private string _registerstatus;
		private string _detectorrunbigstatus;
		private string _detectorrunsmallstatus;
		private string _producerid;
		private string _producedate;
		private string _rundate;
		private string _servicelimit;
		private string _maptype;
		private string _gpsx3d;
		private string _gpsy3d;
		private string _gpsz3d;
		private string _planid;
		private string _planx;
		private string _plany;
		private string _platformcode;
		private string _recordcode;
		private string _communicationid;
		private string _hostsim;
		private string _protocoltype;
		private string _firepartition;
		private string _systemaddress;
		private string _analogvaluetypeid;
		/// <summary>
		/// 
		/// </summary>
		public Guid Guid
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detectorCode
		{
			set{ _detectorcode=value;}
			get{return _detectorcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detectorName
		{
			set{ _detectorname=value;}
			get{return _detectorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mainframeId
		{
			set{ _mainframeid=value;}
			get{return _mainframeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string buildingId
		{
			set{ _buildingid=value;}
			get{return _buildingid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detectorAddr
		{
			set{ _detectoraddr=value;}
			get{return _detectoraddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string orgId
		{
			set{ _orgid=value;}
			get{return _orgid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detectorType
		{
			set{ _detectortype=value;}
			get{return _detectortype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ifcsSystemType
		{
			set{ _ifcssystemtype=value;}
			get{return _ifcssystemtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string partunitloopCode
		{
			set{ _partunitloopcode=value;}
			get{return _partunitloopcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelNo
		{
			set{ _channelno=value;}
			get{return _channelno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hardwareVersion
		{
			set{ _hardwareversion=value;}
			get{return _hardwareversion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string softwareVersion
		{
			set{ _softwareversion=value;}
			get{return _softwareversion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string personId
		{
			set{ _personid=value;}
			get{return _personid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string minalarmValue
		{
			set{ _minalarmvalue=value;}
			get{return _minalarmvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string maxalarmValue
		{
			set{ _maxalarmvalue=value;}
			get{return _maxalarmvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deviceRange
		{
			set{ _devicerange=value;}
			get{return _devicerange;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string basalArea
		{
			set{ _basalarea=value;}
			get{return _basalarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string maxHeight
		{
			set{ _maxheight=value;}
			get{return _maxheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string registerTime
		{
			set{ _registertime=value;}
			get{return _registertime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string registerStatus
		{
			set{ _registerstatus=value;}
			get{return _registerstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detectorRunBigStatus
		{
			set{ _detectorrunbigstatus=value;}
			get{return _detectorrunbigstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string detectorRunSmallStatus
		{
			set{ _detectorrunsmallstatus=value;}
			get{return _detectorrunsmallstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string producerId
		{
			set{ _producerid=value;}
			get{return _producerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string produceDate
		{
			set{ _producedate=value;}
			get{return _producedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string runDate
		{
			set{ _rundate=value;}
			get{return _rundate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string serviceLimit
		{
			set{ _servicelimit=value;}
			get{return _servicelimit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mapType
		{
			set{ _maptype=value;}
			get{return _maptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gpsX3d
		{
			set{ _gpsx3d=value;}
			get{return _gpsx3d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gpsY3d
		{
			set{ _gpsy3d=value;}
			get{return _gpsy3d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gpsZ3d
		{
			set{ _gpsz3d=value;}
			get{return _gpsz3d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string planId
		{
			set{ _planid=value;}
			get{return _planid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string planX
		{
			set{ _planx=value;}
			get{return _planx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string planY
		{
			set{ _plany=value;}
			get{return _plany;}
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
		/// <summary>
		/// 
		/// </summary>
		public string communicationId
		{
			set{ _communicationid=value;}
			get{return _communicationid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hostSim
		{
			set{ _hostsim=value;}
			get{return _hostsim;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string protocolType
		{
			set{ _protocoltype=value;}
			get{return _protocoltype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string firePartition
		{
			set{ _firepartition=value;}
			get{return _firepartition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string systemAddress
		{
			set{ _systemaddress=value;}
			get{return _systemaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string analogValueTypeId
		{
			set{ _analogvaluetypeid=value;}
			get{return _analogvaluetypeid;}
		}
		#endregion Model

	}
}

