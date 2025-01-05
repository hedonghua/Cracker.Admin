<template>
  <div class="login-container"
    :style="{ backgroundImage: `url(${LoginBg})`, backgroundSize: 'cover', backgroundRepeat: 'no-repeat', backgroundPosition: 'center' }">
    <div class="flex flex-col items-center justify-center h-full">
      <div class="login-form-container">
        <p class="login-title">{{ APP_TITLE }}后台登录</p>
        <el-form :model="form" :rules="rules" ref="formRef" size="large">
          <el-form-item prop="username">
            <el-input v-model="form.username" placeholder="请输入登录账号">
              <template #prepend>
                <el-icon>
                  <User />
                </el-icon>
              </template>
            </el-input>
          </el-form-item>
          <el-form-item prop="password">
            <el-input type="password" show-password v-model="form.password" placeholder="请输入登录密码">
              <template #prepend><el-icon>
                  <Lock />
                </el-icon></template>
            </el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" class="w-full mt-2" @click="login">登录</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>

    <!-- 底部声明 -->
    <p class="text-slate-300 text-sm absolute left-1/2 transform -translate-x-1/2 bottom-5">
      发现问题，请联系作者：crackerwork@outlook.com
    </p>
  </div>
</template>

<script lang="ts" setup>
import { FormInstance, FormRules } from "element-plus";
import { reactive, ref } from "vue";
import { LoginForm, userLogin } from "@/api/login";
import { useUserStore, UserAuthInfo } from "@/store/userStore"
import { useRouter } from "vue-router";
import LoginBg from "@/assets/img/bg.jpg";
import './index.styl'
import { useRouteCache } from "@/router/hook";

const APP_TITLE = import.meta.env.VITE_APP_TITLE;
const userStore = useUserStore();
const routeCache = useRouteCache();
const formRef = ref<FormInstance>();
const form = reactive<LoginForm>({
  username: "admin",
  password: "123qwe*",
});
const rules: FormRules<LoginForm> = {
  username: [{ required: true, trigger: "blur", message: "账号不能为空" }],
  password: [{ required: true, trigger: "blur", message: "密码不能为空" }],
};
const router = useRouter();
const login = () => {
  formRef?.value?.validate((valid) => {
    if (valid) {
      userLogin(form).then((res) => {
        //设置身份信息
        userStore.setUser(res.data as UserAuthInfo);
        //初始化路由
        routeCache.loadRoutes();
        router.replace("/home");
      });
    }
  });
};
</script>