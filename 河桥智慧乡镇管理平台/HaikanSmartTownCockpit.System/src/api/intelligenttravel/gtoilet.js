import axios from '@/libs/api.request'

export const getGtoiletList = (data) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/list',
    method: 'post',
    data
  })
}
// createGtoilet
export const createGtoilet = (data) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/create',
    method: 'post',
    data
  })
}

//loadGtoilet
export const loadGtoilet = (data) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/edit/' + data.guid,
    method: 'get'
  })
}

// editGtoilet
export const editGtoilet = (data) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteGtoilet = (ids) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/batch',
    method: 'get',
    params: data
  })
}
//导入
export const GtoiletImport = (data) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/gtoiletimport',
    method: 'post',
    data
  })
}

//导出
export const GtoiletExport = (ids) => {
  return axios.request({
    url: 'intelligenttravel/gtoilet/gtoiletexport?ids=' + ids,
    method: 'get'
  })
}
