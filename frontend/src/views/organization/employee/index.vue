<template>
    <div>
        <el-row :gutter="10">
            <el-col :span="6">
                <div class="re-card">
                    <el-input v-model="filterText" class="mb-4" placeholder="请输入部门名称" />
                    <el-tree ref="deptTreeRef" :filter-node-method="filterNode" :data="deptOptions"
                        @node-click="handleNodeClick" default-expand-all :expand-on-click-node="false" />
                </div>
            </el-col>
            <el-col :span="18">
                <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
                    ref="tableRef">
                    <template #toolbar>
                        <el-button type="primary" v-permission="'admin_system_employee_add'" :loading="loading"
                            icon="Plus" @click="openDialog('新增员工信息')" plain>新增</el-button>
                    </template>
                </re-table>
            </el-col>
        </el-row>
        <el-dialog v-model="dialogVisible" :title="dialogTitle" width="60%" :before-close="handleClose">
            <el-form :model="editForm" ref="editFormRef" label-width="100" :rules="rules">
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="name" label="姓名">
                            <el-input v-model="editForm.name" placeholder="请输入员工姓名" clearable />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="phone" label="电话">
                            <el-input v-model="editForm.phone" placeholder="请输入电话" clearable />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="idNo" label="身份证">
                            <el-input v-model="editForm.idNo" placeholder="请输入身份证" clearable />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="birthDay" label="生日">
                            <el-date-picker v-model="editForm.birthDay" type="date" placeholder="请选择生日"
                                format="YYYY-MM-DD" value-format="YYYY-MM-DD" />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="inTime" label="入职时间">
                            <el-date-picker v-model="editForm.inTime" type="date" placeholder="请选择入职时间"
                                format="YYYY-MM-DD" value-format="YYYY-MM-DD" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="deptId" label="所属部门">
                            <el-tree-select v-model="editForm.deptId" :data="options" :render-after-expand="false"
                                placeholder="请选择所属部门" show-checkbox :check-strictly="true" clearable />
                        </el-form-item>

                    </el-col>
                </el-row>
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="positionId" label="担任职位">
                            <el-cascader v-model="editForm.positionId" :options="positionOptions" placeholder="请选择担任职位"
                                style="width:100%" />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="email" label="邮箱">
                            <el-input type="email" v-model="editForm.email" placeholder="请输入邮箱" clearable />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="email" label="邮箱">
                            <el-input type="email" v-model="editForm.email" placeholder="请输入邮箱" clearable />
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="address" label="现住址">
                            <el-input v-model="editForm.address" placeholder="请输入现住址" clearable />
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="isOut" label="状态">
                            <el-radio-group v-model="editForm.isOut">
                                <el-radio :value="false">在职</el-radio>
                                <el-radio :value="true">离职</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12"> <el-form-item v-if="editForm.isOut" prop="outTime" label="离职时间">
                            <el-date-picker v-model="editForm.outTime" type="date" placeholder="请选择离职时间"
                                format="YYYY-MM-DD" value-format="YYYY-MM-DD" />
                        </el-form-item></el-col>
                </el-row>
                <el-row :gutter="10">
                    <el-col :span="12">
                        <el-form-item prop="frontIdNoUrl" label="身份证正面">
                            <el-upload v-model:file-list="fileList1" :action="uploadApiUrl" list-type="picture-card"
                                :limit="1" :on-success="(d) => uploadSuccess(d, 'frontIdNoUrl')"
                                :on-remove="() => uploadRemove('frontIdNoUrl')">
                                <el-icon>
                                    <Plus />
                                </el-icon>
                            </el-upload>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item prop="backIdNoUrl" label="身份证反面">
                            <el-upload v-model:file-list="fileList2" :action="uploadApiUrl" list-type="picture-card"
                                :limit="1" :on-success="(d) => uploadSuccess(d, 'backIdNoUrl')"
                                :on-remove="() => uploadRemove('backIdNoUrl')">
                                <el-icon>
                                    <Plus />
                                </el-icon>
                            </el-upload>
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
        <el-dialog v-model="detailsDialogVisible" title="员工详情" width="60%">
            <el-descriptions border column="3">
                <el-descriptions-item label="工号">{{ detailsRow['code'] }}</el-descriptions-item>
                <el-descriptions-item label="姓名">{{ detailsRow['name'] }}</el-descriptions-item>
                <el-descriptions-item label="性别">{{ detailsRow['sex'] === 1 ? '男' : '女' }}</el-descriptions-item>
                <el-descriptions-item label="生日">{{ dayjs(detailsRow['birthDay']).format("YYYY-MM-DD")
                    }}</el-descriptions-item>
                <el-descriptions-item label="状态">
                    <re-tag v-if="detailsRow['isOut']" type="danger" text="离职" />
                    <re-tag v-else type="success" text="在职" />
                </el-descriptions-item>
                <el-descriptions-item label="身份证">
                    {{ detailsRow['idNo'] }}
                </el-descriptions-item>
                <el-descriptions-item label="电话">
                    {{ detailsRow['phone'] }}
                </el-descriptions-item>
                <el-descriptions-item label="入职时间">
                    {{ dayjs(detailsRow['inTime']).format("YYYY-MM-DD") }}
                </el-descriptions-item>
                <el-descriptions-item label="离职时间">
                    {{ detailsRow['outTime'] ? dayjs(detailsRow['outTime']).format("YYYY-MM-DD") : '' }}
                </el-descriptions-item>
                <el-descriptions-item label="主部门">
                    {{ detailsRow['deptName'] }}
                </el-descriptions-item>
                <el-descriptions-item label="主职位">
                    {{ detailsRow['positionName'] }}
                </el-descriptions-item>
                <el-descriptions-item label="邮箱">
                    {{ detailsRow['email'] }}
                </el-descriptions-item>
                <el-descriptions-item label="现住址">
                    {{ detailsRow['address'] }}
                </el-descriptions-item>
                <el-descriptions-item label="身份证正面">
                    <re-image width="240px" height="151px" :src='detailsRow["frontIdNoUrl"]'
                        :previewList='[detailsRow["frontIdNoUrl"]]' fit="cover" />
                </el-descriptions-item>
                <el-descriptions-item label="身份证反面">
                    <re-image width="240px" height="151px" :src='detailsRow["backIdNoUrl"]'
                        :previewList='[detailsRow["backIdNoUrl"]]' fit="cover" />
                </el-descriptions-item>
            </el-descriptions>
            <template #footer>
                <div class="dialog-footer">
                    <el-button @click="detailsDialogVisible = false">关闭</el-button>
                </div>
            </template>
        </el-dialog>
    </div>
</template>

<script setup lang="ts">
import { useTable } from "./hook.tsx"
import { onMounted } from "vue";
import './index.scss'
import * as dayjs from 'dayjs'

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
    options,
    positionOptions,
    deptOptions,
    handleNodeClick,
    filterText,
    deptTreeRef,
    filterNode,
    uploadApiUrl,
    uploadSuccess,
    fileList1,
    fileList2,
    detailsDialogVisible,
    detailsRow,
    uploadRemove
} = useTable();

onMounted(() => {
})
</script>