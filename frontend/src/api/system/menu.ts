import request from "@/request";
import { AppOptionNode, ApplicationResult } from "#/data";

export interface MenuItemMena {
  title: string;
  icon: string;
  auths: string[];
  roles?: string[];
}

export interface MenuItem {
  id: string;
  name: string;
  path: string;
  meta: MenuItemMena;
  children: MenuItem[];
}

export interface MenuListItem {
  id: string;
  title: string;
  path: string;
  permission?: string;
  sort: number;
  children: MenuListItem[];
}

// 侧边菜单
export function getSidebarMenus(params?: any) {
  return request.get<any, ApplicationResult<MenuItem[]>>("/adm/account/menus", {
    params: params,
  });
}

// 菜单列表
export function getMenuList(params: any) {
  return request.get<any, ApplicationResult<MenuListItem[]>>("/adm/menu/list", {
    params: params,
  });
}

/**
 * 添加菜单
 * @param data
 * @returns
 */
export function addMenu(data: any) {
  return request.post<any, ApplicationResult<any>>("/adm/menu/add", data);
}

/**
 * 修改菜单
 * @param data
 * @returns
 */
export function updateMenu(data: any) {
  return request.put<any, ApplicationResult<any>>("/adm/menu/update", data);
}

/**
 * 删除菜单
 * @param id
 * @returns
 */
export function deleteMenu(ids: string[]) {
  return request.delete<any, ApplicationResult<any>>("/adm/menu/delete", {
    data: ids,
  });
}

// 菜单选项
export function getMenuOptions() {
  return request.get<any, ApplicationResult<AppOptionNode[]>>(
    "/adm/menu/menu-options"
  );
}
