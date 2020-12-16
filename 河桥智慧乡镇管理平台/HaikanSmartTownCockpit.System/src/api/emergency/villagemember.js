import axios from '@/libs/api.request'

export const getVillagememberList = (data) => {
  return axios.request({
    url: 'emergency/villagemember/list',
    method: 'post',
    data
  })
}
// createVillagemember
export const createVillagemember = (data) => {
  return axios.request({
    url: 'emergency/villagemember/create',
    method: 'post',
    data
  })
}

//loadVillagemember
export const loadVillagemember = (data) => {
  return axios.request({
    url: 'emergency/villagemember/edit/' + data.guid,
    method: 'get'
  })
}

// editVillagemember
export const editVillagemember = (data) => {
  return axios.request({
    url: 'emergency/villagemember/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteVillagemember = (ids) => {
  return axios.request({
    url: 'emergency/villagemember/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/villagemember/batch',
    method: 'get',
    params: data
  })
}
// getVillageList
export const getVillageList = () => {
  return axios.request({
    url: 'emergency/villagemember/villagelist',
    method: 'get'
  })
}
//导入
export const VillagememberImport = (data) => {
  return axios.request({
    url: 'emergency/villagemember/villagememberimport',
    method: 'post',
    data
  })
}

//导出
export const VillagememberExport = (ids) => {
  return axios.request({
    url: 'emergency/villagemember/villagememberexport?ids=' + ids,
    method: 'get'
  })
}
