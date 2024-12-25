import { ReTableColumn } from "@/components/re-table/types";
import { reactive, ref, watch } from "vue";
import {
  getRoleList,
  deleteRole,
  addRole,
  updateRole,
  getRoleMenuIds,
  assignMenu,
} from "@/api/system/role";
import { getMenuOptions } from "@/api/system/menu";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { AppOptionNode } from "#/data";
import { useAuthorization } from "@/hooks/useAuthorization";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "index",
      width: "50px",
    },
    {
      prop: "roleName",
      label: "角色名",
    },
    {
      prop: "remark",
      label: "备注",
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <>
          {userAuth.hasPermission("admin_system_role_assignmenu") ? (
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openAssignMenuDialog("分配菜单", row)}
            >
              分配菜单
            </el-button>
          ) : (
            <></>
          )}
          {userAuth.hasPermission("admin_system_role_update") ? (
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openDialog("编辑角色", row)}
            >
              编辑
            </el-button>
          ) : (
            <></>
          )}
          {userAuth.hasPermission("admin_system_role_delete") ? (
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => remove(row)}
            >
              删除
            </el-button>
          ) : (
            <></>
          )}
        </>
      ),
    },
  ];
  const filters = [
    {
      type: "text",
      label: "角色名",
      key: "roleName",
      placeholder: "请输入角色名",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const assignDialogVisible = ref<boolean>(false);
  const assignEditFormRef = ref<FormInstance>();
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    id: null,
    roleName: null,
    remark: null,
  });
  const assignEditForm = reactive({
    roleId: null,
    menuIds: [],
  });
  const rules = {
    roleName: [{ required: true, trigger: "blur", message: "角色名不能为空" }],
  };
  const menuOptions = ref<Array<AppOptionNode>>([]);
  const loading = ref<boolean>(false);
  const filterText = ref<string>();
  const treeRef = ref<any>();

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteRole(row.id).then((res) => {
        if (res.code === 0) {
          ElMessage.success("删除成功");
          tableRef?.value.refresh();
        } else {
          ElMessage.error(res.message ?? "删除失败");
        }
      });
    });
  };
  const request = (params: any) => {
    return getRoleList(params);
  };
  const handleClose = (done: () => void) => {
    clearEditFormValues();
    done();
  };
  const openDialog = (title: string, row?: any) => {
    dialogTitle.value = title;
    dialogVisible.value = true;
    if (row && title.includes("编辑")) {
      editForm.id = row.id;
      editForm.roleName = row.roleName;
      editForm.remark = row.remark;
    }
  };
  const openAssignMenuDialog = (title: string, row?: any) => {
    if (row) {
      assignEditForm.roleId = row.id;
      getRoleMenuIds(row.id).then((res1) => {
        getMenuOptions().then((res2) => {
          dialogTitle.value = title;
          assignDialogVisible.value = true;
          assignEditForm.roleId = row.id;
          assignEditForm.menuIds = res1.data;

          menuOptions.value = res2.data;
        });
      });
    }
  };
  const closeDialog = () => {
    dialogVisible.value = false;
    clearEditFormValues();
  };
  const clearEditFormValues = () => {
    editFormRef?.value?.resetFields();
    Object.assign(editForm, {
      id: null,
      roleName: null,
      remark: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addRole(editForm)
            .then((res) => {
              loading.value = false;
              if (res.code === 0) {
                ElMessage.success("新增成功");
                closeDialog();
                tableRef?.value.refresh();
              } else {
                ElMessage.error(res.message ?? "新增失败");
              }
            })
            .catch(() => {
              loading.value = false;
            });
        } else {
          updateRole(editForm)
            .then((res) => {
              loading.value = false;
              if (res.code === 0) {
                ElMessage.success("编辑成功");
                closeDialog();
                tableRef?.value.refresh();
              } else {
                ElMessage.error(res.message ?? "编辑失败");
              }
            })
            .catch(() => {
              loading.value = false;
            });
        }
      }
    });
  };
  const assignCloseDialog = () => {
    assignDialogVisible.value = false;
    clearAssignEditFormValues();
  };
  const clearAssignEditFormValues = () => {
    assignEditFormRef?.value?.resetFields();
    Object.assign(editForm, {
      roleId: null,
      menuIds: [],
    });
  };
  const assignConfirmEvent = () => {
    const keys = treeRef.value?.getCheckedKeys();
    if (keys && keys.length > 0) {
      assignEditForm.menuIds = keys;
    }
    assignMenu(assignEditForm).then((res) => {
      if (res.code === AppResponseStatusCode.SUCCESS) {
        ElMessage.success("分配成功");
        assignCloseDialog();
      } else {
        ElMessage.error("分配失败");
      }
    });
  };
  const assignHandleClose = (done: () => void) => {
    clearAssignEditFormValues();
    done();
  };
  const filterNode = (value: string, data: any) => {
    if (!value) return true;
    return data.label.includes(value);
  };

  /*========================== Vue钩子 ========================== */
  watch(filterText, (val) => {
    treeRef.value!.filter(val);
  });
  return {
    request,
    columns,
    filters,
    tableRef,
    dialogVisible,
    handleClose,
    editForm,
    editFormRef,
    rules,
    openDialog,
    closeDialog,
    dialogTitle,
    confirmEvent,
    loading,
    assignDialogVisible,
    treeRef,
    assignConfirmEvent,
    assignHandleClose,
    assignCloseDialog,
    menuOptions,
    assignEditForm,
    filterText,
    filterNode,
  };
}
