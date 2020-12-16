<template>
	<view class="qiun-columns">
		<view class="cu-bar search bg-white">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input v-model="searhdata" @input="searchclicks()" type="text" placeholder="输入名称" confirm-type="search"></input>
			</view>
			<!--   -->
		</view>

		<scroll-view class="bg-white nav text-center">
			<view>
				<view v-for="(item,index) in datalist" :style="item.zhuangtai=='逾期'?'color:red;border: 1px #CCCCCC; border-radius: 12px; height: 150px;margin: 15px; box-shadow: rgb(220,220,220) 0px 0px 15px;':'border: 1px #CCCCCC; border-radius: 12px; height: 150px;margin: 15px; box-shadow: rgb(220,220,220) 0px 0px 15px;'">
					<view style="float: left; width: 45px;">
						<image :src="item.priority=='普通'?'../../static/top3.png':'../../static/tp3.png'" style="width: 45px; height: 45px;"></image>
					</view>
					<view style="float: right;width: 85%;font-size: 12px; color: #AAAAAA;font-weight: bold; margin-top: 15px;">
						<view :style="item.zhuangtai=='逾期'?'color:red;width: 70%; float: left; text-align: left; font-size: 10px; margin-left: 20px;':'width: 70%; float: left; text-align: left; font-size: 10px; margin-left: 20px;'">{{item.startTime}}——{{item.finishTime}}</view>
						<view :style="item.zhuangtai=='逾期'?'color:red;width: 15%;float: right; margin-right: 10px;':'width: 15%;float: right; margin-right: 10px;'">{{item.zhuangtai}}</view>
					</view>
					<view style="clear: both;"></view>
					<view style="border-top:2px dashed #C0C0C0; margin-top: -10px; margin-left:80px;margin-right: 10px;"></view>
					<view style="width: 100%; margin-top: 15px;margin-left: 25px; font-size: 13px; font-weight: bold; text-align: left;">
						<view>名称：</view>
					</view>
					<view style="width: 100%; margin-top: 10px;margin-left: 25px; font-size: 13px; font-weight: bold; text-align: left;">
						<view style="font-size: 11px; width: 50%;float: left;">{{item.missionHeadline}}</view>
						<view style="font-size: 13px;width: 50%;">负责人：<span style="font-size: 11px;">{{item.principal.length>8?item.principal.substring(0,8)+'...':item.principal}}</span></view>
					</view>
					<view style="width: 100%; margin-top: 5px;margin-left: 10px; font-size: 13px; font-weight: bold; text-align: left;">
						<!-- <view :style="item.zhuangtai=='逾期'?'color:red;font-size: 11px;float: left;color: #AAAAAA; margin-top: 20px;':'font-size: 11px; width: 50%;float: left;color: #AAAAAA; margin-top: 20px;'">所属工作：{{item.priorityHeadline}}</view> -->
						<view style="font-size: 13px;width: 60%; float: right;margin-top: 8px ;margin-right: 10px;">
							<button :style="item.issfyc=='1'? '':'display:none;'" style="width: 40px;float:left; height: 20px; line-height: 20px; border-radius: 5px;  background: #66A2F7;color: #FFFFFF;"
							 @click="toGetOther(item.missionUuid,item.principal)"><span style="margin:0px;padding:-5px; font-size: 10px; vertical-align:middle ;">指派</span>
							</button>
							<button :style="item.issfyc=='2'?' ':'display:none;'" style="width: 40px;float:left; height: 20px; line-height: 20px; border-radius: 5px;  background: #66A2F7;color: #FFFFFF;"
							 @click="Accomplish(item.missionUuid)"><span style="margin:0px;padding:-5px; font-size: 10px; vertical-align:middle ;">完成</span>
							</button>
							<button :style="item.issfycss=='是'?' display:none;':''" style="width: 40px;float:left; height: 20px; line-height: 20px; border-radius: 5px;  background: #66A2F7;color: #FFFFFF;"
							 @click="huibao(item.missionUuid)"><span style="margin:0px;padding:-1px; font-size: 10px; vertical-align:middle ;">汇报</span>
							</button>
							<button style="width: 40px; height: 20px; line-height: 20px; border-radius: 5px;  background: #66A2F7;color: #FFFFFF;"
							 @click="xiangqing(item.missionUuid,item.principaluuid,item.participant)"><span style="margin:0px;padding:-1px; font-size: 10px; vertical-align:middle ;">详情</span>
							</button>
						</view>
					</view>
				</view>
				<view>
					<u-popup mode="right" v-model="isShow">
						<view class="content">
							<scroll-view scroll-y="true">
								<view>
									<view style="font-size: 16px;line-height: 2;color: #000000;letter-spacing: 2px;font-weight: 500;margin: 30px 0;">选择指派人</view>
									<view style="margin: 10px 0;">
										<!-- 此uuid:{{editList.uuid}} -->
										负责人：{{person}}
									</view>
									<view style="clear: both;overflow: hidden;">
										<view class="officeItem" v-for="(item,index) in deparmentList" :style="{backgroundColor:isActive[index].bgc,color:isActive[index].color}"
										 @click="chooseOffice(index)">{{item.name}}</view>
									</view>
								</view>
							</scroll-view>
							<view class="confrim-btn">
								<u-button @click="editOk" style="margin: 10px 0;">确定</u-button>
								<u-button @click="isShow=false" style="margin: 10px 0;">取消</u-button>
							</view>
						</view>
					</u-popup>
				</view>
			</view>
		</scroll-view>
	</view>
