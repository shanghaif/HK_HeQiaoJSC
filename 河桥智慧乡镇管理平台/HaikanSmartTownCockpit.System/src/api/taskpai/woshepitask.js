import axios from '@/libs/api.request'

//查询数据
export const getdatalist = (data) => {
  return axios.request({
    url: 'task/woshenpitask/list',
    method: 'post',
    data
  })
}

//审批通过
export const shenhe = (data) => {
  return axios.request({
    url: 'task/woshenpitask/shenhe',
    method: 'post',
    data
  })
}

//代办
export const daiban = (data) => {
  return axios.request({
    url: 'task/woshenpitask/daiban',
    method: 'post',
    data
  })
}


//审批不通过
export const shenheontongguo = (data) => {
  return axios.request({
    url: 'task/woshenpitask/shenheontongguo',
    method: 'post',
    data
  })
}