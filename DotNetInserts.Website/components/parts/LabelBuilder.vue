<script setup lang="ts">
import type { Label, SideName } from "~/services/InsertModels";
import TextInput from "~/components/shared/TextInput.vue";
import NumberInput from "~/components/shared/NumberInput.vue";
import { PhTrash } from "@phosphor-icons/vue";

/**
 * Labels are edited inline rather than through the overlay panel: there are only
 * ever a handful per part and each is six small fields, so pushing them onto the
 * panel stack costs more clicks than it saves.
 */
const props = defineProps<{
  labels: Label[];
}>();

// "bottom" is the front face, matching the cutout side names.
const sides: { value: SideName | ""; label: string }[] = [
  { value: "bottom", label: "Front" },
  { value: "top", label: "Back" },
  { value: "left", label: "Left" },
  { value: "right", label: "Right" },
  { value: "", label: "Floor" },
];

function add() {
  props.labels.push({
    text: "Label",
    side: "bottom",
    size: 8,
    depth: 0.6,
    along: 0,
    up: 0,
  });
}

function remove(label: Label) {
  const index = props.labels.indexOf(label);
  if (index > -1) {
    props.labels.splice(index, 1);
  }
}

// An empty select means the floor, which the schema spells as no side at all.
function setSide(label: Label, value: string) {
  if (value === "") {
    delete label.side;
  } else {
    label.side = value as SideName;
  }
}
</script>

<template>
  <div class="space-y-2">
    <h3 class="text-sm font-medium text-gray-700">Labels</h3>

    <div
      v-for="(label, index) in labels"
      :key="index"
      class="space-y-2 p-2 border border-gray-300 rounded-md"
    >
      <div class="flex gap-2 items-end">
        <div class="grow">
          <TextInput label="Text" v-model="label.text" />
        </div>
        <button
          class="cursor-pointer p-2 rounded-md hover:bg-gray-100"
          :aria-label="`Remove label ${label.text}`"
          @click="remove(label)"
        >
          <PhTrash />
        </button>
      </div>

      <div class="flex flex-col gap-2">
        <label class="block text-sm font-medium text-gray-500">Face</label>
        <select
          class="px-2 py-1 rounded-md border border-gray-300 w-full"
          :value="label.side ?? ''"
          @change="setSide(label, ($event.target as HTMLSelectElement).value)"
        >
          <option v-for="side in sides" :key="side.label" :value="side.value">
            {{ side.label }}
          </option>
        </select>
      </div>

      <NumberInput label="Cap height" v-model.number="label.size!" />
      <NumberInput label="Depth" v-model.number="label.depth!" />
      <NumberInput label="Nudge across" v-model.number="label.along!" />
      <NumberInput label="Nudge up" v-model.number="label.up!" />
    </div>

    <button
      class="w-full px-4 py-2 cursor-pointer text-sm font-medium text-gray-700 bg-white border border-dotted border-gray-300 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
      @click="add"
    >
      Add label
    </button>
  </div>
</template>
