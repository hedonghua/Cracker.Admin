import request from "@/utils/request";
import { ApplicationResult, PagedResult } from "#/data";

export interface TenantItem {
  id: string;
  name: string;
  connectionString: string;
  redisConnection: string;
  remark: string;
}

/**
 * 添加租户
 * @param data
 * @returns
 */
export function addTenant(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/api/tenant/addTenant",
    data
  );
}

/**
 * 租户列表
 * @param params
 * @returns
 */
export function getTenantList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<TenantItem>>>(
    "/api/tenant/GetTenantList",
    { params: params }
  );
}

/**
 * 更新租户
 * @param data
 * @returns
 */
export function updateTenant(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/api/tenant/UpdateTenant",
    data
  );
}

/**
 * 删除租户
 * @param id
 * @returns
 */
export function deleteTenant(id: string) {
  return request.delete<any, ApplicationResult<any>>(
    "/api/tenant/DeleteTenant/" + id
  );
}

/** 获取解密信息 */
export function getDecryptInfo(params) {
  return request.get<any, ApplicationResult<string>>(
    "/api/tenant/getDecryptInfo",
    { params: params }
  );
}
