<template>
	<view>
		<form>
			<!-- <view class="cu-form-group">
				<view class="title">重点工作：</view>
				<input placeholder="所属重点工作" style="width: 70%;" v-model="formModel.priorityname" name="input" disabled="on"></input>
			</view> -->
			<view class="cu-form-group">
				<view class="title">任务标题：</view>
				<text disabled="on" style="width: 70%;" name="input">{{formModel.missionHeadline}}</text>
			</view>
			<view class="cu-form-group">
				<view class="title">负责人：</view>
				<text style="width: 70%;" placeholder="负责人" disabled="on">{{formModel.principalname}}</text>
			</view>
			<view class="cu-form-group">
				<view class="title">开始时间：</view>
				<text style="width: 70%;color:#333 !important;" name="input" disabled="on">{{formModel.startTime}}</text>
			</view>
			<view class="cu-form-group">
				<view class="title">结束时间：</view>
				<text style="width: 70%;" placeholder-style="color:#333;" name="input" disabled="on">{{formModel.finishTime}}</text>
			</view>
			<view class="cu-form-group">
				<view class="title">任务描述：</view>
				<text style="width: 70%;" disabled="on" name="input" placeholder="任务描述">{{formModel.missionDescribe}}</text>
			</view>
			<view class="cu-form-group ">
				<view class="title">优先级：</view>
				<text style="width: 70%;" placeholder-style="color:#333;" placeholder="优先级" name="text" disabled="on">{{formModel.priority}}</text>
			</view>
			<view class="cu-form-group">
				<view class="title">参与人：</view>
				<text style="width: 70%;" placeholder-style="color:#333;" placeholder="参与人" name="input" disabled="on">{{formModel.selectcanyurenname}}</text>
			</view>
			<view class="cu-form-group">
				<view class="title">备注：</view>
				<text style="width: 70%;word-break:break-all;" placeholder-style="color:#333;" name="input" placeholder="备注" disabled="on">{{formModel.remark}}</text>
			</view>
			<view class="padding flex flex-direction">
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF; margin: 2px;" @click="rwqkzjcx">查看工作汇报</button>
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF;" @click="dogetscorelist">返回</button>
			</view>
		</form>
	</view>

</template>

<script>
	import {
		formateDate
	} from '@/global/utils.js';
	import {
		loaddata,
		appwanchen
	} from '@/api/taskandapi/taskapis.js'
	export default {
		data() {
			return {
				show1: '',
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
					auditOpinion: "", //审核意见
				},
				uuids: ''

			};
		},
		onLoad(e) {
			this.uuids = e.muuid;
			loaddata({
				uuid: e.muuid
			}).then(res => {
				console.log("返回的数据")
				console.log(res)
				this.formModel = res.data.data
			})
			var arr = e.pauuid.split(',');

			arr.forEach(function(item, index) {

			})
		},
		methods: {
			//查看参与人
			showModal(e) {
				this.modalName = e.currentTarget.dataset.target
			},
			hideModal(e) {
				this.modalName = null
			},
			//确定返回
			dogetscorelist() {
				uni.navigateBack(); //返回
				//uni.redirectTo();
			},
			rwqkzjcx() {
				console.log(this.uuids)
				uni.navigateTo({
					url: '/pages/Task/MyTaskzonjie?rwid=' + this.uuids
				})
			}
		}
	}
</script>

<style>
	.cu-form-group .title {
		min-width: calc(4em + 15px);
		width: 28%;
		text-align: right;
	}
</style>
