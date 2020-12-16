import http from '@/components/utils/http.js'

//查询所有任务
export const getdatalist = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/getdatalistss',
    method: 'post',
  },data)
}

//更改参与人
export const editPerson = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/editPerson',
    method: 'post',
  },data)
}

//查询任务的个数
export const taskcount = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/taskcount?uuid='+data.uuid,
		method: 'get',
	},data)
}

//按条件查询
export const taskwhere = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/taskwhere?type='+data.type+'&uuid='+data.uuid+'&searhdata='+data.searhdata,
		method: 'get',
	},data)
}

//重点工作下拉框
export const getpersonaldiary = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/personaldiaryList',
    method: 'get',
  })
}

//查询出所有用户
export const gealluser = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/gealluser/'+data.type,
    method: 'get',
  })
}

//添加
export const create = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/appcreate',
    method: 'post',
  },data)
}

//编辑与详情
export const loaddata = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/AppEdit?uuid='+ data.uuid,
    method: 'get',
  },data)
}


//完成任务
export const appwanchen = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/Appwanchen',
    method: 'post',
  },data)
}

//查询操作日志
export const caozuolist = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskcaozuo/caozuorizhi?uuid='+data.uuid,
    method: 'get',
  },data)
}


//添加操作日志
export const appcreaterizhi = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskcaozuo/appcreaterizhi',
    method: 'post',
  },data)
}

//图片上传
export const RegistPicture = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskcaozuo/RegistPicture',
    method: 'post',
  },data)
}


export const getuserinfo = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getuserinfos/' + data,
		method: 'get'
	})
}
export const getzzduserinfo = (data) => {
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getzzduserinfo/' + data,
		method: 'get'
	})
}

//完成
export const update = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskcaozuo/updatestatus/'+data,
    method: 'get',
  })
}


//通过任务汇报uuid获取汇报信息
export const renwuzongjie = (data) => {
  return http.httpTokenRequest({
    url: 'api/v1/task/app/taskapp/renwuzongjie/'+data.uuid,
    method: 'get',
  })
}

//查询部门
export const getdepartlist=(data)=>{
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/getdepartlist',
		method: 'get',
	})
}

//任务id修改完成情况
export const getdataWork=(data)=>{
	return http.httpTokenRequest({
		url: 'api/v1/task/app/taskapp/FinishWork?uuid='+data,
		method: 'get',
	})
}