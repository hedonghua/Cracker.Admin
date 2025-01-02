import { RouteRecordRaw, createRouter, createWebHashHistory } from "vue-router";
import Login from "@/views/login/index.vue";
import Layout from "@/components/layout/index.vue";
import Home from "@/views/home/index.vue";
import Person from "@/views/person/index.vue";
import { MenuItem, getSidebarMenus } from "@/api/system/menu";
import { useRouteCache } from "./hook";
import { HOME_PATH } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";

// views下页面
const modulesRoutes = import.meta.glob("/src/views/**/*.{vue,tsx}");
// 固定路由
const routes: RouteRecordRaw[] = [
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/",
    name: "Layout",
    component: Layout,
    redirect: HOME_PATH,
    children: [
      {
        path: HOME_PATH,
        component: Home,
        meta: {
          title: "首页",
          icon: "ic:round-home",
        },
      },
      {
        path: "/errors",
        meta: {
          title: "错误页面",
          icon: "material-symbols:error",
          hidden: true,
        },
        children: [
          {
            path: "/errors/403",
            name: "err403",
            meta: {
              title: "403(禁止访问)",
            },
            component: () => import("@/views/errors/403.vue"),
          },
          {
            path: "/errors/404",
            name: "err404",
            meta: {
              title: "404(页面不存在)",
            },
            component: () => import("@/views/errors/403.vue"),
          },
          {
            path: "/errors/500",
            name: "err500",
            meta: {
              title: "500(服务器错误)",
            },
            component: () => import("@/views/errors/500.vue"),
          },
        ],
      },
      {
        path: "/person",
        component: Person,
        meta: {
          title: "基本资料",
          hidden: true,
        },
      },
      {
        path: "/develop/genTableColumn/:id",
        component: () => import("@/views/develop/genTableColumn/index.vue"),
        meta: {
          title: "列配置",
          hidden: true,
        },
      },
    ],
  },
];
// 生成路由：由动态数据"MenuItem[]"=>"RouteRecordRaw[]"
const genRoutes = (array: MenuItem[]): RouteRecordRaw[] => {
  const modulesRoutesKeys = Object.keys(modulesRoutes);
  const dynamicRoutes: RouteRecordRaw[] = [];
  for (let index = 0; index < array.length; index++) {
    const item = array[index];
    if (!item.path) continue;
    const routeIndex = modulesRoutesKeys.findIndex((mr) =>
      mr.includes(item.path + "/index.vue")
    );
    const r: RouteRecordRaw = {
      path: item.path,
      name: item.name,
      meta: {
        title: item?.meta?.title,
        icon: item?.meta?.icon,
        auths: item?.meta?.auths,
        roles: item?.meta?.roles,
      },
      children: [],
    };
    if (!item.children || item.children.length === 0) {
      if (routeIndex !== -1) {
        r.component = modulesRoutes[modulesRoutesKeys[routeIndex]];
      }
    } else {
      r.children = genRoutes(item.children);
    }
    dynamicRoutes.push(r);
  }
  return dynamicRoutes;
};

// 合并固定路由、动态路由
const mergeRoutes = async (): Promise<RouteRecordRaw[]> => {
  const allRoutes = [...routes];
  const userAuth = useAuthorization(true);
  if (!userAuth.isAuthenticated()) return allRoutes;
  try {
    const { data } = await getSidebarMenus();
    const asyncRoutes = genRoutes(data);
    const layoutRouteIndex = allRoutes.findIndex((x) => x.path === "/");
    if (layoutRouteIndex !== -1) {
      allRoutes[layoutRouteIndex].children = [
        ...allRoutes[layoutRouteIndex].children!,
        ...asyncRoutes,
      ];
    }
  } catch (error) {
    console.error("合并路由错误：", error);
  }
  useRouteCache().setCache(allRoutes);
  return allRoutes;
};

const router = createRouter({
  history: createWebHashHistory(),
  routes: await mergeRoutes(),
});

// const initRoutes = async () => {
//   const allRoutes = [...routes];
//   const userAuth = useAuthorization(true);
//   if (!userAuth.isAuthenticated()) return;
//   try {
//     const { data } = await getSidebarMenus();
//     const asyncRoutes = genRoutes(data);
//     const layoutRoute = routes.find((x) => x.path === "/");
//     const layoutRouteIndex = routes.findIndex((x) => x.path === "/");
//     if (layoutRoute) {
//       asyncRoutes.forEach((asyncRoute) => {
//         router.addRoute(layoutRoute.name!, asyncRoute);
//       });
//       allRoutes[layoutRouteIndex].children = [
//         ...allRoutes[layoutRouteIndex].children!,
//         ...asyncRoutes,
//       ];
//     }
//   } catch (error) {
//     console.error("合并路由错误：", error);
//   }
//   useRouteCache().setCache(allRoutes);
// };

// initRoutes();

//全局前置路由守卫
router.beforeEach((to, _) => {
  const userAuth = useAuthorization();
  //const tabManager = useTabManager();

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
