<template>
	<view>
		<view class="cu-form-group align-start">
			<view class="title">已完成：</view>
			<textarea disabled="on" v-model="formModel.fields.ywcinfos" placeholder="已完成"></textarea>
		</view>
		<view class="cu-form-group align-start">
			<view class="title">需要协调：</view>
			<textarea disabled="on" v-model="formModel.fields.xyxtinfo"></textarea>
		</view>
		<view class="cu-form-group">
			<view class="title">创建时间</view>
			<input name="input" disabled="on">{{formModel.fields.addtimes}}</input>
		</view>

		<view>
			<view class="cu-bar bg-white">
				<view class="action">附件：</view>
			</view>
			<view class="cu-form-group">
				<image :src="formModel.fields.fjinfo"></image>
			</view>
		</view>
		<view class="padding flex flex-direction">
			<button class="cu-btn bg-grey lg" @click="quxiao()">返回</button>
		</view>
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		renwuzongjie,
	} from '@/api/taskandapi/taskapis.js'
	
	export default {
		data() {
			return {
				formModel: {
					fields: {
						addtimes: '',
						xyxtinfo: '',
						ywcinfos: '',
						fjinfo: ''
					},
				},
			};
		},
		onLoad(option) {
			//const item = JSON.parse(decodeURIComponent(option.data));
			const item=option;
			renwuzongjie({uuid:item.uid}).then(res => {
				this.formModel.fields=res.data.data;
				this.formModel.fields.fjinfo = http.baseUrl + 'UploadFiles/PersonalDiary/' + res.data.data.fjinfo
				console.log(this.formModel.fields.fjinfo)
			});
			
			// this.formModel.fields.ywcinfos = item.ywcinfos;
			// this.formModel.fields.xyxtinfo = item.xyxtinfo;
			// this.formModel.fields.addtimes = item.addtimes;
			// this.formModel.fields.fjinfo = http.baseUrl + 'UploadFiles/PersonalDiary/' + item.fjinfo;
			// console.log(this.formModel.fields.fjinfo)
		},

		methods: {
			quxiao() {
				uni.navigateBack(); //返回
			}
		}
	}
</script>

<style>
</style>
