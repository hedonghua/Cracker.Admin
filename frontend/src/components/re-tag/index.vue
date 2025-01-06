<template>
    <span :class="getClass()">{{ text }}</span>
</template>

<script setup lang="ts">
import "./index.scss"

const props = defineProps<{
    type?: string;
    text: string;
    plain?: boolean;
    color?: string;
}>();

function getClass() {
    let className = "";
    if (props.type === "primary") className = "re-tag-primary";
    else if (props.type === "danger") className = "re-tag-danger";
    else if (props.type === "success") className = "re-tag-success";
    className += " re-tag";
    return className;
}
</script>