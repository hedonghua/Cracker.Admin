import request from "@/request";
import { ApplicationResult } from "#/data";

export interface LoginForm {
  username: string;
  password: string;
}

export interface LoginData extends TokenData {
  username: string;
  auths?: string[];
}

export interface TokenData {
  accessToken: string;
  refreshToken: string;
  expiredTime: Date;
}

export interface UserInfoData {
  roles: string[];
  auths: string[];
  avatar: string;
  nickName: string;
  sex: number;
}

// 用户登录
export function userLogin(params: LoginForm) {
  return request.post<any, ApplicationResult<LoginData>>(
    "/api/account/login",
    params
  );
}

// 刷新token
export function refreshToken(refreshToken: LoginForm) {
  return request.post<any, ApplicationResult<TokenData>>(
    "/api/account/refresh-token?refreshToken=" + refreshToken
  );
}

// 用户信息
export function getUserInfo() {
  return request.get<any, ApplicationResult<UserInfoData>>(
    "/api/account/userinfo"
  );
}

// 更新用户基本信息
export function updateUserInfo(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/api/account/update-info",
    data
  );
}

// 更新用户密码
export function updateUserPwd(data: any) {
  return request.put<any, ApplicationResult<any>>(
    "/api/account/update-pwd",
    data
  );
}

// 注销
export function signout() {
  return request.post<any, ApplicationResult<boolean>>("/api/account/signout");
}
