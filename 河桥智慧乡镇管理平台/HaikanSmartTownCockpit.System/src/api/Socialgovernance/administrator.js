import axios from '@/libs/api.request'

export const getRescueteamList = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/list',
    method: 'post',
    data
  })
}
// createRescueteam
export const createRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/create',
    method: 'post',
    data
  })
}

//loadRescueteam
export const loadRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/edit/' + data.guid,
    method: 'get'
  })
}

// editRescueteam
export const editRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteRescueteam = (ids) => {
  return axios.request({
    url: 'socialgovernance/administrator/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HqCommunaImport = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/HqCommunaImport',
    method: 'post',
    data
  })
}

//导出
export const HqCommunaExport = (ids) => {
  return axios.request({
    url: 'socialgovernance/administrator/ExportPass?ids=' + ids,
    method: 'get'
  })
}

//获取数据
export const rentalHousfoGet = (data) => {
  return axios.request({
    url: 'socialgovernance/administrator/AdminfoGet?guid=' + data,
    method: 'get',
  })
}
