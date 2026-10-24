<script setup lang="ts">
import type { ComputedSize } from "~/services/InsertModels";
import { referenceSuggestions, type ReferenceSuggestion } from "~/services/ModelLookup";
import { useInsertStore } from "~/stores/InsertStore";

const props = defineProps<{
  size: ComputedSize;
  label: string;
}>();

const store = useInsertStore();
const input = ref<HTMLInputElement | null>(null);

// The `#` reference being typed at the caret: where it starts and what follows the `#`.
const token = ref<{ start: number; query: string } | null>(null);
const activeIndex = ref(0);

const matches = computed<ReferenceSuggestion[]>(() => {
  if (!token.value) return [];
  const query = token.value.query.toLowerCase();
  const all = referenceSuggestions(store.model);
  // Prefix matches first, so "Tr" puts "Tray.X" above "SmallTray.X".
  const prefix = all.filter((s) => s.path.toLowerCase().startsWith(query));
  const inner = all.filter(
    (s) => !s.path.toLowerCase().startsWith(query) && s.path.toLowerCase().includes(query)
  );
  return [...prefix, ...inner].slice(0, 50);
});

function updateToken() {
  const el = input.value;
  if (!el) return;
  const caret = el.selectionStart ?? el.value.length;
  const before = el.value.slice(0, caret);
  const match = /#(\{[^}#]*|[\w.]*)$/.exec(before);
  token.value = match
    ? { start: match.index, query: match[1].replace(/^\{/, "") }
    : null;
  activeIndex.value = 0;
}

function accept(suggestion: ReferenceSuggestion) {
  const el = input.value;
  if (!el || !token.value) return;
  const value = props.size.expression ?? "";
  const caret = el.selectionStart ?? value.length;
  const after = value.slice(caret);
  // Swallow the rest of a reference the caret is in the middle of, so completing
  // "#Tr|ay.X" does not leave "ay.X" dangling after it.
  const braced = value.startsWith("#{", token.value.start);
  const rest = (braced ? /^[^}#]*\}/ : /^[\w.]*/).exec(after)?.[0] ?? "";

  const head = value.slice(0, token.value.start) + suggestion.insert;
  props.size.expression = head + after.slice(rest.length);
  token.value = null;

  nextTick(() => {
    el.focus();
    el.setSelectionRange(head.length, head.length);
  });
}

function onKeydown(e: KeyboardEvent) {
  if (!token.value || matches.value.length === 0) return;
  switch (e.key) {
    case "ArrowDown":
      activeIndex.value = (activeIndex.value + 1) % matches.value.length;
      break;
    case "ArrowUp":
      activeIndex.value = (activeIndex.value - 1 + matches.value.length) % matches.value.length;
      break;
    case "Enter":
    case "Tab":
      accept(matches.value[activeIndex.value]);
      break;
    case "Escape":
      token.value = null;
      break;
    default:
      return;
  }
  e.preventDefault();
}
</script>

<template>
  <div class="relative">
    <label class="block text-sm font-medium text-gray-700">{{ label }}</label>
    <input
      ref="input"
      type="text"
      v-model="size.expression"
      @input="updateToken"
      @click="updateToken"
      @keyup.left="updateToken"
      @keyup.right="updateToken"
      @keydown="onKeydown"
      @blur="token = null"
      class="px-2 py-1 w-full rounded-md border border-gray-300 text-gray-500 focus:border-blue-500 focus:ring-blue-500"
      placeholder="Enter expression (e.g., #Tray.X - 2), type # for references"
      autocomplete="off"
      spellcheck="false"
    >
    <ul
      v-if="token && matches.length > 0"
      class="absolute z-20 mt-1 w-full max-h-64 overflow-y-auto bg-white shadow-lg rounded-md py-1 border border-gray-200"
    >
      <li
        v-for="(item, i) in matches"
        :key="item.insert"
        @mousedown.prevent="accept(item)"
        @mouseenter="activeIndex = i"
        class="px-3 py-1 cursor-pointer text-sm flex justify-between gap-2"
        :class="i === activeIndex ? 'bg-blue-50 text-blue-700' : 'text-gray-700'"
      >
        <span class="font-mono break-all">{{ item.insert }}</span>
        <span class="text-xs text-gray-400 whitespace-nowrap">{{ item.detail }}</span>
      </li>
    </ul>
  </div>
</template>
