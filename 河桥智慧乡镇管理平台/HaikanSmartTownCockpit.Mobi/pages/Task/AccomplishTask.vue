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
				<input placeholder="负责人" v-model="formModel.principalname" name="input"></input>
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

			<view class="cu-form-group">
				<view class="title">参与人：</view>
				<input placeholder="参与人" @click="showModal" data-target="Modal" v-model="formModel.selectcanyurenname" name="input"
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
			<view class="cu-form-group">
				<view class="title">备注：</view>
				<input placeholder="请输入备注" v-model="formModel.remark" name="input"></input>
			</view>

			<view class="cu-form-group">
				<view style="color:#DD514C">此操作是完成该任务，是否继续</view>
			</view>

			<view class="padding flex flex-direction">
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF;" @click="dogetscorelist">确定</button>
				<button class="cu-btn bg-grey lg" @click="navigateBack">取消</button>
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
				type: "",
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
				}

			};
		},
		onLoad(e) {
			this.type = e.type
			loaddata({
				uuid: e.uuid
			}).then(res => {
				console.log(res)
				this.formModel = res.data.data
			})
		},
		methods: {
			//查看参与人
			showModal(e) {
				console.log(e)
				this.modalName = e.currentTarget.dataset.target
			},
			hideModal(e) {
				this.modalName = null
			},
			//确定完成任务
			dogetscorelist() {
				uni.showModal({
					title: "提示",
					content: "确定完成此任务?",
					confirmText: "确定",
					cancelText: "取消",
					success: (res) => {
						if (res.confirm) { //确定完成
							uni.showLoading()

							//JSON.stringify(this.formModel)
							appwanchen(JSON.stringify(this.formModel)).then(res => {
								if (res.data.code === 200) {
									var tishi = "已提交完成,请等待审批人审批!";
									if (this.formModel.approvername == "无") {
										tishi = "操作成功";
									}
									uni.showToast({
										title: "提示",
										content: tishi,
										showCancel: false
									})
									uni.hideLoading()
									uni.redirectTo({
										url: '/pages/Task/MyTask'
									})

								} else {
									uni.showToast({
										title: "提示",
										content: "提交完成出错!",
									})
									uni.hideLoading()
									uni.navigateBack(); //返回
								}
							})

						}
					}

				})
			},
			//取消
			navigateBack() {
				uni.navigateBack(); //返回
			}




		}
	}
</script>

<style>
	.cu-form-group .title {
		min-width: calc(4em + 15px);
	}
</style>
