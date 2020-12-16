<template>
	<view>
		<view class="cu-card dynamic" @click="Accomplish(item)" v-for="(item,index) in zongjieList" :key="index">
			<view class="cu-item shadow">
				<view class="text-content">
					已完成：{{item.ywcinfos.length>15?item.ywcinfos.substring(0,15)+"...":item.ywcinfos}}
				</view>
				<view class="text-content">
					需要协调：{{item.xyxtinfo.length>15?item.xyxtinfo.substring(0,15)+"...":item.xyxtinfo}}
				</view>
				<view class="text-content">
					创建时间：{{item.addtimes}}
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		getuserzongjiePriority,
	} from '@/api/cetiwen/cetiwenlist.js'

	export default {
		data() {
			return {
				isCard: false,
				zongjieList: [],
				gzid: '',
				userid: ''
			};
		},
		onLoad(option) {
			let that = this;
			that.gzid = option.gzid;
			that.userid = option.userid;
			that.getuserzongjieList();
		},
		methods: {
			getuserzongjieList() {
				let that = this;
				var value = that.gzid + "&" + that.userid;
				getuserzongjiePriority(value).then(res => {
					console.log(res.data.data);
					that.zongjieList = res.data.data;
				});
			},
			//跳转完成任务
			Accomplish(list) {				
				console.log("list：");
				console.log(list);
				//const data = encodeURIComponent(JSON.stringify(list));
				uni.navigateTo({
					url: '/pages/Priority/PriorityGZHJShows?uuid=' + list.uid
				})
			},
		}
	}
</script>

<style>
</style>
