<template>
  <div class="navbar-wrapper flex items-center">
    <!-- 侧边菜单收缩切换按钮 -->
    <div class="btn-toggle-wrapper h-full" @click="toggleSidebar">
      <span class="btn-toggle">
        <re-icon v-if="collapse" name="ant-design:menu-unfold-outlined" />
        <re-icon v-else name="ant-design:menu-fold-outlined" />
      </span>
    </div>
    <ul class="w-full list-none flex flex-row-reverse items-center">
      <!-- 用户头像，昵称 -->
      <li style="padding: 0;">
        <el-dropdown @command="handleCommand" class="w-full h-full px-2.5 border-none" trigger="click">
          <div class="flex">
            <div class="flex items-center">
              <img v-if="userStore.info?.avatar" :src="ossDomain + userStore.info?.avatar" alt="头像" />
              <img v-else :src="UserBoyAvatar" alt="头像" />
            </div>
            <div class="flex items-center ml-2">{{ userStore.user?.username }}</div>
          </div>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="baseInfo">基本资料</el-dropdown-item>
              <el-dropdown-item command="signOut">退出系统</el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </li>
      <!-- 全屏 -->
      <li @click="toggleFullScreen">
        <el-tooltip content="全屏">
          <el-icon>
            <FullScreen />
          </el-icon>
        </el-tooltip>
      </li>
      <!-- 搜索 -->
      <li @click="openSearchDialog">
        <el-tooltip content="搜索">
          <re-icon name="Search" />
        </el-tooltip>
      </li>
    </ul>

    <!--菜单搜索-->
    <el-dialog v-model="dialogVisible" title="菜单搜索" width="450" :append-to-body='true'>
      <el-input v-model="filterText" placeholder="请输入菜单名进行过滤" clearable />
      <ul class="mt-2 max-h-60 overflow-auto">
        <li class="cursor-pointer py-1 px-2 hover:bg-slate-100" v-for="v, i in computedMenus" :key="i"
          @click="toPage(v)">
          {{ v.meta?.title }}
        </li>
      </ul>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import "./index.styl"
import { useRouter } from "vue-router"
import { computed, onMounted, ref, watch } from "vue";
import { useTabManager } from "@/hooks/useTabManager";
import { useUserStore } from "@/store/userStore";
import { useAuthorization } from "@/hooks/useAuthorization";
import UserBoyAvatar from "@/assets/img/boy.png"
import { getSidebarMenus } from "@/api/system/menu";

const ossDomain = import.meta.env.VITE_OSS_DOMAIN;
const userStore = useUserStore();
const tabManager = useTabManager();
const collapse = ref<boolean>(false);
const emits = defineEmits(['changeSidebarStatus'])
const screenWidth = ref(window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth)
const router = useRouter();
const userAuth = useAuthorization();
// 切换全屏
const toggleFullScreen = () => {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen();
  } else {
    if (document.exitFullscreen) {
      document.exitFullscreen();
    }
  }
}
// dropdown下拉选择
const handleCommand = (cmd: string) => {
  switch (cmd) {
    case "signOut":
      userAuth.signOut();
      router.replace("/login")
      break;
    case "baseInfo":
      const personRoute = router.getRoutes().find(x => x.path === '/person');
      if (personRoute) {
        tabManager.append('/person');
      }
      break;
    default:
      break;
  }
}
// 切换侧边栏
const toggleSidebar = () => {
  collapse.value = !collapse.value;
  emits("changeSidebarStatus", collapse.value)
}

onMounted(() => {
  // 内置函数监听窗口变化
  window.onresize = () => {
    return (() => {
      screenWidth.value = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth
    })()
  }
})
// 监听窗口宽度，小于1024px自动收缩侧边栏
watch(() => screenWidth.value, (val) => {
  collapse.value = val < 1024;
  emits("changeSidebarStatus", collapse.value)
})

//菜单搜索
const dialogVisible = ref<boolean>(false);
const menus = ref<Array<any>>([]);
const filterText = ref<string>('');
const openSearchDialog = () => {
  getSidebarMenus({ listStruct: 1 }).then(res => {
    dialogVisible.value = true;
    menus.value = res.data;
  })
}
const computedMenus = computed((): Array<any> => {
  if (!menus.value || menus.value.length === 0) return [];
  return menus.value.filter(x => x.meta?.title?.indexOf(filterText.value) !== -1);
});
const toPage = (v: any) => {
  tabManager.append(v.path);
  dialogVisible.value = false;
}
</script>