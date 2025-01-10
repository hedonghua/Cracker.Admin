import { App } from "vue";
import ReImage from "./re-image/index.vue";
import ReIcon from "./re-icon/index.vue";
import ReTable from "./re-table/index.vue";
import ReTag from "./re-tag/index.vue";

/**
 * 加载自定义组件
 * @param app
 */
function loadComponents(app: App<Element>) {
  app.component("re-image", ReImage);
  app.component("re-icon", ReIcon);
  app.component("re-table", ReTable);
  app.component("re-tag", ReTag);
}

export default loadComponents;
