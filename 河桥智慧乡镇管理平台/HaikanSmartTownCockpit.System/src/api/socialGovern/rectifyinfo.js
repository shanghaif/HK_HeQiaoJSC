import axios from '@/libs/api.request'

export const getRectifyInfoList = (data) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/list',
    method: 'post',
    data
  })
}
// createRectifyInfo
export const createRectifyInfo = (data) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/create',
    method: 'post',
    data
  })
}

//loadRectifyInfo
export const loadRectifyInfo = (data) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/edit/' + data.guid,
    method: 'get'
  })
}

// editRectifyInfo
export const editRectifyInfo = (data) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteRectifyInfo = (ids) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/batch',
    method: 'get',
    params: data
  })
}
//导入
export const RectifyInfoImport = (data) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/rectifyinfoimport',
    method: 'post',
    data
  })
}

//导出
export const RectifyInfoExport = (ids) => {
  return axios.request({
    url: 'socialgovern/rectifyinfo/rectifyinfoexport?ids=' + ids,
    method: 'get'
  })
}
