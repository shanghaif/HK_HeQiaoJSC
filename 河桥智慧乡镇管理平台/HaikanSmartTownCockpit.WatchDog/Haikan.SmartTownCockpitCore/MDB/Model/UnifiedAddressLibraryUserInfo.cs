using System;
namespace big_data.Model
{
	/// <summary>
	/// 楼栋和户籍信息匹配表
	/// </summary>
	[Serializable]
	public partial class UnifiedAddressLibraryUserInfo
	{
		public UnifiedAddressLibraryUserInfo()
		{}
		#region Model
		private int _id;
		private int? _unifiedaddresslibraryid;
		private string _useridlist;
		private DateTime? _addtime= DateTime.Now;
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
		public int? UnifiedAddressLibraryID
		{
			set{ _unifiedaddresslibraryid=value;}
			get{return _unifiedaddresslibraryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserIDList
		{
			set{ _useridlist=value;}
			get{return _useridlist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

