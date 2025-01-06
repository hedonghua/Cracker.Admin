<template>
  <div class="sidebar-wrapper">
    <div class="system-title">
      <a class="flex justify-center items-center text-white w-full h-full" href="/">
        <img :src="VueLogo" class="mr-2" alt="logo" />
        <span v-if="!model" class="font-bold">{{ APP_TITLE }}</span>
      </a>
    </div>
    <el-menu :default-active="computedActivePath" active-text-color="#ffffff" background-color="#17253b"
      text-color="#7c859f" class="custom-el-menu" :collapse="model" @select="menuSelected">
      <sidebar-item v-if="menus" v-for="(item, index) in menus" :key="index" :item="item" />
    </el-menu>
  </div>
</template>

<script lang="ts" setup>
import { computed, onMounted, ref, watch } from 'vue';
import { RouteRecordRaw, useRouter } from "vue-router";
import { useRouteCache } from '@/router/hook';
import './index.styl'
import SidebarItem from './sidebar-item.vue'
import VueLogo from "@/assets/vue.svg"
import { useTabManager } from '@/hooks/useTabManager';
import { HOME_PATH } from '@/consts'

const APP_TITLE = import.meta.env.VITE_APP_TITLE;
const model = defineModel()
const menus = ref<RouteRecordRaw[] | undefined>([])
const routesCache = useRouteCache();
const tabManager = useTabManager();
const router = useRouter();
const menuSelected = (index: string) => {
  tabManager.append(index);
};
onMounted(() => {
  const rawRoutes = routesCache.getTreeCache();
  menus.value = rawRoutes?.find(x => x.path === "/")?.children?.filter(x => !x.meta?.hidden && x.path !== HOME_PATH);
})
const computedActivePath = computed((): string => {
  if(router.currentRoute.value.path === HOME_PATH) return HOME_PATH;
  return tabManager.getActiveItem()?.path ?? HOME_PATH;
});
// 监听收缩状态
watch(() => model.value, (val) => {
  const elMenuDom = document.querySelector(".custom-el-menu")
  if (!elMenuDom) return;
  // true收缩，false展开
  if (val) {
    document.querySelector(".el-aside")?.classList?.add("re-el-menu--collapse");
    document.querySelector(".el-header")?.classList?.add("re-el-header-width");
    document.querySelector(".re-tab-wrapper")?.classList?.add("re-el-header-width");
  } else {
    document.querySelector(".el-aside")?.classList?.remove("re-el-menu--collapse");
    document.querySelector(".el-header")?.classList?.remove("re-el-header-width");
    document.querySelector(".re-tab-wrapper")?.classList?.remove("re-el-header-width");
  }
})
</script>