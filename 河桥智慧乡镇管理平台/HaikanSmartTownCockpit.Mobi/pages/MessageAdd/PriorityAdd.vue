<template>
	<view>
		<form>
			<view class="cu-form-group">
				<view class="title"><view style="color:red;float: left;">*</view>工作标题：</view>
				<input v-model="formModel.fields.priorityHeadline" placeholder="请输入工作标题" name="input"></input>
			</view>
			<view class="cu-form-group align-start">
				<view class="title"><view style="color:red;float: left;">*</view>工作描述：</view>
				<textarea v-model="formModel.fields.priorityDescribe" maxlength="-1" :disabled="modalName!=null" @input="textareaBInput"
				 placeholder="请输入工作描述"></textarea>
			</view>

			
			<view>
				<scroll-view class="page" style="color: #000000;">
					<view class="cu-list menu">
						<view class="cu-item arrow" @click="checkchange1()">
							<view class="action">
								<text class="text-grey" style="color:#000; margin-top: 2px;display: inline-block;"><view style="color:red;float: left;">*</view>负责人</text>
							</view>
							<view class="action">
								<text class="text-grey text-sm" style="color: #000000;">{{usernamelist1.length>10?usernamelist1.substr(0,3)+'.....':usernamelist1}}</text>
							</view>
						</view>
					</view>
				</scroll-view>
			</view>
			
			<view>
				<scroll-view class="page" style="color: #000000;">
					<view class="cu-list menu">
						<view class="cu-item arrow" @click="checkchange()">
							<view class="action">
								<text class="text-grey" style="color:#000; margin-top: 2px;display: inline-block;"><view style="color:red;float: left;">*</view>参与人</text>
							</view>
							<view class="action">
								<text class="text-grey text-sm" style="color: #000000;">{{usernamelist.length>10?usernamelist.substr(0,3)+'.....':usernamelist}}</text>
							</view>
						</view>
					</view>
				</scroll-view>
			</view>
			
			<view class="cu-modal bottom-modal" :class="modalName=='ChooseModal'?'show':''" @tap="hideModal">
				<view class="cu-dialog" @tap.stop="">
					<view class="cu-bar bg-white">
						<!-- <view class="action text-blue" @tap="hideModal">取消</view> -->
						<view class="action text-green" @tap="hideModal">确定</view>
					</view>
					<view class="grid col-3 padding-sm">
						<view v-for="(item,index) in checkbox" class="padding-xs" :key="index">
							<button class="cu-btn orange lg block" :class="item.checkeds?'bg-orange':'line-orange'" @tap="ChooseCheckbox"
							 :data-value="item.value"> {{item.name}}
								<!-- <view class="cu-tag sm round" :class="item.checkeds?'bg-white text-orange':'bg-orange'" v-if="item.hot">HOT</view> -->
							</button>
						</view>
					</view>
				</view>
			</view>
			<view class="cu-form-group">
				<view class="title">结束时间：</view>
				<picker mode="date" :value="formModel.fields.endTime" @change="DateChange">
					<view class="picker">
						{{formModel.fields.endTime}}
					</view>
				</picker>
			</view>
			<!-- <view class="cu-form-group">
				<view class="title">排序字段：</view>
				<input v-model="formModel.fields.sortord" placeholder="请输入排序字段" name="input"></input>
			</view> -->
			<view class="padding flex flex-direction">
				<button class="cu-btn bg-red margin-tb-sm lg" @click="submitfrom" style="background-color: #0081FF;">确定</button>
				<button class="cu-btn bg-grey lg" @click="tiaozhuan">取消</button>
			</view>
		</form>
	</view>
</template>

