<template>
  <div>
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
  </div>
</template>

<script setup lang="ts">
import "./index.scss";
import SideBar from "./sidebar/index.vue";
import NavBar from "./navbar/index.vue";
import ReTab from "./re-tab/index.vue";
import { onMounted, ref } from "vue";
import { getUserInfo, UserInfoData } from "@/api/login";
import { useUserStore } from "@/store/userStore";
import { useMqtt } from "@/hooks/useMqtt";

const userStore = useUserStore();
const mqtt = useMqtt();
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
    mqtt.connect();
  });
});
</script>
