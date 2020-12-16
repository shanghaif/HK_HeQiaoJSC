import axios from '@/libs/api.request'

//地址库对接
export const btnToken = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/backtoken',
    method: 'post',
  })
}

//列表
export const HomeAddressList = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/HomeAddressList',
    method: 'post',
    data
  })
}

//添加
export const HomeAddressCreate = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/HomeAddressCreate',
    method: 'post',
    data
  })
}

//获取数据
export const HomeAddressfoGet = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/HomeAddressfoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const HomeAddressEdit = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/HomeAddressEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HomeAddressImport = (data) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/HomeAddressImport',
    method: 'post',
    data
  })
}

//导出
export const HomeAddressExport = (ids) => {
  return axios.request({
    url: 'HomeAddress/HomeAddress/ExportPass?ids=' + ids,
    method: 'get'
  })
}
