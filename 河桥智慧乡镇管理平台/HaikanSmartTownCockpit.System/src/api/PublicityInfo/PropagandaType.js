import axios from '@/libs/api.request'

//列表
export const PTypeList = (data) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/PTypeList',
    method: 'post',
    data
  })
}
 
//添加
export const PTypeCreate = (data) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/PTypeCreate',
    method: 'post',
    data
  })
}

//获取数据
export const PTypefoGet = (data) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/PTypefoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const PTypeEdit = (data) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/PTypeEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const PTypeImport = (data) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/PTypeImport',
    method: 'post',
    data
  })
}

//导出
export const PTypeExport = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PropaType/ExportPass?ids=' + ids,
    method: 'get'
  })
}





