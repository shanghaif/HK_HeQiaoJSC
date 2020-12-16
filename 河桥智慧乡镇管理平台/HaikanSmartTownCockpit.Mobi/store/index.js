//引入vue和vuex
import Vue from 'vue'
import Vuex from 'vuex'
Vue.use(Vuex)

const store = new Vuex.Store({//全局变量定义
    state: {
        hasLogin: false,
        userName: "",
		userType:"",
        userId:'',
		userDepartmentName:'',
		userDepartmentGuid:'',
		roleName:'',
        token:'',
		seltab:0,
		seltit:'首页',
		phone:''
    },
    mutations: {
        login:function(state, user,phone) {
			console.log(user);
            state.userName = user.userName;
            state.hasLogin = true;
			state.userType = user.userType;
            state.userId = user.userId;
			state.userDepartmentName=user.DepartmentName,
			state.userDepartmentGuid=user.DepartmentGuid,
			state.roleName = user.RoleName;
            state.token = user.token;
        },
        logout(state) {
           state.userName = "";
           state.hasLogin = false;
           state.userId = '';
		   state.userType = '';
		   state.userDepartmentName= '';
		   state.userDepartmentGuid= '';
		   state.roleName ='';
           state.token = '';
		   state.phone='';
        }
    }
})
export default store