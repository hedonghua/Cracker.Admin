/// <reference types="vite/client" />
declare module "*.vue" {
  import { DefineComponent } from "vue";
  const component: DefineComponent<{}, {}, any>;
  export default component;
}
// 声明导入图片
declare module "*.svg";
declare module "*.png";
declare module "*.jpg";
declare module "*.jpeg";