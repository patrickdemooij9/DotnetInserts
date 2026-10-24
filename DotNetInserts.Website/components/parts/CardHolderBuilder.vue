<script setup lang="ts">
import type { CardHolderPart } from '~/services/InsertModels';
import LabelBuilder from './LabelBuilder.vue';


defineEmits<{
  (e: "close"): void;
}>();

const props = defineProps<{
    part: CardHolderPart
}>();

// labels is optional in the schema, so materialise it here rather than making
// every file that predates labels carry an empty array.
if (!props.part.labels) props.part.labels = [];
</script>

<template>
  <div class="space-y-4">
    <div class="flex gap-4 items-center">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
        </svg>
      </button>
      <div class="grow">
        <label class="block text-sm font-medium text-gray-700">Part name</label>
        <input 
          type="text" 
          v-model="part.name"
          class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
        >
      </div>
    </div>

    <div>
      <h3 class="text-sm font-medium text-gray-700 mb-2">Size</h3>
      <SizeBuilder :size="part.size" />
    </div>

    <div>
      <h3 class="text-sm font-medium text-gray-700 mb-2">Inner size</h3>
      <SizeBuilder :size="part.innerSize" />
    </div>

    <div>
      <h3 class="text-sm font-medium text-gray-700 mb-2">Location</h3>
      <SizeBuilder :size="part.location" />
    </div>

    <div class="flex items-center">
      <input
        type="checkbox"
        v-model="part.roundedCorners"
        class="h-4 w-4 rounded border-gray-300 text-blue-600 focus:ring-blue-500"
      >
      <label class="ml-2 text-sm text-gray-700">Rounded corners</label>
    </div>

    <LabelBuilder :labels="part.labels!" />
  </div>
</template>
