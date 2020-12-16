namespace ClassLibrary1.HData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HeQiaoDb : DbContext
    {
        public HeQiaoDb()
            : base("name=HeQiaoDb")
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Barometric> Barometric { get; set; }
        public virtual DbSet<BuildHouse> BuildHouse { get; set; }
        public virtual DbSet<Canyin> Canyin { get; set; }
        public virtual DbSet<CityManagement> CityManagement { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ConcerQiao> ConcerQiao { get; set; }
        public virtual DbSet<CpcmanInfo> CpcmanInfo { get; set; }
        public virtual DbSet<Cunzhenmb> Cunzhenmb { get; set; }
        public virtual DbSet<DangActivity> DangActivity { get; set; }
        public virtual DbSet<DangerousRoom> DangerousRoom { get; set; }
        public virtual DbSet<DangOrganization> DangOrganization { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentDeclare> DepartmentDeclare { get; set; }
        public virtual DbSet<DepartSumary> DepartSumary { get; set; }
        public virtual DbSet<DisasterInfo> DisasterInfo { get; set; }
        public virtual DbSet<DzhizaihaiInfo> DzhizaihaiInfo { get; set; }
        public virtual DbSet<Emergency> Emergency { get; set; }
        public virtual DbSet<Evacuate> Evacuate { get; set; }
        public virtual DbSet<Followvillager> Followvillager { get; set; }
        public virtual DbSet<Gtoilet> Gtoilet { get; set; }
        public virtual DbSet<HeDaowater> HeDaowater { get; set; }
        public virtual DbSet<HomeAddress> HomeAddress { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<HqCommunal> HqCommunal { get; set; }
        public virtual DbSet<Mission> Mission { get; set; }
        public virtual DbSet<MissionJournal> MissionJournal { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganPeoInfo> OrganPeoInfo { get; set; }
        public virtual DbSet<PaibanInfo> PaibanInfo { get; set; }
        public virtual DbSet<ParkingLot> ParkingLot { get; set; }
        public virtual DbSet<PerformanceDeclare> PerformanceDeclare { get; set; }
        public virtual DbSet<PersonalDiary> PersonalDiary { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<PriorityJournal> PriorityJournal { get; set; }
        public virtual DbSet<Promo> Promo { get; set; }
        public virtual DbSet<PromoTeam> PromoTeam { get; set; }
        public virtual DbSet<Propaganda> Propaganda { get; set; }
        public virtual DbSet<PublicityFronts> PublicityFronts { get; set; }
        public virtual DbSet<PublicityType> PublicityType { get; set; }
        public virtual DbSet<Qiye> Qiye { get; set; }
        public virtual DbSet<RectifyInfo> RectifyInfo { get; set; }
        public virtual DbSet<RegionInfo> RegionInfo { get; set; }
        public virtual DbSet<RegionInfos> RegionInfos { get; set; }
        public virtual DbSet<RegionInfos_copy> RegionInfos_copy { get; set; }
        public virtual DbSet<Relationships> Relationships { get; set; }
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
        public virtual DbSet<Sightseer> Sightseer { get; set; }
        public virtual DbSet<SmokeGan> SmokeGan { get; set; }
        public virtual DbSet<SthOrganic> SthOrganic { get; set; }
        public virtual DbSet<SystemIcon> SystemIcon { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<SystemMenu> SystemMenu { get; set; }
        public virtual DbSet<SystemPermission> SystemPermission { get; set; }
        public virtual DbSet<SystemRole> SystemRole { get; set; }
        public virtual DbSet<SystemRolePermissionMapping> SystemRolePermissionMapping { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<SystemUserRoleMapping> SystemUserRoleMapping { get; set; }
        public virtual DbSet<TheUnion> TheUnion { get; set; }
        public virtual DbSet<TheUnionZhengc> TheUnionZhengc { get; set; }
        public virtual DbSet<Tourist> Tourist { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<TownMilitia> TownMilitia { get; set; }
        public virtual DbSet<UAV> UAV { get; set; }
        public virtual DbSet<UnifiedAddressLibraryUserInfo> UnifiedAddressLibraryUserInfo { get; set; }
        public virtual DbSet<Userinfoty> Userinfoty { get; set; }
        public virtual DbSet<Village> Village { get; set; }
        public virtual DbSet<VillageMember> VillageMember { get; set; }
        public virtual DbSet<Volunteers> Volunteers { get; set; }
        public virtual DbSet<VolunteerTeam> VolunteerTeam { get; set; }
        public virtual DbSet<WaterLevel> WaterLevel { get; set; }
        public virtual DbSet<WaterLevelSheb> WaterLevelSheb { get; set; }
        public virtual DbSet<WomenActivities> WomenActivities { get; set; }
        public virtual DbSet<WomenFederation> WomenFederation { get; set; }
        public virtual DbSet<WomenHouse> WomenHouse { get; set; }
        public virtual DbSet<Woods> Woods { get; set; }
        public virtual DbSet<XlProject> XlProject { get; set; }
        public virtual DbSet<XunchaDuty> XunchaDuty { get; set; }
        public virtual DbSet<XunchaInfo> XunchaInfo { get; set; }
        public virtual DbSet<Ygiene> Ygiene { get; set; }
        public virtual DbSet<YoukeOverflow> YoukeOverflow { get; set; }
        public virtual DbSet<Youth> Youth { get; set; }
        public virtual DbSet<YouthPosition> YouthPosition { get; set; }
        public virtual DbSet<UnifiedAddressLibrary> UnifiedAddressLibrary { get; set; }
        public virtual DbSet<View_Menu> View_Menu { get; set; }
        public virtual DbSet<View_SystemPermissionWithMenu> View_SystemPermissionWithMenu { get; set; }
        public virtual DbSet<View_SystemPermissionWithMenu2> View_SystemPermissionWithMenu2 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DangActivity>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.DeclareName)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.DeclareDepartment)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.DeclareTime)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.BonusPoint)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.PlusContent)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.Deduction)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.DeductionContent)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentDeclare>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<DepartSumary>()
                .Property(e => e.DeSuAddTime)
                .IsUnicode(false);

            modelBuilder.Entity<DepartSumary>()
                .Property(e => e.DepartName)
                .IsUnicode(false);

            modelBuilder.Entity<DzhizaihaiInfo>()
                .Property(e => e.Diqu)
                .IsUnicode(false);

            modelBuilder.Entity<DzhizaihaiInfo>()
                .Property(e => e.Shuliang)
                .IsUnicode(false);

            modelBuilder.Entity<HomeAddress>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<HomeAddress>()
                .Property(e => e.Addresscode)
                .IsUnicode(false);

            modelBuilder.Entity<HomeAddress>()
                .Property(e => e.lon)
                .HasPrecision(10, 7);

            modelBuilder.Entity<HomeAddress>()
                .Property(e => e.lat)
                .HasPrecision(10, 7);

            modelBuilder.Entity<Mission>()
                .Property(e => e.Missiontype)
                .IsUnicode(false);

            modelBuilder.Entity<Mission>()
                .Property(e => e.Fujian)
                .IsUnicode(false);

            modelBuilder.Entity<MissionJournal>()
                .Property(e => e.Read)
                .IsUnicode(false);

            modelBuilder.Entity<MissionJournal>()
                .Property(e => e.Completed)
                .IsUnicode(false);

            modelBuilder.Entity<MissionJournal>()
                .Property(e => e.Coordination)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Naem)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Note>()
                .Property(e => e.ModuleNaem)
                .IsUnicode(false);

            modelBuilder.Entity<PaibanInfo>()
                .Property(e => e.ZbYear)
                .IsUnicode(false);

            modelBuilder.Entity<PriorityJournal>()
                .Property(e => e.Read)
                .IsUnicode(false);

            modelBuilder.Entity<PriorityJournal>()
                .Property(e => e.Completed)
                .IsUnicode(false);

            modelBuilder.Entity<PriorityJournal>()
                .Property(e => e.Coordination)
                .IsUnicode(false);

            modelBuilder.Entity<Promo>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<PublicityFronts>()
                .Property(e => e.Cover)
                .IsUnicode(false);

            modelBuilder.Entity<RegionInfo>()
                .Property(e => e.RegionX)
                .IsUnicode(false);

            modelBuilder.Entity<RegionInfo>()
                .Property(e => e.RegionY)
                .IsUnicode(false);

            modelBuilder.Entity<Relationships>()
                .Property(e => e.Addpeople)
                .IsUnicode(false);

            modelBuilder.Entity<Relationships>()
                .Property(e => e.Picture)
                .IsUnicode(false);

            modelBuilder.Entity<ResponseInit>()
                .Property(e => e.Addpeoople)
                .IsUnicode(false);

            modelBuilder.Entity<ResponseInit>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<ResponseInit>()
                .Property(e => e.Situation)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityCode>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityCode>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityCode>()
                .Property(e => e.ChargeName)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityCode>()
                .Property(e => e.ChargePhone)
                .IsUnicode(false);

            modelBuilder.Entity<Sightseer>()
                .Property(e => e.Shengneiwai)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.OperationContent)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<SystemLog>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<SystemUser>()
                .Property(e => e.ManageDepartment)
                .IsUnicode(false);

            modelBuilder.Entity<TheUnion>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<UAV>()
                .Property(e => e.UAVName)
                .IsUnicode(false);

            modelBuilder.Entity<UAV>()
                .Property(e => e.UAVNumber)
                .IsUnicode(false);

            modelBuilder.Entity<UAV>()
                .Property(e => e.UAVUrl)
                .IsUnicode(false);

            modelBuilder.Entity<UAV>()
                .Property(e => e.UAVAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Village>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<Village>()
                .Property(e => e.VillageName)
                .IsUnicode(false);

            modelBuilder.Entity<VillageMember>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<WomenActivities>()
                .Property(e => e.AddPeople)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.oid)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.SOURCEADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.CITY)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.COUNTY)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.TOWN)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.COMMUNITY)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.SQUAD)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.VILLAGE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.SZONE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.STREET)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.DOOR)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.RESREGION)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.BUILDING)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.BUILDING_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.UNIT)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.FLOOR)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.ROOM)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.GRID_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.BUILDING_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.HOUSE_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.CREATETIME)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.UPDATETIME)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.FROM_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.TO_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.BUILDING_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.DATASOURCE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.REVERSE1)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.REVERSE2)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.LON)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.LAT)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.Z)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.ROOM_FLOOR)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.ADDRTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.GUID)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.INSERTTIME)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.SYSTEMID)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.BELONG_BUILDING)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.ADDRESS_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.REMARK)
                .IsUnicode(false);

            modelBuilder.Entity<UnifiedAddressLibrary>()
                .Property(e => e.AddTime)
                .IsUnicode(false);
        }
    }
}
