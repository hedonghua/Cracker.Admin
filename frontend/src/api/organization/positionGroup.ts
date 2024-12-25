import request from "@/request";
import { ApplicationResult } from "#/data";

export interface PositionGroupItem {
  /**
   * 分组名
   */
  groupName: string;
  /**
   * 备注
   */
  remark: string;
  /**
   * 父ID
   */
  parentId: string;
  /**
   * 层级父ID
   */
  parentIds: string;
}

/**
 * 添加职位分组
 * @param data
 * @returns
 */
export function addPositionGroup(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/adm/position-group/add",
    data
  );
}

/**
 * 职位分组列表
 * @param params
 * @returns
 */
export function getPositionGroupList(params: any) {
  return request.get<any, ApplicationResult<Array<PositionGroupItem>>>(
    "/adm/position-group/list",
    { params: params }
  );
}

/**
 * 更新职位分组
 * @param data
 * @returns
 */
export function updatePositionGroup(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/adm/position-group/update",
    data
  );
}

/**
 * 删除职位分组
 * @param id
 * @returns
 */
export function deletePositionGroup(id: string) {
  return request.delete<any, ApplicationResult<any>>(
    "/adm/position-group/delete/" + id
  );
}

/**
 * 职位分组选项
 * @param id
 * @returns
 */
export function getPositionGroupOptions() {
  return request.get<any, ApplicationResult<any>>(
    "/adm/position-group/options"
  );
}
