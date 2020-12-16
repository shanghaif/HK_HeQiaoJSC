<template>
	<view>
		<scroll-view class="page" :style="butonss" style="color: #000000;">
			<view class="cu-list menu">
				<view class="cu-item">
					<view class="action">
						<checkbox-group @change="allChoose">
							<label>
								<checkbox class='round' value="all" :class="{'checked':allChecked}" :checked="allChecked?true:false" style="font-size: 14px; width: 20px; height: 20px;"></checkbox>
								<text style="margin-left: 5px;color:#000; display: inline-block;">全选</text>
							</label>
						</checkbox-group>
					</view>
				</view>
				<checkbox-group class="cu-list menu" @change="changeCheckbox">
					<view class="cu-item" v-for="(item,index) in checkbox" :key="index">
						<view class="action">
							<checkbox class='round' :value="item.id+'&'+item.name" :checked="checkedArr.includes(item.id+'&'+item.name)"
							 style="font-size: 14px; width: 20px; height: 20px;"></checkbox>
							<text class="text-grey" style="margin-left: 5px;color:#000; margin-top: 2px;display: inline-block;">{{item.name}}（{{item.count}}人）</text>
						</view>
						<view class="action">
							<text class="text-grey text-sm" style="color: #000000;">
								<button @click="showDrawer('showRight')" :disabled="checkedArr.includes(item.id+'&'+item.name)" :style="checkedArr.includes(item.id+'&'+item.name)==true?'color:#C0C0C0':'color:rgb(0, 122, 255)'"
								 style="    color: rgb(0, 122, 255);    display: inline-block; background: #FFFFFF; height: 15px; line-height: 15px; border: 0px;   font-size: 14px;    margin: auto;    position: initial;    overflow: inherit;    padding: inherit;    line-height: initial;">下级</button></text>
						</view>
					</view>
				</checkbox-group>
			</view>
		</scroll-view>

		<uni-drawer ref="showRight" mode="right" :mask-click="false" @change="change($event,'showRight')">
			<view class="scroll-view" :style="">
				<scroll-view class="scroll-view-box" scroll-y="true">
					<view class="cu-list menu">
						<view class="cu-item">
							<view class="action">
								<checkbox-group @change="allChooses">
									<label>
										<checkbox class='round' value="alls" :class="{'checked':allCheckeds}" :checked="allCheckeds?true:false" style="font-size: 14px; width: 20px; height: 20px;"></checkbox>
										<text style="margin-left: 5px;color:#000; display: inline-block;">全选</text>
									</label>
								</checkbox-group>
							</view>
						</view>
					</view>
					<checkbox-group class="cu-list menu" @change="changeCheckboxs" style="border-top: 0.5px solid #DDDDDD;margin-top:0px;">
						<view class="cu-item" v-for="(item,index) in checkboxs" :key="index">
							<view class="action">
								<checkbox class='round' :value="item.id+'&'+item.name" :checked="checkedArrs.includes(item.id+'&'+item.name)"
								 style="font-size: 14px; width: 20px; height: 20px;"></checkbox>
								<text style="margin-left: 5px;color:#000; ">{{item.name}}</text>
							</view>
						</view>
					</checkbox-group>

					<div id="button_divs">
						<view class="close" style="padding: 0px;">
							<view class="word-btn" hover-class="word-btn--hover" :hover-start-time="20" :hover-stay-time="70" @click="closeDrawer('showRight')"><text
								 class="word-btn-white">确定</text></view>
						</view>
					</div>
				</scroll-view>
			</view>

		</uni-drawer>
		<div id="button_div">
			<view class="cu-list menu">
				<view class="cu-item" style="padding: 0px;">
					<view class="action" style="color: #007AFF;float: left; margin-left: 5px;">已选择：{{yjxzdsr}}人</view>
					<button @click="qsdanniu()" :disabled="qddisabled" :style="qdbutton" class="action" style="width: 130px; background: #007AFF; color: #FFFFFF;float: left; margin-right: 5px;">确定({{qdyjxzdsr}}/1000)</button>
				</view>
			</view>
		</div>
	</view>
</template>

