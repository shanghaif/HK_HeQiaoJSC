import axios from '@/libs/api.request'

//列表
export const HqCommunaList = (data) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/HqCommunaList',
    method: 'post',
    data
  })
}
 
//添加
export const HqCommunaCreate = (data) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/HqCommunaCreate',
    method: 'post',
    data
  })
}

//获取数据
export const HqCommunafoGet = (data) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/HqCommunafoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const HqCommunaEdit = (data) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/HqCommunaEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const HqCommunaImport = (data) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/HqCommunaImport',
    method: 'post',
    data
  })
}

//导出
export const HqCommunaExport = (ids) => {
  return axios.request({
    url: 'HqCommuna/HqCommuna/ExportPass?ids=' + ids,
    method: 'get'
  })
}





