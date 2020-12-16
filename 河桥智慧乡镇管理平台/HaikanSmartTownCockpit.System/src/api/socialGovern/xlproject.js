import axios from '@/libs/api.request'

export const getXlProjectList = (data) => {
  return axios.request({
    url: 'socialgovern/xlproject/list',
    method: 'post',
    data
  })
}
// createXlProject
export const createXlProject = (data) => {
  return axios.request({
    url: 'socialgovern/xlproject/create',
    method: 'post',
    data
  })
}

//loadXlProject
export const loadXlProject = (data) => {
  return axios.request({
    url: 'socialgovern/xlproject/edit/' + data.guid,
    method: 'get'
  })
}

// editXlProject
export const editXlProject = (data) => {
  return axios.request({
    url: 'socialgovern/xlproject/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteXlProject = (ids) => {
  return axios.request({
    url: 'socialgovern/xlproject/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'socialgovern/xlproject/batch',
    method: 'get',
    params: data
  })
}
//导入
export const XlProjectImport = (data) => {
  return axios.request({
    url: 'socialgovern/xlproject/xlprojectimport',
    method: 'post',
    data
  })
}

//导出
export const XlProjectExport = (ids) => {
  return axios.request({
    url: 'socialgovern/xlproject/xlprojectexport?ids=' + ids,
    method: 'get'
  })
}
