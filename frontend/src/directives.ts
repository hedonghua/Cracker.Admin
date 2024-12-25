import { App } from "vue";
import { useAuthorization } from "./hooks/useAuthorization";

/**
 * 加载自定义指令
 * @param app
 */
function loadDirectives(app: App<Element>) {
  const userAuth = useAuthorization();
  /**
   * 权限指令
   */
  app.directive("permission", {
    created(el, binding) {
      if (!userAuth.hasPermission(binding.value)) {
        el.style.display = "none";
      }
    },
  });
}

export default loadDirectives;
