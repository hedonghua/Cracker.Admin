using AutoMapper;

using Cracker.Admin.Entity;
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
        CreateMap<DictDto, SysDict>();
        CreateMap<SysDict, DictListDto>();
        CreateMap<SysLoginLog, LoginLogListDto>();
        CreateMap<SysBusinessLog, BusinessLogListDto>();
        CreateMap<OrgPositionGroup, PositionGroupListDto>();
        CreateMap<OrgEmployee, EmployeeListDto>();
        CreateMap<OrgPosition, PositionListDto>();
        CreateMap<OrgDept, DeptListDto>();
    }
}