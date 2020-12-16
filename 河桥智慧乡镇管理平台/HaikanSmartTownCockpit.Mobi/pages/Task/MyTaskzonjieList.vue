<template>
	<view>
		<view class="cu-bar search bg-white">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input v-model="datacx.cxname" @input="searchclicks()" type="text" placeholder="输入标题,撰写时间" confirm-type="search"></input>
			</view>
			<!--   -->
		</view>


		<view class="cu-card dynamic" @click="Accomplish(item)" v-for="(item,index) in zongjieList" :key="index">
			<view class="cu-item shadow">
				<view class="text-content">
					个人日志标题：{{item.headline.length>15?item.headline.substring(0,15)+"...":item.headline}}
				</view>
				<view class="text-content">
					撰写时间：{{item.time}}
				</view>
				<!-- <view class="text-content">
					创建者：{{item.adduser}}
				</view> -->
			</view>
		</view>
	</view>
</template>

<script>
	import {
		getuserzongjie,
	} from '@/api/cetiwen/cetiwenlist.js'

	export default {
		data() {
			return {
				isCard: false,
				zongjieList: [],
				rwid: '',
				userid: ''
			};
		},
		onLoad(option) {
			let that = this;
			that.rwid = option.rwid;
			that.userid = option.userid;
			that.getuserzongjieList();
		},
		methods: {
			getuserzongjieList() {
				let that = this;
				var value = that.rwid + "&" + that.userid;
				getuserzongjie(value).then(res => {
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
					url: '/pages/Personallog/datadatas?uuid=' + list.uid
				})
			},
		}
	}
</script>

<style>
</style>
