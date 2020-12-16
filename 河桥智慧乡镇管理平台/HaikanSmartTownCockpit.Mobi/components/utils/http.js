//const baseUrl = 'http://192.168.0.221:4441/';
const baseUrl = 'http://localhost:54321/'
// const baseUrl = 'http://haikan.51vip.biz/';
// const baseUrl = 'https://rwgj.api.haikan.com.cn/';
//const baseUrl = 'https://rwgj.hkwqr.com/';
// const baseUrl = 'http://112.17.130.233:4474/';

const httpRequest = (opts, data) => {
	let httpDefaultOpts = {
		url: baseUrl + opts.url,
		data: data,
		method: opts.method,
		header: opts.method == 'get' ? {
			'X-Requested-With': 'XMLHttpRequest',
			"Accept": "application/json",
			"Content-Type": "application/json; charset=UTF-8"
		} : {
			'X-Requested-With': 'XMLHttpRequest',
			'Content-Type': 'application/json; charset=UTF-8'
		},
		dataType: 'json',
	}
	let promise = new Promise(function(resolve, reject) {
		uni.request(httpDefaultOpts).then(
			(res) => {
				resolve(res[1])
			}
		).catch(
			(response) => {
				reject(response)
			}
		)
	})
	return promise
};
//带Token请求
const httpTokenRequest = (opts, data) => {
	let token = "";
	// uni.getStorage({
	// key: 'token',
	// success: function(ress) {
	// token = ress.data
	// }
	// });
	try {
		token = uni.getStorageSync('token');
	} catch (e) {
		//TODO handle the exception
	};
	//此token是登录成功后后台返回保存在storage中的
	let httpDefaultOpts = {
		url: baseUrl + opts.url,
		data: data,
		method: opts.method,
		header: opts.method == 'get' ? {
			'Authorization': 'Bearer ' + token,
			'X-Requested-With': 'XMLHttpRequest',
			"Accept": "application/json",
			"Content-Type": "application/json; charset=UTF-8"
		} : {
			'Authorization': 'Bearer ' + token,
			'X-Requested-With': 'XMLHttpRequest',
			'Content-Type': 'application/json; charset=UTF-8'
		},
		dataType: 'json',
	}
	let promise = new Promise(function(resolve, reject) {
		uni.request(httpDefaultOpts).then(
			(res) => {
				resolve(res[1])
			}
		).catch(
			(response) => {
				reject(response)
			}
		)
	})
	return promise
};

export default {
	baseUrl,
	httpRequest,
	httpTokenRequest
}
