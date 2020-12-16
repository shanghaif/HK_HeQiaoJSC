import axios from '@/libs/api.request'

export const getConsumableList = (data) => {
  return axios.request({
    url: 'material/consumable/list',
    method: 'post',
    data
  })
}
// createDepartment
export const createConsumable = (data) => {
  return axios.request({
    url: 'material/consumable/create',
    method: 'post',
    data
  })
}

//loadDepartment
export const loadConsumable = (data) => {
  return axios.request({
    url: 'material/consumable/edit/' + data.guid,
    method: 'get'
  })
}

// editDepartment
export const editConsumable = (data) => {
  return axios.request({
    url: 'material/consumable/edit',
    method: 'post',
    data
  })
}
// delete department
export const deleteConsumable = (ids) => {
  return axios.request({
    url: 'material/consumable/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'material/consumable/batch',
    method: 'get',
    params: data
  })
}
