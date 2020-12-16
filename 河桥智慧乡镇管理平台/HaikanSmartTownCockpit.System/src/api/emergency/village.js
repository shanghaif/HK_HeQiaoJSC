import axios from '@/libs/api.request'

export const getVillageList = (data) => {
  return axios.request({
    url: 'emergency/village/list',
    method: 'post',
    data
  })
}
// createVillage
export const createVillage = (data) => {
  return axios.request({
    url: 'emergency/village/create',
    method: 'post',
    data
  })
}

//loadVillage
export const loadVillage = (data) => {
  return axios.request({
    url: 'emergency/village/edit/' + data.guid,
    method: 'get'
  })
}

// editVillage
export const editVillage = (data) => {
  return axios.request({
    url: 'emergency/village/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteVillage = (ids) => {
  return axios.request({
    url: 'emergency/village/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/village/batch',
    method: 'get',
    params: data
  })
}

//导入
export const VillageImport = (data) => {
  return axios.request({
    url: 'emergency/village/villageimport',
    method: 'post',
    data
  })
}

//导出
export const VillageExport = (ids) => {
  return axios.request({
    url: 'emergency/village/villageexport?ids=' + ids,
    method: 'get'
  })
}


export const loadVillageList = () => {
  return axios.request({
    url: 'emergency/village/VillageList/' ,
    method: 'get'
  })
}
