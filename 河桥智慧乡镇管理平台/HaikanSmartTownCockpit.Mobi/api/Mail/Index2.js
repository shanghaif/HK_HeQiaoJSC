import http from '@/components/utils/http.js'

//查询数据
export const getproductList=(data)=>{
	return http.httpTokenRequest({
		url:'api/v1/Work/WorkList/GetList2/'+data,
		method:'get',
		})
}