<script>
	import http from '@/components/utils/http.js'
	import {
		getuserinfos
	} from '@/api/cetiwen/cetiwenlist.js'


	export default {
		data() {
			return {
				showMask: false,
				mode: "single",
				hashFirst: false,
				chooseList: [],
				checkbox: [{
					id: '1',
					name: "运营部",
					count: '2'
				}, {
					id: '2',
					name: "设计部",
					count: '0'
				}, {
					id: '1',
					name: "产品部",
					count: '2'
				}, {
					id: '4',
					name: "人事部",
					count: '3'
				}, {
					id: '5',
					name: "行政部",
					count: '2'
				}, {
					id: '6',
					name: "技术部",
					count: '1'
				}],
				checkboxs: [{
					id: '1',
					name: "王勾践"
				}, {
					id: '2',
					name: "刘建军"
				}, {
					id: '3',
					name: "刘欢欢"
				}, {
					id: '4',
					name: "刘海华"
				}],
				disableds: '',
				checkedArr: [], //复选框选中的值(部门)				
				checkedArrs: [], //复选框选中的值（下级人）
				ids: '',
				allChecked: false,
				allCheckeds:false,
				yjxzdsr: 0,
				qdyjxzdsr: 0,
				qddisabled: 'disabled',
				qdbutton: 'background:#ddd',
				butonss: 'margin-bottom: 45px;'
			}
		},
		onLoad(option) {
			let that = this;
			console.log("父页面传过来的");
			console.log(option.username);
			const item = JSON.parse(decodeURIComponent(option.username));
			console.log(item);
			if (item != "") {
				var listityem = item.toString().split(',');
				if (listityem.length > 0) {
					for (let i = 0; i < listityem.length; i++) {
						if (!that.checkedArrs.includes(listityem[i])) {
							that.checkedArrs.push(listityem[i]);
						}
					}
				}
			}
			that.bindxzr();
			// dd.getAuthCode({
			// 	success: (res) => {
			// 		var code = res.authCode; //授权码
			// 		console.log("很高兴获得了授权请求码：", code);
			// 		console.log("appkey：", this.appkey);
			// 		getuserinfos(code).then(res => {
			// 			console.log("日志信息：", res);
			// 		})
			// 	}
			// });
		},
		methods: {
			CheckboxChange(e) {
				console.log(e);
			},
			// 多选复选框改变事件
			changeCheckbox(e) {
				let that = this;
				that.checkedArr = e.detail.value;
				console.log(e.detail.value);
			},
			changeCheckboxs(e) {
				let that = this;
				that.checkedArrs = e.detail.value;
				console.log(that.checkedArrs);
				that.bindxzr();
			},
			checkedArrss(ids) {
				console.log("投资路基" + ids);
			},
			// 打开窗口
			showDrawer(e) {
				console.log(e);
				this.$refs[e].open()
			},
			// 关闭窗口
			closeDrawer(e) {
				let that = this;
				this.$refs[e].close();
				console.log(that.checkedArrs);
				that.$eventHub.$emit('fire', that.checkedArrs);
				that.checkedArrs = "";
				uni.navigateBack({

				})
			},
			// 抽屉状态发生变化触发
			change(e, type) {
				console.log((type === 'showLeft' ? '左窗口' : '右窗口') + (e ? '打开' : '关闭'));
				this[type] = e
			}, 
			// 全选事件
			allChoose(e) {
				console.log(e);
				let that = this;
				let chooseItem = e.detail.value;
				// 全选
				if (chooseItem[0] == 'all') {
					that.allChecked = true;
					for (let item of that.checkbox) {
						let itemVal = String(item.id + '&' + item.name);
						if (!that.checkedArr.includes(itemVal)) {
							that.checkedArr.push(itemVal);
						}
					}
					console.log(that.checkedArr);
				} else {
					// 取消全选
					that.allChecked = false;
					that.checkedArr = [];
					console.log(that.checkedArr);
				}
				that.bindxzr();
			},
			// 全选事件
			allChooses(e) {
				console.log(e);
				let that = this;
				let chooseItem = e.detail.value;
				// 全选
				if (chooseItem[0] == 'alls') {
					that.allCheckeds = true;
					for (let item of that.checkboxs) {
						let itemVal = String(item.id + '&' + item.name);
						if (!that.checkedArrs.includes(itemVal)) {
							that.checkedArrs.push(itemVal);
						}
					}
					console.log(that.checkedArrs);
				} else {
					// 取消全选
					that.allCheckeds = false;
					that.checkedArrs = [];
					console.log(that.checkedArrs);
				}
				that.bindxzr();
			},
			bindxzr() {
				let that = this;
				that.yjxzdsr = that.checkedArrs.length;
				that.qdyjxzdsr = that.checkedArrs.length;
				if (that.checkedArrs.length > 0 || that.checkedArr.length > 0) {
					that.qddisabled = '';
					that.qdbutton = 'background:#007AFF';
				} else {
					that.qddisabled = 'disabled';
					that.qdbutton = 'background:#ddd';
				}
				if (that.checkbox.length > 12) {
					that.butonss = "margin-bottom: 45px;";
				} else {
					that.butonss = "";
				}
			},
			qsdanniu() {
				let that = this;
				that.$eventHub.$emit('fire', that.checkedArrs);
				uni.navigateBack({

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
