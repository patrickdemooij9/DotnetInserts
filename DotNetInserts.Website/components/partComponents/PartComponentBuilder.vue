<script setup lang="ts">
import type { PartComponent } from "~/services/InsertModels";
import BoxComponentBuilder from "./BoxComponentBuilder.vue";
import { BuilderOverlayHelper } from "../BuilderOverlayHelper";
import type { Component } from "vue";
import CircleComponentBuilder from "./CircleComponentBuilder.vue";
import CardTrayComponentBuilder from "./CardTrayComponentBuilder.vue";
import SvgComponentBuilder from "./SvgComponentBuilder.vue";

const props = defineProps<{
  partComponent: PartComponent;
}>();

const components: { [key: string]: Component } = {
  "box": BoxComponentBuilder,
  "circle": CircleComponentBuilder,
  "cardTray": CardTrayComponentBuilder,
  "svg": SvgComponentBuilder
};

function openPartPanel() {
  BuilderOverlayHelper.Overlays.value.push({
    component: components[props.partComponent!.$type],
    props: {
      partComponent: props.partComponent,
    },
  });
}
</script>

<template>
  <v-btn variant="outlined" class="w-full" @click="openPartPanel">
    {{ partComponent?.name }}
  </v-btn>
</template>
