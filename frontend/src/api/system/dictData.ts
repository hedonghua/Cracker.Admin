import request from "@/request";
import { ApplicationResult, PagedResult } from "#/data";

export interface DictItem {
  id: string;
  key: string;
  value?: string;
  dictType?: string;
  sort: number;
  label: string;
  remark: string;
}

/**
 * 字典数据列表
 * @param params
 * @returns
 */
export function getDictDataList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<DictItem>>>(
    "/api/dictData/list",
    { params: params }
  );
}

/**
 * 新增字典数据
 * @param data
 * @returns
 */
export function addDictData(data: any) {
  return request.post<any, ApplicationResult<any>>("/api/dictData/add", data);
}

/**
 * 修改字典数据
 * @param data
 * @returns
 */
export function updateDictData(data: any) {
  return request.put<any, ApplicationResult<any>>("/api/dictData/update", data);
}

/**
 * 删除字典数据
 * @param id
 * @returns
 */
export function deleteDictData(ids: string[]) {
  return request.delete<any, ApplicationResult<any>>("/api/dictData/delete", {
    data: ids,
  });
}