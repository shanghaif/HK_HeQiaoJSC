import axios from '@/libs/api.request'

export const getLogList = (data) => {
  return axios.request({
    url: 'rbac/systemlog/LogList',
    method: 'post',
    data
  })
}