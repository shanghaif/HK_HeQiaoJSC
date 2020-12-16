using Haikan.DataTools;
using System;
using System.Data;
using System.Collections.Generic;
using ClassLibrary1.Model;
namespace ClassLibrary1.BLL
{
	/// <summary>
	/// TcqInfo
	/// </summary>
	public partial class TcqInfo
	{
		private readonly ClassLibrary1.DAL.TcqInfo dal=new ClassLibrary1.DAL.TcqInfo();
		public TcqInfo()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid Guid)
		{
			return dal.Exists(Guid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ClassLibrary1.Model.TcqInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ClassLibrary1.Model.TcqInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Guid Guid)
		{
			
			return dal.Delete(Guid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Guidlist )
		{
			return dal.DeleteList(PageValidate.SafeLongFilter(Guidlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ClassLibrary1.Model.TcqInfo GetModel(Guid Guid)
		{
			
			return dal.GetModel(Guid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public ClassLibrary1.Model.TcqInfo GetModelByCache(Guid Guid)
		{
			
			string CacheKey = "TcqInfoModel-" + Guid;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Guid);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ClassLibrary1.Model.TcqInfo)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ClassLibrary1.Model.TcqInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ClassLibrary1.Model.TcqInfo> DataTableToList(DataTable dt)
		{
			List<ClassLibrary1.Model.TcqInfo> modelList = new List<ClassLibrary1.Model.TcqInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ClassLibrary1.Model.TcqInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

