import axios from '@/libs/api.request'

export const getXunchadutyList = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/list',
    method: 'post',
    data
  })
}
// createXunchaduty
export const createXunchaduty = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/create',
    method: 'post',
    data
  })
}

//loadXunchaduty
export const loadXunchaduty = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/edit/' + data.guid,
    method: 'get'
  })
}

// editXunchaduty
export const editXunchaduty = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteXunchaduty = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/batch',
    method: 'get',
    params: data
  })
}
//导入
export const XunchadutyImport = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/xunchadutyimport',
    method: 'post',
    data
  })
}

//导出
export const XunchadutyExport = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xunchaduty/xunchadutyexport?ids=' + ids,
    method: 'get'
  })
}
