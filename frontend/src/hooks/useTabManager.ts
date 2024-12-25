import { CloseTabType, TabModel, useTabStore } from "@/store/tabStore";
import { useRouter } from "vue-router";
import { HOME_PATH } from "@/consts";

export function useTabManager() {
  const coreTabStore = useTabStore();
  const router = useRouter();

  function setActiveWhite(){
    coreTabStore.setActive("");
  }

  function setActive(v: TabModel) {
    coreTabStore.setActive(v.id);
    router.replace(v.path);
  }

  function append(path: string, title?: string | null | undefined) {
    if (path === "/" || path === HOME_PATH) {
      coreTabStore.setActive("");
      router.replace("/");
      return;
    }
    const arr = getDisplayTabs();
    const existIndex = arr.findIndex((x) => x.path === path);
    //存在就选中
    if (existIndex >= 0) {
      setActive(arr[existIndex]);
      return;
    }
    if (!title) {
      title = (router.getRoutes().find((x) => x.path === path)?.meta?.title ??
        null) as string | null;
    }
    coreTabStore.append(path, title);
    router.replace(path);
  }

  function close(type: CloseTabType, targetId?: string | null | undefined) {
    coreTabStore.close(type, replaceRouter, targetId);
  }

  function getDisplayTabs() {
    return coreTabStore.displayTabs;
  }

  function getActiveItem() {
    return coreTabStore.activeItem;
  }

  function replaceRouter(path: string) {
    router?.replace(path);
  }

  return {
    append,
    setActive,
    close,
    getDisplayTabs,
    getActiveItem,
    setActiveWhite
  };
}
