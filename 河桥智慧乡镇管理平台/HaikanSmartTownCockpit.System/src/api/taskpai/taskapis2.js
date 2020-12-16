import axios from '@/libs/api.request'

//查询数据
export const getdatalist = (data) => {
    return axios.request({
        url: 'task/task/list',
        method: 'post',
        data
    })
}

//重点工作下拉框
export const getpersonaldiary = (data) => {
    return axios.request({
        url: 'task/task/personaldiaryList',
        method: 'get',
    })
}


//科室下拉框
export const keshidata = (data) => {
    return axios.request({
        url: 'task/task/keshidata',
        method: 'get',
    })
}

//科室下拉框
export const keshidata2 = (data) => {
    return axios.request({
        url: 'task/task/keshidata2',
        method: 'get',
    })
}


//审批人下拉框
export const getfuzeren = (data) => {
    return axios.request({
        url: 'task/task/systemuserList',
        method: 'get',
    })
}

//审批人下拉框
export const systemuserlistuuid = (data) => {
    return axios.request({
        url: 'task/task/systemuserlistuuid/'+data.uuid,
        method: 'get',
    })
}


//负责人穿梭框
export const getcanyuren2 = (data) => {
    return axios.request({
        url: 'task/task/allsystemuser2',
        method: 'get',
    })
}

export const systemuserlistid = (data) => {
    return axios.request({
        url: 'task/task/systemuserlistid/'+data.uuid,
        method: 'get',
    })
}


//获取已选择的人员审批人
export const binddataok3 = (data) => {
    return axios.request({
        url: 'task/task/selectsystemuser3/' + data.id,
        method: 'get',
        params: data
    })
    }


//获取已选择的人员负责人
export const binddataok2 = (data) => {
return axios.request({
    url: 'task/task/selectsystemuser2/' + data.id,
    method: 'get',
    params: data
})
}

//参与人穿梭框
export const getcanyuren = (data) => {
        return axios.request({
            url: 'task/task/allsystemuser',
            method: 'get',
        })
    }
    //获取已选择的人员参与人
export const binddataok = (data) => {
    return axios.request({
        url: 'task/task/selectsystemuser/' + data.id,
        method: 'get',
        params: data
    })
}


//添加
export const create = (data) => {
    return axios.request({
        url: 'task/task/create',
        method: 'post',
        data
    })
}

//添加科室总结
export const createkeshi = (data) => {
    return axios.request({
        url: 'task/task/createkeshis',
        method: 'post',
        data
    })
}



//编辑与详情
export const loaddata = (data) => {
    return axios.request({
        url: 'task/task/Edit/' + data.id,
        method: 'get',
        data
    })
}



//编辑与详情(科室工作总结)
export const Editkeshi = (data) => {
    return axios.request({
        url: 'task/task/Editkeshis/' + data.id,
        method: 'get',
        data
    })
}


//编辑后保存
export const baocunedit = (data) => {
    return axios.request({
        url: 'task/task/baocunEdit',
        method: 'post',
        data
    })
}

//编辑后保存(科室工作总结)
export const baocunEditkeshi = (data) => {
    return axios.request({
        url: 'task/task/baocunEditkeshis',
        method: 'post',
        data
    })
}

//删除
export const deletelist = (data) => {
    return axios.request({
        url: 'task/task/delete2/' + data.ids,
        method: 'get'
    })
}

//查询操作日志
export const caozuolist = (data) => {
    return axios.request({
        url: 'task/task/caozuorizhi/' + data.uuid,
        method: 'get',
        data
    })
}


//添加操作日志
export const appcreaterizhi = (data) => {
    return axios.request({
        url: 'task/task/appcreaterizhi',
        method: 'post',
        data
    })
}



//同步人员信息
export const getalluseranddep = () => {
    return axios.request({
        url: 'task/task/getalluseranddep',
        method: 'get',
    })
}

// 上传附件
export const RegistPicture = (data) => {
    return axios.request({
        url: 'personal/personalDiary/RegistPicture',
        method: 'post',
        data
    })
}

//免登
export const getuserinfos = (data) => {
	return axios.request({
		url: 'task/app/taskapp/getuserinfos/' + data,
		method: 'get'
	})
}