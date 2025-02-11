import request from "@/utils/request";
import { ApplicationResult, PagedResult } from "#/data";

export interface UserItem {
  id: string;
  avatar: string;
  username: string;
  sex: string;
}

/**
 * 添加用户
 * @param data
 * @returns
 */
export function addUser(data: any) {
  return request.post<any, ApplicationResult<any>>("/api/user/add", data);
}

/**
 * 用户列表
 * @param params
 * @returns
 */
export function getUserList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<UserItem>>>(
    "/api/user/list",
    { params: params }
  );
}

/**
 * 删除用户
 * @param id
 * @returns
 */
export function deleteUser(id: string) {
  return request.delete<any, ApplicationResult<any>>("/api/user/delete/" + id);
}

/**
 * 分配角色
 * @param data
 * @returns
 */
export function assignRole(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/api/user/assign-role",
    data
  );
}

/**
 * 更改启用状态
 * @param id
 * @returns
 */
export function switchEnabledStatus(id: string) {
  return request.put<any, ApplicationResult<any>>(
    "/api/user/change-enabled/" + id
  );
}

/**
 * 获取用户角色
 * @param id 用户ID
 * @returns
 */
export function getUserRoleIds(id: string) {
  return request.get<any, ApplicationResult<never[]>>(
    "/api/user/roles/" + id
  );
}
