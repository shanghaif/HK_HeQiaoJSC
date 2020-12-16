using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.Department;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemMenu;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemPermission;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemRole;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.SystemUser;
using HaikanSmartTownCockpit.Api.Entities;
using HaikanSmartTownCockpit.Api.ViewModels.Emergency;
using HaikanSmartTownCockpit.Api.ViewModels.Emergency.Emergencyresponse;
using HaikanSmartTownCockpit.Api.ViewModels.SecurityCode;
using HaikanSmartTownCockpit.Api.ViewModels.Intelligenttravel;
using HaikanSmartTownCockpit.Api.ViewModels.SocialGovern;
using HaikanSmartTownCockpit.Api.ViewModels.Socialgovernance;
using HaikanSmartTownCockpit.Api.ViewModels.WorkforceManage;
using HaikanSmartTownCockpit.Api.ViewModels.Environmental;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.PersonalDiary;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.Performance;
using HaikanSmartTownCockpit.Api.ViewModels.Rbac.Priority;
using HaikanSmartTownCockpit.Api.ViewModels.KeyPoint;
//using HaikanSmartTownCockpit.Api.ViewModels.Person;
//using HaikanSmartTownCockpit.Api.ViewModels.Base;
//using HaikanSmartTownCockpit.Api.ViewModels.Score;

