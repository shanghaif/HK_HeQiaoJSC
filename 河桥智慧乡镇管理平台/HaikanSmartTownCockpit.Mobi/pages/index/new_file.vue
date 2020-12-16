<template>
</template>

<script>
	import {
		getuserinfo
	} from '@/api/taskandapi/taskapis.js'
	export default {
		data() {
			return {}
		},
		onLoad() {
			// console.log("加载钉钉方法")
			// //钉钉接口方法dd.getAuthCode获取授权码
			// dd.getAuthCode({
			// 	success: (res) => {
			// 		var code = res.authCode; //授权码
			// 		console.log("很高兴获得了授权请求码：", code);
			// 		getuserinfo(code).then(res => {
			// 			console.log("日志信息：");
			// 			console.log(res);
			// 			getApp().globalData.UserUUId = res.data.data[0].useruuid; //用户uuid
			// 			getApp().globalData.UserId = res.data.data[0].userid; //用户id
			// 			getApp().globalData.UserName = res.data.data[0].name; //用户姓名
			// 			console.log(getApp().globalData.UserUUId)
			// 			console.log("当前用户姓名")
			// 			console.log(getApp().globalData.UserUUId)
			// 		})
			// 	}
			// });
		},
		methods: {

		}
	}
</script>

<style>
</style>
