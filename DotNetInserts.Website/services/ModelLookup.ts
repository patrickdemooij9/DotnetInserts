import { toRaw } from "vue";
import type {
  BoxPart,
  DotNetInsertsRoot,
  Part,
  PartComponent,
  Size3D,
} from "./InsertModels";

export interface LocationOwner {
  part: BoxPart;
  component: NonNullable<PartComponent>;
}

/**
 * The component whose `location` is `size`, and the part it sits in. The size editors
 * are handed only the dimension object, so this is how one of them learns which
 * component it is placing and which siblings it could be placed against.
 */
export function findLocationOwner(
  root: DotNetInsertsRoot,
  size: Size3D
): LocationOwner | null {
  const target = toRaw(size);
  for (const part of root.parts ?? []) {
    if (part?.$type !== "box") continue;
    for (const component of part.components) {
      if (component && toRaw(component.location) === target) {
        return { part, component };
      }
    }
  }
  return null;
}

/** The part whose `location` is `size`, for the same reason as above. */
export function findPartLocationOwner(
  root: DotNetInsertsRoot,
  size: Size3D
): Part | null {
  const target = toRaw(size);
  return (root.parts ?? []).find((part) => part && toRaw(part.location) === target) ?? null;
}

export interface ReferenceSuggestion {
  /** What goes into the expression, e.g. `#Tray.X` or `#{Card tray.Well.X}`. */
  insert: string;
  /** The dotted path without decoration, for matching against what was typed. */
  path: string;
  detail: string;
}

// The backend accepts a bare `#Name.X` only for word characters; anything else has to
// be braced. A `.` or `}` in a name cannot be referenced at all, since `.` separates
// the path and `}` would end the brace early.
const bareName = /^\w+$/;
const unreferenceable = /[.}]/;

function reference(path: string[]): string {
  const joined = path.join(".");
  return path.every((p) => bareName.test(p)) ? `#${joined}` : `#{${joined}}`;
}

/** Everything a computed expression can reference with `#`. */
export function referenceSuggestions(root: DotNetInsertsRoot): ReferenceSuggestion[] {
  const suggestions: ReferenceSuggestion[] = [];
  const axes = ["X", "Y", "Z"];

  for (const part of root.parts ?? []) {
    if (!part?.name || unreferenceable.test(part.name)) continue;

    for (const axis of axes) {
      const path = [part.name, axis];
      suggestions.push({ insert: reference(path), path: path.join("."), detail: "Part size" });
    }

    if (part.$type !== "box") continue;
    const seen = new Set<string>();
    for (const component of part.components) {
      if (!component?.name || unreferenceable.test(component.name)) continue;
      // Only the first of two same-named components is reachable, as on the server.
      if (seen.has(component.name)) continue;
      seen.add(component.name);

      for (const axis of axes) {
        const path = [part.name, component.name, axis];
        suggestions.push({
          insert: reference(path),
          path: path.join("."),
          detail: "Component size",
        });
      }
    }
  }
  return suggestions;
}
