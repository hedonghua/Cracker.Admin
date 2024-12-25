import axios, { AxiosInstance, AxiosResponse } from "axios";
import dayjs from "dayjs";
import { ElMessage } from "element-plus";
import { refreshToken as getAccessToken } from "@/api/login";
import { useUserStore } from "@/store/userStore";
import Utils from "@/utils";

/**
 * 无需token名单
 */
const anyAccessList = ["/adm/account/refresh-token", "/adm/account/login"];

// 创建axios实例
const service: AxiosInstance = axios.create({
  timeout: 2000,
  baseURL: import.meta.env?.VITE_API_BASE_URL,
});

// 添加请求拦截器
service.interceptors.request.use(
  function (config) {
    // 在发送请求之前做些什么
    let httpUrl: string = Utils.getPureUrl(JSON.parse(JSON.stringify(config.url)));
    if (anyAccessList.includes(httpUrl)) {
      return config;
    }
    const userStoreStr = localStorage.getItem("user");
    if (userStoreStr) {
      const localUserStore = JSON.parse(userStoreStr)["user"];
      const accessToken = localUserStore?.accessToken;
      if (accessToken) {
        const expiredTime = localUserStore?.expiredTime;
        //token过期，请求刷新token
        if (!dayjs(expiredTime).isAfter(new Date())) {
          const refreshToken = localUserStore?.refreshToken;
          if (refreshToken) {
            getAccessToken(refreshToken).then((res) => {
              if (res.code === 0) {
                const userStore = useUserStore();
                userStore.updateAccessToken(
                  res.data.accessToken,
                  res.data.refreshToken,
                  res.data.expiredTime
                );
              }
            });
          }
        }
        config.headers["Authorization"] = "Bearer " + accessToken;
      }
    }
    return config;
  },
  function (error) {
    // 对请求错误做些什么
    return Promise.reject(error);
  }
);

// 添加响应拦截器
service.interceptors.response.use(
  function (response: AxiosResponse<any, any>) {
    // 响应正确格式：{code:10000,message:''}；格式错误或code非10000都调用Promise.reject()
    if (response.status === 200) {
      // 存在code属性表示格式正确
      const code = response.data["code"] as number | undefined;
      if (code || code === 0) {
        if (code === 0) return response.data;
        else {
          if (response.data.message) ElMessage.error(response.data.message);
          else ElMessage.error("响应失败，状态码：" + code);
        }
      } else {
        ElMessage.error("响应数据格式错误");
      }
    }
    return Promise.reject(response);
  },
  function (error) {
    // 超出 2xx 范围的状态码都会触发该函数。
    // 对响应错误做点什么
    if (error?.response?.status) {
      let msg;
      switch (error.response.status) {
        case 401:
          msg = "身份信息已过期，请重新登录";
          break;
        case 403:
          msg = "您没有访问权限，请联系管理员";
          break;
        case 404:
          msg = "访问资源不存在，请联系管理员";
          break;
        case 500:
          msg = "服务器内部错误，请联系管理员";
          break;
        default:
          msg = "未知错误，状态码：" + error.response.status;
          break;
      }
      ElMessage.error(msg);
    }
    return Promise.reject(error);
  }
);

export default service;
