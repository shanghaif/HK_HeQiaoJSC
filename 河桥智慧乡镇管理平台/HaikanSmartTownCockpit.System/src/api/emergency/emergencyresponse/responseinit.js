import axios from '@/libs/api.request'

export const getResponseinitList = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/list',
    method: 'post',
    data
  })
}
// createResponseinit
export const createResponseinit = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/create',
    method: 'post',
    data
  })
}

//loadResponseinit
export const loadResponseinit = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/edit/' + data.guid,
    method: 'get'
  })
}

// editResponseinit
export const editResponseinit = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteResponseinit = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/batch',
    method: 'get',
    params: data
  })
}
//SendInit
export const SendInit = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/sendinit/' + data.guid,
    method: 'get'
  })
}
//导入
export const ResponseinitImport = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/responseinitimport',
    method: 'post',
    data
  })
}

//导出
export const ResponseinitExport = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinit/responseinitexport?ids=' + ids,
    method: 'get'
  })
}
