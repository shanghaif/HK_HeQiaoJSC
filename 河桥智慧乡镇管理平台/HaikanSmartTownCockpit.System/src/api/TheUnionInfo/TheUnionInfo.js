import axios from '@/libs/api.request'

export const getPromoInfoList = (data) => {
  return axios.request({
    url: 'TheUnionInfo/TheUnionInfo/list',
    method: 'post',
    data
  })
}

// createPromoInfo
export const createPromoInfo = (data) => {
  return axios.request({
    url: 'TheUnionInfo/TheUnionInfo/create',
    method: 'post',
    data
  })
}

//loadPromoInfo
export const loadPromoInfo = (data) => {
  return axios.request({
    url: 'TheUnionInfo/TheUnionInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editPromoInfo
export const editPromoInfo = (data) => {
  return axios.request({
    url: 'TheUnionInfo/TheUnionInfo/edit',
    method: 'post',
    data
  })
}

// delete user
export const deletePromoInfo = (ids) => {
  return axios.request({
    url: 'TheUnionInfo/TheUnionInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'TheUnionInfo/TheUnionInfo/batch',
    method: 'get',
    params: data
  })
}




