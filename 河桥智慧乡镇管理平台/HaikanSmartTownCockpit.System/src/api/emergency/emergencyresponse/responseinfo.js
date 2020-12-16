import axios from '@/libs/api.request'

export const getResponseinfoList = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/list',
    method: 'post',
    data
  })
}
// createResponseinfo
export const createResponseinfo = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/create',
    method: 'post',
    data
  })
}

//loadResponseinfo
export const loadResponseinfo = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/edit/' + data.guid,
    method: 'get'
  })
}

// editResponseinfo
export const editResponseinfo = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteResponseinfo = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/batch',
    method: 'get',
    params: data
  })
}

//导入
export const ResponseinfoImport = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/responseinfoimport',
    method: 'post',
    data
  })
}

//导出
export const ResponseinfoExport = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/responseinfo/responseinfoexport?ids=' + ids,
    method: 'get'
  })
}
