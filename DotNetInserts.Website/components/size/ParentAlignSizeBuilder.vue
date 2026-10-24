<script setup lang="ts">
import type { ParentAlignSize } from "~/services/InsertModels";
import PartPicker from "../PartPicker.vue";
import NumberInput from "~/components/shared/NumberInput.vue";

defineProps<{
  size: ParentAlignSize;
  label: string;
}>();

const items = [
  { title: "Top", value: 0 },
  { title: "Right", value: 1 },
  { title: "Bottom", value: 2 },
  { title: "Left", value: 3 },
];
</script>

<template>
  <div class="flex gap-2">
    <div class="w-1/2">
      <select 
        v-model="size.alignType"
        class="px-2 py-1 w-full rounded-md border border-gray-300 text-gray-500 focus:border-blue-500 focus:ring-blue-500"
      >
        <option v-for="item in items" :key="item.value" :value="item.value">
          {{ item.title }}
        </option>
      </select>
    </div>

    <div class="w-1/2">
      <NumberInput label="Gap" v-model="size.gap"/>
    </div>
  </div>
</template>
