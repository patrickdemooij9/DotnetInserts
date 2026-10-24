<script setup lang="ts">
import NumberInput from "~/components/shared/NumberInput.vue";

/**
 * The shared editor behind "align to component" and "align to part": a direction, what
 * to align to, and the gap. Only the axis the direction points along uses the gap - the
 * others just line up with the target, exactly as the server resolves them.
 */
const props = defineProps<{
  align: { alignType: number; gap: number } & Record<string, any>;
  /** Which property on `align` holds the target: `componentId` or `partId`. */
  idKey: string;
  targets: { id: string; name: string }[];
  axis: "x" | "y" | "z";
  placeholder: string;
}>();

const items = [
  { title: "Top of", value: 0 },
  { title: "Right of", value: 1 },
  { title: "Bottom of", value: 2 },
  { title: "Left of", value: 3 },
];

const pointsAlong = computed(() => {
  if (props.axis === "x") return props.align.alignType === 1 || props.align.alignType === 3;
  if (props.axis === "y") return props.align.alignType === 0 || props.align.alignType === 2;
  return false;
});
</script>

<template>
  <div class="space-y-2">
    <div class="flex gap-2">
      <select
        v-model="align.alignType"
        class="w-1/3 px-2 py-1 rounded-md border border-gray-300 text-gray-500 focus:border-blue-500 focus:ring-blue-500"
      >
        <option v-for="item in items" :key="item.value" :value="item.value">
          {{ item.title }}
        </option>
      </select>
      <select
        v-model="align[idKey]"
        class="w-2/3 px-2 py-1 rounded-md border border-gray-300 text-gray-500 focus:border-blue-500 focus:ring-blue-500"
      >
        <option value="" disabled>{{ placeholder }}</option>
        <option v-for="target in targets" :key="target.id" :value="target.id">
          {{ target.name }}
        </option>
      </select>
    </div>
    <NumberInput v-if="pointsAlong" label="Gap" v-model="align.gap" />
    <p v-else class="text-xs text-gray-500">Lines up with the target on this axis.</p>
  </div>
</template>