</template>

<script>
	import {
		taskwhere,
		getdataWork,
		editPerson
	} from '@/api/taskandapi/taskapis.js'
	import {
		getuserlistss
	} from '@/api/cetiwen/cetiwenlist.js' //人员显示
	export default {
		data() {
			return {
				isShow: false,
				person: '',
				searhdata: "",
				btnname: "完成",
				index: 0,
				datalist: [],
				all: true,
				xg: false,
				uid: '',
				TabCur: 0,
				scrollLeft: 0,
				isActive: [], //样式数组
				deparmentList: [], //人员数组
				editName: [], //参与人员数组
				type: '',
				editList: { //传递的参数
					id: '',
					uuid: '',
					partName: '',
				}
			}
		},
		onLoad(option) {
			this.deparmentList = [];
			this.isActive = [];
			this.editName = [];
			let that = this;
			// this.editList.id=getApp().globalData.UserUUId;//人员ID
			this.editList.id='7FA89CB5-5C41-49DF-9DD7-4ED6282720A';
			that.type = option.type;
			that.searchclicks();
		},
		methods: {
			searchclicks() {
				let that = this;
				// let data=getApp().globalData.UserUUId;//人员信息
				let data = '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
				getuserlistss(data).then(res => {
					this.deparmentList = [];
					this.isActive = [];
					this.editName = [];
					console.log('人员显示')
					console.log(res)
					for (var i = 0; i < res.data.data.length; i++) {
						this.deparmentList.push({
							name: res.data.data[i].realName,
							uuid: res.data.data[i].systemUserUuid
						});
						this.isActive.push({
							bgc: '#fff',
							color: '#333'
						})
						this.isActive[0].bgc = '#007AFF'
						this.isActive[0].color = '#fff'
					}
					this.editName.push(this.deparmentList[0].uuid)
					console.log(this.editName)
				});
				taskwhere({
					type: that.type,
					// id: getApp().globalData.UserId,
					uuid: '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF',
					//uuid:getApp().globalData.UserUUId,
					searhdata: that.searhdata
				}).then(res => {
					console.log("与我相关")
					console.log(res);
					if (res.data.data.length > 0) {
						that.datalist = res.data.data
						if (that.type == "ywc") {
							that.btnname = "详情";
							//this.show = false;
						} else {
							that.btnname = "完成";
						}

					} else {
						that.datalist = [];
						uni.showToast({
							content: "没有找到你要的数据",
						})
					}
				})
			},
			//提交更新人员
			editOk() {
				let that = this;
				this.editList.partName = "";
				for (var i = 0; i < this.editName.length; i++) {
					this.editList.partName += this.editName[i] + ',';
				}
				console.log(this.editList.partName)
				editPerson(this.editList).then(res => {
					uni.showToast({
						title: "指定成功",
					})
					this.isShow = false;
					that.searchclicks();
				})
			},
			//选中人员
			chooseOffice(index) {
				if (this.isActive[index].bgc == '#007AFF') {
					console.log('here')
					this.isActive[index].bgc = '#fff'
					this.isActive[index].color = '#333'
					// this.editName.splice(index, 1);
					// console.log(this.editName)
				} else {
					this.isActive[index].bgc = '#007AFF'
					this.isActive[index].color = '#fff'
					// this.editName.push(this.deparmentList[index].uuid)
					// console.log(this.editName)
				}
				this.editName = [];
				for (var i = 0; i < this.isActive.length; i++) {
					// console.log(this.isActive[i].bgc)
					if (this.isActive[i].bgc == '#007AFF') {
						// console.log('我进来了'+i+'次')
						// console.log(this.deparmentList[i])
						let params = this.deparmentList[i].uuid
						this.editName.push(params)
					}
				}
				// console.log('在这里')
				// console.log(this.isActive)
				// console.log(this.deparmentList)
				console.log(this.editName)
			},
			//指派人员
			toGetOther(uuid, person) {
				console.log(person)
				let that = this;
				that.isShow = true;
				that.person = person;
				this.editList.uuid = uuid;
			},
			searchclick: function(event) {
				let that = this;
				that.searchclicks();
			},
			xiangqing(muuid, pruuid, pauuid) {
				uni.navigateTo({
					url: '/pages/Task/Taskywc?muuid=' + muuid + '&pruuid=' + pruuid + '&pauuid=' + pauuid
				})
			},
			huibao(uuid) {
				uni.navigateTo({
					url: '/pages/MessageAdd/PersonalDiaryAdd?uuid=' + uuid
				})
			},
			//跳转完成任务
			Accomplish(uuid) {
				console.log(uuid);
				let uid = uuid;

				uni.showModal({
					title: "提示",
					content: "确定完成此任务?",
					confirmText: "确定",
					cancelText: "取消",
					success: (res) => {
						if (res.confirm) { //确定完成
							uni.showLoading()
							console.log(uid);
							//JSON.stringify(this.formModel)
							getdataWork(uid).then(res => {
								if (res.data.code === 200) {
									let tishi = "操作成功";
									uni.showToast({
										title: "提示",
										content: tishi,
										showCancel: false
									})
									uni.hideLoading()
									uni.redirectTo({
										url: './MyTask'
									})

								} else {
									uni.showToast({
										title: "提示",
										content: "提交完成出错!",
									})
									uni.hideLoading()
									uni.navigateBack(); //返回
								}
								this.searchclicks();
							})

						}
					}

				})
				// uni.navigateTo({
				// 	url: '/pages/Task/AccomplishTask?uuid=' + uuid + '&type=' + this.type
				// })
			},
			geturl(type) {
				//跳转页面
				uni.navigateTo({
					url: '/pages/Task/MyTaskList?type=' + type
				})
			},
			tabSelect(e) {
				this.TabCur = e.currentTarget.dataset.id;
				this.scrollLeft = (e.currentTarget.dataset.id - 1) * 60
				if (this.TabCur == 0) {
					this.all = true;
					this.xg = false;
				} else {
					this.all = false;
					this.xg = true;
				}
			}

		}
	}
</script>

<style>
	.content {
		margin: 20rpx;
		text-align: center;
		max-width: 200px;
		position: relative;
		height: 100%;
	}

	.confrim-btn {
		position: absolute;
		bottom: 50px;
		width: 100%;
	}

	.officeItem {
		// padding: 0 10px;
		width: 80px;
		margin: 6px;
		float: left;
		text-align: center;
		height: 40px;
		line-height: 40px;
		font-size: 12px;
		/* background-color: #007AFF; */
		border: 1px solid #ccc;
		border-radius: 4px;
	}
</style>
