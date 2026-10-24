<script setup lang="ts">
import type { CircleCutout } from "~/services/InsertModels";
import CutoutFields from "./CutoutFields.vue";

defineEmits<{
  (e: "close"): void;
}>();

defineProps<{
  cutout: CircleCutout;
}>();
</script>

<template>
  <div>
    <div class="flex gap-4 mb-4">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
        </svg>
      </button>
      <h2 class="text-lg font-medium text-gray-900">Circle Cutout</h2>
    </div>

    <div class="space-y-4">
      <p class="text-xs text-gray-500">
        Round when size X and Y match, elliptical when they differ.
      </p>
      <CutoutFields :cutout="cutout" />
    </div>
  </div>
</template>
