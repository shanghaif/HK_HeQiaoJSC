import axios from '@/libs/api.request'

export const getEvacuateList = (data) => {
  return axios.request({
    url: 'emergency/evacuate/list',
    method: 'post',
    data
  })
}
// createEvacuate
export const createEvacuate = (data) => {
  return axios.request({
    url: 'emergency/evacuate/create',
    method: 'post',
    data
  })
}

//loadEvacuate
export const loadEvacuate = (data) => {
  return axios.request({
    url: 'emergency/evacuate/edit/' + data.guid,
    method: 'get'
  })
}

// editEvacuate
export const editEvacuate = (data) => {
  return axios.request({
    url: 'emergency/evacuate/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteEvacuate = (ids) => {
  return axios.request({
    url: 'emergency/evacuate/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/evacuate/batch',
    method: 'get',
    params: data
  })
}
//导入
export const EvacuateImport = (data) => {
  return axios.request({
    url: 'emergency/evacuate/EvacuateImport',
    method: 'post',
    data
  })
}

//导出
export const EvacuateExport = (ids) => {
  return axios.request({
    url: 'emergency/evacuate/EvacuateExport?ids=' + ids,
    method: 'get'
  })
}
