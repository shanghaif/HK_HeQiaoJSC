using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 班级表
	/// </summary>
	[Serializable]
	public partial class StuClass
	{
		public StuClass()
		{}
		#region Model
		private int _id;
		private DateTime? _createtime= DateTime.Now;
		private Guid _stuclassuuid;
		private Guid _majoruuid;
		private string _stuclassname;
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
		public Guid StuClassUuid
		{
			set{ _stuclassuuid=value;}
			get{return _stuclassuuid;}
		}
		/// <summary>
		/// 专业uuid
		/// </summary>
		public Guid MajorUuid
		{
			set{ _majoruuid=value;}
			get{return _majoruuid;}
		}
		/// <summary>
		/// 班级名称
		/// </summary>
		public string StuClassName
		{
			set{ _stuclassname=value;}
			get{return _stuclassname;}
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

