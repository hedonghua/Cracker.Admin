// 接口响应类型，带data
export interface ApplicationResult<T> {
  code: number;
  message: string;
  data: T;
}

// 分页列表模型
export interface PagedResult<T> {
  items: Array<T>;
  totalCount: number;
}

/**
 * app选项
 */
export interface AppOption {
  label: string;
  value: string;
  extra: any;
}

/**
 * app选项树形
 */
export interface AppOptionNode extends AppOption {
  children: AppOptionNode;
}
