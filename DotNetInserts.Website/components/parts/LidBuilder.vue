<script setup lang="ts">
import type { BoxLid, SideName } from "~/services/InsertModels";
import NumberInput from "~/components/shared/NumberInput.vue";

const props = defineProps<{
  lid: BoxLid;
}>();

const sides: { value: SideName; label: string }[] = [
  { value: "bottom", label: "Front" },
  { value: "top", label: "Back" },
  { value: "left", label: "Left" },
  { value: "right", label: "Right" },
];

const hasSnap = ref(props.lid.snap !== undefined);
const hasGrip = ref(props.lid.grip !== undefined);

watch(hasSnap, (on) => {
  if (on) {
    // Defaults mirror SnapFitDataModel, so the builder and the generator agree on
    // what an untouched snap means.
    props.lid.snap = {
      sides: ["left", "right"],
      armLength: 26,
      armHeight: 6,
      slotWidth: 0.6,
      detent: 0.5,
      leadInAngle: 30,
      retentionAngle: 45,
      engageClearance: 0.1,
    };
    // A snap needs the explicit fit; the original one cannot hold one.
    props.lid.skirtThickness ??= 1.6;
    props.lid.clearance ??= 0.15;
    // The two are mutually exclusive and the generator rejects the pair.
    delete props.lid.spaceUnderLid;
  } else {
    delete props.lid.snap;
  }
});

watch(hasGrip, (on) => {
  if (on) {
    props.lid.grip = { sides: ["top", "bottom"], width: 24, depth: 1.6 };
  } else {
    delete props.lid.grip;
  }
});

function toggle(list: SideName[] | undefined, side: SideName, on: boolean): SideName[] {
  const next = new Set(list ?? []);
  if (on) next.add(side);
  else next.delete(side);
  return [...next];
}
</script>

<template>
  <div class="space-y-3">
    <NumberInput label="Lid wall size" v-model.number="lid.wallSize" />
    <NumberInput label="Top thickness" v-model.number="lid.topSize" />
    <NumberInput label="Lid height" v-model.number="lid.lidBoxSize!" />

    <div class="space-y-2 pt-2 border-t border-gray-200">
      <h4 class="text-sm font-medium text-gray-700">Fit</h4>
      <p class="text-xs text-gray-500">
        Setting either of these switches the lid to the explicit fit, where the skirt
        and the clearance are independent. Left blank, the lid keeps its original
        behaviour, which ties both to the wall size.
      </p>
      <NumberInput label="Skirt thickness" v-model.number="lid.skirtThickness!" />
      <NumberInput label="Clearance per side" v-model.number="lid.clearance!" />
    </div>

    <div class="space-y-2 pt-2 border-t border-gray-200">
      <label class="flex items-center gap-2 text-sm font-medium text-gray-700">
        <input type="checkbox" v-model="hasSnap" class="h-4 w-4 rounded border-gray-300" />
        Snap closure
      </label>

      <div v-if="hasSnap && lid.snap" class="space-y-2 pl-2">
        <span class="block text-sm font-medium text-gray-500">Arms on</span>
        <div class="flex flex-wrap gap-3">
          <label v-for="side in sides" :key="side.value" class="flex items-center gap-1 text-sm">
            <input
              type="checkbox"
              class="h-4 w-4 rounded border-gray-300"
              :checked="lid.snap.sides?.includes(side.value)"
              @change="lid.snap!.sides = toggle(lid.snap!.sides, side.value, ($event.target as HTMLInputElement).checked)"
            />
            {{ side.label }}
          </label>
        </div>

        <NumberInput label="Arm length" v-model.number="lid.snap.armLength!" />
        <NumberInput label="Arm height" v-model.number="lid.snap.armHeight!" />
        <NumberInput label="Slot width" v-model.number="lid.snap.slotWidth!" />
        <p class="text-xs text-gray-500">
          0 gives the breakaway build: the slot fuses while printing and the arm is
          cracked free by hand. Tuned to one layer height. 0.4 and up prints as a real
          gap on any printer.
        </p>

        <NumberInput label="Detent" v-model.number="lid.snap.detent!" />
        <NumberInput label="Lead-in angle" v-model.number="lid.snap.leadInAngle!" />
        <NumberInput label="Retention angle" v-model.number="lid.snap.retentionAngle!" />
        <p class="text-xs text-gray-500">
          Both angles are degrees off vertical and cannot exceed 45. One face
          overhangs on the lid and the other on the box, so past 45 one of the two
          parts needs support material.
        </p>
      </div>
    </div>

    <div class="space-y-2 pt-2 border-t border-gray-200">
      <label class="flex items-center gap-2 text-sm font-medium text-gray-700">
        <input type="checkbox" v-model="hasGrip" class="h-4 w-4 rounded border-gray-300" />
        Thumb grip
      </label>

      <div v-if="hasGrip && lid.grip" class="space-y-2 pl-2">
        <p class="text-xs text-gray-500">
          A dish in the box wall under the lid edge. Put it on the walls without arms,
          or a snapped lid is hard to get off.
        </p>
        <div class="flex flex-wrap gap-3">
          <label v-for="side in sides" :key="side.value" class="flex items-center gap-1 text-sm">
            <input
              type="checkbox"
              class="h-4 w-4 rounded border-gray-300"
              :checked="lid.grip.sides?.includes(side.value)"
              @change="lid.grip!.sides = toggle(lid.grip!.sides, side.value, ($event.target as HTMLInputElement).checked)"
            />
            {{ side.label }}
          </label>
        </div>
        <NumberInput label="Width" v-model.number="lid.grip.width!" />
        <NumberInput label="Depth" v-model.number="lid.grip.depth!" />
      </div>
    </div>
  </div>
</template>
