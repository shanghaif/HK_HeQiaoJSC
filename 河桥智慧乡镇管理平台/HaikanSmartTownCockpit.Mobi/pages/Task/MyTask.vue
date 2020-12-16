<template>
	<view class="qiun-columns">
		<!-- 搜索 -->
		<view class="cu-bar search bg-white">
			<view class="search-form round">
				<text class="cuIcon-search"></text>
				<input v-model="datacx.cxname" @input="searchclicks()" type="text" placeholder="输入任务名称" confirm-type="search"></input>
			</view>
			<!--   -->
		</view>
		<view class="cu-bar search bg-white">
			<image src="../../static/lbt11.png" style="background-size: 100%; width: 100%; line-height: 140px; height: 140px; margin:0px 15px;"></image>
		</view>
		<scroll-view class="bg-white nav text-center">
			<!-- 所有任务 -->
			<view style="margin-bottom: 10px; font-weight: bold; font-size: 18px; margin:0px 2em;" class="cu-item" :class="index==TabCur?'text-blue cur':''"
			 v-for="(item,index) in 2" :key="index" @tap="tabSelect" :data-id="index">
				{{name[index]}}
			</view>
			<view v-if="all" style="padding-bottom: 50px;">
				<view v-for="(item,index) in datalist" :style="item.zhuangtai=='逾期'?'color:red;border: 1px #CCCCCC; border-radius: 12px;margin: 15px; box-shadow: rgb(220,220,220) 0px 0px 15px;':'border: 1px #CCCCCC; border-radius: 12px;margin: 15px; box-shadow: rgb(220,220,220) 0px 0px 15px;'">
					<view style="float: left; width: 45px;">
						<image :src="item.priority=='普通'?'../../static/top3.png':'../../static/tp3.png'" style="width: 45px; height: 45px;"></image>
					</view>
					<view style="float: right;width: 85%;font-size: 12px; color: #AAAAAA;font-weight: bold; margin-top: 15px;">
						<view :style="item.zhuangtai=='逾期'?'color:red;width: 70%; float: left; text-align: left; font-size: 10px; margin-left: 20px;':'width: 70%; float: left; text-align: left; font-size: 10px; margin-left: 20px;'">{{item.starttime}}——{{item.finishtime}}</view>
						<view :style="item.zhuangtai=='逾期'?'color:red;width: 15%;float: right; margin-right: 10px;':'width: 15%;float: right; margin-right: 10px;'">{{item.zhuangtai}}</view>
					</view>
					<view style="clear: both;"></view>
					<view style="border-top:2px dashed #C0C0C0; margin-top: -10px; margin-left:80px;margin-right: 10px;"></view>
					<view style="width: 100%; margin-top: 15px;margin-left: 25px; font-size: 13px; font-weight: bold; text-align: left;">
						<view>任务名称：</view>
					</view>
					<view style="width: 100%; margin-top: 10px;margin-left: 25px; font-size: 13px; font-weight: bold; text-align: left;">
						<view style="font-size: 11px; width: 50%;float: left;">{{item.missionheadline}}</view>
						<view style="font-size: 13px;width: 50%;">负责人：{{item.principal}}<span style="font-size: 11px;"></span></view>
					</view>
					<view style="width: 100%; margin-top: 5px;font-size: 13px; font-weight: bold; text-align: left;overflow: hidden;padding: 0 10px 10px 10px;">
						<!-- <view :style="item.zhuangtai=='逾期'?'color:red;font-size: 11px;float: left;color: #AAAAAA; margin-top: 20px;':'font-size: 11px; width: 50%;float: left;color: #AAAAAA; margin-top: 20px;'">所属工作：{{item.priorityHeadline}}</view> -->
						<view style="margin-left: 25px;">/{{item.isAllCount}}</view>
						<view style="font-size: 13px;float: right;margin-top: 8px ;margin-right: 10px;">
							<button :style="item.issfyc=='2'? '':'display:none;'" style="width: 60px;float:left; height: 30px; line-height: 30px; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;"
							 @click="toGetOther(item.missionuuid,item.principal)"><span style="margin:0px;padding:5px; font-size: 13px; vertical-align:middle ;">指派</span>
							</button>
							<button :style="item.issfyc=='1'? '':' display:none;'" style="width: 60px;float:left; height: 30px; line-height: 30px; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;"
							 @click="huibao(item.missionuuid)"><span style="margin:0px;padding:5px; font-size: 13px; vertical-align:middle ;">汇报</span>
							</button>
							<!-- <button :style="item.issfyc=='2'? '':' display:none;'" style="width: 60px;float:left; height: 30px; line-height: 30px; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;"
							 @click="wancheng(item.missionuuid)"><span style="margin:0px;padding:5px; font-size: 13px; vertical-align:middle ;"></span>
							</button> -->
							<button :style="item.iswancheng=='1'? '':' display:none;'" style="width: 60px;float:left; height: 30px; line-height: 30px; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;"
							 @click="wancheng(item.missionuuid)"><span style="margin:0px;padding:5px; font-size: 13px; vertical-align:middle ;">完成</span>
							</button>
							<button style="width: 60px; height: 30px; line-height: 30px; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;"
							 @click="xiangqing(item.missionuuid,item.principaluuid,item.participant)"><span style="margin:0px;padding:5px; font-size: 13px; vertical-align:middle ;">详情</span>
							</button>
						</view>
					</view>
				</view>
			</view>
			<!-- 弹出框 -->
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
			<!-- 与我相关 -->
			<view v-if="xg">
				<view class="padding flex flex-direction">
					<view style="border: 1px #CCCCCC; border-radius: 12px; height: 150px; box-shadow: rgb(220,220,220) 0px 0px 15px;">
						<view style="border-right: 1px #CCCCCC;width: 33%;float: left;box-shadow: rgb(240,240,240) 3px 0px 0px;height: 100px;margin-top: 20px;">
							<view style="color: #4b71e6; font-size: 26px; font-weight: bold; margin-top: 3px;">{{allcount.jxzcount}}</view>
							<view style="font-size: 18px; font-weight: bold; margin-top: 10px;">进行中</view>
							<view style="font-size: 18px; font-weight: bold; margin-top: 10px;">
								<button @click="geturl('jxz')" style="width: 90px; height: 30px; line-height: 30px;margin: 0px auto; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;">
									<span style="font-size: 13px; vertical-align:middle ;">详情</span>
								</button>
							</view>
						</view>
						<view style="border-right: 1px #CCCCCC;width: 33%;float: left;box-shadow: rgb(240,240,240) 3px 0px 0px;height: 100px;margin-top: 20px;">
							<view style="color: #4b71e6; font-size: 26px; font-weight: bold; margin-top: 3px;">{{allcount.jjdqcount}}</view>
							<view style="font-size: 18px; font-weight: bold; margin-top: 10px;">即将到期</view>
							<view style="font-size: 18px; font-weight: bold; margin-top: 10px;">
								<button @click="geturl('jjdq')" style="width: 90px; height: 30px; line-height: 30px;margin: 0px auto; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;">
									<span style="font-size: 13px; vertical-align:middle ;">详情</span>
								</button>
							</view>
						</view>
						<view style="border-right: 1px #CCCCCC;width: 33%;float: left;box-shadow: rgb(240,240,240) 3px 0px 0px;height: 100px;margin-top: 20px;">
							<view style="color: #4b71e6; font-size: 26px; font-weight: bold; margin-top: 3px;">{{allcount.ywccount}}</view>
							<view style="font-size: 18px; font-weight: bold; margin-top: 10px;">已完成</view>
							<view style="font-size: 18px; font-weight: bold; margin-top: 10px;">
								<button @click="geturl('ywc')" style="width: 90px; height: 30px; line-height: 30px;margin: 0px auto; border-radius: 20px;  background: #66A2F7;color: #FFFFFF;">
									<span style="font-size: 13px; vertical-align:middle ;">详情</span>
								</button>
							</view>
						</view>

					</view>
				</view>
			</view>
		</scroll-view>
	</view>
