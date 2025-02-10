<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="primary" v-permission="'admin_system_dict_add'" :loading="loading" icon="Plus"
                    @click="openDialog('新增字典')" plain>新增</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="110" :rules="rules">
                <el-form-item prop="name" label="租户名称">
                    <el-input v-model="editForm.name" placeholder="请输入租户名称" clearable />
                </el-form-item>
                <el-form-item prop="connectionString" label="连接字符串">
                    <el-input v-model="editForm.connectionString" placeholder="请输入连接字符串" clearable />
                </el-form-item>
                <el-form-item prop="redisConnection" label="redis连接">
                    <el-input v-model="editForm.redisConnection" placeholder="请输入redis连接" clearable />
                </el-form-item>
                <el-form-item prop="remark" label="备注">
                    <el-input v-model="editForm.remark" placeholder="请输入备注" clearable />
                </el-form-item>
            </el-form>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="closeDialog">取消</el-button>
                    <el-button type="primary" @click="confirmEvent">
                        确定
                    </el-button>
                </div>
            </template>
        </el-dialog>
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
    dialogVisible,
    handleClose,
    editForm,
    rules,
    openDialog,
    dialogTitle,
    closeDialog,
    confirmEvent,
    loading,
    editFormRef,
} = useTable();

onMounted(() => {
})
</script>