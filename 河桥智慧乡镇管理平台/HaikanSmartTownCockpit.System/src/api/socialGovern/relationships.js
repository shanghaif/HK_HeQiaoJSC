import axios from '@/libs/api.request'

// createRelationships
export const createRelationships= (data) => {
  return axios.request({
    url: 'socialgovern/relationships/create',
    method: 'post',
    data
  })
}

//loadRelationships
export const loadRelationships = () => {
  return axios.request({
    url: 'socialgovern/relationships/LoadRelationships',
    method: 'get'
  })
}




