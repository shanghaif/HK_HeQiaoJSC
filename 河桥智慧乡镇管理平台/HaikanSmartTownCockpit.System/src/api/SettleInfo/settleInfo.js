import axios from '@/libs/api.request'

export const GetList = (data) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/List',
    method: 'post',
    data
  })
}

export const GetCreate = (data) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/Create',
    method: 'post',
    data
  })
}

export const GetEdit = (data) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/Edit',
    method: 'post',
    data
  })
}

export const GetShow = (data) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/Show?guid=' + data,
    method: 'get',
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/batch',
    method: 'get',
    params: data
  })
}

//导入
export const GetImport = (data) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/Import',
    method: 'post',
    data
  })
}

//导出
export const GetExportPass = (ids) => {
  return axios.request({
    url: 'SettleInfo/settleInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}
