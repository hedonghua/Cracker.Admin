<template>
    <div>
        <re-table row-key="id" :request="request" :columns="columns" :enabled-filter="true" :filters="filters"
            ref="tableRef">
            <template #toolbar>
            </template>
        </re-table>
    </div>
</template>

<script setup lang="ts">
import { useTable } from "./hook.tsx"

const {
    request,
    columns,
    filters,
    tableRef
} = useTable();
</script>