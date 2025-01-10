<template>
  <div class="re-tab-wrapper flex justify-between" ref="tabRef">
    <!-- 菜单标签 -->
    <div class="h-full flex items-center relative">
      <el-tag
        class="mr-2"
        @click="jumpPage({ id: '', path: '/', title: '' })"
        :type="getActiveType(['/', HOME_PATH])"
        >首页</el-tag
      >
      <div class="flex" id="dynamicTabContainer">
        <div id="dynamicTabContent" class="flex">
          <el-tag
            closable
            class="mr-2"
            v-for="v in computedTabs"
            :key="v.id"
            @close="(e: any) => closeTab(e, v.id)"
            @click="jumpPage(v)"
            :type="getActiveType([v.path])"
          >
            {{ v.title }}
          </el-tag>
        </div>
      </div>
    </div>
    <!-- 操作按钮 -->
    <div class="h-full flex items-center">
      <div class="flex items-center">
        <el-tooltip content="左移">
          <el-button link icon="ArrowLeft" @click="leftMoveShowTab"></el-button>
        </el-tooltip>
        <el-divider direction="vertical" />
      </div>
      <div class="flex items-center">
        <el-tooltip content="右移">
          <el-button
            link
            icon="ArrowRight"
            @click="rightMoveShowTab"
          ></el-button>
        </el-tooltip>
        <el-divider direction="vertical" />
      </div>
      <el-tooltip content="刷新">
        <el-button link icon="Refresh" @click="doRefresh"></el-button
      ></el-tooltip>
      <el-divider direction="vertical" />
      <el-tooltip content="更多">
        <el-dropdown @command="handleCommand" trigger="click">
          <el-button link icon="ArrowDown"></el-button>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="closeCurrent"
                >关闭当前标签页</el-dropdown-item
              >
              <el-dropdown-item command="closeLeft"
                >关闭左侧标签页</el-dropdown-item
              >
              <el-dropdown-item command="closeRight"
                >关闭右侧标签页</el-dropdown-item
              >
              <el-dropdown-item command="closeOther"
                >关闭其它标签页</el-dropdown-item
              >
              <el-dropdown-item command="closeAll" divided
                >关闭全部标签页</el-dropdown-item
              >
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </el-tooltip>
    </div>
  </div>
</template>

<script setup lang="ts">
import "./index.scss";
import { useRoute } from "vue-router";
import { computed, onMounted, watch, ref } from "vue";
import { CloseTabType, TabModel } from "@/store/tabStore";
import { useTabManager } from "@/hooks/useTabManager";
import { HOME_PATH } from "@/consts";

const tabManager = useTabManager();
const route = useRoute();
const tabRef = ref<Element>();
const tabWrapperWidth = ref<number>(0);
let translateX = 0; // 初始平移量
let translateStep = 50; // 每次点击按钮时平移的步长
// dropdown下拉选择
const handleCommand = (cmd: string) => {
  switch (cmd) {
    case "closeCurrent":
      tabManager.close(CloseTabType.CURRENT);
      break;
    case "closeLeft":
      tabManager.close(CloseTabType.LEFT);
      break;
    case "closeRight":
      tabManager.close(CloseTabType.RIGHT);
      break;
    case "closeOther":
      tabManager.close(CloseTabType.OTHERS);
      break;
    case "closeAll":
      tabManager.close(CloseTabType.ALL);
      break;
  }
};
const jumpPage = (v: TabModel) => {
  tabManager.setActive(v);
};
const closeTab = (_: any, id: string) => {
  tabManager.close(CloseTabType.TARGET, id);
};
const leftMoveShowTab = () => {
  const content = document.getElementById("dynamicTabContent")!;
  translateX -= translateStep;
  // 限制最小平移量，防止内容完全移出视口
  translateX = Math.max(translateX, 0);
  content.style.transform = `translateX(${-translateX}px)`;
};
const rightMoveShowTab = () => {
  // 计算内容的最大可平移量，防止平移超过内容范围
  const content = document.getElementById("dynamicTabContent")!;
  const maxTranslate = content.scrollWidth - content.parentElement!.offsetWidth;
  translateX = Math.min(translateX + translateStep, maxTranslate);
  content.style.transform = `translateX(${-translateX}px)`;
};
const doRefresh = () => {
  window.location.reload();
};
const computedTabs = computed((): TabModel[] => {
  return tabManager.getDisplayTabs();
});
const getActiveType = (path: string[]): string => {
  for (let i = 0; i < path.length; i++) {
    if (path[i] === route.path) {
      return "primary";
    }
  }
  return "info";
};
onMounted(() => {
  const resizeObserver = new ResizeObserver((entries) => {
    // 处理大小变化的回调函数
    for (const entry of entries) {
      tabWrapperWidth.value = entry.target.clientWidth - 131 - 30 - 44 - 20;
    }
  });
  resizeObserver.observe(tabRef.value!);
});
watch(
  () => tabWrapperWidth.value,
  (val) => {
    if (val > 0) {
      document.getElementById("dynamicTabContainer")!.style.width = val + "px";
    }
  }
);
</script>
