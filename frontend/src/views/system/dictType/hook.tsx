import { ReTableColumn } from "@/components/re-table/types";
import { reactive, ref } from "vue";
import {
  getDictTypeList,
  addDictType,
  updateDictType,
  deleteDictType,
} from "@/api/system/dictType";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { useAuthorization } from "@/hooks/useAuthorization";
import utils from "@/utils";
import _ from "lodash";
import { useTabManager } from "@/hooks/useTabManager";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "name",
      label: "字典名称",
    },
    {
      prop: "dictType",
      label: "字典类型",
    },
    {
      prop: "isEnabled",
      label: "是否启用",
      render: (row: any) =>
        utils.renderJsx(row.isEnabled, <span>启用</span>, <span>禁用</span>),
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
          <el-button
            size="small"
            link
            type="primary"
            onclick={() => openDetails(row)}
          >
            详情
          </el-button>
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
      label: "字典名称",
      key: "name",
      placeholder: "请输入字典名称",
    },
    {
      type: "text",
      label: "字典类型",
      key: "dictType",
      placeholder: "请输入字典类型",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    id: null,
    dictType: null,
    name: null,
    isEnabled: true,
    remark: null,
  });
  const rules = {
    name: [{ required: true, trigger: "blur", message: "字典名称不能为空" }],
    dictType: [
      { required: true, trigger: "blur", message: "字典类型不能为空" },
    ],
  };
  const loading = ref<boolean>(false);
  const tabManager = useTabManager();

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteDictType(row.dictType).then((res) => {
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
    return getDictTypeList(params);
  };
  const handleClose = (done: () => void) => {
    clearEditFormValues();
    done();
  };
  const openDialog = (title: string, row?: any) => {
    dialogTitle.value = title;
    dialogVisible.value = true;
    if (row && title.includes("编辑")) {
      _.merge(editForm, row);
    }
  };
  const closeDialog = () => {
    dialogVisible.value = false;
    clearEditFormValues();
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
          addDictType(editForm)
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
          updateDictType(editForm)
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

  const openDetails = (row: any) => {
    //动态参数，不能通过path找到title，需要手动传
    tabManager.append(
      "/system/dictData/" + row.dictType,
      "字典数据"
    );
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
    loading
  };
}
