import axios from '@/libs/api.request'

//列表
export const GetList = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/GetList',
    method: 'post',
    data
  })
}
 
//添加
export const GetCreate = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/GetCreate',
    method: 'post',
    data
  })
}

//获取数据
export const GetfoGet = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/GetfoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const GetEdit = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/GetEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const GetImport = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/GetImport',
    method: 'post',
    data
  })
}

//导出
export const GetExport = (ids) => {
  return axios.request({
    url: 'FuLianInfo/WomenHouseInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}





