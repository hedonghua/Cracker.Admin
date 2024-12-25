import { ApplicationResult, PagedResult } from "#/data";

export type CustomRenderFunc = (row: any) => any;
export type CustomRequestFunc = (
  para: any
) =>
  | Promise<ApplicationResult<PagedResult<any>>>
  | Promise<ApplicationResult<any>>;

export interface ReTableColumn {
  type?: "default" | "selection" | "index" | "expand";
  index?: number | Function;
  label?: string;
  columnKey?: string;
  prop?: string;
  width?: string | number;
  minWidth?: string | number;
  fixed?: "left" | "right" | boolean;
  renderHeader?: Function;
  sortable?: boolean | string;
  sortMethod?: Function;
  sortBy?: Function | string | object;
  sortOrders?: object;
  resizable?: boolean;
  formatter?: Function;
  showOverflowTooltip?: boolean;
  align?: "left" | "center" | "right";
  headerAlign?: "left" | "center" | "right";
  className?: string;
  labelClassName?: string;
  selectable?: Function;
  reserveSelection?: boolean;
  filters?: object;
  filterPlacement?:
    | "top"
    | "top-start"
    | "top-end"
    | "bottom"
    | "bottom-start"
    | "bottom-end"
    | "left"
    | "left-start"
    | "left-end"
    | "right"
    | "right-start"
    | "right-end";
  filterClassName?: string;
  filterMultiple?: boolean;
  filterMethod?: Function;
  filteredValue?: object;
  render?: CustomRenderFunc;
  isDate?: boolean;
  isTime?: boolean;
}

export interface FilterStruct {
  type: string;
  label: string;
  key: string;
  placeholder?: string;
  clearable?: boolean;
  defaultValue?: string;
}
