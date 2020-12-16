import axios from '@/libs/api.request'

//查询所有数据列表
export const getRoleList = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/list',
        method: 'post',
        data
    })
}


//查询所有数据列表
export const collectGetRoleList = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/collectList',
        method: 'post',
        data
    })
}

// 创建绩效
export const createRole = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/create',
        method: 'post',
        data
    })
}

//编辑-显示数据
export const loadRole = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/edit/' + data.guid,
        method: 'get'
    })
}

// 保存编辑后的
export const editRole = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/edit',
        method: 'post',
        data
    })
}

// 删除
export const deleteRole = (ids) => {
    return axios.request({
        url: 'performance/performanceDeclare/delete/' + ids,
        method: 'get'
    })
}

// 批量操作
export const batchCommand = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/batch',
        method: 'get',
        params: data
    })
}

//导入
export const RubbishInfoImport = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/daoru',
        method: 'post',
        data
    })
}

//部门下拉框
export const getfuzeren = (data) => {
    return axios.request({
        url: 'performance/performanceDeclare/departmentList',
        method: 'get',
    })
}