<script>
	import {
		formateDate
	} from '@/global/utils.js';
	import {
		createPriority,
		gealluser,
	} from '@/api/priorityapi/priority.js'
	export default {
		data() {
			return {
				canyuname: '',
				fuzename: '',
				date: formateDate(new Date(), 'Y-M-D'),
				imgList: [],
				modalName: null,
				formModel: {
					fields: {
						priorityHeadline: "",
						priorityDescribe: "",
						principal: "",
						participant: "",
						establishTime: "",
						endTime:formateDate(new Date(), 'Y-M-D'),
						establishName: "",
						sortord: "",
						//useruuid:getApp().globalData.UserUUId,//当前登录人uuid
						useruuid:'7FA89CB5-5C41-49DF-9DD7-4ED6282720AF0',
						username:getApp().globalData.UserName,//当前登陆人姓名
					},
				},
				modalNamefuze: null,
				radio: '',
				radiobt: [],
				checkbox: [],
				//参与人
				UserName: "",
				userlist: [],
				usernamelist: '',
				useridlist: '',
				userid: '',
				//负责人
				UserName1: "",
				userlist1: [],
				usernamelist1: '',
				useridlist1: '',
				userid1: '',
			};
		},
		onLoad() {
			//多选人员绑定
			gealluser({
				type: "duo"
			}).then(res => {
				this.checkbox = res.data.data
			})
		},
		methods: {
			//结束时间选择
			DateChange(e) {
				this.formModel.fields.endTime = e.detail.value
			},
			//打开选人窗口(负责人)
			showModalfuze(e) {
				this.modalNamefuze = e.currentTarget.dataset.target
				//单选人员绑定
				gealluser({
					type: "shao"
				}).then(res => {
					this.radiobt = res.data.data
				})
			},
			//打开选人窗口
			showModal(e) {
				this.modalName = e.currentTarget.dataset.target
				this.canyuname = this.formModel.fields.participant = '';
				//单选人员绑定
				gealluser({
					type: "shao"
				}).then(res => {
					this.radiobt = res.data.data
				})
			},
			//确定(关闭选人窗口)
			hideModal(e) {
				this.modalName = null
				this.modalNamefuze = null
				//多选
				for (let i = 0; i < this.checkbox.length; i++) {
					if (this.checkbox[i].checkeds == true) {
						this.canyuname += this.checkbox[i].name + ",";
						this.formModel.fields.participant += this.checkbox[i].value + ",";
					}
				}
			},
			//选择负责人
			RadioChangefuze(e) {
				this.radio = e.detail.value
				this.fuzename = this.radiobt[e.target.value].name;
				this.formModel.fields.principal = this.radiobt[e.target.value].value; //负责人uuid
				console.log(this.radiobt[e.target.value].value)
				this.modalNamefuze = null;
			},
			//选择参与人员
			ChooseCheckbox(e) {
				let items = this.checkbox;
				let values = e.currentTarget.dataset.value;
				for (let i = 0, lenI = items.length; i < lenI; ++i) {
					if (items[i].value == values) {
						items[i].checkeds = !items[i].checkeds;
						break
					}
				}
			},
			tiaozhuan() {
				uni.navigateTo({
					url: '/pages/index/index',
				})
			},
			//任务描述
			textareaBInput(e) {
				this.formModel.fields.priorityDescribe = e.detail.value
			},
			//保存
			submitfrom() {
				// this.formModel.fields.establishName = "超级管理员" //添加人超级管理员先写死
				this.formModel.fields.establishName = getApp().globalData.UserName //添加人超级管理员先写死
/* 				var reg = /^[1-9]\d*$/;
				if (!reg.test(this.formModel.fields.sortord)) {
					uni.showToast({
						title: '排序字段格式不对',
						icon: 'none',
					});
					return;
				}; */
				let valid = this.validateCarDispatchForm();
				if (valid != "true") {
					uni.showModal({
						title: '提示',
						confirmText: "确定",
						cancelText: "取消",
						content: valid,
						showCancel: false
					})
				} else {
					createPriority(JSON.stringify(this.formModel.fields)).then(res => {
						//console.log(res.data)
						if (res.data.code == 200) {
							uni.showModal({
								title: '提示',
								confirmText: "确定",
								cancelText: "取消",
								content: res.data.message,
								showCancel: false,

							})
							uni.redirectTo({
								url: '/pages/index/index'
							})
						} else {
							uni.showModal({
								title: '提示',
								confirmText: "确定",
								cancelText: "取消",
								content: res.data.message,
								showCancel: false
							})
						}
					})
				}
			},
			//非空验证
			validateCarDispatchForm() {
				let _valid = "true";
				if (this.formModel.fields.priorityHeadline == '' || this.formModel.fields.priorityHeadline == null) {
					_valid = "工作标题不能为空";
					return _valid;
				}
				if (this.formModel.fields.priorityDescribe == '' || this.formModel.fields.priorityDescribe == null) {
					_valid = "工作描述不能为空";
					return _valid;
				}
				if (this.formModel.fields.principal == '' || this.formModel.fields.principal == null) {
					_valid = "负责人不能为空";
					return _valid;
				}
				if (this.formModel.fields.participant == '' || this.formModel.fields.participant == null) {
					_valid = "参与人不能为空";
					return _valid;
				}
				if (this.formModel.fields.sortord != '' && this.formModel.fields.sortord != null) {
					var r = /^\+?[1-9][0-9]*$/;　　//正整数
					  if(!r.test(this.formModel.fields.sortord))
					  {
						  _valid = "排序字段需为整数";
						  return _valid;
					}
					
					
				}
				return _valid;
			},
			
			//参与人
			checkchange() {
				let that = this;
				that.$eventHub.$on("fire", function(data) {
					console.log("子页面传过来的");
					console.log(data);
					that.userlist = data;
					if (data.length > 0) {
						for (let item = 0; item < data.length; item++) {
							if (item == 0) {
								that.useridlist = data[item].toString().split('&')[0];
								that.usernamelist = data[item].toString().split('&')[1];
								that.userid = data[item].toString().split('&')[2];
								that.UserName = data[item].toString();
							} else {
								that.useridlist += "," + data[item].toString().split('&')[0];
								that.usernamelist += "," + data[item].toString().split('&')[1];
								that.userid += "," + data[item].toString().split('&')[2];
								that.UserName += "," + data[item].toString();
							}
						}
					} else {
						that.UserName = "";
						that.usernamelist = "";
						that.userid = "";
						that.useridlist = "";
					}
					console.log("参与人id");
					console.log(that.userid);
					that.formModel.fields.participant = that.userid; //参与人id
					console.log(that.formModel.fields.participant);
					//清除监听，不清除会消耗资源
					that.$eventHub.$off('fire');
				})
				console.log("username=");
				console.log(that.UserName);
				uni.navigateTo({
					url: '/pages/index/personnel?username=' + JSON.stringify(encodeURIComponent(that.UserName))
				})
			},
			
			//负责人
			checkchange1() {
				let that = this;
				that.$eventHub.$on("fire", function(data) {
					console.log("子页面传过来的");
					console.log(data);
					that.userlist1 = data;
					if (data.length > 0) {
						for (let item = 0; item < data.length; item++) {
							if (item == 0) {
								that.useridlist1 = data[item].toString().split('&')[0];
								that.usernamelist1 = data[item].toString().split('&')[1];
								that.userid1 = data[item].toString().split('&')[2];
								that.UserName1 = data[item].toString();
							} else {
								that.useridlist1 += "," + data[item].toString().split('&')[0];
								that.usernamelist1 += "," + data[item].toString().split('&')[1];
								that.userid1 += "," + data[item].toString().split('&')[2];
								that.UserName1 += "," + data[item].toString();
							}
						}
					} else {
						that.UserName1 = "";
						that.usernamelist1 = "";
						that.userid1 = "";
						that.useridlist1 = "";
					}
					console.log("负责人uuid");
					console.log(that.usernamelist1);
					console.log(that.useridlist1);
					that.formModel.fields.principal = that.useridlist1; //负责人uuid
					console.log(that.formModel.fields.principal);
					//清除监听，不清除会消耗资源
					that.$eventHub.$off('fire');
				})
				console.log("username1=");
				console.log(that.UserName1);
				uni.navigateTo({
					url: '/pages/index/personnel?username=' + JSON.stringify(encodeURIComponent(that.UserName1))
				})
			},
			
		
		
		
		
		
		}
	}
</script>


<style>
</style>
