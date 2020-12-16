import axios from '@/libs/api.request'

export const getUavList = (data) => {
  return axios.request({
    url: 'emergency/uav/list',
    method: 'post',
    data
  })
}
//loadRescuemember
export const loadUavUrl = (data) => {
  return axios.request({
    url: 'emergency/uav/geturl/' + data.guid,
    method: 'get'
  })
}
// createUav
export const createUav = (data) => {
  return axios.request({
    url: 'emergency/uav/create',
    method: 'post',
    data
  })
}

//loadUav
export const loadUav = (data) => {
  return axios.request({
    url: 'emergency/uav/edit/' + data.guid,
    method: 'get'
  })
}

// editUav
export const editUav = (data) => {
  return axios.request({
    url: 'emergency/uav/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteUav = (ids) => {
  return axios.request({
    url: 'emergency/uav/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/uav/batch',
    method: 'get',
    params: data
  })
}
//导入
export const UavImport = (data) => {
  return axios.request({
    url: 'emergency/uav/uavimport',
    method: 'post',
    data
  })
}

//导出
export const UavExport = (ids) => {
  return axios.request({
    url: 'emergency/uav/uavexport?ids=' + ids,
    method: 'get'
  })
}
