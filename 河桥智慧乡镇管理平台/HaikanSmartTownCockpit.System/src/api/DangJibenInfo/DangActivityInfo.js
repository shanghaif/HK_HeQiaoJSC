import axios from '@/libs/api.request'

export const getPromoInfoList = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangActivityInfo/list',
    method: 'post',
    data
  })
}
// createPromoInfo
export const createPromoInfo = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangActivityInfo/create',
    method: 'post',
    data
  })
}

//loadPromoInfo
export const loadPromoInfo = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangActivityInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editPromoInfo
export const editPromoInfo = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangActivityInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deletePromoInfo = (ids) => {
  return axios.request({
    url: 'DangJibenInfo/DangActivityInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangActivityInfo/batch',
    method: 'get',
    params: data
  })
}




