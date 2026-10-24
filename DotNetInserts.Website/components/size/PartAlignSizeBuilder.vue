<script setup lang="ts">
import type { PartAlignSize, Part } from "~/services/InsertModels";
import AlignFields from "./AlignFields.vue";
import { useInsertStore } from "~/stores/InsertStore";

const props = defineProps<{
  size: PartAlignSize;
  owner: Part;
  axis: "x" | "y" | "z";
}>();

const store = useInsertStore();
const others = computed(() =>
  (store.model.parts ?? [])
    .filter((p) => p !== null && p.id !== props.owner!.id)
    .map((p) => ({ id: p!.id, name: p!.name }))
);
</script>

<template>
  <AlignFields
    :align="size"
    id-key="partId"
    :targets="others"
    :axis="axis"
    placeholder="Select part"
  />
</template>
