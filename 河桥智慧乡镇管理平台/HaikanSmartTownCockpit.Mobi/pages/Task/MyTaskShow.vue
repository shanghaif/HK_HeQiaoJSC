<template>
	<view class="cu-list menu">
		<view class="cu-bar search bg-white">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input :adjust-position="false" type="text" v-model="searhdata" placeholder="输入名称,负责人,结束时间" confirm-type="search"></input>
			</view>
			<view class="action">
				<button class="cu-btn bg-cyan margin-tb-sm" @click="searchclick">搜索</button>
			</view>
		</view>
		<view class="cu-item" style="text-align: center;">
			<view class="content">
				<text class="text-grey" style="font-weight: bold;">名称</text>
			</view>
			<view class="content">
				<text class="text-grey" style="font-weight: bold;">负责人</text>							
			</view>
			<view class="content">
				<text class="text-grey" style="font-weight: bold;">结束时间</text>
			</view>
			<view class="content">
				<text class="text-grey" style="font-weight: bold;">操作</text>
			</view>
		</view>
		<view v-for="(item,index) in datalist" class="cu-item" style="text-align: center;">
			<view class="content">
				<text class="text-grey">{{item.misname}}</text>
			</view>
			<view class="content">
				<!-- <input  data-target="Modal" v-model="item.fuzerename" name="input"
				 disabled="on"></input> -->
				<text class="text-grey">{{item.fuzerename.length>5?item.fuzerename.substring(0,5)+"...":item.fuzerename}}</text>
			</view>
			<view class="content">
				<text class="text-grey">{{item.endtime}}</text>
			</view>
			<view class="content">
				<button class="cu-btn bg-cyan margin-tb-sm" v-if="wanchenshow(item.fuzerenuuid,item.auditstatus,item.accomplish)"
				 @click="Accomplish(item.renwuuuid)">{{btnname}}</button>
				 <button class="cu-btn bg-cyan margin-tb-sm" @click="MissionJournal(item.renwuuuid)">个人日志详细</button>
				<!-- <text class="cuIcon-search" v-if="show" @click="MissionJournal(item.renwuuuid)"></text> -->
			</view>

		</view>
	</view>
</template>

<script>
	import {
		taskwhere,
	} from '@/api/taskandapi/taskapis.js'

	export default {
		data() {
			return {
				show: true,
				btnname: "完成",
				index: 0,
				datalist: [],
				type: "",
				searhdata: "", //搜索框内容
			}
		},
		onLoad(e) {
			/* e//获取地址栏参数 */
			//登录人id和uuid先写死

			this.type = e.type
			this.onloaddata()
			//location.reload()

		},
		/* onPullDownRefresh() {
			this.onloaddata() //下拉重新加载数据
			setTimeout(function() {
				uni.stopPullDownRefresh();
			}, 1000);
		}, */
		methods: {

			//数据加载
			onloaddata() {
				console.log(this.searhdata)
				taskwhere({
					type: this.type,
					// id: getApp().globalData.UserId,
					//uuid: getApp().globalData.UserUUId,
					uuid:'7FA89CB5-5C41-49DF-9DD7-4ED6282720AF',
					searhdata: this.searhdata
				}).then(res => {
					if (res.data.data.length > 0) {
						this.datalist = res.data.data
						if (this.type == "ywc") {
							this.btnname = "详情";
							//this.show = false;
						} else {
							this.btnname = "完成";
						}

					} else {
						this.datalist = [];
						uni.showToast({
							content: "没有找到你要的数据",
						})
					}

				})
			},
			//搜索
			searchclick() {
				this.onloaddata() //重新加载数据
			},
			//完成按钮显示与隐藏
			wanchenshow(fuzerenuuid, zhuangtai, accomplish) {
				console.log(accomplish)
						console.log(fuzerenuuid)				
						if (this.btnname == "完成") {
					if (fuzerenuuid.indexOf('7FA89CB5-5C41-49DF-9DD7-4ED6282720AF') != -1 && zhuangtai == "0" && accomplish == 0) {
						return true
						
					} else {
						return false
					}
				} else {
					if (fuzerenuuid == '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF') {
						return true
					} else {
						return false
					}
				}


			},

			//跳转完成任务
			Accomplish(uuid) {
				if (this.btnname == "完成") {
					uni.navigateTo({
						url: '/pages/Task/AccomplishTask?uuid=' + uuid + '&type=' + this.type
					})
				} else {
					uni.navigateTo({
						url: '/pages/Task/Taskywc?uuid=' + uuid
					})
				}

			},
			//跳转操作日志
			MissionJournal(uuid) {
				uni.navigateTo({
					url: '/pages/Task/MissionJournal?uuid=' + uuid
				})
			},

		}
	}
</script>


<style>
</style>
