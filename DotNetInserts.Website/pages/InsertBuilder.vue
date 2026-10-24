<script setup lang="ts">
import { PhTrash, PhEye, PhEyeSlash, PhCopy } from "@phosphor-icons/vue";
import { clonePart } from "~/services/Cloning";
import { BuilderOverlayHelper } from "~/components/BuilderOverlayHelper";
import BoxPartBuilder from "~/components/parts/BoxPartBuilder.vue";
import CardHolderBuilder from "~/components/parts/CardHolderBuilder.vue";
import type { InsertRenderResult } from "~/models/InsertRenderResult";
import type ItemListConfig from "~/models/ItemListConfig";
import type ItemListItem from "~/models/ItemListItem";
import type { DotNetInsertsRoot, Part } from "~/services/InsertModels";
import { SendToServer } from "~/services/InsertService";
import ItemList from "~/components/shared/ItemList.vue";
import NumberInput from "~/components/shared/NumberInput.vue";
import TextInput from "~/components/shared/TextInput.vue";
import { useInsertStore } from "~/stores/InsertStore";

const loadedInsertResults = ref<InsertRenderResult[]>([]);
const mainFilePaths = ref<string[]>([]);
const focusedPartId = ref<string | null>(null);
const isLoadingFile = ref(false);
const errorMessage = ref<string | null>(null);

const AutosaveKey = "dotnetinserts:model";

// The server skips hidden parts, so its part indices count visible parts only.
const focusedPartIndex = computed(() => {
  if (focusedPartId.value === null) return -1;
  const visibleParts = model.parts?.filter((p) => p !== null && !p.hidden) ?? [];
  const index = visibleParts.findIndex((p) => p?.id === focusedPartId.value);
  return index < mainFilePaths.value.length ? index : -1;
});

const displayedFilePaths = computed(() => {
  if (focusedPartIndex.value < 0) return mainFilePaths.value;
  return [mainFilePaths.value[focusedPartIndex.value]];
});

/** The selected part plus the lids, inlays, etc. it generated. */
const focusedPartWithExtrasPaths = computed(() =>
  focusedPartIndex.value < 0
    ? []
    : loadedInsertResults.value
        .filter((r) => r.partIndex === focusedPartIndex.value)
        .map((r) => r.stlFilePath)
);

watch(
  () => BuilderOverlayHelper.Overlays.value.length,
  (length) => {
    if (length === 0) focusedPartId.value = null;
  }
);

const store = useInsertStore();
const model = store.model;

const visiblePartCount = computed(() => model.parts?.filter((p) => p && !p.hidden).length ?? 0);

// Work survives a refresh or a closed tab. Storage can be unavailable (private mode, full
// quota), and losing the autosave there is better than breaking the builder.
onMounted(() => {
  try {
    const saved = localStorage.getItem(AutosaveKey);
    if (saved) store.setModel(JSON.parse(saved));
  } catch (error) {
    console.warn("Could not restore the autosaved insert:", error);
  }

  watch(
    () => model,
    () => {
      try {
        localStorage.setItem(AutosaveKey, JSON.stringify(model));
      } catch {
        // See above.
      }
    },
    { deep: true }
  );
});

const listConfig = computed<ItemListConfig>(() => ({
  items:
    model.parts?.map((part) => ({
      alias: part!.id,
      label: part!.name,
      hidden: part!.hidden ?? false,
    })) ?? [],
  addItems: [
    {
      alias: "box",
      label: "Box",
    },
    {
      alias: "cardHolder",
      label: "Card holder",
    },
  ],
  newButtonLabel: "Add new part",
}));

async function renderStl() {
  if (isLoadingFile.value) return;
  if (visiblePartCount.value === 0) {
    errorMessage.value = "Add at least one visible part before rendering.";
    return;
  }

  isLoadingFile.value = true;
  errorMessage.value = null;
  try {
    const result = await SendToServer(model);
    loadedInsertResults.value = result;
    mainFilePaths.value = result.filter((r) => !r.isExtra).map((r) => r.stlFilePath);
  } catch (error) {
    errorMessage.value = `Render failed: ${error instanceof Error ? error.message : String(error)}`;
  } finally {
    isLoadingFile.value = false;
  }
}

const showExportMenu = ref(false);
const exportMenuRoot = ref<HTMLElement | null>(null);

function closeMenusOnOutsideClick(event: MouseEvent) {
  if (showExportMenu.value && !exportMenuRoot.value?.contains(event.target as Node)) {
    showExportMenu.value = false;
  }
}
onMounted(() => document.addEventListener("click", closeMenusOnOutsideClick));
onBeforeUnmount(() => document.removeEventListener("click", closeMenusOnOutsideClick));

async function exportStls(paths: string[]) {
  showExportMenu.value = false;
  errorMessage.value = null;
  try {
    for (const filePath of paths) {
      const response = await fetch(filePath);
      if (!response.ok) throw new Error(`download failed (${response.status})`);
      const blob = await response.blob();
      const url = URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.href = url;
      const fileName = new URL(filePath).pathname.split("/").pop() ?? "part.stl";
      a.download = fileName;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      URL.revokeObjectURL(url);
    }
  } catch (error) {
    errorMessage.value = `Export failed: ${error instanceof Error ? error.message : String(error)}. Try rendering again.`;
  }
}

