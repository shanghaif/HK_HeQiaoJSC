<template>
	<view style="margin: 0 10px;">
		<view v-for="(item,index) in messageList">
			<view style="display:flex;margin:10px;line-height:2;">
				<view class="title">事件发生地：</view>
				<text disabled="on" style="width: 70%;" name="input" placeholder="请输入事件发生地">{{item.eventAddress}}</text>
			</view>
			<view style="display:flex;margin:10px;line-height:2;">
				<view class="title">事件描述：</view>
				<text style="width: 70%;" placeholder="事件描述" disabled="on">{{item.eventContent}}</text>
			</view>
			<view style="display:flex;margin:10px;line-height:2;">
				<view class="title">事件状态：</view>
				<input style="width: 70%;" placeholder="事件状态" v-model="item.eventStatus" name="input" disabled="on"></input>
			</view>
			<view style="display:flex;margin:10px;line-height:2;">
				<view class="title">事件类型：</view>
				<input style="width: 70%;" placeholder="事件类型" v-model="item.eventTypeId" name="input" disabled="on"></input>
			</view>
			<view style="display:flex;margin:10px;line-height:2;">
				<view class="title">事件开始时间：</view>
				<text style="width: 70%;" disabled="on" name="input" placeholder="事件开始时间">{{item.startTime}}</text>
			</view>
			<view class="padding flex flex-direction">
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF; margin: 2px;" @click="submit">确定</button>
				<button class="cu-btn bg-red margin-tb-sm lg" style="background-color: #0081FF;" @click="dogetscorelist">返回</button>
			</view>
		</view>
	</view>

</template>

<script>
	import {
		formateDate
	} from '@/global/utils.js';
	import {
		GetPriorityDetial,
		editDetial
	} from '@/api/Mail/Index.js'
	export default {
		data() {
			return {
				modalName: null,
				messageList: [],
				uuids: ''

			};
		},
		onLoad(e) {
			this.uuids = e.uuid;
			console.log(e.uuid)
			GetPriorityDetial(e.uuid).then(res => {
				console.log("返回的数据")
				console.log(res)
				this.messageList = res.data.data
			})
		},
		methods: {
			//确定返回
			dogetscorelist() {
				uni.navigateBack(); //返回
				//uni.redirectTo();
			},
			submit() {
				editDetial(this.uuids).then(res => {
					uni.showToast({
						title: "提示",
						content: '已查阅',
						showCancel: false
					})
					uni.redirectTo({
						url: './PriorityShow'
					})
				});
				this.backPage()
			},
			backPage(){
				uni.navigateBack();
			}
		}
	}
</script>

<style>
	page{
		background-color: #fff;
	}
	.title {
		width: 30%;
	}
</style>
