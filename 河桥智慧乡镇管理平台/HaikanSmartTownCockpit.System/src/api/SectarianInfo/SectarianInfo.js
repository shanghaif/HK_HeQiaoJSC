import axios from '@/libs/api.request'

//列表
export const SectarianInfoList = (data) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/SectarianInfoList',
    method: 'post',
    data
  })
}
 
//添加
export const SectarianInfoCreate = (data) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/SectarianInfoCreate',
    method: 'post',
    data
  })
}

//获取数据
export const SectarianInfofoGet = (data) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/SectarianInfofoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const SectarianInfoEdit = (data) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/SectarianInfoEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const SectarianInfoImport = (data) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/SectarianInfoImport',
    method: 'post',
    data
  })
}

//导出
export const SectarianInfoExport = (ids) => {
  return axios.request({
    url: 'SectarianInfo/SectarianInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}





