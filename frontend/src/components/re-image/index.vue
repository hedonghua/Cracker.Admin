<template>
    <el-image v-if="src" :style="getStyle()" :src="src" :preview-src-list="previewList" fit="cover"
        :preview-teleported="true" @error="errorFunc" />
</template>

<script setup lang="ts">
const props = defineProps<{
    width: string;
    height: string;
    roundedFull: boolean;
    src: string;
    previewList: string[];
}>();

const getStyle = () => {
    const style = { width: props.width, height: props.height };
    if (props.roundedFull) {
        style['borderRadius'] = "50%";
    }
    return style;
}
const errorFunc = () => {
    console.log(props.src)
}
</script>

<style></style>