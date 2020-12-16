import axios from '@/libs/api.request'

//列表
export const RuzhuList = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/RuzhuList',
    method: 'post',
    data
  })
}
 
//添加
export const RuzhuCreate = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/RuzhuCreate',
    method: 'post',
    data
  })
}

//获取数据
export const HoruzhuGet = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/HoruzhuGet',
    method: 'post',
    data
  })
}
//获取数据
export const RuzhufoGet = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/RuzhufoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const RuzhuEdit = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/RuzhuEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/batch',
    method: 'get',
    params: data
  })
} 

//导入
export const RuzhuImport = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/RuzhuImport',
    method: 'post',
    data
  })
}

//导出
export const RuzhuExport = (data) => {
  return axios.request({
    url: 'Hotelinfo/Hoteltongji/ExportPass?ids=' + data.ids  + '&htname=' + data.htname,
    method: 'get'
  })
}





