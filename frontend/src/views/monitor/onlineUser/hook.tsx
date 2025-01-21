import { ReTableColumn } from "@/components/re-table/types";
import { ref } from "vue";
import { getOnlineUserList, logoutUser } from "@/api/monitor/onlineUser";
import { ElMessage } from "element-plus";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
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
      prop: "creationTime",
      isTime: true,
      width: 180,
      label: "登录时间",
    },
    {
      fixed: "right",
      label: "操作",
      render: (row: any) => (
        <el-button
          size="small"
          link
          type="primary"
          onclick={() => doLogout(row)}
        >
          注销
        </el-button>
      ),
    },
  ];
  const filters = [
    {
      type: "text",
      label: "账号",
      key: "userName",
      placeholder: "请输入账号",
    },
  ];
  const tableRef = ref();

  /*========================== 自定义函数 ========================== */
  const request = (params: any) => {
    return getOnlineUserList(params);
  };
  const doLogout = (row: any) => {
    logoutUser(row.userId + ":" + row.sessionId).then((res) => {
      if (res.code === 0) {
        ElMessage.success("注销成功");
        tableRef.value?.refresh();
      }
    });
  };

  return {
    request,
    columns,
    filters,
    tableRef,
  };
}
