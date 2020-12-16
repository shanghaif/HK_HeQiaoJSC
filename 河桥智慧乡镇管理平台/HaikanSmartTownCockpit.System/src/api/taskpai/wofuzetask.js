import axios from '@/libs/api.request'

//查询数据
export const getdatalist = (data) => {
  return axios.request({
    url: 'task/wofuzutask/list',
    method: 'post',
    data
  })
}


//查询数据(科室工作总结)
export const keshizongjie = (data) => {
  return axios.request({
    url: 'task/wofuzutask/keshizongjies',
    method: 'post',
    data
  })
}



//完成
export const wanchen = (data) => {
  return axios.request({
    url: 'task/wofuzutask/wanchen',
    method: 'post',
    data
  })
}

//推送消息
export const notes = (data) => {
  return axios.request({
    url: 'task/wofuzutask/notes/'+data.uuid,
    method: 'get',
  })
}

//修改权限
export const editpower = (data) => {
  console.log(data.guid);
  return axios.request({
    url: 'task/wofuzutask/editpowers/'+data.guid,
    method: 'get',
  })
}


//添加汇报
export const createhuibao = (data) => {
  return axios.request({
      url: 'task/wofuzutask/createhuibao',
      method: 'post',
      data
  })
}


//根据任务绑定科室
export const depdata = (data) => {
  return axios.request({
    url: 'task/wofuzutask/depdata/'+data.uuid,
    method: 'get',
  })
}

//根据任务绑定科室
export const userdata = (data) => {
  return axios.request({
    url: 'task/wofuzutask/userdata/',
    method: 'get',
    params: data
  })
}

//根据任务绑定科室
export const selecehuibao = (data) => {
  return axios.request({
    url: 'task/wofuzutask/selecehuibao/',
    method: 'get',
    params: data
  })
}


//查看汇报详细
export const hblooks = (data) => {
  return axios.request({
    url: 'task/wofuzutask/hblooks/'+data.uuid,
    method: 'get',
  })
}











