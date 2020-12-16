import axios from '@/libs/api.request'

export const getPromoInfoList = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenActivitiesInfo/list',
    method: 'post',
    data
  })
}
// createPromoInfo
export const createPromoInfo = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenActivitiesInfo/create',
    method: 'post',
    data
  })
}

//loadPromoInfo
export const loadPromoInfo = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenActivitiesInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editPromoInfo
export const editPromoInfo = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenActivitiesInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deletePromoInfo = (ids) => {
  return axios.request({
    url: 'FuLianInfo/WomenActivitiesInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'FuLianInfo/WomenActivitiesInfo/batch',
    method: 'get',
    params: data
  })
}




