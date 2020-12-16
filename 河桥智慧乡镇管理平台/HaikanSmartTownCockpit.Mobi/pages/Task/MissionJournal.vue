<template>
	<view>
		<form>
			<view class="cu-form-group margin-top">
				<view class="title">所属重点工作：</view>
				<input placeholder="所属重点工作" v-model="formModel.priorityname" name="input" disabled="on"></input>
			</view>
			<view class="cu-form-group">
				<view class="title">任务标题：</view>
				<input placeholder="任务标题" v-model="formModel.missionHeadline" name="input" disabled="on"></input>
			</view>
			<view class="cu-form-group">
				<view class="title">负责人：</view>
				<input placeholder="负责人" v-model="formModel.principalname" disabled="on" name="input"></input>
			</view>
			<view class="cu-form-group">
				<view class="title">开始时间：</view>
				<input placeholder="开始时间" v-model="formModel.startTime" name="input" disabled="on"></input>
			</view>
			<view class="cu-form-group">
				<view class="title">结束时间：</view>
				<input placeholder="结束时间" v-model="formModel.finishTime" name="input" disabled="on"></input>
			</view>
			<view class="cu-form-group align-start">
				<view class="title">任务描述：</view>
				<textarea disabled="on" v-model="formModel.missionDescribe" placeholder="任务描述"></textarea>
			</view>
			<view class="cu-form-group ">
				<view class="title">优先级：</view>
				<input placeholder="优先级" v-model="formModel.priority" name="input" disabled="on"></input>
			</view>
			<!-- 			<view class="cu-form-group">
				<view class="title">计划工时：</view>
				<input placeholder="计划工时" v-model="formModel.manhourt" name="input" disabled="on"></input>
			</view> -->
			<view class="cu-form-group">
				<view class="title">参与人：</view>
				<input placeholder="参与人" @tap="showModal" data-target="Modal" v-model="formModel.selectcanyurenname" name="input"
				 disabled="on"></input>
			</view>

			<view class="cu-modal" :class="modalName=='Modal'?'show':''">
				<view class="cu-dialog">
					<view class="cu-bar bg-white justify-end">
						<view class="content">参与人</view>
						<view class="action" @tap="hideModal">
							<text class="cuIcon-close text-red"></text>
						</view>
					</view>
					<view class="padding-xl">
						{{formModel.selectcanyurenname}}
					</view>
				</view>
			</view>




			<!-- <view class="cu-form-group">
				<view class="title">审批人：</view>
				<input placeholder="审批人" v-model="formModel.approvername" name="input" disabled="on"></input>
			</view> -->

			<div id="new_message" style="max-height: 500px; overflow: auto;">
				<view class="cu-list menu margin-top" v-for="(item,index) in datalist">
					<view class="cu-item">
						<view class="content">
							<text class="text-grey" style="font-weight: bold;">操作日志：</text>
						</view>
						<view class="content">
							<text class="text-grey" style="white-space:nowrap;">{{item.establishTime}}</text>
						</view>
						<!-- 					<view class="content" style="text-align: right;">
						<text style="font-weight: bold; color:#DD514C;">{{item.read}}已读</text>
					</view> -->
					</view>
					<view class="cu-item">
						<view class="content" style="margin-right: 5px;">
							<text class="text-grey nameclass">{{item.chuanjianname}}</text>
						</view>
						<view class="content">
							<text class="text-grey" style="color: #8799A3;">{{item.content}}</text>
							<view class="grid col-4 grid-square flex-sub">
								<view class="bg-img" :data-url="imgurl+img">
									<image :src="imgurl+item.img" mode="aspectFill"></image>
								</view>
							</view>
						</view>
					</view>
				</view>
			</div>
			<view class="cu-list menu margin-top" style="height: 200px;"></view>
			<view style="position: fixed;bottom: 0px;left: 0px;right: 0px;background-color: white;">
				<view class="cu-bar bg-white margin-top">
					<view class="action">
						操作日志添加：
					</view>
					<!-- 				<view class="action">
									{{imgList.length}}/4
								</view> -->
				</view>

				<view class="cu-form-group">
					<view class="title">内容</view>
					<input placeholder="请输入内容" onkeyup="value=value.replace(/[^\a-\z\A-\Z]/g,'')" onpaste="value=value.replace(/[^\a-\z\A-\Z]/g,'')" oncontextmenu = "value=value.replace(/[^\a-\z\A-\Z]/g,'')" v-model="formmodelrizhi.content" name="input">
					<text class="lg text-gray cuIcon-camerafill" style="margin-right: 3px;" @tap="ChooseImage"></text>
					</input>
					<button class="cu-btn bg-green shadow" @click="dogetscorelist">发送</button>
				</view>
				<view class="cu-form-group">
					<view class="grid col-4 grid-square flex-sub">
						<view class="bg-img" v-for="(item,index) in imgList" :key="index" @tap="ViewImage" :data-url="imgList[index]">
							<image :src="imgList[index]" mode="aspectFill"></image>
							<view class="cu-tag bg-red" @tap.stop="DelImg" :data-index="index">
								<text class='cuIcon-close'></text>
							</view>
						</view>

					</view>
				</view>

			</view>

		</form>
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		loaddata,
		appwanchen,
		caozuolist,
		appcreaterizhi,
		RegistPicture
	} from '@/api/taskandapi/taskapis.js'
	export default {
		data() {
			return {
				imgurl:http.baseUrl+"UploadFiles/PersonalDiary/",
				yemiandata: "",
				baocunuuid: "",
				timer: null,
				datacount1: 0,
				datacount2: 0,
				datalist: [],
				imgList: [],
				modalName: null,
				formModel: {
					priorityname: "", //所属重点工作
					missionHeadline: '', //任务标题
					principalname: '', //负责人
					startTime: '', //开始时间
					finishTime: '', //结束时间
					missionDescribe: '', //任务描述
					priority: '', //优先级
					manhourt: '', //计划工时
					approvername: '', //审批人
					auditStatus: '', //审核状态
					selectcanyurenname: '', //参与人
					establishName: '', //创建人
					remark: "", //备注
				},
				//操作日志
				formmodelrizhi: {
					missionUuid: "", //所属任务
					content: "", //日志内容
					accessory: "", //日志图片
					establishName: "", //创建人
				}
			};
		},
		onLoad(e) {
			console.log("url地址")
			console.log(this.imgurl)

			this.yemiandata = e.uuid
			loaddata({
				uuid: e.uuid
			}).then(res => {
				this.formModel = res.data.data
			})
			this.baocunuuid = e.uuid;
			this.settimeout();
		},
		methods: {
			//选择图片
			ChooseImage() {
				uni.chooseImage({
					count: 1, //默认9
					success: (res) => {
						if (this.imgList.length >= 0) {
							this.imgList = this.imgList.concat(res.tempFilePaths)
							this.formmodelrizhi.accessory = this.imgList[0];
						} else {
							this.imgList = res.tempFilePaths
						}
						const tempFilePaths = res.tempFilePaths; //拿到选择的图片，是一个数组
						console.log("进入")
						uni.uploadFile({
							url: http.baseUrl + 'api/v1/task/app/taskcaozuo/RegistPicture', //post请求的地址
							filePath: tempFilePaths[0],
							name: 'file',
							fileType: 'image',
							formData: {
								'username': "" //formData是指除了图片以外，额外加的字段
							},
							success: (uploadFileRes) => {
								console.log(uploadFileRes)
								//这里要注意，uploadFileRes.data是个String类型，要转对象的话需要JSON.parse一下
								var obj = JSON.parse(uploadFileRes.data);
								console.log(obj)
								this.formmodelrizhi.accessory = obj.path;
							},
							fail(rest) {
								console.log(rest)
							}
						})
					}
				});

			},
			//显示图片
			ViewImage(e) {
				uni.previewImage({
					urls: this.imgList,
					current: e.currentTarget.dataset.url
				});
			},
			//删除图片
			DelImg(e) {
				/* uni.showModal({
					title: '删除',
					content: '确定要删除吗？',
					cancelText: '取消',
					confirmText: '确定',
					success: res => {
						if (res.confirm) {
							this.imgList.splice(e.currentTarget.dataset.index, 1)
						}
					}
				}) */
				this.imgList.splice(e.currentTarget.dataset.index, 1)
			},



			settimeout() {
				this.timer = setInterval(this.ofdj, 500); //每隔500毫秒获取操作日志
			},
			//获取操作日志
			ofdj() {
				if (this.baocunuuid != this.yemiandata) //切换了页面，停止计时器
				{
					clearInterval(this.timer); //停止计时器
				} else {
					this.baocunuuid = this.yemiandata;
					caozuolist({
						uuid: this.yemiandata
					}).then(res => {
						this.datalist = res.data.data
						this.datacount2 = res.data.data.length
					})

					if (this.datacount1 != this.datacount2) {
						this.$nextTick(() => { //滚动到div最底部
							var container = this.$el.querySelector("#new_message");
							container.scrollTop = container.scrollHeight;
						});
						uni.pageScrollTo({
							duration: 20,
							scrollTop: 99999999999
						})
						this.datacount1 = this.datacount2;
					}
				}


			},
			//点击确定添加操作日志
			dogetscorelist() {
				this.formmodelrizhi.missionUuid = this.yemiandata;
				// this.formmodelrizhi.establishName = getApp().globalData.UserUUId;
				this.formmodelrizhi.establishName='7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
				console.log("进入发送")
				console.log("选择的图片")
				console.log(this.imgList)
				if (this.imgList.length > 0) {

						//this.formmodelrizhi.accessory = this.imgList[0];
						//JSON.stringify(this.formmodelrizhi)
						appcreaterizhi(JSON.stringify(this.formmodelrizhi)).then(res => {
							if (res.data.code === 200) {
								this.formmodelrizhi.content = "";
								this.imgList = [];
							} else {
								uni.showModal({
									title: "提示",
									showCancel: false,
									content: "发送失败!",
									buttonText: "确定"
							
								})
							}
						})
					
					this.formmodelrizhi.content = "";
					this.imgList = [];

				} else
				if (this.formmodelrizhi.content.trim() != "") {
					//JSON.stringify(this.formmodelrizhi)
					let specialKey = /[`~!@#$%^&*()_\-+=<>?:"{}|,.\/;'\\[\]·~！@#￥%……&*（）——\-+={}|《》？：“”【】、；‘'，。、]/im;
					  let content = this.formmodelrizhi.content.trim();
					if(specialKey.test(content)){
						_valid = "不能输入特殊字符";
						return _valid;
					}else{
						appcreaterizhi(JSON.stringify(this.formmodelrizhi)).then(res => {
							if (res.data.code === 200) {
								this.formmodelrizhi.content = "";
								this.imgList = [];
							} else {
								uni.showModal({
									title: "提示",
									showCancel: false,
									content: "发送失败!",
									buttonText: "确定"
						
								})
							}
						})
					}					
				} else {
					uni.showToast({
						content: "请输入日志内容",
					})
				}

			},
			//获取样式字体
			getstyle(e) {
				if (e == res.data.data.useruuid) //这条信息是当前登陆人发送的
				{
					return "color:green;"
				}
			},
			//获取样式div
			getstylevie(e) {
				if (e == res.data.data.useruuid) //这条信息是当前登陆人发送的
				{
					return "background-color: #CCE6FF;"
				}
			},
			//查看参与人
			showModal(e) {
				this.modalName = e.currentTarget.dataset.target
			},
			hideModal(e) {
				this.modalName = null
			},


		}
	}
</script>

<style>
	.cu-form-group .title {
		min-width: calc(4em + 15px);
	}

	.nameclass {
		font-weight: bold;
		white-space: nowrap;
	}
</style>
