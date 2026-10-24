<script setup lang="ts">
import { PhArrowLeft } from "@phosphor-icons/vue";
import type { CardTrayComponent } from "~/services/InsertModels";
import NumberInput from "~/components/shared/NumberInput.vue";
import TextInput from "~/components/shared/TextInput.vue";

const props = defineProps<{
  partComponent: CardTrayComponent;
}>();

if (props.partComponent.dividerSize === undefined) {
  props.partComponent.dividerSize = { X: 0, Y: 0, Z: 0 };
}
if (props.partComponent.dividerBottomCutoutSize === undefined) {
  props.partComponent.dividerBottomCutoutSize = { X: 0, Y: 0, Z: 0 };
}
</script>

<template>
  <div>
    <div class="flex gap-4 items-center mb-4">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <PhArrowLeft />
      </button>
      <div class="grow">
        <TextInput label="Component name" v-model="partComponent.name"></TextInput>
      </div>
    </div>

    <SizeBuilder class="mb-4" label="Size" :size="partComponent.size"> </SizeBuilder>

    <SizeBuilder class="mb-4" label="Location" :size="partComponent.location"> </SizeBuilder>

    <div class="mb-4">
      <label class="block text-sm font-medium text-gray-500"
        >Divider size</label
      >
      <div class="flex gap-2">
        <NumberInput label="X" v-model="partComponent.dividerSize.X" />
        <NumberInput label="Y" v-model="partComponent.dividerSize.Y" />
        <NumberInput label="Z" v-model="partComponent.dividerSize.Z" />
      </div>
    </div>

    <NumberInput class="mb-4" label="Wall size" v-model="partComponent.dividerWallSize" />

    <div class="mb-4">
      <label class="block text-sm font-medium text-gray-500"
        >Divider cutout bottom</label
      >
      <div class="flex gap-2">
        <NumberInput
          label="X"
          v-model="partComponent.dividerBottomCutoutSize.X"
        />
        <NumberInput
          label="Y"
          v-model="partComponent.dividerBottomCutoutSize.Y"
        />
        <NumberInput
          label="Z"
          v-model="partComponent.dividerBottomCutoutSize.Z"
        />
      </div>
    </div>

    <NumberInput label="Slots" v-model="partComponent.numberOfSlots" />
  </div>
</template>
