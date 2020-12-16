import http from '@/components/utils/http.js'

//提交个人日志
export const createPersonal = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/Create',
		method: 'post'},
		data
	)
}

export const createPersonal2 = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/Create2',
		method: 'post'},
		data
	)
}

export const createPersonal3 = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/Create3',
		method: 'post'},
		data
	)
}

//根据登录信息绑定科室
export const depdata = () => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/depdata',
		method: 'get',
		})
}

//根据科室绑定人员信息
export const userdatalist = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/userdatalist/'+data,
		method: 'get',
		})
}


//根据选择人员加载日志
export const personlist = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/personlist/'+data,
		method: 'get',
		})
}


//查询
export const getdatalist = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/Personal/app/PersonalDiaryApp/getdatalist',
    method: 'post',
  },data)
}


//根据日志uuid获取日志信息
export const persondata = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/Personal/app/PersonalDiaryApp/persondata/'+data.uuid,
		method: 'get',
		})
}



