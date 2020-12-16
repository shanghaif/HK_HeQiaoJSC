using System;
namespace big_data.Model
{
	/// <summary>
	/// 河桥镇村信息表
	/// </summary>
	[Serializable]
	public partial class Town
	{
		public Town()
		{}
		#region Model
		private Guid _townuuid;
		private int _id;
		private int? _isdeleted;
		private string _townname;
		private string _townpeople;
		private string _partymember;
		private string _geographical;
		private string _company;
		private Guid _sjtownuuid;
		private string _towngrade;
		private string _lon;
		private string _lat;
		private string _addtime;
		private string _addpeople;
		/// <summary>
		/// 
		/// </summary>
		public Guid TownUuid
		{
			set{ _townuuid=value;}
			get{return _townuuid;}
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
		public int? IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 村镇名称
		/// </summary>
		public string TownName
		{
			set{ _townname=value;}
			get{return _townname;}
		}
		/// <summary>
		/// 人口
		/// </summary>
		public string TownPeople
		{
			set{ _townpeople=value;}
			get{return _townpeople;}
		}
		/// <summary>
		/// 党员人数
		/// </summary>
		public string PartyMember
		{
			set{ _partymember=value;}
			get{return _partymember;}
		}
		/// <summary>
		/// 地域面积
		/// </summary>
		public string Geographical
		{
			set{ _geographical=value;}
			get{return _geographical;}
		}
		/// <summary>
		/// 企业数量
		/// </summary>
		public string Company
		{
			set{ _company=value;}
			get{return _company;}
		}
		/// <summary>
		/// 上级村镇UUID
		/// </summary>
		public Guid SjTownUuid
		{
			set{ _sjtownuuid=value;}
			get{return _sjtownuuid;}
		}
		/// <summary>
		/// 村镇等级
		/// </summary>
		public string TownGrade
		{
			set{ _towngrade=value;}
			get{return _towngrade;}
		}
		/// <summary>
		/// 经度
		/// </summary>
		public string Lon
		{
			set{ _lon=value;}
			get{return _lon;}
		}
		/// <summary>
		/// 纬度
		/// </summary>
		public string Lat
		{
			set{ _lat=value;}
			get{return _lat;}
		}
		/// <summary>
		/// 注册时间
		/// </summary>
		public string AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 添加人
		/// </summary>
		public string AddPeople
		{
			set{ _addpeople=value;}
			get{return _addpeople;}
		}
		#endregion Model

	}
}

