import { toRaw } from "vue";
import type { Part, PartComponent } from "./InsertModels";

type Component = NonNullable<PartComponent>;

/**
 * A copy that shares nothing with the original: a spread would leave the two pointing
 * at the same size, location and cutout objects, so editing one would edit both.
 */
function deepCopy<T>(value: T): T {
  return JSON.parse(JSON.stringify(toRaw(value))) as T;
}

/** A repeatable carries a component of its own, which needs its own id as well. */
function renewIds(component: Component, idMap: Map<string, string>) {
  const id = crypto.randomUUID();
  idMap.set(component.id, id);
  component.id = id;

  if (component.$type === "repeatable" && component.model) renewIds(component.model, idMap);
}

/**
 * Points a clone's componentAlign locations at the copies rather than the originals.
 * Anything aligned to a component outside the copied set is left alone, since that
 * component still exists.
 */
function remapAlignments(component: Component, idMap: Map<string, string>) {
  for (const axis of ["x", "y", "z"] as const) {
    const dimension = component.location[axis];
    if (dimension.$type !== "componentAlign") continue;

    const replacement = idMap.get(dimension.componentId);
    if (replacement) dimension.componentId = replacement;
  }

  if (component.$type === "repeatable" && component.model) remapAlignments(component.model, idMap);
}

/** A standalone copy of one component, for the clone button in a part's component list. */
export function cloneComponent(component: Component): Component {
  const clone = deepCopy(component);
  clone.name = `${component.name} (copy)`;
  renewIds(clone, new Map());
  return clone;
}

/**
 * A standalone copy of a part, components and all. The part keeps its own location, so
 * a cloned part lands on top of the original until it is moved - visible in the render,
 * where two parts overlapping is easier to notice than a copy hidden somewhere else.
 */
export function clonePart(part: NonNullable<Part>): NonNullable<Part> {
  const clone = deepCopy(part);
  clone.id = crypto.randomUUID();
  clone.name = `${part.name} (copy)`;

  if (clone.$type === "box") {
    const idMap = new Map<string, string>();
    for (const component of clone.components) {
      if (component) renewIds(component, idMap);
    }
    for (const component of clone.components) {
      if (component) remapAlignments(component, idMap);
    }
  }
  return clone;
}
