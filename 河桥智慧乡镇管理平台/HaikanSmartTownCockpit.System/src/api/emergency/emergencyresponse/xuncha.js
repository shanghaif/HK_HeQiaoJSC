import axios from '@/libs/api.request'

export const getXunchaList = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/list',
    method: 'post',
    data
  })
}
// createXuncha
export const createXuncha = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/create',
    method: 'post',
    data
  })
}

//loadXuncha
export const loadXuncha = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/edit/' + data.guid,
    method: 'get'
  })
}

// editXuncha
export const editXuncha = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteXuncha = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/batch',
    method: 'get',
    params: data
  })
}
//导入
export const XunchaImport = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/xunchaimport',
    method: 'post',
    data
  })
}

//导出
export const XunchaExport = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/xuncha/xunchaexport?ids=' + ids,
    method: 'get'
  })
}
