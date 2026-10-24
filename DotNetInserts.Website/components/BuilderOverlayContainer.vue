<script setup lang="ts">
import { BuilderOverlayHelper } from "./BuilderOverlayHelper";

function closeOverlay(index: number) {
  BuilderOverlayHelper.Overlays.value.splice(index, 1);
}
</script>

<template>
  <TransitionGroup name="slide" tag="div">
    <div
      v-for="(overlay, i) in BuilderOverlayHelper.Overlays.value"
      :key="i"
      class="absolute inset-0 px-4 py-2 bg-white overflow-y-auto"
    >
      <component :is="overlay.component" v-bind="overlay.props" @close="closeOverlay(i)"></component>
    </div>
  </TransitionGroup>
</template>

<style lang="css" scoped>
.slide-enter-active,
.slide-leave-active {
  transition: transform 0.3s;
}
.slide-enter-from {
  transform: translateX(-100%);
}
.slide-enter-to {
  transform: translateX(0);
}
.slide-leave-from {
  transform: translateX(0);
}
.slide-leave-to {
  transform: translateX(-100%);
}
</style>
