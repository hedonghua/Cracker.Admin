import request from "@/request";
import { AppOption, ApplicationResult, PagedResult } from "#/data";

export interface RoleItem {
  id: string;
  roleName: string;
  remark: string;
}

/**
 * 添加角色
 * @param data
 * @returns
 */
export function addRole(data: any) {
  return request.post<any, ApplicationResult<any>>("/api/role/add", data);
}

/**
 * 角色列表
 * @param params
 * @returns
 */
export function getRoleList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<RoleItem>>>(
    "/api/role/list",
    { params: params }
  );
}

/**
 * 更新角色
 * @param data
 * @returns
 */
export function updateRole(data: any) {
  return request.put<any, ApplicationResult<any>>("/api/role/update", data);
}

/**
 * 删除角色
 * @param id
 * @returns
 */
export function deleteRole(id: string) {
  return request.delete<any, ApplicationResult<any>>("/api/role/delete/" + id);
}

/**
 * 分配菜单
 * @param data
 * @returns
 */
export function assignMenu(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/api/role/assign-menu",
    data
  );
}

/**
 * 角色选项
 * @returns
 */
export function getRoleOptions() {
  return request.get<any, ApplicationResult<Array<AppOption>>>(
    "/api/role/options"
  );
}

/**
 * 获取角色菜单
 * @param id 角色ID
 * @returns
 */
export function getRoleMenuIds(id: string) {
  return request.get<any, ApplicationResult<never[]>>(
    "/api/role/menus/" + id
  );
}