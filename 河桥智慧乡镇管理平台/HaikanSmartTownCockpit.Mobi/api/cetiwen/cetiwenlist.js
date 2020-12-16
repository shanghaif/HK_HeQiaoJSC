import http from '@/components/utils/http.js'

export const getuserinfos = (data) => {
	console.log(11111);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserinfos/' + data,
		method: 'Get'
	})
}
export const getdepartlist = () => {
	console.log(22222);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getdepartlist',
		method: 'Get'
	})
}

export const getdepartlists = (data) => {
	console.log(22222);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getdepartlists/' + data,
		method: 'Get'
	})
}

export const getdepartlistsss = (data) => {
	console.log(22222);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getdepartlistsss/' + data,
		method: 'Get'
	})
}

export const getuserlist = (data) => {
	console.log(333);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserlist/' + data,
		method: 'Get'
	})
}

export const getuserlists = (data) => {
        console.log(44);
        return http.httpTokenRequest({
                url: 'api/v1/task/app/taskapp/getuserlists/',
                method: 'Post'
        },data)
}
export const getuserlistss = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserlistss/' + data,
		method: 'Get'
	})
}
export const getuserlistss2 = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getusername/' + data.id,
		method: 'Get'
	})
}
export const getuserprioritylist = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserprioritylist/' + data,
		method: 'Get'
	})
}

export const getuserprioritylist2 = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserprioritylist2/' + data,
		method: 'Get'
	})
}

export const getuserzongjie = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserzongjie/' + data,
		method: 'Get'
	})
}
export const getuserzongjiePriority = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserzongjiePriority/' + data,
		method: 'Get'
	})
}


//通过工作uuid获取工作信息
export const gongzuozongjie = (data) => {
	console.log(44);
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/gongzuozongjie/' + data.uuid,
		method: 'Get'
	})
}






