<template>
	<view>专有钉钉测试方法</view>
</template>

<script>
	import dd from 'gdt-jsapi';
	export default {
		data() {
			return {}
		},
		onLoad() {
			console.log("加载专有钉钉方法")
			//钉钉接口方法dd.getAuthCode获取授权码
			dd.getAuthCode({}).then((result) => {
				if (result) {
					console.log(JSON.stringify(result, null, null));
				}
			})
		},
		methods: {

		}
	}
</script>

<style>
</style>
