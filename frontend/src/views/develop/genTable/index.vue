<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef" @selection-change="handleSelectionChange">
            <template #toolbar>
                <el-button type="primary" v-permission="'admin_system_positiongroup_add'" :loading="loading" icon="Plus"
                    @click="openSelectDialog()" plain>新增</el-button>
                <el-button type="danger" v-permission="'admin_system_dict_delete'" icon="Delete"
                    :disabled="manyButtonDisabled" plain @click="() => deleteBatch(ids)">批量删除</el-button>
            </template>
        </re-table>
        <el-dialog v-model="selectDialogVisible" title="选择数据库表" width="60%" :before-close="selectHandleClose">
            <re-table row-key="tableName" :data="dataTableList" :columns="dataTableColumns"
                @selection-change="selectionChange" ref="dataTableRef">
            </re-table>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="selectCloseDialog">取消</el-button>
                    <el-button type="primary" @click="selectConfirmEvent">
                        确定
                    </el-button>
                </div>
            </template>
        </el-dialog>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="60%" :before-close="handleClose">
            <el-form ref="editFormRef" :model="editForm" :rules="rules" label-width="120px">
                <el-form-item label="表名" prop="tableName">
                    <el-input v-model="editForm.tableName" placeholder="请输入表名" />
                </el-form-item>
                <el-form-item label="业务名" prop="businessName">
                    <el-input v-model="editForm.businessName" placeholder="请输入业务名" />
                </el-form-item>
                <el-form-item label="实体名" prop="entityName">
                    <el-input v-model="editForm.entityName" placeholder="请输入实体名" />
                </el-form-item>
                <el-form-item label="模块名" prop="moduleName">
                    <el-input v-model="editForm.moduleName" placeholder="请输入模块名" />
                </el-form-item>
                <el-form-item label="描述信息" prop="comment">
                    <el-input v-model="editForm.comment" placeholder="请输入描述信息" />
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
import { onMounted, ref } from "vue";

const ids = ref<Array<string>>([]);
const selectionChange = (rows: any[]) => {
    selectDatabataTableRows.value = rows
}
const manyButtonDisabled = ref<Boolean>(true);
const handleSelectionChange = (rows: any[]) => {
    manyButtonDisabled.value = !(rows && rows.length > 0);
    if (!manyButtonDisabled.value) {
        ids.value = rows.map((item) => item.genTableId);
    }
}

const {
    request,
    columns,
    filters,
    tableRef,
    dialogVisible,
    handleClose,
    editForm,
    rules,
    openSelectDialog,
    dialogTitle,
    closeDialog,
    confirmEvent,
    loading,
    editFormRef,
    dataTableList,
    dataTableRef,
    dataTableColumns,
    selectDialogVisible,
    selectHandleClose,
    selectDatabataTableRows,
    selectCloseDialog,
    selectConfirmEvent,
    deleteBatch
} = useTable();

onMounted(() => {
})
</script>