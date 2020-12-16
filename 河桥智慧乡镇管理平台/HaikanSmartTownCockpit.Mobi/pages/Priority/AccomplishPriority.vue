<template>
	<view>
		<view class="cu-form-group align-start">
			<view class="title">工作标题：</view>
			<textarea disabled="on" v-model="productList.headline" placeholder="工作标题"></textarea>
		</view>
		<view class="cu-form-group align-start">
			<view class="title">工作描述：</view>
			<textarea disabled="on" v-model="productList.describe" placeholder="工作描述"></textarea>
		</view>
		<view class="cu-form-group">
			<!-- <view class="title">负责人：</view>
			<input name="input" disabled="on">{{productList.principalName}}</input> -->
			<view class="title">负责人：</view>
			<input placeholder="负责人" data-target="Modal" v-model="productList.principalName" name="input" disabled="on"></input>
		</view>
		<view class="cu-form-group align-start">
			<view class="title">参与人：</view>
			<textarea disabled="on" v-model="productList.participant" placeholder="参与人"></textarea>
		</view>
		<view class="cu-form-group">
			<view class="title">创建时间</view>
			<input name="input" disabled="on">{{productList.establishTime}}</input>
		</view>
		<view class="cu-form-group">
			<view class="title">备注</view>
			<input name="input">{{productList.remark}}</input>
		</view>
		<!-- <view class="cu-form-group align-start">
			<view class="title">备注：</view>
			<textarea  disabled="on" v-model="productList.remark" placeholder="备注"></textarea>
		</view> -->
		<view class="cu-form-group">
			<view style="color:#DD514C">此操作是完成该重点工作，重点工作下有任务没完成则不可以完成，是否继续</view>
		</view>

		<view class="padding flex flex-direction">
			<button class="cu-btn bg-red margin-tb-sm lg" :style="productList.issfyc=='是'?' display:none;':type" style="background-color: #0081FF;margin: 2px;;"
			 @click="dogetscorelist(productList.priorityUuid)">确定</button>
			<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF; margin: 2px;" @click="gzqkzjcx">工作情况总结查看</button>
			<button class="cu-btn bg-grey lg" @click="quxiao()">取消</button>
		</view>

	</view>
</template>

<script>
	import {
		getproductList
	} from '@/api/Mail/Index2.js'

	import {
		GetPriorityDetial
	} from '@/api/Mail/Index.js'
	export default {
		data() {
			return {
				isCard: false,
				type:"",
				productList: [],
				id: "",
				uid: "",
			};
		},
		onLoad(option) {
			console.log("页面的传值")
			console.log(option.data);
			let that = this;
			if(option.type=="xiangqing"){
				that.type="display:none;";
			}
			that.uid ='7FA89CB5-5C41-49DF-9DD7-4ED6282720AF';
			// const item = JSON.parse(decodeURIComponent(option.data));
			//console.log(item);
			this.doGetPriorityDetial(option.data, that.uid);
		},

		methods: {
			doGetPriorityDetial(guid, uid) {
				let uuid = guid + ',' + uid;
				console.log(uuid);
				GetPriorityDetial(uuid).then(res => {
					if (res.data.code == 200) {
						this.productList = res.data.data;
					}
				})
			},

			//跳转完成任务
			dogetscorelist(id) {
				console.log(id)
				getproductList(id).then(res => {
					if (res.data.code == "200") {
						uni.showModal({
							title: '提示',
							content: res.data.message,
							confirmText: "确定",
							showCancel: false,
							success: (r) => {
								if (r.confirm) {
									uni.redirectTo({
										url: '/pages/index/index'
									})
								}
							}
						})
					} else {
						uni.showModal({
							title: '提示',
							confirmText: "确定",
							content: res.data.message,
							showCancel: false,
							success: (r) => {}
						})
					}
				});
			},
			quxiao() {
				console.log('123123');
				uni.navigateBack(); //返回
			},
			gzqkzjcx() {
				uni.navigateTo({
					url: '/pages/Priority/PriorityDepartList?gzid=' +this.productList.priorityUuid
				})
			}
		}
	}
</script>

<style>
</style>
