import { ReTableColumn } from "@/components/re-table/types";
import { onBeforeMount, ref } from "vue";
import {
  genTableColumnList,
  saveGenTableColumn,
} from "@/api/develop/genTableColumn";
import { useRoute } from "vue-router";
import { ElMessage, ElMessageBox } from "element-plus";
import { useAuthorization } from "@/hooks/useAuthorization";
import * as _ from "lodash";
import { useCodeGenerator } from "./codeGenerator";
import { useTabManager } from "@/hooks/useTabManager";

export function useTable() {
  /*========================== 字段 ========================== */
  const columns: ReTableColumn[] = [
    {
      type: "index",
      width: "50px",
    },
    {
      prop: "columnName",
      label: "列名",
    },
    {
      prop: "columnType",
      label: "列类型",
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
      prop: "csharpPropName",
      label: "C#属性名",
      render: (row: any) => (
        <el-input v-model={row.csharpPropName} placeholder="请输入C#属性名" />
      ),
    },
    {
      prop: "jsFieldName",
      label: "JS字段名",
      render: (row: any) => (
        <el-input v-model={row.jsFieldName} placeholder="请输入JS字段名" />
      ),
    },
    {
      prop: "csharpType",
      label: "C#类型",
      render: (row: any) => (
        <el-select v-model={row.csharpType} placeholder="请选择C#类型">
          {codeGenerator.csharpTypeOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "jsType",
      label: "JS类型",
      render: (row: any) => (
        <el-select v-model={row.jsType} placeholder="请选择JS类型">
          {codeGenerator.jsTypeOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "htmlType",
      label: "HTML类型",
      render: (row: any) => (
        <el-select v-model={row.htmlType} placeholder="请选择HTML类型">
          {codeGenerator.htmlTypeOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "isNullable",
      label: "是否可空",
      render: (row: any) => (
        <el-select v-model={row.isNullable} placeholder="请选择">
          {codeGenerator.trueOrFalseOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "isInsert",
      label: "是否参与新增",
      render: (row: any) => (
        <el-select v-model={row.isInsert} placeholder="请选择">
          {codeGenerator.trueOrFalseOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "isUpdate",
      label: "是否参与修改",
      render: (row: any) => (
        <el-select v-model={row.isUpdate} placeholder="请选择">
          {codeGenerator.trueOrFalseOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "isSearch",
      label: "是否参与搜索",
      render: (row: any) => (
        <el-select v-model={row.isSearch} placeholder="请选择">
          {codeGenerator.trueOrFalseOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "searchType",
      label: "搜索类型",
      render: (row: any) => (
        <el-select v-model={row.searchType} placeholder="请选择">
          {codeGenerator.searchTypeOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
    {
      prop: "isShow",
      label: "是否在表格中显示",
      render: (row: any) => (
        <el-select v-model={row.isShow} placeholder="请选择">
          {codeGenerator.trueOrFalseOptions.map((item) => {
            return (
              <el-option
                key={item.value}
                label={item.label}
                value={item.value}
              ></el-option>
            );
          })}
        </el-select>
      ),
    },
  ];
  const filters = [
    {
      type: "text",
      label: "列名",
      key: "columnName",
      placeholder: "请输入列名",
    },
  ];
  const userAuth = useAuthorization();
  const tabManager = useTabManager();
  const tableRef = ref();
  const loading = ref<boolean>(false);
  const genTableId = ref<string>();
  const codeGenerator = useCodeGenerator();

  /*========================== 自定义函数 ========================== */
  const request = (params: any) => {
    const endParams = { ...params, genTableId: genTableId.value };
    return genTableColumnList(endParams);
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
  const submit = () => {
    ElMessageBox.confirm("确定提交？", "提示", {
      confirmButtonText: "确定",
      cancelButtonText: "取消",
      type: "warning",
    }).then(() => {
      const data = tableRef?.value.getTableData();
      if (!data || data.length === 0) {
        ElMessage.error("当前页无数据");
        return;
      }
      const requestData = { items: data, genTableId: genTableId.value };
      loading.value = true;
      saveGenTableColumn(requestData)
        .then((res) => {
          if (res.code === 0) {
            ElMessage.success("提交成功");
          } else {
            ElMessage.error(res.message ?? "提交失败");
          }
          loading.value = false;
        })
        .catch((_) => {
          loading.value = false;
        });
    });
  };
  const goBack = () => {
    tabManager.append("/develop/genTable");
  };

  /*========================== Vue钩子 ========================== */
  onBeforeMount(() => {
    const route = useRoute();
    genTableId.value = route.params.id as string;
  });

  return {
    request,
    columns,
    filters,
    tableRef,
    confirmEvent,
    loading,
    submit,
    goBack,
  };
}
