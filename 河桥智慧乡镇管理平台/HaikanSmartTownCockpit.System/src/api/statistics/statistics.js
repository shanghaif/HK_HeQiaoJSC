import axios from '@/libs/api.request'

//查询数据
export const selectcount = (data) => {
  return axios.request({
    url: 'statistics/statistics/selectcount/'+data.userid,
    method: 'get',
  })
}

//参与人任务排行统计
export const selectcanyupaihang = () => {
  return axios.request({
    url: 'statistics/statistics/selectcanyupaihang',
    method: 'get',
  })
}

//参与人任务排行统计
export const selectcanyupaihangd = (data) => {
  return axios.request({
    url: 'statistics/statistics/selectcanyupaihangd/'+data.date,
    method: 'get',
  })
}


//负责人任务排行统计
export const selectfuzepaihang = () => {
  return axios.request({
    url: 'statistics/statistics/selectfuzepaihang',
    method: 'get',
  })
}

//负责人任务排行统计
export const selectfuzepaihangd = (data) => {
  return axios.request({
    url: 'statistics/statistics/selectfuzepaihangd/'+data.date,
    method: 'get',
  })
}


//任务进展排行统计详情
export const selectjinzhan = (data) => {
  return axios.request({
    url: 'statistics/statistics/selectjinzhan/'+data.order,
    method: 'get',
  })
}