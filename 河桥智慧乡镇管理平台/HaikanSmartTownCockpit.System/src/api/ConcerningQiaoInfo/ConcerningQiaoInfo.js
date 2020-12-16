import axios from '@/libs/api.request'



//列表
export const HomeAddressList = (data) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/HomeAddressList',
    method: 'post',
    data
  })
}

//添加
export const HomeAddressCreate = (data) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/HomeAddressCreate',
    method: 'post',
    data
  })
}

//获取数据
export const HomeAddressfoGet = (data) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/HomeAddressfoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const HomeAddressEdit = (data) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/HomeAddressEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HomeAddressImport = (data) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/HomeAddressImport',
    method: 'post',
    data
  })
}

//导出
export const HomeAddressExport = (ids) => {
  return axios.request({
    url: 'ConcerningQiaoInfo/ConcerningQiaoInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}
