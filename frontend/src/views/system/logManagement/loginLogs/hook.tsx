import { ReTableColumn } from "@/components/re-table/types";
import { ref } from "vue";
import { getLoginLogList } from "@/api/system/logManagement/loginLogs";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      prop: "id",
      label: "ID",
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
    {
      prop: "os",
      label: "系统",
    },
    {
      prop: "browser",
      label: "浏览器",
    },
    {
      prop: "operationMsg",
      label: "消息",
    },
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
  ];
  const filters = [
    {
      type: "text",
      label: "账号",
      key: "userName",
      placeholder: "请输入登录账号",
    },
    {
      type: "text",
      label: "地址",
      key: "address",
      placeholder: "请输入登录地址",
    },
    {
      type: "text",
      label: "系统",
      key: "os",
      placeholder: "请输入操作系统",
    },
  ];
  const tableRef = ref();

  /*========================== 自定义函数 ========================== */
  const request = (params: any) => {
    return getLoginLogList(params);
  };
  return {
    request,
    columns,
    filters,
    tableRef,
  };
}
