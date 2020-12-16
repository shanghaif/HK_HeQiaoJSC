import axios from '@/libs/api.request'

export const getPromoInfoList = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoInfo/list',
    method: 'post',
    data
  })
}
// createPromoInfo
export const createPromoInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoInfo/create',
    method: 'post',
    data
  })
}

//loadPromoInfo
export const loadPromoInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editPromoInfo
export const editPromoInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deletePromoInfo = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PromoInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoInfo/batch',
    method: 'get',
    params: data
  })
}




