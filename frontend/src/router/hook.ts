import { RouteRecordRaw } from "vue-router";

const useRouteCache = () => {
  /**
   * 设置路由缓存
   * @param routes
   */
  const setCache = (routes: RouteRecordRaw[]) => {
    localStorage.setItem("routes", JSON.stringify(routes));
  };
  /**
   * 获取路由缓存
   * @returns 
   */
  const getCache = (): RouteRecordRaw[] | undefined => {
    const local = localStorage.getItem("routes");
    if (local) {
      return JSON.parse(local) as RouteRecordRaw[];
    }
    return undefined;
  };

  return {
    setCache,
    getCache,
  };
};

export { useRouteCache };
