import axios from '@/libs/api.request'

//列表
export const GetList = (data) => {
  return axios.request({
    url: 'KeyPoint/Teenager/List',
    method: 'post',
    data
  })
}
 
//添加
export const GetCreate = (data) => {
  return axios.request({
    url: 'KeyPoint/Teenager/Create',
    method: 'post',
    data
  })
}

//获取数据
export const LoadInfo = (data) => {
  return axios.request({
    url: 'KeyPoint/Teenager/load?guid=' + data,
    method: 'get',
  })
}

//编辑
export const GetEdit = (data) => {
  return axios.request({
    url: 'KeyPoint/Teenager/Edit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'KeyPoint/Teenager/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'KeyPoint/Teenager/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const GetImport = (data) => {
  return axios.request({
    url: 'KeyPoint/Teenager/GetImport',
    method: 'post',
    data
  })
}

//导出
export const GetExport = (ids) => {
  return axios.request({
    url: 'KeyPoint/Teenager/ExportPass?ids=' + ids,
    method: 'get'
  })
}





