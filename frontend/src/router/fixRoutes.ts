import Login from "@/views/login/index.vue";
import Layout from "@/components/layout/index.vue";
import Home from "@/views/home/index.vue";
import Person from "@/views/person/index.vue";
import { HOME_PATH } from "@/consts";
import { RouteRecordRaw } from "vue-router";

// 固定路由
const fixRoutes: RouteRecordRaw[] = [
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
      {
        path: "/system/dictData/:dictType",
        component: () => import("@/views/system/dictData/index.vue"),
        meta: {
          title: "字典数据",
          hidden: true,
        },
      },
    ],
  },
];

export default fixRoutes;
