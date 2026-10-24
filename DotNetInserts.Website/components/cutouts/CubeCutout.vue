<script setup lang="ts">
import type { CubeCutout } from "~/services/InsertModels";
import CutoutFields from "./CutoutFields.vue";
import NumberInput from "~/components/shared/NumberInput.vue";

defineEmits<{
  (e: "close"): void;
}>();

defineProps<{
  cutout: CubeCutout;
}>();
</script>

<template>
  <div class="">
    <div class="flex gap-4 mb-4">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
        </svg>
      </button>
      <h2 class="text-lg font-medium text-gray-900">Cube Cutout</h2>
    </div>

    <div class="space-y-4">
      <CutoutFields :cutout="cutout" />

      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Round radius</label>
        <NumberInput v-model="cutout.roundRadius" />
        <p class="mt-1 text-xs text-gray-500">Leave empty for square corners.</p>
      </div>
    </div>
  </div>
</template>
