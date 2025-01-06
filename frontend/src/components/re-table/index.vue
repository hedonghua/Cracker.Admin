<template>
  <div class="re-table-wrapper">
    <!-- 查询 --->
    <div class="re-card re-filter-card" v-if="getIsShowFilter()">
      <el-form :model="filterForm" :inline="true" ref="searchFormRef">
        <el-form-item :label="v.label" v-for="(v, i) in filters" :key="i" :prop="v.key">
          <el-input v-if="v.type === 'text' || !v.type" :placeholder="v.placeholder" :clearable="v.clearable"
            v-model="filterForm[v.key]" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="onQuery" :loading="isLoading">
            <Icon v-if="!isLoading" class="mr-1" icon="material-symbols:search-rounded" />
            查询
          </el-button>
          <el-button @click="onReset">
            <Icon class="mr-1" icon="material-symbols:refresh" />
            重置
          </el-button>
        </el-form-item>
      </el-form>
    </div>
    <!-- 列表 -->
    <div :class="(getIsShowFilter() ? 'mt-4' : '') + ' re-card'">
      <div class="re-table-toolbar mb-4">
        <slot name="toolbar"></slot>
      </div>
      <el-table v-loading="isLoading" :data="tableData" :header-cell-style="headerCellStyle"
        :cell-style="props.cellStyle" :border="props.border" :default-expand-all="props.defaultExpandAll"
        :row-key="props.rowKey" empty-text="暂无数据" :tree-props="props.treeProps"
        @row-click="(row: any, col: any, e: Event) => emits('row-click', row, col, e)"
        @selection-change="tableSelectionChange">
        <el-table-column v-for="(v, i) in props.columns" :key="i" :type="v.type" :index="v.index"
          :column-key="v.columnKey" :prop="v.prop" :label="v.label" :width="v.width" :fixed="v.fixed"
          :render-header="v.renderHeader" :sortable="v.sortable" :sort-method="v.sortMethod" :sort-by="v.sortBy"
          :sort-orders="v.sortOrders" :resizable="v.resizable"
          :formatter="v.formatter ?? ((a, b, cellValue) => defaultFormatter(a, b, cellValue, v.isDate, v.isTime))"
          :show-overflow-tooltip="v.showOverflowTooltip" :align="v.align" :header-align="v.headerAlign"
          :class-name="v.className" :label-class-name="v.labelClassName" :selectable="v.selectable"
          :reserve-selection="v.reserveSelection" :filters="v.filters" :filter-placement="v.filterPlacement"
          :filter-class-name="v.filterClassName" :filter-multiple="v.filterMultiple" :filter-method="v.filterMethod"
          :filtered-value="v.filteredValue">
          <template #default="scope" v-if="v.render && !isLoading">
            <component :is="v.render(scope.row)"></component>
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页 -->
      <div class="w-full flex flex-row-reverse mt-4">
        <el-pagination v-model:page-size="filterForm.size" :total="total" :page-sizes="[10, 20, 30, 40, 50]"
          :pager-count="5" @size-change="handleSizeChange" @current-change="handleCurrentChange" size="small" background
          layout="total, sizes, prev, pager, next" />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import "./index.scss"
import { Icon } from "@iconify/vue";
import { CSSProperties, onBeforeMount, onMounted, reactive, ref } from "vue";
import { FormInstance } from "element-plus/es/components/form";
import { ReTableColumn, FilterStruct, CustomRequestFunc } from './types'
import { PagedResult } from "#/data";
import utils from "@/utils"
import dayjs from "dayjs";

