import axios from '@/libs/api.request'

export const getRescueteamList = (data) => {
  return axios.request({
    url: 'emergency/rescueteam/list',
    method: 'post',
    data
  })
}
// createRescueteam
export const createRescueteam = (data) => {
  return axios.request({
    url: 'emergency/rescueteam/create',
    method: 'post',
    data
  })
}

//loadRescueteam
export const loadRescueteam = (data) => {
  return axios.request({
    url: 'emergency/rescueteam/edit/' + data.guid,
    method: 'get'
  })
}

// editRescueteam
export const editRescueteam = (data) => {
  return axios.request({
    url: 'emergency/rescueteam/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteRescueteam = (ids) => {
  return axios.request({
    url: 'emergency/rescueteam/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/rescueteam/batch',
    method: 'get',
    params: data
  })
}
//导入
export const RescueteamImport = (data) => {
  return axios.request({
    url: 'emergency/rescueteam/rescueteamimport',
    method: 'post',
    data
  })
}

//导出
export const RescueteamExport = (ids) => {
  return axios.request({
    url: 'emergency/rescueteam/rescueteamexport?ids=' + ids,
    method: 'get'
  })
}
