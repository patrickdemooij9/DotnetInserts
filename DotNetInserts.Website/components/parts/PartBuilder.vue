<script setup lang="ts">
import BoxPartBuilder from "./BoxPartBuilder.vue";
import { BuilderOverlayHelper } from "../BuilderOverlayHelper";
import type { Part } from "~/services/InsertModels";
import type { Component } from "vue";
import CardHolderBuilder from "./CardHolderBuilder.vue";
import { PhTrash } from "@phosphor-icons/vue";

const props = defineProps<{
  part: Part;
}>();

const emit = defineEmits<{
  (e: "removePart"): void;
}>();

const components: { [key: string]: Component } = {
  "box": BoxPartBuilder,
  "cardHolder": CardHolderBuilder,
};

function openPartPanel() {
  BuilderOverlayHelper.Overlays.value.push({
    component: components[props.part!.$type],
    props: {
      part: props.part,
    },
  });
}

function removePart(event: Event) {
  event.stopPropagation();
  emit("removePart");
}
</script>

<template>
  <button 
    @click="openPartPanel"
    class="flex align-middle justify-between w-full px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 group"
  >
  <span>{{ part?.name }}</span>
    

    <div class="hidden group-hover:flex gap-2">
      <button class="pointer" @click="removePart">
        <PhTrash/>
      </button>
    </div>
  </button>
</template>
