<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="primary" v-permission="'admin_system_position_add'" :loading="loading" icon="Plus"
                    @click="openDialog('新增职位')" plain>新增</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="60" :rules="rules">
                <el-form-item prop="name" label="名称">
                    <el-input v-model="editForm.name" placeholder="请输入职位名称" clearable />
                </el-form-item>
                <el-form-item prop="groupId" label="分组">
                    <el-cascader v-model="editForm.groupId" :options="options" placeholder="选择职位分组" />
                </el-form-item>
                <el-form-item prop="level" label="职级">
                    <el-input-number v-model="editForm.level" :min="1" :max="9999" />
                </el-form-item>
                <el-form-item prop="description" label="描述">
                    <el-input v-model="editForm.description" placeholder="请输入职位描述" clearable />
                </el-form-item>
                <el-form-item prop="status" label="状态">
                    <el-radio-group v-model="editForm.status">
                        <el-radio :value="1">正常</el-radio>
                        <el-radio :value="2">停用</el-radio>
                    </el-radio-group>
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