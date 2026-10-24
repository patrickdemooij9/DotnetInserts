<script setup lang="ts">
import { useInsertStore } from '~/stores/InsertStore';

const model = defineModel<string>();

const parts = useInsertStore().parts;
</script>

<template>
<div class="relative">
        <select 
            v-model="model"
            class="w-full px-4 py-2 border rounded-lg bg-white shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 appearance-none"
        >
            <option value="" disabled>Select Part</option>
            <option 
                v-for="part in parts" 
                :key="part?.id" 
                :value="part?.id"
            >
                {{ part?.name }}
            </option>
        </select>
        <div class="absolute inset-y-0 right-0 flex items-center px-2 pointer-events-none">
            <svg class="w-4 h-4 text-gray-400" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clip-rule="evenodd" />
            </svg>
        </div>
    </div>
</template>