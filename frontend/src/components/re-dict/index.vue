<template>
  <span v-if="html === 'text'" v-text="getLabel()"></span>
  <el-radio-group v-else-if="html === 'radio'" v-model="val">
    <el-radio v-for="(v, i) in options" :key="i" :value="v.value">{{
      v.label
    }}</el-radio>
  </el-radio-group>
</template>

<script lang="ts" setup>
import { getDictDataOptions } from "@/api/system/dictType";
import { onMounted, ref, watch } from "vue";

const props = withDefaults(
  defineProps<{
    dictType: string;
    html?: "text" | "select" | "radio";
    value?: string;
  }>(),
  {
    html: "text",
  }
);
const emit = defineEmits(["update:value"]);
const options = ref<any[]>([]);
const val = ref<string>(props.value ?? "");

const getLabel = () => {
  if (!props.value) return "";
  return options.value.find((x) => x.value === props.value)?.label;
};

onMounted(() => {
  getDictDataOptions(props.dictType).then((res) => {
    if (res.data) {
      options.value = res.data;
    }
  });
});
watch(
  () => props.value,
  (newVal) => {
    if (!newVal) {
      val.value = "";
      return;
    }
    val.value = newVal;
  }
);

watch(val, (newVal) => {
  emit("update:value", newVal);
});
</script>
