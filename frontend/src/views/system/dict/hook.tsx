import { ReTableColumn } from "@/components/re-table/types";
import { reactive, ref } from "vue";
import {
  getDictList,
  addDict,
  updateDict,
  deleteDict,
  refreshDict,
} from "@/api/system/dict";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import utils from "@/utils";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "groupName",
      label: "组",
    },
    {
      prop: "key",
      label: "键",
    },
    {
      prop: "value",
      label: "值",
    },
    {
      prop: "label",
      label: "显示文本",
    },
    {
      prop: "sort",
      label: "排序",
    },
    {
      prop: "remark",
      label: "备注",
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <div>
          {utils.renderJsx(
            userAuth.hasPermission("admin_system_dict_update"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openDialog("编辑字典", row)}
            >
              编辑
            </el-button>
          )}
          {utils.renderJsx(
            userAuth.hasPermission("admin_system_dict_delete"),
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
      label: "组名",
      key: "groupName",
      placeholder: "请输入分组名",
    },
    {
      type: "text",
      label: "键",
      key: "key",
      placeholder: "请输入字典键",
    },
    {
      type: "text",
      label: "值",
      key: "value",
      placeholder: "请输入字典值",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    id: null,
    groupName: null,
    key: null,
    value: null,
    sort: 0,
    label: null,
    remark: null,
  });
  const rules = {
    key: [{ required: true, trigger: "blur", message: "字典键不能为空" }],
    value: [{ required: true, trigger: "blur", message: "字典值不能为空" }],
  };
  const loading = ref<boolean>(false);
  const manyButtonDisabled = ref<boolean>(true);
  const selectRows = ref<Array<any>>([]);

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteDict([row.id]).then((res) => {
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
    return getDictList(params);
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
      editForm.groupName = row.groupName;
      editForm.key = row.key;
      editForm.value = row.value;
      editForm.label = row.label;
      editForm.sort = row.sort;
      editForm.remark = row.remark;
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
      groupName: null,
      key: null,
      value: null,
      sort: 0,
      label: null,
      remark: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addDict(editForm)
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
          updateDict(editForm)
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
  const forceRefreshDict = () => {
    ElMessageBox.confirm("此操作将刷新字典缓存，确定继续？", "敏感操作", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      loading.value = true;
      refreshDict().then((res) => {
        loading.value = false;
        if (res.code === AppResponseStatusCode.SUCCESS) {
          ElMessage.success("刷新成功");
        } else {
          ElMessage.error("刷新失败");
        }
      });
    });
  };
  const selectionChange = (rows: any[]) => {
    manyButtonDisabled.value = !(rows && rows.length > 0);
    if (!manyButtonDisabled.value) {
      selectRows.value = rows;
    }
  };
  const deleteBatch = () => {
    if (selectRows.value.length === 0) {
      ElMessage.info("请选择一行操作");
      return;
    }
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      const ids = selectRows.value.map((x) => x.id);
      deleteDict(ids).then((res) => {
        if (res.code === 0) {
          ElMessage.success("删除成功");
          tableRef?.value.refresh();
        } else {
          ElMessage.error(res.message ?? "删除失败");
        }
      });
    });
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
    forceRefreshDict,
    deleteBatch,
    selectionChange,
    manyButtonDisabled,
  };
}
