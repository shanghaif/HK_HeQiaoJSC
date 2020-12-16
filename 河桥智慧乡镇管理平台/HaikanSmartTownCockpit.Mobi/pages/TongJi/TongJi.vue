<template>
	<view class="qiun-columns">

		<scroll-view scroll-x class="bg-white nav text-center">
			<view style="margin-bottom: 10px;" class="cu-item" :class="index==TabCur?'text-blue cur':''" v-for="(item,index) in 3"
			 :key="index" @tap="tabSelect" :data-id="index">
				{{name[index]}}
			</view>

			<!-- 任务分布统计 -->
			<view v-if="fenbu">
				<view class="qiun-bg-white qiun-title-bar qiun-common-mt">
					<view class="qiun-title-dot-light">任务分布情况</view>
				</view>
				<view class="qiun-charts">
					<canvas canvas-id="canvasPie" id="canvasPie" class="charts" :width="cWidth*pixelRatio" :height="cHeight*pixelRatio"
					 :style="{'width':cWidth+'px','height':cHeight+'px'}" @touchstart="touchPie"></canvas>
				</view>
				<view class="qiun-bg-white qiun-title-bar qiun-common-mt">
					<view class="qiun-title-dot-light">优先级分布情况</view>
				</view>
				<view class="qiun-charts">
					<canvas canvas-id="canvasPie2" id="canvasPie2" class="charts" :width="cWidth*pixelRatio" :height="cHeight*pixelRatio"
					 :style="{'width':cWidth+'px','height':cHeight+'px'}" @touchstart="touchPie2"></canvas>
				</view>

			</view>

			<!-- 任务负责统计 -->
			<view v-if="fuze">
				<view class="qiun-bg-white qiun-title-bar qiun-common-mt">
					<view class="qiun-title-dot-light">任务负责排行统计</view>
				</view>
				<view class="qiun-charts">
					<canvas canvas-id="canvasColumn" id="canvasColumn" class="charts" :width="cWidth*pixelRatio" :height="cHeight*pixelRatio"
					 :style="{'width':cWidth+'px','height':cHeight+'px'}" @touchstart="touchColumn"></canvas>
				</view>
				<view class="qiun-bg-white qiun-title-bar qiun-common-mt">
					<view class="qiun-title-dot-light">任务负责排行统计<button class="cu-btn line-orange" @click="qiehuan">
							<text class="cuIcon-order"></text>{{ordertext}}</button></view>
				</view>
				<view class="qiun-charts">
					<view class="warp">
						<view class="box">
							<!-- <!-- <view class="title">默认效果</view> -->
							<t-table>
								<t-tr>
									<t-th>姓名</t-th>
									<t-th>负责总数</t-th>
									<t-th>已逾期</t-th>
									<t-th>进行中</t-th>
									<t-th>已完成</t-th>
								</t-tr>
								<t-tr v-for="item in tableList">
									<t-td>{{item.name.length>3?item.name.substring(0,3)+'...':item.name}}</t-td>
									<t-td><text style="color: #800000;">{{ item.sun }}</text></t-td>
									<t-td>{{ item.yuqi }}</t-td>
									<t-td>{{ item.jinxing }}</t-td>
									<t-td>{{ item.wancheng }}</t-td>
								</t-tr>
							</t-table>
						</view>
					</view>
				</view>
			</view>

			<!-- 任务参与统计 -->
			<view v-if="canyu">
				<view class="qiun-bg-white qiun-title-bar qiun-common-mt">
					<view class="qiun-title-dot-light">任务参与排行统计</view>
				</view>

				<view class="qiun-charts">
					<canvas canvas-id="canvasColumns" id="canvasColumns" class="charts" :width="cWidth*pixelRatio" :height="cHeight*pixelRatio"
					 :style="{'width':cWidth+'px','height':cHeight+'px'}" @touchstart="touchColumns"></canvas>
				</view>
				<view class="qiun-bg-white qiun-title-bar qiun-common-mt">
					<view class="qiun-title-dot-light">任务参与排行统计<button class="cu-btn line-orange" @click="qiehuans">
							<text class="cuIcon-order"></text>{{ordertexts}}</button></view>
				</view>
				<view class="qiun-charts">
					<view class="warp">
						<view class="box">
							<!-- <!-- <view class="title">默认效果</view> -->
							<t-table>
								<t-tr>
									<t-th>姓名</t-th>
									<t-th>参与总数</t-th>
									<t-th>已逾期</t-th>
									<t-th>进行中</t-th>
									<t-th>已完成</t-th>
								</t-tr>
								<t-tr v-for="item in tableLists">
									<t-td>{{item.name.length>3?item.name.substring(0,3)+'...':item.name}}</t-td>
									<t-td><text style="color: #800000;">{{ item.sun }}</text></t-td>
									<t-td>{{ item.yuqi }}</t-td>
									<t-td>{{ item.jinxing }}</t-td>
									<t-td>{{ item.wancheng }}</t-td>
								</t-tr>
							</t-table>
						</view>
					</view>
				</view>
			</view>
		</scroll-view>


	</view>
