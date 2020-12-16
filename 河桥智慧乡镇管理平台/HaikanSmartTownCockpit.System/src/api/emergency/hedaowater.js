import axios from '@/libs/api.request'

export const getHeDaowaterList = (data) => {
  return axios.request({
    url: 'emergency/hedaowater/list',
    method: 'post',
    data
  })
}
// createHeDaowater
export const createHeDaowater = (data) => {
  return axios.request({
    url: 'emergency/hedaowater/create',
    method: 'post',
    data
  })
}

//loadHeDaowater
export const loadHeDaowater = (data) => {
  return axios.request({
    url: 'emergency/hedaowater/edit/' + data.guid,
    method: 'get'
  })
}

// editHeDaowater
export const editHeDaowater = (data) => {
  return axios.request({
    url: 'emergency/hedaowater/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteHeDaowater = (ids) => {
  return axios.request({
    url: 'emergency/hedaowater/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'emergency/hedaowater/batch',
    method: 'get',
    params: data
  })
}
//导入
export const HeDaowaterImport = (data) => {
  return axios.request({
    url: 'emergency/hedaowater/hedaowaterimport',
    method: 'post',
    data
  })
}

//导出
export const HeDaowaterExport = (ids) => {
  return axios.request({
    url: 'emergency/hedaowater/hedaowaterexport?ids=' + ids,
    method: 'get'
  })
}
