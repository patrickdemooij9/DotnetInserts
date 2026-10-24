<script setup lang="ts">
import type { BoxPart, PartComponent } from "~/services/InsertModels";
import SizeBuilder from "../size/SizeBuilder.vue";
import type ItemListConfig from "~/models/ItemListConfig";
import type ItemListItem from "~/models/ItemListItem";
import ItemList from "~/components/shared/ItemList.vue";
import {
  OpenComponentPanel,
} from "../BuilderOverlayHelper";
import { PhArrowLeft, PhCopy, PhTrash } from "@phosphor-icons/vue";
import TextInput from "~/components/shared/TextInput.vue";
import NumberInput from "~/components/shared/NumberInput.vue";
import LabelBuilder from "./LabelBuilder.vue";
import { cloneComponent } from "~/services/Cloning";
import LidBuilder from "./LidBuilder.vue";

const props = defineProps<{
  part: BoxPart;
}>();

// labels is optional in the schema, so materialise it here rather than making
// every file that predates labels carry an empty array.
if (!props.part.labels) props.part.labels = [];

const showLid = ref<boolean>(props.part.lid !== undefined);
watch(showLid, () => {
  if (showLid.value) {
    props.part.lid = {
      wallSize: 3,
      topSize: 3
    }
  }else{
    delete props.part.lid;
  }
})

const listConfig = computed<ItemListConfig>(() => ({
  items: props.part.components.map<ItemListItem>((comp) => ({
    alias: comp!.id,
    label: comp!.name,
  })),
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
    {
      alias: 'repeatable',
      label: 'Repeatable'
    },
    {
      alias: "svg",
      label: "SVG image",
    }
  ],
  newButtonLabel: "Add new component",
}));

defineEmits<{
  (e: "close"): void;
}>();

function addPart(type: string) {
  // An engraving has no useful zero: a depth of 0 removes nothing and a footprint of 0
  // leaves the artwork at whatever size the file happens to be. Every other component
  // starts empty because its size is dictated by what goes in it.
  const isSvg = type === "svg";
  const defaultSize = isSvg ? { x: 30, y: 30, z: 0.6 } : { x: 0, y: 0, z: 0 };

  props.part.components.push({
    // @ts-expect-error
    $type: type,
    id: crypto.randomUUID(),
    name: "New Component",
    ...(isSvg ? { imagePath: "", anchor: "bottom", mirrored: false, export: true } : {}),
    size: {
      x: {
        $type: "fixed",
        size: defaultSize.x,
      },
      y: {
        $type: "fixed",
        size: defaultSize.y,
      },
      z: {
        $type: "fixed",
        size: defaultSize.z,
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
  });
}

function removePart(partComponent: PartComponent) {
  const index = props.part.components.indexOf(partComponent);
  if (index > -1) {
    props.part.components.splice(index, 1);
  }
}

function handleListItemClick(item: ItemListItem) {
  const partComponent = props.part.components.find(
    (comp) => comp?.id == item.alias
  );
  if (!partComponent) return;

  OpenComponentPanel(partComponent);
}

function handleListItemRemove(item: ItemListItem) {
  const partComponent = props.part.components.find(
    (comp) => comp?.id == item.alias
  );
  if (!partComponent) return;

  removePart(partComponent);
}

function handleListItemDuplicate(item: ItemListItem){
  const partComponent = props.part.components.find(
    (comp) => comp?.id == item.alias
  );
  if (!partComponent) return;

  const index = props.part.components.indexOf(partComponent);
  props.part.components.splice(index + 1, 0, cloneComponent(partComponent));
}
</script>

<template>
  <div class="space-y-4">
    <div class="flex gap-4 items-center">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <PhArrowLeft/>
      </button>
      <div class="grow">
        <TextInput label="Part name" v-model="part.name"/>
      </div>
    </div>

    <SizeBuilder label="Size" :size="part.size" />
    <SizeBuilder label="Location" :size="part.location" />

    <NumberInput label="Round radius" v-model.number="part.roundRadius!"/>

    <ItemList
      :config="listConfig"
      @add-item="(item) => addPart(item.alias)"
      @click-item="handleListItemClick"
    >
      <template #actions="{ item }">
        <button
          class="cursor-pointer"
          title="Delete"
          @click.stop="() => handleListItemRemove(item)"
        >
          <PhTrash />
        </button>
        <button
          class="cursor-pointer"
          title="Clone"
          @click.stop="() => handleListItemDuplicate(item)"
        >
          <PhCopy />
        </button>
      </template>
    </ItemList>

    <LabelBuilder :labels="part.labels!" />

    <label>Show lid details</label>
    <input type="checkbox" v-model="showLid"/>
    <div v-if="showLid">
      <LidBuilder :lid="part.lid!" />
    </div>
  </div>
</template>
