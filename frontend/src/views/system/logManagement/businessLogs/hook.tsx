import { ReTableColumn } from "@/components/re-table/types";
import { ref } from "vue";
import { getBusinessLogList } from "@/api/system/logManagement/businessLogs";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      prop: "id",
      label: "ID",
  },
    {
      prop: "nodeName",
      label: "节点",
    },
    {
      prop: "userName",
      label: "账号",
    },
    {
      prop: "ip",
      label: "IP",
    },
    {
      prop: "address",
      label: "地址",
    },
    // {
    //   prop: "millSeconds",
    //   label: "耗时",
    //   formatter: (row: any) => {
    //     return row.millSeconds + "ms";
    //   },
    // },
    {
      prop: "isSuccess",
      label: "结果",
      width: 80,
      render: (row: any) => {
        const text = row.isSuccess ? "成功" : "失败";
        return row.isSuccess ? (
          <re-tag type="success" text={text} />
        ) : (
          <re-tag type="danger" text={text} />
        );
      },
    },
    {
      prop: "creationTime",
      isTime: true,
      width: 180,
      label: "时间",
    },
    {
      label: "操作",
      render: (row: any) => (
        <el-button
          size="small"
          link
          type="primary"
          onclick={() => openDetailsDialog(row)}
        >
          详情
        </el-button>
      ),
    },
  ];
  const filters = [
    {
      type: "text",
      label: "节点",
      key: "nodeName",
      placeholder: "请输入操作系统",
    },
    {
      type: "text",
      label: "账号",
      key: "userName",
      placeholder: "请输入登录账号",
    },
    {
      type: "text",
      label: "方法",
      key: "action",
      placeholder: "请输入操作方法名",
    },
  ];
  const tableRef = ref();
  const dialogVisible = ref<boolean>(false);
  const currentRow = ref<any>();

  /*========================== 自定义函数 ========================== */
  const request = (params: any) => {
    return getBusinessLogList(params);
  };
  const openDetailsDialog = (row: any) => {
    dialogVisible.value = true;
    currentRow.value = row;
  };
  return {
    request,
    columns,
    filters,
    tableRef,
    openDetailsDialog,
    dialogVisible,
    currentRow,
  };
}
