<script setup lang="ts">
import { PhArrowLeft } from "@phosphor-icons/vue";
import type { CircleComponent, CubeCutout } from "~/services/InsertModels";
import TextInput from "~/components/shared/TextInput.vue";

const props = defineProps<{
  partComponent: CircleComponent;
}>();
</script>

<template>
  <div>
    <div class="flex gap-4 mb-2">
      <button @click="$emit('close')" class="h-full">
        <PhArrowLeft/>
      </button>
      <TextInput label="Component name" v-model="partComponent.name"></TextInput>
    </div>
    <p>Size</p>
    <SizeBuilder :size="partComponent.size"> </SizeBuilder>

    <p>Location</p>
    <SizeBuilder :size="partComponent.location"> </SizeBuilder>
  </div>
</template>
