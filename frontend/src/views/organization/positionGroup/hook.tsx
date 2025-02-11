import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref } from "vue";
import {
  getPositionGroupList,
  addPositionGroup,
  updatePositionGroup,
  deletePositionGroup,
  getPositionGroupOptions,
} from "@/api/organization/positionGroup";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import Utils from "@/utils/tool";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "groupName",
      label: "名称",
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
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_positiongroup_update"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openDialog("编辑职位", row)}
            >
              编辑
            </el-button>
          )}
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_positiongroup_delete"),
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
      key: "groupName",
      placeholder: "请输入分组名称",
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
    parentId: null,
    remark: null,
  });
  const rules = {
    groupName: [{ required: true, trigger: "blur", message: "名称不能为空" }],
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
      deletePositionGroup(row.id).then((res) => {
        if (res.code === 0) {
          ElMessage.success("删除成功");
          tableRef?.value.refresh();
          getOptions();
        } else {
          ElMessage.error(res.message ?? "删除失败");
        }
      });
    });
  };
  const request = async (params: any) => {
    const { data } = await getPositionGroupList(params);
    return Utils.getTree(data, null, "parentId", "id");
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
      editForm.parentId = row.parentId;
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
      parentId: null,
      remark: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addPositionGroup(editForm)
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
          updatePositionGroup(editForm)
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
    getPositionGroupOptions().then((res) => {
      options.value = Utils.getTree(res.data, null, "extra", "value");
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
