<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef" @selection-change="selectionChange">
            <template #toolbar>
                <el-button type="primary" icon="Plus" plain @click="openDialog('新增菜单')" :loading="loading"
                    v-permission="'admin_system_menu_add'">新增</el-button>
                <el-button type="danger" v-permission="'admin_system_menu_delete'" icon="Delete"
                    :disabled="manyButtonDisabled" plain
                    @click="deleteBatch">批量删除</el-button>
            </template>
        </re-table>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="700" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="80" :rules="rules">
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="functionType" label="类型">
                            <el-radio-group v-model="editForm.functionType">
                                <el-radio :value="1">菜单</el-radio>
                                <el-radio :value="2">按钮</el-radio>
                            </el-radio-group>
                        </el-form-item>
                        <el-form-item prop="title" label="标题">
                            <el-input v-model="editForm.title" placeholder="请输入标题/显示名" clearable />
                        </el-form-item>
                        <el-form-item prop="path" label="路由">
                            <el-input v-model="editForm.path" placeholder="请输入路由" clearable />
                        </el-form-item>
                        <el-form-item prop="icon" label="图标">
                            <el-input v-model="editForm.icon" placeholder="请输入图标类名" clearable />
                        </el-form-item>
                        <el-form-item prop="parentId" label="上级">
                            <!-- <template #label>
                                <el-popover placement="top" effect="dark" trigger="hover" content="选择上级菜单">
                                    <template #reference>
                                        <el-icon class="mt-2">
                                            <InfoFilled />
                                        </el-icon>
                                    </template>
                                </el-popover>
                                上级
                            </template> -->
                            <el-tree-select v-model="editForm.parentId" :data="treeMenus" :render-after-expand="false"
                                placeholder="选择上级菜单" show-checkbox :check-strictly="true" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="hidden" label="隐藏">
                            <el-switch v-model="editForm.hidden" />
                        </el-form-item>
                        <el-form-item prop="name" label="组件名">
                            <el-input v-model="editForm.name" placeholder="请输入组件名" clearable />
                        </el-form-item>
                        <el-form-item prop="permission" label="授权码">
                            <el-input v-model="editForm.permission" placeholder="请输入授权码" clearable />
                        </el-form-item>
                        <el-form-item prop="sort" label="排序值">
                            <el-input-number v-model="editForm.sort" :min="1" :max="9999" />
                        </el-form-item>
                    </el-col>
                </el-row>
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
    treeMenus,
    deleteBatch,
    manyButtonDisabled,
    selectionChange
} = useTable();
onMounted(() => {
})
</script>