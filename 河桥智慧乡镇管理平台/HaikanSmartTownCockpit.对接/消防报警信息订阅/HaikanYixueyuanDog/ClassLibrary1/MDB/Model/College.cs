using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 学院表
	/// </summary>
	[Serializable]
	public partial class College
	{
		public College()
		{}
		#region Model
		private int _id;
		private DateTime? _createtime= DateTime.Now;
		private Guid _collegeuuid;
		private string _collegename;
		private int? _isdeleted;
		private string _addtime;
		private string _addpeople;
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
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CollegeUuid
		{
			set{ _collegeuuid=value;}
			get{return _collegeuuid;}
		}
		/// <summary>
		/// 学院名称
		/// </summary>
		public string CollegeName
		{
			set{ _collegename=value;}
			get{return _collegename;}
		}
		/// <summary>
		/// 0.未删 1.已删
		/// </summary>
		public int? IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 添加时间
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

