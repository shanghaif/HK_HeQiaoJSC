import http from '@/components/utils/http.js'

//提交重点工作
export const createPriority = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/priorityAdd/app/priorityapp/create',
		method: 'post'},
		data
	)
}
//查询出所有用户
export const gealluser = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/priorityAdd/app/priorityapp/gealluser/'+data.type,
    method: 'get',
  })
}
export const taskwhere = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/priorityAdd/app/priorityapp/prioritybyguid/'+data.type,
		method: 'get',
	})
}