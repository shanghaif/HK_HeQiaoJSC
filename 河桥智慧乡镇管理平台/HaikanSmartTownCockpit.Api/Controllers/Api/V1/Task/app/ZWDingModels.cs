using System;
using System.Collections.Generic;

namespace HaikanSmartTownCockpit.Api.Controllers.Api.V1.Task.app
{

    public class ZWDingModels
    {

    }
    #region 工作通知

    public class WorkMsgId
    {
        public int id { get; set; }
        public string page { get; set; }
    }

    //public class WorkMsgBodyModel
    //{
    //    public WorkMsgBodyActioncard action_card { get; set; }
    //    public string msgtype { get; set; }

    //}

    //public class WorkMsgBodyModelText
    //{
    //    public string msgtype { get; set; }
    //    public WorkMsgBodyText text { get; set; }
    //}

    //public class WorkMsgBodyText
    //{
    //    public string content { get; set; }

    //}

    //public class WorkMsgBodyActioncard
    //{

    //    public string markdown { get; set; }
    //    public string single_title { get; set; }
    //    public string single_url { get; set; }
    //    public string title { get; set; }
    //}

    public class WorkMsgModel
    {
        public bool success { get; set; } = false;
        public WorkMsgModelContent content { get; set; }
        public string message { get; set; }
        public string errorMsg { get; set; }
    }

    public class WorkMsgModelContent
    {
        public string MsgId { get; set; }
    }

    #endregion

