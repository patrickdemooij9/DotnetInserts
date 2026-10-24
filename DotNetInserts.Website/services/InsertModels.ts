// Common types for Size and Location
export interface FixedSize {
  $type: "fixed";
  size: number;
}
export interface ConstantSize {
  $type: "constant";
  constantType: number;
}
export interface ParentAlignSize {
  $type: "parentAlign";
  alignType: number;
  gap: number;
}
/**
 * Places a part beside another part of the insert. Parts sit on their minimum corner,
 * so on the axis the direction points along this butts against that side of the target
 * `gap` apart; on any other axis it takes the target's coordinate, lining the two
 * parts' lower edges up. Only valid in a part's `location`.
 */
export interface PartAlignSize {
  $type: "partAlign";
  partId: string;
  alignType: number;
  gap: number;
}
export interface ComputedSize {
  $type: "computed";
  expression: string;
}
/**
 * Places a component beside a sibling component in the same part. On the axis the
 * direction points along (x for Right/Left, y for Top/Bottom) it butts against that
 * side of the target, `gap` apart; on any other axis it takes the target's coordinate.
 * Only valid in a component's `location`.
 */
export interface ComponentAlignSize {
  $type: "componentAlign";
  componentId: string;
  alignType: number;
  gap: number;
}
export type SizeAxis =
  | FixedSize
  | ConstantSize
  | ParentAlignSize
  | PartAlignSize
  | ComputedSize
  | ComponentAlignSize;

export interface Size3D {
  x: SizeAxis;
  y: SizeAxis;
  z: SizeAxis;
}

export interface Location3D {
  x: SizeAxis;
  y: SizeAxis;
  z: SizeAxis;
}

// Cutout types
//
// A cutout carries its own placement: which wall (`side`) and how deep (`mode`).
// `mode: "through"` cuts the full component height and breaks through the floor -
// the finger scoop that actually lets you get components out. `mode: "notch"`
// bites down from the top rim by `size.z` and leaves the floor intact.
export type CutoutSide = "top" | "right" | "bottom" | "left";
export type CutoutMode = "notch" | "through";
export type CutoutType = "cube" | "scoop" | "circle";

export interface CutoutBase {
  side: CutoutSide;
  mode: CutoutMode;
  /** x runs along the wall, y is depth into it, z is the cut height. */
  size: Size3D;
  /** Optional nudge from the automatically centred position. */
  offset?: Size3D;
}

export interface CubeCutout extends CutoutBase {
  $type: "cube";
  /** Omitted for square corners. */
  roundRadius?: number;
}
export interface ScoopCutout extends CutoutBase {
  $type: "scoop";
  curveSize?: number;
}
export interface CircleCutout extends CutoutBase {
  $type: "circle";
}
export type Cutout = CubeCutout | ScoopCutout | CircleCutout;

// Pre-`cutouts` schema. Only ever read, and only to migrate an imported file -
// see migrateInsert in ~/services/CutoutMigration. Never written back out.
export interface LegacyCubeCutout {
  $type: "cube";
  size: { X: number; Y: number; Z: number };
  position?: Size3D;
  roundRadius?: number | null;
}
export interface LegacyScoopCutout {
  $type: "horizontal";
  size: { X: number; Y: number; Z: number };
  position?: Size3D;
  curveSize?: number;
}
export type LegacyCutout = LegacyCubeCutout | LegacyScoopCutout | null;

export interface LegacyCutouts {
  top?: LegacyCutout;
  right?: LegacyCutout;
  bottom?: LegacyCutout;
  left?: LegacyCutout;
}

