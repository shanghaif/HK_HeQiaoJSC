<template>
	<view style="background-color: #fff;height: 100%;">
		<form>
			<view class="cu-form-group align-start">
				<view class="title">
					<view style="color:red;float: left;">*</view>任务标题：
				</view>
				<textarea v-model="formModel.missionHeadline" placeholder="请输入任务标题"></textarea>
			</view>
			<view class="cu-form-group">
				<view class="title">开始时间：</view>
				<picker mode="date" :value="formModel.startTime" @change="TimeChange">
					<view class="picker">
						{{formModel.startTime}}
					</view>
				</picker>
			</view>
			<view class="cu-form-group">
				<view class="title">结束时间：</view>
				<picker mode="date" :value="formModel.finishTime" @change="DateChange">
					<view class="picker">
						{{formModel.finishTime}}
					</view>
				</picker>
			</view>

			<view class="cu-form-group align-start">
				<view class="title">任务描述：</view>
				<textarea v-model="formModel.missionDescribe" maxlength="-1" :disabled="modalName!=null" @input="textareaBInput"
				 placeholder="请输入任务描述"></textarea>
			</view>
			<view class="cu-form-group ">
				<view class="title">
					<view style="color:red;float: left;">*</view>优先级：
				</view>
				<picker @change="PickerChange" :value="index" :range="picker">
					<view class="picker">
						{{index>-1?picker[index]:'请选择'}}
					</view>
				</picker>
			</view>
			<view style="padding: 0 15px;">
				<view class="title" style="width: 100%;">
					<view style="color:red;float: left;">*</view>执行单位：
				</view>
				<view style="clear: both;overflow: hidden;">
					<view class="officeItem" v-for="(item,index) in officeList" :style="{backgroundColor:isActive[index].bgc,color:isActive[index].color}"
					 @click="chooseOffice(index)">{{item.name}}</view>
				</view>
			</view>

			<view class="cu-modal bottom-modal" :class="modalNamecanyu=='ChooseModal'?'show':''" @tap="hideModal">
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

			<view class="padding flex flex-direction" style="clear: both;">
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF;" @click="dogetscorelist">确定</button>
				<!-- <button class="cu-btn bg-grey lg" @click="fanhui">取消</button> -->
			</view>
		</form>
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		formateDate
	} from '@/global/utils.js';
	import {
		gealluser,
		create,
		getdepartlist
	} from '@/api/taskandapi/taskapis.js';
	import {
		getuserinfos
	} from '@/api/cetiwen/cetiwenlist.js'

	export default {
		data() {
			return {
				officeList: [],
				isActive: [],
				canyuname: '',
				shenpiname: '',
				fuzename: '',
				formModel: {
					priorityUUID: '', //所属重点工作uuid
					missionHeadline: '', //任务标题
					// principal: getApp().globalData.UserUUId, //负责人uuid
					startTime: formateDate(new Date(), 'Y-M-D h:min'), //开始时间
					finishTime: formateDate(new Date(), 'Y-M-D h:min'), //结束时间
					missionDescribe: '', //任务描述
					priority: '', //优先级
					manhour: '', //计划工时
					administrativeOffice: '', //下派科室
					administrativeOffice2:[],//暂时数组
				    establishName: '' ,//创建人uuid
					// useruuid:getApp().globalData.UserUUId,//当前登录人uuid
					// username:getApp().globalData.UserName,//当前登陆人姓名
				},
				index: -1, //优先级默认普通
				picker: ['普通', '紧急'],
				imgList: [],
				modalName: null,
				modalNamecanyu: null,
				modalNamefuze: null,
				radio: '',
				radiobt: [],
				checkbox: [],

			};
		},
		mounted() {
			// this.formModel.establishName=getApp().globalData.UserUUId;
			this.formModel.establishName='7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
			this.formModel.administrativeOffice=[];
			getdepartlist().then(res => {
				this.officeList = [];
				this.isActive = [];
				console.log(res.data.data)
				for (var i = 0; i < res.data.data.length; i++) {
					console.log('进入添加')
					this.officeList.push({
						name: res.data.data[i].name,
						uuid: res.data.data[i].id
					});
					this.isActive.push({
						bgc: '#fff',
						color: '#333'
					})
					this.isActive[0].bgc = '#007AFF'
					this.isActive[0].color = '#fff'
				}
				this.formModel.administrativeOffice2.push(this.officeList[0].uuid);
			})
		},
		methods: {
			chooseOffice(index) {
				if (this.isActive[index].bgc == '#007AFF') {
					console.log('here')
					this.isActive[index].bgc = '#fff'
					this.isActive[index].color = '#333'
					// this.formModel.administrativeOffice2.splice(index,1
					// console.log(this.formModel.administrativeOffice2) 
				} else {
					this.isActive[index].bgc = '#007AFF'
					this.isActive[index].color = '#fff'
					// this.formModel.administrativeOffice2.push(this.officeList[index].uuid);
					// console.log(this.formModel.administrativeOffice2)
				}
				this.formModel.administrativeOffice2=[];
				for(var i=0;i<this.isActive.length;i++){
					// console.log(this.isActive[i].bgc)
					if(this.isActive[i].bgc == '#007AFF'){
						// console.log('我进来了'+i+'次')
						// console.log(this.deparmentList[i])
						let params=this.officeList[i].uuid
						this.formModel.administrativeOffice2.push(params)
					}
				}
				console.log(this.formModel.administrativeOffice2)
			},
			//开始时间选择
			TimeChange(e) {
				this.formModel.startTime = e.detail.value
			},
			//结束时间选择
			DateChange(e) {
				this.formModel.finishTime = e.detail.value
			},
			//任务描述
			textareaBInput(e) {
				this.formModel.missionDescribe = e.detail.value
			},
			//优先级下拉框选择
			PickerChange(e) {
				this.index = e.detail.value
				this.formModel.priority = this.picker[this.index]
			},
			//表单验证
			panduan() {
				console.log(123123)
				console.log(this.formModel)
				if (this.formModel.missionHeadline === "") {
					console.log('i am here')
					uni.showToast({
						icon: 'none',
						title: "请输入任务标题",
					})
					return false
				}
				if (this.formModel.startTime >= this.formModel.finishTime) {
					uni.showToast({
						icon: 'none',
						title: "开始时间不能大于或等于结束时间",
					})
					return false
				}
				if (this.formModel.priority == "") {
					uni.showToast({
						icon: 'none',
						title: "请选择优先级",
					})
					return false
				}
				return true
			},
			//确定提交
			dogetscorelist() {
				
				if (this.panduan()) {
					this.formModel.administrativeOffice='';
					for(var i=0;i<this.formModel.administrativeOffice2.length;i++){
						this.formModel.administrativeOffice+=this.formModel.administrativeOffice2[i]+',';
					}
					uni.showLoading()
					create(this.formModel).then(res => {
						console.log(111111)
						console.log(res)
						if (res.data.code === 200) {
							uni.showToast({
								title: "添加成功!",
								icon: "success",
								showCancel: false
							})
							console.log("添加成功")
							uni.hideLoading()
							//uni.navigateBack(); //返回
						uni.navigateTo({
							url: '/pages/index/index'
						})

						} else {
							uni.showModal({
								title: "添加失败!",
								showCancel: false,
								buttonText: "确定",
								success: (res) => {
									uni.navigateBack(); //返回
								}
							})
							uni.hideLoading()
						}
					})
				}

			},
			//取消
			// fanhui() {
			// 	uni.navigateTo({
			// 		url: '/pages/index/index'
			// 	})
			// },
		}
	}
</script>
<style>
	page {
		height: 100%;
	}

	.officeItem {
		margin: 10px 0;
		width: 31%;
		margin: 7px 1%;
		float: left;
		text-align: center;
		height: 40px;
		line-height: 40px;
		font-size: 14px;
		/* background-color: #007AFF; */
		border: 1px solid #ccc;
		border-radius: 4px;
	}

	.active {
		background-color: #007AFF;
		color: #fff;
	}
</style>