</template>

<script>
	import tTable from '@/components/t-table/t-table.vue';
	import tTh from '@/components/t-table/t-th.vue';
	import tTr from '@/components/t-table/t-tr.vue';
	import tTd from '@/components/t-table/t-td.vue';


	import uCharts from '../../components/u-charts/u-charts.js';
	import {
		selectcount,
		selectfuzepaihangapp,
		selectcanyupaihangapp,
		selectjinzhan,
		selectjinzhandesc,
		selectmissionscanyu,
		selectmissionscanyudesc,
	} from '@/api/tongji/tongjiapp.js'

	var _self;
	var canvaPie = null;
	var canvaPie2 = null;
	var canvaColumn = null;
	var canvaColumns=null;

	export default {
		components: {
			tTable,
			tTh,
			tTr,
			tTd,
		},
		data() {
			return {
				ordertext: '降序',
				ordertexts: '降序',
				order: 'desc',
				orders: 'desc',
				tableListasc:[],
				tableListdesc:[],
				tableList: [],
				tableListsasc:[],
				tableListsdesc:[],
				tableLists: [],
				fenbu: true,
				fuze: false,
				canyu: false,

				cWidth: '',
				cHeight: '',
				pixelRatio: 1,
				serverData: '',

				TabCur: 0,
				scrollLeft: 0,
				name: ["任务分布统计", "任务负责排行", "任务参与排行"]

			}
		},
		mounted() {
			_self = this;

			uni.getSystemInfo({
				success: function(res) {
					if (res.pixelRatio > 1) {
						//正常这里给2就行，如果pixelRatio=3性能会降低一点
						//_self.pixelRatio =res.pixelRatio;
						_self.pixelRatio = 2;
					}
				}
			});
			this.cWidth = uni.upx2px(750);
			this.cHeight = uni.upx2px(500);
			this.getServerData();
			this.fuzeddload();
			this.ceshiload();
		},
		methods: {
			
			//负责排行
			fuzedd(){
				uni.showLoading()
				//排行
				selectfuzepaihangapp().then(res => {
					let Column = res.data
				
				
					Column.categories = Column.categories;
					Column.series = Column.series;
				
					_self.showColumn("canvasColumn", Column); //柱状图
					uni.hideLoading()
				});
			},
			
			fuzeddload(){
				
				let that =this;
				selectjinzhan().then(res => {
					//任务负责数据
					console.log("任务负责数据")
					console.log(res)
					that.tableListasc = res.data.data;
				
				selectjinzhandesc().then(res => {
					//任务负责数据
					console.log("任务负责数据")
					console.log(res)
					that.tableList=that.tableListdesc = res.data.data;
				});					
				});	
			},
			
			
			
			
			getServerData() {
				//任务分布
				uni.showLoading()
				selectcount({
					//userid: getApp().globalData.UserUUId,
					userid: '423968df-a15d-4b5f-9e25-005486496332'
				}).then(res => {
					console.log("任务分布")
					console.log(res.data);
					let Pie = {
						series: [{
							"name": "进行中",
							"data": res.data.jinxingzhong
						}, {
							"name": "已逾期",
							"data": res.data.yiyuqi
						}, {
							"name": "按时完成",
							"data": res.data.anshiwancheng
						}]
					};
					let Pie2 = {
						series: [{
							"name": "普通",
							"data": res.data.putong
						}, {
							"name": "紧急",
							"data": res.data.jinji
						}]
					};
					_self.showPie("canvasPie", Pie); //任务分布
					_self.showPie2("canvasPie2", Pie2); //优先级分布
					console.log("已获取数据")
					console.log(Pie)
					console.log(Pie2)
				uni.hideLoading()
				})


			},
			ceshi() {
				uni.showLoading()
				//参与人排行
				selectcanyupaihangapp().then(res => {
					let Column = res.data
				    console.log("测试数据");
				    console.log(res);
					Column.categories = Column.categories;
					Column.series = Column.series;
				
					_self.showColumns("canvasColumns", Column); //柱状图	
					uni.hideLoading()
				});
			},
			ceshiload(){
				let that =this;
				console.log("测试测试测试")
				selectmissionscanyu().then(res => {
					//任务参与数据
					console.log("任务参与数据")
					console.log(res)
					that.tableListsasc = res.data.data;
				selectmissionscanyudesc().then(res => {
					//任务参与数据 	
					console.log("任务参与数据")
					console.log(res)
					that.tableLists=that.tableListsdesc = res.data.data;
				});					
				});	
			},
			
			
		
			qiehuan() {
				uni.showLoading()
				if (this.order == "desc") {
					this.order = "asc";
					this.ordertext = '升序';
					this.tableList=this.tableListasc;
					
				} else {
					this.order = "desc";
					this.ordertext = '降序';
					this.tableList=this.tableListdesc;
					
				}
				uni.hideLoading()
			},
			
			qiehuans() {
				uni.showLoading()
				if (this.orders == "desc") {
					this.orders = "asc";
					this.ordertexts = '升序';
					this.tableLists=this.tableListsasc;
					console.log(this.tableLists)
					
				} else {
					this.orders = "desc";
					this.ordertexts = '降序';
					this.tableLists=this.tableListsdesc;
					console.log(this.tableLists)
				}
				uni.hideLoading()
			},


			showPie(canvasId, chartData) {
				canvaPie = new uCharts({
					$this: _self,
					canvasId: canvasId,
					type: 'pie',
					fontSize: 11,
					legend: {
						show: true
					},
					background: '#FFFFFF',
					pixelRatio: _self.pixelRatio,
					series: chartData.series,
					animation: true,
					width: _self.cWidth * _self.pixelRatio,
					height: _self.cHeight * _self.pixelRatio,
					dataLabel: true,
					extra: {
						pie: {
							lableWidth: 15
						}
					},
				});
			},
			showPie2(canvasId, chartData) {
				canvaPie2 = new uCharts({
					$this: _self,
					canvasId: canvasId,
					type: 'pie',
					fontSize: 11,
					legend: {
						show: true
					},
					background: '#FFFFFF',
					pixelRatio: _self.pixelRatio,
					series: chartData.series,
					animation: true,
					width: _self.cWidth * _self.pixelRatio,
					height: _self.cHeight * _self.pixelRatio,
					dataLabel: true,
					extra: {
						pie: {
							lableWidth: 15
						}
					},
				});
			},
			touchPie(e) {
				canvaPie.showToolTip(e, {
					format: function(item) {
						return item.name + ':' + item.data
					}
				});
			},
			touchPie2(e) {
				canvaPie2.showToolTip(e, {
					format: function(item) {
						return item.name + ':' + item.data
					}
				});
			},
            //柱状图2
			showColumns(canvasId, chartData) {
				canvaColumns = new uCharts({
					$this: _self,
					canvasId: canvasId,
					type: 'column',
					legend: {
						show: true
					},
					fontSize: 11,
					background: '#FFFFFF',
					pixelRatio: _self.pixelRatio,
					animation: true,
					categories: chartData.categories,
					series: chartData.series,
					xAxis: {
						disableGrid: true,
					},
					yAxis: {
						min: 0
						//disabled:true
					},
					dataLabel: true,
					width: _self.cWidth * _self.pixelRatio,
					height: _self.cHeight * _self.pixelRatio,
					extra: {
						column: {
							type: 'group',
							width: _self.cWidth * _self.pixelRatio * 0.45 / chartData.categories.length
						}
					}
				});

			},
			//柱状图
			showColumn(canvasId, chartData) {
				canvaColumn = new uCharts({
					$this: _self,
					canvasId: canvasId,
					type: 'column',
					legend: {
						show: true
					},
					fontSize: 11,
					background: '#FFFFFF',
					pixelRatio: _self.pixelRatio,
					animation: true,
					categories: chartData.categories,
					series: chartData.series,
					xAxis: {
						disableGrid: true,
					},
					yAxis: {
						min: 0
						//disabled:true
					},
					dataLabel: true,
					width: _self.cWidth * _self.pixelRatio,
					height: _self.cHeight * _self.pixelRatio,
					extra: {
						column: {
							type: 'group',
							width: _self.cWidth * _self.pixelRatio * 0.45 / chartData.categories.length
						}
					}
				});

			},
			touchColumns(e) {
				canvaColumns.showToolTip(e, {
					format: function(item, category) {
						if (typeof item.data === 'object') {
							return category + ' ' + item.name + ':' + item.data.value
						} else {
							return category + ' ' + item.name + ':' + item.data
						}
					}
				});
			},
			touchColumn(e) {
				canvaColumn.showToolTip(e, {
					format: function(item, category) {
						if (typeof item.data === 'object') {
							return category + ' ' + item.name + ':' + item.data.value
						} else {
							return category + ' ' + item.name + ':' + item.data
						}
					}
				});
			},

			tabSelect(e) {
				this.TabCur = e.currentTarget.dataset.id;
				this.scrollLeft = (e.currentTarget.dataset.id - 1) * 40
				if (this.TabCur == 0) {
					this.fenbu = true;
					this.fuze = false;
					this.canyu = false;
					this.getServerData()
				} else if (this.TabCur == 1) {
					this.fenbu = false;
					this.fuze = true;
					this.canyu = false;
					this.fuzedd();
				} else {
					this.fenbu = false;
					this.fuze = false;
					this.canyu = true;
					this.ceshi();
				}
			}

		}
	}
</script>

<style>
	page {
		background: #F2F2F2;
		width: 750upx;
		overflow-x: hidden;
	}

	.qiun-padding {
		padding: 2%;
		width: 100%;
	}

	.qiun-wrap {
		display: flex;
		flex-wrap: wrap;
	}

	.qiun-rows {
		display: flex;
		flex-direction: row !important;
	}

	.qiun-columns {
		margin-bottom: 50px;
		display: flex;
		flex-direction: column !important;
	}

	.qiun-common-mt {
		margin-top: 10upx;
	}

	.qiun-bg-white {
		background: #FFFFFF;
	}

	.qiun-title-bar {
		width: 100%;
		padding: 10upx 2%;
		flex-wrap: nowrap;
		text-align: left;
	}

	.qiun-title-dot-light {
		border-left: 10upx solid #0ea391;
		padding-left: 10upx;
		font-size: 32upx;
		color: #000000
	}

	.qiun-charts {
		/* width: 750upx;
		height: 700upx; */

		padding-bottom: 10px;

		background-color: #FFFFFF;
	}

	.charts {
		width: 750upx;
		height: 500upx;
		background-color: #FFFFFF;
	}
</style>
