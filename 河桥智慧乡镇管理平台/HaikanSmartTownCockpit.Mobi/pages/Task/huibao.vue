<template>
	<view>
		<form>
			<view class="cu-form-group">
				<view class="title" style="width: 28%;"><view style="color:red;float: left;">*</view>已完成：</view>

				<textarea v-model="formModel.fields.completed" placeholder="请输入已完成的任务" name="input" style="width: 70%;"></textarea>
			</view>
			<view class="cu-form-group">
				<view class="title" style="width: 28%;"><view style="color:red;float: left;">*</view>需要协调：</view>
				<textarea v-model="formModel.fields.coordination" placeholder="请输入需要协调的任务" name="input" style="width: 70%;"></textarea>
			</view>
			<view>
				<view class="cu-bar bg-white">
					<view class="action">附件：</view>
				</view>
				<view class="cu-form-group">
					<view class="grid col-4 grid-square flex-sub">
						<view class="bg-img" v-for="(item,index) in imgList" :key="index" @tap="ViewImage" :data-url="imgList[index]">
							<image :src="imgList[index]" mode="aspectFill"></image>
							<view class="cu-tag bg-red" @tap.stop="DelImg" :data-index="index">
								<text class='cuIcon-close'></text>
							</view>
						</view>
						<view class="solids" @tap="ChooseImage" v-if="imgList.length<1">
							<text class='cuIcon-cameraadd'></text>
						</view>
					</view>
				</view>
			</view>


			<view class="padding flex flex-direction">
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF;" @click="submitfrom">确定</button>
				<button class="cu-btn bg-grey lg" @click="tiaozhuan">取消</button>
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
		createPersonal2,
	} from '@/api/personalapi/personal.js'
	export default {
		data() {
			return {
				time: formateDate(new Date(), 'Y-M-D'),
				date: formateDate(new Date(), 'Y-M-D'),
				url: "",
				imgList: [],
				modalName: null,
				textareaBValue: '',
				formModel: {
					fields: {
						missionUUID: "",
						completed: "",
						coordination: "",
						accessory: "",
						establishName: "",
					},
				},
			};
		},
		onLoad(e) {
			this.formModel.fields.missionUuid = e.uuid
			console.log(this.formModel.fields.missionUuid);
		},
		methods: {
			tiaozhuan() {
				uni.navigateTo({
					url: '/pages/index/index',
				})
			},
			//选择图片
			ChooseImage() {

				uni.chooseImage({
					count: 1, //默认9
					success: (res) => {
						if (this.imgList.length >= 0) {
							this.imgList = this.imgList.concat(res.tempFilePaths)
							this.formModel.fields.accessory = this.imgList[0];
						} else {
							this.imgList = res.tempFilePaths
						}
						const tempFilePaths = res.tempFilePaths; //拿到选择的图片，是一个数组
						console.log("进入")
						uni.uploadFile({
							url: http.baseUrl + 'api/v1/personal/app/personalDiaryApp/registPicture', //post请求的地址
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
								this.formModel.fields.accessory = obj.path;
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
				uni.showModal({
					title: '删除',
					content: '确定要删除吗？',
					cancelText: '取消',
					confirmText: '确定',
					success: res => {
						if (res.confirm) {
							this.imgList.splice(e.currentTarget.dataset.index, 1)
						}
					}
				})
			},
			//保存
			submitfrom() {
				this.formModel.fields.establishName='7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
				//this.formModel.fields.establishName = getApp().globalData.UserUUId; //添加人超级管理员先写死
				console.log(this.formModel.fields.establishName);
				let valid = this.validateCarDispatchForm();
				console.log("验证")
				console.log(valid)
				if (valid != "true") {
					uni.showModal({
						title: '提示',
						confirmText: "确定",
						cancelText: "取消",
						content: valid,
						showCancel: false
					})
				} else {
					createPersonal2(JSON.stringify(this.formModel.fields)).then(res => {
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
				if (this.formModel.fields.completed == '' || this.formModel.fields.completed == null||this.formModel.fields.coordination == '' || this.formModel.fields.coordination == null) {
					_valid = "已完成和需要协调不能为空";
					return _valid;
				} 
				return _valid;
			}

		}
	}
</script>
<style>
</style>