    #region AccessToken
    public class ZWDingAccessToken
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ZWDingAccessTokenContent Content { get; set; } = new ZWDingAccessTokenContent();

    }

    public class ZWDingAccessTokenContent
    {
        public ZWDingAccessTokenModel Data { get; set; } = new ZWDingAccessTokenModel();
        public bool Success { get; set; }
    }

    public class ZWDingAccessTokenModel
    {
        public int ExpiresIn { get; set; }
        public string AccessToken { get; set; } = "";

    }
    #endregion

    #region 根组织RootOrganization
    public class ZWDingRootOrganization
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ZWDingRootContent Content { get; set; } = new ZWDingRootContent();

    }

    public class ZWDingRootContent
    {
        public ZWDingRootOrganizationModel Data { get; set; } = new ZWDingRootOrganizationModel();
        public bool Success { get; set; } = false;
    }

    public class ZWDingRootOrganizationModel
    {
        public string OrganizationName { get; set; } = "";
        public string OrganizationCode { get; set; } = "";
        public string Status { get; set; } = "";
    }

    #endregion

    #region 查询下级部门
    public class ZWDingJuniorDepartment
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ZWDingJuniorDepartmentContent Content { get; set; } = new ZWDingJuniorDepartmentContent();
    }

    public class ZWDingJuniorDepartmentContent
    {
        public int TotalSize { get; set; }
        public List<string> Data { get; set; }
        public bool Success { get; set; } = false;
        public string RequestId { get; set; }
        public int PageSize { get; set; }
        public string ResponseMessage { get; set; }
        public int CurrentPage { get; set; }
        public string ResponseCode { get; set; }
    }
    #endregion

    #region 通讯录权限
    public class ZWDingAuthOrganization
    {
        public bool Success { get; set; } = false;
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public ZWDingAuthOrganizationContent Content { get; set; }
    }
    public class ZWDingAuthOrganizationContent
    {
        public List<string> UserVisibleScopes { get; set; }
        public List<string> DeptVisibleScopes { get; set; } = new List<string>();
    }

    #endregion

    #region 查询组织列表
    public class ZWDingGetOrganizations
    {
        public string Message { get; set; }
        public bool Success { get; set; } = false;
        public ZWDingGetOrganizationsContent Content = new ZWDingGetOrganizationsContent();
    }

    public class ZWDingGetOrganizationsContent
    {
        public bool Success { get; set; } = false;
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public List<ZWDingGetOrganizationsModel> Data { get; set; } = new List<ZWDingGetOrganizationsModel>();
    }

    public class ZWDingGetOrganizationsModel
    {
        public string OrganizationName { get; set; }
        public string ParentCode { get; set; }
        public string OrganizationCode { get; set; }
        public string Status { get; set; }
    }
    #endregion

    #region 批量获取人员
    public class ZWDingGetUsers
    {
        public string Message { get; set; }
        public bool Success { get; set; } = false;
        public ZWDingGetUserContent Content = new ZWDingGetUserContent();
    }

    public class ZWDingGetUserContent
    {
        public bool Success { get; set; } = false;
        public int TotalSize { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<ZWDingGetUserModel> Data { get; set; }
    }

    public class ZWDingGetUserModel
    {
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string Status { get; set; }
        public string AccountId { get; set; } = "";
    }

    #endregion

    #region 免登
    public class ZWDingGetLoginUser
    {
        public bool Success { get; set; } = false;
        public ZWDingGetLoginUserContent Content { get; set; } = new ZWDingGetLoginUserContent();
    }

    public class ZWDingGetLoginUserContent
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public bool Success { get; set; } = false;
        public ZWDingGetLoginUserModel Data { get; set; }
    }

    public class ZWDingGetLoginUserModel
    {
        public string Account { get; set; }//账号名
        public long AccountId { get; set; }//账号id
        public string ClientId { get; set; }//应用名
        public string EmployeeCode { get; set; }//租户下人员编码
        public string LastName { get; set; }//姓名
        public string Namespace { get; set; }//账号类型
        public string NickNameCn { get; set; }//昵称
        public string RealmId { get; set; }//租户id
        public string RealmName { get; set; }//租户名称
        public string TenantUserId { get; set; }//租户+用户唯一标识
        public string Openid { get; set; }//应用+用户唯一标识
    }

    #endregion

    #region 批量获取人员AccountId
    public class ZWDingGetEmployeeAccountIds
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public ZWDingGetEmployeeAccountIdsContent Content { get; set; } = new ZWDingGetEmployeeAccountIdsContent();
    }

    public class ZWDingGetEmployeeAccountIdsContent
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public bool Success { get; set; } = false;
        public List<ZWDingGetEmployeeAccountIdsModel> Data { get; set; } =new List<ZWDingGetEmployeeAccountIdsModel>();
    }

    public class ZWDingGetEmployeeAccountIdsModel
    {
        public string AccountId { get; set; } = "";
        public string EmployeeCode { get; set; } = "";
    }

    #endregion



    #region 手机号获取人员
    public class ZWDingGetByMobile
    {
        public bool Success { get; set; } = false;
        public ZWDingGetByMobileContent Content { get; set; } = new ZWDingGetByMobileContent();
    }

    public class ZWDingGetByMobileContent
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public bool Success { get; set; } = false;
        public ZWDingGetByMobileModel Data = new ZWDingGetByMobileModel();
    }

    public class ZWDingGetByMobileModel
    {
        public string EmployeeCode { get; set; } = "";
    }
    #endregion
    #region  获取通讯录权限范围
    public class ContentscopesV2
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> userVisibleScopes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> deptVisibleScopes { get; set; }
    }

    public class RootscopesV2
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ContentscopesV2 content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errorLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errorMsg { get; set; }
    }

    #endregion
    #region 根据组织 Code 查询详情
    public class DatagetOrganizationByCode
    {
        /// <summary>
        /// 
        /// </summary>
        public int displayOrder { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string typeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string businessStripCodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string institutionLevelCode { get; set; }
        /// <summary>
        /// 浙江杭州
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 技术部门
        /// </summary>
        public string organizationName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool leaf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string typeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string divisionCode { get; set; }
        /// <summary>
        /// 杭州海看网络科技有限公司
        /// </summary>
        public string parentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organizationCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
    }

    public class ContentgetOrganizationByCode
    {
        /// <summary>
        /// 
        /// </summary>
        public DatagetOrganizationByCode data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string responseMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string responseCode { get; set; }
    }

    public class RootgetOrganizationByCode
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ContentgetOrganizationByCode content { get; set; }
    }

    #endregion
    #region 查询组织下人员详情，包含个人信息和任职信息
    public class GovEmployeePositions
    {
        /// <summary>
        /// 
        /// </summary>
        public string posJobRankCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int orderInOrganization { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool mainJob { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empPosUnitCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string employeeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empPosEmployeeRoleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jobAttributesCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organizationCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 刘建军
        /// </summary>
        public string employeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empGender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string employeeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empPoliticalStatusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GovEmployeePositions> govEmployeePositions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empJobLevelCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empBudgetedPostCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
    }

    public class ContentEmployeePositions
    {
        /// <summary>
        /// 
        /// </summary>
        public int totalSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Data> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string responseMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int currentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string responseCode { get; set; }
    }

    public class RootEmployeePositions
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ContentEmployeePositions content { get; set; }
    }

    #endregion
}
