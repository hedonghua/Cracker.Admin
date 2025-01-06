import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref, watch } from "vue";
import {
  getEmployeeList,
  addEmployee,
  updateEmployee,
  deleteEmployee,
} from "@/api/organization/employee";
import { getDeptOptions } from "@/api/organization/dept";
import { getPositionOptions } from "@/api/organization/position";
import { ElMessage, ElMessageBox, ElTree, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import Utils from "@/utils";
import { assign, cloneDeep } from "lodash";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "code",
      label: "工号",
    },
    {
      prop: "name",
      label: "姓名",
    },
    {
      prop: "deptName",
      label: "主部门",
    },
    {
      prop: "positionName",
      label: "主职位",
    },
    {
      prop: "sex",
      label: "性别",
      render: (row: any) => {
        const sex = row.sex === 1 ? "男" : "女";
        return <span>{sex}</span>;
      },
    },
    {
      prop: "phone",
      label: "电话",
    },
    {
      prop: "birthDay",
      label: "生日",
      isDate: true,
    },
    {
      prop: "isOut",
      label: "状态",
      render: (row: any) => {
        return row.isOut ? (
          <re-tag type="danger" text="离职" />
        ) : (
          <re-tag type="success" text="在职" />
        );
      },
    },
    {
      fixed: "right",
      label: "操作",
      width: 180,
      render: (row: any) => (
        <div>
          <el-button
            size="small"
            link
            type="primary"
            onclick={() => openDetailsDialog(row)}
          >
            详情
          </el-button>
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_employee_update"),
            <el-button
              size="small"
              link
              type="primary"
              onclick={() => openDialog("编辑员工信息", row)}
            >
              编辑
            </el-button>
          )}
          {Utils.renderJsx(
            userAuth.hasPermission("admin_system_employee_delete"),
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
      label: "关键字",
      key: "keyword",
      placeholder: "请输入姓名/工号/电话",
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
    sex: 1,
    phone: null,
    idNo: null,
    frontIdNoUrl: null,
    backIdNoUrl: null,
    birthDay: null,
    address: null,
    email: null,
    inTime: null,
    outTime: null,
    isOut: false,
    userId: null,
    deptId: null,
    positionId: null,
  });
  const rules = {
    name: [{ required: true, trigger: "blur", message: "姓名不能为空" }],
    sex: [{ required: true, trigger: "blur", message: "性别不能为空" }],
    birthDay: [{ required: true, trigger: "blur", message: "生日不能为空" }],
    phone: [
      { required: true, trigger: "blur", message: "电话不能为空" },
      {
        pattern: Utils.PHONE_PATTERN,
        message: "电话格式错误",
        trigger: "blur",
      },
    ],
    idNo: [
      { required: true, trigger: "blur", message: "身份证不能为空" },
      {
        pattern: Utils.IDNO_PATTERN,
        message: "身份证格式错误",
        trigger: "blur",
      },
    ],
    inTime: [{ required: true, trigger: "blur", message: "入职时间不能为空" }],
    deptId: [{ required: true, trigger: "blur", message: "所属部门不能为空" }],
    positionId: [
      { required: true, trigger: "blur", message: "担任职位不能为空" },
    ],
  };
  const options = ref<Array<any>>([]);
  const deptOptions = ref<Array<any>>([]);
  const positionOptions = ref<Array<any>>([]);
  const loading = ref<boolean>(false);
  const filterText = ref<string>();
  const deptTreeRef = ref<InstanceType<typeof ElTree>>();
  const uploadApiUrl = import.meta.env.VITE_UPLOAD_API;
  const ossDomain = import.meta.env.VITE_OSS_DOMAIN;
  const fileList1 = ref<Array<any>>([]);
  const fileList2 = ref<Array<any>>([]);
  const detailsDialogVisible = ref<boolean>(false);
  const detailsRow = ref<any>();

  /*========================== 自定义函数 ========================== */
  const remove = (row: any) => {
    ElMessageBox.confirm("删除后无法恢复，是否继续？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      deleteEmployee(row.id).then((res) => {
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
    return getEmployeeList(params);
  };
  const handleClose = (done: () => void) => {
    clearEditFormValues();
    done();
  };
  const openDialog = (title: string, row?: any) => {
    dialogTitle.value = title;
    dialogVisible.value = true;
    if (row && title.includes("编辑")) {
      assign(editForm, row);
      if (row.frontIdNoUrl) {
        fileList1.value = [{ url: ossDomain + row.frontIdNoUrl }];
      }
      if (row.backIdNoUrl) {
        fileList2.value = [{ url: ossDomain + row.backIdNoUrl }];
      }
    }
    getOptions();
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
      sex: 1,
      phone: null,
      idNo: null,
      frontIdNoUrl: null,
      backIdNoUrl: null,
      birthDay: null,
      address: null,
      email: null,
      inTime: null,
      outTime: null,
      isOut: false,
      userId: null,
      deptId: null,
      positionId: null,
    });
  };
  const confirmEvent = () => {
    editFormRef.value?.validate((valid: any) => {
      if (valid) {
        loading.value = true;
        const dto = cloneDeep(editForm);
        const positionIds = dto.positionId as any;
        if (
          positionIds instanceof Array &&
          positionIds.length &&
          positionIds.length > 0
        ) {
          dto.positionId = positionIds[positionIds.length - 1];
        }
        if (dialogTitle.value?.includes("新增")) {
          addEmployee(dto)
            .then((res) => {
              loading.value = false;
              if (res.code === AppResponseStatusCode.SUCCESS) {
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
          updateEmployee(dto)
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
  const getOptions = () => {
    getDeptOptions().then((res) => {
      options.value = Utils.getTree(res.data, null, "extra", "value", "sort");
      deptOptions.value = cloneDeep(options.value);
    });
    getPositionOptions().then((res) => {
      positionOptions.value = res.data;
    });
  };
  const handleNodeClick = (data: any) => {
    tableRef.value?.addParam("deptId", data.value);
  };
  const filterNode = (value: string, data: any) => {
    if (!value) return true;
    return data.label.includes(value);
  };
  const uploadSuccess = (res: any, field: string) => {
    editForm[field] = res.data;
  };
  const uploadRemove = (field: string) => {
    editForm[field] = null;
  };
  const openDetailsDialog = (row: any) => {
    detailsDialogVisible.value = true;
    detailsRow.value = row;
  };

  /*========================== Vue钩子 ========================== */
  onMounted(() => {
    getOptions();
  });
  watch(filterText, (val) => {
    deptTreeRef.value!.filter(val);
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
    positionOptions,
    deptOptions,
    handleNodeClick,
    deptTreeRef,
    filterText,
    filterNode,
    uploadApiUrl,
    uploadSuccess,
    fileList1,
    fileList2,
    detailsDialogVisible,
    detailsRow,
    uploadRemove,
  };
}
