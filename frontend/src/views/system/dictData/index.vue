<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef" @selection-change="selectionChange">
            <template #toolbar>
                <el-button type="primary" v-permission="'admin_system_dict_add'" :loading="loading" icon="Plus"
                    @click="openDialog('新增字典')" plain>新增</el-button>
                <el-button type="danger" v-permission="'admin_system_dict_delete'" icon="Delete"
                    :disabled="manyButtonDisabled" plain @click="deleteBatch">批量删除</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="500" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="80" :rules="rules">
                <el-form-item prop="key" label="字典键">
                    <el-input v-model="editForm.key" placeholder="请输入字典键" clearable />
                </el-form-item>
                <el-form-item prop="value" label="字典值">
                    <el-input v-model="editForm.value" placeholder="请输入字典值" clearable />
                </el-form-item>
                <el-form-item prop="label" label="显示文本">
                    <el-input v-model="editForm.label" placeholder="请输入显示文本" clearable />
                </el-form-item>
                <el-form-item prop="sort" label="排序">
                    <el-input-number v-model="editForm.sort" :min="0" :max="9999" />
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
    deleteBatch,
    selectionChange,
    manyButtonDisabled
} = useTable();

onMounted(() => {
})
</script>