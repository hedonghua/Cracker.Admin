<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="primary" :loading="loading" icon="Plus" plain v-permission="'admin_system_user_add'"
                    @click="openDialog('新增用户')">新增</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="80" :rules="rules">
                <el-form-item prop="userName" label="账号">
                    <el-input v-model="editForm.userName" placeholder="请输入用户名" clearable />
                </el-form-item>
                <el-form-item prop="password" label="密码">
                    <el-input type="password" v-model="editForm.password" placeholder="请输入密码" clearable />
                </el-form-item>
                <el-form-item prop="nickName" label="昵称">
                    <el-input v-model="editForm.nickName" placeholder="请输入昵称" clearable />
                </el-form-item>
                <el-form-item prop="sex" label="性别">
                    <el-radio-group v-model="editForm.sex">
                        <el-radio :value="1">男</el-radio>
                        <el-radio :value="0">女</el-radio>
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
        <el-dialog v-model="assignDialogVisible" :title="dialogTitle" width="500" :before-close="assignHandleClose">
            <el-form :model="assignEditForm" ref="assignEditFormRef" :rules="rules">
                <el-form-item prop="roleIds">
                    <el-checkbox-group v-model="assignEditForm.roleIds">
                        <el-checkbox v-for="v in roleOptions" :key="v.value" :label="v.label" :value="v.value" border />
                    </el-checkbox-group>
                </el-form-item>
            </el-form>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="assignCloseDialog">取消</el-button>
                    <el-button type="primary" @click="assignConfirmEvent">
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
    assignDialogVisible,
    assignEditForm,
    assignEditFormRef,
    assignConfirmEvent,
    assignHandleClose,
    assignCloseDialog,
    roleOptions
} = useTable();
onMounted(() => {
})
</script>