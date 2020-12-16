using System;
namespace ClassLibrary1.Model
{
	/// <summary>
	/// 教职工
	/// </summary>
	[Serializable]
	public partial class Teacher
	{
		public Teacher()
		{}
		#region Model
		private int _id;
		private DateTime? _createtime= DateTime.Now;
		private Guid _teacheruuid;
		private string _name;
		private string _sex;
		private string _gh;
		private Guid _departmentuuid;
		private string _yicard;
		private Guid _dormitoryuuid;
		private string _picture;
		private Guid _dormitorynumuuid;
		private string _phone;
		private string _idcard;
		private string _email;
		private string _nation;
		private string _political;
		private string _religion;
		private int? _isdeleted;
		private string _addtime;
		private string _addpeople;
		private Guid _schooldistrictuuid;
		private string _professionaltitle;
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
		public Guid TeacherUuid
		{
			set{ _teacheruuid=value;}
			get{return _teacheruuid;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 工号
		/// </summary>
		public string Gh
		{
			set{ _gh=value;}
			get{return _gh;}
		}
		/// <summary>
		/// 部门
		/// </summary>
		public Guid DepartmentUuid
		{
			set{ _departmentuuid=value;}
			get{return _departmentuuid;}
		}
		/// <summary>
		/// 一卡通
		/// </summary>
		public string YiCard
		{
			set{ _yicard=value;}
			get{return _yicard;}
		}
		/// <summary>
		/// 宿舍
		/// </summary>
		public Guid DormitoryUuid
		{
			set{ _dormitoryuuid=value;}
			get{return _dormitoryuuid;}
		}
		/// <summary>
		/// 人脸底图
		/// </summary>
		public string Picture
		{
			set{ _picture=value;}
			get{return _picture;}
		}
		/// <summary>
		/// 门牌号
		/// </summary>
		public Guid DormitoryNumUuid
		{
			set{ _dormitorynumuuid=value;}
			get{return _dormitorynumuuid;}
		}
		/// <summary>
		/// 联系方式
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 身份证号
		/// </summary>
		public string IdCard
		{
			set{ _idcard=value;}
			get{return _idcard;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 民族
		/// </summary>
		public string Nation
		{
			set{ _nation=value;}
			get{return _nation;}
		}
		/// <summary>
		/// 政治面貌
		/// </summary>
		public string Political
		{
			set{ _political=value;}
			get{return _political;}
		}
		/// <summary>
		/// 宗教信仰
		/// </summary>
		public string Religion
		{
			set{ _religion=value;}
			get{return _religion;}
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
		/// 学区uuid
		/// </summary>
		public Guid SchoolDistrictUuid
		{
			set{ _schooldistrictuuid=value;}
			get{return _schooldistrictuuid;}
		}
		/// <summary>
		/// 职称
		/// </summary>
		public string ProfessionalTitle
		{
			set{ _professionaltitle=value;}
			get{return _professionaltitle;}
		}
		#endregion Model

	}
}

