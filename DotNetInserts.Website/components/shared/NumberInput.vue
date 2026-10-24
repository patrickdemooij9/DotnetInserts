<script setup lang="ts">
// Provide a numeric model (v-model) and a label prop.
defineModel<number>();

const props = defineProps<{
  label?: string
}>();

const emit = defineEmits<(e: 'update:modelValue', value: number | undefined) => void>();

function onInput(e: Event) {
  const v = (e.target as HTMLInputElement).value;
  // If the field is empty, emit undefined so parent can clear the value.
  if (v === '') {
    emit('update:modelValue', undefined);
    return;
  }

  const num = Number(v);
  // If parsing failed, emit undefined instead of NaN.
  if (Number.isNaN(num)) {
    emit('update:modelValue', undefined);
    return;
  }

  emit('update:modelValue', num);
}
</script>

<template>
  <div class="flex gap-2 items-center px-2 py-1 rounded-md border border-gray-300 focus-within:ring-2 focus-within:ring-blue-500 flex-1 min-w-0">
    <label class="shrink-0 block text-sm font-medium text-gray-500" v-if="label">{{ props.label }}</label>
    <input
      type="number"
      class="w-full min-w-0 flex-1 bg-transparent focus:outline-none [appearance:textfield] [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:appearance-none"
      placeholder="0"
      :value="modelValue ?? ''"
      @input="onInput"
      step="any"
    />
    <span class="block text-sm font-medium text-gray-500">mm</span>
  </div>
</template>