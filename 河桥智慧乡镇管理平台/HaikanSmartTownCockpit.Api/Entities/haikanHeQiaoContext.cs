using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HaikanSmartTownCockpit.Api.Entities
{
    public partial class haikanHeQiaoContext : DbContext
    {
        public haikanHeQiaoContext()
        {
        }

        public haikanHeQiaoContext(DbContextOptions<haikanHeQiaoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Alarmdata> Alarmdata { get; set; }
        public virtual DbSet<Barometric> Barometric { get; set; }
        public virtual DbSet<BuildHouse> BuildHouse { get; set; }
        public virtual DbSet<Canyin> Canyin { get; set; }
        public virtual DbSet<CarAnFang> CarAnFang { get; set; }
        public virtual DbSet<CityManagement> CityManagement { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ConcerQiao> ConcerQiao { get; set; }
        public virtual DbSet<CpcmanInfo> CpcmanInfo { get; set; }
        public virtual DbSet<Cunzhenmb> Cunzhenmb { get; set; }
        public virtual DbSet<DangActivity> DangActivity { get; set; }
        public virtual DbSet<DangOrganization> DangOrganization { get; set; }
        public virtual DbSet<DangerousGoods> DangerousGoods { get; set; }
        public virtual DbSet<DangerousRoom> DangerousRoom { get; set; }
        public virtual DbSet<DepartSumary> DepartSumary { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentDeclare> DepartmentDeclare { get; set; }
        public virtual DbSet<DisasterInfo> DisasterInfo { get; set; }
        public virtual DbSet<Drug> Drug { get; set; }
        public virtual DbSet<DzhizaihaiInfo> DzhizaihaiInfo { get; set; }
        public virtual DbSet<Emergency> Emergency { get; set; }
        public virtual DbSet<Evacuate> Evacuate { get; set; }
        public virtual DbSet<FangXun> FangXun { get; set; }
        public virtual DbSet<Followvillager> Followvillager { get; set; }
        public virtual DbSet<Gtoilet> Gtoilet { get; set; }
        public virtual DbSet<HeDaowater> HeDaowater { get; set; }
        public virtual DbSet<HomeAddress> HomeAddress { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<HqCommunal> HqCommunal { get; set; }
        public virtual DbSet<KeyYouthList> KeyYouthList { get; set; }
        public virtual DbSet<ListOfKeyPetitioners> ListOfKeyPetitioners { get; set; }
        public virtual DbSet<MaxOrMin> MaxOrMin { get; set; }
        public virtual DbSet<Mentally> Mentally { get; set; }
        public virtual DbSet<Mission> Mission { get; set; }
        public virtual DbSet<MissionJournal> MissionJournal { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<OrganPeoInfo> OrganPeoInfo { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Otherstaffs> Otherstaffs { get; set; }
        public virtual DbSet<PaibanInfo> PaibanInfo { get; set; }
        public virtual DbSet<ParkingLot> ParkingLot { get; set; }
        public virtual DbSet<ParticularPerson> ParticularPerson { get; set; }
        public virtual DbSet<PerformanceDeclare> PerformanceDeclare { get; set; }
        public virtual DbSet<PersonalDiary> PersonalDiary { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<PriorityJournal> PriorityJournal { get; set; }
        public virtual DbSet<Promo> Promo { get; set; }
        public virtual DbSet<PromoTeam> PromoTeam { get; set; }
        public virtual DbSet<Propaganda> Propaganda { get; set; }
        public virtual DbSet<PublicityFronts> PublicityFronts { get; set; }
        public virtual DbSet<PublicityType> PublicityType { get; set; }
        public virtual DbSet<PyramidSale> PyramidSale { get; set; }
        public virtual DbSet<Qiye> Qiye { get; set; }
        public virtual DbSet<RectifyInfo> RectifyInfo { get; set; }
        public virtual DbSet<RegionInfo> RegionInfo { get; set; }
        public virtual DbSet<RegionInfos> RegionInfos { get; set; }
        public virtual DbSet<RegionInfosCopy> RegionInfosCopy { get; set; }
        public virtual DbSet<Relationships> Relationships { get; set; }
        public virtual DbSet<ReleasedPrison> ReleasedPrison { get; set; }
        public virtual DbSet<RentoutRoom> RentoutRoom { get; set; }
        public virtual DbSet<RenyuZhuany> RenyuZhuany { get; set; }
        public virtual DbSet<RescueMember> RescueMember { get; set; }
        public virtual DbSet<RescueTeam> RescueTeam { get; set; }
        public virtual DbSet<ResponseInfo> ResponseInfo { get; set; }
        public virtual DbSet<ResponseInit> ResponseInit { get; set; }
        public virtual DbSet<Runbusiness> Runbusiness { get; set; }
        public virtual DbSet<RuzhuInfo> RuzhuInfo { get; set; }
        public virtual DbSet<Scenic> Scenic { get; set; }
        public virtual DbSet<Sectarian> Sectarian { get; set; }
        public virtual DbSet<Security> Security { get; set; }
        public virtual DbSet<SecurityCode> SecurityCode { get; set; }
        public virtual DbSet<SecurityReport> SecurityReport { get; set; }
        public virtual DbSet<SettleInfo> SettleInfo { get; set; }
        public virtual DbSet<Sightseer> Sightseer { get; set; }
        public virtual DbSet<SmokeGan> SmokeGan { get; set; }
        public virtual DbSet<SthOrganic> SthOrganic { get; set; }
        public virtual DbSet<SystemIcon> SystemIcon { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<SystemMenu> SystemMenu { get; set; }
        public virtual DbSet<SystemPermission> SystemPermission { get; set; }
        public virtual DbSet<SystemRole> SystemRole { get; set; }
        public virtual DbSet<SystemRolePermissionMapping> SystemRolePermissionMapping { get; set; }
        public virtual DbSet<SystemSetting> SystemSetting { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<SystemUserRoleMapping> SystemUserRoleMapping { get; set; }
        public virtual DbSet<TaskDecompInfo> TaskDecompInfo { get; set; }
        public virtual DbSet<TaskDecomposition> TaskDecomposition { get; set; }
        public virtual DbSet<TcqInfo> TcqInfo { get; set; }
        public virtual DbSet<TcqType> TcqType { get; set; }
        public virtual DbSet<TheUnion> TheUnion { get; set; }
        public virtual DbSet<TheUnionZhengc> TheUnionZhengc { get; set; }
        public virtual DbSet<Tongji> Tongji { get; set; }
        public virtual DbSet<Tourist> Tourist { get; set; }
        public virtual DbSet<TouristOfDay> TouristOfDay { get; set; }
        public virtual DbSet<TouristOfMonth> TouristOfMonth { get; set; }
        public virtual DbSet<TouristOfWeek> TouristOfWeek { get; set; }
        public virtual DbSet<TouristOfYear> TouristOfYear { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<TownMilitia> TownMilitia { get; set; }
        public virtual DbSet<Uav> Uav { get; set; }
        public virtual DbSet<UnifiedAddressLibrary> UnifiedAddressLibrary { get; set; }
        public virtual DbSet<UnifiedAddressLibraryUserInfo> UnifiedAddressLibraryUserInfo { get; set; }
        public virtual DbSet<Userinfoty> Userinfoty { get; set; }
        public virtual DbSet<ViewMenu> ViewMenu { get; set; }
        public virtual DbSet<ViewSystemPermissionWithMenu> ViewSystemPermissionWithMenu { get; set; }
        public virtual DbSet<ViewSystemPermissionWithMenu2> ViewSystemPermissionWithMenu2 { get; set; }
        public virtual DbSet<Village> Village { get; set; }
        public virtual DbSet<VillageMember> VillageMember { get; set; }
        public virtual DbSet<VolunteerTeam> VolunteerTeam { get; set; }
        public virtual DbSet<Volunteers> Volunteers { get; set; }
        public virtual DbSet<WaterLevel> WaterLevel { get; set; }
        public virtual DbSet<WaterLevelSheb> WaterLevelSheb { get; set; }
        public virtual DbSet<WomenActivities> WomenActivities { get; set; }
        public virtual DbSet<WomenFederation> WomenFederation { get; set; }
        public virtual DbSet<WomenHouse> WomenHouse { get; set; }
        public virtual DbSet<Woods> Woods { get; set; }
        public virtual DbSet<XfBuilding> XfBuilding { get; set; }
        public virtual DbSet<XiaoQuAnFang> XiaoQuAnFang { get; set; }
        public virtual DbSet<XiaoQuAnFangDev> XiaoQuAnFangDev { get; set; }
        public virtual DbSet<XlProject> XlProject { get; set; }
        public virtual DbSet<XunchaDuty> XunchaDuty { get; set; }
        public virtual DbSet<XunchaInfo> XunchaInfo { get; set; }
        public virtual DbSet<Yclactivity> Yclactivity { get; set; }
        public virtual DbSet<Ygiene> Ygiene { get; set; }
        public virtual DbSet<YoukeOverflow> YoukeOverflow { get; set; }
        public virtual DbSet<Youth> Youth { get; set; }
        public virtual DbSet<YouthPosition> YouthPosition { get; set; }
        public virtual DbSet<Yqfkryb> Yqfkryb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=192.168.0.214.;Initial Catalog=heqiao;Persist Security Info=True;User ID=heqiao;Password=haikan051030");
                optionsBuilder.UseSqlServer("Data Source=172.18.19.212;Initial Catalog=heqiao;Persist Security Info=True;User ID=hq;Password=Haikan051030");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.ActivityUuid);

                entity.HasComment("支部活动");

                entity.Property(e => e.ActivityUuid)
                    .HasColumnName("ActivityUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(255)
                    .HasComment("活动名称");

                entity.Property(e => e.ActivityTime)
                    .HasMaxLength(255)
                    .HasComment("活动时间");

                entity.Property(e => e.ActivityWay)
                    .HasMaxLength(255)
                    .HasComment("活动方式");

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("添加时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("活动地点");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.AdministratorUuid);

                entity.HasComment("网格员信息表");

                entity.Property(e => e.AdministratorUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdminAddress)
                    .HasMaxLength(255)
                    .HasComment("分管区域	(无");

                entity.Property(e => e.AdminQuanxian)
                    .HasMaxLength(255)
                    .HasComment("管理权限(无");

                entity.Property(e => e.AdminVillages)
                    .HasMaxLength(100)
                    .HasComment("行政村");

                entity.Property(e => e.AdministratorName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.CunjiZhanghao)
                    .HasMaxLength(255)
                    .HasComment("村级账号");

                entity.Property(e => e.GriddingNum)
                    .HasMaxLength(255)
                    .HasComment("网格数");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号	");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系号码	");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别(无");

                entity.Property(e => e.SuozaiWangge)
                    .HasMaxLength(255)
                    .HasComment("所在网格");

                entity.Property(e => e.WanggeZhanghao)
                    .HasMaxLength(255)
                    .HasComment("网格账号");

                entity.Property(e => e.Wanggeyuan)
                    .HasMaxLength(255)
                    .HasComment("网格员");
            });

            modelBuilder.Entity<Alarmdata>(entity =>
            {
                entity.HasKey(e => e.AlarmUuid)
                    .HasName("PK__Alarmdat__FBCFCE024EBF2899");

                entity.HasComment("消防报警信息");

                entity.Property(e => e.AlarmUuid)
                    .HasColumnName("AlarmUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AlarmTypeId)
                    .HasMaxLength(255)
                    .HasComment("报警类型");

                entity.Property(e => e.AnalogValue)
                    .HasMaxLength(255)
                    .HasComment("模拟量值");

                entity.Property(e => e.AnalogValueTypeId)
                    .HasMaxLength(255)
                    .HasComment("模拟量类型");

                entity.Property(e => e.AudioUrl)
                    .HasMaxLength(255)
                    .HasComment("语音链接");

                entity.Property(e => e.AutoStatus)
                    .HasMaxLength(255)
                    .HasComment("是否设备自动上传0:否;1:是");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(255)
                    .HasComment("建筑ID");

                entity.Property(e => e.ChargingUserName)
                    .HasMaxLength(255)
                    .HasComment("用户名称");

                entity.Property(e => e.ChargingUserTel)
                    .HasMaxLength(255)
                    .HasComment("用户电话");

                entity.Property(e => e.ConfirmUserId)
                    .HasMaxLength(255)
                    .HasComment("事件确认人ID");

                entity.Property(e => e.DetectorId)
                    .HasMaxLength(255)
                    .HasComment("设备ID 该值为探测器设备主键id，主机报警是该值为-1，可根据该值信息在‘探测器信息’接口中获取探测器信息");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(255)
                    .HasComment("事件结束时间");

                entity.Property(e => e.EventAddress)
                    .HasMaxLength(255)
                    .HasComment("事件发生地");

                entity.Property(e => e.EventContent)
                    .HasMaxLength(255)
                    .HasComment("事件描述");

                entity.Property(e => e.EventStatus)
                    .HasMaxLength(255)
                    .HasComment("事件状态");

                entity.Property(e => e.EventTypeId)
                    .HasMaxLength(255)
                    .HasComment("事件类型");

                entity.Property(e => e.HandingTime)
                    .HasMaxLength(255)
                    .HasComment("处置时间/确认时间");

                entity.Property(e => e.HandlingSuggestion)
                    .HasMaxLength(255)
                    .HasComment("处理意见");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .HasComment("图片链接");

                entity.Property(e => e.IsHandled)
                    .HasMaxLength(255)
                    .HasComment("是否已处理");

                entity.Property(e => e.IsRead)
                    .HasColumnName("isRead")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("是否已读");

                entity.Property(e => e.MainframeId)
                    .HasMaxLength(255)
                    .HasComment("网关ID 该值为主机设备主键id，可根据该值信息在‘传输设备/网关信息’接口中获取主机信息");

                entity.Property(e => e.OrgId)
                    .HasMaxLength(255)
                    .HasComment("单位ID");

                entity.Property(e => e.PlatformCode)
                    .HasMaxLength(255)
                    .HasComment("平台编码");

                entity.Property(e => e.RecordCode)
                    .HasMaxLength(255)
                    .HasComment("记录号");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .HasComment("备注");

                entity.Property(e => e.ReportUserId)
                    .HasMaxLength(255)
                    .HasComment("上报人ID 该值为人员主键id，可根据该值信息在‘获取人员信息’接口中获取人员信息");

                entity.Property(e => e.ResetStatus)
                    .HasMaxLength(255)
                    .HasComment("复位状态");

                entity.Property(e => e.ResetTime)
                    .HasMaxLength(255)
                    .HasComment("复位时间");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(255)
                    .HasComment("事件开始时间");

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(255)
                    .HasComment("视频链接");
            });

            modelBuilder.Entity<Barometric>(entity =>
            {
                entity.HasKey(e => e.BarometricUuid)
                    .HasName("PK__Barometr__8938CB7CBEAC8103");

                entity.HasComment("大气信息表");

                entity.Property(e => e.BarometricUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.BarTime)
                    .HasMaxLength(255)
                    .HasComment("时间	");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Linjie)
                    .HasMaxLength(255)
                    .HasComment("临界点	");

                entity.Property(e => e.NowShuzhi)
                    .HasMaxLength(255)
                    .HasComment("当日数值	");
            });

            modelBuilder.Entity<BuildHouse>(entity =>
            {
                entity.HasKey(e => e.BuildHouseUuid);

                entity.HasComment("建房监控信息表");

                entity.Property(e => e.BuildHouseUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdministratorUuid)
                    .HasMaxLength(255)
                    .HasComment("网格员UUID");

                entity.Property(e => e.ApproveTime)
                    .HasMaxLength(255)
                    .HasComment("审批时间");

                entity.Property(e => e.ArtisanCred)
                    .HasMaxLength(255)
                    .HasComment("工匠证书");

                entity.Property(e => e.BuildArea)
                    .HasMaxLength(255)
                    .HasComment("建筑面积");

                entity.Property(e => e.HouseAddress)
                    .HasMaxLength(255)
                    .HasComment("位置");

                entity.Property(e => e.Household)
                    .HasMaxLength(255)
                    .HasComment("户主");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号	");

                entity.Property(e => e.LandNum)
                    .HasColumnName("LandNUm")
                    .HasMaxLength(255)
                    .HasComment("用地呈报表编号");

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.MonitorHouseId)
                    .HasMaxLength(255)
                    .HasComment("监控编号	");

                entity.Property(e => e.OccupyArea)
                    .HasMaxLength(255)
                    .HasComment("占地面积");

                entity.Property(e => e.PeopleNum).HasComment("人口");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系号码");

                entity.Property(e => e.PliesNum)
                    .HasMaxLength(255)
                    .HasComment("层数");

                entity.Property(e => e.ProjectCred)
                    .HasMaxLength(255)
                    .HasComment("规划许可证号");

                entity.Property(e => e.Town)
                    .HasMaxLength(255)
                    .HasComment("行政村");

                entity.Property(e => e.Way)
                    .HasMaxLength(255)
                    .HasComment("方式");
            });

            modelBuilder.Entity<Canyin>(entity =>
            {
                entity.HasKey(e => e.CanyinUuid);

                entity.HasComment("餐饮店信息表");

                entity.Property(e => e.CanyinUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.CanyinAddress)
                    .HasMaxLength(255)
                    .HasComment("所处位置");

                entity.Property(e => e.CanyinName)
                    .HasMaxLength(255)
                    .HasComment("店名");

                entity.Property(e => e.Fuzeren)
                    .HasMaxLength(255)
                    .HasComment("店主");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系电话");

                entity.Property(e => e.Staues)
                    .HasMaxLength(255)
                    .HasComment("状态");

                entity.Property(e => e.Town)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");
            });

            modelBuilder.Entity<CarAnFang>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__CarAnFan__A2B5777C545233D9");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Ability)
                    .HasColumnName("ability")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AlarmTypeName)
                    .HasColumnName("alarmTypeName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CrossTime)
                    .HasColumnName("crossTime")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HappenTime)
                    .HasColumnName("happenTime")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageIndexCode)
                    .HasColumnName("imageIndexCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MixedName)
                    .HasColumnName("mixedName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MonitorName)
                    .HasColumnName("monitorName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PersonName)
                    .HasColumnName("personName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("phoneNo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlatePicUrl)
                    .HasColumnName("platePicUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlateTypeName)
                    .HasColumnName("plateTypeName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VehiclePicUrl)
                    .HasColumnName("vehiclePicUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CityManagement>(entity =>
            {
                entity.HasKey(e => e.CityManagementUuid);

                entity.HasComment("城管执法信息记录表");

                entity.Property(e => e.CityManagementUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.BeiChulirren)
                    .HasMaxLength(255)
                    .HasComment("被处理人	");

                entity.Property(e => e.ChuliQingk)
                    .HasMaxLength(255)
                    .HasComment("处理情况");

                entity.Property(e => e.Chuliren)
                    .HasMaxLength(255)
                    .HasComment("处理人");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Incident)
                    .HasMaxLength(255)
                    .HasComment("事件");

                entity.Property(e => e.ZhifaAddress)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.ZhifaRen)
                    .HasMaxLength(255)
                    .HasComment("执法人	");

                entity.Property(e => e.ZhifaTime)
                    .HasMaxLength(255)
                    .HasComment("时间");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyUuid);

                entity.HasComment("公司表");

                entity.Property(e => e.CompanyUuid)
                    .HasColumnName("CompanyUUID")
                    .HasComment("公司UUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.BusinessLogin)
                    .HasMaxLength(255)
                    .HasComment("工商注册号");

                entity.Property(e => e.CompanyAddress)
                    .HasMaxLength(255)
                    .HasComment("企业地址");

                entity.Property(e => e.CompanyEmail)
                    .HasMaxLength(255)
                    .HasComment("企业邮箱");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .HasComment("公司名称");

                entity.Property(e => e.CompanyPhone)
                    .HasMaxLength(100)
                    .HasComment("企业联系电话 ");

                entity.Property(e => e.CompanyType)
                    .HasMaxLength(100)
                    .HasComment("企业类型");

                entity.Property(e => e.District)
                    .HasMaxLength(100)
                    .HasComment("所属地区");

                entity.Property(e => e.EngName)
                    .HasMaxLength(255)
                    .HasComment("英文名 ");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Industry)
                    .HasColumnName("industry")
                    .HasMaxLength(100)
                    .HasComment("所属行业");

                entity.Property(e => e.LegalPeople)
                    .HasMaxLength(100)
                    .HasComment("法定代表人");

                entity.Property(e => e.Officialwebsite)
                    .HasMaxLength(255)
                    .HasComment("企业官网");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .HasComment("状态");

                entity.Property(e => e.SystemUserUuid)
                    .HasColumnName("SystemUserUUID")
                    .HasComment("用户UUID");

                entity.Property(e => e.UnifiedSocialCode)
                    .HasMaxLength(255)
                    .HasComment("统一社会信用代码");
            });

            modelBuilder.Entity<ConcerQiao>(entity =>
            {
                entity.HasKey(e => e.ConcerningQiaoUuid);

                entity.HasComment("涉侨人员信息表");

                entity.Property(e => e.ConcerningQiaoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Cnaddress)
                    .HasColumnName("CNAddress")
                    .HasComment("中国居住地");

                entity.Property(e => e.ConcerningType)
                    .HasMaxLength(255)
                    .HasComment("涉侨类型:归侨、侨眷、港澳同胞眷属、归国留学人员、海外留学人员眷属、华侨、港澳同胞及外籍华人");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasComment("邮箱");

                entity.Property(e => e.Gwaddress)
                    .HasColumnName("GWAddress")
                    .HasComment("国外所在地");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("手机号");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.UserIdCardNum)
                    .HasMaxLength(255)
                    .HasComment("证件号码");

                entity.Property(e => e.UserIdCardType)
                    .HasMaxLength(255)
                    .HasComment("证件类型");

                entity.Property(e => e.Xjaddress)
                    .HasColumnName("XJAddress")
                    .HasComment("先居地址");
            });

            modelBuilder.Entity<CpcmanInfo>(entity =>
            {
                entity.HasKey(e => e.CpcmanUuid);

                entity.HasComment("党员基本信息");

                entity.Property(e => e.CpcmanUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Age).HasComment("年龄");

                entity.Property(e => e.Birth)
                    .HasColumnType("datetime")
                    .HasComment("生日");

                entity.Property(e => e.DangOrganizationName).HasMaxLength(255);

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .HasComment("学历");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Politics)
                    .HasMaxLength(255)
                    .HasComment("面貌");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.TableNum).HasMaxLength(255);
            });

            modelBuilder.Entity<Cunzhenmb>(entity =>
            {
                entity.HasKey(e => e.CunzhenmbUuid)
                    .HasName("PK__Cunzhenm__8A89C1FBB42897D1");

                entity.HasComment("村镇民兵");

                entity.Property(e => e.CunzhenmbUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.Age).HasComment("年龄");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("生日");

                entity.Property(e => e.DepartmentUuid)
                    .HasColumnName("DepartmentUUID")
                    .HasComment("相关部门");

                entity.Property(e => e.Domicile)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .HasComment("学历");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasComment("邮箱");

                entity.Property(e => e.Household)
                    .HasMaxLength(255)
                    .HasComment("户别");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("电话号码");

                entity.Property(e => e.QianYiEtime)
                    .HasColumnName("QianYiETime")
                    .HasMaxLength(255)
                    .HasComment("迁出时间");

                entity.Property(e => e.QianYiStime)
                    .HasColumnName("QianYiSTime")
                    .HasMaxLength(255)
                    .HasComment("迁入时间");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.Relation)
                    .HasMaxLength(100)
                    .HasComment("与户主的关系");

                entity.Property(e => e.Residence)
                    .HasMaxLength(255)
                    .HasComment("户籍地");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.UserIdCard).HasMaxLength(255);

                entity.Property(e => e.Wechat)
                    .HasMaxLength(255)
                    .HasComment("微信");
            });

            modelBuilder.Entity<DangActivity>(entity =>
            {
                entity.HasKey(e => e.DangActivityUuid);

                entity.HasComment("党组织活动");

                entity.Property(e => e.DangActivityUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Content).HasComment("内容");

                entity.Property(e => e.Cover).HasComment("封面");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否删除");

                entity.Property(e => e.IsRelease)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否发布");

                entity.Property(e => e.Photo).HasComment("3张图片");

                entity.Property(e => e.ReleaseTime).HasComment("活动时间");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("标题");

                entity.Property(e => e.VillageUuid)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");
            });

            modelBuilder.Entity<DangOrganization>(entity =>
            {
                entity.HasKey(e => e.DangOrganizationUuid);

                entity.HasComment("党组织基本信息");

                entity.Property(e => e.DangOrganizationUuid).ValueGeneratedNever();

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.ChuangliTime)
                    .HasMaxLength(255)
                    .HasComment("创立时间");

                entity.Property(e => e.DangClerk).HasMaxLength(255);

                entity.Property(e => e.DangOrganizationName)
                    .HasMaxLength(255)
                    .HasComment("组织名称");

                entity.Property(e => e.DangOrganizationPeople)
                    .HasMaxLength(255)
                    .HasComment("组织人数");

                entity.Property(e => e.DangOrganizationRemarks)
                    .HasMaxLength(255)
                    .HasComment("组织简介");

                entity.Property(e => e.DangType)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<DangerousGoods>(entity =>
            {
                entity.HasKey(e => e.MentallyUuid)
                    .HasName("PK__Dangerou__8F8AEFFF28408AF7");

                entity.HasComment("危险品从业人口");

                entity.Property(e => e.MentallyUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("工作单位");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("血型");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("联系手机");

                entity.Property(e => e.CorporatePhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("企业法人联系电话");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("现住地址");

                entity.Property(e => e.Danger)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("危险从业类别");

                entity.Property(e => e.DateBirth).HasComment(@"出生日期

出生日期");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("文化程度");

                entity.Property(e => e.EffectiveTime)
                    .HasColumnName("Effective time")
                    .HasComment("有效期开始日期");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("电子邮件");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注程度");

                entity.Property(e => e.EndTime).HasComment("有效期结束日期");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("曾用名");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身高");

                entity.Property(e => e.HousesNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("房屋编号");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.IsDeleted).HasComment("0.未删 1.已删");

                entity.Property(e => e.LegalPerson)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("企业法人代表");

                entity.Property(e => e.LegalPersonPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("企业法人联系手机");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("婚姻状况");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("职业");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("其他地址");

                entity.Property(e => e.OwningGrid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网格");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("联系电话");

                entity.Property(e => e.PoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("政治面貌");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍地详址");

                entity.Property(e => e.Religious)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("宗教信仰");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍地址");

                entity.Property(e => e.RoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("无房原因");

                entity.Property(e => e.ServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("最新服务时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"性别

性别");

                entity.Property(e => e.Waiter)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("有无服务成员");

                entity.Property(e => e.Work)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("从业资格证书");

                entity.Property(e => e.WorkNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("从业资格证书号");
            });

            modelBuilder.Entity<DangerousRoom>(entity =>
            {
                entity.HasKey(e => e.DangerousRoomUuid);

                entity.HasComment("危房信息表");

                entity.Property(e => e.DangerousRoomUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdministratorName)
                    .HasMaxLength(255)
                    .HasComment("网格员名字");

                entity.Property(e => e.AdministratorPhone)
                    .HasMaxLength(255)
                    .HasComment("网格员电话");

                entity.Property(e => e.AdministratorUuid).HasComment("网格员UUID");

                entity.Property(e => e.Areaid).HasComment("行政区划编码");

                entity.Property(e => e.Areaname)
                    .HasMaxLength(255)
                    .HasComment("行政区划名称");

                entity.Property(e => e.Buildingid)
                    .HasMaxLength(255)
                    .HasComment("建设楼幢ID");

                entity.Property(e => e.Communityname)
                    .HasMaxLength(255)
                    .HasComment("社区");

                entity.Property(e => e.DangerousAddress).HasComment("位置");

                entity.Property(e => e.DangerousMaster)
                    .HasMaxLength(255)
                    .HasComment("户主");

                entity.Property(e => e.DangerousPhone)
                    .HasMaxLength(255)
                    .HasComment("联系电话");

                entity.Property(e => e.Gisx)
                    .HasMaxLength(1)
                    .HasComment("GIS-X");

                entity.Property(e => e.Gisy)
                    .HasMaxLength(255)
                    .HasComment("GIS-Y");

                entity.Property(e => e.Ib)
                    .HasMaxLength(255)
                    .HasComment("类别");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Jdbgcjsj).HasComment("鉴定时间");

                entity.Property(e => e.Jdjl)
                    .HasMaxLength(255)
                    .HasComment("鉴定等级");

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.Streetname)
                    .HasMaxLength(255)
                    .HasComment("街道（乡镇）");

                entity.Property(e => e.TownUuid).HasComment("所属村镇UUID");

                entity.Property(e => e.Xcyq)
                    .HasMaxLength(255)
                    .HasComment("危房巡查要求");

                entity.Property(e => e.Xmmc)
                    .HasMaxLength(255)
                    .HasComment("小区（项目）名称");

                entity.Property(e => e.Yhorgid)
                    .HasMaxLength(255)
                    .HasComment("组织唯一编码");

                entity.Property(e => e.Zldz)
                    .HasMaxLength(255)
                    .HasComment("坐落地址");
            });

            modelBuilder.Entity<DepartSumary>(entity =>
            {
                entity.HasKey(e => e.DeSumaryUuid)
                    .HasName("PK__DepartSu__9266D7B36D6A8F93");

                entity.HasComment("科室总结表");

                entity.Property(e => e.DeSumaryUuid)
                    .HasColumnName("DeSumaryUUID")
                    .HasComment("科室总结UUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeSuAddTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("创建时间");

                entity.Property(e => e.DeSuDescribe).HasComment("科室总结详情内容");

                entity.Property(e => e.DeSuDocument).HasComment("附件名");

                entity.Property(e => e.DeSuHeadLine).HasComment("科室总结标题");

                entity.Property(e => e.DeSumaryId)
                    .HasColumnName("DeSumaryID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DepartName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MissionId)
                    .HasColumnName("MissionID")
                    .HasComment("对应科室UUID");

                entity.Property(e => e.PriorityId)
                    .HasColumnName("PriorityID")
                    .HasComment("对应的重点工作UUID");

                entity.Property(e => e.SyUserUuid).HasColumnName("SyUserUUID");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentUuid)
                    .HasName("PK__Departme__1AA1F4C05ED85AB0");

                entity.HasComment("部门");

                entity.Property(e => e.DepartmentUuid)
                    .HasColumnName("DepartmentUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Dingid)
                    .HasMaxLength(255)
                    .HasComment("钉钉部门id");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("添加时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除（0.未删除，1已删除）");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");
            });

            modelBuilder.Entity<DepartmentDeclare>(entity =>
            {
                entity.HasKey(e => e.DepartmentDeclareUuid)
                    .HasName("PK__Departme__B8057B5833660963");

                entity.HasComment("绩效申报");

                entity.Property(e => e.DepartmentDeclareUuid)
                    .HasColumnName("DepartmentDeclareUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AuditOpinion)
                    .HasMaxLength(255)
                    .HasComment("审核意见");

                entity.Property(e => e.AuditStatus)
                    .HasMaxLength(255)
                    .HasComment("审核状态（1.待审核，2.审核通过，3.审核不通过）");

                entity.Property(e => e.BonusPoint)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("加分项");

                entity.Property(e => e.DeclareDepartment)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("部门");

                entity.Property(e => e.DeclareName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.DeclareTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("时间");

                entity.Property(e => e.Deduction)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("减分项");

                entity.Property(e => e.DeductionContent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("减分内容");

                entity.Property(e => e.DeductionScore).HasComment("减分分值");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("添加时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.PerformanceDeclareUuid).HasColumnName("PerformanceDeclareUUID");

                entity.Property(e => e.PlusContent)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("加分分值");

                entity.Property(e => e.PlusScore).HasComment("加分分值");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");
            });

            modelBuilder.Entity<DisasterInfo>(entity =>
            {
                entity.HasKey(e => e.DisasterInfoUuid)
                    .HasName("PK_Table_1");

                entity.HasComment("其他灾害点信息表");

                entity.Property(e => e.DisasterInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdministratorName)
                    .HasMaxLength(255)
                    .HasComment("网格员名称");

                entity.Property(e => e.AdministratorPhone)
                    .HasMaxLength(255)
                    .HasComment("网格员电话");

                entity.Property(e => e.AdministratorUuid).HasComment("网格员UUID");

                entity.Property(e => e.DisasterAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("灾害位置");

                entity.Property(e => e.DisasterName)
                    .HasMaxLength(255)
                    .HasComment("灾害名称");

                entity.Property(e => e.DisasterStaues)
                    .HasMaxLength(255)
                    .HasComment("处理状态");

                entity.Property(e => e.DisasterTime)
                    .HasMaxLength(255)
                    .HasComment("发生时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.MapRegion).HasComment("地图区域");

                entity.Property(e => e.TownUuid).HasComment("所属村镇UUID");
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.HasKey(e => e.DrugUuid)
                    .HasName("PK__Drug__40359CD1809813EF");

                entity.HasComment("吸毒人员表");

                entity.Property(e => e.DrugUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注程度");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("血型");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("联系手机");

                entity.Property(e => e.ControlStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("管控现状");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("现住地址");

                entity.Property(e => e.DateBirth).HasComment(@"出生日期

出生日期");

                entity.Property(e => e.Detoxification)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("戒毒情况");

                entity.Property(e => e.Detoxificationes)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("是否服用美沙酮戒毒");

                entity.Property(e => e.DrugStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("吸毒状态");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("文化程度");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("电子邮件");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("工作单位");

                entity.Property(e => e.FirstTime).HasComment("首吸时间");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("曾用名");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身高");

                entity.Property(e => e.HousesNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("房屋编号");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.IsDeleted).HasComment("0.未删 1.已删");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("婚姻状况");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("职业");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("其他地址");

                entity.Property(e => e.OwningGrid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网格");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("联系电话");

                entity.Property(e => e.PoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("政治面貌");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍地详址");

                entity.Property(e => e.Religious)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("宗教信仰");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍地址");

                entity.Property(e => e.RoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("无房原因");

                entity.Property(e => e.Seizeddate)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("查获日期");

                entity.Property(e => e.ServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("最新服务时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"性别

性别");

                entity.Property(e => e.SourceDrugs)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("毒品来源");

                entity.Property(e => e.Species)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("滥用毒品种类");

                entity.Property(e => e.Waiter)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("有无服务成员");
            });

            modelBuilder.Entity<DzhizaihaiInfo>(entity =>
            {
                entity.HasKey(e => e.DzhizaihaiInfoUuid);

                entity.HasComment("地质灾害点信息表");

                entity.Property(e => e.DzhizaihaiInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Diqu)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("地区");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Shuliang)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("数量");
            });

            modelBuilder.Entity<Emergency>(entity =>
            {
                entity.HasKey(e => e.EmergencyUuid);

                entity.HasComment("应急响应信息表");

                entity.Property(e => e.EmergencyUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.EmergencyDengji)
                    .HasMaxLength(255)
                    .HasComment("响应等级");

                entity.Property(e => e.EmergencyQuyu)
                    .HasMaxLength(255)
                    .HasComment("响应区域	");

                entity.Property(e => e.FuzerPhone)
                    .HasMaxLength(255)
                    .HasComment("负责人电话");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.QuyuFuzer)
                    .HasMaxLength(255)
                    .HasComment("区域负责人");

                entity.Property(e => e.RescueTeamUuid)
                    .HasMaxLength(255)
                    .HasComment("救援小队Uuid");
            });

            modelBuilder.Entity<Evacuate>(entity =>
            {
                entity.HasKey(e => e.EvacuateUuid)
                    .HasName("PK__Evacuate__9784AC1A11AAE900");

                entity.HasComment("疏散人员信息表");

                entity.Property(e => e.EvacuateUuid)
                    .HasColumnName("EvacuateUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EvacuateTime)
                    .HasMaxLength(50)
                    .HasComment("疏散时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(20)
                    .HasComment("身份证号");

                entity.Property(e => e.IsDeleted).HasComment("0.未 1.已");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasComment("人员姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(5)
                    .HasComment("民族");

                entity.Property(e => e.Origion)
                    .HasMaxLength(50)
                    .HasComment("来源");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasComment("电话");

                entity.Property(e => e.SceneSpot)
                    .HasMaxLength(50)
                    .HasComment("景区");

                entity.Property(e => e.Sex)
                    .HasMaxLength(2)
                    .HasComment("性别");

                entity.Property(e => e.State).HasComment("状态");
            });

            modelBuilder.Entity<FangXun>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__FangXun__497F6CB42F59CD5B");

                entity.HasComment("防汛防台应急预案任务分解表");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Command)
                    .HasColumnName("command")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("总指挥");

                entity.Property(e => e.Governmentofficials)
                    .HasColumnName("governmentofficials")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("机关干部");

                entity.Property(e => e.Jobrequirements)
                    .HasColumnName("jobrequirements")
                    .IsUnicode(false)
                    .HasComment("工作要求");

                entity.Property(e => e.Member)
                    .HasColumnName("member")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("成员");

                entity.Property(e => e.Personinchargeofvillageenterprise)
                    .HasColumnName("personinchargeofvillageenterprise")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("村企负责人");

                entity.Property(e => e.Teamleader)
                    .HasColumnName("teamleader")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("专班组长");

                entity.Property(e => e.Workingclass)
                    .HasColumnName("workingclass")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("工作专班");
            });

            modelBuilder.Entity<Followvillager>(entity =>
            {
                entity.HasKey(e => e.FollowvillagerUuid);

                entity.HasComment("乡贤信息表");

                entity.Property(e => e.FollowvillagerUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.FollowvillagerName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号	");

                entity.Property(e => e.Jieshao).HasComment("介绍");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("电话号码	");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别	");

                entity.Property(e => e.Shiji).HasComment("基本事迹");

                entity.Property(e => e.Staues)
                    .HasMaxLength(255)
                    .HasComment("状态");

                entity.Property(e => e.VillageUuid).HasComment("所属村镇UUID	");
            });

            modelBuilder.Entity<Gtoilet>(entity =>
            {
                entity.HasKey(e => e.GtoiletUuid);

                entity.HasComment("3A公厕信息表");

                entity.Property(e => e.GtoiletUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.GtoiletAddress)
                    .HasMaxLength(255)
                    .HasComment("位置");

                entity.Property(e => e.GtoiletName)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.GtoiletStaues)
                    .HasMaxLength(255)
                    .HasComment("使用情况");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.KongqiZhil)
                    .HasMaxLength(255)
                    .HasComment("空气质量");

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.Picture).HasMaxLength(255);

                entity.Property(e => e.WaterYujin)
                    .HasMaxLength(255)
                    .HasComment("水量预警");
            });

            modelBuilder.Entity<HeDaowater>(entity =>
            {
                entity.HasKey(e => e.HeDaowaterUuid);

                entity.HasComment("河道水位监测");

                entity.Property(e => e.HeDaowaterUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.CurrentPrecipitation)
                    .HasColumnType("decimal(6, 1)")
                    .HasComment("当前降水量");

                entity.Property(e => e.FangxunDj)
                    .HasMaxLength(255)
                    .HasComment("防汛等级");

                entity.Property(e => e.HeDaoHeDaowaterSw)
                    .HasMaxLength(255)
                    .HasComment("水位");

                entity.Property(e => e.HeDaowaterAddress)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.HeDaowaterTime)
                    .HasMaxLength(255)
                    .HasComment("时间");

                entity.Property(e => e.HeDaowaterYujin)
                    .HasMaxLength(255)
                    .HasComment("水位预警");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ljiedian)
                    .HasMaxLength(255)
                    .HasComment("临界点");
            });

            modelBuilder.Entity<HomeAddress>(entity =>
            {
                entity.HasKey(e => e.HomeAddressUuid)
                    .IsClustered(false);

                entity.HasComment("地址信息表");

                entity.Property(e => e.HomeAddressUuid)
                    .HasColumnName("HomeAddressUUID")
                    .HasComment("UUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address)
                    .IsUnicode(false)
                    .HasComment("家庭地址");

                entity.Property(e => e.Addresscode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("地址编码");

                entity.Property(e => e.Building)
                    .HasColumnName("building")
                    .HasMaxLength(255)
                    .HasComment("建筑物");

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("building_num")
                    .HasMaxLength(255)
                    .HasComment("楼牌");

                entity.Property(e => e.Ccmmunity)
                    .HasColumnName("ccmmunity")
                    .HasMaxLength(255)
                    .HasComment("社区");

                entity.Property(e => e.Door)
                    .HasColumnName("door")
                    .HasMaxLength(255)
                    .HasComment("门牌");

                entity.Property(e => e.Floor)
                    .HasColumnName("floor")
                    .HasMaxLength(255)
                    .HasComment("楼层");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasColumnName("lat")
                    .HasColumnType("decimal(10, 7)")
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasColumnName("lon")
                    .HasColumnType("decimal(10, 7)")
                    .HasComment("经度");

                entity.Property(e => e.Resregion)
                    .HasColumnName("resregion")
                    .HasMaxLength(255)
                    .HasComment("小区");

                entity.Property(e => e.Room)
                    .HasColumnName("room")
                    .HasMaxLength(255)
                    .HasComment("户室");

                entity.Property(e => e.RoomFloor).HasColumnName("room_floor");

                entity.Property(e => e.Squad)
                    .HasColumnName("squad")
                    .HasMaxLength(255)
                    .HasComment("行政组");

                entity.Property(e => e.Street)
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .HasComment("街路巷");

                entity.Property(e => e.Szone)
                    .HasColumnName("szone")
                    .HasMaxLength(255)
                    .HasComment("专业区");

                entity.Property(e => e.Town)
                    .HasColumnName("town")
                    .HasMaxLength(255)
                    .HasComment("街道");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(255)
                    .HasComment("单元");

                entity.Property(e => e.Village)
                    .HasColumnName("village")
                    .HasMaxLength(255)
                    .HasComment("自然村");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.HotelUuid);

                entity.HasComment("民宿信息表");

                entity.Property(e => e.HotelUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.HotelLocation).HasComment("民宿地址");

                entity.Property(e => e.HotelName).HasComment("民宿名称");

                entity.Property(e => e.HotelPeoPle).HasComment("联系人");

                entity.Property(e => e.HotelPhone).HasComment("联系方式");

                entity.Property(e => e.HotelRecommend).HasComment("民宿介绍");

                entity.Property(e => e.HotelType)
                    .HasMaxLength(255)
                    .HasComment("民宿状态");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Village)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("所属村");
            });

            modelBuilder.Entity<HqCommunal>(entity =>
            {
                entity.HasKey(e => e.HqCommunalUuid);

                entity.HasComment("河桥镇公共设施信息");

                entity.Property(e => e.HqCommunalUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.HqCommunalId)
                    .HasColumnName("HqCommunalID")
                    .HasMaxLength(255)
                    .HasComment("设施编号	");

                entity.Property(e => e.HqCommunalLocation).HasComment("设施位置");

                entity.Property(e => e.HqCommunalName)
                    .HasMaxLength(255)
                    .HasComment("设施名称");

                entity.Property(e => e.HqCommunalStaues)
                    .HasMaxLength(50)
                    .HasComment("设施状态	");

                entity.Property(e => e.HqCommunalType)
                    .HasMaxLength(50)
                    .HasComment("设施类型");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.TownUuid).HasComment("所属村镇UUID");
            });

            modelBuilder.Entity<KeyYouthList>(entity =>
            {
                entity.HasKey(e => e.KeyYouthListUuid)
                    .HasName("PK__KeyYouth__A2DECBFC805AEB33");

                entity.HasComment(@"重点青少年清单																														
");

                entity.Property(e => e.KeyYouthListUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"关注程度

");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"血型

");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系电话

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.CurrentAddress1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"出生日期

");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"文化程度

");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"电子邮件

");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"工作单位

");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"曾用名

");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"身高

");

                entity.Property(e => e.HouseholdRegistrationPoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.LatestServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"最新服务时间

");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"婚姻状况

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"民族

");

                entity.Property(e => e.NoRoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"无房原因

");

                entity.Property(e => e.NumberOfTheHouse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"房屋编号

");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"职业

");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"其他地址

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PersonType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"人员类型

");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"政治面貌

");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地详址

");

                entity.Property(e => e.ReligiousBelief)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"宗教信仰

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地址

");

                entity.Property(e => e.ServiceMembers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有无服务成员

");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");
            });

            modelBuilder.Entity<ListOfKeyPetitioners>(entity =>
            {
                entity.HasKey(e => e.ListOfKeyPetitionersUuid)
                    .HasName("PK__ListOfKe__4E6912414B46FF96");

                entity.HasComment("重点上访人员清单");

                entity.Property(e => e.ListOfKeyPetitionersUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"关注程度

");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"血型

");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系电话

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"出生日期

");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"文化程度

");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"电子邮件

");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"工作单位

");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"曾用名

");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"身高

");

                entity.Property(e => e.HouseholdRegistrationPoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.LatestServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"最新服务时间

");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"婚姻状况

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"民族

");

                entity.Property(e => e.NoRoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"无房原因

");

                entity.Property(e => e.NumberOfTheHouse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"房屋编号

");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"职业

");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"其他地址

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PetitionStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"上访状态

");

                entity.Property(e => e.PetitionType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"上访类型

");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"政治面貌

");

                entity.Property(e => e.ReasonForPetition)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"上访原因
");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地详址

");

                entity.Property(e => e.ReligiousBelief)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"宗教信仰

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地址

");

                entity.Property(e => e.ServiceMembers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有无服务成员

");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");
            });

            modelBuilder.Entity<MaxOrMin>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__MaxOrMin__A2B5777CAE6F06A4");

                entity.HasComment("最大人口&最小人口表");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.MaxCount)
                    .IsUnicode(false)
                    .HasComment("最大人口");

                entity.Property(e => e.MinCount)
                    .IsUnicode(false)
                    .HasComment("最小人口");
            });

            modelBuilder.Entity<Mentally>(entity =>
            {
                entity.HasKey(e => e.MentallyUuid)
                    .HasName("PK__Mentally__3214EC2731634DA5");

                entity.HasComment("精神病人员清单");

                entity.Property(e => e.MentallyUuid).ValueGeneratedNever();

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("血型");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("联系手机");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("现住地址");

                entity.Property(e => e.Danger)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("危险程度");

                entity.Property(e => e.DateBirth).HasComment("出生日期");

                entity.Property(e => e.DiseaseName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("患病名称");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("文化程度");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("电子邮件");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("工作单位");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("曾用名");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身高");

                entity.Property(e => e.HousesNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("房屋编号");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.IsDeleted).HasComment("0.未删 1.已删");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("婚姻状况");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("职业");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("其他地址");

                entity.Property(e => e.OwningGrid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网格");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("联系电话");

                entity.Property(e => e.PoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("政治面貌");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍地详址");

                entity.Property(e => e.Rehabilitation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("康复机构");

                entity.Property(e => e.Religious)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("宗教信仰");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍地址");

                entity.Property(e => e.RoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("无房原因");

                entity.Property(e => e.ServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("最新服务时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.Treatment)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("是否接受过治疗");

                entity.Property(e => e.Waiter)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("有无服务成员");
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.HasKey(e => e.MissionUuid)
                    .HasName("PK__Mission__29F99DBB4288D7C0");

                entity.HasComment("任务");

                entity.Property(e => e.MissionUuid)
                    .HasColumnName("MissionUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accomplish)
                    .HasMaxLength(255)
                    .HasComment("是否完成（0.未完成，1.完成）");

                entity.Property(e => e.AdministrativeOffice)
                    .HasMaxLength(255)
                    .HasComment("科室");

                entity.Property(e => e.Approver).HasComment("审批人");

                entity.Property(e => e.AuditOpinion)
                    .HasMaxLength(255)
                    .HasComment("审核意见");

                entity.Property(e => e.AuditStatus)
                    .HasMaxLength(255)
                    .HasComment("状态（0,.办理中,,2.已完成,3.已逾期）");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("创建人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("创建时间");

                entity.Property(e => e.FinishTime)
                    .HasMaxLength(255)
                    .HasComment("结束时间");

                entity.Property(e => e.Fujian)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("附件");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsAssigned)
                    .HasColumnName("isAssigned")
                    .HasComment("指派部门");

                entity.Property(e => e.IsSave)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Isouttime)
                    .HasMaxLength(255)
                    .HasComment("是否逾期");

                entity.Property(e => e.Manhour)
                    .HasMaxLength(255)
                    .HasComment("计划工时");

                entity.Property(e => e.MissionDescribe)
                    .HasMaxLength(255)
                    .HasComment("任务描述");

                entity.Property(e => e.MissionHeadline)
                    .HasMaxLength(255)
                    .HasComment("任务标题");

                entity.Property(e => e.Missiontype)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("任务类型(0,上级交办,1下级上传)");

                entity.Property(e => e.Participant).HasComment("参与人");

                entity.Property(e => e.Preserve)
                    .HasMaxLength(255)
                    .HasComment("是否保存（1.未保存，2.保存）");

                entity.Property(e => e.Principal).HasComment("负责人");

                entity.Property(e => e.Priority)
                    .HasMaxLength(255)
                    .HasComment("优先级");

                entity.Property(e => e.PriorityUuid)
                    .HasColumnName("PriorityUUID")
                    .HasComment("所属重点工作");

                entity.Property(e => e.Recycle)
                    .HasMaxLength(255)
                    .HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .HasComment("备注");

                entity.Property(e => e.Sortord)
                    .HasMaxLength(255)
                    .HasComment("任务完成时间");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(255)
                    .HasComment("开始时间");

                entity.Property(e => e.ZhiDingTime)
                    .HasMaxLength(255)
                    .HasComment("置顶时间");
            });

            modelBuilder.Entity<MissionJournal>(entity =>
            {
                entity.HasKey(e => e.MissionJournalUuid)
                    .HasName("PK__MissionJ__25CCA6EE97AEC616");

                entity.HasComment("任务完成情况");

                entity.Property(e => e.MissionJournalUuid)
                    .HasColumnName("MissionJournalUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accessory)
                    .HasMaxLength(255)
                    .HasComment("附件");

                entity.Property(e => e.Completed)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("已完成");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasComment("内容");

                entity.Property(e => e.Coordination)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("需要协调");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("创建人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("创建时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.MissionUuid)
                    .HasColumnName("MissionUUID")
                    .HasComment("任务uuid");

                entity.Property(e => e.Read)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("已读人员id");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.NoteUuid)
                    .HasName("PK__Note__ECD9452CACE767CC");

                entity.HasComment("短信表-钉钉");

                entity.Property(e => e.NoteUuid)
                    .HasColumnName("NoteUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("内容");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("添加时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModuleNaem)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("任务uuid");

                entity.Property(e => e.ModuleUuid)
                    .HasColumnName("ModuleUUID")
                    .HasComment("接收人uuid");

                entity.Property(e => e.Naem)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("名称");
            });

            modelBuilder.Entity<OrganPeoInfo>(entity =>
            {
                entity.HasKey(e => e.OrganPeoInfoUuid);

                entity.HasComment("工会人员信息表");

                entity.Property(e => e.OrganPeoInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("出生日期");

                entity.Property(e => e.Duty)
                    .HasMaxLength(255)
                    .HasComment("职位");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .HasComment("学历");

                entity.Property(e => e.HouseAddress)
                    .HasMaxLength(255)
                    .HasComment("家庭地址");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号");

                entity.Property(e => e.ManType)
                    .HasMaxLength(255)
                    .HasComment("妇联类型");

                entity.Property(e => e.OrganName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.OrganizationUuid).HasComment("组织UUID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("手机号");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.Specific).HasComment("具体情况");

                entity.Property(e => e.TutelageName)
                    .HasMaxLength(255)
                    .HasComment("负责人");

                entity.Property(e => e.TutelagePhone)
                    .HasMaxLength(255)
                    .HasComment("负责电话");

                entity.Property(e => e.ZjWork)
                    .HasColumnName("zjWork")
                    .HasMaxLength(255)
                    .HasComment("工作");

                entity.HasOne(d => d.OrganizationUu)
                    .WithMany(p => p.OrganPeoInfo)
                    .HasForeignKey(d => d.OrganizationUuid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrganPeoInfo_FK");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrganizationUuid);

                entity.HasComment("工会信息表");

                entity.Property(e => e.OrganizationUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.ChuangliTime)
                    .HasMaxLength(255)
                    .HasComment("创立时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(255)
                    .HasComment("组织名称");

                entity.Property(e => e.OrganizationPeople)
                    .HasMaxLength(255)
                    .HasComment("组织人数");

                entity.Property(e => e.OrganizationRemarks)
                    .HasMaxLength(255)
                    .HasComment("组织简介");
            });

            modelBuilder.Entity<Otherstaffs>(entity =>
            {
                entity.HasKey(e => e.OtherstaffsUuid)
                    .HasName("PK__Othersta__CCBB1B00208C9D34")
                    .IsClustered(false);

                entity.HasComment("其他人员信息表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Otherstaffs")
                    .IsClustered();

                entity.Property(e => e.OtherstaffsUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注程度");

                entity.Property(e => e.AttentionCause)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注原因");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"血型

");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系电话

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"出生日期

");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"文化程度

");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"电子邮件

");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"工作单位

");

                entity.Property(e => e.FamilyPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("家庭成员联系电话");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"曾用名

");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"身高

");

                entity.Property(e => e.HouseholdRegistrationPoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.LatestServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"最新服务时间

");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"婚姻状况

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"民族

");

                entity.Property(e => e.NoRoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"无房原因

");

                entity.Property(e => e.NumberOfTheHouse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"房屋编号

");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"职业

");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"其他地址

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"政治面貌

");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地详址

");

                entity.Property(e => e.ReligiousBelief)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"宗教信仰

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地址

");

                entity.Property(e => e.ServiceMembers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有无服务成员

");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.WorkCondition)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("工作情况");
            });

            modelBuilder.Entity<PaibanInfo>(entity =>
            {
                entity.HasKey(e => e.PaibanInfoUuid)
                    .HasName("PK_paibanInfo");

                entity.HasComment("排班信息表");

                entity.Property(e => e.PaibanInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("删除状态");

                entity.Property(e => e.ZbBc)
                    .HasMaxLength(255)
                    .HasComment("值班班次");

                entity.Property(e => e.ZbLingdao)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("值班(住夜)领导");

                entity.Property(e => e.ZbTime)
                    .HasMaxLength(255)
                    .HasComment("号数");

                entity.Property(e => e.ZbYear)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("年份");

                entity.Property(e => e.Zbrenyuan).HasComment("值班人员");

                entity.Property(e => e.Zyrenyuan).HasComment("住夜人员");
            });

            modelBuilder.Entity<ParkingLot>(entity =>
            {
                entity.HasKey(e => e.ParkingLotUuid)
                    .HasName("PK__ParkingL__BC638E2B164A277A");

                entity.HasComment("停车场信息表");

                entity.Property(e => e.ParkingLotUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.ParkingIndexCode).HasMaxLength(255);

                entity.Property(e => e.ParkingLotAddress)
                    .HasMaxLength(255)
                    .HasComment("位置");

                entity.Property(e => e.ParkingLotName)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.ParkingLotsru)
                    .HasMaxLength(255)
                    .HasComment("收入");

                entity.Property(e => e.Schewei)
                    .HasMaxLength(255)
                    .HasComment("剩余车位	");

                entity.Property(e => e.Ychewei)
                    .HasMaxLength(255)
                    .HasComment("已用车位	");

                entity.Property(e => e.Zchewei)
                    .HasMaxLength(255)
                    .HasComment("总车位");
            });

            modelBuilder.Entity<ParticularPerson>(entity =>
            {
                entity.HasKey(e => e.ParticularPersonUuid)
                    .HasName("PK__Particul__A56BB5EF0ECC1F22")
                    .IsClustered(false);

                entity.HasComment("特定人员信息表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_ParticularPerson")
                    .IsClustered();

                entity.Property(e => e.ParticularPersonUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注程度");

                entity.Property(e => e.AttentionCause)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注原因");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"血型

");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系电话

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"出生日期

");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"文化程度

");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"电子邮件

");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"工作单位

");

                entity.Property(e => e.FamilyPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("家庭成员联系电话");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"曾用名

");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"身高

");

                entity.Property(e => e.HouseholdRegistrationPoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.JoinTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("加入时间");

                entity.Property(e => e.LatestServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"最新服务时间

");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"婚姻状况

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"民族

");

                entity.Property(e => e.NoRoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"无房原因

");

                entity.Property(e => e.NumberOfTheHouse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"房屋编号

");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"职业

");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"其他地址

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"政治面貌

");

                entity.Property(e => e.Privy)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关系人");

                entity.Property(e => e.PrivyPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关系人电话");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地详址

");

                entity.Property(e => e.ReligiousBelief)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"宗教信仰

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地址

");

                entity.Property(e => e.ServiceMembers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有无服务成员

");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("类别");
            });

            modelBuilder.Entity<PerformanceDeclare>(entity =>
            {
                entity.HasKey(e => e.PerformanceDeclareUuid)
                    .HasName("PK__Performa__31FF73619C8D57C9");

                entity.HasComment("绩效-科室上报");

                entity.Property(e => e.PerformanceDeclareUuid)
                    .HasColumnName("PerformanceDeclareUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accessory)
                    .HasMaxLength(255)
                    .HasComment("附件");

                entity.Property(e => e.AuditOpinion)
                    .HasMaxLength(255)
                    .HasComment("审核意见");

                entity.Property(e => e.AuditStatus)
                    .HasMaxLength(255)
                    .HasComment("审核状态（1.待审核，2.审核通过，3.审核不通过）");

                entity.Property(e => e.Describe)
                    .HasMaxLength(255)
                    .HasComment("申报描述");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("申报人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("申报时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasComment("申报类型");
            });

            modelBuilder.Entity<PersonalDiary>(entity =>
            {
                entity.HasKey(e => e.PersonalDiaryUuid)
                    .HasName("PK__Personal__C5D18108BC6186FA");

                entity.HasComment("个人日志");

                entity.Property(e => e.PersonalDiaryUuid)
                    .HasColumnName("PersonalDiaryUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accessory)
                    .HasMaxLength(255)
                    .HasComment("附件");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasComment("内容");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("创建人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("创建时间");

                entity.Property(e => e.Headline)
                    .HasMaxLength(255)
                    .HasComment("标题");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.MissionUuid)
                    .HasColumnName("MissionUUID")
                    .HasComment("任务uuid");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("类型");

                entity.Property(e => e.WriteTime)
                    .HasMaxLength(255)
                    .HasComment("撰写时间");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.HasKey(e => e.PriorityUuid)
                    .HasName("PK__Priority__BB625C1F03B65A84");

                entity.HasComment("重点工作");

                entity.Property(e => e.PriorityUuid)
                    .HasColumnName("PriorityUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accomplish)
                    .HasMaxLength(255)
                    .HasComment("是否完成（0.未完成，1.完成，2逾期）");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(255)
                    .HasComment("重点工作结束时间");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("创建人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("创建时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Participant).HasComment("参与人");

                entity.Property(e => e.Principal).HasComment("负责人");

                entity.Property(e => e.PriorityDescribe).HasComment("重点工作描述");

                entity.Property(e => e.PriorityHeadline)
                    .HasMaxLength(255)
                    .HasComment("重点工作标题");

                entity.Property(e => e.Recycle)
                    .HasMaxLength(255)
                    .HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .HasComment("备注");

                entity.Property(e => e.Sortord).HasComment("排序字段");

                entity.Property(e => e.ZhiDingTime).HasMaxLength(255);
            });

            modelBuilder.Entity<PriorityJournal>(entity =>
            {
                entity.HasKey(e => e.PriorityJournalUuid)
                    .HasName("PK__MissionJ__25CCA6EE3FF03915_copy1");

                entity.HasComment("工作完成情况");

                entity.Property(e => e.PriorityJournalUuid)
                    .HasColumnName("PriorityJournalUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accessory)
                    .HasMaxLength(255)
                    .HasComment("附件");

                entity.Property(e => e.Completed)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("已完成");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasComment("内容");

                entity.Property(e => e.Coordination)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("需要协调");

                entity.Property(e => e.EstablishName)
                    .HasMaxLength(255)
                    .HasComment("创建人");

                entity.Property(e => e.EstablishTime)
                    .HasMaxLength(255)
                    .HasComment("创建时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("是否删除（0.未删，1.已删）");

                entity.Property(e => e.PriorityUuid)
                    .HasColumnName("PriorityUUID")
                    .HasComment("重点工作uuid");

                entity.Property(e => e.Read)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("已读人员id");
            });

            modelBuilder.Entity<Promo>(entity =>
            {
                entity.HasKey(e => e.PromoUuid);

                entity.HasComment("党建宣传活动表");

                entity.Property(e => e.PromoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Content).HasComment("内容");

                entity.Property(e => e.Cover).HasComment("封面");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否删除");

                entity.Property(e => e.IsRelease)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否发布");

                entity.Property(e => e.Photo).HasComment("3张图片");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("datetime")
                    .HasComment("活动时间");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("标题");
            });

            modelBuilder.Entity<PromoTeam>(entity =>
            {
                entity.HasKey(e => e.PromoTeamUuid)
                    .HasName("PK_nvarchar(MAX)");

                entity.HasComment("宣传小队表");

                entity.Property(e => e.PromoTeamUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("生日");

                entity.Property(e => e.Cunz).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Politics)
                    .HasMaxLength(255)
                    .HasComment("政治面貌");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.TeamCaptain)
                    .HasMaxLength(255)
                    .HasComment("队长");

                entity.Property(e => e.TeamChengyuan).HasComment("小队成员");

                entity.Property(e => e.TeamJieshao).HasComment("介绍");

                entity.Property(e => e.TeamType)
                    .HasMaxLength(255)
                    .HasComment("小队类型");
            });

            modelBuilder.Entity<Propaganda>(entity =>
            {
                entity.HasKey(e => e.PropagandaTypeUuid);

                entity.HasComment("宣传队伍类型表");

                entity.Property(e => e.PropagandaTypeUuid).ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0.未删 1.已删");

                entity.Property(e => e.Picture).HasComment("图片");

                entity.Property(e => e.State).HasComment("0.保存中 1.已发布");

                entity.Property(e => e.SumCount)
                    .HasMaxLength(255)
                    .HasComment("数量");
            });

            modelBuilder.Entity<PublicityFronts>(entity =>
            {
                entity.HasKey(e => e.PublicityFrontsUuid);

                entity.HasComment("党建宣传阵地表");

                entity.Property(e => e.PublicityFrontsUuid).ValueGeneratedNever();

                entity.Property(e => e.Address).HasComment("地址");

                entity.Property(e => e.AdminPeoPle)
                    .HasMaxLength(255)
                    .HasComment("管理人");

                entity.Property(e => e.Cover)
                    .IsUnicode(false)
                    .HasComment("封面");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("发布时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Introduction).HasComment("内容");

                entity.Property(e => e.IsDelete).HasComment("0.未删 1.已删");

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.Picture).HasComment("图片");

                entity.Property(e => e.PublicityFrontsName)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.PublicityTypeUuid).HasComment("所属类型");

                entity.Property(e => e.State).HasComment("0.保存中 1.已发布");

                entity.Property(e => e.Title).HasComment("简介");

                entity.Property(e => e.VillageUuid).HasComment("所属村庄");
            });

            modelBuilder.Entity<PublicityType>(entity =>
            {
                entity.HasKey(e => e.PublicityTypeUuid);

                entity.HasComment("党建宣传阵地类型表");

                entity.Property(e => e.PublicityTypeUuid).ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0.未删 1.已删");

                entity.Property(e => e.Picture).HasComment("图片");

                entity.Property(e => e.PublicityTypeName).HasComment("类型名称");

                entity.Property(e => e.State).HasComment("0.保存中 1.已发布");

                entity.Property(e => e.SumCount)
                    .HasMaxLength(255)
                    .HasComment("数量");

                entity.Property(e => e.Title).HasComment("介绍");
            });

            modelBuilder.Entity<PyramidSale>(entity =>
            {
                entity.HasKey(e => e.PyramidSaleUuid)
                    .HasName("PK__PyramidS__9E797C7BAA139794")
                    .IsClustered(false);

                entity.HasComment("传销人员信息表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_PyramidSale")
                    .IsClustered();

                entity.Property(e => e.PyramidSaleUuid).ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("关注程度");

                entity.Property(e => e.BindingHours)
                    .HasColumnName("bindingHours")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("限制时间");

                entity.Property(e => e.BindingHoursEnd)
                    .HasColumnName("bindingHoursEnd")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"血型

");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系电话

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"出生日期

");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"文化程度

");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"电子邮件

");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"工作单位

");

                entity.Property(e => e.FamilyPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("家庭成员联系电话");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"曾用名

");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"身高

");

                entity.Property(e => e.HouseholdRegistrationPoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.LatestServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"最新服务时间

");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"婚姻状况

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"民族

");

                entity.Property(e => e.NoRoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"无房原因

");

                entity.Property(e => e.NumberOfTheHouse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"房屋编号

");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"职业

");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"其他地址

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PenaltyType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("处罚类别");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"政治面貌

");

                entity.Property(e => e.PyramidSaleClass)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("传销等级");

                entity.Property(e => e.PyramidSaleType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("传销类别");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地详址

");

                entity.Property(e => e.ReligiousBelief)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"宗教信仰

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地址

");

                entity.Property(e => e.ServiceMembers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有无服务成员

");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.UnlawfulAct)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("违法行为");
            });

            modelBuilder.Entity<Qiye>(entity =>
            {
                entity.HasKey(e => e.QiyeUuid);

                entity.HasComment("企业信息表");

                entity.Property(e => e.QiyeUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Faren)
                    .HasMaxLength(255)
                    .HasComment("法人");

                entity.Property(e => e.Guimo)
                    .HasMaxLength(255)
                    .HasComment("规模");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系方式");

                entity.Property(e => e.QiyeAddress)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.QiyeName)
                    .HasMaxLength(255)
                    .HasComment("企业名字");

                entity.Property(e => e.QiyeType)
                    .HasMaxLength(255)
                    .HasComment("企业类型");
            });

            modelBuilder.Entity<RectifyInfo>(entity =>
            {
                entity.HasKey(e => e.RectifyInfoUuid);

                entity.HasComment("矫正人员信息表");

                entity.Property(e => e.RectifyInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("现住地址");

                entity.Property(e => e.Birthday)
                    .HasMaxLength(255)
                    .HasComment("出生日期");

                entity.Property(e => e.Cirterion)
                    .HasMaxLength(255)
                    .HasComment("原判刑期");

                entity.Property(e => e.Csrq)
                    .HasColumnName("csrq")
                    .HasMaxLength(255);

                entity.Property(e => e.Cym)
                    .HasColumnName("cym")
                    .HasMaxLength(255);

                entity.Property(e => e.Datas)
                    .HasColumnName("datas")
                    .HasMaxLength(255);

                entity.Property(e => e.DweiPhone)
                    .HasMaxLength(255)
                    .HasComment("定位手机	");

                entity.Property(e => e.Dzdwfs)
                    .HasColumnName("dzdwfs")
                    .HasMaxLength(255);

                entity.Property(e => e.Fjx)
                    .HasColumnName("fjx")
                    .HasMaxLength(255);

                entity.Property(e => e.Fzlx)
                    .HasColumnName("fzlx")
                    .HasMaxLength(255);

                entity.Property(e => e.Gatsfzhm)
                    .HasColumnName("gatsfzhm")
                    .HasMaxLength(255);

                entity.Property(e => e.Gatsfzlx)
                    .HasColumnName("gatsfzlx")
                    .HasMaxLength(255);

                entity.Property(e => e.Gattxzhm)
                    .HasColumnName("gattxzhm")
                    .HasMaxLength(255);

                entity.Property(e => e.Gattxzlx)
                    .HasColumnName("gattxzlx")
                    .HasMaxLength(255);

                entity.Property(e => e.Hycd)
                    .HasColumnName("hycd")
                    .HasMaxLength(255);

                entity.Property(e => e.Hzhm)
                    .HasColumnName("hzhm")
                    .HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证");

                entity.Property(e => e.Jcqk)
                    .HasColumnName("jcqk")
                    .HasMaxLength(255);

                entity.Property(e => e.JiesuTime)
                    .HasMaxLength(255)
                    .HasComment("矫正结束时间");

                entity.Property(e => e.Jtzm)
                    .HasColumnName("jtzm")
                    .HasMaxLength(255);

                entity.Property(e => e.Jzlb)
                    .HasColumnName("jzlb")
                    .HasMaxLength(255);

                entity.Property(e => e.Jzryxzzcqk)
                    .HasColumnName("jzryxzzcqk")
                    .HasMaxLength(255);

                entity.Property(e => e.KaishiTime)
                    .HasMaxLength(255)
                    .HasComment("矫正开始时间");

                entity.Property(e => e.Marriage)
                    .HasMaxLength(255)
                    .HasComment("婚姻状态");

                entity.Property(e => e.Mz)
                    .HasColumnName("mz")
                    .HasMaxLength(255);

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.RectifyInfoName)
                    .HasMaxLength(255)
                    .HasComment("姓名	");

                entity.Property(e => e.RectifyInfoStaues)
                    .HasMaxLength(255)
                    .HasComment("矫正状态	");

                entity.Property(e => e.RectifyInfoUnit)
                    .HasMaxLength(255)
                    .HasComment("矫正单位");

                entity.Property(e => e.RectifyTiem)
                    .HasMaxLength(255)
                    .HasComment("入矫时间	");

                entity.Property(e => e.RectifyType)
                    .HasMaxLength(255)
                    .HasComment("矫正类别	");

                entity.Property(e => e.Service)
                    .HasMaxLength(255)
                    .HasComment("有无服务成员");

                entity.Property(e => e.ServicingTime)
                    .HasMaxLength(255)
                    .HasComment("服务时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.Sfcydzdwgl)
                    .HasColumnName("sfcydzdwgl")
                    .HasMaxLength(255);

                entity.Property(e => e.Sflf)
                    .HasColumnName("sflf")
                    .HasMaxLength(255);

                entity.Property(e => e.Sftg)
                    .HasColumnName("sftg")
                    .HasMaxLength(255);

                entity.Property(e => e.Sfyqk)
                    .HasColumnName("sfyqk")
                    .HasMaxLength(255);

                entity.Property(e => e.Sfzh)
                    .HasColumnName("sfzh")
                    .HasMaxLength(255);

                entity.Property(e => e.ShangbanStaues)
                    .HasMaxLength(255)
                    .HasComment("司法部上报状态");

                entity.Property(e => e.Sqjzqx)
                    .HasColumnName("sqjzqx")
                    .HasMaxLength(255);

                entity.Property(e => e.Sqjzrybh)
                    .HasColumnName("sqjzrybh")
                    .HasMaxLength(255);

                entity.Property(e => e.Sqjzryjsfs)
                    .HasColumnName("sqjzryjsfs")
                    .HasMaxLength(255);

                entity.Property(e => e.Sqjzryjsrq)
                    .HasColumnName("sqjzryjsrq")
                    .HasMaxLength(255);

                entity.Property(e => e.Standard)
                    .HasMaxLength(255)
                    .HasComment("文化程度");

                entity.Property(e => e.Tbzhm)
                    .HasColumnName("tbzhm")
                    .HasMaxLength(100);

                entity.Property(e => e.Whcd)
                    .HasColumnName("whcd")
                    .HasMaxLength(255);

                entity.Property(e => e.Xb)
                    .HasColumnName("xb")
                    .HasMaxLength(255);

                entity.Property(e => e.Ywgattxz)
                    .HasColumnName("ywgattxz")
                    .HasMaxLength(255);

                entity.Property(e => e.Ywgztsfz)
                    .HasColumnName("ywgztsfz")
                    .HasMaxLength(255);

                entity.Property(e => e.Ywhz)
                    .HasColumnName("ywhz")
                    .HasMaxLength(255);

                entity.Property(e => e.Ywtbz)
                    .HasColumnName("ywtbz")
                    .HasMaxLength(255);

                entity.Property(e => e.Zyjwzxrystzk)
                    .HasColumnName("zyjwzxrystzk")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RegionInfo>(entity =>
            {
                entity.HasComment("区域基础表格");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegionX)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegionY)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegionInfos>(entity =>
            {
                entity.HasComment("区域整理后表");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasComment("区域id");

                entity.Property(e => e.RegionXyinfo)
                    .HasColumnName("RegionXYInfo")
                    .HasComment("坐标集合 例如：120.3654541,30.4414447|120.3654541,30.4414447");

                entity.Property(e => e.UnifiedAddressLibraryId)
                    .HasColumnName("UnifiedAddressLibraryID")
                    .HasComment("地址库ID集合");
            });

            modelBuilder.Entity<RegionInfosCopy>(entity =>
            {
                entity.ToTable("RegionInfos_copy");

                entity.HasComment("区域整理后表");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasComment("区域id");

                entity.Property(e => e.RegionXyinfo)
                    .HasColumnName("RegionXYInfo")
                    .HasComment("坐标集合 例如：120.3654541,30.4414447|120.3654541,30.4414447");

                entity.Property(e => e.UnifiedAddressLibraryId)
                    .HasColumnName("UnifiedAddressLibraryID")
                    .HasComment("地址库ID集合");
            });

            modelBuilder.Entity<Relationships>(entity =>
            {
                entity.HasKey(e => e.RelationshipsUuid)
                    .HasName("PK__Relation__A722E7D4D32AEAE0");

                entity.Property(e => e.RelationshipsUuid)
                    .HasColumnName("RelationshipsUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Addpeople)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Picture).IsUnicode(false);
            });

            modelBuilder.Entity<ReleasedPrison>(entity =>
            {
                entity.HasKey(e => e.ReleasedPrisonUuid)
                    .HasName("PK__Released__CEB5E8BD1EB7A859")
                    .IsClustered(false);

                entity.HasComment("刑满释放人员信息表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_ReleasedPrison")
                    .IsClustered();

                entity.Property(e => e.ReleasedPrisonUuid).ValueGeneratedNever();

                entity.Property(e => e.ArrangeCause)
                    .IsUnicode(false)
                    .HasComment("未安置原因");

                entity.Property(e => e.ArrangeTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("安置日期");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"血型

");

                entity.Property(e => e.Charge)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("罪名");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系电话

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.ControlClass)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("管控等级");

                entity.Property(e => e.CriminalAct)
                    .IsUnicode(false)
                    .HasComment("犯罪行为");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"出生日期

");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"文化程度

");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"电子邮件

");

                entity.Property(e => e.Employer)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"工作单位

");

                entity.Property(e => e.FamilyPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("家庭成员联系电话");

                entity.Property(e => e.Felony)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("是否重犯");

                entity.Property(e => e.FormerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"曾用名

");

                entity.Property(e => e.Height)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"身高

");

                entity.Property(e => e.HouseholdRegistrationPoliceStation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("户籍派出所");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.KeytoControl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("是否重点管控");

                entity.Property(e => e.LatestServiceHours)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"最新服务时间

");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"婚姻状况

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"民族

");

                entity.Property(e => e.NoRoomReason)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"无房原因

");

                entity.Property(e => e.NumberOfTheHouse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"房屋编号

");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"职业

");

                entity.Property(e => e.OtherAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"其他地址

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PeopleType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("人员类型");

                entity.Property(e => e.PoliticalStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"政治面貌

");

                entity.Property(e => e.Professional)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("原职业");

                entity.Property(e => e.RegisteredAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地详址

");

                entity.Property(e => e.ReligiousBelief)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"宗教信仰

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ResidenceAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"户籍地址

");

                entity.Property(e => e.Sentence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("原判刑期");

                entity.Property(e => e.ServiceMembers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有无服务成员

");

                entity.Property(e => e.ServingPlace)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("劳教场所");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.SolutionTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("解判刑期");

                entity.Property(e => e.YearFelony)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("本年度是否重犯");
            });

            modelBuilder.Entity<RentoutRoom>(entity =>
            {
                entity.HasKey(e => e.RentoutRoomUuid);

                entity.HasComment("出租房信息汇总");

                entity.Property(e => e.RentoutRoomUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.DaoqiTime)
                    .HasMaxLength(255)
                    .HasComment("到期时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RentoutInfo)
                    .HasMaxLength(255)
                    .HasComment("出租房信息");

                entity.Property(e => e.RentoutMoney)
                    .HasMaxLength(255)
                    .HasComment("价格	");

                entity.Property(e => e.RentoutStaues)
                    .HasMaxLength(255)
                    .HasComment("出租状态");

                entity.Property(e => e.RentoutTime)
                    .HasMaxLength(255)
                    .HasComment("租房时间");

                entity.Property(e => e.RentoutYezhu)
                    .HasMaxLength(255)
                    .HasComment("业主");

                entity.Property(e => e.RentoutZuhu)
                    .HasMaxLength(255)
                    .HasComment("租户");
            });

            modelBuilder.Entity<RenyuZhuany>(entity =>
            {
                entity.HasKey(e => e.RenyuZhuanyUuid);

                entity.HasComment("人员转移信息记录表");

                entity.Property(e => e.RenyuZhuanyUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Fuzeren)
                    .HasMaxLength(255)
                    .HasComment("负责人");

                entity.Property(e => e.FuzerenPhone)
                    .HasMaxLength(255)
                    .HasComment("负责人联系电话");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.XiangyingDj)
                    .HasMaxLength(255)
                    .HasComment("响应等级");

                entity.Property(e => e.Xzhuanyi)
                    .HasMaxLength(255)
                    .HasComment("需转移人员");

                entity.Property(e => e.ZhaunyiQingk)
                    .HasMaxLength(255)
                    .HasComment("转移情况");
            });

            modelBuilder.Entity<RescueMember>(entity =>
            {
                entity.HasKey(e => e.RescueMemberUuid)
                    .HasName("PK__RescueMe__22853D1CD3734E2B");

                entity.HasComment("救援小队成员表");

                entity.Property(e => e.RescueMemberUuid)
                    .HasColumnName("RescueMemberUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDelete).HasComment("0.未删 1.已删");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(10)
                    .HasComment("成员姓名");

                entity.Property(e => e.MemberPhone)
                    .HasMaxLength(20)
                    .HasComment("成员联系电话");

                entity.Property(e => e.MemberSex)
                    .HasMaxLength(2)
                    .HasComment("成员性别");

                entity.Property(e => e.RescueTeamUuid)
                    .HasColumnName("RescueTeamUUID")
                    .HasComment("小队UUID");

                entity.HasOne(d => d.RescueTeamUu)
                    .WithMany(p => p.RescueMember)
                    .HasForeignKey(d => d.RescueTeamUuid)
                    .HasConstraintName("FK__RescueMem__Rescu__43D61337");
            });

            modelBuilder.Entity<RescueTeam>(entity =>
            {
                entity.HasKey(e => e.RescueTeamUuid);

                entity.HasComment("救援小队信息");

                entity.Property(e => e.RescueTeamUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TeamCaptain)
                    .HasMaxLength(255)
                    .HasComment("队长");

                entity.Property(e => e.TeamName)
                    .HasMaxLength(255)
                    .HasComment("队名");

                entity.Property(e => e.TeamPhone)
                    .HasMaxLength(255)
                    .HasComment("联系电话	");

                entity.Property(e => e.TeamRenshu)
                    .HasMaxLength(255)
                    .HasComment("小队人数	");
            });

            modelBuilder.Entity<ResponseInfo>(entity =>
            {
                entity.HasKey(e => e.ResponseInfoUuid);

                entity.HasComment("响应情况表");

                entity.Property(e => e.ResponseInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系电话	");

                entity.Property(e => e.ResponseInitUuid)
                    .HasColumnName("ResponseInitUUID")
                    .HasComment("响应发起uuid");

                entity.Property(e => e.State).HasComment("0.未响应 1.已响应");

                entity.Property(e => e.TongzhiQingk)
                    .HasColumnName("TongzhiQIngk")
                    .HasMaxLength(255)
                    .HasComment("通知情况");

                entity.Property(e => e.TongzhiRen)
                    .HasMaxLength(255)
                    .HasComment("通知人员");

                entity.Property(e => e.Village)
                    .HasMaxLength(255)
                    .HasComment("村庄	");

                entity.Property(e => e.VillageMemberUuid)
                    .HasColumnName("VillageMemberUUID")
                    .HasComment("村庄成员uuid");

                entity.Property(e => e.XiangyingDj)
                    .HasMaxLength(255)
                    .HasComment("响应等级	");

                entity.Property(e => e.ZaiciTongzhi)
                    .HasMaxLength(255)
                    .HasComment("再次通知");

                entity.HasOne(d => d.ResponseInitUu)
                    .WithMany(p => p.ResponseInfo)
                    .HasForeignKey(d => d.ResponseInitUuid)
                    .HasConstraintName("FK__ResponseI__Respo__55009F39");

                entity.HasOne(d => d.VillageMemberUu)
                    .WithMany(p => p.ResponseInfo)
                    .HasForeignKey(d => d.VillageMemberUuid)
                    .HasConstraintName("FK__ResponseI__Villa__540C7B00");
            });

            modelBuilder.Entity<ResponseInit>(entity =>
            {
                entity.HasKey(e => e.ResponseInitUuid)
                    .HasName("PK__Response__4E48619536F12263");

                entity.HasComment("响应发起记录表");

                entity.Property(e => e.ResponseInitUuid)
                    .HasColumnName("ResponseInitUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Addpeoople)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDelete).HasComment("0.未删1.已删");

                entity.Property(e => e.Level)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("等级");

                entity.Property(e => e.ReleaseState).HasComment("0.保存中1.已通知");

                entity.Property(e => e.Situation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("情况");

                entity.Property(e => e.Villages).HasComment("通知村庄集合");
            });

            modelBuilder.Entity<Runbusiness>(entity =>
            {
                entity.HasKey(e => e.StudentUuid);

                entity.HasComment("返乡创业");

                entity.Property(e => e.StudentUuid)
                    .HasColumnName("StudentUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("具体地址");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("");

                entity.Property(e => e.Condition)
                    .HasMaxLength(255)
                    .HasComment("备注");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .HasComment("行政村");

                entity.Property(e => e.HeadTime)
                    .HasMaxLength(255)
                    .HasComment("");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Policy)
                    .HasMaxLength(255)
                    .HasComment("联系电话");

                entity.Property(e => e.Project)
                    .HasMaxLength(255)
                    .HasComment("负责人");

                entity.Property(e => e.Sex)
                    .HasMaxLength(100)
                    .HasComment("");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(255)
                    .HasComment("单位名称");
            });

            modelBuilder.Entity<RuzhuInfo>(entity =>
            {
                entity.HasKey(e => e.RuzhuInfoUuid);

                entity.HasComment("入住信息统计表");

                entity.Property(e => e.RuzhuInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.HotelName).HasMaxLength(255);

                entity.Property(e => e.HotelUuid).HasComment("酒店名称UUID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LikaiTime)
                    .HasMaxLength(255)
                    .HasComment("离开时间");

                entity.Property(e => e.RuzhuDay)
                    .HasMaxLength(255)
                    .HasComment("入住天数");

                entity.Property(e => e.RuzhuMoney)
                    .HasMaxLength(255)
                    .HasComment("价格");

                entity.Property(e => e.RuzhuName)
                    .HasMaxLength(255)
                    .HasComment("入住登记姓名");

                entity.Property(e => e.RuzhuPeople)
                    .HasMaxLength(255)
                    .HasComment("入住人数");

                entity.Property(e => e.RuzhuRoom)
                    .HasMaxLength(255)
                    .HasComment("房间号");

                entity.Property(e => e.RuzhuTime)
                    .HasMaxLength(255)
                    .HasComment("入住时间");

                entity.HasOne(d => d.HotelUu)
                    .WithMany(p => p.RuzhuInfo)
                    .HasForeignKey(d => d.HotelUuid)
                    .HasConstraintName("FK_RuzhuInfo_Hotel");
            });

            modelBuilder.Entity<Scenic>(entity =>
            {
                entity.HasKey(e => e.ScenicUuid);

                entity.HasComment("景点信息表");

                entity.Property(e => e.ScenicUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.ScenicAddress)
                    .HasMaxLength(255)
                    .HasComment("景点地址");

                entity.Property(e => e.ScenicGrade)
                    .HasMaxLength(255)
                    .HasComment("景点等级");

                entity.Property(e => e.ScenicName)
                    .HasMaxLength(255)
                    .HasComment("景点名称");

                entity.Property(e => e.ScenicPeoPle)
                    .HasMaxLength(255)
                    .HasComment("实时人数");

                entity.Property(e => e.ScenicQuantity)
                    .HasMaxLength(255)
                    .HasComment("日游数量");

                entity.Property(e => e.ScenicRemarks).HasComment("景点介绍");

                entity.Property(e => e.ScenicTickets)
                    .HasMaxLength(255)
                    .HasComment("门票");

                entity.Property(e => e.ScenicType)
                    .HasMaxLength(255)
                    .HasComment("状态");
            });

            modelBuilder.Entity<Sectarian>(entity =>
            {
                entity.HasKey(e => e.SectarianUuid);

                entity.HasComment("宗教信息表");

                entity.Property(e => e.SectarianUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SectarianLocation).HasComment("详细地址");

                entity.Property(e => e.SectarianName).HasComment("场所名称");

                entity.Property(e => e.SectarianRecommend).HasComment("场所介绍");

                entity.Property(e => e.SectarianTime).HasComment("建立时间");

                entity.Property(e => e.SectarianType)
                    .HasMaxLength(255)
                    .HasComment("所属宗派");
            });

            modelBuilder.Entity<Security>(entity =>
            {
                entity.HasKey(e => e.SecurityUuid);

                entity.HasComment("安防人员管理");

                entity.Property(e => e.SecurityUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号	");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系电话	");

                entity.Property(e => e.SecurityAddress)
                    .HasMaxLength(255)
                    .HasComment("地点");

                entity.Property(e => e.SecurityName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.SecurityStaues)
                    .HasMaxLength(255)
                    .HasComment("状态");

                entity.Property(e => e.SecurityTime)
                    .HasMaxLength(255)
                    .HasComment("时间");
            });

            modelBuilder.Entity<SecurityCode>(entity =>
            {
                entity.HasKey(e => e.SecurityCodeUuid)
                    .HasName("PK__Security__88844FB0F052A98C");

                entity.HasComment("安全码");

                entity.Property(e => e.SecurityCodeUuid)
                    .HasColumnName("SecurityCodeUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("区域");

                entity.Property(e => e.ChargeName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("负责人");

                entity.Property(e => e.ChargePhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("负责人电话");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0.未删1.已删");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("安全码名单");

                entity.Property(e => e.ScannTime).HasComment("扫码时间");

                entity.Property(e => e.State).HasComment("0.未扫码 1.已扫码");
            });

            modelBuilder.Entity<SecurityReport>(entity =>
            {
                entity.HasKey(e => e.SecurityReportUuid)
                    .IsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("IX_SecurityReport")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.SecurityReportUuid)
                    .HasColumnName("SecurityReportUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.State).HasMaxLength(100);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.SecurityUu)
                    .WithMany(p => p.SecurityReport)
                    .HasForeignKey(d => d.SecurityUuid)
                    .HasConstraintName("FK_SecurityReport_Security");
            });

            modelBuilder.Entity<SettleInfo>(entity =>
            {
                entity.HasKey(e => e.SettleUuid)
                    .HasName("SettleInfo_PK");

                entity.HasComment("落户人数分析");

                entity.Property(e => e.SettleUuid)
                    .HasColumnName("SettleUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Birthdate)
                    .HasMaxLength(255)
                    .HasComment("出生日期");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.NativePlace)
                    .HasMaxLength(255)
                    .HasComment("籍贯");

                entity.Property(e => e.PlaceAbode)
                    .HasMaxLength(255)
                    .HasComment("居住地");

                entity.Property(e => e.QianYiDe)
                    .HasMaxLength(255)
                    .HasComment("迁移地");

                entity.Property(e => e.SettleTime)
                    .HasMaxLength(255)
                    .HasComment("落户时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");
            });

            modelBuilder.Entity<Sightseer>(entity =>
            {
                entity.HasKey(e => e.SightseerUuid);

                entity.HasComment("游客信息表");

                entity.Property(e => e.SightseerUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Age)
                    .HasMaxLength(255)
                    .HasComment("年龄");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号	");

                entity.Property(e => e.Laiyuandi)
                    .HasMaxLength(255)
                    .HasComment("来源地");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("电话号码	");

                entity.Property(e => e.ScenicUuid).HasComment("景点UUID	");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别	");

                entity.Property(e => e.Shengneiwai)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("是否省内人员");

                entity.Property(e => e.SightseerName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.Staues)
                    .HasMaxLength(255)
                    .HasComment("状态");
            });

            modelBuilder.Entity<SmokeGan>(entity =>
            {
                entity.HasKey(e => e.SmokeGanUuid);

                entity.HasComment("烟感信息记录表");

                entity.Property(e => e.SmokeGanUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.SmokeGanBaojin)
                    .HasMaxLength(255)
                    .HasComment("烟感报警	");

                entity.Property(e => e.SmokeGanShebei)
                    .HasMaxLength(255)
                    .HasComment("设备编号	");

                entity.Property(e => e.SmokeGanStaues)
                    .HasMaxLength(255)
                    .HasComment("是否开启	");
            });

            modelBuilder.Entity<SthOrganic>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.HasComment("团组织架构");

                entity.HasIndex(e => e.SthOrganicUuid)
                    .HasName("IX_SthOrganic")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Cadre)
                    .HasMaxLength(255)
                    .HasComment("干部");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.Number).HasComment("人数");

                entity.Property(e => e.Secretary)
                    .HasMaxLength(255)
                    .HasComment("书记");

                entity.Property(e => e.SthOrganicUuid)
                    .HasColumnName("SthOrganicUUID")
                    .HasComment("");
            });

            modelBuilder.Entity<SystemIcon>(entity =>
            {
                entity.HasKey(e => e.SystemIconUuid)
                    .HasName("PK__SystemIc__540CED9D42DF8109");

                entity.HasComment("图标");

                entity.Property(e => e.SystemIconUuid)
                    .HasColumnName("SystemIconUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Custom).HasMaxLength(60);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Size).HasMaxLength(20);
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.HasKey(e => e.SystemLogUuid)
                    .HasName("PK__SystemLo__65A475E79A921FFD");

                entity.HasComment("系统日志表");

                entity.Property(e => e.SystemLogUuid)
                    .HasColumnName("SystemLogUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("IP地址");

                entity.Property(e => e.IsDelete).HasComment("标记删除1，未删除2，已删除");

                entity.Property(e => e.OperationContent)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasComment("操作内容");

                entity.Property(e => e.OperationTime)
                    .HasColumnType("datetime")
                    .HasComment("操作时间");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("操作类型(增删改查)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作用户ID");

                entity.Property(e => e.UserIdtype)
                    .HasColumnName("UserIDType")
                    .HasComment("用户名和类型");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("操作用户用户名");
            });

            modelBuilder.Entity<SystemMenu>(entity =>
            {
                entity.HasKey(e => e.SystemMenuUuid)
                    .HasName("PK__DncMenu__A2B5777CA1511602");

                entity.HasComment("菜单表");

                entity.Property(e => e.SystemMenuUuid)
                    .HasColumnName("SystemMenuUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.BeforeCloseFun).HasMaxLength(255);

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.CreatedByUserName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.ModifiedByUserName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParentName).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<SystemPermission>(entity =>
            {
                entity.HasKey(e => e.SystemPermissionUuid)
                    .HasName("PK__DncPermi__18DD8CCDCC3FD718");

                entity.HasComment("权限表");

                entity.Property(e => e.SystemPermissionUuid)
                    .HasColumnName("SystemPermissionUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionCode).HasMaxLength(255);

                entity.Property(e => e.CaPower).HasMaxLength(255);

                entity.Property(e => e.CreatedByUserName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.ModifiedByUserName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.SystemMenuUuid).HasColumnName("SystemMenuUUID");

                entity.HasOne(d => d.SystemMenuUu)
                    .WithMany(p => p.SystemPermission)
                    .HasForeignKey(d => d.SystemMenuUuid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_SystemPermission_SystemMenu");
            });

            modelBuilder.Entity<SystemRole>(entity =>
            {
                entity.HasKey(e => e.SystemRoleUuid)
                    .HasName("PK__DncRole__DF75FB283AD1E2C6");

                entity.HasComment("角色表");

                entity.HasIndex(e => e.RoleName)
                    .HasName("UQ_roleName")
                    .IsUnique();

                entity.Property(e => e.SystemRoleUuid)
                    .HasColumnName("SystemRoleUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsFixation)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否内置  0否,1是");

                entity.Property(e => e.RoleName).HasMaxLength(255);
            });

            modelBuilder.Entity<SystemRolePermissionMapping>(entity =>
            {
                entity.HasKey(e => new { e.SystemRoleUuid, e.SystemPermissionUuid })
                    .HasName("PK__DncRoleP__1EF823E41817FDB5");

                entity.HasComment("角色权限关系");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");

                entity.Property(e => e.SystemPermissionUuid).HasColumnName("SystemPermissionUUID");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasKey(e => e.SystemSettingUuid)
                    .IsClustered(false);

                entity.HasComment("系统设置");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_SystemSetting")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.SystemSettingUuid)
                    .HasColumnName("SystemSettingUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.WaterLevelCriticalPoint)
                    .HasColumnType("decimal(6, 2)")
                    .HasComment("水位临界点");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.HasKey(e => e.SystemUserUuid)
                    .HasName("PK__DncUser__A2B5777C0780DFF0")
                    .IsClustered(false);

                entity.HasComment("系统用户");

                entity.Property(e => e.SystemUserUuid)
                    .HasColumnName("SystemUserUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.ArmyTime)
                    .HasMaxLength(255)
                    .HasComment("参军年限");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("生日");

                entity.Property(e => e.DepartmentUuid)
                    .HasColumnName("DepartmentUUID")
                    .HasComment("相关部门");

                entity.Property(e => e.Dinguserid).HasMaxLength(255);

                entity.Property(e => e.Domicile)
                    .HasMaxLength(255)
                    .HasComment("户籍地");

                entity.Property(e => e.DyStaues)
                    .HasMaxLength(255)
                    .HasComment("是否党员");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .HasComment("学历");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasComment("邮箱");

                entity.Property(e => e.Evaluate)
                    .HasMaxLength(255)
                    .HasComment("考评级别");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdcardMd5)
                    .HasColumnName("IDCardMD5")
                    .HasMaxLength(255)
                    .HasComment("身份证号MD5");

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号");

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.JoinArmy)
                    .HasColumnName("JoinArmy	")
                    .HasMaxLength(255)
                    .HasComment("参军意愿");

                entity.Property(e => e.LoginName)
                    .HasMaxLength(255)
                    .HasComment("登录名");

                entity.Property(e => e.ManageDepartment).HasColumnType("text");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .HasComment("职业");

                entity.Property(e => e.OldCard).HasMaxLength(255);

                entity.Property(e => e.OrganizationUuid)
                    .HasMaxLength(255)
                    .HasComment("所属组织UUID");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(255)
                    .HasComment("密码");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("电话");

                entity.Property(e => e.Politics)
                    .HasMaxLength(255)
                    .HasComment("政治面貌");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(255)
                    .HasComment("职位");

                entity.Property(e => e.QianYiDe)
                    .HasMaxLength(255)
                    .HasComment("迁移地");

                entity.Property(e => e.QianYiTime)
                    .HasMaxLength(255)
                    .HasComment("迁移时间");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .HasComment("真实姓名");

                entity.Property(e => e.Residence)
                    .HasMaxLength(255)
                    .HasComment("居住地");

                entity.Property(e => e.Settled)
                    .HasMaxLength(255)
                    .HasComment("落户地");

                entity.Property(e => e.SettledTime)
                    .HasMaxLength(255)
                    .HasComment("落户时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.SystemRoleUuid)
                    .HasColumnName("SystemRoleUUid")
                    .HasMaxLength(255)
                    .HasComment("角色id,用逗号分隔");

                entity.Property(e => e.TownUuid)
                    .HasMaxLength(255)
                    .HasComment("所属村镇UUID");

                entity.Property(e => e.UserIdCard)
                    .HasMaxLength(255)
                    .HasComment("身份证");

                entity.Property(e => e.UserType).HasComment("1 超管 2其他");

                entity.Property(e => e.Wechat)
                    .HasMaxLength(255)
                    .HasComment("微信");

                entity.HasOne(d => d.DepartmentUu)
                    .WithMany(p => p.SystemUser)
                    .HasForeignKey(d => d.DepartmentUuid)
                    .HasConstraintName("Department_FK");
            });

            modelBuilder.Entity<SystemUserRoleMapping>(entity =>
            {
                entity.HasKey(e => new { e.SystemUserUuid, e.SystemRoleUuid })
                    .HasName("PK__DncUserR__328A96E56EE320C2");

                entity.HasComment("用户角色关系");

                entity.Property(e => e.SystemUserUuid).HasColumnName("SystemUserUUID");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TaskDecompInfo>(entity =>
            {
                entity.HasKey(e => e.TaskDecompInfoUuid)
                    .IsClustered(false);

                entity.HasComment("任务分解详情");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_TaskDecompInfo")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.TaskDecompInfoUuid)
                    .HasColumnName("TaskDecompInfoUUID")
                    .HasComment("详情uuid")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.DedicatedTeamLeader)
                    .HasMaxLength(100)
                    .HasComment("专组长班");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Phone).HasMaxLength(500);

                entity.Property(e => e.Principal)
                    .HasMaxLength(500)
                    .HasComment("村,企业负责人员");

                entity.Property(e => e.RelatedCadres)
                    .HasMaxLength(200)
                    .HasComment("相关干部");

                entity.Property(e => e.Requirement).HasComment("工作要求");

                entity.Property(e => e.SpecialWorkTeam)
                    .HasMaxLength(200)
                    .HasComment("工作专班");

                entity.Property(e => e.Tduuid)
                    .HasColumnName("TDUUID")
                    .HasComment("主表uuid");
            });

            modelBuilder.Entity<TaskDecomposition>(entity =>
            {
                entity.HasKey(e => e.TaskDecompositionUuid)
                    .IsClustered(false);

                entity.HasComment("任务分解主表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_TaskDecomposition")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.TaskDecompositionUuid)
                    .HasColumnName("TaskDecompositionUUID")
                    .HasComment("uuid")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Commander)
                    .HasMaxLength(250)
                    .HasComment("总指挥");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Member)
                    .HasMaxLength(500)
                    .HasComment("成员");
            });

            modelBuilder.Entity<TcqInfo>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__TcqInfo__A2B5777C604ECA6E");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.AnalogValueTypeId)
                    .HasColumnName("analogValueTypeId")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BasalArea)
                    .HasColumnName("basalArea")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingId)
                    .HasColumnName("buildingId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNo)
                    .HasColumnName("channelNo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CommunicationId)
                    .HasColumnName("communicationId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetectorAddr)
                    .HasColumnName("detectorAddr")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetectorCode)
                    .HasColumnName("detectorCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetectorName)
                    .HasColumnName("detectorName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetectorRunBigStatus)
                    .HasColumnName("detectorRunBigStatus")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetectorRunSmallStatus)
                    .HasColumnName("detectorRunSmallStatus")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DetectorType)
                    .HasColumnName("detectorType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceRange)
                    .HasColumnName("deviceRange")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirePartition)
                    .HasColumnName("firePartition")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GpsX3d)
                    .HasColumnName("gpsX3d")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GpsY3d)
                    .HasColumnName("gpsY3d")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GpsZ3d)
                    .HasColumnName("gpsZ3d")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HardwareVersion)
                    .HasColumnName("hardwareVersion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HostSim)
                    .HasColumnName("hostSim")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IfcsSystemType)
                    .HasColumnName("ifcsSystemType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainframeId)
                    .HasColumnName("mainframeId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MapType)
                    .HasColumnName("mapType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MaxHeight)
                    .HasColumnName("maxHeight")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MaxalarmValue)
                    .HasColumnName("maxalarmValue")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MinalarmValue)
                    .HasColumnName("minalarmValue")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrgId)
                    .HasColumnName("orgId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PartunitloopCode)
                    .HasColumnName("partunitloopCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId)
                    .HasColumnName("personId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlanId)
                    .HasColumnName("planId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlanX)
                    .HasColumnName("planX")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlanY)
                    .HasColumnName("planY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlatformCode)
                    .HasColumnName("platformCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProduceDate)
                    .HasColumnName("produceDate")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProducerId)
                    .HasColumnName("producerId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProtocolType)
                    .HasColumnName("protocolType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RecordCode)
                    .HasColumnName("recordCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterStatus)
                    .HasColumnName("registerStatus")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterTime)
                    .HasColumnName("registerTime")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RunDate)
                    .HasColumnName("runDate")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceLimit)
                    .HasColumnName("serviceLimit")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwareVersion)
                    .HasColumnName("softwareVersion")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SystemAddress)
                    .HasColumnName("systemAddress")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TcqType>(entity =>
            {
                entity.HasKey(e => e.Tcqguid)
                    .HasName("PK__TcqType__01866AF4F912FFAA");

                entity.Property(e => e.Tcqguid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TcqName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TheUnion>(entity =>
            {
                entity.HasKey(e => e.TheUnionUuid);

                entity.HasComment("工会活动");

                entity.Property(e => e.TheUnionUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Content).HasComment("内容");

                entity.Property(e => e.Cover).HasComment("封面");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否删除");

                entity.Property(e => e.IsRelease)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否发布");

                entity.Property(e => e.Photo).HasComment("3张图片");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("datetime")
                    .HasComment("活动时间");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("标题");

                entity.Property(e => e.VillageUuid)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");
            });

            modelBuilder.Entity<TheUnionZhengc>(entity =>
            {
                entity.HasKey(e => e.TheUnionZhengcUuid);

                entity.HasComment("工会普惠政策");

                entity.Property(e => e.TheUnionZhengcUuid).ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0.未删 1.已删");

                entity.Property(e => e.Neirong).HasComment("政策内容");

                entity.Property(e => e.Picture).HasComment("图片");

                entity.Property(e => e.State).HasComment("0.保存中 1.已发布");

                entity.Property(e => e.TheUnionZhengcName).HasComment("政策名");
            });

            modelBuilder.Entity<Tongji>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Tongji");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.TjJidu).HasMaxLength(255);

                entity.Property(e => e.TjYear).HasMaxLength(255);
            });

            modelBuilder.Entity<Tourist>(entity =>
            {
                entity.HasKey(e => e.TouristUuid);

                entity.HasComment("旅游收入信息");

                entity.Property(e => e.TouristUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ScenicName)
                    .HasMaxLength(255)
                    .HasComment("景点名称");

                entity.Property(e => e.ScenicUuid).HasComment("景点UUID	");

                entity.Property(e => e.TjJidu)
                    .HasMaxLength(255)
                    .HasComment("季度");

                entity.Property(e => e.TjMouth)
                    .HasMaxLength(255)
                    .HasComment("统计月份");

                entity.Property(e => e.TjYear)
                    .HasMaxLength(255)
                    .HasComment("统计年份");

                entity.Property(e => e.TouristMoney).HasComment("金额	");

                entity.Property(e => e.TouristName)
                    .HasMaxLength(255)
                    .HasComment("游客名称	");

                entity.Property(e => e.TouristTime)
                    .HasMaxLength(255)
                    .HasComment("时间");
            });

            modelBuilder.Entity<TouristOfDay>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TouristOfDay");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TouristOfMonth>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TouristOfMonth");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TouristOfWeek>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TouristOfWeek");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TouristOfYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TouristOfYear");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.TownUuid);

                entity.HasComment("河桥镇村信息表");

                entity.Property(e => e.TownUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .HasComment("企业数量");

                entity.Property(e => e.Geographical)
                    .HasMaxLength(255)
                    .HasComment("地域面积");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.PartyMember)
                    .HasMaxLength(255)
                    .HasComment("党员人数");

                entity.Property(e => e.SjTownUuid).HasComment("上级村镇UUID");

                entity.Property(e => e.TownGrade)
                    .HasMaxLength(255)
                    .HasComment("村镇等级");

                entity.Property(e => e.TownName).HasComment("村镇名称");

                entity.Property(e => e.TownPeople)
                    .HasMaxLength(255)
                    .HasComment("人口");
            });

            modelBuilder.Entity<TownMilitia>(entity =>
            {
                entity.HasKey(e => e.TownMilitiaUuid);

                entity.HasComment("村镇民兵信息表");

                entity.Property(e => e.TownMilitiaUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.ArmyTime)
                    .HasMaxLength(255)
                    .HasComment("军年限	");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("出生日期");

                entity.Property(e => e.Census)
                    .HasMaxLength(255)
                    .HasComment("户籍地");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号	");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .HasComment("职业");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("手机号");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.TownMilitiaName)
                    .HasMaxLength(255)
                    .HasComment("人员姓名");
            });

            modelBuilder.Entity<Uav>(entity =>
            {
                entity.HasKey(e => e.Uavuuid)
                    .HasName("PK__UAV__C3F584F524D026FD");

                entity.ToTable("UAV");

                entity.HasComment("无人机");

                entity.Property(e => e.Uavuuid)
                    .HasColumnName("UAVUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Uavaddress)
                    .HasColumnName("UAVAddress")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("无人机监控地址");

                entity.Property(e => e.Uavname)
                    .HasColumnName("UAVName")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("无人机名称");

                entity.Property(e => e.Uavnumber)
                    .HasColumnName("UAVNumber")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("无人机编号");

                entity.Property(e => e.Uavurl)
                    .HasColumnName("UAVUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("链接地址");
            });

            modelBuilder.Entity<UnifiedAddressLibrary>(entity =>
            {
                entity.HasNoKey();

                entity.HasComment("统一地址库");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("添加时间");

                entity.Property(e => e.AddressType)
                    .HasColumnName("ADDRESS_TYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Addrtype)
                    .HasColumnName("ADDRTYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BelongBuilding)
                    .HasColumnName("BELONG_BUILDING")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Building)
                    .HasColumnName("BUILDING")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingCode)
                    .HasColumnName("BUILDING_CODE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingNum)
                    .HasColumnName("BUILDING_NUM")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingPath)
                    .HasColumnName("BUILDING_PATH")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("CITY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Community)
                    .HasColumnName("COMMUNITY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasColumnName("COUNTY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Createtime)
                    .HasColumnName("CREATETIME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Datasource)
                    .HasColumnName("DATASOURCE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Door)
                    .HasColumnName("DOOR")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Floor)
                    .HasColumnName("FLOOR")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FromStatus)
                    .HasColumnName("FROM_STATUS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GridCode)
                    .HasColumnName("GRID_CODE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HouseCode)
                    .HasColumnName("HOUSE_CODE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Inserttime)
                    .HasColumnName("INSERTTIME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");

                entity.Property(e => e.Isvalid).HasColumnName("ISVALID");

                entity.Property(e => e.Lat)
                    .HasColumnName("LAT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lon)
                    .HasColumnName("LON")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Oid)
                    .HasColumnName("oid")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasColumnName("REMARK")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Resregion)
                    .HasColumnName("RESREGION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reverse1)
                    .HasColumnName("REVERSE1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Reverse2)
                    .HasColumnName("REVERSE2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Room)
                    .HasColumnName("ROOM")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoomFloor)
                    .HasColumnName("ROOM_FLOOR")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Sourceaddress)
                    .HasColumnName("SOURCEADDRESS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Squad)
                    .HasColumnName("SQUAD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasColumnName("STREET")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Systemid)
                    .HasColumnName("SYSTEMID")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Szone)
                    .HasColumnName("SZONE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ToStatus)
                    .HasColumnName("TO_STATUS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Town)
                    .HasColumnName("TOWN")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UnifiedAddressLibraryUuid)
                    .HasColumnName("UnifiedAddressLibraryUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Updatetime)
                    .HasColumnName("UPDATETIME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Village)
                    .HasColumnName("VILLAGE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Z)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnifiedAddressLibraryUserInfo>(entity =>
            {
                entity.HasComment("楼栋和户籍信息匹配表");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UnifiedAddressLibraryId).HasColumnName("UnifiedAddressLibraryID");

                entity.Property(e => e.UserIdlist).HasColumnName("UserIDList");
            });

            modelBuilder.Entity<Userinfoty>(entity =>
            {
                entity.HasKey(e => new { e.UserInfoUuid, e.Id })
                    .HasName("PK__Userinfo__B992395488460E95");

                entity.HasComment("统一人员信息表");

                entity.Property(e => e.UserInfoUuid).HasColumnName("UserInfoUUID");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.Age).HasComment("年龄");

                entity.Property(e => e.ArmyTime)
                    .HasMaxLength(255)
                    .HasComment("参军年限");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("生日");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .HasComment("人员类别");

                entity.Property(e => e.Defense)
                    .HasMaxLength(255)
                    .HasComment("是否参军");

                entity.Property(e => e.DepartmentUuid)
                    .HasColumnName("DepartmentUUID")
                    .HasComment("相关部门");

                entity.Property(e => e.Domicile)
                    .HasMaxLength(255)
                    .HasComment("地址");

                entity.Property(e => e.DyStaues)
                    .HasMaxLength(255)
                    .HasComment("是否党员");

                entity.Property(e => e.Education)
                    .HasMaxLength(255)
                    .HasComment("学历");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasComment("邮箱");

                entity.Property(e => e.Evaluate)
                    .HasMaxLength(255)
                    .HasComment("考评级别");

                entity.Property(e => e.Household)
                    .HasMaxLength(255)
                    .HasComment("户别");

                entity.Property(e => e.HouseholderName)
                    .HasMaxLength(100)
                    .HasComment("户主姓名");

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号");

                entity.Property(e => e.JoinArmy)
                    .HasMaxLength(255)
                    .HasComment("参军意愿");

                entity.Property(e => e.JoinDate)
                    .HasMaxLength(255)
                    .HasComment("加入党时间");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .HasComment("职业");

                entity.Property(e => e.OrganizationUuid)
                    .HasMaxLength(255)
                    .HasComment("所属组织UUID");

                entity.Property(e => e.Partybranch)
                    .HasMaxLength(255)
                    .HasComment("所在党支部");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("电话号码");

                entity.Property(e => e.Politics)
                    .HasMaxLength(255)
                    .HasComment("政治面貌");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasMaxLength(255)
                    .HasComment("职位");

                entity.Property(e => e.QianYiEtime)
                    .HasColumnName("QianYiETime")
                    .HasMaxLength(255)
                    .HasComment("迁出时间");

                entity.Property(e => e.QianYiStime)
                    .HasColumnName("QianYiSTime")
                    .HasMaxLength(255)
                    .HasComment("迁入时间");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .HasComment("姓名");

                entity.Property(e => e.Relation)
                    .HasMaxLength(100)
                    .HasComment("与户主的关系");

                entity.Property(e => e.Residence)
                    .HasMaxLength(255)
                    .HasComment("户籍地");

                entity.Property(e => e.Settled)
                    .HasMaxLength(255)
                    .HasComment("落户地");

                entity.Property(e => e.SettledTime)
                    .HasMaxLength(255)
                    .HasComment("落户时间");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.TownUuid).HasComment("所属村镇UUID");

                entity.Property(e => e.UserIdCard).HasMaxLength(255);

                entity.Property(e => e.Wechat)
                    .HasMaxLength(255)
                    .HasComment("微信");

                entity.Property(e => e.Work)
                    .HasMaxLength(255)
                    .HasComment("工作单位");

                entity.HasOne(d => d.TownUu)
                    .WithMany(p => p.Userinfoty)
                    .HasForeignKey(d => d.TownUuid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Userinfot__TownU__1A69E950");
            });

            modelBuilder.Entity<ViewMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Menu");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.BeforeCloseFun).HasMaxLength(255);

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.CreatedByUserName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.ModifiedByUserName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParentName).HasMaxLength(255);

                entity.Property(e => e.Pd).HasColumnName("pd");

                entity.Property(e => e.Ps).HasColumnName("ps");

                entity.Property(e => e.Pt).HasColumnName("pt");

                entity.Property(e => e.SystemMenuUuid).HasColumnName("SystemMenuUUID");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<ViewSystemPermissionWithMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_SystemPermissionWithMenu");

                entity.Property(e => e.MenuAlias).HasMaxLength(255);

                entity.Property(e => e.MenuName).HasMaxLength(255);

                entity.Property(e => e.Pd).HasColumnName("pd");

                entity.Property(e => e.PermissionActionCode).HasMaxLength(255);

                entity.Property(e => e.PermissionName).HasMaxLength(255);

                entity.Property(e => e.Ps).HasColumnName("ps");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");
            });

            modelBuilder.Entity<ViewSystemPermissionWithMenu2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_SystemPermissionWithMenu2");

                entity.Property(e => e.MenuAlias).HasMaxLength(255);

                entity.Property(e => e.MenuName).HasMaxLength(255);

                entity.Property(e => e.Pd).HasColumnName("pd");

                entity.Property(e => e.PermissionActionCode).HasMaxLength(255);

                entity.Property(e => e.PermissionName).HasMaxLength(255);

                entity.Property(e => e.Ps).HasColumnName("ps");
            });

            modelBuilder.Entity<Village>(entity =>
            {
                entity.HasKey(e => e.VillageUuid)
                    .HasName("PK__Village__95B60B5F59FA30EB");

                entity.HasComment("村庄管理");

                entity.Property(e => e.VillageUuid)
                    .HasColumnName("VillageUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OrderBy).HasComment("排序");

                entity.Property(e => e.VillageName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("村庄名称");
            });

            modelBuilder.Entity<VillageMember>(entity =>
            {
                entity.HasKey(e => e.VillageMemberUuid)
                    .HasName("PK__VillageM__FCC26E0E0F3027D2");

                entity.HasComment("村干部");

                entity.Property(e => e.VillageMemberUuid)
                    .HasColumnName("VillageMemberUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDelete).HasComment("0.未删 1.已删");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(10)
                    .HasComment("成员姓名");

                entity.Property(e => e.MemberPhone)
                    .HasMaxLength(20)
                    .HasComment("成员联系电话");

                entity.Property(e => e.MemberSex)
                    .HasMaxLength(2)
                    .HasComment("成员性别");

                entity.Property(e => e.VillageUuid)
                    .HasColumnName("VillageUUID")
                    .HasComment("村庄uuid");

                entity.HasOne(d => d.VillageUu)
                    .WithMany(p => p.VillageMember)
                    .HasForeignKey(d => d.VillageUuid)
                    .HasConstraintName("FK__VillageMe__Villa__51300E55");
            });

            modelBuilder.Entity<VolunteerTeam>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.HasComment("志愿服务队伍");

                entity.HasIndex(e => e.VteamUuid)
                    .HasName("IX_VolunteerTeam")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.Number).HasComment("人数");

                entity.Property(e => e.VteamUuid).HasColumnName("VTeamUUID");
            });

            modelBuilder.Entity<Volunteers>(entity =>
            {
                entity.HasKey(e => e.VolunteersUuid);

                entity.HasComment("侨界志愿者信息表");

                entity.Property(e => e.VolunteersUuid).ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Jieshao).HasComment("个人简介");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("手机号");

                entity.Property(e => e.Staues).HasMaxLength(255);

                entity.Property(e => e.VolunteersName)
                    .HasMaxLength(255)
                    .HasComment("姓名");
            });

            modelBuilder.Entity<WaterLevel>(entity =>
            {
                entity.HasKey(e => e.WaterLevelUuid)
                    .IsClustered(false);

                entity.HasComment("五水共治信息表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_WaterLevel")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.WaterLevelUuid).ValueGeneratedNever();

                entity.Property(e => e.Accurate)
                    .HasMaxLength(255)
                    .HasComment("精准线");

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdminPepo)
                    .HasMaxLength(255)
                    .HasComment("管理人");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度	");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.MonitorWaterId)
                    .HasMaxLength(255)
                    .HasComment("监控编号");

                entity.Property(e => e.ShebeiType)
                    .HasMaxLength(255)
                    .HasComment("设备类型");

                entity.Property(e => e.WaterInfo)
                    .HasMaxLength(255)
                    .HasComment("基本信息	");

                entity.Property(e => e.WaterName)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.Watersw)
                    .HasMaxLength(255)
                    .HasComment("水位");

                entity.Property(e => e.Weizhi)
                    .HasMaxLength(255)
                    .HasComment("位置");
            });

            modelBuilder.Entity<WaterLevelSheb>(entity =>
            {
                entity.HasKey(e => e.WaterLevelShebUuid);

                entity.HasComment("五水共治监控设备信息表");

                entity.Property(e => e.WaterLevelShebUuid).ValueGeneratedNever();

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("增加时间");

                entity.Property(e => e.AdminPepo)
                    .HasMaxLength(255)
                    .HasComment("管理人信息");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.MonitorWaterId).HasMaxLength(255);

                entity.Property(e => e.ShebeiType)
                    .HasMaxLength(255)
                    .HasComment("设备类型");

                entity.Property(e => e.Weizhi)
                    .HasMaxLength(255)
                    .HasComment("设备ID");

                entity.Property(e => e.YujingNum)
                    .HasMaxLength(255)
                    .HasComment("报警数值");

                entity.Property(e => e.YujingType)
                    .HasMaxLength(255)
                    .HasComment("报警状态");
            });

            modelBuilder.Entity<WomenActivities>(entity =>
            {
                entity.HasKey(e => e.WomenActivitiesUuid);

                entity.HasComment("妇联活动记录表");

                entity.Property(e => e.WomenActivitiesUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Content).HasComment("内容");

                entity.Property(e => e.Cover).HasComment("封面");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否删除");

                entity.Property(e => e.IsRelease)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否发布");

                entity.Property(e => e.Photo).HasComment("3张图片");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("datetime")
                    .HasComment("活动时间");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("标题");

                entity.Property(e => e.VillageUuid)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");
            });

            modelBuilder.Entity<WomenFederation>(entity =>
            {
                entity.HasKey(e => e.WomenFederationUuid);

                entity.HasComment("妇联组织信息表");

                entity.Property(e => e.WomenFederationUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address).HasComment("所在地");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系电话");

                entity.Property(e => e.Renshu).HasComment("人数");

                entity.Property(e => e.Staues)
                    .HasMaxLength(255)
                    .HasComment("状态");

                entity.Property(e => e.VillageUuid)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");

                entity.Property(e => e.WomenFederationName)
                    .HasMaxLength(255)
                    .HasComment("组织名称");

                entity.Property(e => e.Zhuxi)
                    .HasMaxLength(255)
                    .HasComment("主席");
            });

            modelBuilder.Entity<WomenHouse>(entity =>
            {
                entity.HasKey(e => e.WomenHouseUuid);

                entity.HasComment("妇女之家信息表");

                entity.Property(e => e.WomenHouseUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address).HasComment("所在地");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("联系电话");

                entity.Property(e => e.Renshu).HasComment("人数");

                entity.Property(e => e.Staues)
                    .HasMaxLength(255)
                    .HasComment("状态");

                entity.Property(e => e.VillageUuid)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");

                entity.Property(e => e.WomenHouseName)
                    .HasMaxLength(255)
                    .HasComment("妇女之家名称");

                entity.Property(e => e.Zhuxi)
                    .HasMaxLength(255)
                    .HasComment("负责人");
            });

            modelBuilder.Entity<Woods>(entity =>
            {
                entity.HasKey(e => e.WoodsUuid);

                entity.HasComment("森林防火设备信息");

                entity.Property(e => e.WoodsUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdministratorName)
                    .HasMaxLength(255)
                    .HasComment("网格员名称");

                entity.Property(e => e.AdministratorPhone)
                    .HasMaxLength(255)
                    .HasComment("网格员电话");

                entity.Property(e => e.ForestAddress)
                    .HasMaxLength(255)
                    .HasComment("森林位置");

                entity.Property(e => e.ForestName)
                    .HasMaxLength(255)
                    .HasComment("森林名称");

                entity.Property(e => e.Guanliren)
                    .HasMaxLength(255)
                    .HasComment("管理人信息");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.ManagePhone)
                    .HasMaxLength(100)
                    .HasComment("管理人电话");

                entity.Property(e => e.MapRegion).HasComment("地图区域");

                entity.Property(e => e.ShebeiAddress)
                    .HasMaxLength(255)
                    .HasComment("所处位置	");

                entity.Property(e => e.ShebeiId)
                    .HasMaxLength(255)
                    .HasComment("设备ID");

                entity.Property(e => e.ShebeiStaues)
                    .HasMaxLength(255)
                    .HasComment("设备状态");

                entity.Property(e => e.ShebeiType)
                    .HasMaxLength(255)
                    .HasComment("设备类型	");
            });

            modelBuilder.Entity<XfBuilding>(entity =>
            {
                entity.HasKey(e => e.BuildingUuid)
                    .HasName("PK__xf_Build__CFD9714FD136FF31");

                entity.ToTable("xf_Building");

                entity.HasComment("消防建筑物信息");

                entity.Property(e => e.BuildingUuid).ValueGeneratedNever();

                entity.Property(e => e.BelongOrgId)
                    .HasColumnName("belongOrgId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingAddr)
                    .HasColumnName("buildingAddr")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingArea).HasColumnName("buildingArea");

                entity.Property(e => e.BuildingFinishTime)
                    .HasColumnName("buildingFinishTime")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingGalleryful).HasColumnName("buildingGalleryful");

                entity.Property(e => e.BuildingHeight).HasColumnName("buildingHeight");

                entity.Property(e => e.BuildingName)
                    .HasColumnName("buildingName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingState)
                    .HasColumnName("buildingState")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingTime)
                    .HasColumnName("buildingTime")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingType)
                    .HasColumnName("buildingType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BuildingUseNature)
                    .HasColumnName("buildingUseNature")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ControlRoomCondition).HasColumnName("controlRoomCondition");

                entity.Property(e => e.ControlRoomPosition)
                    .HasColumnName("controlRoomPosition")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnterOrgNum).HasColumnName("enterOrgNum");

                entity.Property(e => e.ExitForm)
                    .HasColumnName("exitForm")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExitNum).HasColumnName("exitNum");

                entity.Property(e => e.ExitPosition)
                    .HasColumnName("exitPosition")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FireDanger)
                    .HasColumnName("fireDanger")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FireElevatorCarrery).HasColumnName("fireElevatorCarrery");

                entity.Property(e => e.FireElevatorNum).HasColumnName("fireElevatorNum");

                entity.Property(e => e.FireElevatorPosition)
                    .HasColumnName("fireElevatorPosition")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FireResistantLevel)
                    .HasColumnName("fireResistantLevel")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FloorNum).HasColumnName("floorNum");

                entity.Property(e => e.GpsX3d).HasColumnName("gpsX3d");

                entity.Property(e => e.GpsY3d).HasColumnName("gpsY3d");

                entity.Property(e => e.GpsZ3d).HasColumnName("gpsZ3d");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsMezzanine).HasColumnName("isMezzanine");

                entity.Property(e => e.MainMaterial)
                    .HasColumnName("mainMaterial")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MainProduct)
                    .HasColumnName("mainProduct")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerOrgId)
                    .HasColumnName("managerOrgId")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MapType).HasColumnName("mapType");

                entity.Property(e => e.Memo)
                    .HasColumnName("memo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OccupyArea).HasColumnName("occupyArea");

                entity.Property(e => e.OrgAbutSituation)
                    .HasColumnName("orgAbutSituation")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OverFloorArea).HasColumnName("overFloorArea");

                entity.Property(e => e.OverFloorNum).HasColumnName("overFloorNum");

                entity.Property(e => e.PlatformCode)
                    .HasColumnName("platformCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyRight)
                    .HasColumnName("propertyRight")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RecordCode)
                    .HasColumnName("recordCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegionCode)
                    .HasColumnName("regionCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShelterFloorArea).HasColumnName("shelterFloorArea");

                entity.Property(e => e.ShelterFloorNum).HasColumnName("shelterFloorNum");

                entity.Property(e => e.ShelterFloorPosition)
                    .HasColumnName("shelterFloorPosition")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StandardFloorArea).HasColumnName("standardFloorArea");

                entity.Property(e => e.StorageName)
                    .HasColumnName("storageName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNature)
                    .HasColumnName("storageNature")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StorageNum).HasColumnName("storageNum");

                entity.Property(e => e.StorageShape)
                    .HasColumnName("storageShape")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StorageSize).HasColumnName("storageSize");

                entity.Property(e => e.StructureType)
                    .HasColumnName("structureType")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TunnelHeight).HasColumnName("tunnelHeight");

                entity.Property(e => e.TunnelLength).HasColumnName("tunnelLength");

                entity.Property(e => e.UnderFloorArea).HasColumnName("underFloorArea");

                entity.Property(e => e.UnderFloorNum).HasColumnName("underFloorNum");

                entity.Property(e => e.UnitNum).HasColumnName("unitNum");

                entity.Property(e => e.WorkerDailyNum).HasColumnName("workerDailyNum");
            });

            modelBuilder.Entity<XiaoQuAnFang>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__XiaoQuAn__A2B5777CF3C4CA5D");

                entity.HasComment("小区安防人脸信息表");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Ability)
                    .HasColumnName("ability")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件类别");

                entity.Property(e => e.BkgUrl)
                    .HasColumnName("bkgUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("抓拍图片的完整原图");

                entity.Property(e => e.CameraIndexCode)
                    .HasColumnName("cameraIndexCode")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("抓拍这张图片的监控点的唯一标识");

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channelID")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("抓拍这张图片的监控点的通道号");

                entity.Property(e => e.DataType)
                    .HasColumnName("dataType")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件类别，人脸比对的事件类别为faceMatch");

                entity.Property(e => e.DeviceIndexCode)
                    .HasColumnName("deviceIndexCode")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("抓拍这张图片的监控点所属的设备的唯一标识");

                entity.Property(e => e.EventId)
                    .HasColumnName("eventId")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件唯一标识");

                entity.Property(e => e.EventType)
                    .HasColumnName("eventType")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件类型");

                entity.Property(e => e.FaceTime)
                    .HasColumnName("faceTime")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("抓拍这张图片时的时间");

                entity.Property(e => e.HappenTime)
                    .HasColumnName("happenTime")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件发生时间设备时间");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ipAddress")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件来源的地址，人脸抓拍的事件来源为抓拍机的地址。");

                entity.Property(e => e.PicServerIndexCode)
                    .HasColumnName("picServerIndexCode")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("图片服务唯一标志");

                entity.Property(e => e.PortNo)
                    .HasColumnName("portNo")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件来源的端口");

                entity.Property(e => e.SendTime)
                    .HasColumnName("sendTime")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件从接收者（程序处理后）发出的时间	");

                entity.Property(e => e.SrcIndex)
                    .HasColumnName("srcIndex")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件源编号，物理设备是资源编号");

                entity.Property(e => e.SrcName)
                    .HasColumnName("srcName")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件源名称");

                entity.Property(e => e.SrcParentIndex)
                    .HasColumnName("srcParentIndex")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件发生的事件源父设备编号");

                entity.Property(e => e.SrcType)
                    .HasColumnName("srcType")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件源类型");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("事件状态(0-瞬时1-开始2-停止3-事件脉冲4-事件联动结果更新)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("过滤类型(0-全部类型1-人脸抓拍图片过滤2-黑名单库3-白名单库)");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("抓拍到的人脸图片的URL，可能位于设备或ASW服务上");
            });

            modelBuilder.Entity<XiaoQuAnFangDev>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__XiaoQuAn__A2B5777C897C46B0");

                entity.HasComment("小区安防抓拍设备信息");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.DevGuid)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("设备id");

                entity.Property(e => e.DevName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("设备地址");
            });

            modelBuilder.Entity<XlProject>(entity =>
            {
                entity.HasKey(e => e.XlProjectUuid)
                    .HasName("PK__XlProjec__DCA9EE852677227C");

                entity.HasComment("雪亮工程设备信息");

                entity.Property(e => e.XlProjectUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.AdminInfo)
                    .HasMaxLength(255)
                    .HasComment("管理人信息");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度	");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.ShebeiAddress)
                    .HasMaxLength(255)
                    .HasComment("位置");

                entity.Property(e => e.ShebeiType)
                    .HasMaxLength(255)
                    .HasComment("设备状态");

                entity.Property(e => e.UrlType).HasComment("监控请求的url方式类型");

                entity.Property(e => e.XlShebeiId)
                    .HasMaxLength(255)
                    .HasComment("设备ID");

                entity.Property(e => e.XlShebeiType)
                    .HasMaxLength(255)
                    .HasComment("设备类型	");
            });

            modelBuilder.Entity<XunchaDuty>(entity =>
            {
                entity.HasKey(e => e.XunchaDutyUuid)
                    .HasName("PK__XunchaDu__E548CB40D9A8AD9D");

                entity.HasComment("巡查值班表");

                entity.Property(e => e.XunchaDutyUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.FuzerPhone)
                    .HasMaxLength(255)
                    .HasComment("责人电话");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ShebeiId)
                    .HasMaxLength(255)
                    .HasComment("设备编号	");

                entity.Property(e => e.XunchaAddress)
                    .HasMaxLength(255)
                    .HasComment("巡查地点");

                entity.Property(e => e.XunchaInfoRen)
                    .HasMaxLength(255)
                    .HasComment("巡查人");

                entity.Property(e => e.XunchaTime)
                    .HasMaxLength(255)
                    .HasComment("巡查时间	");
            });

            modelBuilder.Entity<XunchaInfo>(entity =>
            {
                entity.HasKey(e => e.XunchaInfoUuid);

                entity.HasComment("巡查信息表");

                entity.Property(e => e.XunchaInfoUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.FuzerPhone)
                    .HasMaxLength(255)
                    .HasComment("责人电话");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.QingkShangbao)
                    .HasMaxLength(255)
                    .HasComment("情况上报	");

                entity.Property(e => e.ShebeiId)
                    .HasMaxLength(255)
                    .HasComment("设备编号	");

                entity.Property(e => e.Staues)
                    .HasMaxLength(255)
                    .HasComment("状态");

                entity.Property(e => e.XunchaAddress)
                    .HasMaxLength(255)
                    .HasComment("巡查地点");

                entity.Property(e => e.XunchaInfoRen)
                    .HasMaxLength(255)
                    .HasComment("巡查人");

                entity.Property(e => e.XunchaTime)
                    .HasMaxLength(255)
                    .HasComment("巡查时间	");
            });

            modelBuilder.Entity<Yclactivity>(entity =>
            {
                entity.HasKey(e => e.YclactivityUuid)
                    .HasName("PK__YCLActiv__018391D702AF654E")
                    .IsClustered(false);

                entity.ToTable("YCLActivity");

                entity.HasComment("共青团活动表");

                entity.HasIndex(e => e.YclactivityUuid)
                    .HasName("IX_YCLActivity")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.YclactivityUuid)
                    .HasColumnName("YCLActivityUuid")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Content).HasComment("内容");

                entity.Property(e => e.Cover).HasComment("封面");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否删除");

                entity.Property(e => e.IsRelease)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否发布");

                entity.Property(e => e.Photo).HasComment("3张图片");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnType("datetime")
                    .HasComment("活动时间");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("标题");

                entity.Property(e => e.VillageUuid)
                    .HasMaxLength(255)
                    .HasComment("所在村镇");
            });

            modelBuilder.Entity<Ygiene>(entity =>
            {
                entity.HasKey(e => e.YgieneUuid);

                entity.HasComment("卫生点信息表");

                entity.Property(e => e.YgieneUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lat)
                    .HasMaxLength(255)
                    .HasComment("纬度");

                entity.Property(e => e.Lon)
                    .HasMaxLength(255)
                    .HasComment("经度");

                entity.Property(e => e.UrlType).HasComment("监控url类型");

                entity.Property(e => e.YgieneAddress)
                    .HasMaxLength(255)
                    .HasComment("位置");

                entity.Property(e => e.YgieneMonitorId)
                    .HasMaxLength(255)
                    .HasComment("监控编号");

                entity.Property(e => e.YgieneName)
                    .HasMaxLength(255)
                    .HasComment("卫生点名称");

                entity.Property(e => e.YgieneStaues)
                    .HasMaxLength(255)
                    .HasComment("监控状态");

                entity.Property(e => e.YgieneType)
                    .HasMaxLength(255)
                    .HasComment("监控类型");
            });

            modelBuilder.Entity<YoukeOverflow>(entity =>
            {
                entity.HasKey(e => e.YoukeOverflowUuid);

                entity.HasComment("游客溢出情况统计");

                entity.Property(e => e.YoukeOverflowUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.JindianName)
                    .HasMaxLength(255)
                    .HasComment("景点名称");

                entity.Property(e => e.OverflowInfo)
                    .HasMaxLength(255)
                    .HasComment("溢出情况");

                entity.Property(e => e.Rongliang)
                    .HasMaxLength(255)
                    .HasComment("容量");

                entity.Property(e => e.SsRenCount)
                    .HasMaxLength(255)
                    .HasComment("实时人数");

                entity.Property(e => e.YujinInfo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("预警情况");
            });

            modelBuilder.Entity<Youth>(entity =>
            {
                entity.HasKey(e => e.YouthUuid);

                entity.HasComment("国防信息适龄青年表");

                entity.Property(e => e.YouthUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.ArmyWill)
                    .HasMaxLength(255)
                    .HasComment("参军意愿");

                entity.Property(e => e.Birth)
                    .HasMaxLength(255)
                    .HasComment("出生日期");

                entity.Property(e => e.Census)
                    .HasMaxLength(255)
                    .HasComment("户籍地");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasComment("身份证号");

                entity.Property(e => e.Nation)
                    .HasMaxLength(255)
                    .HasComment("民族");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(255)
                    .HasComment("职业");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasComment("手机号");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .HasComment("性别");

                entity.Property(e => e.YouthName)
                    .HasMaxLength(255)
                    .HasComment("人员姓名");
            });

            modelBuilder.Entity<YouthPosition>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.HasComment("青年活动阵地");

                entity.HasIndex(e => e.YpositionUuid)
                    .HasName("IX_YouthPosition")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Accessory).HasComment("附件");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasComment("名称");

                entity.Property(e => e.YpositionUuid).HasColumnName("YPositionUUID");
            });

            modelBuilder.Entity<Yqfkryb>(entity =>
            {
                entity.HasKey(e => e.Yqfkrybuuid)
                    .HasName("PK__YQFKRYB__EF5AFDD3AC9D78B5");

                entity.ToTable("YQFKRYB");

                entity.HasComment("疫情防控人员表");

                entity.Property(e => e.Yqfkrybuuid)
                    .HasColumnName("YQFKRYBUuid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Attention)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"关注程度

");

                entity.Property(e => e.CheckMethod)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"排查方式

");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联系手机

");

                entity.Property(e => e.ContactPhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"联络人手机号码

");

                entity.Property(e => e.CurrentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"现住地址

");

                entity.Property(e => e.EpidemicPreventionAndControlStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"疫情防控状态

");

                entity.Property(e => e.FamilyMemberContactInformation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"家庭成员联系方式

");

                entity.Property(e => e.GoToAddressContact)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"去往地址联络人

");

                entity.Property(e => e.GuardianInformation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"监护人员信息

");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("身份证");

                entity.Property(e => e.IsolationTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"隔离时间

");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("姓名");

                entity.Property(e => e.NameOfResponsibleDoctor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"责任医生姓名

");

                entity.Property(e => e.Origin)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"来源地

");

                entity.Property(e => e.OwnedNetwork)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("所属网络");

                entity.Property(e => e.PhoneNumberOfResponsibleDoctor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"责任医生电话

");

                entity.Property(e => e.ReasonForConcern)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"关注原因

");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"备注

");

                entity.Property(e => e.ReturnOrEstimatedReturnTime)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"返回或预计返回时间

");

                entity.Property(e => e.ServiceMemberInformation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"服务成员信息

");

                entity.Property(e => e.ServicePremises)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"服务处所

");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.ToAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"去往地址

");

                entity.Property(e => e.Transportation)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"交通方式

");

                entity.Property(e => e.WhereToGo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"去往地点

");

                entity.Property(e => e.YesNoSuspectedFever)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment(@"有/无疑似发热

");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
