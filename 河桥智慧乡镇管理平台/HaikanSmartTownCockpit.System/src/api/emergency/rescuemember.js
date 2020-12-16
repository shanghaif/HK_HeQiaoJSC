import axios from '@/libs/api.request'

export const getRescuememberList = (data) => {
  return axios.request({
    url: 'emergency/rescuemember/list',
    method: 'post',
    data
  })
}
// createRescuemember
export const createRescuemember = (data) => {
  return axios.request({
    url: 'emergency/rescuemember/create',
    method: 'post',
    data
  })
}

//loadRescuemember
export const loadRescuemember = (data) => {
  return axios.request({
    url: 'emergency/rescuemember/edit/' + data.guid,
    method: 'get'
  })
}

// editRescuemember
export const editRescuemember = (data) => {
  return axios.request({
    url: 'emergency/rescuemember/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteRescuemember = (ids) => {
  return axios.request({
    url: 'emergency/rescuemember/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/rescuemember/batch',
    method: 'get',
    params: data
  })
}
// RecueTeamList
export const getRecueTeamList = () => {
  return axios.request({
    url: 'emergency/rescuemember/recueteamlist',
    method: 'get'
  })
}

//导入
export const RescuememberImport = (data) => {
  return axios.request({
    url: 'emergency/rescuemember/rescuememberimport',
    method: 'post',
    data
  })
}

//导出
export const RescuememberExport = (ids) => {
  return axios.request({
    url: 'emergency/rescuemember/rescuememberexport?ids=' + ids,
    method: 'get'
  })
}
