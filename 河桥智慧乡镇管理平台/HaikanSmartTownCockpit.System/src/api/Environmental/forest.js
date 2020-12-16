import axios from '@/libs/api.request'

export const getRescueteamList = (data) => {
  return axios.request({
    url: 'environmental/forest/list',
    method: 'post',
    data
  })
}
// createRescueteam
export const createRescueteam = (data) => {
  return axios.request({
    url: 'environmental/forest/create',
    method: 'post',
    data
  })
}

//loadRescueteam
export const loadRescueteam = (data) => {
  return axios.request({
    url: 'environmental/forest/edit/' + data.guid,
    method: 'get'
  })
}

// editRescueteam
export const editRescueteam = (data) => {
  return axios.request({
    url: 'environmental/forest/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteRescueteam = (ids) => {
  return axios.request({
    url: 'environmental/forest/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'environmental/forest/batch',
    method: 'get',
    params: data
  })
}

//导入
export const HqCommunaImport = (data) => {
  // Console.log(165);
  return axios.request({
    url: 'environmental/forest/ForestImpor',
    method: 'post',
    data
  })
}

//导出
export const HqCommunaExport = (ids) => {
  return axios.request({
    url: 'environmental/forest/ExportPass?ids=' + ids,
    method: 'get'
  })
}

