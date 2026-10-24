import type {
  BoxComponent,
  Cutout,
  CutoutMode,
  CutoutSide,
  DotNetInsertsRoot,
  LegacyCutout,
  LegacyCutouts,
  PartComponent,
  Size3D,
} from "./InsertModels";

// Upgrades an imported insert from the old cutout schema to the current one, so the
// builder only ever has to deal with `cutouts`. Mirrors CutoutMapper on the C# side:
//
//   horizontalCutouts.<side> -> { side, mode: "through" }
//   verticalCutouts.<side>   -> { side, mode: "notch" }
//   $type "horizontal"       -> $type "scoop"
//
// Order matters - the cutouts are unioned in sequence - so it keeps the original
// traversal (through first, then notches, bottom/top/right/left within each). A
// migrated file therefore renders byte-identically to the original.

const SIDES: CutoutSide[] = ["bottom", "top", "right", "left"];

function fixed(value: number): Size3D {
  return {
    x: { $type: "fixed", size: value },
    y: { $type: "fixed", size: value },
    z: { $type: "fixed", size: value },
  };
}

function sizeFromLegacy(size: { X: number; Y: number; Z: number }): Size3D {
  return {
    x: { $type: "fixed", size: size.X },
    y: { $type: "fixed", size: size.Y },
    z: { $type: "fixed", size: size.Z },
  };
}

function convert(legacy: LegacyCutout, side: CutoutSide, mode: CutoutMode): Cutout | null {
  if (!legacy) return null;

  const base = {
    side,
    mode,
    size: sizeFromLegacy(legacy.size),
    offset: legacy.position ?? fixed(0),
  };

  if (legacy.$type === "horizontal") {
    return { $type: "scoop", ...base, curveSize: legacy.curveSize ?? 10 };
  }

  const cube: Cutout = { $type: "cube", ...base };
  // Left off entirely when absent, rather than written as null - square corners.
  if (legacy.roundRadius != null) cube.roundRadius = legacy.roundRadius;
  return cube;
}

function collect(legacy: LegacyCutouts | null | undefined, mode: CutoutMode): Cutout[] {
  if (!legacy) return [];

  const result: Cutout[] = [];
  for (const side of SIDES) {
    const converted = convert(legacy[side] ?? null, side, mode);
    if (converted) result.push(converted);
  }
  return result;
}

function migrateComponent(component: PartComponent): void {
  if (!component) return;

  if (component.$type === "repeatable") {
    migrateComponent(component.model);
    return;
  }
  if (component.$type !== "box") return;

  const box = component as BoxComponent;
  const legacy = [
    ...collect(box.horizontalCutouts, "through"),
    ...collect(box.verticalCutouts, "notch"),
  ];

  if (legacy.length > 0) {
    box.cutouts = [...(box.cutouts ?? []), ...legacy];
  }

  // Drop the old properties so they are never written back out.
  delete box.horizontalCutouts;
  delete box.verticalCutouts;
}

/** Migrates in place and returns the same object, for convenient chaining. */
export function migrateInsert(root: DotNetInsertsRoot): DotNetInsertsRoot {
  for (const part of root.parts ?? []) {
    if (part?.$type !== "box") continue;
    for (const component of part.components ?? []) {
      migrateComponent(component);
    }
  }
  return root;
}
