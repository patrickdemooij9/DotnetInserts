<script setup lang="ts">
import type ItemListConfig from "~/models/ItemListConfig";
import type ItemListItem from "~/models/ItemListItem";

defineProps<{
  config: ItemListConfig;
}>();

const emit = defineEmits<{
  (e: "add-item", item: ItemListItem): void;
  (e: "click-item", item: ItemListItem): void;
}>();

const showMenu = ref(false);
const menuRoot = ref<HTMLElement | null>(null);

function closeOnOutsideClick(event: MouseEvent) {
  if (showMenu.value && !menuRoot.value?.contains(event.target as Node)) showMenu.value = false;
}

onMounted(() => document.addEventListener("click", closeOnOutsideClick));
onBeforeUnmount(() => document.removeEventListener("click", closeOnOutsideClick));
</script>

<template>
  <div>
    <div>
      <!-- A row rather than a <button>: the action buttons inside it are buttons too, and
           a button inside a button is invalid HTML that browsers handle inconsistently. -->
      <div
        v-for="item in config.items"
        :key="item.alias"
        role="button"
        tabindex="0"
        @click="emit('click-item', item)"
        @keydown.enter.self="emit('click-item', item)"
        @keydown.space.self.prevent="emit('click-item', item)"
        class="flex items-center justify-between w-full px-4 py-2 mb-2 text-sm font-medium cursor-pointer text-gray-700 bg-white border border-gray-300 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 group"
      >
        <span class="truncate" :class="{ 'opacity-40': item.hidden }">{{ item.label || "(unnamed)" }}</span>

        <!-- Always visible on touch screens, where there is no hover to reveal them. -->
        <div
          class="flex gap-3 shrink-0 ml-2 [@media(hover:hover)]:opacity-0 group-hover:opacity-100 group-focus-within:opacity-100"
        >
          <slot name="actions" :item="item"></slot>
        </div>
      </div>
    </div>

    <div
      ref="menuRoot"
      class="relative"
      v-if="!config.maxItems || config.items.length < config.maxItems"
    >
      <button
        @click="showMenu = !showMenu"
        :aria-expanded="showMenu"
        class="w-full px-4 py-2 cursor-pointer text-sm font-medium text-gray-700 bg-white border border-dashed border-gray-300 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
      >
        + {{ config.newButtonLabel }}
      </button>
      <div
        v-if="showMenu"
        class="absolute z-10 mt-1 w-full bg-white shadow-lg border border-gray-200 rounded-md py-1"
      >
        <button
          v-for="item in config.addItems"
          :key="item.alias"
          @click="
            emit('add-item', item);
            showMenu = false;
          "
          class="block cursor-pointer w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 text-left capitalize"
        >
          {{ item.label }}
        </button>
      </div>
    </div>
  </div>
</template>
