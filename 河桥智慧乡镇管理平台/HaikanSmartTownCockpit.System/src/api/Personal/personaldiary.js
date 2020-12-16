import axios from '@/libs/api.request'

//查询数据列表
export const getRoleList = (data) => {
  return axios.request({
    url: 'personal/personalDiary/list',
    method: 'post',
    data
  })
}

// 创建个人日志
export const createRole = (data) => {
  return axios.request({
    url: 'personal/personalDiary/create',
    method: 'post',
    data
  })
}

//编辑个人日志
export const loadRole = (data) => {
  return axios.request({
    url: 'personal/personalDiary/edit/' + data.guid,
    method: 'get'
  })
}

// 保存编辑后的个人日志
export const editRole = (data) => {
  return axios.request({
    url: 'personal/personalDiary/edit',
    method: 'post',
    data
  })
}

// 删除个人日志
export const deleteRole = (ids) => {
  return axios.request({
    url: 'personal/personalDiary/delete/' + ids,
    method: 'get'
  })
}

// 批量操作
export const batchCommand = (data) => {
  return axios.request({
    url: 'personal/personalDiary/batch',
    method: 'get',
    params: data
  })
}

// 上传附件
export const RegistPicture = (data) => {
  return axios.request({
    url: 'personal/personalDiary/RegistPicture',
    method: 'post',
    data
  })
}


