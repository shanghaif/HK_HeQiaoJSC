import axios from '@/libs/api.request'


export const SecList = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/List',
    method: 'post',
    data
  })
}

export const SecCreate = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Create',
    method: 'post',
    data
  })
}

export const SecEdit = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Edit',
    method: 'post',
    data
  })
}

export const SecLoad = (guid) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Load/'+guid,
    method: 'get',
  })
}

export const SecDelete = (ids) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/delete/' + ids,
    method: 'get'
  })
}



// batch command
export const SecBatchCommand = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/batch',
    method: 'get',
    params: data
  })
}

//导入
export const SecImport = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Import',
    method: 'post',
    data
  })
}

//导出
export const SecExport = (ids) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/ExportPass?ids=' + ids,
    method: 'get'
  })
}




export const SerList = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/List2',
    method: 'post',
    data
  })
}

export const SerCreate = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Create2',
    method: 'post',
    data
  })
}

export const SerEdit = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Edit2',
    method: 'post',
    data
  })
}

export const SerLoad = (guid) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/Load2/'+guid,
    method: 'get',
  })
}

export const SerDelete = (ids) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/delete2/' + ids,
    method: 'get'
  })
}



// batch command
export const SerBatchCommand = (data) => {
  return axios.request({
    url: 'SocialGovern/SecurityPerson/batch2',
    method: 'get',
    params: data
  })
}
