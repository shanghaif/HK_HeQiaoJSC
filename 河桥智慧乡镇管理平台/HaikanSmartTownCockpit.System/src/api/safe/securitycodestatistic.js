import axios from '@/libs/api.request'

export const getSecurityCodeStatisticList = (data) => {
  return axios.request({
    url: 'safe/securitycodestatistic/list',
    method: 'post',
    data
  })
}
export const getSecuritycodeList = (data) => {
  return axios.request({
    url: 'safe/securitycodestatistic/numberlist',
    method: 'post',
    data
  })
}
