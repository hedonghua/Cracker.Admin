<template>
    <Icon v-if="name && name.indexOf(':') !== -1" :icon="name" />
    <el-icon v-else>
        <component :is="name" />
    </el-icon>
</template>

<script setup lang="ts">
import { Icon } from '@iconify/vue';
import { ref } from 'vue';

const props = defineProps<{
    name: string
}>()
const name = ref<string>(props.name)
</script>