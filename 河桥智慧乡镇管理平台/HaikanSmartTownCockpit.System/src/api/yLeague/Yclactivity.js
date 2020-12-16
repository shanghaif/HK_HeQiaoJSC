import axios from '@/libs/api.request'

export const getPromoInfoList = (data) => {
  return axios.request({
    url: 'YouthLeague/yclActivityInfo/list',
    method: 'post',
    data
  })
}
// createPromoInfo
export const createPromoInfo = (data) => {
  return axios.request({
    url: 'YouthLeague/yclActivityInfo/create',
    method: 'post',
    data
  })
}

//loadPromoInfo
export const loadPromoInfo = (data) => {
  return axios.request({
    url: 'YouthLeague/yclActivityInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editPromoInfo
export const editPromoInfo = (data) => {
  return axios.request({
    url: 'YouthLeague/yclActivityInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deletePromoInfo = (ids) => {
  return axios.request({
    url: 'YouthLeague/yclActivityInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'YouthLeague/yclActivityInfo/batch',
    method: 'get',
    params: data
  })
}




