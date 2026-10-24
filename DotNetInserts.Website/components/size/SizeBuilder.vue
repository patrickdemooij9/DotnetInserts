<script setup lang="ts">
import type { SizeAxis, Size3D } from "~/services/InsertModels";
import FixedSizeBuilder from "./FixedSizeBuilder.vue";
import ParentAlignSizeBuilder from "./ParentAlignSizeBuilder.vue";
import ComputedSizeBuilder from "./ComputedSizeBuilder.vue";
import ComponentAlignSizeBuilder from "./ComponentAlignSizeBuilder.vue";
import PartAlignSizeBuilder from "./PartAlignSizeBuilder.vue";
import { findLocationOwner, findPartLocationOwner } from "~/services/ModelLookup";
import { useInsertStore } from "~/stores/InsertStore";
import {
  PhBookBookmark,
} from "@phosphor-icons/vue";

const props = defineProps<{
  size: Size3D;
  label?: string;
}>();

type Axis = "x" | "y" | "z";
type SizeType = "fixed" | "parentAlign" | "computed" | "componentAlign" | "partAlign";

const axes: Axis[] = ["x", "y", "z"];
const alignTypes: SizeType[] = ["componentAlign", "partAlign"];
const openMenuPos = ref(-1);

const store = useInsertStore();
// One of these is set when this edits a location rather than a size, and which one
// decides whether there is anything to align to: siblings inside a part, or the other
// parts of the insert.
const owner = computed(() => findLocationOwner(store.model, props.size));
const partOwner = computed(() => findPartLocationOwner(store.model, props.size));

const components: { [key: string]: Component } = {
  fixed: FixedSizeBuilder,
  parentAlign: ParentAlignSizeBuilder,
  computed: ComputedSizeBuilder,
  componentAlign: ComponentAlignSizeBuilder,
  partAlign: PartAlignSizeBuilder,
};

const selectItems = computed(() => {
  const items: { title: string; value: SizeType }[] = [
    { title: "Fixed", value: "fixed" },
    { title: "Parent align", value: "parentAlign" },
    { title: "Computed", value: "computed" },
  ];
  if (owner.value) items.push({ title: "Align to component", value: "componentAlign" });
  if (partOwner.value) items.push({ title: "Align to part", value: "partAlign" });
  return items;
});

const typeTitles: Record<string, string> = {
  fixed: "Fixed",
  parentAlign: "Parent align",
  computed: "Computed",
  componentAlign: "Align to component",
  partAlign: "Align to part",
};

function setType(axis: Axis, type: SizeType) {
  openMenuPos.value = -1;
  if (props.size[axis].$type === type) return;

  if (type === "componentAlign" || type === "partAlign") {
    // Start from whatever another axis already aligns to: "right of A" on X nearly
    // always wants "right of A" on Y too, to line up with it.
    const sibling = axes.map((a) => props.size[a]).find((s) => s.$type === type) as
      | { alignType: number; componentId?: string; partId?: string }
      | undefined;
    const target = (type === "componentAlign" ? sibling?.componentId : sibling?.partId) ?? "";
    const alignType = sibling?.alignType ?? (axis === "y" ? 0 : 1);

    props.size[axis] =
      type === "componentAlign"
        ? { $type: "componentAlign", componentId: target, alignType, gap: 0 }
        : { $type: "partAlign", partId: target, alignType, gap: 0 };
    return;
  }

  (props.size[axis] as SizeAxis).$type = type;
}

/** Whether the axis' current type still has something to align to. */
function isResolvable(axis: Axis) {
  const type = props.size[axis].$type;
  if (type === "componentAlign") return owner.value !== null;
  if (type === "partAlign") return partOwner.value !== null;
  return true;
}

function extraProps(axis: Axis) {
  const type = props.size[axis].$type;
  if (type === "componentAlign") return { owner: owner.value, axis };
  if (type === "partAlign") return { owner: partOwner.value, axis };
  return {};
}
</script>

<template>
  <div class="space-y-4">
    <div v-for="(axis, pos) in axes" :key="axis" class="flex flex-col gap-2">
      <div class="flex gap-2 align-middle">
        <span class="text-sm font-medium text-gray-500">{{ label }} {{ axis.toUpperCase() }} · {{ typeTitles[size[axis].$type] ?? size[axis].$type }}</span>
        <div class="relative">
          <button
            title="Change how this value is set"
            @click="openMenuPos = openMenuPos === pos ? -1 : pos"
            class="px-2 py-1 cursor-pointer text-sm font-medium text-gray-700 bg-white border border-dotted border-gray-300 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            <PhBookBookmark />
          </button>
          <div
            v-if="openMenuPos === pos"
            class="absolute z-10 mt-1 bg-white shadow-lg rounded-md py-1"
          >
            <button
              v-for="item in selectItems"
              :key="item.value"
              @click="setType(axis, item.value)"
              class="block cursor-pointer w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 text-left whitespace-nowrap"
            >
              {{ item.title }}
            </button>
          </div>
        </div>
      </div>
      <div class="grow">
        <component
          v-if="isResolvable(axis)"
          :is="components[size[axis].$type]"
          :size="size[axis]"
          v-bind="extraProps(axis)"
        ></component>
        <p v-else class="text-xs text-red-600">
          {{ size[axis].$type === "partAlign" ? "Align to part" : "Align to component" }}
          only works on a {{ size[axis].$type === "partAlign" ? "part's" : "component's" }}
          location.
        </p>
      </div>
    </div>
  </div>
</template>
