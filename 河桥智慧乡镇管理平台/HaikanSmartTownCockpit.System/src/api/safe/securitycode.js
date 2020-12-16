import axios from '@/libs/api.request'

export const getSecuritycodeList = (data) => {
  return axios.request({
    url: 'safe/securitycode/list',
    method: 'post',
    data
  })
}
// createSecuritycode
export const createSecuritycode = (data) => {
  return axios.request({
    url: 'safe/securitycode/create',
    method: 'post',
    data
  })
}

//loadSecuritycode
export const loadSecuritycode = (data) => {
  return axios.request({
    url: 'safe/securitycode/edit/' + data.guid,
    method: 'get'
  })
}

// editSecuritycode
export const editSecuritycode = (data) => {
  return axios.request({
    url: 'safe/securitycode/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteSecuritycode = (ids) => {
  return axios.request({
    url: 'safe/securitycode/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'safe/securitycode/batch',
    method: 'get',
    params: data
  })
}

//导入
export const SecuritycodeImport = (data) => {
  return axios.request({
    url: 'safe/securitycode/securitycodeimport',
    method: 'post',
    data
  })
}

//导出
export const SecuritycodeExport = (ids) => {
  return axios.request({
    url: 'safe/securitycode/securitycodeexport?ids=' + ids,
    method: 'get'
  })
}
