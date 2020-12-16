import axios from '@/libs/api.request'

//列表
export const HotelinfoList = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/HotelinfoList',
    method: 'post',
    data
  })
}
 
//添加
export const HotelinfoCreate = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/HotelinfoCreate',
    method: 'post',
    data
  })
}

//获取数据
export const HotelinfofoGet = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/HotelinfofoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const HotelinfoEdit = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/HotelinfoEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const HotelinfoImport = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/HotelinfoImport',
    method: 'post',
    data
  })
}

//导出
export const HotelinfoExport = (ids) => {
  return axios.request({
    url: 'Hotelinfo/Hotelinfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}





