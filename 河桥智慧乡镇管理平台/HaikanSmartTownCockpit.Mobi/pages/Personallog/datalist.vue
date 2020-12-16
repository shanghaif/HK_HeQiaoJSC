<template>
	<view>
		<view class="cu-bar search bg-white">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input v-model="datacx.cxname" @input="searchclicks()" type="text" placeholder="输入标题,撰写时间" confirm-type="search"></input>
			</view>
			<!--   -->
		</view>
		
		
		<view class="cu-card dynamic" @click="Accomplish(item)" v-for="(item,index) in rizhilist" :key="index">
			<view class="cu-item shadow">
				<view class="text-content">
					个人日志标题：{{item.headline.length>15?item.headline.substring(0,15)+"...":item.headline}}
				</view>
				<view class="text-content">
					撰写时间：{{item.time}}
				</view>
				<view class="text-content">
					创建者：{{item.adduser}}
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	import {
		personlist,//日志
		getdatalist,//查询
	} from '@/api/personalapi/personal.js'

	export default {
		data() {
			return {
				rizhilist: [],
				datacx: {
					cxname: "",//查找的条件
					uname:"",//传递的人员uuid
				},
			};
		},
		onLoad(option) {
			let that=this;
			that.datacx.uname=option.userid;
			that.getuserzongjieList(that.datacx.uname);
		},
		methods: {
			getuserzongjieList(userid) {
				personlist(userid).then(res => {
					console.log(res.data.data);
					this.rizhilist = res.data.data;
				});
			},
			//跳转完成任务
			Accomplish(list) {
				console.log("list:")
				console.log(list)
				//const data = encodeURIComponent(JSON.stringify(list));
				uni.navigateTo({
					url: '/pages/Personallog/datadatas?uuid=' + list.uid
				})
			},
			
			searchclicks() {
				let that=this;
				console.log(that.datacx.uname)
				//查询出所有任务
				getdatalist(JSON.stringify(that.datacx)).then(res => {
					if (res.data.data.length > 0) {
						that.rizhilist = res.data.data
					} else {
						that.rizhilist = [];
						uni.showToast({
							icon:'none',
							title: "没有找到你要的数据",
						})
					}
			
				})
			},
			
		}
	}
</script>

<style>
</style>
