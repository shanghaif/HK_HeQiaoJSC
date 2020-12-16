import axios from '@/libs/api.request'

export const getRescueteamList = (data) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/list',
    method: 'post',
    data
  })
}
// createRescueteam
export const createRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/create',
    method: 'post',
    data
  })
}

//loadRescueteam
export const loadRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/edit/' + data.guid,
    method: 'get'
  })
}

// editRescueteam
export const editRescueteam = (data) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteRescueteam = (ids) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HqCommunaImport = (data) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/HqCommunaImport',
    method: 'post',
    data
  })
}

//导出
export const HqCommunaExport = (ids) => {
  return axios.request({
    url: 'socialgovernance/smokeInduction/ExportPass?ids=' + ids,
    method: 'get'
  })
}