async function downloadJson() {
  const json = JSON.stringify(model, null, 2);
  const blob = new Blob([json], { type: "application/json" });
  const url = URL.createObjectURL(blob);
  const a = document.createElement("a");
  a.href = url;
  a.download = `${model.name || "insert"}.json`;
  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
  URL.revokeObjectURL(url);
}

function looksLikeInsert(value: unknown): value is DotNetInsertsRoot {
  const v = value as DotNetInsertsRoot;
  return !!v && typeof v === "object" && Array.isArray(v.parts) && !!v.size && typeof v.size.x === "number";
}

function loadInsert(json: unknown) {
  if (!looksLikeInsert(json)) {
    throw new Error("this does not look like a DotNetInserts file (it needs a size and a list of parts)");
  }
  closeAllPanels();
  clearRender();
  store.setModel(json);
  renderStl();
}

async function importJson(event: Event) {
  const input = event.target as HTMLInputElement;
  const file = input.files?.[0];
  // Cleared so picking the same file again still fires a change event.
  input.value = "";
  if (!file) return;

  if (!confirmReplace()) return;
  try {
    loadInsert(JSON.parse(await file.text()));
  } catch (error) {
    errorMessage.value = `Could not open ${file.name}: ${error instanceof Error ? error.message : String(error)}`;
  }
}

async function loadExample() {
  if (!confirmReplace()) return;
  try {
    const response = await fetch("/examples/agent-avenue.json");
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    loadInsert(await response.json());
  } catch (error) {
    errorMessage.value = `Could not load the example: ${error instanceof Error ? error.message : String(error)}`;
  }
}

function newInsert() {
  if (!confirmReplace()) return;
  closeAllPanels();
  clearRender();
  store.reset();
}

function confirmReplace() {
  return (
    (model.parts?.length ?? 0) === 0 ||
    confirm("Replace the current insert? Download its JSON first if you want to keep it.")
  );
}

function closeAllPanels() {
  BuilderOverlayHelper.Overlays.value.splice(0);
  focusedPartId.value = null;
}

function clearRender() {
  loadedInsertResults.value = [];
  mainFilePaths.value = [];
  errorMessage.value = null;
}

