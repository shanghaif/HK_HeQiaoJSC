import axios from '@/libs/api.request'

export const townlist = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/townlist',
    method: 'post',
    data
  })
}

export const CpcList = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/CpcList',
    method: 'post',
    data
  })
}

export const CpcCreate = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/CpcCreate',
    method: 'post',
    data
  })
}

export const CpcEdit = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/CpcEdit',
    method: 'post',
    data
  })
}

export const CpcShow = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/CpcShow?guid=' + data,
    method: 'get',
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'cpcman/cpcman/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/batch',
    method: 'get',
    params: data
  })
}

//导入
export const UserInfoImport = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/UserInfoImport',
    method: 'post',
    data
  })
}

//导出
export const UserInfoExport = (data) => {
  return axios.request({
    url: 'cpcman/cpcman/ExportPass?ids=' + data.ids+'&town='+data.town,
    method: 'get'
  })
}
