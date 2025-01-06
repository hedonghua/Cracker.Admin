/**
 * 自定义工具类
 */
export default class Utils {
  static IDNO_PATTERN =
    /(^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$)|(^[1-9]\d{5}\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{2}$)/;
  static PHONE_PATTERN = /^1[3456789]\d{9}$/;

  /**
   * 格式化空值显示
   * @param value
   * @returns
   */
  static formatterNullableContent(value: any) {
    if (value === "0" || value === "0" || value === 0) return 0;
    else if (value === false) return "false";
    else if (value === true) return "true";
    return Boolean(value) ? value : "--";
  }

  /**
   * 获取纯净url，不带query参数
   * @param url
   */
  static getPureUrl(url: string) {
    if (!url) return "";
    return url.split("?")[0];
  }

  static getTree(
    arr: Array<any>,
    rootId: string | null,
    parentField: string,
    flagId: string,
    sortField?: string
  ) {
    if (!(arr instanceof Array)) return [];
    if (!arr || arr.length === 0) return [];
    const top = arr.filter((a) => a[parentField] === rootId || !a[parentField]);
    if (top.length === 0) return arr;
    for (const item of top) {
      item["children"] = this.getChildren(
        arr,
        parentField,
        item[flagId],
        flagId,
        sortField
      );
      if (sortField) {
        item["children"] = item["children"].sort((x) => x[sortField]);
      }
    }
    return top;
  }

  private static getChildren(
    arr: Array<any>,
    parentField: string,
    pid: string,
    flagId: string,
    sortField?: string
  ) {
    if (!arr || arr.length === 0) return null;
    const children = arr.filter((a) => a[parentField] === pid);
    for (const item of children) {
      item["children"] = this.getChildren(
        arr,
        parentField,
        item[flagId],
        flagId,
        sortField
      );
      if (sortField) {
        item["children"] = item["children"].sort((x) => x[sortField]);
      }
    }
    return children;
  }

  static generateUniqueId() {
    return Math.random().toString(36).substr(2, 9);
  }

  static renderJsx(isDisplay: boolean, jsx: any, blankJsx: any = null) {
    if (isDisplay) {
      return jsx;
    }
    return blankJsx;
  }
}
