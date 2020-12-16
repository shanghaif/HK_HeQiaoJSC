import axios from '@/libs/api.request'

export const GetList = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/List',
    method: 'post',
    data
  })
}

export const xiangList = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/xiangList',
    method: 'post',
    data
  })
}

export const GetCreate = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/Create',
    method: 'post',
    data
  })
}

export const GetEdit = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/Edit',
    method: 'post',
    data
  })
}

export const GetShow = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/Show?guid=' + data,
    method: 'get',
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/batch',
    method: 'get',
    params: data
  })
}

//导入
export const GetImport = (data) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/Import',
    method: 'post',
    data
  })
}

//导出
export const GetExportPass = (ids) => {
  return axios.request({
    url: 'DangJibenInfo/DangOrganization/ExportPass?ids=' + ids,
    method: 'get'
  })
}

export const oGetList = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/oList',
      method: 'post',
      data
    })
  }
  
  export const oGetCreate = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/oCreate',
      method: 'post',
      data
    })
  }
  
  export const oGetEdit = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/oEdit',
      method: 'post',
      data
    })
  }
  
  export const oGetShow = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/oShow?guid=' + data,
      method: 'get',
    })
  }
  
  // delete department
  export const odeleteDepartment = (ids) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/odelete/' + ids,
      method: 'get'
    })
  }
  
  // batch command
  export const obatchCommand = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/obatch',
      method: 'get',
      params: data
    })
  }
  
  //导入
  export const oGetImport = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/oImport',
      method: 'post',
      data
    })
  }
  
  //导出
  export const oGetExportPass = (data) => {
    return axios.request({
      url: 'DangJibenInfo/DangOrganization/oExportPass?ids=' + data.ids+"&guid="+data.guid,
      method: 'get'
    })
  }