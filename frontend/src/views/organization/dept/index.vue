<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="primary" v-permission="'admin_system_dept_add'" :loading="loading" icon="Plus"
                    @click="openDialog('新增部门')" plain>新增</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="60" :rules="rules">
                <el-form-item prop="name" label="名称">
                    <el-input v-model="editForm.name" placeholder="请输入部门名称" clearable />
                </el-form-item>
                <el-form-item prop="parentId" label="上级">
                    <el-tree-select v-model="editForm.parentId" :data="options" :render-after-expand="false"
                        placeholder="选择部门上级" show-checkbox :check-strictly="true" clearable />
                </el-form-item>
                <el-form-item prop="email" label="邮箱">
                    <el-input v-model="editForm.email" placeholder="请输入邮箱" clearable />
                </el-form-item>
                <el-form-item prop="phone" label="电话">
                    <el-input v-model="editForm.phone" placeholder="请输入电话" clearable />
                </el-form-item>
                <el-form-item prop="remark" label="描述">
                    <el-input v-model="editForm.description" placeholder="请输入描述" clearable />
                </el-form-item>
                <el-form-item prop="status" label="状态">
                    <el-radio-group v-model="editForm.status">
                        <el-radio :value="1">正常</el-radio>
                        <el-radio :value="2">停用</el-radio>
                    </el-radio-group>
                </el-form-item>
                <el-form-item prop="sort" label="排序值">
                    <el-input-number v-model="editForm.sort" :min="1" :max="9999" />
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