<template>
    <div>
        <el-dialog v-model="dialogVisible" :close-on-click-modal="false" @close="handleClose"
            :close-on-press-escape="false" title="预览" width="75%" append-to-body>
            <el-tabs v-model="activeName">
                <el-tab-pane v-for="item, index in Object.keys(codes ?? {})" :key="index" v-if="codes"
                    :label="codes[item]?.label + '.cs'" :name="item">
                    <p class="float-right mr-2 mb-2">
                        <el-button type="primary" link icon="DocumentCopy" @click="copyCode(item)">复制</el-button>
                    </p>
                    <textarea readonly v-html="codes[item]?.value" :id="item"
                        class="w-full textarea-code border-none focus:outline-none"></textarea>
                </el-tab-pane>
            </el-tabs>
        </el-dialog>
    </div>
</template>

<script lang="ts" setup>
import { watch, ref } from 'vue';
import { previewCode } from '@/api/develop/genTable';
import { AppOption } from '#/data';
import { ElMessage } from 'element-plus';

type previewCodeResult = {
    entityClass: AppOption;
    iService: AppOption;
    service: AppOption;
    entityDto: AppOption;
    entitySearchDto: AppOption;
    entityResultDto: AppOption;
    controller: AppOption;
}

const props = defineProps({
    genTableId: {
        type: String,
        required: true
    },
    visible: {
        type: Boolean,
        default: false
    }
})
const dialogVisible = ref<boolean>(false);
const emit = defineEmits(['update:visible']);
const activeName = ref('entityClass');
const codes = ref<previewCodeResult>();

const getPreviewCode = () => {
    previewCode({ genTableId: props.genTableId }).then(res => {
        codes.value = {
            entityClass: res.data.entityClass,
            iService: res.data.iService,
            service: res.data.service,
            entityDto: res.data.entityDto,
            entitySearchDto: res.data.entitySearchDto,
            entityResultDto: res.data.entityResultDto,
            controller: res.data.controller
        }
    })
}
const handleClose = () => {
    dialogVisible.value = false;
    emit('update:visible', false);
}
const copyCode = (key: string) => {
    // 获取textarea元素
    var textarea = document.getElementById(key) as HTMLTextAreaElement;
    if (!textarea) return;
    // 选择textarea中的内容
    textarea.select();
    // 尝试复制选中的内容到剪贴板
    try {
        var successful = document.execCommand('copy');
        if (successful) {
            ElMessage.success('复制成功！')
        } else {
            ElMessage.error('复制失败！')
        }
    } catch (err: any) {
        console.error(err)
    }
}

watch(() => props.visible, (val) => {
    dialogVisible.value = val;
    if (val) {
        getPreviewCode();
    }
})
</script>

<style scoped>
.textarea-code {
    min-height: 480px;
    resize: none;
}
</style>