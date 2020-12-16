import axios from '@/libs/api.request'

export const getRescueteamList = (data) => {
  return axios.request({
    url: 'environmental/farmHouse/list',
    method: 'post',
    data
  })
}

export const getTownList = () => {
  return axios.request({
    url: 'environmental/farmHouse/TownList',
    method: 'get'
  })
}

// 创建
export const createRescueteam = (data) => {
  return axios.request({
    url: 'environmental/farmHouse/create',
    method: 'post',
    data
  })
}

//loadRescueteam
export const loadRescueteam = (data) => {
  return axios.request({
    url: 'environmental/farmHouse/edit/' + data.guid,
    method: 'get'
  })
}

// editRescueteam
export const editRescueteam = (data) => {
  return axios.request({
    url: 'environmental/farmHouse/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteRescueteam = (ids) => {
  return axios.request({
    url: 'environmental/farmHouse/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'environmental/farmHouse/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HqCommunaImport = (data) => {
  return axios.request({
    url: 'environmental/farmHouse/FarmHouseImport',
    method: 'post',
    data
  })
}

//导出
export const HqCommunaExport = (ids) => {
  return axios.request({
    url: 'environmental/farmHouse/ExportPass?ids=' + ids,
    method: 'get'
  })
}