namespace HaikanSmartTownCockpit.Api.Configurations
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region SystemUser
            CreateMap<SystemUser, UserJsonModel>();
            CreateMap<UserCreateViewModel, SystemUser>();
            CreateMap<UserEditViewModel, SystemUser>();
            CreateMap<SystemUser, UserEditViewModel>();
            #endregion
            #region SystemRole
            CreateMap<SystemRole, RoleJsonModel>();
            CreateMap<RoleCreateViewModel, SystemRole>();
            CreateMap<SystemRole, RoleCreateViewModel>();
            #endregion

            #region SystemMenu
            CreateMap<SystemMenu, MenuJsonModel>();
            CreateMap<MenuCreateViewModel, SystemMenu>();
            CreateMap<MenuEditViewModel, SystemMenu>();
            CreateMap<SystemMenu, MenuEditViewModel>();
            #endregion

            #region SystemPermission
            CreateMap<SystemPermission, PermissionJsonModel>()
                .ForMember(d => d.MenuName, s => s.MapFrom(x => x.SystemMenuUu.Name))
                .ForMember(d => d.PermissionTypeText, s => s.MapFrom(x => x.Type.ToString()));
            CreateMap<PermissionCreateViewModel, SystemPermission>();
            CreateMap<PermissionEditViewModel, SystemPermission>();
            CreateMap<SystemPermission, PermissionEditViewModel>();
            #endregion
            #region RescueTeam

            CreateMap<RescueTeamViewModel, RescueTeam>();
            CreateMap<RescueTeam, RescueTeamViewModel>();
            #endregion
            #region RescueMember

            CreateMap<RescueMemberViewModel, RescueMember>();
            CreateMap<RescueMember, RescueMemberViewModel>();
            #endregion
            #region XunchaInfo

            CreateMap<XunchaInfoViewModel, XunchaInfo>();
            CreateMap<XunchaInfo, XunchaInfoViewModel>();
            #endregion
            #region XunchaDuty

            CreateMap<XunchaDutyViewModel, XunchaDuty>();
            CreateMap<XunchaDuty, XunchaDutyViewModel>();
            #endregion
            #region Village

            CreateMap<VillageViewModel, Village>();
            CreateMap<Village, VillageViewModel>();
            #endregion
            #region VillageMember

            CreateMap<VillageMemberViewModel, VillageMember>();
            CreateMap<VillageMember, VillageMemberViewModel>();
            #endregion
            #region ResponseInit

            CreateMap<ResponseInitViewModel, ResponseInit>();
            CreateMap<ResponseInit, ResponseInitViewModel>();
            #endregion
            #region SecurityCode

            CreateMap<SecurityCodeViewModel, SecurityCode>();
            CreateMap<SecurityCode, SecurityCodeViewModel>();
            #endregion
            #region Evacuate

            CreateMap<Evacuate, EvacuateViewModel>();
            CreateMap<EvacuateViewModel, Evacuate>();
            #endregion
            #region RenyuZhuany

            CreateMap<RenyuZhuany, RenyuzhuanyViewModel>();
            CreateMap<RenyuzhuanyViewModel, RenyuZhuany>();
            #endregion
            #region HeDaowater

            CreateMap<HeDaowaterViewModel, HeDaowater>();
            CreateMap<HeDaowater, HeDaowaterViewModel>();
            #endregion
            #region Uav

            CreateMap<Uav, UAVViewModel>();
            CreateMap<UAVViewModel, Uav>();
            #endregion
            #region ResponseInfo

            CreateMap<ResponseInfo, ResponseInfoViewModel>();
            CreateMap<ResponseInfoViewModel, ResponseInfo>();
            CreateMap<Relationships, RelationshipsViewModel>();
            #endregion
            #region Gtoilet
            CreateMap<Gtoilet, GtoiletViewModel>();
            CreateMap<GtoiletViewModel, Gtoilet>();

            #endregion
            #region ParkingLot
            CreateMap<ParkingLotViewModel, ParkingLot>();
            CreateMap<ParkingLot, ParkingLotViewModel>();

            #endregion
            #region CityManagement
            CreateMap<CityManagementViewModel, CityManagement>();
            CreateMap<CityManagement, CityManagementViewModel>();

            #endregion
            #region XlProject
            CreateMap<XlProjectViewModel, XlProject>();
            CreateMap<XlProject, XlProjectViewModel>();

            #endregion
            #region RectifyInfo
            CreateMap<RectifyInfoViewModel, RectifyInfo>();
            CreateMap<RectifyInfo, RectifyInfoViewModel>();

            #endregion
            #region Ygiene

            CreateMap<HealthmanagementViewModel, Ygiene>();
            CreateMap<Ygiene, HealthmanagementViewModel>();
            #endregion
            #region SmokeGan

            CreateMap<smokeInductionViewModel, SmokeGan>();
            CreateMap<SmokeGan, smokeInductionViewModel>();
            #endregion
            #region PaibanInfo

            CreateMap<WorkManageViewModel, PaibanInfo>();
            CreateMap<PaibanInfo, WorkManageViewModel>();
            #endregion
            #region RentoutRoom

            CreateMap<rentalHousViewModel, RentoutRoom>();
            CreateMap<RentoutRoom, rentalHousViewModel>();
            #endregion
            #region Administrator

            CreateMap<administratorViewModel, Administrator>();
            CreateMap<Administrator, administratorViewModel>();
            #endregion
            #region Woods

            CreateMap<forestViewModel, Woods>();
            CreateMap<Woods, forestViewModel>();
            #endregion
            #region BuildHouse

            CreateMap<farmHouseViewModel, BuildHouse>();
            CreateMap<BuildHouse, farmHouseViewModel>();
            #endregion

            #region SystemUser
            CreateMap<SystemUser, UserJsonModel>();
            CreateMap<UserCreateViewModel, SystemUser>();
            CreateMap<UserEditViewModel, SystemUser>();
            CreateMap<SystemUser, UserEditViewModel>();
            #endregion


            #region Department
            CreateMap<Department, DepartmentJsonViewModel>();
            CreateMap<Department, DepartmentCreateViewModel>();
            #endregion

            #region PersonalDiary

            CreateMap<PersonalDiary, PersonalCreateModel>();
            #endregion

            #region Priority

            CreateMap<Priority, PriorityCreateModel>();
            #endregion

            #region PerformanceDeclare

            CreateMap<PerformanceDeclare, PerformanceCreateModel>();
            #endregion

            #region DepartmentDeclare

            CreateMap<DepartmentDeclare, DepartmentDeclareModel>();
            #endregion
            #region Fangxun
            CreateMap<FangxunViewModel, FangXun>();
            CreateMap<FangXun, FangxunViewModel>();
            #endregion

            #region Teenager
            CreateMap<TeenagerViewModel, KeyYouthList>();
            CreateMap<KeyYouthList, TeenagerViewModel>();
            #endregion
        }
    }
}
