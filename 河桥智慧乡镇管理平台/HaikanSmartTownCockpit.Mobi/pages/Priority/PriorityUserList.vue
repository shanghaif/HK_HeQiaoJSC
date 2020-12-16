<template>
	<view>
		<scroll-view class="page" style="color: #000000;">
			<view class="cu-list menu">
				<checkbox-group class="cu-list menu">
					<view class="cu-item" v-for="(item,index) in checkbox" :key="index" @click="getuser(item.systemUserUuid)">
						<view class="action">
							<text class="text-grey" style="margin-left: 5px;color:#000; margin-top: 2px;display: inline-block;">{{item.realName.length>15?item.realName.substring(0,15)+'...':item.realName}}</text>
						</view>
						<view class="action">
							>
						</view>
					</view>
				</checkbox-group>
			</view>
		</scroll-view>
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		getuserprioritylist2,
	} from '@/api/cetiwen/cetiwenlist.js'


	export default {
		data() {
			return {
				checkbox: [],
				uuid: '',
				gzid: ''
			}
		},
		onLoad(option) {
			let that = this;
			that.uuid = option.gzid + '&' + option.depid;
			that.gzid = option.gzid;
			that.userid=option.userid;
			console.log("that.uuid:" + that.uuid);
			that.getuserlist();
		},
		methods: {
			getuserlist() {
				let that = this;
				console.log("that.uuid:" + that.uuid);
				getuserprioritylist2(that.userid).then(res => {
					console.log("日志信息：");
					console.log(res);
					that.checkbox = res.data.data;
				})
			},
			getuser(guid) {
				let that = this;
				uni.navigateTo({
					url: '/pages/Priority/PriorityGZHJList?userid=' + guid + '&gzid=' + that.gzid
				})
			}
		},

	}
</script>

<style>
	@import '@/colorui/uni-nvue.scss';

	#button_div {
		width: 100%;
		background: #FFFFFF;
		background-color: #FFFFFF;
		border-top: 1px solid #ddd;
		position: fixed;
		bottom: 0px;
	}

	#button_divs {
		width: 100%;
		background: #FFFFFF;
		background-color: #FFFFFF;
		position: fixed;
		bottom: 0px;
	}

	.content {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
	}

	.logo {
		height: 200rpx;
		width: 200rpx;
		margin-top: 200rpx;
		margin-left: auto;
		margin-right: auto;
		margin-bottom: 50rpx;
	}

	.text-area {
		display: flex;
		justify-content: center;
	}

	.title {
		font-size: 36rpx;
		color: #8f8f94;
	}

	.cu-form-group .title {
		min-width: calc(4em + 15px);
	}

	.page {
		border-bottom: 0.5px solid #ddd;
	}

	.uni-button {
		clear: all;
	}

	.header {
		/* #ifndef APP-NVUE */
		display: flex;
		/* #endif */
		flex-direction: row;
		padding: 10px 15px;
		align-items: center;
		border-top-width: 1px;
		border-top-color: #f5f5f5;
		border-top-style: solid;
		background-color: #ffffff;
	}

	.input-view {
		/* #ifndef APP-NVUE */
		display: flex;
		/* #endif */
		align-items: center;
		flex-direction: row;
		background-color: #e7e7e7;
		height: 30px;
		border-radius: 15px;
		padding: 0 10px;
		flex: 1;
		background-color: #f5f5f5;
	}

	.uni-drawer-info {
		background-color: #ffffff;
		padding: 15px;
		padding-top: 5px;
		color: #3b4144;
	}

	.uni-padding-wrap {
		padding: 0 15px;
		line-height: 1.8;
	}

	.input {
		flex: 1;
		padding: 0 5px;
		height: 24px;
		line-height: 24px;
		font-size: $uni-font-size-base;
		background-color: transparent;
	}

	.close {
		padding: 15px;
	}

	.example-body {
		/* #ifndef APP-NVUE */
		display: flex;
		/* #endif */
		flex-direction: row;
		padding: 0;
	}

	.draw-cotrol-btn {
		flex: 1;
	}

	.info {
		padding: 15px;
		color: #666;
	}

	.info-text {
		font-size: 14px;
		color: #666;
	}

	.scroll-view {
		/* #ifndef APP-NVUE */
		width: 100%;
		height: 100%;
		/* #endif */
		flex: 1
	}

	// 处理抽屉内容滚动
	.scroll-view-box {
		flex: 1;
		position: absolute;
		top: 0;
		right: 0;
		bottom: 0;
		left: 0;
	}

	.info-content {
		padding: 5px 15px;
	}

	.cu-list.menu>.cu-item:last-child::after {
		border-bottom: 0.5px solid #ddd
	}
</style>
