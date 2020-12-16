import axios from '@/libs/api.request'

//列表
export const DangJibenInfoList = (data) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/DangJibenInfoList',
    method: 'post',
    data
  })
}
 
//添加
export const DangJibenInfoCreate = (data) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/DangJibenInfoCreate',
    method: 'post',
    data
  })
}

//获取数据
export const DangJibenInfoGet = (data) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/DangJibenInfoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const DangJibenInfoEdit = (data) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/DangJibenInfoEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const DangJibenInfoImport = (data) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/DangJibenInfoImport',
    method: 'post',
    data
  })
}

//导出
export const DangJibenInfoExport = (ids) => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}


export const dangzuzhi = () => {
  return axios.request({
    url: 'DangJibenInfo/CpcmanInfo/dangzuzhi',
    method: 'get'
  })
}


