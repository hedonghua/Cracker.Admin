<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="info" @click="goBack()" icon="Back" plain>返回</el-button>
                <el-button type="success" v-permission="'admin_system_positiongroup_add'" :loading="loading" icon="Check"
                    @click="submit()" plain>提交</el-button>
            </template>
        </re-table>
    </div>
</template>

<script setup lang="ts">
import { useTable } from "./hook.tsx"
import { onMounted } from "vue";

const {
    request,
    columns,
    filters,
    tableRef,
    loading,
    submit,
    goBack
} = useTable();

onMounted(() => {
})
</script>