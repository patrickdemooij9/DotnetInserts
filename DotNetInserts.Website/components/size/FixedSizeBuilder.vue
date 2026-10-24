<script setup lang="ts">
import type { FixedSize } from "~/services/InsertModels";
import NumberInput from "~/components/shared/NumberInput.vue";

defineProps<{
  size: FixedSize;
  label: string;
}>();
</script>

<template>
  <NumberInput v-model="size.size" />
</template>
