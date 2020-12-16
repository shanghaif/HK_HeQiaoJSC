import axios from '@/libs/api.request'

//列表
export const GetList = (data) => {
  return axios.request({
    url: 'JingdianInfo/lvyoutongji/GetList',
    method: 'post',
    data
  })
}
 

//导出
export const GetExport = (ids) => {
  return axios.request({
    url: 'JingdianInfo/lvyoutongji/ExportPass?ids=' + ids,
    method: 'get'
  })
}



