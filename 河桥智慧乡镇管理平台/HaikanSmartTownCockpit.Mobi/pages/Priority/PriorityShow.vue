<template>
	<view style="padding-bottom: 40px;" >
		<view class="cu-card dynamic" v-for="(item,index) in productList" :key="index">
			<view  class="cu-item shadow" @click="detail(item.alarmUuid)" style="display: flex;padding: 10px 5px;">
				<view>
					标题：{{item.eventContent}}
				</view>
				<view style="flex: 1;">
					<view style="float: right;">上报时间：{{item.startTime}}</view>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		getproductList
	} from '@/api/Mail/Index.js'
	export default {
		data() {
			return {
				isCard: false,
				productList: [],
				index: 0,
				searhdata: "",
				index: 0,
				datalist: [],
				all: true,
				xg: false,
				TabCur: 0,
				scrollLeft: 0
			};
		},
		mounted() {
			let that = this;
			that.searchclicks();
		},
		methods: {
			searchclicks() {
				let that = this;
				// // that.datacx.uname = getApp().globalData.UserUUId;
				// that.datacx.uname = '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF';
				// console.log(that.datacx.uname)
				//查询出所有任务
				getproductList().then(res => {
					if (res.data.data.length > 0) {
						that.productList = res.data.data
						console.log("数据")
						console.log(res.data.data)
					} else {
						that.productList = [];
						uni.showToast({
							icon:'none',
							title: "暂无未读消息",
						})
					}

				})
			},
			detail(uuid) {
				uni.navigateTo({
					url: '/pages/Priority/MessageDetail?uuid='+uuid
				})
			}
		}
	}
</script>

<style>
</style>
