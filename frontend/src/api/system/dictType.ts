import request from "@/utils/request";
import { ApplicationResult, AppOption, PagedResult } from "#/data";

export interface DictTypeItem {
  id: string;
  name: string;
  isEnabled: string;
  dictType: string;
  remark: string;
}

/**
 * 字典类型列表
 * @param params
 * @returns
 */
export function getDictTypeList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<DictTypeItem>>>(
    "/api/dictType/GetDictTypeList",
    { params: params }
  );
}

/**
 * 新增字典类型
 * @param data
 * @returns
 */
export function addDictType(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/api/dictType/AddDictType",
    data
  );
}

/**
 * 修改字典类型
 * @param data
 * @returns
 */
export function updateDictType(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/api/dictType/UpdateDictType",
    data
  );
}

/**
 * 删除字典类型
 * @param id
 * @returns
 */
export function deleteDictType(dictType: string) {
  return request.delete<any, ApplicationResult<any>>(
    "/api/dictType/DeleteDictType/" + dictType
  );
}

/**
 * 获取字典选项
 * @param dictType 
 * @returns 
 */
export function getDictDataOptions(dictType: string) {
  return request.get<any, ApplicationResult<AppOption[]>>(
    "/api/dictType/GetDictDataOptions",
    {
      params: {
        type: dictType,
      },
    }
  );
}
