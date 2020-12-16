using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// TcqType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TcqType
	{
		public TcqType()
		{}
		#region Model
		private Guid _tcqguid;
		private int? _tcqid;
		private string _tcqname;
		/// <summary>
		/// 
		/// </summary>
		public Guid Tcqguid
		{
			set{ _tcqguid=value;}
			get{return _tcqguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tcqid
		{
			set{ _tcqid=value;}
			get{return _tcqid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TcqName
		{
			set{ _tcqname=value;}
			get{return _tcqname;}
		}
		#endregion Model

	}
}

