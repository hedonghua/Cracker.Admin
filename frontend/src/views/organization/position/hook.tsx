import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref } from "vue";
import { getPositionGroupOptions } from "@/api/organization/positionGroup";
import {
  addPosition,
  getPositionList,
  updatePosition,
  deletePosition,
} from "@/api/organization/position";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import Utils from "@/utils";
import { cloneDeep } from "lodash";

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
      prop: "level",
      label: "职级",
    },
    {
      prop: "layerName",
      label: "分组",
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
      prop: "description",
      label: "描述",
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <div>
          <el-button
            v-if={userAuth.hasPermission("admin_system_position_update")}
            size="small"
            link
            type="primary"
            onclick={() => openDialog("编辑职位", row)}
          >
            编辑
          </el-button>
          <el-button
            v-if={userAuth.hasPermission("admin_system_position_delete")}
            size="small"
            link
            type="primary"
            onclick={() => remove(row)}
          >
            删除
          </el-button>
        </div>
      ),
    },
  ];
  const filters = [
    {
      type: "text",
      label: "编号",
      key: "code",
      placeholder: "请输入职位编号",
    },
    {
      type: "text",
      label: "名称",
      key: "name",
      placeholder: "请输入职位名称",
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
    level: 1,
    status: 1,
    description: null,
    groupId: null,
  });
  const rules = {
    name: [{ required: true, trigger: "blur", message: "职位名称不能为空" }],
    groupId: [{ required: true, trigger: "blur", message: "职位分组不能为空" }],
    level: [{ required: true, trigger: "blur", message: "职级不能为空" }],
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
      deletePosition(row.id).then((res) => {
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
    return getPositionList(params);
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
      editForm.level = row.level;
      editForm.status = row.status;
      editForm.description = row.description;
      editForm.groupId = row.groupId;
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
      level: 1,
      status: 1,
      description: null,
      groupId: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        const dto = cloneDeep(editForm);
        const groupIds = dto.groupId as any;
        if (
          groupIds instanceof Array &&
          groupIds.length &&
          groupIds.length > 0
        ) {
          dto.groupId = groupIds[groupIds.length - 1];
        }
        if (dialogTitle.value?.includes("新增")) {
          addPosition(dto)
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
          updatePosition(dto)
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
