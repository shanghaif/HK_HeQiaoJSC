import axios from '@/libs/api.request'

export const getPromoTeamInfoList = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/list',
    method: 'post',
    data
  })
}
// createPromoTeamInfo
export const createPromoTeamInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/create',
    method: 'post',
    data
  })
}

//loadPromoTeamInfo
export const loadPromoTeamInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/EditGet/' + data.guid,
    method: 'get'
  })
}

// editPromoTeamInfo
export const editPromoTeamInfo = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deletePromoTeamInfo = (ids) => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/batch',
    method: 'get',
    params: data
  })
}


//获取阵地类型下拉框
export const zhnedilx = () => {
  return axios.request({
    url: 'PublicityInfo/PromoTeamInfo/zhnedilx',
    method: 'get'
  })
}

