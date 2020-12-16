import axios from '@/libs/api.request'

//查询数据
export const getDepartmentList = (data) => {
    return axios.request({
        url: 'rbac/department/list',
        method: 'post',
        data
    })
}

//创建科室
export const createDepartment = (data) => {
    return axios.request({
        url: 'rbac/department/create',
        method: 'post',
        data
    })
}

//编辑显示数据
export const loadDepartment = (data) => {
    return axios.request({
        url: 'rbac/department/edit/' + data.guid,
        method: 'get'
    })
}

//保存编辑
export const editDepartment = (data) => {
    return axios.request({
        url: 'rbac/department/edit',
        method: 'post',
        data
    })
}

//获取科室列表
export const loaddepartmentListDetail = () => {
    return axios.request({
        url: 'rbac/department/DepartmentList',
        method: 'get'
    })
}

//删除科室
export const deleteDepartment = (ids) => {
    return axios.request({
        url: 'rbac/department/delete/' + ids,
        method: 'get'
    })
}

//批量操作
export const batchCommand = (data) => {
    return axios.request({
        url: 'rbac/department/batch',
        method: 'get',
        params: data
    })
}