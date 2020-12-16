import axios from '@/libs/api.request'

export const getRenyuzhuanyList = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/list',
    method: 'post',
    data
  })
}
// createRenyuzhuany
export const createRenyuzhuany = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/create',
    method: 'post',
    data
  })
}

//loadRenyuzhuany
export const loadRenyuzhuany = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/edit/' + data.guid,
    method: 'get'
  })
}

// editRenyuzhuany
export const editRenyuzhuany = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteRenyuzhuany = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/batch',
    method: 'get',
    params: data
  })
}
//导入
export const RenyuzhuanyImport = (data) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/renyuzhuanyimport',
    method: 'post',
    data
  })
}

//导出
export const RenyuzhuanyExport = (ids) => {
  return axios.request({
    url: 'emergency/emergencyresponse/renyuzhuany/renyuzhuanyexport?ids=' + ids,
    method: 'get'
  })
}
