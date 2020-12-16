import http from '@/components/utils/http.js'


//查询数据
export const selectcount = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectcount/'+data.userid,
    method: 'get',
  })
}

//负责人任务排行统计
export const selectfuzepaihangapp = () => {
  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectfuzepaihangapp',
    method: 'get',
  })
}
//参与人任务排行统计
export const selectcanyupaihangapp = () => {
  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectcanyupaihangapp',
    method: 'get',
  })
}



//任务进展排行统计详情(升序)
export const selectjinzhan = () => {
  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectjinzhan',
    method: 'get',
  })
}

//任务进展排行统计详情(降序)
export const selectjinzhandesc = () => {
  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectjinzhandesc',
    method: 'get',
  })
}


//任务参与进展排行统计详情(升序)
export const selectmissionscanyu = () => {

  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectmissionscanyu',
    method: 'get',
  })
}

//任务参与进展排行统计详情(降序)
export const selectmissionscanyudesc = () => {

  return http.httpTokenRequest({
    url: 'api/v1/statistics/statisticsapp/selectmissionscanyudesc',
    method: 'get',
  })
}