import axios from '@/libs/api.request'

//查询所有数据列表
export const getRoleList = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/list',
        method: 'post',
        data
    })
}

//查询我负责的数据列表
export const principalListRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/principalList',
        method: 'post',
        data
    })
}

//查询我创建的数据列表
export const establishListRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/establishList',
        method: 'post',
        data
    })
}

//查询我参与的数据列表
export const participantListRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/participantList',
        method: 'post',
        data
    })
}

//查询已完成的数据列表
export const accomplishListRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/accomplishList',
        method: 'post',
        data
    })
}

//查询回收站的数据列表-已删除的数据
export const recycleListRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/recycleList',
        method: 'post',
        data
    })
}

// 创建全部重点工作
export const createRole = (data) => {
    console.log("测试22");
    console.log(data);
    return axios.request({
        url: 'emphasisWork/allPriority/create',
        method: 'post',
        data
    })
}

//编辑重点工作-显示数据
export const loadRole = (data) => {
    console.log("进去接口！")
    return axios.request({
        url: 'emphasisWork/allPriority/edit/' + data.id,
        method: 'get'
    })
}

// 保存编辑后的重点工作
export const editRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/edit',
        method: 'post',
        data
    })
}

// 删除重点工作-标记删除
export const deleteRole = (ids) => {
    return axios.request({
        url: 'emphasisWork/allPriority/delete/' + ids,
        method: 'get'
    })
}

// 批量操作-标记
export const batchCommand = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/batch',
        method: 'get',
        params: data
    })
}

// 删除重点工作-真删
export const deleterecycleRole = (ids) => {
    return axios.request({
        url: 'emphasisWork/allPriority/deleteRecycle/' + ids,
        method: 'get'
    })
}

// 批量操作-真删
export const batchBatchRecycle = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/batchRecycle',
        method: 'get',
        params: data
    })
}

// 恢复重点工作-单个
export const deleteRenewRole = (ids) => {
    return axios.request({
        url: 'emphasisWork/allPriority/deleteRenew/' + ids,
        method: 'get'
    })
}

// 完成重点工作
export const priorityAccomplishRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/priorityAccomplish',
        method: 'post',
        data
    })
}

// 重启重点工作
export const priorityRestartRole = (ids) => {
    return axios.request({
        url: 'emphasisWork/allPriority/priorityRestart/' + ids,
        method: 'get'
    })
}

// 升序
export const updateIsascRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/updateIsASC?sortord=' + data.sortord.toString() + '&guid=' + data.guid,
        method: 'get',
    })
}

// 降序
export const updateIsdescRole = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/updateIsDESC?sortord=' + data.sortord.toString() + '&guid=' + data.guid,
        method: 'get',
    })
}

//参与人穿梭框
export const getcanyuren = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/allsystemuser/',
        method: 'get',
    })
}
//负责人穿梭框
export const getfuzerenkuang = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/allsystemuser2/',
        method: 'get',
    })
}
//负责人下拉框
export const getfuzeren = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/systemuserList',
        method: 'get',
    })
}
export const getuserinfo = (data) => {
	return axios.request({
		url: 'api/v1/emphasisWork/allPriority/getuserinfo/' + data,
		method: 'Get'
	})
}

    //获取已选择的人员参与人
    export const binddataok = (data) => {
        return axios.request({
            url: 'emphasisWork/allPriority/selectsystemuser/' + data.id,
            method: 'get',
            params: data
        })
    }

    //获取已选择的人员负责人
export const binddataok2 = (data) => {
    return axios.request({
        url: 'emphasisWork/allPriority/selectsystemuser2/' + data.id,
        method: 'get',
        params: data
    })
    }

//推送消息
export const notescanyu = (data) => {
    return axios.request({
      url: 'emphasisWork/allPriority/notescanyu/'+data.uuid,
      method: 'get',
    })
  }

    