<script setup lang="ts">
import { PhArrowLeft } from "@phosphor-icons/vue";
import type ItemListConfig from "~/models/ItemListConfig";
import type ItemListItem from "~/models/ItemListItem";
import type { RepeatableComponent } from "~/services/InsertModels";
import ItemList from "~/components/shared/ItemList.vue";
import NumberInput from "~/components/shared/NumberInput.vue";
import TextInput from "~/components/shared/TextInput.vue";
import { OpenComponentPanel } from "../BuilderOverlayHelper";

const props = defineProps<{
  partComponent: RepeatableComponent;
}>();

const listConfig = computed<ItemListConfig>(() => ({
  items: props.partComponent.model ? [{
    alias: props.partComponent.model.id,
    label: props.partComponent.model.name
  }] : [],
  addItems: [
    {
      alias: "box",
      label: "Box",
    },
    {
      alias: "circle",
      label: "Circle",
    },
    {
      alias: "cardTray",
      label: "Card tray",
    },
  ],
  newButtonLabel: "Add new component",
  maxItems: 1
}));

function addPart(type: string) {
  props.partComponent.model = {
    // @ts-expect-error
    $type: type,
    id: crypto.randomUUID(),
    name: "New Component",
    size: {
      x: {
        $type: "fixed",
        size: 0,
      },
      y: {
        $type: "fixed",
        size: 0,
      },
      z: {
        $type: "fixed",
        size: 0,
      },
    },
    location: {
      x: {
        $type: "fixed",
        size: 0,
      },
      y: {
        $type: "fixed",
        size: 0,
      },
      z: {
        $type: "fixed",
        size: 0,
      },
    },
  };
}

function handleListItemClick(item: ItemListItem) {
  OpenComponentPanel(props.partComponent.model!);
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
    
    <NumberInput label="amount X" v-model="partComponent.amountX"></NumberInput>

    <NumberInput label="amount Y" v-model="partComponent.amountY"></NumberInput>

    <ItemList :config="listConfig" @click-item="handleListItemClick" @add-item="(type) => addPart(type.alias)">

    </ItemList>
  </div>
</template>
