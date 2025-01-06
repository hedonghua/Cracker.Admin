<template>
  <el-image
    v-if="src"
    :style="getStyle()"
    :src="src"
    :preview-src-list="previewArray"
    fit="cover"
    :preview-teleported="true"
    @error="errorFunc"
  />
</template>

<script setup lang="ts">
import { computed } from "vue";

const props = withDefaults(
  defineProps<{
    width: string;
    height: string;
    rounded?: boolean;
    src: string;
    previewList?: string[];
  }>(),
  {
    rounded: false,
  }
);

const getStyle = () => {
  const style = { width: props.width, height: props.height };
  if (props.rounded) {
    style["borderRadius"] = "50%";
  }
  return style;
};
const errorFunc = () => {
  console.error(props.src);
};
const previewArray = computed((): string[] => {
  return [props.src];
});
</script>

<style></style>
