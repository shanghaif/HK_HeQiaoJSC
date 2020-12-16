import axios from '@/libs/api.request'

//列表
export const PTypeList = (data) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/PTypeList',
    method: 'post',
    data
  })
}
 
//添加
export const PTypeCreate = (data) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/PTypeCreate',
    method: 'post',
    data
  })
}

//获取数据
export const PTypefoGet = (data) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/PTypefoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const PTypeEdit = (data) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/PTypeEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const PTypeImport = (data) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/PTypeImport',
    method: 'post',
    data
  })
}

//导出
export const PTypeExport = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PTypeInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}





