import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref } from "vue";
import {
  getMenuList,
  addMenu,
  updateMenu,
  deleteMenu,
  getMenuOptions,
} from "@/api/system/menu";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
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
      label: "标题",
      prop: "title",
    },
    {
      label: "路由/地址",
      prop: "path",
    },
    {
      label: "组件名",
      prop: "name",
    },
    {
      label: "授权码",
      prop: "permission",
    },
    {
      label: "图标",
      render: (row: any) => {
        Utils.renderJsx(row.icon, <re-icon name={row.icon} />);
      },
    },
    {
      label: "排序",
      prop: "sort",
    },
    {
      label: "隐藏",
      prop: "hidden",
      render: (row: any) => (row.hidden ? <span>是</span> : <span>否</span>),
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <div>
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_menu_update"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openDialog("编辑菜单", row)}
            >
              编辑
            </el-button>
          )}
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_menu_delete"),
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
      label: "标题",
      key: "title",
      placeholder: "请输入菜单标题",
    },
    {
      type: "text",
      label: "路由",
      key: "path",
      placeholder: "请输入路由/地址",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const treeMenus = ref<Array<any>>([]);
  const editForm = reactive({
    id: null,
    title: null,
    name: null,
    icon: null,
    path: null,
    functionType: 1,
    permission: null,
    sort: 0,
    hidden: false,
    parentId: null,
  });
  const rules = {
    title: [{ required: true, trigger: "blur", message: "标题不能为空" }],
    functionType: [
      { required: true, trigger: "blur", message: "类型不能为空" },
    ],
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
      deleteMenu([row.id]).then((res) => {
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
    return getMenuList(params);
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
      editForm.title = row.title;
      editForm.name = row.name;
      editForm.icon = row.icon;
      editForm.path = row.path;
      editForm.functionType = row.functionType;
      editForm.permission = row.permission;
      editForm.sort = row.sort;
      editForm.hidden = row.hidden;
      editForm.parentId = row.parentId;
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
      title: null,
      name: null,
      icon: null,
      path: null,
      functionType: 1,
      permission: null,
      sort: 0,
      hidden: false,
      parentId: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addMenu(editForm)
            .then((res) => {
              loading.value = false;
              if (res.code === 0) {
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
          updateMenu(editForm)
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
      deleteMenu(ids).then((res) => {
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
  const getOptions = () => {
    getMenuOptions().then((res) => {
      treeMenus.value = res.data;
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
    treeMenus,
    deleteBatch,
    manyButtonDisabled,
    selectionChange,
  };
}
