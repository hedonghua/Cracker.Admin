<template>
  <el-config-provider :locale="locale" :size="themeStore.size">
    <router-view />
  </el-config-provider>
</template>

<script lang="ts" setup>
import { useThemeStore } from "@/store/themeStore";
import zhCn from "element-plus/es/locale/lang/zh-cn";
import en from "element-plus/es/locale/lang/en";
import { computed, onBeforeMount } from "vue";
import Utils from "./utils/tool";

const themeStore = useThemeStore();
const locale = computed(() => (themeStore.language === "zh-cn" ? zhCn : en));

onBeforeMount(() => {
  // document.documentElement 是全局变量时
  const el = document.documentElement;
  // const el = document.getElementById('xxx')
  // 获取 css 变量
  getComputedStyle(el).getPropertyValue(`--el-color-primary`);
  // 设置 css 变量
  el.style.setProperty("--el-color-primary", themeStore.themeColor);

  //颜色模式
  const html = document.querySelector("html");
  if (html) {
    if (Utils.isDark(themeStore.color)) {
      html.classList.remove("light");
      html.classList.add("dark");
    } else {
      html.classList.remove("dark");
      html.classList.add("light");
    }
  }
});
</script>

<style scoped></style>
