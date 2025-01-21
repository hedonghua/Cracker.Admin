<template>
  <div class="person-wrapper">
    <div class="re-card" style="padding: 20px">
      <el-form
        label-width="120px"
        label-position="left"
        ref="formRef"
        :model="form"
        :rules="rules"
      >
        <el-form-item label="用户名">
          <p>{{ userStore.user?.username }}</p>
        </el-form-item>
        <el-form-item label="头像" prop="avatar">
          <el-upload
            v-model:file-list="fileList"
            :action="uploadApiUrl"
            list-type="picture-card"
            :limit="1"
            :on-success="uploadSuccess"
          >
            <el-icon>
              <Plus />
            </el-icon>
          </el-upload>
        </el-form-item>
        <el-form-item label="性别" prop="sex">
          <re-dict v-model:value="form.sex" html="radio" dict-type="sex" />
        </el-form-item>
        <el-form-item label="昵称" prop="nickName">
          <el-input placeholder="请输入昵称" v-model="form.nickName" />
        </el-form-item>
        <el-form-item>
          <!-- <el-button @click="resetForm()">重置</el-button> -->
          <el-button
            type="primary"
            @click="submitUpdateUserInfo"
            :loading="loading1"
            >确定</el-button
          >
        </el-form-item>
      </el-form>
      <el-divider />
      <el-form label-width="120px" label-position="left" :model="passwordForm">
        <el-form-item label="旧密码">
          <el-input
            type="password"
            show-password
            v-model="passwordForm.oldPwd"
            placeholder="请输入旧密码"
          />
        </el-form-item>
        <el-form-item label="新密码">
          <el-input
            type="password"
            show-password
            v-model="passwordForm.newPwd"
            placeholder="请输入新密码"
          />
        </el-form-item>
        <el-form-item label="确认密码">
          <el-input
            type="password"
            show-password
            placeholder="请再次输入新密码"
            v-model="passwordForm.surePwd"
          />
        </el-form-item>
        <el-form-item>
          <!-- <el-button @click="clearPwdForm">清空</el-button> -->
          <el-button type="primary" :loading="loading2" @click="submitUpdatePwd"
            >确定</el-button
          >
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useUserStore } from "@/store/userStore";
import { ElMessage, ElMessageBox, FormInstance } from "element-plus";
import { onMounted, reactive, ref } from "vue";
import { updateUserInfo, updateUserPwd } from "@/api/login";
import { useAuthorization } from "@/hooks/useAuthorization";

const formRef = ref<FormInstance>();
const uploadApiUrl = import.meta.env.VITE_UPLOAD_API;
const ossDomain = import.meta.env.VITE_OSS_DOMAIN;
const userStore = useUserStore();
const fileList = ref<Array<any>>([]);
const loading1 = ref<boolean>(false);
const loading2 = ref<boolean>(false);
const form = reactive({
  avatar: "",
  nickName: "",
  sex: "1",
});
const passwordForm = reactive({
  oldPwd: "",
  newPwd: "",
  surePwd: "",
});
const userAuth = useAuthorization();
const rules = {
  nickName: [{ required: true, trigger: "blur", message: "昵称不能为空" }],
  sex: [{ required: true, trigger: "blur", message: "性别不能为空" }],
  avatar: [{ required: true, trigger: "blur", message: "头像不能为空" }],
};

const uploadSuccess = (res: any) => {
  form.avatar = res.data;
};

const submitUpdateUserInfo = () => {
  formRef.value?.validate((valid) => {
    if (valid) {
      loading1.value = true;
      console.log(form);
      updateUserInfo(form).then((res) => {
        loading1.value = false;
        if (res.code === 0) {
          ElMessage.success("更新信息成功");
        } else {
          ElMessage.error(res.message ?? "更新信息失败");
        }
      });
    }
  });
};
const submitUpdatePwd = () => {
  if (!passwordForm.oldPwd) {
    ElMessage.warning("旧密码不能为空");
    return;
  } else if (!passwordForm.newPwd) {
    ElMessage.warning("新密码不能为空");
    return;
  } else if (!passwordForm.surePwd) {
    ElMessage.warning("确认密码不能为空");
    return;
  }
  ElMessageBox.confirm("确定更新登录密码？", "敏感操作", {
    confirmButtonText: "确定",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    loading2.value = true;
    updateUserPwd(passwordForm).then((res) => {
      loading2.value = false;
      if (res.code === 0) {
        ElMessage.success({
          message: "更新密码成功",
          duration: 1500,
          onClose: () => {
            userAuth.signOut();
          },
        });
      } else {
        ElMessage.error(res.message ?? "更新密码失败");
      }
    });
  });
};

onMounted(() => {
  if (userStore.info?.avatar) {
    form.avatar = userStore.info?.avatar;
  } else {
    form.avatar = "/assets/img/boy.png";
  }
  if (form.avatar) {
    fileList.value.push({ url: ossDomain + form.avatar });
  }
  form.sex = userStore.info?.sex?.toString() ?? "1";
  form.nickName = userStore.info?.nickName ?? "";
});
</script>
