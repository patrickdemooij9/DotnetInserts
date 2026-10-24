<script setup lang="ts">
import { PhArrowLeft, PhUploadSimple } from "@phosphor-icons/vue";
import type { SvgComponent } from "~/services/InsertModels";
import TextInput from "~/components/shared/TextInput.vue";
import { apiUrl, UploadImage } from "~/services/InsertService";

const props = defineProps<{
  partComponent: SvgComponent;
}>();

// Both are optional in the schema; materialising them keeps the checkboxes from
// starting out indeterminate on a component loaded from an older file.
props.partComponent.anchor ??= "bottom";
props.partComponent.export ??= true;

const fileInput = ref<HTMLInputElement | null>(null);
const uploading = ref(false);
const uploadError = ref<string | null>(null);

/**
 * The thumbnail. An uploaded file is served back by the API under /images, so its name is
 * enough to find it again when this panel is reopened later. A hand-typed path to a file
 * somewhere else on disk has no URL, and the image simply does not appear.
 */
const previewUrl = computed(() => {
  const path = props.partComponent.imagePath;
  if (!path) return null;
  return apiUrl(`images/${path.split("/").pop()}`);
});

const previewFailed = ref(false);
watch(previewUrl, () => (previewFailed.value = false));

async function onFilePicked(event: Event) {
  const input = event.target as HTMLInputElement;
  const file = input.files?.[0];
  if (!file) return;

  uploading.value = true;
  uploadError.value = null;
  try {
    props.partComponent.imagePath = (await UploadImage(file)).imagePath;
  } catch (error) {
    uploadError.value = error instanceof Error ? error.message : String(error);
  } finally {
    uploading.value = false;
    // Clearing it means picking the same file twice still fires a change event.
    input.value = "";
  }
}
</script>

<template>
  <div class="space-y-4">
    <div class="flex gap-4 items-center">
      <button @click="$emit('close')" class="p-2 rounded-lg hover:bg-gray-100">
        <PhArrowLeft />
      </button>
      <div class="grow">
        <TextInput label="Component name" v-model="partComponent.name" />
      </div>
    </div>

    <div class="space-y-2">
      <label class="block text-sm font-medium text-gray-500">Artwork</label>

      <div class="flex items-center gap-3">
        <img
          v-if="previewUrl && !previewFailed"
          :src="previewUrl"
          alt=""
          class="h-12 w-12 object-contain border border-gray-200 rounded-md bg-white"
          @error="previewFailed = true"
        />
        <button
          class="flex items-center gap-2 px-3 py-1 rounded-md border border-gray-300 hover:bg-gray-100 disabled:opacity-50"
          :disabled="uploading"
          @click="fileInput?.click()"
        >
          <PhUploadSimple />
          {{ uploading ? "Uploading..." : "Upload SVG" }}
        </button>
        <input
          ref="fileInput"
          type="file"
          accept=".svg,image/svg+xml"
          class="hidden"
          @change="onFilePicked"
        />
      </div>

      <p v-if="uploadError" class="text-xs text-red-600">{{ uploadError }}</p>
      <p class="text-xs text-gray-500">
        Upload an .svg (max 2 MB). It is stored on the render server so the insert can
        use it, and stays referenced in your downloaded JSON.
      </p>
    </div>

    <SizeBuilder label="Size" :size="partComponent.size" />
    <p class="text-xs text-gray-500">
      X and Y are the footprint the artwork is stretched into, so give them the artwork's
      own proportions or it comes out distorted. Z is how deep it cuts - 0.4 to 0.8 mm is
      one to four layers.
    </p>

    <SizeBuilder label="Location" :size="partComponent.location" />

    <div class="space-y-2 pt-2 border-t border-gray-200">
      <label class="block text-sm font-medium text-gray-500">Cut into</label>
      <select
        v-model="partComponent.anchor"
        class="px-2 py-1 rounded-md border border-gray-300 w-full"
      >
        <option value="bottom">Bottom face</option>
        <option value="top">Top face</option>
      </select>
      <p class="text-xs text-gray-500">
        Unlike every other component, Z is measured from this face rather than from the
        part's top rim. The bottom face is the first layers of the print, which is where a
        filament change is free. Use Location Z to lift it from there - into the floor of a
        well, for instance.
      </p>
    </div>

    <label class="flex items-center gap-2 text-sm font-medium text-gray-700">
      <input
        type="checkbox"
        v-model="partComponent.mirrored"
        class="h-4 w-4 rounded border-gray-300"
      />
      Mirror horizontally
    </label>
    <p class="text-xs text-gray-500">
      A bottom-face engraving is read through the other side of the material, so anything
      with text in it needs this.
    </p>

    <label class="flex items-center gap-2 text-sm font-medium text-gray-700">
      <input
        type="checkbox"
        v-model="partComponent.export"
        class="h-4 w-4 rounded border-gray-300"
      />
      Also print the filling
    </label>
    <p class="text-xs text-gray-500">
      Emits the same shape again under Additional features, positioned to match the recess.
      Load both STLs into the slicer as one object and give them different filaments to get
      a two-colour logo. Without it you just get the recess.
    </p>
  </div>
</template>
