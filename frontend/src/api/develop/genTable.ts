import request from "@/request";
import { ApplicationResult } from "#/data";

export interface GenTableResult {
  /** 配置表ID */
  genTableId: string;
  /**
   * 表名
   */
  tableName: string;
  /**
   * 表描述
   */
  comment: string;
  /**
   * 业务名
   */
  businessName: string;
  /**
   * 实体名
   */
  entityName: string;
}

/**
 * 添加生成表配置
 * @param data
 * @returns
 */
export function addGenTable(data: any) {
  return request.post<any, ApplicationResult<any>>(
    "/api/GenTable/AddGenTable",
    data
  );
}

/**
 * 生成表配置列表
 * @param params
 * @returns
 */
export function getGenTableList(params: any) {
  return request.get<any, ApplicationResult<Array<GenTableResult>>>(
    "/api/GenTable/GetGenTableList",
    { params: params }
  );
}

/**
 * 更新生成表配置
 * @param data
 * @returns
 */
export function updateGenTable(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/api/GenTable/UpdateGenTable",
    data
  );
}

/**
 * 删除生成表配置
 * @param id
 * @returns
 */
export function deleteGenTable(ids: string[]) {
  return request.delete<any, ApplicationResult<any>>(
    "/api/GenTable/DeleteGenTable",
    { data: ids }
  );
}

/**
 * 数据库表列表
 * @param id
 * @returns
 */
export function getDatabaseTableList(params: any) {
  return request.get<any, ApplicationResult<any>>(
    "/api/GenTable/GetDatabaseTableList",
    { params: params }
  );
}

/**
 * 预览代码
 * @param params
 * @returns
 */
export function previewCode(params: any) {
  return request.get<any, ApplicationResult<any>>(
    "/api/GenTable/PreviewCode",
    { params: params }
  );
}