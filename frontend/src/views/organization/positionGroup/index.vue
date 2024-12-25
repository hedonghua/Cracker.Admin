<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="primary" v-permission="'admin_system_positiongroup_add'" :loading="loading" icon="Plus"
                    @click="openDialog('新增职位')" plain>新增</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="60" :rules="rules">
                <el-form-item prop="groupName" label="名称">
                    <el-input v-model="editForm.groupName" placeholder="请输入分组名称" clearable />
                </el-form-item>
                <el-form-item prop="parentId" label="分组">
                    <el-tree-select v-model="editForm.parentId" :data="options" :render-after-expand="false"
                    placeholder="选择职位分组" show-checkbox :check-strictly="true" clearable />
                </el-form-item>
                <el-form-item prop="remark" label="描述">
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
    options
} = useTable();

onMounted(() => {
})
</script>