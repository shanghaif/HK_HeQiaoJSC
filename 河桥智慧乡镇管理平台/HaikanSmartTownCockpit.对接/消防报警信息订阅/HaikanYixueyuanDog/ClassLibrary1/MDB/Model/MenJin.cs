using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// ÃÅ½û¼ÇÂ¼
	/// </summary>
	[Serializable]
	public partial class MenJin
	{
		public MenJin()
		{}
		#region Model
		private int _id;
		private DateTime _createtime= DateTime.Now;
		private Guid _menjinuuid;
		private string _carnumber;
		private int? _cardstatus;
		private int? _cardtype;
		private string _channelcode;
		private string _channelname;
		private string _deptname;
		private string _devicecode;
		private string _devicename;
		private int? _enterorexit;
		private string _recordid;
		private int? _openresult;
		private int? _opentype;
		private string _papernumber;
		private string _personcode;
		private int? _personid;
		private string _personname;
		private string _recordimage;
		private string _swingtime;
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
		public Guid MenJinUuid
		{
			set{ _menjinuuid=value;}
			get{return _menjinuuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string carNumber
		{
			set{ _carnumber=value;}
			get{return _carnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? cardStatus
		{
			set{ _cardstatus=value;}
			get{return _cardstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? cardType
		{
			set{ _cardtype=value;}
			get{return _cardtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelCode
		{
			set{ _channelcode=value;}
			get{return _channelcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelName
		{
			set{ _channelname=value;}
			get{return _channelname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deptName
		{
			set{ _deptname=value;}
			get{return _deptname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deviceCode
		{
			set{ _devicecode=value;}
			get{return _devicecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string deviceName
		{
			set{ _devicename=value;}
			get{return _devicename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? enterOrExit
		{
			set{ _enterorexit=value;}
			get{return _enterorexit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string recordid
		{
			set{ _recordid=value;}
			get{return _recordid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? openResult
		{
			set{ _openresult=value;}
			get{return _openresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? openType
		{
			set{ _opentype=value;}
			get{return _opentype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string paperNumber
		{
			set{ _papernumber=value;}
			get{return _papernumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string personCode
		{
			set{ _personcode=value;}
			get{return _personcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? personId
		{
			set{ _personid=value;}
			get{return _personid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string personName
		{
			set{ _personname=value;}
			get{return _personname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string recordImage
		{
			set{ _recordimage=value;}
			get{return _recordimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string swingTime
		{
			set{ _swingtime=value;}
			get{return _swingtime;}
		}
		#endregion Model

	}
}

