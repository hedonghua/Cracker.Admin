import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref } from "vue";
import {
  getGenTableList,
  addGenTable,
  updateGenTable,
  deleteGenTable,
  getDatabaseTableList,
} from "@/api/develop/genTable";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import { useTabManager } from "@/hooks/useTabManager";
import _ from "lodash";
import utils from "@/utils";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "tableName",
      label: "表名",
    },
    {
      prop: "businessName",
      label: "业务名",
    },
    {
      prop: "entityName",
      label: "实体名",
    },
    {
      prop: "moduleName",
      label: "模块名",
    },
    {
      prop: "comment",
      label: "描述",
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <div>
          {utils.renderJsx(
            userAuth.hasPermission("admin_system_positiongroup_update"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openPreview(row)}
            >
              预览
            </el-button>
          )}
          <el-button
            size="small"
            link
            type="primary"
            onclick={() => openDetails(row)}
          >
            详情
          </el-button>
          {utils.renderJsx(
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
          {utils.renderJsx(
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
      label: "表名",
      key: "tableName",
      placeholder: "请输入表名",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const selectDialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    genTableId: null,
    tableName: null,
    businessName: null,
    entityName: null,
    moduleName: null,
    comment: null,
  });
  const rules = {
    tableName: [{ required: true, trigger: "blur", message: "表名不能为空" }],
    businessName: [
      { required: true, trigger: "blur", message: "业务名不能为空" },
    ],
    entityName: [
      { required: true, trigger: "blur", message: "实体名不能为空" },
    ],
    moduleName: [
      { required: true, trigger: "blur", message: "模块名不能为空" },
    ],
  };
  const dataTableList = ref<Array<any>>([]);
  const loading = ref<boolean>(false);
  const dataTableRef = ref();
  const dataTableQueryParams = reactive({
    page: 1,
    size: 10,
  });
  const dataTableColumns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "tableName",
      label: "表名",
    },
    {
      prop: "comment",
      label: "表描述",
    },
    {
      prop: "createTime",
      label: "创建时间",
    },
    {
      prop: "行数",
      label: "表行数",
    },
  ];
  const selectDatabataTableRows = ref<Array<any>>([]);
  const tabManager = useTabManager();
  const genTableId = ref<string>("");
  const previewDialogVisible = ref<boolean>(false);

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    deleteBatch([row.genTableId]);
  };
  const request = (params: any) => {
    return getGenTableList(params);
  };
  const handleClose = (done: () => void) => {
    clearEditFormValues();
    done();
  };
  const selectHandleClose = (done: () => void) => {
    done();
  };
  const openDialog = (title: string, row?: any) => {
    dialogTitle.value = title;
    dialogVisible.value = true;
    if (row && title.includes("编辑")) {
      _.merge(editForm, row);
    }
  };
  const openSelectDialog = () => {
    selectDialogVisible.value = true;
  };
  const closeDialog = () => {
    dialogVisible.value = false;
    clearEditFormValues();
  };
  const selectCloseDialog = () => {
    selectDialogVisible.value = false;
  };
  const clearEditFormValues = () => {
    editFormRef?.value?.resetFields();
    _.mapValues(editForm, () => null);
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addGenTable(editForm)
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
          updateGenTable(editForm)
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
  const selectConfirmEvent = () => {
    if (selectDatabataTableRows.value.length <= 0) {
      ElMessage.warning("请选择表");
      return;
    } else if (selectDatabataTableRows.value.length > 1) {
      ElMessage.warning("只能选择一个表");
      return;
    }
    selectDialogVisible.value = false;
    //打开新窗口
    const firstRow = selectDatabataTableRows.value[0];
    editForm.tableName = firstRow.tableName;
    editForm.entityName = firstRow.tableName;
    editForm.comment = firstRow.comment;
    openDialog("新增生成表");
  };
  const getOptions = () => {
    getDatabaseTableList(dataTableQueryParams).then((res) => {
      dataTableList.value = res.data.items;
    });
  };
  const deleteBatch = (ids: string[]) => {
    if (ids.length <= 0) {
      ElMessage.warning("请选择要删除的记录");
      return;
    }

    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteGenTable(ids).then((res) => {
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
  const openDetails = (row: any) => {
    //动态参数，不能通过path找到title，需要手动传
    tabManager.append(
      "/develop/genTableColumn/" + row.genTableId,
      "生成列配置"
    );
  };
  const openPreview = (row: any) => {
    genTableId.value = row.genTableId;
    previewDialogVisible.value = true;
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
    openSelectDialog,
    closeDialog,
    dialogTitle,
    confirmEvent,
    loading,
    dataTableList,
    dataTableRef,
    dataTableColumns,
    selectDialogVisible,
    selectHandleClose,
    selectCloseDialog,
    selectConfirmEvent,
    selectDatabataTableRows,
    deleteBatch,
    genTableId,
    previewDialogVisible,
  };
}
