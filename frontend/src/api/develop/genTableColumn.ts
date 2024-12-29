import request from "@/request";
import { ApplicationResult } from "#/data";

export interface GenTableColumnResult {
  /** ID */
  id: string;
  /** 配置表ID */
  genTableId: string;
  /** * 表名 */
  tableName: string;
  /** * 列名 */
  columnName: string;
  /** * C#属性名 */
  csharpPropName: string;
  /** * JS字段名 */
  jsFieldName: string;
  /** 数据库列类型 */
  columnType: string;
  /** C#类型 */
  csharpType: string;
  /** JS类型 */
  jsType: string;
  /** HTML类型 */
  htmlType: string;
  /** 列描述 */
  comment: string;
  /** 最大长度 */
  maxLength: string;
  /** 是否可空 */
  isNullable: boolean;
  /** 是否参与新增 */
  isInsert: string;
  /** 是否参与修改 */
  isUpdate: string;
  /** 是否参与搜索 */
  isSearch: string;
  /** 搜索类型 */
  searchType: string;
  /** 是否在表格中显示 */
  isShow: string;
}

/**
 * 生成列配置列表
 * @param params
 * @returns
 */
export function genTableColumnList(params: any) {
  return request.get<any, ApplicationResult<Array<GenTableColumnResult>>>(
    "/api/GenTableColumn/GetGenTableColumnList",
    { params: params }
  );
}

/**
 * 保存生成列配置
 * @param data
 * @returns
 */
export function saveGenTableColumn(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/api/GenTableColumn/SaveGenTableColumn",
    data
  );
}