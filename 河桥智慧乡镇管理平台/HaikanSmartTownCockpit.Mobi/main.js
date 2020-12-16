import Vue from 'vue'
import App from './App'
import uView from "uview-ui";
Vue.use(uView);
import store from './store'
Vue.config.productionTip = false

import Task from './pages/Task/MyTask.vue'
Vue.component('Task',Task)

// import AddTask from './pages/MessageAdd/AddTask.vue'
// Vue.component('AddTask',AddTask)
import MessageAdd from './pages/MessageAdd/MessageList.vue'
Vue.component('MessageAdd',MessageAdd)

import Priority from './pages/Priority/PriorityShow.vue'
Vue.component('Priority',Priority)

import TongJi from './pages/TongJi/TongJi.vue'
Vue.component('TongJi',TongJi)

import Personallog from './pages/Personallog/personalloglist.vue'
Vue.component('Personallog',Personallog)


App.mpType = 'app'

const app = new Vue({
    ...App
})
app.$mount()

Vue.prototype.$eventHub = new Vue();
