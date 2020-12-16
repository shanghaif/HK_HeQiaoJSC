<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
	import Vue from 'vue'
      import 'xe-utils'
      import VXETable from 'vxe-table'
      import 'vxe-table/lib/index.css'

	  Vue.use(VXETable)
	  
export default {
  name: 'App',
  		globalData: {
			UserId: '',
			UserName: ''
		},
		onLaunch: function() {
			console.log('App Launch')
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide')
		}
}

</script>

<style lang="less">
.size{
  width: 100%;
  height: 100%;
}
html,body{
  .size;
  overflow: hidden;
  margin: 0;
  padding: 0;
}
#app {
  .size;
}
</style>
