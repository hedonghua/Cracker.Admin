<template>
    <div>
        <el-result title="禁止访问" sub-title="抱歉，页面请求失败">
            <template #icon>
                <el-image :src="ForBidden" style="width:280px" />
            </template>
            <template #extra>
                <el-button type="primary" @click="back">返回上一页</el-button>
            </template>
        </el-result>
    </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router';
import ForBidden from "@/assets/img/403.svg"

const router = useRouter();
const back = () => {
    router.go(-1);
}
</script>