</template>

<script>
	import {
		taskcount,
		getuserinfo,
		getdatalist,
		getzzduserinfo,
		editPerson,
		getdataWork
	} from '@/api/taskandapi/taskapis.js'
	import {
		getuserlistss
	} from '@/api/cetiwen/cetiwenlist.js'
	import dd from 'gdt-jsapi';
	export default {
		data() {
			return {
				isShow: false,
				person: '',
				allcount: {
					jxzcount: 0,
					jjdqcount: 0,
					ywccount: 0
				},
				searhdata: "",
				index: 0,
				datalist: [],
				isActive: [], //样式数组
				deparmentList: [], //人员数组
				all: true,
				xg: false,
				TabCur: 0,
				scrollLeft: 0,
				name: ["所有任务", "与我相关"],
				datacx: {
					cxname: "",
					uname: '',
				},
				editName: [], //参与人员数组
				editList: { //传递的参数
					id: '',
					uuid: '',
					partName: '',
				}
			}
		},
		mounted() {
			this.deparmentList = [];
			this.isActive = [];
			this.editName = [];
			uni.showLoading()
			var that = this;
			// this.editList.id=getApp().globalData.UserUUId;//人员ID
			this.editList.id = '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF';
			// console.log("加载专有钉钉/浙政钉方法")
			//  dd.getAuthCode({
			// 	corpId: "39633"
			// }).then(res => {
			// 	console.log(res);
			// 	console.log(res.code);
			// 	var code = res.code;
			// 	getzzduserinfo(code).then(res => {
			// 		console.log("日志信息：", res);
			// 		getApp().globalData.UserUUId = res.data.data[0].useruuid; //用户uuid
			// 		getApp().globalData.UserId = res.data.data[0].userid; //用户id
			// 		getApp().globalData.UserName = res.data.data[0].name; //用户姓名
			// 		that.datacx.uname=res.data.data[0].useruuid;
			// 		console.log("当前用户姓名");
			// 		console.log(getApp().globalData.UserName);
			// 		that.getcount();
			// 		that.searchclicks();
			// 		that.getuserlistss2();
			// 	})
			// }).catch(err => {
			// 	console.log(err);
			// })
			// var authcode = "41c4448d87374c08b9aa5b87e07abb000a815a01";
			// getzzduserinfo(authcode).then(res => {
			// 	console.log("日志信息：", res);
			// 	getApp().globalData.UserUUId = res.data.data[0].useruuid; //用户uuid
			// 	getApp().globalData.UserId = res.data.data[0].userid; //用户id
			// 	getApp().globalData.UserName = res.data.data[0].name; //用户姓名
			// 	that.datacx.uname=res.data.data[0].useruuid;
			// 	console.log("当前用户姓名")
			// 	console.log(getApp().globalData.UserName)
			// 	that.getcount();
			// 	that.searchclicks();
			// 	that.getuserlistss2();
			// })

			// getApp().globalData.UserUUId="7FA89CB5-5C41-49DF-9DD7-4ED6282720AF";
			// that.getcount();
			// that.searchclicks();
			// that.getuserlistss2();
			that.datacx.uname = '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF';
			that.getcount();
			that.searchclicks();
			that.getuserlistss2();



		},
		methods: {
			//人员显示
			getuserlistss2() {
				getuserlistss(this.datacx.uname).then(res => {
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
				})
			},



			//完成代码
			wancheng(uuid) {
				let that = this;
				console.log(uuid)
				uni.showModal({
					title: '确定完成此任务吗？',
					success(res) {
						if (res.confirm) {
							getdataWork(uuid).then(res => {
								console.log('完成')
								that.getcount();
								that.searchclicks();
							})
						}
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
					that.getcount();
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
				let that = this;
				that.isShow = true;
				that.person = person;
				this.editList.uuid = uuid;
			},
			searchclicks() {
				uni.showLoading()
				let that = this;
				// that.datacx.uname = getApp().globalData.UserUUId;
				that.datacx.uname = '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF';
				// console.log(that.datacx.uname)
				//查询出所有任务
				getdatalist(JSON.stringify(that.datacx)).then(res => {
					if (res.data.data != undefined && res.data.data.length > 0 && res.data.data != "") {
						that.datalist = res.data.data
						console.log(that.datalist)
					} else {
						that.datalist = [];
						uni.showToast({
							content: "没有找到你要的数据",
						})
					}
					uni.hideLoading()
				})
			},
			getcount() {
				let that = this;
				taskcount({
					// id: getApp().globalData.UserId,
					//uuid: getApp().globalData.UserUUId
					uuid: '7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
				}).then(res => {
					that.allcount = res.data;
				})
				console.log(that.allcount)
			},
			xiangqing(muuid, pruuid, pauuid) {
				uni.navigateTo({
					url: '/pages/Task/Taskywc?muuid=' + muuid + '&pruuid=' + pruuid + '&pauuid=' + pauuid
				})
			},
			huibao(uuid) {
				console.log('hb' + uuid)
				uni.navigateTo({
					url: '/pages/MessageAdd/PersonalDiaryAdd?uuid=' + uuid
				})

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
					this.searchclicks();
				} else {
					this.all = false;
					this.xg = true;
					this.getcount();
				}
			}

		},
		onPullDownRefresh() {
			//监听下拉刷新动作的执行方法，每次手动下拉刷新都会执行一次
			console.log('refresh');
			setTimeout(function() {
				this.getcount();
				this.searchclicks();
				this.getuserlistss2();
				uni.stopPullDownRefresh(); //停止下拉刷新动画
			}, 1000);
		}

	}
</script>

<style lang="scss" scoped>
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
