<script setup lang="ts">
import type { Cutout } from "~/services/InsertModels";
import CubeCutout from "./CubeCutout.vue";
import { BuilderOverlayHelper } from "../BuilderOverlayHelper";
import ScoopCutoutBuilder from "./ScoopCutoutBuilder.vue";
import CircleCutoutBuilder from "./CircleCutoutBuilder.vue";
import { PhTrash } from "@phosphor-icons/vue";

const props = defineProps<{
  cutout: Cutout;
}>();

defineEmits<{
  (e: "remove"): void;
}>();

const components: { [key: string]: Component } = {
  cube: CubeCutout,
  scoop: ScoopCutoutBuilder,
  circle: CircleCutoutBuilder,
};

function openPanel() {
  BuilderOverlayHelper.Overlays.value.push({
    component: components[props.cutout.$type],
    props: {
      cutout: props.cutout,
    },
  });
}
</script>

<template>
  <div class="flex gap-2 items-center">
    <button
      @click="openPanel"
      class="grow flex gap-2 items-center px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 text-left"
    >
      <span class="capitalize">{{ cutout.$type }}</span>
      <span class="text-gray-400">·</span>
      <span class="capitalize text-gray-500">{{ cutout.side }}</span>
      <span
        :class="[
          'ml-auto px-2 py-0.5 rounded-full text-xs capitalize',
          cutout.mode === 'through'
            ? 'bg-blue-100 text-blue-700'
            : 'bg-gray-100 text-gray-600',
        ]"
      >
        {{ cutout.mode }}
      </span>
    </button>
    <button
      @click="$emit('remove')"
      class="p-2 rounded-lg text-gray-500 hover:bg-gray-100 hover:text-red-600"
      title="Remove cutout"
    >
      <PhTrash />
    </button>
  </div>
</template>
