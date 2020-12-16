using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 一卡通流水信息表
	/// </summary>
	[Serializable]
	public partial class YiCardLSXX
	{
		public YiCardLSXX()
		{}
		#region Model
		private int _id;
		private Guid _yicarduuid;
		private string _yicardname;
		private string _possessor;
		private string _consumtype;
		private string _expensecal;
		private string _balance;
		private int? _isdeleted;
		private string _addtime;
		private string _addpeople;
		private string _consumtime;
		private string _address;
		private string _sitename;
		private string _depname;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid YicardUuid
		{
			set{ _yicarduuid=value;}
			get{return _yicarduuid;}
		}
		/// <summary>
		/// 一卡通卡号
		/// </summary>
		public string YicardName
		{
			set{ _yicardname=value;}
			get{return _yicardname;}
		}
		/// <summary>
		/// 持有人
		/// </summary>
		public string Possessor
		{
			set{ _possessor=value;}
			get{return _possessor;}
		}
		/// <summary>
		/// 消费类型
		/// </summary>
		public string ConsumType
		{
			set{ _consumtype=value;}
			get{return _consumtype;}
		}
		/// <summary>
		/// 消防记录
		/// </summary>
		public string ExpenseCal
		{
			set{ _expensecal=value;}
			get{return _expensecal;}
		}
		/// <summary>
		/// 余额
		/// </summary>
		public string Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddPeople
		{
			set{ _addpeople=value;}
			get{return _addpeople;}
		}
		/// <summary>
		/// 消费时间
		/// </summary>
		public string ConsumTime
		{
			set{ _consumtime=value;}
			get{return _consumtime;}
		}
		/// <summary>
		/// 操作地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 经营商户
		/// </summary>
		public string SiteName
		{
			set{ _sitename=value;}
			get{return _sitename;}
		}
		/// <summary>
		/// 所属单位或班级
		/// </summary>
		public string DepName
		{
			set{ _depname=value;}
			get{return _depname;}
		}
		#endregion Model

	}
}

