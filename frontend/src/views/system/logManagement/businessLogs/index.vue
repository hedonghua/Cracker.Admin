<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" title="详情" width="700">
            <el-descriptions column="1">
                <el-descriptions-item label="路径：">{{ currentRow.url }}</el-descriptions-item>
                <el-descriptions-item label="系统：">{{ currentRow.os }}</el-descriptions-item>
                <el-descriptions-item label="浏览器：">{{ currentRow.browser }}</el-descriptions-item>
                <el-descriptions-item label="http方法：">{{ currentRow.httpMethod }}</el-descriptions-item>
                <el-descriptions-item label="操作方法：">{{ currentRow.action }}</el-descriptions-item>
                <el-descriptions-item label="操作结果：">{{ Utils.formatterNullableContent(currentRow.operationMsg)
                    }}</el-descriptions-item>
            </el-descriptions>
        </el-dialog>
    </div>
</template>

<script setup lang="ts">
import Utils from "@/utils";
import { useTable } from "./hook.tsx"

const {
    request,
    columns,
    filters,
    tableRef,
    dialogVisible,
    currentRow
} = useTable();
</script>