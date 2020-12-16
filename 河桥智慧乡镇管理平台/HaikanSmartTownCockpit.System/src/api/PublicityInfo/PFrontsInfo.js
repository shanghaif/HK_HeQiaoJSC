import axios from '@/libs/api.request'

export const getPFrontsInfoList = (data) => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/list',
    method: 'post',
    data
  })
}
// createPFrontsInfo
export const createPFrontsInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/create',
    method: 'post',
    data
  })
}

//loadPFrontsInfo
export const loadPFrontsInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editPFrontsInfo
export const editPFrontsInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deletePFrontsInfo = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/batch',
    method: 'get',
    params: data
  })
}


//获取阵地类型下拉框
export const zhnedilx = () => {
  return axios.request({
    url: 'PublicityInfo/PFrontsInfo/zhnedilx',
    method: 'get'
  })
}

