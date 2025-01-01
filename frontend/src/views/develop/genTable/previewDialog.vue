<template>
    <div>
        <el-dialog v-model="dialogVisible" :close-on-click-modal="false" @close="handleClose"
            :close-on-press-escape="false" title="预览" width="75%" append-to-body>
            <el-tabs v-model="activeName">
                <el-tab-pane :label="codes?.entityClass?.label + '.cs'" name="EntityClass">
                    <textarea readonly v-html="codes?.entityClass?.value"
                        class="w-full textarea-code border-none focus:outline-none"></textarea>
                </el-tab-pane>
                <el-tab-pane :label="codes?.iService?.label + '.cs'" name="IService">
                    <textarea readonly v-html="codes?.iService?.value"
                        class="w-full textarea-code border-none focus:outline-none"></textarea>
                </el-tab-pane>
                <el-tab-pane :label="codes?.service?.label + '.cs'" name="Service">
                    <textarea readonly v-html="codes?.service?.value"
                        class="w-full textarea-code border-none focus:outline-none"></textarea>
                </el-tab-pane>
            </el-tabs>
        </el-dialog>
    </div>
</template>

<script lang="ts" setup>
import { watch, ref, onMounted } from 'vue';
import { previewCode } from '@/api/develop/genTable';
import { AppOption } from '#/data';

type previewCodeResult = {
    entityClass: AppOption;
    iService: AppOption;
    service: AppOption;
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
const activeName = ref('EntityClass');
const codes = ref<previewCodeResult>();

const getPreviewCode = () => {
    previewCode({ genTableId: props.genTableId }).then(res => {
        codes.value = {
            entityClass: res.data.entityClass,
            iService: res.data.iService,
            service: res.data.service,
        }
    })
}
const handleClose = () => {
    dialogVisible.value = false;
    emit('update:visible', false);
}

watch(() => props.visible, (val) => {
    dialogVisible.value = val;
    if (val && !codes.value) {
        getPreviewCode();
    }
})
onMounted(() => {
    if (props.visible) {
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