const headerCellStyle: CSSProperties = { background: '#fafafa', color: '#334155' };
const props = withDefaults(defineProps<{
  columns: ReTableColumn[],
  //当request函数存在时，data不生效
  data?: Array<any>,
  height?: string | number,
  maxHeight?: string | number,
  stripe?: boolean,
  border?: boolean,
  size?: '' | 'large' | 'default' | 'small',
  fit?: boolean,
  showHeader?: boolean,
  highlightCurrentRow?: boolean,
  currentRowKey?: string | number,
  rowClassName?: Function | string,
  rowStyle?: Function | object,
  cellClassName?: Function | object,
  cellStyle?: Function | object,
  headerRowClassName?: Function | string,
  headerRowStyle?: Function | object,
  headerCellClassName?: Function | string,
  rowKey: string,
  emptyText?: string,
  defaultExpandAll?: boolean,
  expandRowKeys?: object,
  defaultSort?: object,
  tooltipEffect?: 'dark' | 'light',
  tooltipOptions?: object,
  showSummary?: boolean,
  sumText?: string,
  summaryMethod?: Function,
  spanMethod?: Function,
  selectOnIndeterminate?: boolean,
  indent?: number,
  lazy?: boolean,
  load?: Function,
  treeProps?: object,
  tableLayout?: 'fixed' | 'auto',
  scrollbarAlwaysOn?: boolean,
  showOverflowTooltip?: boolean,
  flexible?: boolean,
  enabledFilter?: boolean,
  filters?: Array<FilterStruct>,
  request?: CustomRequestFunc | undefined
}>(), {
  stripe: false,
  border: false,
  size: "default",
  fit: true,
  showHeader: true,
  enabledFilter: false
});
const emits = defineEmits<{
  (e: 'row-click', row: any, col: any, event: Event): void,
  (e: 'selection-change', selection: any[]): void,
  (e: 'request-callback', res: any): void
}>();
const filterForm = reactive({
  page: 1,
  size: 10
});
const extraParamKeys = ref<string[]>([]);
const total = ref<number>(0);
const tableData = ref<Array<any>>(props.data ?? []);
const selectedRows = ref<any[]>();
const selectedRowKeys = ref<any[]>();
const searchFormRef = ref<FormInstance>();
const isLoading = ref<boolean>(false);
//查询
const onQuery = () => {
  if (props.request) {
    requestData();
  }
}
//重置
const onReset = () => {
  searchFormRef?.value?.resetFields();
  for (const key of extraParamKeys.value!) {
    delete filterForm[key];
  }
  if (props.request) {
    requestData();
  }
}
const tableSelectionChange = (newSelection: any[]) => {
  selectedRows.value = newSelection;
  if (props.rowKey) {
    selectedRowKeys.value = selectedRows.value?.map(x => x[props.rowKey]);
  }
  emits("selection-change", newSelection)
}

//异步请求接口数据
function requestData() {
  if (props.request) {
    isLoading.value = true;
    props.request(filterForm).then((res: PagedResult<any> | any) => {
      emits("request-callback", res)
      if (res instanceof Array) {
        tableData.value = res;
      } else if (res.data instanceof Array) {
        tableData.value = res.data;
      }
      else if (res.data && res.data.totalCount) {
        tableData.value = res.data.items;
        total.value = res.data.totalCount;
      } else {
        tableData.value = [];
      }
      isLoading.value = false;
    }).catch(() => {
      isLoading.value = false;
    });
  }
}

function handleSizeChange(val: number) {
  filterForm.size = val;
  requestData();
}

function handleCurrentChange(val: number) {
  filterForm.page = val;
  requestData();
}

function defaultFormatter(_1: any, _2: any, cellValue: any, isDate?: boolean, isTime?: boolean) {
  const formatValue = utils.formatterNullableContent(cellValue);
  if (isDate) {
    return dayjs(formatValue).format("YYYY-MM-DD");
  }
  if (isTime) {
    return dayjs(formatValue).format("YYYY-MM-DD HH:mm:ss");
  }
  return formatValue;
}

const refresh = async () => {
  requestData();
}
const addParam = (key: string, value: any) => {
  extraParamKeys.value?.push(key);
  filterForm[key] = value;
  requestData();
}
const removeParam = (key: string) => {
  delete filterForm[key];
  const i = extraParamKeys.value?.findIndex(x => x === key) ?? -1;
  if (i >= 0) {
    extraParamKeys.value?.splice(i, 1);
  }
  requestData();
}
const getIsShowFilter = () => {
  return (props.enabledFilter && props.filters && props.filters.length > 0) ?? false
}
const getTableData = () => {
  return tableData.value;
}

defineExpose({
  refresh,
  selectedRows,
  selectedRowKeys,
  addParam,
  removeParam,
  getTableData
})

onBeforeMount(() => {
  if (getIsShowFilter()) {
    for (let i = 0; i < props.filters!.length; i++) {
      const item = props.filters![i];
      filterForm[item.key] = item?.defaultValue;
    }
  }
})
onMounted(() => {
  requestData();
})
</script>