import axios from '@/libs/api.request'

export const getRescueteamList = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/list',
    method: 'post',
    data
  })
}
// createRescueteam
export const createRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/create',
    method: 'post',
    data
  })
}

//loadRescueteam
export const loadRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/edit/' + data.guid,
    method: 'get'
  })
}

// editRescueteam
export const editRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteRescueteam = (ids) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HqCommunaImport = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/HqCommunaImport',
    method: 'post',
    data
  })
}

//导出
export const HqCommunaExport = (ids) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/ExportPass?ids=' + ids,
    method: 'get'
  })
}

//获取数据
export const rentalHousfoGet = (data) => {
  return axios.request({
    url: 'socialgovernance/rentalHous/rentalHousfoGet?guid=' + data,
    method: 'get',
  })
}
