import { ApplicationResult, PagedResult } from "#/data";
import request from "@/request";

export interface OnlineUserResult {
  userId: string;
  userName: string;
  ip: string;
  address: string;
  os: string;
  browser: string;
  creationTime: string;
}

/**
 * 在线用户
 * @param params
 * @returns
 */
export function getOnlineUserList(params: any) {
  return request.get<any, ApplicationResult<PagedResult<OnlineUserResult>>>(
    "/api/OnlineUser/GetOnlineUserList",
    { params: params }
  );
}

/**
 * 注销用户
 * @param params
 * @returns
 */
export function logoutUser(userId: string) {
  return request.post<any, ApplicationResult<PagedResult<OnlineUserResult>>>(
    "/api/OnlineUser/Logout?userId=" + userId
  );
}
