<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
                <el-button type="primary" icon="Plus" :loading="loading" plain
                    @click="openDialog('新增角色')" v-permission="'admin_system_role_add'">新增</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="80" :rules="rules">
                <el-form-item prop="roleName" label="角色名">
                    <el-input v-model="editForm.roleName" placeholder="请输入角色名" clearable />
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
        <el-dialog v-model="assignDialogVisible" :title="dialogTitle" width="50%" :before-close="assignHandleClose">
            <el-input v-model="filterText" placeholder="请输入菜单名进行过滤" />

            <el-tree default-expand-all highlight-current check-strictly class="mt-1" node-key="value"
                :data="menuOptions" :filter-node-method="filterNode" show-checkbox ref="treeRef"
                :default-checked-keys="assignEditForm.menuIds">
                <template #default="{ node }">
                    <span class="custom-tree-node">
                        <span>{{ node.label }}</span>
                    </span>
                </template>
            </el-tree>
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
    treeRef,
    assignConfirmEvent,
    assignHandleClose,
    assignCloseDialog,
    menuOptions,
    assignEditForm,
    filterText,
    filterNode
} = useTable();
onMounted(() => {
})
</script>