import request from "@/request";
import { ApplicationResult } from "#/data";


/**
 * 添加部门
 * @param data
 * @returns
 */
export function addDept(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/adm/dept/add",
    data
  );
}

/**
 * 部门列表
 * @param params
 * @returns
 */
export function getDeptList(params: any) {
  return request.get<any, ApplicationResult<Array<any>>>(
    "/adm/dept/list",
    { params: params }
  );
}

/**
 * 更新部门
 * @param data
 * @returns
 */
export function updateDept(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/adm/dept/update",
    data
  );
}

/**
 * 删除部门
 * @param id
 * @returns
 */
export function deleteDept(id: string) {
  return request.delete<any, ApplicationResult<any>>(
    "/adm/dept/delete/" + id
  );
}

/**
 * 部门选项
 * @param id
 * @returns
 */
export function getDeptOptions() {
  return request.get<any, ApplicationResult<any>>(
    "/adm/dept/options"
  );
}
