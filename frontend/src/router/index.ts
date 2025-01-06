import { createRouter, createWebHashHistory } from "vue-router";
import { useAuthorization } from "@/hooks/useAuthorization";
import fixRoutes from "./fixRoutes";
import { useRouteCache } from "./hook";

const router = createRouter({
  history: createWebHashHistory(),
  routes: fixRoutes,
});

//全局前置路由守卫
router.beforeEach(async (to, _) => {
  const userAuth = useAuthorization();
  const routeCache = useRouteCache();

  //未登录或token过期
  if (!userAuth.isAuthenticated() && to.name !== "Login") {
    return { name: "Login" };
  } else if (to.meta?.roles) {
    //const needRoles = to.meta?.roles as string[];
    //无角色权限
    // if (!userAuth.isInRole(needRoles)) {
    //   tabManager.setActiveWhite();
    //   return { path: "/errors/403" };
    // }
  } else if (to.matched.length === 0) {
    //路由不存在，先加载路由；加载后再判断路由是否存在
    await routeCache.loadRoutes();
    const listRoutes = routeCache.getListCache();
    if (listRoutes != null && listRoutes.some((x) => x.path === to.fullPath)) {
      return to.fullPath;
    }
    return { path: "/errors/403" };
  }
  return true;
});

export default router;
