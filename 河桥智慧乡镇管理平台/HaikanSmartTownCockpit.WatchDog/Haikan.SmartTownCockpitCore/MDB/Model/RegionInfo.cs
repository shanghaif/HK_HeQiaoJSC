using System;
namespace big_data.Model
{
	/// <summary>
	/// 区域基础表格
	/// </summary>
	[Serializable]
	public partial class RegionInfo
	{
		public RegionInfo()
		{}
		#region Model
		private int _id;
		private int? _regionid;
		private string _regionx;
		private string _regiony;
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
		public int? RegionID
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionX
		{
			set{ _regionx=value;}
			get{return _regionx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionY
		{
			set{ _regiony=value;}
			get{return _regiony;}
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

