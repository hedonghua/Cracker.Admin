import { UserState, useUserStore } from "@/store/userStore";
import dayjs from "dayjs";
import { useTabManager } from "./useTabManager";
import { CloseTabType } from "@/store/tabStore";
import { signout } from "@/api/login";
import { AppResponseStatusCode } from "@/consts";
import { useRouteCache } from "@/router/hook";

export function useAuthorization(local?: boolean) {
  const userStore = (local ? getLocal() : getUseUserStore()) as UserState;

  function getLocal() {
    const str = localStorage.getItem("user");
    if (str) {
      var obj = JSON.parse(str);
      return {
        auths: obj["auths"],
        roles: obj["auths"],
        user: obj["user"],
      };
    }
    return {
      auths: [],
      roles: [],
      user: {},
    };
  }

  function getUseUserStore() {
    const storeIns = useUserStore();
    return {
      auths: storeIns.auths,
      roles: storeIns.roles,
      user: storeIns.user,
    };
  }

  /**
   * 是否含此权限
   * @param args 权限字符串
   * @returns 有一个不满足返回false
   */
  function hasPermission(...args: string[]): boolean {
    for (let i = 0; i < args.length; i++) {
      if (!userStore.auths.includes(args[i])) {
        return false;
      }
    }
    return true;
  }

  /**
   * 是否在拥有角色中
   * @param args 角色名
   * @returns 含有其中一个返回true
   */
  function isInRole(args: string[]): boolean {
    for (let i = 0; i < args.length; i++) {
      if (userStore.roles.includes(args[i])) {
        return true;
      }
    }
    return false;
  }

  /**
   * 是否登录
   * @returns
   */
  function isAuthenticated(): boolean {
    if (userStore.user != null) {
      return dayjs(userStore.user.expiredTime).isAfter(new Date());
    }
    return false;
  }

  /**
   * 登出
   */
  function signOut(executeApi: boolean = true) {
    function _signout() {
      useTabManager().close(CloseTabType.ALL);
      useUserStore().clear();
      const routeCache = useRouteCache();
      routeCache.clear();

      window.location.href = "/login";
    }

    if (executeApi) {
      signout().then((res) => {
        if (res.code === AppResponseStatusCode.SUCCESS) {
          _signout();
        }
      });
    } else {
      _signout();
    }
  }

  return {
    hasPermission,
    isInRole,
    isAuthenticated,
    signOut,
  };
}
