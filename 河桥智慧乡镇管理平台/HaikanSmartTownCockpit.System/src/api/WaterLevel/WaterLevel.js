import axios from '@/libs/api.request'

//列表
export const GetList = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/GetList',
    method: 'post',
    data
  })
}
 
//添加
export const GetCreate = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/GetCreate',
    method: 'post',
    data
  })
}

//获取数据
export const GetfoGet = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/GetfoGet?guid=' + data,
    method: 'get',
  })
}

//获取数据
export const Getlishijk = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/Getlishijk',
    method: 'post',
    data
  })
}

//编辑
export const GetEdit = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/GetEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const GetImport = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/GetImport',
    method: 'post',
    data
  })
}

//导入
export const GetImportjk = (data) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/GetImportjk',
    method: 'post',
    data
  })
}

//导出
export const GetExport = (ids) => {
  return axios.request({
    url: 'WaterLevel/WaterLevel/ExportPass?ids=' + ids,
    method: 'get'
  })
}





