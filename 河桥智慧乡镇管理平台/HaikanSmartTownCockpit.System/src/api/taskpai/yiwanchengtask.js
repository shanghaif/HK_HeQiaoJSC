import axios from '@/libs/api.request'

//查询数据
export const getdatalist = (data) => {
  return axios.request({
    url: 'task/yiwanchengtask/list',
    method: 'post',
    data
  })
}

//重启任务
export const chongqitasktrue = (data) => {
  return axios.request({
    url: 'task/yiwanchengtask/chongqitasktrue',
    method: 'post',
    data
  })
}