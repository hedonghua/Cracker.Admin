import { defineStore } from "pinia";

export const useThemeStore = defineStore("theme", {
  state: (): Theme => ({
    language: "zh-cn",
    size: "default",
  }),
  persist: true,
  actions: {
    setSize(size: "large" | "default" | "small") {
      this.size = size;
    },
  },
});

export interface Theme {
  language: string;
  size: "large" | "default" | "small";
}
