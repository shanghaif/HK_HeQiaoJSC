<template>
	<view >
		<view class="cu-custom" :style="[{height:CustomBar + 'px'}]" style="z-index: 999999999;">
			<view class="cu-bar fixed" :style="style" :class="[bgImage!=''?'none-bg text-white bg-img':'',bgColor]">
				<view class="action" @tap="BackPage" v-if="isBack">
					<text class="cuIcon-back"></text>
					<slot name="backText"></slot>
				</view>
				<view class="content" :style="[{top:StatusBar + 'px'}]">
					<slot name="content"></slot>
				</view>
				<slot name="right"></slot>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				StatusBar: this.StatusBar,
				CustomBar: this.CustomBar
			};
		},
		name: 'cu-custom',
		computed: {
			style() {
				var StatusBar= this.StatusBar;
				var CustomBar= this.CustomBar;
				var bgImage = this.bgImage;
				var style = `height:${CustomBar}px;padding-top:${StatusBar}px;`;
				if (this.bgImage) {
					style = `${style}background-image:url(${bgImage});`;
				}
				return style
			}
		},
		props: {
			bgColor: {
				type: String,
				default: ''
			},
			isBack: {
				type: [Boolean, String],
				default: false
			},
			bgImage: {
				type: String,
				default: ''
			},
			tourl:{
				type: String,
				default: ''
			}
		},
		methods: {
			BackPage() {
				if(this.tourl==''||this.tourl==null)
				{
					console.log(11);
					uni.navigateBack({
					delta: 1
				});
				}
				else
				{
					console.log(1);
					uni.redirectTo({
						url:this.tourl
					})
				}
				
			}
		}
	}
</script>

<style>
	.cu-bar.fixed,
	.nav.fixed {
		position: static !important;
		width: 100%;
		top: 0;
		z-index: 1024;
		box-shadow: 0 1upx 6upx rgba(0, 0, 0, 0.1);
	}
	
.cu-list.grid>.cu-item {
	display: flex;
	padding: 20upx 0 30upx;
	transition-duration: 0s;
	flex-direction: column
}
</style>
