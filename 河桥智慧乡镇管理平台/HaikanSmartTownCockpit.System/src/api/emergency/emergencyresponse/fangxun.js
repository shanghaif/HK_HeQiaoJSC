import axios from '@/libs/api.request'

export const getFangXunList = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/list',
    method: 'post',
    data
  })
}
// createRenyuzhuany
export const createFangXun = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/create',
    method: 'post',
    data
  })
}

//loadRenyuzhuany
export const loadFangXun = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/edit/' + data.guid,
    method: 'get'
  })
}

// editRenyuzhuany
export const editFangXun = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteFangXun = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/batch',
    method: 'get',
    params: data
  })
}
//导入
export const RenyuzhuanyImport = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/renyuzhuanyimport',
    method: 'post',
    data
  })
}

//导出
export const FangXunExport = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/fangxun/FangXunExport?ids=' + ids,
    method: 'get'
  })
}
