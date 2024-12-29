import { ReTableColumn } from "@/components/re-table/types";
import { onMounted, reactive, ref } from "vue";
import {
  genTableColumnList,
  saveGenTableColumn,
} from "@/api/develop/genTableColumn";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { AppResponseStatusCode } from "@/consts";
import { useAuthorization } from "@/hooks/useAuthorization";
import _ from "lodash";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "selection",
      width: "50px",
    },
    {
      prop: "columnName",
      label: "列名",
    },
    {
      prop: "csharpPropName",
      label: "C#属性名",
    },
    {
      prop: "jsFieldName",
      label: "JS字段名",
    },
    {
      prop: "columnType",
      label: "数据库列类型",
    },
    {
      prop: "csharpType",
      label: "C#类型",
    },
    {
      prop: "jsType",
      label: "JS类型",
    },
    {
      prop: "jsType",
      label: "JS类型",
    },
    {
      prop: "htmlType",
      label: "HTML类型",
    },
    {
      prop: "comment",
      label: "列描述",
    },
    {
      prop: "maxLength",
      label: "最大长度",
    },
    {
      prop: "isNullable",
      label: "是否可空",
    },
    {
      prop: "isInsert",
      label: "是否参与新增",
    },
    {
      prop: "isUpdate",
      label: "是否参与修改",
    },
    {
      prop: "isSearch",
      label: "是否参与搜索",
    },
    {
      prop: "searchType",
      label: "搜索类型",
    },
    {
      prop: "isShow",
      label: "是否在表格中显示",
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
  const loading = ref<boolean>(false);

  /*========================== 自定义函数 ========================== */
  const request = (params: any) => {
    return genTableColumnList(params);
  };
  const confirmEvent = () => {
    ElMessageBox.confirm("确定保存？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      tableRef?.value.refresh();
    });
  };

  /*========================== Vue钩子 ========================== */
  onMounted(() => {});
  return {
    request,
    columns,
    filters,
    tableRef,
    confirmEvent,
    loading,
  };
}
