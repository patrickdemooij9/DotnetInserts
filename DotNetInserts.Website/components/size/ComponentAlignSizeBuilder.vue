<script setup lang="ts">
import type { ComponentAlignSize } from "~/services/InsertModels";
import type { LocationOwner } from "~/services/ModelLookup";
import AlignFields from "./AlignFields.vue";

const props = defineProps<{
  size: ComponentAlignSize;
  owner: LocationOwner;
  axis: "x" | "y" | "z";
}>();

const siblings = computed(() =>
  props.owner.part.components
    .filter((c) => c !== null && c.id !== props.owner.component.id)
    .map((c) => ({ id: c!.id, name: c!.name }))
);
</script>

<template>
  <AlignFields
    :align="size"
    id-key="componentId"
    :targets="siblings"
    :axis="axis"
    placeholder="Select component"
  />
</template>
