import { createRouter, createWebHashHistory } from "vue-router";
import { useAuthorization } from "@/hooks/useAuthorization";
import fixRoutes from "./fixRoutes";

const router = createRouter({
  history: createWebHashHistory(),
  routes: fixRoutes,
});

//全局前置路由守卫
router.beforeEach(async (to, _) => {
  const userAuth = useAuthorization();

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
    return { path: "/errors/403" };
  }
  return true;
});

export default router;
