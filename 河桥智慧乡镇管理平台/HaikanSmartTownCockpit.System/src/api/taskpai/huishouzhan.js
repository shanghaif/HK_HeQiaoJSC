import axios from '@/libs/api.request'

//查询数据
export const getdatalist = (data) => {
  return axios.request({
    url: 'task/recycletask/list',
    method: 'post',
    data
  })
}


//重启任务
export const chognqitask = (data) => {
  return axios.request({
    url: 'task/recycletask/chongqitask',
    method: 'post',
    data
  })
}

//删除数据(真删)
export const deletetrue = (data) => {
  return axios.request({
    url: 'task/recycletask/Deletetrue/'+data.ids,
    method: 'get'
  })
}