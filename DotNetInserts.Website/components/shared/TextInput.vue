<script setup lang="ts">
// Provide a numeric model (v-model) and a label prop.
defineModel<string>();

const props = defineProps<{
  label: string
}>();

const emit = defineEmits<(e: 'update:modelValue', value: string | undefined) => void>();

function onInput(e: Event) {
  const v = (e.target as HTMLInputElement).value;
  // If the field is empty, emit undefined so parent can clear the value.
  if (v === '') {
    emit('update:modelValue', undefined);
    return;
  }

  emit('update:modelValue', v);
}
</script>

<template>
  <div class="flex flex-col gap-2">
    <label class="block text-sm font-medium text-gray-500">{{ props.label }}</label>
    <input
      type="text"
      class="px-2 py-1 rounded-md border border-gray-300 w-full"
      :value="modelValue ?? ''"
      @input="onInput"
    />
  </div>
</template>