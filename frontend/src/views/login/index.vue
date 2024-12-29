<template>
  <div class="login-container">
    <el-row class="h-full">
      <el-col :span="12" class="z-10 bg-sky-500">
        <div class="flex flex-col justify-center w-full h-full">
          <div class="content">
            <p>{{ APP_TITLE }}，纯手写后台管理框架，不依赖任何Admin框架，使用Vite构建，UI框架ElementPlus，封装了一些常用组件，例如：图片组件、智能表格组件、tag标签组件等等。</p>
          </div>
          <img :src="LoginSvg" class="login-img" />
        </div>
      </el-col>
      <el-col :span="12">
        <div class="flex flex-col items-center justify-center min-h-full">
          <div class="login-form-container">
            <h2>{{ APP_TITLE }}后台登录</h2>
            <el-form :model="form" :rules="rules" ref="formRef">
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
                <el-button type="primary" class="w-full" @click="login">登录</el-button>
              </el-form-item>
            </el-form>
            <p class="text-gray-600">发现问题，请联系作者：crackerwork@outlook.com</p>
          </div>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts" setup>
import { FormInstance, FormRules } from "element-plus";
import { reactive, ref } from "vue";
import { LoginForm, userLogin } from "@/api/login";
import { useUserStore, UserAuthInfo } from "@/store/userStore"
import LoginSvg from '@/assets/img/login.svg'
import './index.styl'

const APP_TITLE = import.meta.env.VITE_APP_TITLE;
const userStore = useUserStore();
const formRef = ref<FormInstance>();
const form = reactive<LoginForm>({
  username: "admin",
  password: "123qwe*",
});
const rules: FormRules<LoginForm> = {
  username: [{ required: true, trigger: "blur", message: "账号不能为空" }],
  password: [{ required: true, trigger: "blur", message: "密码不能为空" }],
};
const login = () => {
  formRef?.value?.validate((valid) => {
    if (valid) {
      userLogin(form).then((res) => {
        userStore.setUser(res.data as UserAuthInfo);
        window.location.href = "/home";
      });
    }
  });
};
</script>