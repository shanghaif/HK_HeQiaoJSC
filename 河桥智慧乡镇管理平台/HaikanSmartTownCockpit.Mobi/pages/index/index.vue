<template>
	<view>
		<Task v-if="PageCur=='Task'"></Task>
		<!-- <MessageAdd v-if="PageCur=='MessageAdd'"></MessageAdd> -->
		<Priority v-if="PageCur=='Priority'"></Priority>
		<TongJi v-if="PageCur=='TongJi'"></TongJi>
		<Personallog v-if="PageCur=='Personallog'"></Personallog>
		<view class="cu-bar tabbar bg-white shadow foot">
			<view class="action" @click="NavChange" data-cur="Priority">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/tabbar/component' + [PageCur=='component'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='Priority'?'text-green':'text-gray'">消息列表</view>
			</view>
			<view class="action" @click="NavChange" data-cur="Task">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/tabbar/plugin' + [PageCur == 'plugin'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='Task'?'text-green':'text-gray'">工作</view>
			</view>
			<view class="action" @click="NavChange" data-cur="Personallog">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/tabbar/about' + [PageCur == 'about'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='Personallog'?'text-green':'text-gray'">个人日志</view>
			</view>
			<view class="action" @click="NavChange2" data-cur="MessageAdd">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/tabbar/basics' + [PageCur == 'basics'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='MessageAdd'?'text-green':'text-gray'">添加</view>
			</view>
			<view class="action" @click="NavChange" data-cur="TongJi">
				<view class='cuIcon-cu-image'>
					<image :src="'/static/tabbar/about' + [PageCur == 'about'?'_cur':''] + '.png'"></image>
				</view>
				<view :class="PageCur=='TongJi'?'text-green':'text-gray'">统计</view>
			</view>
		</view>
	</view>
</template>
<script src="https://g.alicdn.com/gdt/jsapi/1.3.2/index.js"></script>
<script>
	import {
		getzzduserinfo
	} from '@/api/taskandapi/taskapis.js'
	import dd from 'gdt-jsapi';
	export default {
		data() {
			return {
				PageCur: 'Task',
			}
		},
		onLoad() {
			// let that = this;
			// 钉钉接口方法dd.getAuthCode获取授权码
			// dd.getAuthCode({
			// 	corpId: "39633"
			// }).then(res => {
			// 	console.log(res);
			// }).catch(err => {
			// 	console.log(err);
			// })
			// ddss.getAuthCode({
			// 	corpId: "39633"
			// }).then((result) => {
			// 	if (result) {
			// 		console.log(JSON.stringify(result, null, null));
			// 		// code: 'hYLK98jkf0m' //string authCode
			// 	}
			// })
			// my.getAuthCode({
			//   scopes: ['auth_base'],
			//   success: (res) => {
			//     my.alert({
			//       content: res.authCode,
			//     });
			//   },
			// });			
		},
		mounted() {
			// console.log("加载专有钉钉方法")
			// dd.getAuthCode({
			// 	corpId: "39633"
			// }).then(res => {
			// 	console.log(res);
			// 	console.log(res.code);
			// 	var code = res.code;
			// 	// getzzduserinfo(code).then(res => {
			// 	// 	console.log("日志信息：", res);
			// 	// 	getApp().globalData.UserUUId = res.data.data[0].useruuid; //用户uuid
			// 	// 	getApp().globalData.UserId = res.data.data[0].userid; //用户id
			// 	// 	getApp().globalData.UserName = res.data.data[0].name; //用户姓名
			// 	// 	console.log("当前用户姓名");
			// 	// 	console.log(getApp().globalData.UserName);
			// 	// })
			// }).catch(err => {
			// 	console.log(err);
			// })
		},
		methods: {
			NavChange: function(e) {

				this.PageCur = e.currentTarget.dataset.cur
			},
			NavChange2:function(){
				uni.navigateTo({
					url: '/pages/MessageAdd/TaskAdd2'
				})
			}
		}
	}
</script>

<style>
</style>
