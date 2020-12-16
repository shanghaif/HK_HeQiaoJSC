<template>
	<view>
		<form>

			<view class="cu-form-group">
				<view class="title" style="width: 28%;">
					标题：
				</view>
				<input v-model="formModel.fields.headline" placeholder="请输入标题" name="input" style="width: 70%;"></input>
			</view>
			<view class="cu-form-group">
				<view class="title" style="width: 30%;">撰写时间</view>
				<view style="width: 70%;">
					<!-- <picker  mode="date" :value="formModel.fields.writeTime" @change="TimeChange" placeholder="请选择时间"> -->
					<view class="picker">
						{{formModel.fields.writeTime}}
					</view>
					<!-- </picker> -->
				</view>
			</view>
			<view class="cu-form-group">
				<view class="title" style="width: 30%;">类型</view>
				<view style="width: 70%;">
					<checkbox-group @change="checkchange">
						<label v-for="(item, index) in checkboxList" style="font-size: 12px;">
						<checkbox :value="item.name"/>
							<text>{{ item.name }}</text>
							</label>
					</checkbox-group>
				</view>
			</view>
			<view class="cu-form-group align-start">
				<view class="title" style="width: 30%;">
					内容
				</view>
				<textarea maxlength="-1" :disabled="modalName!=null" @input="textareaBInput" placeholder="请输入内容" v-model="formModel.fields.content"
				 style="width: 70%;"></textarea>
			</view>

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

			<view class="padding flex flex-direction">
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF;" @click="submitWancheng">完成</button>
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
		createPersonal,
	} from '@/api/personalapi/personal.js'
	export default {
		data() {
			return {
				checkboxList: [
					{
						name: '领导交办',
						checked: false,
						disabled: false
					},
					{
						name: '日常工作',
						checked: false,
						disabled: false
					}
				],
				time: formateDate(new Date(), 'Y-M-D'),
				date: formateDate(new Date(), 'Y-M-D'),
				url: "",
				imgList: [],
				modalName: null,
				textareaBValue: '',
				formModel: {
					fields: {
						headline: "",
						content: "",
						writeTime: formateDate(new Date(), 'Y-M-D'),
						accessory: "",
						missionUuid:'',
						establishName:'',
						type:'',
					},
				},
			};
		},
		onLoad(option) {
			this.formModel.fields.missionUuid=option.uuid;
			// this.formModel.fields.establishName=getApp().globalData.UserUUId;
			this.formModel.fields.establishName='7FA89CB5-5C41-49DF-9DD7-4ED6282720AF'
			console.log(this.formModel.fields.establishName)
			console.log(this.formModel.fields.establishName)
			//this.formModel.fields.establishName=getApp().globalData.UserUUId;
		},
		methods: {
			tiaozhuan() {
				uni.navigateTo({
					url: '/pages/index/index',
				})
			},
			//时间选择
			TimeChange(e) {
				this.formModel.fields.writeTime = e.detail.value
			},
			//任务描述
			textareaBInput(e) {
				this.textareaBValue = e.detail.value
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

			checkchange(e) {
				this.formModel.fields.type = "";
				var list = e.detail.value;
				for (var i = 0; i < list.length; i++) {
					this.formModel.fields.type += list[i] + ',';
				}
				console.log(this.formModel.fields.type)
			},


			//保存
			submitfrom() {
				console.log("保存")
				
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
					//JSON.stringify(this.formModel.fields)
					createPersonal(JSON.stringify(this.formModel.fields)).then(res => {
						console.log(res)
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
				if (this.formModel.fields.headline == '' || this.formModel.fields.headline == null) {
					_valid = "标题不能为空";
					return _valid;
				}
				if (this.formModel.fields.type == '' || this.formModel.fields.type == null) {
					_valid = "请选择类型";
					return _valid;
				}
				if (this.formModel.fields.content == '' || this.formModel.fields.content == null) {
					_valid = "内容不能为空";
					return _valid;
				}
				return _valid;
			}

		}
	}
</script>


<style>
</style>
