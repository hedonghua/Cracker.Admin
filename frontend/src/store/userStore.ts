import { defineStore } from "pinia";

/**
 * 当前用户相关状态
 */
export const useUserStore = defineStore("user", {
  state: (): UserState => {
    return {
      user: null,
      info: null,
      roles: [],
      auths: [],
      menus: [],
    };
  },
  persist: true,
  actions: {
    setUser(user: UserAuthInfo) {
      this.user = user;
      if (user.auths) {
        this.auths = user.auths;
      }
    },
    setInfo(personalInfo: UserPersonalInfo) {
      this.info = personalInfo;
    },
    updateAccessToken(
      accessToken: string,
      refreshToken: string,
      expiredTime: Date
    ) {
      if (this.user) {
        this.user.accessToken = accessToken;
        this.user.refreshToken = refreshToken;
        this.user.expiredTime = expiredTime;
      }
    },
    setAuthorization(roles: string[], auths: string[]) {
      this.roles = roles;
      this.auths = auths;
    },
    setMenus(menus: any[]) {
      this.menus = menus;
    },
    clear() {
      this.user = null;
      this.info = null;
      this.roles = [];
      this.auths = [];
      this.menus = [];
    },
  },
});

export interface UserState {
  user: UserAuthInfo | null;
  info: UserPersonalInfo | null;
  roles: string[];
  auths: string[];
  menus: any[];
}

/**
 * 用户鉴权信息
 */
export interface UserAuthInfo {
  username: string; //用户名
  accessToken: string; //访问token
  refreshToken: string; //刷新token
  expiredTime: Date; //过期时间
  auths?: string[];
}

/**
 * 用户个人信息
 */
export interface UserPersonalInfo {
  avatar: string;
  nickName: string;
  sex: number;
}
