import axios from '@/libs/api.request'

//列表
export const QiyeList = (data) => {
  return axios.request({
    url: 'Qiye/Qiye/QiyeList',
    method: 'post',
    data
  })
}
 
//添加
export const QiyeCreate = (data) => {
  return axios.request({
    url: 'Qiye/Qiye/QiyeCreate',
    method: 'post',
    data
  })
}

//获取数据
export const QiyefoGet = (data) => {
  return axios.request({
    url: 'Qiye/Qiye/QiyefoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const QiyeEdit = (data) => {
  return axios.request({
    url: 'Qiye/Qiye/QiyeEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'Qiye/Qiye/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'Qiye/Qiye/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const QiyeImport = (data) => {
  return axios.request({
    url: 'Qiye/Qiye/QiyeImport',
    method: 'post',
    data
  })
}

//导出
export const QiyeExport = (ids) => {
  return axios.request({
    url: 'Qiye/Qiye/ExportPass?ids=' + ids,
    method: 'get'
  })
}





