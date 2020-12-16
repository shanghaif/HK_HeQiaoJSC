import axios from '@/libs/api.request'

export const getDecompositionList = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/list',
    method: 'post',
    data
  })
}

export const getDecompInfoList = (data) => {
    return axios.request({
      url: 'emergency/Decomposition/DecompInfo',
      method: 'post',
      data
    })
  }


// createDecomposition
export const createDecompInfo = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/InfoCreate',
    method: 'post',
    data
  })
}

//loadDecomposition
export const loadDecomposition = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/Load?guid=' + data,
    method: 'get'
  })
}

export const loadDecompInfo = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/InfoLoad?guid=' + data,
    method: 'get'
  })
}

// editDecomposition
export const editDecomposition = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/Edit',
    method: 'post',
    data
  })
}

export const editDecompInfo = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/InfoEdit',
    method: 'post',
    data
  })
}
// delete user
export const deleteDecompInfo = (ids) => {
  return axios.request({
    url: 'emergency/Decomposition/InfoDelete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchDecompInfoCommand = (data) => {
  return axios.request({
    url: 'emergency/Decomposition/InfoBatch',
    method: 'get',
    params: data
  })
}

//导入
// export const DecompositionImport = (data) => {
//   return axios.request({
//     url: 'emergency/Decomposition/Decompositionimport',
//     method: 'post',
//     data
//   })
// }

// //导出
// export const DecompositionExport = (ids) => {
//   return axios.request({
//     url: 'emergency/Decomposition/Decompositionexport?ids=' + ids,
//     method: 'get'
//   })
// }


