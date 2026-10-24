<script setup lang="ts">
import { ref } from "vue";
import type {
  BoxComponent,
  Cutout,
  CutoutType,
  Size3D,
} from "~/services/InsertModels";
import CutoutBuilder from "../cutouts/CutoutBuilder.vue";
import { PhArrowLeft } from "@phosphor-icons/vue";
import TextInput from "~/components/shared/TextInput.vue";

const props = defineProps<{
  partComponent: BoxComponent;
}>();

const showCutoutMenu = ref(false);

const cutoutTypes: { title: string; value: CutoutType; hint: string }[] = [
  { title: "Cube", value: "cube", hint: "Rectangular, optionally rounded" },
  { title: "Scoop", value: "scoop", hint: "Rounded trough with curved lead-in" },
  { title: "Circle", value: "circle", hint: "Round or elliptical" },
];

function size(x: number, y: number, z: number): Size3D {
  return {
    x: { $type: "fixed", size: x },
    y: { $type: "fixed", size: y },
    z: { $type: "fixed", size: z },
  };
}

function addCutout(type: CutoutType) {
  // Defaults to a through-cut: that is the finger scoop, and it is what people
  // actually want most of the time.
  const base = {
    side: "top",
    mode: "through",
    size: size(30, 20, 8),
    offset: size(0, 0, 0),
  } as const;

  let cutout: Cutout;
  switch (type) {
    case "scoop":
      cutout = { $type: "scoop", ...base, curveSize: 5 };
      break;
    case "circle":
      cutout = { $type: "circle", ...base };
      break;
    default:
      cutout = { $type: "cube", ...base };
      break;
  }

  if (!props.partComponent.cutouts) {
    props.partComponent.cutouts = [];
  }
  props.partComponent.cutouts.push(cutout);
}

function removeCutout(index: number) {
  props.partComponent.cutouts?.splice(index, 1);
}
</script>

<template>
  <div class="">
    <div class="flex gap-4 items-center">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <PhArrowLeft />
      </button>
      <div class="grow">
        <TextInput label="Component name" v-model="partComponent.name" />
      </div>
    </div>

    <div class="space-y-4">
      <SizeBuilder label="Size" :size="partComponent.size" />

      <SizeBuilder label="Location" :size="partComponent.location" />

      <div class="flex items-center">
        <input
          type="checkbox"
          v-model="partComponent.roundBottom"
          class="h-4 w-4 rounded border-gray-300 text-blue-600 focus:ring-blue-500"
        />
        <label class="ml-2 text-sm text-gray-700">Round bottom</label>
      </div>

      <div>
        <h3 class="text-lg font-medium text-gray-900 mb-2">Cutouts</h3>
        <p class="mb-2 text-xs text-gray-500">
          Applied in order. Each one picks its own wall and depth, and a wall can
          carry as many as you need.
        </p>

        <div class="space-y-2">
          <CutoutBuilder
            v-for="(cutout, index) in partComponent.cutouts ?? []"
            :key="index"
            :cutout="cutout"
            @remove="removeCutout(index)"
          />
        </div>

        <div class="relative mt-2">
          <button
            @click="showCutoutMenu = !showCutoutMenu"
            class="w-full px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Add cutout
          </button>
          <div
            v-if="showCutoutMenu"
            class="absolute z-10 mt-1 w-full bg-white shadow-lg rounded-md py-1"
          >
            <button
              v-for="item in cutoutTypes"
              :key="item.value"
              @click="
                addCutout(item.value);
                showCutoutMenu = false;
              "
              class="block w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 text-left"
            >
              <span class="font-medium">{{ item.title }}</span>
              <span class="block text-xs text-gray-500">{{ item.hint }}</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
