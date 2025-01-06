import { defineStore } from "pinia";

export const useThemeStore = defineStore("theme", {
  state: (): Theme => ({
    language: "zh-cn",
  }),
  persist: true,
  actions: {},
});

export interface Theme {
  language: string;
}
