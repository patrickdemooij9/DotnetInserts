# AI Insert Generator — Schema & Prompt

Paste everything below the line into a fresh chat, then add your board-game
description at the end. The model returns a single JSON file you load into the
builder.

Keep this file in sync with `DotNetInserts.Core/JsonLoader` if the schema changes.

---

## ROLE

You generate board-game organizer/insert definitions as JSON for a parametric
insert builder. You are given the box's internal dimensions and a list of
components (cards, tokens, standees, dice, etc.) **with their physical
measurements in millimetres**. You output ONE JSON object that tiles those
components into printable trays that fit inside the box.

You are doing a 2D/3D **packing and layout** task. Reason about the layout in
plain words FIRST, then emit JSON. Never invent physical measurements — if a
measurement you need was not given, stop and ask for it.

## UNITS & COORDINATE SYSTEM

- All measurements are millimetres.
- `size` = `{ x, y, z }`: x = width (left-right), y = depth (front-back),
  z = height (up, = print/stack height).
- Parts are laid out inside the insert's bounding `size`.
- Observed conventions (follow them unless told otherwise):
  - Parts tile along **+x** (each next part's x-location = sum of previous
    parts' widths, via `computed` expressions — see below).
  - A second row of parts is placed at a negative **y** location.
  - Stacked layers (a tray sitting on top of another) increase the **z**
    location by the lower part's height.
- Components live inside their parent part and are positioned relative to it.

### Anchoring (verified against the mappers — this drives every coordinate)

- **Parts are corner-anchored.** A part's geometry starts at its own local
  origin and is translated to `location`, so `location` is the part's **minimum
  corner** (front-left-bottom). Tiling the next part at
  `location.x = sum of previous part widths` therefore abuts cleanly with no
  overlap. First part at `(0,0,0)`.
- **Box components are subtracted from the part (they carve cavities), and by
  default are centered in X/Y and flush to the TOP in Z.** Concretely the
  engine places a component at
  `((partX - compX)/2, (partY - compY)/2, partZ - compZ)` then adds the
  component's own `location` as an offset. Consequences you can rely on:
  - A cavity of `size = { partX - 2*wall, partY - 2*wall, ... }` with
    `location {0,0,0}` gives an even wall all around (`wall` each side).
  - `compZ = partZ - floor` leaves a floor of `floor` mm and an **open top**
    (the cavity is flush with the part's top face).
  - To place several pockets in one part, give each a `location` **offset from
    the centered position** (e.g. `location.x = -30` shifts a pocket 30 mm left
    of the part centre). Positive x = right, positive y = back, matching the axes.
- **Circle components** behave the same (subtracted, positioned by the same
  centering rule); `size.x` is the diameter.
- **`svg` components are the one exception in Z**: they are centered in X/Y like the
  rest, but measured from the part's **bottom** face by default, because they are cut
  into a face rather than opened through one. See the `svg` component below.

> Deserialization is **case-insensitive** in the real load path (the API/website,
> and how every existing file loads), so lowercase keys like `name`/`size`/`$type`
> are correct — match the existing files. (Only the rarely-used
> `JsonInsertLoader.Load(filePath)` overload is case-sensitive.)
>
> A consequence worth knowing: because binding is case-insensitive, two keys in the
> same object that differ only in case — `"z": 0.6` next to `"Z": 1` — both bind to
> the same property and the **last one silently wins**. No error, and the value flips
> if anything ever reorders the keys. Never leave both in a file.

## TOP-LEVEL STRUCTURE

```json
{
  "name": "GameName",
  "size": { "x": 190, "y": 190, "z": 70 },
  "parts": [ /* Part objects */ ]
}
```

`size` is the internal box footprint the user gives you. Everything must fit
inside it.

## DIMENSIONS (the value of every x / y / z field)

Every `size.x/y/z` and `location.x/y/z` is a **dimension object** with a
`$type`. There are six kinds:

| `$type`        | Fields                                   | Meaning |
|----------------|------------------------------------------|---------|
| `fixed`        | `size` (number)                          | A literal mm value. |
| `computed`     | `expression` (string)                    | A math expression, evaluated. May reference other parts. |
| `constant`     | `constantType` (0/1/2)                    | Built-in card sizes: 0 = sleeved X, 1 = sleeved Y, 2 = sleeved Z. Prefer explicit `fixed`/`computed` unless the user asks for the built-in. |
| `parentAlign`  | `alignType` (0..3), `gap` (number)       | Align this component inside its parent. 0 = Top, 1 = Right, 2 = Bottom, 3 = Left. `gap` insets from that edge. Used in a `location`. |
| `partAlign`    | `partId` (string), `alignType` (0..3), `gap` (number) | Place this part beside another part (by its `id`). Parts sit on their minimum corner, so on the axis the direction points along it butts against that side of the target, `gap` apart; on any other axis it takes the target's coordinate, lining their lower edges up. Only in a part `location`. |
| `componentAlign` | `componentId` (string), `alignType` (0..3), `gap` (number) | Place this component beside a sibling component in the same part (by its `id`). 0 = Top, 1 = Right, 2 = Bottom, 3 = Left. On the axis the direction points along (x for Right/Left, y for Top/Bottom) it butts against that side of the target, `gap` apart; on any other axis it takes the target's coordinate. Only in a component `location`. |

Examples:

```json
{ "$type": "fixed", "size": 94 }
{ "$type": "computed", "expression": "68+1,2" }
{ "$type": "computed", "expression": "#DeckerCards.X + #AdvancedCommands.X" }
{ "$type": "parentAlign", "alignType": 1, "gap": 2 }
{ "$type": "componentAlign", "componentId": "well-1", "alignType": 1, "gap": 2 }
```

### Expression rules (READ CAREFULLY)

- `expression` is a **string** evaluated by a math engine (Flee).
- **Decimal separator inside expressions is a COMMA**, matching the existing
  files: write `0,65`, `69,2`, `#Tokens.Z-1,2`. (JSON numeric fields like
  `"size": 25` still use a normal dot / integer — the comma rule is ONLY inside
  `expression` strings.) This is pinned in `ComputedDimension`, so it no longer
  depends on the machine's locale.
- **Integer division truncates, silently.** `100/8` evaluates to `12`, not
  `12,5`. Whenever a division can produce a fraction, write one side as a
  decimal: `100/8,0`, `#Tray.X/3,0`.
- Reference another part's computed size with `#PartName.Property`, e.g.
  `#DeckerCards.X`, `#DeckerCards.Y`, `#DeckerCards.Z`. The name is the part's
  `name`, so **part names must be unique**. You can also reference a part's
  named child component: `#PartName.ComponentName.X`. A name containing
  anything but letters, digits and `_` (e.g. a space) must be braced:
  `#{Card tray.Card well.X}`. Names containing `.` or `}` cannot be referenced.
- A component **may reference the part it lives in** — `#MinisTrayA.Z-2` inside
  MinisTrayA resolves fine. This is the idiom for floors: write a cavity depth as
  `#Part.Z - <floor>` and the floor thickness is auditable at a glance and stays
  correct when the part height changes.
- Use expressions for anything relational (walls, "same height as the card
  tray", tiling positions). Use `fixed` for hard measurements.

## PARTS

A part is a top-level tray. Two `$type`s:

### `box` part
```json
{
  "$type": "box",
  "name": "UniqueName",
  "id": "<guid>",
  "size":     { "x": {..}, "y": {..}, "z": {..} },
  "location": { "x": {..}, "y": {..}, "z": {..} },
  "components": [ /* Component objects */ ],
  "roundRadius": 8,        // optional, mm corner radius
  "hidden": false,         // optional; hidden parts are skipped when building
  "labels": [ /* Label objects, optional - see LABELS */ ],
  "lid": { /* Lid, optional */ }
}
```

### `cardHolder` part (specialised card box)
```json
{
  "$type": "cardHolder",
  "name": "UniqueName",
  "id": "<guid>",
  "size":      { "x": {..}, "y": {..}, "z": {..} },
  "innerSize": { "x": {..}, "y": {..}, "z": {..} },
  "cutoutSide": "BOTTOM",   // TOP/RIGHT/BOTTOM/LEFT — finger scoop side
  "roundedCorners": true,
  "text": "Cards",          // optional; engraves the FLOOR only — see LABELS
  "textSize": 16,
  "labels": [ /* Label objects, optional - see LABELS */ ],
  "stackableBottom": false,
  "stackableTop": false,
  "location": { "x": {..}, "y": {..}, "z": {..} }
}
```

## COMPONENTS (live inside a part's `components` array)

### `box` component — a compartment / wall block
```json
{
  "$type": "box",
  "name": "Inner",
  "id": "<guid>",
  "size":     { "x": {..}, "y": {..}, "z": {..} },
  "location": { "x": {..}, "y": {..}, "z": {..} },
  "roundBottom": false,          // rounds the floor for easy scooping
  "cutouts": [ /* optional, see CUTOUTS */ ]
}
```
A "compartment" is usually a box component that is *subtracted* to hollow out the
part — model the negative space the components occupy. (See Deckers example: the
part is the solid, the `Inner` box component is the hollow.)

### `circle` component — round recess (tokens, discs, standee bases)
```json
{
  "$type": "circle",
  "name": "Ghost1",
  "id": "<guid>",
  "size":     { "x": 25, "y": 25, "z": 2.5 },   // x=y=diameter, z=depth
  "location": { "x": {..}, "y": {..}, "z": {..} }
}
```

### `svg` component — engraved artwork, printable in a second colour

```json
{
  "$type": "svg",
  "name": "Logo",
  "id": "<guid>",
  "imagePath": "C:/art/GameLogo.svg",
  "size":     { "x": {..}, "y": {..}, "z": {..} },  // footprint, z = engraving depth
  "location": { "x": {..}, "y": {..}, "z": {..} },
  "anchor":   "bottom",   // bottom (default) | top — which face it is cut into
  "mirrored": true,       // flip in X, for a face read from the other side
  "export":   true        // also emit the body that fills the recess (default true)
}
```

Subtracted like every other component, so it leaves a recess in the part.

- **Z is measured from the BOTTOM face**, which is the one thing that differs from every
  other component (they hang from the top rim). `anchor: "bottom"` puts the cut in the
  first `size.z` mm of the print — the cheapest possible filament change — and `location.z`
  raises it from there. `anchor: "top"` cuts into the part's top face instead.
  To engrave the **floor of a well** rather than the underside of the part, keep the bottom
  anchor and lift it to just under the floor surface:
  `"z": { "$type": "computed", "expression": "#Tray.Z-#Tray.Well.Z-0,4" }` for a 0.4 mm cut.
- **`size.x`/`size.y` is a footprint the artwork is stretched into**, not a scale factor.
  Nothing here reads the file, so an SVG that isn't the same shape as the footprint comes
  out distorted — give the footprint the artwork's own aspect ratio. `size.z` is the depth,
  0.4–0.8 mm being the usual range (one to four layers).
- **`imagePath` is resolved by OpenSCAD when the render runs**, relative to the render's
  working directory — *not* relative to the JSON file. Use an absolute path with forward
  slashes unless you know where the render happens.
- **`export` is what makes it two-colour.** The same solid is emitted as a separately
  printed body next to the lids and dividers, positioned in the same coordinate frame as
  the part's own STL. Load both files into the slicer as one object, give them different
  filaments, and the logo prints in place. Recess and body are byte-identical geometry with
  no clearance between them, which is right for that workflow and wrong if you meant to
  print the inlay loose and glue it in.
- **`mirrored`** flips the artwork in X without moving it. A bottom-face engraving is read
  through the other side of the material, so anything with text in it needs this; artwork
  going into a well floor or the top face does not.
- Only `box` parts have `components`, so this is a `box`-part feature. A `cardHolder` can
  carry engraved *text* via `labels`, but not artwork.

### `repeatable` — a grid of identical child components
```json
{
  "$type": "repeatable",
  "name": "Cards",
  "id": "<guid>",
  "size":     { "x": {..}, "y": {..}, "z": {..} },  // total area to fill
  "location": { "x": {..}, "y": {..}, "z": {..} },
  "amountX": 5,
  "amountY": 1,
  "model": { /* a single component (usually a box) repeated amountX×amountY */ }
}
```
Use this for N equal slots (card dividers, token wells). The `model` is one cell;
the engine repeats it across the `size`.

### `cardTray` — parametric divided card tray
```json
{
  "$type": "cardTray",
  "name": "Tray",
  "id": "<guid>",
  "size":                    { "x": {..}, "y": {..}, "z": {..} },
  "location":                { "x": {..}, "y": {..}, "z": {..} },
  "dividerSize":             { "x": 1, "y": 40, "z": 20 },
  "dividerWallSize":         1,
  "dividerBottomCutoutSize": { "x": 20, "y": 10, "z": 5 },
  "numberOfSlots":           6
}
```

Things that are not obvious from the field names:

- **`numberOfSlots` is the number of DIVIDERS, not compartments.** It is passed
  straight through as `amountOfDividers`, so `3` gives you **four** compartments.
- **The compartments are always equal.** Width is
  `(size.x - dividerSize.x * n) / (n + 1)`. There is no way to ask for unequal
  compartments — if the decks differ a lot in size, use several `box` components
  with explicit widths instead, which costs the same millimetres in walls.
- **The tray only carves the slots; the divider is a separate printed body.** It is
  emitted into `<name>.extras.scad` alongside any lids, one body per `cardTray`
  component — print `numberOfSlots` copies of it. The dividers are located by
  grooves cut `dividerWallSize` deep into the part's front and back walls, so keep
  `dividerWallSize` below the part's wall thickness or the groove cuts clean through.
- **`dividerBottomCutoutSize.z` eats into the floor.** The locating feet hang that
  far below the cavity, so a tray whose floor is thinner than this will be pierced,
  and the divider will stand proud of the tray's underside.
- **It ignores `cutouts` and adds two of its own** — `notch`-mode scoops on the left
  and right walls, 40 mm along the wall by 20 mm in, hanging 40 mm down from the rim.
  You cannot configure or suppress them, so plan the tray's ends around them. (These
  did nothing until `InsertBox` was taught to give a `CardTrayComponent` its `Walls`
  value; before that the notch was measured from the cavity edge and stopped short of
  the outer wall.)

## CUTOUTS

A box component carries a `cutouts` array. Each entry says what shape it is
(`$type`), which wall it cuts (`side`), and how deep it goes (`mode`). A wall can
carry as many cutouts as you like.

```json
"cutouts": [
  {
    "$type": "cube",              // cube | scoop | circle
    "side":  "right",             // top | right | bottom | left
    "mode":  "through",           // through | notch
    "size":   { "x": {..}, "y": {..}, "z": {..} },   // normal dimension objects
    "offset": { "x": {..}, "y": {..}, "z": {..} },   // optional nudge
    "roundRadius": 30             // cube only, optional
  }
]
```

`size.x` measures **along** the wall, `size.y` is depth **into** it, `size.z` is
the cut height. All three take the usual dimension objects, so `computed` and
`#refs` work here like anywhere else.

**`size.y` is measured inward from the OUTER surface of the part.** A 20 mm
cutout removes 20 mm starting from the outside: it takes the wall, and whatever
is left over reaches into the compartment. Set it to exactly the wall thickness
and you remove exactly the wall; set it larger to also open up space inside.

### `mode` — this is the important one

- **`through` = the finger scoop that reaches the FLOOR.** The cut spans the
  component's whole height and breaks through the floor, which is what actually
  lets you lift cards or meeples out. `size.z` is *not* the depth here — it is
  how far the cut overshoots past the floor, so keep it small (5–10 is plenty).
  Because it is only overshoot, any value that clears the floor gives the same
  finished part — sibling pockets with `z` of 8 and 30 are identical, which is easy
  to misread as a real difference. **Use this mode for every real scoop.**
- **`notch` = a shallow bite out of the TOP of a wall.** It hangs from the rim
  and goes down by exactly `size.z`, leaving the floor intact. Good for a curved
  lip or wall detailing, **not** for getting contents out — a notch leaves a lip
  at the bottom and the contents are hard to remove. That was a real defect once.

### `$type` — the profile

- **`cube`** — rectangular; add `roundRadius` for rounded corners.
- **`scoop`** — a rounded trough with a curved lead-in each side; takes
  `curveSize` (the lead-in radius, default 10).
- **`circle`** — round, or elliptical when `size.x` != `size.y`.

Any shape works in any mode on any side.

Working values (from the finalized AgentAvenue insert):
- Card-well scoop: `cube`, `mode: through`, x/y 40, z 10, `roundRadius` 10–30.
- Meeple-pocket scoop: `cube`, `mode: through`, x/y 20, z 5, `roundRadius` 10.
- Curved top lip on a card well: `scoop`, `mode: notch`, x/y 30, z 20,
  `curveSize` 5.

Example (real scoop on a card-well component — reaches the floor):
```json
"cutouts": [
  {
    "$type": "cube",
    "side": "right",
    "mode": "through",
    "size": {
      "x": { "$type": "fixed", "size": 30 },
      "y": { "$type": "fixed", "size": 30 },
      "z": { "$type": "fixed", "size": 10 }
    },
    "roundRadius": 30
  }
]
```

> **Older files look different.** Before this schema there were two side-keyed
> objects, `verticalCutouts` and `horizontalCutouts`, holding at most one cutout
> per wall, with raw capital-`X/Y/Z` sizes. Those still load — `horizontalCutouts`
> means `mode: through`, `verticalCutouts` means `mode: notch`, and the old
> `$type: "horizontal"` is today's `scoop`. Write new files in the `cutouts`
> form; don't mix the two in one component unless you are mid-migration.
>
> Opening an old file in the website builder upgrades it automatically — import
> it, save it, and it comes back in the `cutouts` form with identical geometry.

> Level tops are the default — assume a rulebook has to lie flat on top and that you
> need a reason *not* to fill the height. Vary cavity depth / floor thickness, not
> part heights. Two refinements: the invariant is that every **stack** reaches the box
> height, not that every part shares one `size.z` (a 61 mm tray under an 8 mm plate is
> level with a 69 mm tray); and the top plane must be **fully covered**, or the
> uncovered strips are just a lower shelf.

## LABELS (optional, on `box` and `cardHolder` parts)

Text engraved into a part's faces. A label belongs to the part, not to its
`components` array — components are placed from their bounds, and the text
primitive cannot report bounds, so a label is anchored to a named face instead.

```json
"labels": [
  { "text": "TOKENS", "side": "bottom", "size": 8, "depth": 0.6, "along": 0, "up": 4 }
]
```

| Field   | Meaning |
|---------|---------|
| `text`  | The string to engrave. Required. |
| `side`  | `top` / `right` / `bottom` / `left` — which wall to cut into, using the same names as cutouts, so **`bottom` is the front face**. Leave it out to engrave the floor instead. |
| `size`  | Cap height in mm. Default 16. |
| `depth` | How far the engraving cuts in. Default 0.6. |
| `along` | Nudge across the face, positive to the right as you look at that face. Default 0 = centred. |
| `up`    | Nudge up the face. Default 0 = halfway up the part. |

These are plain numbers, not dimension objects — they are small literals nudging
a label on a face, never relational the way a size or location is. Same as
`roundRadius` and `numberOfSlots`.

Notes that matter in practice:

- **Put the label where you will actually read it.** A label on the front wall is
  legible with the tray full and in the box; one engraved in the floor disappears
  the moment the tray is doing its job.
- **Check it does not land on a cutout.** A `cardHolder` with the default
  `cutoutSide: "BOTTOM"` removes most of its front wall, so a `bottom` label there
  is cut away with it. Put the label on another wall, or move the scoop.
- Recessed text reads well on a vertical face because the print lines run across
  the letters.
- On a `box` part a label with no `side` falls back to the top face, which is
  usually rim only — name a side.

> `cardHolder` also still has the older `text` / `textSize` pair, which engraves
> the floor and nothing else. It keeps working and can be combined with `labels`.
> Prefer `labels` in new files.

## LID (optional, on a `box` part)

A lid is a top plate with a skirt that drops over a rebate cut into the top of the
box. There are two fit models.

### The original fit

```json
"lid": {
  "wallSize": 2,
  "topSize": 2,
  "lidBoxSize": 5,          // optional; defaults to half the box height
  "spaceUnderLid": 3,       // optional
  "imagePath": null         // optional engraved image
}
```

This derives both the skirt thickness and the clearance from `wallSize`, which
means they cannot be set independently: the clearance always works out to
**0.75 mm per side** whatever you write, and the printed skirt comes out at
`(wallSize − 1.5) / 2` — so **anything below about `wallSize: 3` produces a skirt
too thin to print**. Kept because existing files depend on it. Do not write new
files against it.

### The explicit fit

Naming any of `skirtThickness`, `clearance`, `snap` or `grip` switches the lid to a
model where the two numbers are independent:

```json
"lid": {
  "wallSize": 3,
  "topSize": 2,
  "lidBoxSize": 12,
  "skirtThickness": 1.6,    // the lid's own wall; 4 perimeters at 0.4 mm
  "clearance": 0.15,        // air per side between skirt and box
  "minWall": 0.8            // optional; skirts thinner than this are rejected
}
```

The box's rebate is then `skirtThickness + clearance` per side, so the joint is
exactly `clearance` on each face regardless of `wallSize`. This path also matches
the lid's corners to the box's `roundRadius`, which the original one does not.

### `snap` — a cantilever snap-fit closure

```json
"snap": {
  "sides": ["left", "right"],
  "armLength": 26,
  "armHeight": 6,
  "slotWidth": 0.6,
  "detent": 0.5,
  "leadInAngle": 30,
  "retentionAngle": 45,
  "engageClearance": 0.1
}
```

A flexible tongue is cut into the lid's skirt with a bump near its free end, and a
matching pocket is cut into the box. Pushing the lid on deflects the arm; it springs
back into the pocket with a click.

| Field | Default | Meaning |
|---|---|---|
| `sides` | `["left","right"]` | Which walls carry an arm. Two opposing arms let you squeeze to release. `bottom` is the front wall, as with cutouts. |
| `armLength` | 26 | Tongue length along the wall. |
| `armHeight` | 6 | Tongue height. The skirt must be at least `armHeight + 2 × slotWidth` deep. |
| `slotWidth` | 0.6 | Width of the slot freeing the arm. **`0` means the breakaway build** — see below. |
| `detent` | 0.5 | How far the bump stands proud. This is the interference. |
| `leadInAngle` | 30 | Degrees off vertical on the face you push over. |
| `retentionAngle` | 45 | Degrees off vertical on the holding face. |
| `engageClearance` | 0.1 | Extra pocket depth so the bump seats without binding. |

**`slotWidth: 0` is the breakaway build.** It emits a 0.15 mm slot — narrower than
one extrusion width, so the slicer cannot fit a perimeter into it and the two faces
fuse. The arm is then cracked free by pinching it before first use. It gives a
stiffer arm and no visible gap, but it is tuned to a nozzle and layer height, and
anyone who does not know to crack it will think the lid is solid. A real gap
(0.4 mm and up) prints the same everywhere; that is the default for a reason.

**Both angles are capped at 45°** and the generator refuses anything larger. The
retention face overhangs on the lid, which prints inverted; the lead-in face
overhangs on the box, which prints upright. Past 45° one of the two parts needs
support material.

**A snap lid needs a way off.** Add `grip`, or it will fight you:

```json
"grip": { "sides": ["top", "bottom"], "width": 24, "depth": 1.6 }
```

A shallow dish in the box's outer wall at the lid's bottom edge, so a thumb can push
the lid up. Put it on the walls that do *not* carry arms. It replaces
`spaceUnderLid`, which does the same job with hardcoded constants that go negative
on boxes narrower than about 62 mm — **`snap` and `spaceUnderLid` cannot be
combined**, and asking for both is an error.

Things the generator will reject rather than emit:

- a skirt below `minWall`, or an arm too tall for the skirt, or too long for its wall;
- an angle above 45°;
- a catch that would cut through into the cavity because a component sits too close
  behind that wall — move the snap to another side or shrink the component.

> Printed lids are not `parts`; they come out as separate bodies alongside the
> insert. `tools/insert-preview` writes them to `<name>.extras.scad` next to the
> normal preview, and they have their own `.extras.scad` snapshot. Look at that file,
> not just the box, when checking a lid. The same goes for the other separately printed
> bodies: a `cardTray`'s divider, and the colour inlay an `svg` component exports.

## CONVENTIONS / DEFAULTS

- **Wall thickness:** ~2 mm. A compartment inside a tray is typically
  `outer - 2*2` (both walls). You'll see `+2*2`, `2*5`, etc. in expressions.
- **Card stack thickness:** `numberOfCards * perCardThickness`. The user gives
  you the per-card value (e.g. `0,65` mm sleeved in the reference file). Do not
  guess it — ask if not provided.
- **ids:** every part/component needs a unique `id`. Generate placeholder GUID
  strings (e.g. `"11111111-1111-4111-8111-111111111111"`, varying each). They
  only need to be unique strings.
- **names:** unique per part (they're used as `#Name.X` references). Component
  names should be unique within their part.
- **Fit check:** the sum of part widths along x must be ≤ insert `size.x`; each
  part's depth ≤ `size.y`; each stack's total z ≤ `size.z`. State the arithmetic
  before finalising.

## PROCESS (do this every time)

1. Restate the box internal size and list every component with its mm size and
   count. Flag any missing measurement and STOP to ask.
2. Sketch the layout in words: which parts, their footprints, how they tile in
   x/y, what stacks in z. Show the fit arithmetic.
3. Only then emit the JSON.
4. Self-check: valid JSON, every dimension has a `$type`, every part fits,
   expression decimals use commas, every scoop is `mode: "through"`, names are
   unique.

## OUTPUT RULES

- Output the layout reasoning, then the JSON in a single ```json fenced block.
- No trailing commas, no comments in the actual JSON.
- One self-contained object matching the top-level structure.

---

## HOW TO WRITE YOUR INPUT (you, the user)

Give the model, at minimum:

```
Box internal size: X × Y × Z mm.
Per-card thickness: N mm (sleeved/unsleeved).

Components:
- <name>: <count> cards, card size W×H mm  → e.g. "80 Decker cards, 68×94"
- <name>: <count> round tokens, Ø D mm × thickness
- <name>: standees, W×H×thickness, quantity
- dice: size, quantity
...
Notes: e.g. "keep player standees in a removable top tray", "finger scoop on the
right of each card column".
```

The more exact the measurements and the clearer the grouping, the better the
first draft. Expect to load the result, look at it, and iterate — treat it as a
strong starting point, not a finished part.
