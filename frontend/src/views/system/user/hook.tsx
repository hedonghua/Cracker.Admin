import { ReTableColumn } from "@/components/re-table/types";
import { reactive, ref } from "vue";
import {
  getUserList,
  addUser,
  deleteUser,
  switchEnabledStatus,
  getUserRoleIds,
  assignRole,
} from "@/api/system/user";
import { getRoleOptions } from "@/api/system/role";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppOption } from "#/data";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import Utils from "@/utils";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "index",
      width: "50px",
    },
    {
      prop: "avatar",
      label: "头像",
      render: (row: any) => (
        <re-image
          width="40px"
          height="40px"
          src={getAvatar(row.avatar)}
          previewList={[getAvatar(row.avatar)]}
          roundedFull={true}
          fit="cover"
        />
      ),
    },
    {
      prop: "nickName",
      label: "昵称",
    },
    {
      prop: "userName",
      label: "用户名",
    },
    {
      prop: "sex",
      label: "性别",
      render: (row: any) => <span>{row.sex === 1 ? "男" : "女"}</span>,
    },
    {
      prop: "isEnabled",
      label: "是否启用",
      render: (row: any) => (
        <el-switch
          key={row.id}
          v-model={row.isEnabled}
          onChange={(val: boolean) => changeEnabledStatus(val, row)}
        />
      ),
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <div>
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_user_assignrole"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openAssignRoleDialog(row)}
            >
              分配角色
            </el-button>
          )}
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_user_delete"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => remove(row)}
            >
              删除
            </el-button>
          )}
        </div>
      ),
    },
  ];
  const filters = [
    {
      type: "text",
      label: "用户名",
      key: "username",
      placeholder: "请输入用户名",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    userName: null,
    password: null,
    nickName: null,
    sex: 1,
    isEnabled: true,
  });
  const rules = {
    userName: [{ required: true, trigger: "blur", message: "用户名不能为空" }],
    nickName: [{ required: true, trigger: "blur", message: "昵称不能为空" }],
    password: [{ required: true, trigger: "blur", message: "密码不能为空" }],
  };
  const loading = ref<boolean>(false);
  const assignDialogVisible = ref<boolean>(false);
  const assignEditFormRef = ref<FormInstance>();
  const assignEditForm = reactive({
    userId: "",
    roleIds: [],
  });
  const roleOptions = ref<Array<AppOption>>([]);
  const ossDomain = import.meta.env.VITE_OSS_DOMAIN;

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteUser(row.id).then((res) => {
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
    return getUserList(params);
  };
  const handleClose = (done: () => void) => {
    clearEditFormValues();
    done();
  };
  const assignHandleClose = (done: () => void) => {
    clearAssignEditFormValues();
    done();
  };
  const clearEditFormValues = () => {
    editFormRef?.value?.resetFields();
    Object.assign(editForm, {
      userName: null,
      password: null,
    });
  };
  const clearAssignEditFormValues = () => {
    assignEditFormRef?.value?.resetFields();
    Object.assign(editForm, {
      id: "",
      roleIds: [],
    });
  };
  const openDialog = (title: string) => {
    dialogTitle.value = title;
    dialogVisible.value = true;
  };
  const closeDialog = () => {
    dialogVisible.value = false;
    clearEditFormValues();
  };
  const assignCloseDialog = () => {
    assignDialogVisible.value = false;
    clearAssignEditFormValues();
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        addUser(editForm)
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
      }
    });
  };
  const changeEnabledStatus = (_: boolean, row: any) => {
    switchEnabledStatus(row.id).then((res) => {
      if (res.code === 0) {
        ElMessage.success("操作成功");
      } else {
        ElMessage.error(res.message ?? "操作失败");
      }
    });
  };
  const openAssignRoleDialog = (row: any) => {
    getUserRoleIds(row.id).then((res1) => {
      getRoleOptions().then((res2) => {
        dialogTitle.value = "分配角色";
        assignDialogVisible.value = true;
        assignEditForm.userId = row.id;
        assignEditForm.roleIds = res1.data;

        roleOptions.value = res2.data;
      });
    });
  };
  const assignConfirmEvent = () => {
    assignRole(assignEditForm).then((res) => {
      if (res.code === AppResponseStatusCode.SUCCESS) {
        ElMessage.success("分配成功");
        assignCloseDialog();
      } else {
        ElMessage.error("分配失败");
      }
    });
  };
  const getAvatar = (url: string) => {
    return ossDomain + url;
  };
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
    assignEditFormRef,
    assignEditForm,
    assignConfirmEvent,
    assignHandleClose,
    assignCloseDialog,
    roleOptions,
  };
}
