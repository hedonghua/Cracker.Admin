using AutoMapper;

using Cracker.Admin.Account.Dtos;
using Cracker.Admin.Entities;
using Cracker.Admin.Organization.Dtos;
using Cracker.Admin.System.Dtos;
using Cracker.Admin.System.LogManagement.Dtos;

namespace Cracker.Admin;

public class CrackerAdminApplicationAutoMapperProfile : Profile
{
    public CrackerAdminApplicationAutoMapperProfile()
    {
        CreateMap<SysUser, UserListDto>();
        CreateMap<SysRole, RoleListDto>();
        CreateMap<MenuDto, SysMenu>();
        CreateMap<SysMenu, MenuListDto>();
        CreateMap<DictDataDto, SysDictData>();
        CreateMap<SysDictData, DictDataListDto>();
        CreateMap<SysLoginLog, LoginLogListDto>();
        CreateMap<SysBusinessLog, BusinessLogListDto>();
        CreateMap<OrgPositionGroup, PositionGroupListDto>();
        CreateMap<OrgEmployee, EmployeeListDto>();
        CreateMap<OrgPosition, PositionListDto>();
        CreateMap<OrgDept, DeptListDto>();
        CreateMap<DeptDto, OrgDept>();
        CreateMap<TokenResultDto, LoginResultDto>();
        CreateMap<EmployeeDto, OrgEmployee>();
        CreateMap<PositionGroupDto, OrgPositionGroup>();
        CreateMap<PositionDto, OrgPosition>();
        CreateMap<BusinessLogDto, SysBusinessLog>();
        CreateMap<TenantDto, SysTenant>();
    }
}