<template>
  <div>
    <el-config-provider :locale="locale">
      <el-container>
        <el-aside>
          <side-bar v-model="collapse" />
        </el-aside>
        <el-container>
          <el-header>
            <nav-bar @change-sidebar-status="changeSidebarStatus" />
          </el-header>
          <re-tab />
          <el-main>
            <router-view />
          </el-main>
        </el-container>
      </el-container>
    </el-config-provider>
  </div>
</template>

<script setup lang="ts">
import "./index.scss";
import SideBar from "./sidebar/index.vue";
import NavBar from "./navbar/index.vue";
import ReTab from "./re-tab/index.vue";
import { computed, onMounted, ref } from "vue";
import { getUserInfo, UserInfoData } from "@/api/login";
import { useUserStore } from "@/store/userStore";
import { useThemeStore } from "@/store/themeStore";
import zhCn from "element-plus/es/locale/lang/zh-cn";
import en from "element-plus/es/locale/lang/en";

const userStore = useUserStore();
const themeStore = useThemeStore();
const collapse = ref<boolean>(false);
const changeSidebarStatus = (_collapse: boolean) => {
  collapse.value = _collapse;
};
onMounted(() => {
  getUserInfo().then((infoRes) => {
    const userInfo = infoRes.data as UserInfoData;
    userStore.setAuthorization(userInfo.roles, userInfo.auths);
    userStore.setInfo({
      avatar: userInfo.avatar,
      sex: userInfo.sex,
      nickName: userInfo.nickName,
    });
  });
});

const locale = computed(() => (themeStore.language === "zh-cn" ? zhCn : en));
</script>
