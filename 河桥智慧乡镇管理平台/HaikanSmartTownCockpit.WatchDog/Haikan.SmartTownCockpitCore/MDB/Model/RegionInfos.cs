using System;
namespace big_data.Model
{
	/// <summary>
	/// 区域整理后表
	/// </summary>
	[Serializable]
	public partial class RegionInfos
	{
		public RegionInfos()
		{}
		#region Model
		private int _id;
		private int? _regionid;
		private string _regionxyinfo;
		private DateTime? _addtime= DateTime.Now;
		private string _unifiedaddresslibraryid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 区域id
		/// </summary>
		public int? RegionID
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 坐标集合 例如：120.3654541,30.4414447|120.3654541,30.4414447
		/// </summary>
		public string RegionXYInfo
		{
			set{ _regionxyinfo=value;}
			get{return _regionxyinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 地址库ID集合
		/// </summary>
		public string UnifiedAddressLibraryID
		{
			set{ _unifiedaddresslibraryid=value;}
			get{return _unifiedaddresslibraryid;}
		}
		#endregion Model

	}
}

