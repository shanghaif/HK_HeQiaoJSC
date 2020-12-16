<template>
	<view style="display: flex;height: 100%;flex-direction: column;">
		<view style="height: 50px;width: 100%;">
			<u-tabs-swiper ref="uTabs" :list="list" :current="current" @change="tabsChange" :is-scroll="false"
			 swiperWidth="750"></u-tabs-swiper>
		</view>
		<swiper :current="swiperCurrent" @transition="transition" @animationfinish="animationfinish" height="1400" style="flex: 1; width: 100%;height: 800px;overflow-y: auto;">
			<swiper-item class="swiper-item">
				<scroll-view scroll-y style="height: 800px;width: 100%;" @scrolltolower="onreachBottom">
					<!-- 显示部门 -->
					<view class="cu-list menu">
						<checkbox-group class="cu-list menu">
							<view class="cu-item" v-for="(item,index) in checkbox" :key="index" @click="getuser(item.depUUID)">
								<view class="action">
									<text class="text-grey" style="margin-left: 5px;color:#000; margin-top: 2px;display: inline-block;">{{item.name.length>15?item.name.substring(0,15)+'...':item.name}}（{{item.count}}人）</text>
								</view>
								<view class="action">
									>
								</view>
							</view>
						</checkbox-group>
					</view>
				</scroll-view>
			</swiper-item>
			<swiper-item class="swiper-item">
				<scroll-view scroll-y style="height: 800px;width: 100%;" @scrolltolower="onreachBottom">
					<view class="cu-bar search bg-white">
						<view class="search-form round">
							<text class="cuIcon-search"></text>
							<input v-model="datacx.cxname" @input="searchclicks()" type="text" placeholder="输入标题,撰写时间" confirm-type="search"></input>
						</view>
						<!--   -->
					</view>
					<!-- 显示部门 -->
					<view class="cu-list menu">
						<checkbox-group class="cu-list menu">
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
						</checkbox-group>
					</view>
				</scroll-view>
			</swiper-item>
		</swiper>
		<!-- <scroll-view class="page" style="color: #000000;height: auto;padding-bottom: 45px;">
			<view style="margin-bottom: 10px;" class="cu-item" v-for="(item,index) in list" @click="sectionChange(index)">
				{{item.name}}
			</view>
			<view class="cu-list menu">
				<checkbox-group class="cu-list menu">
					<view class="cu-item" v-for="(item,index) in checkbox" :key="index" @click="getuser(item.depUUID)">
						<view class="action">
							<text class="text-grey" style="margin-left: 5px;color:#000; margin-top: 2px;display: inline-block;">{{item.name.length>15?item.name.substring(0,15)+'...':item.name}}（{{item.count}}人）</text>
						</view>
						<view class="action">
							>
						</view>
					</view>
				</checkbox-group>
			</view>
		</scroll-view> -->
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		depdata, //绑定科室
		personlist,//日志
		getdatalist//查询
	} from '@/api/personalapi/personal.js'


	export default {
		data() {
			return {
				list: [{
					name: '所有任务'
				}, {
					name: '与我相关'
				}],
				rizhilist: [],
				// 因为内部的滑动机制限制，请将tabs组件和swiper组件的current用不同变量赋值
				current: 0, // tabs组件的current值，表示当前活动的tab选项
				swiperCurrent: 0, // swiper组件的current值，表示当前那个swiper-item是活动的
				checkbox: [],
				datacx: {
					cxname: "",//查找的条件
					uname:"",//传递的人员uuid
				},
				// gzid: getApp().globalData.UserUUId
				gzid:'7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
			}
		},
		mounted() {
			console.log("加载个人日志")
			let that = this;
			that.getdepart();
			 that.datacx.uname='7FA89CB5-5C41-49DF-9DD7-4ED6282720AF';
			// that.datacx.uname=getApp().globalData.UserUUId;
			that.getuserzongjieList(that.datacx.uname);
		},
		methods: {
			//查询
			searchclicks() {
				this.rizhilist=[];
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
			//个人日志
			getuserzongjieList(userid) {
				personlist(userid).then(res => {
					console.log(res.data.data);
					this.rizhilist = res.data.data;
				});
			},
			// tabs通知swiper切换
			tabsChange(index) {
				this.swiperCurrent = index;
			},
			// swiper-item左右移动，通知tabs的滑块跟随移动
			transition(e) {
				let dx = e.detail.dx;
				this.$refs.uTabs.setDx(dx);
			},
			// 由于swiper的内部机制问题，快速切换swiper不会触发dx的连续变化，需要在结束时重置状态
			// swiper滑动结束，分别设置tabs和swiper的状态
			animationfinish(e) {
				let current = e.detail.current;
				this.$refs.uTabs.setFinishCurrent(current);
				this.swiperCurrent = current;
				this.current = current;
			},
			// scroll-view到底部加载更多
			onreachBottom() {
				
			},
			getdepart() {
				let that = this;
				depdata().then(res => {
					console.log("日志信息：");
					console.log(res);
					that.checkbox = res.data.data;
				})
			},
			getuser(depid) {
				uni.navigateTo({
					url: '/pages/Personallog/userlist?depid=' + depid
				})
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
		},

	}
</script>

<style>
	page{
		height: 100%;
	}
</style>
