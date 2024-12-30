<template>
    <div>
        <el-dialog v-model="dialogVisible" :close-on-click-modal="false" @close="handleClose"
            :close-on-press-escape="false" title="预览" width="80%" append-to-body>
            <el-tabs v-model="activeName">
                <el-tab-pane label="实体类" name="entityClass">
                    <div v-html="codes.entityClass"></div>
                </el-tab-pane>
            </el-tabs>
        </el-dialog>
    </div>
</template>

<script lang="ts" setup>
import { watch, ref } from 'vue';
import { previewCode } from '@/api/develop/genTable';

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
const codes = ref<any>();

const getPreviewCode = () => {
    previewCode({ genTableId: props.genTableId }).then(res => {
        codes.value = res.data;
    })
}
const handleClose = () => {
    dialogVisible.value = false;
    emit('update:visible', false);
}

watch(() => props.visible, (val) => {
    dialogVisible.value = val;
    if (val) {
        getPreviewCode();
    }
})
</script>