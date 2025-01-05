import { RouteRecordRaw } from "vue-router";
import router from "./index";
import { MenuItem, getSidebarMenus } from "@/api/system/menu";
import fixRoutes from "./fixRoutes";
import { useAuthorization } from "@/hooks/useAuthorization";

// views下页面
const modulesRoutes = import.meta.glob("/src/views/**/*.{vue,tsx}");

const useRouteCache = () => {
  /**
   * 设置路由缓存
   * @param routes
   */
  const loadRoutes = async () => {
    //带树形
    const treeRoutes = [...fixRoutes];
    //列表路由
    const listRoutes = [...fixRoutes];

    const userAuth = useAuthorization(true);
    if (!userAuth.isAuthenticated()) return;
    try {
      const { data } = await getSidebarMenus();
      const asyncRoutes = genRoutes(data, listRoutes);
      const layoutRoute = fixRoutes.find((x) => x.path === "/");
      const layoutRouteIndex = fixRoutes.findIndex((x) => x.path === "/");
      if (layoutRoute) {
        asyncRoutes.forEach((asyncRoute) => {
          router.addRoute(layoutRoute.name!, asyncRoute);
        });
        treeRoutes[layoutRouteIndex].children = [
          ...treeRoutes[layoutRouteIndex].children!,
          ...asyncRoutes,
        ];
      }
    } catch (error) {
      console.error("加载路由错误：", error);
    }
    localStorage.setItem("treeRoutes", JSON.stringify(treeRoutes));
    localStorage.setItem("listRoutes", JSON.stringify(listRoutes));
  };

  // 生成路由：由动态数据"MenuItem[]"=>"RouteRecordRaw[]"
  const genRoutes = (
    array: MenuItem[],
    listRoutes?: any[]
  ): RouteRecordRaw[] => {
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
        listRoutes?.push(r);
      } else {
        r.children = genRoutes(item.children, listRoutes);
      }
      dynamicRoutes.push(r);
    }
    return dynamicRoutes;
  };

  /**
   * 获取路由缓存
   * @returns
   */
  const getCache = (): RouteRecordRaw[] | undefined => {
    const local = localStorage.getItem("treeRoutes");
    if (local) {
      return JSON.parse(local) as RouteRecordRaw[];
    }
    return undefined;
  };

  return {
    loadRoutes,
    getCache,
  };
};

export { useRouteCache };
