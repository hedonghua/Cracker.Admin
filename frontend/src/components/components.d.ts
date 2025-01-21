import ReImage from "./re-image/index.vue";
import ReIcon from "./re-icon/index.vue";
import ReTable from "./re-table/index.vue";
import ReTag from "./re-tag/index.vue";
import ReDict from "./re-dict/index.vue";

declare module "vue" {
  export interface GlobalComponents {
    /** 图片预览 */
    ReImage: typeof ReImage;
    /** 显示图标 */
    ReIcon: typeof ReIcon;
    /** 智能表格 */
    ReTable: typeof ReTable;
    /** 标签 */
    ReTag: typeof ReTag;
    /** 字典 */
    ReDict: typeof ReDict;
  }
}
