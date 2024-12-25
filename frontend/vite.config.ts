import { defineConfig, loadEnv } from "vite";
import { resolve } from "path";
import { viteMockServe } from "vite-plugin-mock";
import tailwindcss from "tailwindcss";
import autoprefixer from "autoprefixer";
import vue from "@vitejs/plugin-vue";
import vueJsx from "@vitejs/plugin-vue-jsx";

const pathResolve = (path: string): string => resolve(process.cwd(), path);

// https://vitejs.dev/config/
export default defineConfig(({ command, mode }) => {
  const env = loadEnv(mode, process.cwd(), "");
  return {
    // vite 配置
    define: {
      __APP_ENV__: JSON.stringify(env.APP_ENV),
    },
    plugins: [
      vue(),
      vueJsx(),
      viteMockServe({
        mockPath: "./mock", // mock文件存放的位置
        enable: mode === "mock", //在开发环境中启用 mock
      }),
    ],
    resolve: {
      alias: {
        "@": pathResolve("src"),
        "#": pathResolve("types"),
      },
    },
    css: {
      postcss: {
        plugins: [tailwindcss, autoprefixer],
      },
    },
    server: {
      proxy: {
        "/dev_api": {
          target: "http://localhost:44383",
          changeOrigin: true,
          rewrite: (path) => path.replace(/^\/dev_api/, ""),
        },
        "/prod_api": {
          target: "http://jsonplaceholder.typicode.com",
          changeOrigin: true,
          rewrite: (path) => path.replace(/^\/prod_api/, ""),
        },
      },
    },
    base: './',
  };
});
