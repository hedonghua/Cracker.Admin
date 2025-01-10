<template>
  <el-drawer
    v-model="drawer"
    title="个性设置"
    @close="drawerClose"
    append-to-body
    size="300px"
  >
    <!-- <p class="mb-4">颜色</p>
    <el-radio-group
      v-model="themeStore.color"
      @change="colorChange"
      class="w-full"
    >
      <el-radio-button label="浅色" value="light" />
      <el-radio-button label="深色" value="dark" />
      <el-radio-button label="自动" value="auto" />
    </el-radio-group> -->

    <p class="mb-4">主题色</p>
    <div class="grid grid-cols-4 gap-4 text-center text-xs">
      <div v-for="(item, index) in themeColors" :key="index">
        <div
          class="border border-solid border-gray-300 rounded-md p-1 hover:cursor-pointer"
          :style="{
            borderColor:
              themeStore.themeColor === item.color
                ? `${item.color} !important`
                : '',
          }"
          @click="($event) => themeColorChange(item.color, $event)"
        >
          <div v-if="item.color === 'custom'">
            <!-- <re-icon name="fluent:color-32-light" class="custom-picker" /> -->
            <el-color-picker v-model="customThemeColor" />
          </div>
          <div
            v-else
            class="color-span"
            :style="{ backgroundColor: item.color }"
          ></div>

          <p class="mt-2">{{ item.name }}</p>
        </div>
      </div>
    </div>

    <p class="mt-6">界面显示</p>
    <div class="text-sm w-full">
      <div class="flex justify-between mt-2 mb-1">
        <p class="flex items-center">Logo</p>
        <el-switch v-model="themeStore.pageSwitch.logo" />
      </div>
    </div>

    <el-divider />

    <div class="w-full">
      <el-button
        type="primary"
        @click="resetThemeConfig"
        icon="RefreshLeft"
        class="w-full"
        >恢复默认设置</el-button
      >
    </div>
  </el-drawer>
</template>

<script setup lang="ts">
import { useThemeStore, defaultTheme } from "@/store/themeStore";
import Utils from "@/utils";
import { ref, watch } from "vue";

const props = defineProps({
  drawer: {
    type: Boolean,
    default: false,
  },
});

const drawer = ref<boolean>(props.drawer);
const emit = defineEmits(["update:drawer"]);
const themeStore = useThemeStore();
const themeColors = [
  {
    color: "#409EFF",
    name: "默认",
  },
  {
    color: "#52c41a",
    name: "绿色",
  },
  {
    color: "#f5222d",
    name: "红色",
  },
  {
    color: "#fa541c",
    name: "橙色",
  },
  {
    color: "#722ed1",
    name: "紫色",
  },
  {
    color: "#344256",
    name: "灰色",
  },
  {
    color: "#0bd092",
    name: "浅绿色",
  },
  {
    color: "#efbd48",
    name: "淡黄色",
  },
  // {
  //   color: "custom",
  //   name: "自定义",
  // },
];
const customThemeColor = ref<string>("");

const drawerClose = () => {
  drawer.value = false;
  emit("update:drawer", drawer.value);
};

const toggleColorMode = () => {
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
};

// const colorChange = (color: any) => {
//   themeStore.setColor(color);
//   toggleColorMode();
// };

const updateElementThemeVar = (themeColor: string) => {
  // document.documentElement 是全局变量时
  const el = document.documentElement;
  // const el = document.getElementById('xxx')
  // 获取 css 变量
  getComputedStyle(el).getPropertyValue(`--el-color-primary`);
  // 设置 css 变量
  el.style.setProperty("--el-color-primary", themeColor);
};

const themeColorChange = (themeColor: string, _: any) => {
  themeStore.setThemeColor(themeColor);
  updateElementThemeVar(themeColor);
};

const resetThemeConfig = () => {
  themeStore.reset();
  updateElementThemeVar(defaultTheme.themeColor);
  toggleColorMode();
};

watch(
  () => props.drawer,
  (val) => {
    drawer.value = val;
  }
);
</script>

<style lang="scss">
.color-span {
  width: 20px;
  height: 20px;
  border-radius: 4px;
  margin: 3px auto;
}

.custom-picker {
  width: 20px;
  height: 20px;
  margin: 3px auto;
}

.center-text {
  display: flex;
  align-items: center;
  justify-content: center;
}

.el-color-picker {
  display: hidden !important;
}
</style>
