using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 宿舍房间号
	/// </summary>
	[Serializable]
	public partial class DormitoryNum
	{
		public DormitoryNum()
		{}
		#region Model
		private int _id;
		private DateTime? _createtime= DateTime.Now;
		private Guid _dormitorynumuuid;
		private Guid _dormitoryuuid;
		private string _dormitorynumname;
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
		public Guid DormitoryNumUuid
		{
			set{ _dormitorynumuuid=value;}
			get{return _dormitorynumuuid;}
		}
		/// <summary>
		/// 宿舍楼uuid
		/// </summary>
		public Guid DormitoryUuid
		{
			set{ _dormitoryuuid=value;}
			get{return _dormitoryuuid;}
		}
		/// <summary>
		/// 门牌号
		/// </summary>
		public string DormitoryNumName
		{
			set{ _dormitorynumname=value;}
			get{return _dormitorynumname;}
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

