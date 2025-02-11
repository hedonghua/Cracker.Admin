import { ReTableColumn } from "@/components/re-table/types";
import { reactive, ref } from "vue";
import {
  getTenantList,
  addTenant,
  updateTenant,
  deleteTenant,
  getDecryptInfo,
} from "@/api/system/tenant";
import { Action, ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { useAuthorization } from "@/hooks/useAuthorization";
import utils from "@/utils/tool";
import _ from "lodash";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "name",
      label: "租户名称",
    },
    {
      prop: "connectionString",
      label: "连接字符串",
      render: (row: any) => {
        return (
          <div className="flex">
            <span
              className="hover:cursor-pointer mr-1"
              onclick={() => toggleShowConnectionString(row)}
            >
              {utils.renderJsx(
                Boolean(row.connectionStringDisplay),
                <re-icon key="1" name="Hide" />,
                <re-icon key="2" name="View" />
              )}
            </span>
            <span>********</span>
          </div>
        );
      },
    },
    {
      prop: "redisConnection",
      label: "redis连接",
      render: (row: any) => {
        return (
          <div className="flex">
            <span
              className="hover:cursor-pointer mr-1"
              onclick={() => toggleShowRedisConnection(row)}
            >
              {utils.renderJsx(
                Boolean(row.redisConnectionDisplay),
                <re-icon key="1" name="Hide" />,
                <re-icon key="2" name="View" />
              )}
            </span>
            <span>********</span>
          </div>
        );
      },
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
              onclick={() => openDialog("编辑租户", row)}
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
      label: "租户名称",
      key: "name",
      placeholder: "请输入租户名称",
    },
  ];
  const userAuth = useAuthorization();
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const dialogTitle = ref<string>();
  const editFormRef = ref<FormInstance>();
  const editForm = reactive({
    id: null,
    name: "",
    connectionString: "",
    redisConnection: "",
    remark: null,
  });
  const rules = {
    name: [{ required: true, trigger: "blur", message: "租户名称不能为空" }],
    connectionString: [
      { required: true, trigger: "blur", message: "连接字符串不能为空" },
    ],
    redisConnection: [
      { required: true, trigger: "blur", message: "redis连接不能为空" },
    ],
  };
  const loading = ref<boolean>(false);

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteTenant(row.id).then((res) => {
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
    return getTenantList(params);
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
      doGetDecryptInfo(row.id, "all", (data: string) => {
        const arr = data.split("||");
        editForm.connectionString = arr[0];
        editForm.redisConnection = arr[1];
      });
    }
  };
  const closeDialog = () => {
    dialogVisible.value = false;
    clearEditFormValues();
  };
  const clearEditFormValues = () => {
    editFormRef?.value?.resetFields();
    _.mapValues(editForm, () => null);
    editForm.name = "";
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        if (dialogTitle.value?.includes("新增")) {
          addTenant(editForm)
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
          updateTenant(editForm)
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

  const doGetDecryptInfo = (id: string, type: string, callback: Function) => {
    getDecryptInfo({
      tenantId: id,
      type: type,
    }).then((res) => {
      callback(res.data);
    });
  };

  const toggleShowConnectionString = (row: any) => {
    if (row.connectionStringDisplay) {
      delete row.connectionStringDisplay;
    } else {
      doGetDecryptInfo(row.id, "conn", (data: string) => {
        row.connectionStringDisplay = data;
        ElMessageBox.alert(
          `<div style="width:400px;" class="break-words">${row.connectionStringDisplay}</div>`,
          "连接字符串",
          {
            dangerouslyUseHTMLString: true,
            callback: (_: Action) => {
              delete row.connectionStringDisplay;
            },
          }
        );
      });
    }
  };

  const toggleShowRedisConnection = (row: any) => {
    if (row.redisConnectionDisplay) {
      delete row.redisConnectionDisplay;
    } else {
      doGetDecryptInfo(row.id, "redis", (data: string) => {
        row.redisConnectionDisplay = data;
        ElMessageBox.alert(
          `<div style="width:400px;" class="break-words">${row.redisConnectionDisplay}</div>`,
          "redis连接",
          {
            dangerouslyUseHTMLString: true,
            callback: (_: Action) => {
              delete row.redisConnectionDisplay;
            },
          }
        );
      });
    }
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
  };
}
