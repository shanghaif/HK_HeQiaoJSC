using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 专业表
	/// </summary>
	[Serializable]
	public partial class Major
	{
		public Major()
		{}
		#region Model
		private int _id;
		private DateTime? _createtime= DateTime.Now;
		private Guid _majoruuid;
		private string _majorname;
		private int? _isdeleted;
		private string _addtime;
		private string _addpeople;
		private Guid _collegeuuid;
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
		public Guid MajorUuid
		{
			set{ _majoruuid=value;}
			get{return _majoruuid;}
		}
		/// <summary>
		/// 专业名称
		/// </summary>
		public string MajorName
		{
			set{ _majorname=value;}
			get{return _majorname;}
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
		/// <summary>
		/// 学院uuid
		/// </summary>
		public Guid CollegeUuid
		{
			set{ _collegeuuid=value;}
			get{return _collegeuuid;}
		}
		#endregion Model

	}
}