function addPart(item: string) {
  if (item === "box") {
    model.parts?.push({
      $type: "box",
      name: "New Part",
      id: crypto.randomUUID(),
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
      components: [],
    });
  } else if (item === "cardHolder") {
    model.parts?.push({
      $type: "cardHolder",
      name: "New Card Holder",
      id: crypto.randomUUID(),
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
      innerSize: {
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
    });
  }
}

function removePart(part: Part) {
  const index = model.parts!.indexOf(part);
  if (index > -1) {
    model.parts!.splice(index, 1);
  }
}

function togglePartVisibility(id: string) {
  const part = model.parts?.find((p) => p?.id === id);
  if (part) {
    part.hidden = !part.hidden;
  }
}

const secondaryButton =
  "px-2 py-1 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 cursor-pointer disabled:opacity-40 disabled:cursor-not-allowed";

const components: { [key: string]: Component } = {
  box: BoxPartBuilder,
  cardHolder: CardHolderBuilder,
};

function handleListItemClick(listItem: ItemListItem) {
  const part = model.parts?.find((item) => item?.id == listItem.alias);
  if (!part) return;

  openPartPanel(part);
}

function handleListItemRemove(listItem: ItemListItem) {
  const part = model.parts?.find((item) => item?.id == listItem.alias);
  if (!part) return;
  if (!confirm(`Delete "${part.name || "this part"}"?`)) return;

  removePart(part);
}

function handleListItemDuplicate(listItem: ItemListItem) {
  const part = model.parts?.find((item) => item?.id == listItem.alias);
  if (!part) return;

  const index = model.parts!.indexOf(part);
  model.parts!.splice(index + 1, 0, clonePart(part));
}

function openPartPanel(part: Part) {
  focusedPartId.value = part!.id;
  BuilderOverlayHelper.Overlays.value.push({
    component: components[part!.$type],
    props: {
      part: part,
    },
  });
}
</script>

<template>
  <div class="flex flex-col md:flex-row h-dvh">
    <div class="md:w-1/3 md:min-w-[22rem] flex flex-col flex-1 md:flex-none md:h-full min-h-0 border-r border-gray-200">
      <header class="flex items-center justify-between gap-2 px-4 py-2 border-b border-gray-200">
        <h1 class="font-semibold text-gray-800">DotNetInserts</h1>
        <div class="flex gap-2">
          <button @click="loadExample" :class="secondaryButton">Load example</button>
          <button @click="newInsert" :class="secondaryButton">New</button>
        </div>
      </header>

      <div
        v-if="errorMessage"
        role="alert"
        class="flex items-start gap-2 mx-4 mt-2 px-3 py-2 text-sm text-red-800 bg-red-50 border border-red-200 rounded-md"
      >
        <span class="grow">{{ errorMessage }}</span>
        <button class="cursor-pointer text-red-600 hover:text-red-800" title="Dismiss" @click="errorMessage = null">✕</button>
      </div>

      <!-- The footer is a normal flex item below this rather than laid over it, so the
           scroll area ends where the footer starts however many rows the buttons wrap to. -->
      <div class="relative flex-1 min-h-0">
        <div
          class="absolute inset-0 overflow-y-auto px-4 py-2"
          v-if="BuilderOverlayHelper.Overlays.value.length === 0"
        >
          <div class="mb-4">
            <label class="block text-sm font-medium text-gray-700">Game box inner size</label>
            <div class="flex gap-2">
              <NumberInput label="X" v-model="model.size.x" />
              <NumberInput label="Y" v-model="model.size.y" />
              <NumberInput label="Z" v-model="model.size.z" />
            </div>
            <p class="mt-1 text-xs text-gray-500">
              Shown as the blue outline in the preview, so you can see whether everything fits.
            </p>
          </div>

          <TextInput class="mb-4" label="Insert name" v-model="model.name" />

          <p class="block text-sm font-medium text-gray-700 mb-2">Parts</p>
          <p v-if="!model.parts?.length" class="mb-2 text-sm text-gray-500">
            No parts yet. Add a box below, or
            <button class="text-blue-600 underline cursor-pointer" @click="loadExample">load an example</button>
            to see how a finished insert is put together.
          </p>

          <ItemList
            :config="listConfig"
            @add-item="(item) => addPart(item.alias)"
            @click-item="handleListItemClick"
          >
            <template #actions="{ item }">
              <button
                class="cursor-pointer"
                :title="item.hidden ? 'Show (include in render)' : 'Hide (skip in render)'"
                @click.stop="togglePartVisibility(item.alias)"
              >
                <PhEyeSlash v-if="item.hidden" />
                <PhEye v-else />
              </button>
              <button
                class="cursor-pointer"
                title="Duplicate"
                @click.stop="() => handleListItemDuplicate(item)"
              >
                <PhCopy />
              </button>
              <button
                class="cursor-pointer"
                title="Delete"
                @click.stop="() => handleListItemRemove(item)"
              >
                <PhTrash />
              </button>
            </template>
          </ItemList>

          <template v-if="loadedInsertResults.some((r) => r.isExtra)">
            <p class="mt-4 text-sm font-medium text-gray-700">Additional features</p>
            <p class="text-xs text-gray-500">Lids, inlays and other extra pieces. Click one to preview it.</p>
            <div class="flex flex-wrap gap-2">
              <button
                v-for="item in loadedInsertResults.filter((r) => r.isExtra)"
                :key="item.insertName"
                @click="mainFilePaths = [item.stlFilePath]"
                :class="[secondaryButton, 'mt-2']"
              >
                {{ item.insertName }}
              </button>
            </div>
          </template>
        </div>

        <BuilderOverlayContainer />
      </div>

      <div class="p-4 bg-white border-t border-gray-300 flex flex-wrap gap-2">
        <button
          @click="renderStl"
          :disabled="isLoadingFile"
          class="px-3 py-1 text-sm font-medium text-white bg-blue-600 border border-blue-600 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 cursor-pointer disabled:opacity-50 disabled:cursor-wait"
        >
          {{ isLoadingFile ? "Rendering…" : "Render" }}
        </button>
        <div ref="exportMenuRoot" class="relative inline-block">
          <button
            @click="showExportMenu = !showExportMenu"
            :disabled="mainFilePaths.length === 0"
            :title="mainFilePaths.length === 0 ? 'Render first to export STLs' : undefined"
            :class="secondaryButton"
          >
            Export STLs
          </button>
          <div
            v-if="showExportMenu"
            class="absolute bottom-full mb-1 left-0 bg-white shadow-lg border border-gray-200 rounded-md py-1 w-56 z-10"
          >
            <button
              v-if="focusedPartId !== null"
              @click="exportStls(displayedFilePaths)"
              class="block w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 text-left cursor-pointer"
            >
              Export selected part
            </button>
            <button
              v-if="focusedPartWithExtrasPaths.length > 1"
              @click="exportStls(focusedPartWithExtrasPaths)"
              class="block w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 text-left cursor-pointer"
            >
              Export selected part + features
            </button>
            <button
              @click="exportStls(mainFilePaths)"
              class="block w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 text-left cursor-pointer"
            >
              Export all parts
            </button>
          </div>
        </div>
        <button @click="downloadJson" :class="secondaryButton" title="Save your design to continue later">
          Download JSON
        </button>
        <label :class="[secondaryButton, 'inline-block']" title="Open a previously downloaded design">
          Upload JSON
          <input type="file" accept=".json,application/json" @change="importJson" class="hidden" />
        </label>
      </div>
    </div>
    <div class="order-first md:order-none h-[40dvh] md:h-full md:flex-1 min-w-0">
      <ClientOnly>
        <BuilderPanel
          :file-paths="displayedFilePaths"
          :is-loading="isLoadingFile"
          :model-size="model.size"
        />
      </ClientOnly>
    </div>
  </div>
</template>
