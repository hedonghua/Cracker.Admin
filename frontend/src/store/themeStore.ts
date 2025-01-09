import { defineStore } from "pinia";

export const defaultTheme: Theme = {
  language: "zh-cn",
  size: "default",
  color: "light",
  themeColor: "#409EFF",
  pageSwitch: { logo: true },
};

export const useThemeStore = defineStore("theme", {
  state: (): Theme => ({
    language: defaultTheme.language,
    size: defaultTheme.size,
    color: defaultTheme.color,
    themeColor: defaultTheme.themeColor,
    pageSwitch: JSON.parse(JSON.stringify(defaultTheme.pageSwitch)),
  }),
  persist: true,
  actions: {
    setSize(size: "large" | "default" | "small") {
      this.size = size;
    },
    setColor(color: "light" | "dark" | "auto") {
      this.color = color;
    },
    setThemeColor(color: string) {
      this.themeColor = color;
    },
    setPageSwitch(pageSwitch: any) {
      this.pageSwitch = pageSwitch;
    },
    reset() {
      if (this.language !== defaultTheme.language) {
        this.language = defaultTheme.language;
      }
      if (this.size !== defaultTheme.size) {
        this.size = defaultTheme.size;
      }
      if (this.color !== defaultTheme.color) {
        this.color = defaultTheme.color;
      }
      if (this.themeColor !== defaultTheme.themeColor) {
        this.themeColor = defaultTheme.themeColor;
      }
      if (this.pageSwitch.logo !== defaultTheme.pageSwitch.logo) {
        this.pageSwitch.logo = defaultTheme.pageSwitch.logo;
      }
    },
  },
});

export interface Theme {
  /** 语言 */
  language: string;
  /** 尺寸 */
  size: "large" | "default" | "small";
  /** 颜色 */
  color: "light" | "dark" | "auto";
  /** 主题色 */
  themeColor: string;
  /** 界面显示 */
  pageSwitch: any;
}
