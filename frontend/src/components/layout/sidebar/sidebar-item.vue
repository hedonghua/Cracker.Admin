<script setup lang="ts">
import { RouteRecordRaw } from "vue-router";

defineProps<{ item: RouteRecordRaw }>();
</script>

<template>
  <el-sub-menu
    v-if="item.children && item.children.length > 0"
    :index="item.path"
  >
    <template #title>
      <re-icon
        v-if="item.meta?.icon"
        :name="item.meta?.icon as string"
        class="mr-2"
      />
      <span>{{ item.meta?.title }}</span>
    </template>
    <sidebar-item
      v-if="!item.meta?.hidden"
      v-for="(v, i) in item.children"
      :key="i"
      :item="v"
    ></sidebar-item>
  </el-sub-menu>
  <el-menu-item v-else-if="!item.meta?.hidden" :index="item.path">
    <re-icon
      v-if="item.meta?.icon"
      :name="item.meta?.icon as string"
      class="mr-2"
    />
    <span>{{ item.meta?.title }}</span>
  </el-menu-item>
</template>