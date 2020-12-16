<template>
	<view>
		<scroll-view class="page" style="color: #000000;">
			<view class="cu-list menu">
				<checkbox-group class="cu-list menu">
					<view class="cu-item" v-for="(item,index) in checkbox" :key="index" @click="getuser(item.uuid)">
						<view class="action">
							<text class="text-grey" style="margin-left: 5px;color:#000; margin-top: 2px;display: inline-block;">{{item.realname.length>15?item.realname.substring(0,15)+'...':item.realname}}</text>
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
	import {
		userdatalist,//人员
	} from '@/api/personalapi/personal.js'


	export default {
		data() {
			return {
				checkbox: [],
				depid: '',
			}
		},
		onLoad(option) {
			this.getuserlist(option.depid);
		},
		methods: {
			getuserlist(depid) {
				console.log("科室uuid"+depid)
				userdatalist(depid).then(res => {
					console.log("日志信息：");
					console.log(res);
					this.checkbox = res.data.data;
				})
			},
			getuser(userid) {
				let that = this;
				uni.navigateTo({
					url: '/pages/Personallog/datalist?userid=' + userid
				})
			}
		},

	}
</script>

<style>
</style>
