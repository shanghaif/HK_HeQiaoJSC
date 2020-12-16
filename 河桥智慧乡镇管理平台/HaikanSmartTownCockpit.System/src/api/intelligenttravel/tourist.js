import axios from '@/libs/api.request'

export const getTouristList = (data) => {
  return axios.request({
    url: 'intelligenttravel/tourist/list',
    method: 'post',
    data
  })
}
