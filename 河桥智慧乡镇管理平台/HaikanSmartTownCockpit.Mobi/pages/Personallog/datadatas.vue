<template>
	<view>
		<view class="cu-form-group align-start">
			<view class="title">个人日志标题：</view>
			<textarea disabled="on" v-model="formModel.fields.headline" placeholder="已完成"></textarea>
		</view>
		<view class="cu-form-group align-start">
			<view class="title">类型：</view>
			<textarea disabled="on" v-model="formModel.fields.type" placeholder="已完成"></textarea>
		</view>
		<view class="cu-form-group align-start">
			<view class="title">内容：</view>
			<textarea disabled="on" v-model="formModel.fields.content"></textarea>
		</view>
		<view class="cu-form-group">
			<view class="title">撰写时间：</view>
			<input name="input" disabled="on">{{formModel.fields.time}}</input>
		</view>
		
		<view class="cu-form-group">
			<view class="title">创建人</view>
			<input name="input" disabled="on">{{formModel.fields.adduser}}</input>
		</view>

		<view>
			<view class="cu-bar bg-white">
				<view class="action">附件：</view>
			</view>
			<view class="cu-form-group">
				<image :src="formModel.fields.accessory"></image>
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
		persondata,
	} from '@/api/personalapi/personal.js'
	
	export default {
		data() {
			return {
				formModel: {
					fields: {
						headline: '',
						time: '',
						content: '',
						adduser: '',
						accessory:'',
						type:''
					},
				},
			};
		},
		onLoad(option) {
			//const item = JSON.parse(decodeURIComponent(option.data));
			const item=option;
			console.log(item);
			persondata({uuid:item.uuid}).then(res => {
				console.log("日志信息：");
				console.log(res);
				this.formModel.fields = res.data.data;
				this.formModel.fields.accessory=http.baseUrl + 'UploadFiles/PersonalDiary/' + res.data.data.accessory;
			});
			
			// this.formModel.fields.headline = item.headline;
			// this.formModel.fields.time = item.time;
			// this.formModel.fields.content = item.content;
			// this.formModel.fields.establishName = item.adduser;
			// this.formModel.fields.accessory = http.baseUrl + 'UploadFiles/PersonalDiary/' + item.accessory;
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