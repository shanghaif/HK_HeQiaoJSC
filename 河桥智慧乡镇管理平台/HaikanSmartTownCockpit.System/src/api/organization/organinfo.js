import axios from '@/libs/api.request'

export const GetList = (data) => {
  return axios.request({
    url: 'Organization/OrganInfo/List',
    method: 'post',
    data
  })
}

export const GetCreate = (data) => {
  return axios.request({
    url: 'Organization/OrganInfo/Create',
    method: 'post',
    data
  })
}

export const GetEdit = (data) => {
  return axios.request({
    url: 'Organization/OrganInfo/Edit',
    method: 'post',
    data
  })
}

export const GetShow = (data) => {
  return axios.request({
    url: 'Organization/OrganInfo/Show?guid=' + data,
    method: 'get',
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'Organization/OrganInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'Organization/OrganInfo/batch',
    method: 'get',
    params: data
  })
}

//导入
export const GetImport = (data) => {
  return axios.request({
    url: 'Organization/OrganInfo/Import',
    method: 'post',
    data
  })
}

//导出
export const GetExportPass = (ids) => {
  return axios.request({
    url: 'Organization/OrganInfo/ExportPass?ids=' + ids,
    method: 'get'
  })
}

export const oGetList = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/oList',
      method: 'post',
      data
    })
  }
  
  export const oGetCreate = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/oCreate',
      method: 'post',
      data
    })
  }
  
  export const oGetEdit = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/oEdit',
      method: 'post',
      data
    })
  }
  
  export const oGetShow = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/oShow?guid=' + data,
      method: 'get',
    })
  }
  
  // delete department
  export const odeleteDepartment = (ids) => {
    return axios.request({
      url: 'Organization/OrganInfo/odelete/' + ids,
      method: 'get'
    })
  }
  
  // batch command
  export const obatchCommand = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/obatch',
      method: 'get',
      params: data
    })
  }
  
  //导入
  export const oGetImport = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/oImport',
      method: 'post',
      data
    })
  }
  
  //导出
  export const oGetExportPass = (data) => {
    return axios.request({
      url: 'Organization/OrganInfo/oExportPass?ids=' + data.ids+"&guid="+data.guid,
      method: 'get'
    })
  }