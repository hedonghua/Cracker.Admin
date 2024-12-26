import request from "@/request";
import { ApplicationResult, PagedResult } from "#/data";

export interface DictItem {
  id: string;
  key: string;
  value?: string;
  group?: string;
  sort: number;
  label: string;
  remark: string;
}

/**
 * 字典列表
 * @param params
 * @returns
 */
export function getDictList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<DictItem>>>(
    "/api/dict/list",
    { params: params }
  );
}

/**
 * 新增字典
 * @param data
 * @returns
 */
export function addDict(data: any) {
  return request.post<any, ApplicationResult<any>>("/api/dict/add", data);
}

/**
 * 修改字典
 * @param data
 * @returns
 */
export function updateDict(data: any) {
  return request.put<any, ApplicationResult<any>>("/api/dict/update", data);
}

/**
 * 删除字典
 * @param id
 * @returns
 */
export function deleteDict(ids: string[]) {
  return request.delete<any, ApplicationResult<any>>("/api/dict/delete", {
    data: ids,
  });
}

/**
 * 刷新字典缓存
 * @returns
 */
export function refreshDict() {
  return request.post<any, ApplicationResult<any>>("/api/dict/refresh");
}
