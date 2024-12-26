import request from "@/request";
import { ApplicationResult, PagedResult } from "#/data";

export interface PositionItem {
  id: string;
  /**
   * 职位编号
   */
  code: string;
  /**
   * 职位名称
   */
  name: string;
  /**
   * 职级
   */
  level: string;
  /**
   * 状态：1正常2停用
   */
  status: string;
  /**
   * 描述
   */
  description: string;
  /**
   * 职位分组
   */
  groupId: string;
}

/**
 * 添加职位
 * @param data
 * @returns
 */
export function addPosition(data: any) {
  return request.post<any, ApplicationResult<any>>("/api/position/add", data);
}

/**
 * 职位列表
 * @param params
 * @returns
 */
export function getPositionList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<PositionItem>>>(
    "/api/position/list",
    { params: params }
  );
}

/**
 * 更新职位
 * @param data
 * @returns
 */
export function updatePosition(data: any) {
  return request.put<any, ApplicationResult<any>>("/api/position/update", data);
}

/**
 * 删除职位
 * @param id
 * @returns
 */
export function deletePosition(id: string) {
  return request.delete<any, ApplicationResult<any>>(
    "/api/position/delete/" + id
  );
}

/**
 * 职位分组+职位选项
 */
export function getPositionOptions() {
  return request.get<any, ApplicationResult<any>>("/api/position/options");
}