// Component types
export interface BoxComponent {
  $type: "box";
  id: string;
  name: string;
  size: Size3D;
  location: Location3D;
  cutouts?: Cutout[];
  roundBottom?: boolean;
  /** @deprecated Legacy input only; migrated to `cutouts` on import. */
  horizontalCutouts?: LegacyCutouts | null;
  /** @deprecated Legacy input only; migrated to `cutouts` on import. */
  verticalCutouts?: LegacyCutouts | null;
}
export interface RepeatableComponent {
  $type: "repeatable";
  id: string;
  name: string;
  amountX: number;
  amountY: number;
  model: BoxComponent | RepeatableComponent;
  size: Size3D;
  location: Location3D;
}
export interface CircleComponent {
  $type: "circle";
  id: string;
  name: string;
  size: Size3D;
  location: Location3D;
}
export interface CardTrayComponent {
  $type: "cardTray";
  id: string;
  name: string;
  size: Size3D;
  location: Location3D;
  dividerSize: { X: number; Y: number; Z: number };
  dividerWallSize: number;
  dividerBottomCutoutSize: { X: number; Y: number; Z: number };
  numberOfSlots: number;
}
/**
 * Vector artwork engraved into a face of the part. Unlike every other component it is
 * measured from the part's bottom face, and with `export` it also emits the body that
 * fills the recess, for a two-colour print.
 */
export interface SvgComponent {
  $type: "svg";
  id: string;
  name: string;
  size: Size3D;
  location: Location3D;
  /** Passed to OpenSCAD's import(); resolved where the render runs, not next to the JSON. */
  imagePath: string;
  anchor?: "bottom" | "top";
  mirrored?: boolean;
  export?: boolean;
}
export type PartComponent =
  | BoxComponent
  | RepeatableComponent
  | CircleComponent
  | SvgComponent
  | null;

// Which wall a label or cutout sits on. "bottom" is the front face.
export type SideName = "top" | "right" | "bottom" | "left";

/**
 * Text engraved into one face of a part. Plain numbers rather than dimension
 * objects: these are small literals nudging a label on a face, never relational
 * the way a size or location is.
 *
 * Omitting `side` engraves the floor, which is what the older cardHolder `text`
 * property has always done.
 */
export interface Label {
  text: string;
  side?: SideName;
  size?: number;
  depth?: number;
  along?: number;
  up?: number;
}

// Part types
export interface BoxPart {
  $type: "box";
  id: string;
  name: string;
  size: Size3D;
  components: PartComponent[];
  location: Location3D;
  roundRadius?: number | null;
  labels?: Label[];
  lid?: BoxLid;
  hidden?: boolean;
}
export interface CardHolderPart {
  $type: "cardHolder";
  id: string;
  name: string;
  size: Size3D;
  innerSize: Size3D;
  cutoutSide?: number;
  roundedCorners?: boolean;
  text?: string | null;
  textSize?: number;
  labels?: Label[];
  stackableBottom?: boolean;
  stackableTop?: boolean;
  location: Location3D;
  hidden?: boolean;
}
export type Part = BoxPart | CardHolderPart | null;

/**
 * A cantilever snap closure on a lid: a flexible tongue in the skirt with a bump
 * that clicks into a pocket in the box.
 *
 * `slotWidth: 0` is the breakaway build - a slot too narrow for the slicer to fit a
 * perimeter into, so it fuses and gets cracked free by hand before first use.
 */
export interface SnapFit {
  sides?: SideName[];
  armLength?: number;
  armHeight?: number;
  slotWidth?: number;
  detent?: number;
  leadInAngle?: number;
  retentionAngle?: number;
  engageClearance?: number;
}

/** A thumb dish in the box wall under the lid edge, so a snapped lid can be opened. */
export interface LidGrip {
  sides?: SideName[];
  width?: number;
  depth?: number;
}

export interface BoxLid {
  wallSize: number;
  topSize: number;
  lidBoxSize?: number;
  /** Superseded by `grip`. Cannot be combined with `snap`. */
  spaceUnderLid?: number;
  imagePath?: string;
  /**
   * Naming any of these switches the lid to the explicit fit, where the skirt
   * thickness and the clearance are independent. Without them the original
   * behaviour is kept, which ties both to wallSize and cannot hold a snap.
   */
  skirtThickness?: number;
  clearance?: number;
  minWall?: number;
  snap?: SnapFit;
  grip?: LidGrip;
}

// Root object
export interface DotNetInsertsRoot {
  name: string;
  size: { x: number; y: number; z: number };
  parts?: Part[];
}