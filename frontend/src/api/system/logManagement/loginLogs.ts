import { ApplicationResult, PagedResult } from "#/data";
import request from "@/request";

export interface LoginLogDto {
  id: string;
  userName: string;
  ip: string;
  address: string;
  os: string;
  browser: string;
  operationMsg: string;
  isSuccess: boolean;
  creationTime: string;
}

/**
 * 登录日志
 * @param params
 * @returns
 */
export function getLoginLogList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<LoginLogDto>>>(
    "/api/login-log/list",
    { params: params }
  );
}
