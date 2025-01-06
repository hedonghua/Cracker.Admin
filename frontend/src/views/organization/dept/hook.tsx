import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref } from "vue";
import {
  getDeptList,
  addDept,
  updateDept,
  deleteDept,
  getDeptOptions,
} from "@/api/organization/dept";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import Utils from "@/utils";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "code",
      label: "编号",
    },
    {
      prop: "name",
      label: "名称",
    },
    {
      prop: "email",
      label: "邮箱",
    },
    {
      prop: "phone",
      label: "电话",
    },
    {
      prop: "status",
      label: "状态",
      render: (row: any) => {
        return row.status === 1 ? (
          <re-tag type="success" text="正常" />
        ) : (
          <re-tag type="danger" text="停用" />
        );
      },
    },
    {
      prop: "sort",
      label: "排序",
    },
    {
      prop: "description",
      label: "描述",
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <div>
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_dept_update"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openDialog("编辑部门", row)}
            >
              编辑
            </el-button>
          )}
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_dept_update"),
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
      label: "名称",
      key: "name",
      placeholder: "请输入部门名称",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    id: null,
    name: null,
    sort: 1,
    status: 1,
    curatorId: null,
    email: null,
    phone: null,
    parentId: null,
    description: null,
  });
  const rules = {
    name: [{ required: true, trigger: "blur", message: "部门名称不能为空" }],
  };
  const options = ref<Array<any>>([]);
  const loading = ref<boolean>(false);

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteDept(row.id).then((res) => {
        if (res.code === 0) {
          ElMessage.success("删除成功");
          tableRef?.value.refresh();
        } else {
          ElMessage.error(res.message ?? "删除失败");
        }
      });
    });
  };
  const request = async (params: any) => {
    const { data } = await getDeptList(params);
    return Utils.getTree(data, null, "parentId", "id", "sort");
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
      editForm.name = row.name;
      editForm.sort = row.sort;
      editForm.status = row.status;
      editForm.curatorId = row.curatorId;
      editForm.email = row.email;
      editForm.phone = row.phone;
      editForm.parentId = row.parentId;
      editForm.description = row.description;
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
      name: null,
      sort: 1,
      status: 1,
      curatorId: null,
      email: null,
      phone: null,
      parentId: null,
      description: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addDept(editForm)
            .then((res) => {
              loading.value = false;
              if (res.code === AppResponseStatusCode.SUCCESS) {
                ElMessage.success("新增成功");
                closeDialog();
                tableRef?.value.refresh();
                getOptions();
              } else {
                ElMessage.error(res.message ?? "新增失败");
              }
            })
            .catch(() => {
              loading.value = false;
            });
        } else {
          updateDept(editForm)
            .then((res) => {
              loading.value = false;
              if (res.code === 0) {
                ElMessage.success("编辑成功");
                closeDialog();
                tableRef?.value.refresh();
                getOptions();
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
  const getOptions = () => {
    getDeptOptions().then((res) => {
      options.value = Utils.getTree(res.data, null, "extra", "value", "sort");
    });
  };

  /*========================== Vue钩子 ========================== */
  onMounted(() => {
    getOptions();
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
    options,
  };
}
