import axios from '@/libs/api.request'

export const getCityManagementList = (data) => {
  return axios.request({
    url: 'socialgovern/citymanagement/list',
    method: 'post',
    data
  })
}
// createCityManagement
export const createCityManagement = (data) => {
  return axios.request({
    url: 'socialgovern/citymanagement/create',
    method: 'post',
    data
  })
}

//loadCityManagement
export const loadCityManagement = (data) => {
  return axios.request({
    url: 'socialgovern/citymanagement/edit/' + data.guid,
    method: 'get'
  })
}

// editCityManagement
export const editCityManagement = (data) => {
  return axios.request({
    url: 'socialgovern/citymanagement/edit',
    method: 'post',
    data
  })
}
// delete
export const deleteCityManagement = (ids) => {
  return axios.request({
    url: 'socialgovern/citymanagement/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'socialgovern/citymanagement/batch',
    method: 'get',
    params: data
  })
}
//导入
export const CityManagementImport = (data) => {
  return axios.request({
    url: 'socialgovern/citymanagement/citymanagementimport',
    method: 'post',
    data
  })
}

//导出
export const CityManagementExport = (ids) => {
  return axios.request({
    url: 'socialgovern/citymanagement/citymanagementexport?ids=' + ids,
    method: 'get'
  })
}
