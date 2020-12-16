import axios from '@/libs/api.request'

//查询数据
export const getdatalist = (data) => {
  return axios.request({
    url: 'task/caogaotask/list',
    method: 'post',
    data
  })
}

//添加到草稿箱
export const createcaogao = (data) => {
  return axios.request({
    url: 'task/caogaotask/create',
    method: 'post',
    data
  })
}


//编辑后保存到草稿箱
export const baocuneditcaogao = (data) => {
  return axios.request({
    url: 'task/caogaotask/baocunEdit',
    method: 'post',
    data
  })
}