import http from '@/components/utils/http.js'

//查询数据
// export const getproductList = (data) => {
// 	return http.httpTokenRequest({
// 		url: 'api/v1/Work/WorkList/GetList/' + data,
// 		method: 'get',
// 	})
// }
//查询数据
export const GetPriorityDetial = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Work/WorkList/GetPriorityDetial/' + data,
		method: 'get',
	})
}
//修改数据
export const editDetial = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Work/WorkList/editDetial/' + data,
		method: 'get',
	})
}
export const getproductList = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Work/WorkList/GetList',
		method: 'post',
	}, data)
}
