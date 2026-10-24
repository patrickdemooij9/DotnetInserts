<script setup lang="ts">
import type { Cutout, CutoutMode, CutoutSide } from "~/services/InsertModels";
import SizeBuilder from "../size/SizeBuilder.vue";

const props = defineProps<{
  cutout: Cutout;
}>();

const sides: { title: string; value: CutoutSide }[] = [
  { title: "Top", value: "top" },
  { title: "Right", value: "right" },
  { title: "Bottom", value: "bottom" },
  { title: "Left", value: "left" },
];

const modes: { title: string; value: CutoutMode; hint: string }[] = [
  {
    title: "Through",
    value: "through",
    hint: "Cuts the full height and breaks through the floor. Use this for a finger scoop — size Z is only how far it overshoots past the floor.",
  },
  {
    title: "Notch",
    value: "notch",
    hint: "Bites down from the top rim by size Z and leaves the floor intact. Wall detailing, not for getting contents out.",
  },
];

const modeHint = computed(
  () => modes.find((m) => m.value === props.cutout.mode)?.hint ?? ""
);

function ensureOffset() {
  if (!props.cutout.offset) {
    props.cutout.offset = {
      x: { $type: "fixed", size: 0 },
      y: { $type: "fixed", size: 0 },
      z: { $type: "fixed", size: 0 },
    };
  }
}
</script>

<template>
  <div class="space-y-4">
    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Side</label>
      <div class="grid grid-cols-4 gap-2">
        <button
          v-for="side in sides"
          :key="side.value"
          @click="cutout.side = side.value"
          :class="[
            'px-3 py-2 text-sm rounded-md border',
            cutout.side === side.value
              ? 'bg-blue-600 text-white border-blue-600'
              : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-50',
          ]"
        >
          {{ side.title }}
        </button>
      </div>
    </div>

    <div>
      <label class="block text-sm font-medium text-gray-700 mb-1">Depth</label>
      <div class="grid grid-cols-2 gap-2">
        <button
          v-for="mode in modes"
          :key="mode.value"
          @click="cutout.mode = mode.value"
          :class="[
            'px-3 py-2 text-sm rounded-md border',
            cutout.mode === mode.value
              ? 'bg-blue-600 text-white border-blue-600'
              : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-50',
          ]"
        >
          {{ mode.title }}
        </button>
      </div>
      <p class="mt-1 text-xs text-gray-500">{{ modeHint }}</p>
    </div>

    <div>
      <h3 class="text-lg font-medium text-gray-900 mb-2">Size</h3>
      <p class="mb-2 text-xs text-gray-500">
        X runs along the wall, Z is the cut height. Y is depth measured inward
        from the outside of the part — set it to the wall thickness to remove
        just the wall, or larger to reach into the compartment.
      </p>
      <SizeBuilder :size="cutout.size" />
    </div>

    <div>
      <h3 class="text-lg font-medium text-gray-900 mb-2">Offset</h3>
      <p class="mb-2 text-xs text-gray-500">
        Nudge from the centre of the wall. Leave at zero to stay centred.
      </p>
      <button
        v-if="!cutout.offset"
        @click="ensureOffset"
        class="w-full px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50"
      >
        Add offset
      </button>
      <SizeBuilder v-else :size="cutout.offset" />
    </div>
  </div>
</template>
