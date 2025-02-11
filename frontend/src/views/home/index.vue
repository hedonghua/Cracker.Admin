<template>
  <div>
    <div class="re-card">
      <el-descriptions title="项目信息" :column="1">
        <el-descriptions-item label="名称">{{
          APP_TITLE
        }}</el-descriptions-item>
        <el-descriptions-item label="概述"
          >前端使用re-admin（自己写的前端框架，不依赖任何admin框架），后端基于abp框架，实现了rbac（基于角色权限访问控制）通用权限管理，精确到按钮级别的权限控制。
        </el-descriptions-item>
        <el-descriptions-item label="背景"
          >想写一款后台管理框架，这是我一直想做的事，用于开源和自己接单做项目</el-descriptions-item
        >
        <el-descriptions-item label="使用技术">
          <ul class="list-disc ml-8 mt-2">
            <li>.NETCore</li>
            <li>Abp</li>
            <li>MySQL</li>
            <li>Redis</li>
            <li>EntityFrameworkCore</li>
            <li>Dapper</li>
            <li>JWT</li>
            <li>Coravel</li>
            <li>AutoMapper</li>
            <li>Serilog</li>
            <li>IP2Region.Net</li>
            <li>MQTT</li>
            <li>Vue</li>
            <li>Vite</li>
            <li>TypeScript</li>
            <li>MockJs</li>
            <li>VueRouter</li>
            <li>Pinia</li>
            <li>Lodash</li>
            <li>Axios</li>
            <li>DayJs</li>
            <li>ElementUI Plus</li>
            <li>Sass</li>
            <li>Iconify</li>
            <li>TailwindCSS</li>
          </ul>
        </el-descriptions-item>
      </el-descriptions>

      <el-descriptions title="仓库信息" :column="1" class="mt-2">
        <el-descriptions-item label="Github">
          <a
            href="https://github.com/hedonghua/Cracker.Admin"
            style="color: var(--el-color-primary)"
            target="_blank"
            >https://github.com/hedonghua/Cracker.Admin</a
          >
        </el-descriptions-item>
      </el-descriptions>

      <el-descriptions title="联系作者" :column="1" class="mt-2">
        <el-descriptions-item label="QQ"
          >1491184849</el-descriptions-item
        >
        <el-descriptions-item label="邮箱"
          >crackerwork@outlook.com/1491184849@qq.com</el-descriptions-item
        >
      </el-descriptions>
    </div>

    <el-row :gutter="20" class="mt-3">
      <el-col :span="12">
        <div class="re-card">
          <h2 class="el-descriptions__title">更新日志</h2>
          <el-collapse accordion class="mt-4">
            <el-collapse-item title="支持多租户（2025-02-11）" name="多租户">
              <div>
                支持多租户，租户之间可以独立分库，独立redis；并且支持动态切换；在后台创建租户后，会自动生成租户数据、迁移种子数据
              </div>
            </el-collapse-item>
            <el-collapse-item
              title="mqttServer（2025-02-08）"
              name="mqttServer"
            >
              <div>项目中搭建简易的mqtt服务器</div>
            </el-collapse-item>
            <el-collapse-item title="个性设置（2025-01-09）" name="个性设置">
              <div>增加个性设置（主题色、Logo显示/隐藏）</div>
            </el-collapse-item>
            <el-collapse-item title="路由优化（2025-01-06）" name="路由优化">
              <div>固定路由拆分，动态路由优化，退出登录清空路由缓存</div>
            </el-collapse-item>
            <el-collapse-item
              title="修改登录样式（2025-01-04）"
              name="登录样式"
            >
              <div>登录表单居中，增加登录背景图</div>
            </el-collapse-item>
            <el-collapse-item title="基本功能（2024-12-25）" name="基本功能">
              <div>基本功能做完，需要持续优化、测试</div>
            </el-collapse-item>
          </el-collapse>
        </div>
      </el-col>
      <el-col :span="12">
        <div class="re-card">
          <h2 class="el-descriptions__title">捐赠支持</h2>
          <div class="mt-4 flex">
            <div class="text-center">
              <p>微信</p>
              <re-image
                width="150px"
                height="150px"
                :src="WxPay"
                :previewList="[WxPay]"
                fit="cover"
              />
            </div>
            <div class="text-center ml-5">
              <p>支付宝</p>
              <re-image
                width="150px"
                height="150px"
                :src="AliPay"
                :previewList="[AliPay]"
                fit="cover"
              />
            </div>
          </div>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from "vue";
import WxPay from "@/assets/img/wxpay.jpg";
import AliPay from "@/assets/img/alipay.jpg";

const APP_TITLE = import.meta.env.VITE_APP_TITLE;

onMounted(() => {});
</script>
