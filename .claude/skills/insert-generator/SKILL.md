---
name: insert-generator
description: >-
  Generate a board-game organizer/insert definition as JSON for this project's
  parametric insert builder (the DotNetInserts / JsonInsertLoader schema). Use
  this whenever the user wants to create, draft, or lay out an insert, organizer,
  tray, or set of compartments for a board game — e.g. "make an insert for
  Wingspan", "design a tray for these tokens", "generate the JSON for a card
  box", "organize this game's components", or when they give box dimensions plus
  a list of components (cards, tokens, standees, dice, meeples) and want them
  packed into printable trays. Trigger even if they don't say the word "JSON" or
  "insert" explicitly but are clearly describing organizing game components into
  a box.
---

# Insert Generator

Generate a valid insert-definition JSON for this project's parametric builder,
from a box size plus a list of physical components. The output is one JSON file
matching the `JsonInsertLoader` schema, ready to load in the builder.

## The schema is documented separately — read it first

The complete schema (all `$type`s, the dimension system, cutouts, conventions,
and the comma-decimal expression quirk) lives in
[docs/ai-insert-prompt.md](../../../docs/ai-insert-prompt.md). **Read that file
before generating** — it is the source of truth and it changes when the C# models
in `DotNetInserts.Core/JsonLoader` change. This SKILL.md only covers the
in-repo workflow around it.

If anything in the schema doc looks stale versus the code, trust
`DotNetInserts.Core/JsonLoader/Models` (the `[JsonDerivedType]` discriminators on
`IPart`, `IComponentModel`, `ICutoutModel`, `IDimension`) and mention the
discrepancy to the user.

## Layout principles

Apply these before reaching for the schema — they are what makes an insert pleasant
to use rather than merely correct.

- **Tokens go in big open bins, not per-token wells.** Roughly 90% of the time the
  player wants to tip a handful of tokens in and grab them back out. A tight well
  per token type looks tidy in the render and is slow at the table. Group tokens by
  when they are used (all the action tokens together, all the health together) and
  give each group a bin far larger than the tokens need. Reserve a snug pocket for
  the cases that genuinely earn it: a single unique piece that would otherwise get
  lost, or a component whose orientation matters. Give every bin
  `roundBottom: true` — it turns the floor into a trough (on a 124 mm bin, 12 mm
  deep at the edges falling to 1 mm at the centre) so the tokens gather in the
  middle by themselves.
- **One scoop per compartment is the default.** Put it on the wall the user will
  actually reach over — usually `bottom` (the front). Add a second only when there
  is a specific reason, e.g. the compartment is reached from two sides in play.
  Copying a two-scoop pattern out of a reference file without that reason is a
  mistake that has been made before.
- **Every pocket a hand has to reach into needs a scoop**, miniature pockets
  included. A 30 mm base at the bottom of a 50 mm hole cannot be pinched out. Lay
  the grid out so each pocket touches an *outer* wall, then scoop that wall — a
  scoop through an internal divider just joins two pockets together.
  The exception is a round-bottomed dump bin: the trough already delivers the
  contents to your fingers, so `roundBottom` and `cutouts` are alternatives there,
  not companions. Don't put both on a bin.
- **`roundRadius` no longer narrows the opening.** A cube cutout is square across the
  face that meets the wall and rounded only at the far end, so the clear width at the
  wall is the full `size.x` and the cut reaches exactly `size.y` inward, whichever
  side it is on. Keep `roundRadius` at or below `min(size.x, size.y)` — above that the
  corner circles escape the declared box and the cut comes out oversized.
- **Cards on edge beat cards flat** whenever the box is tall enough for the card's
  short dimension. It converts a large footprint into a small one and makes every
  deck reachable from the top. Check it explicitly: a 68 mm card needs 68 mm of
  interior height plus a floor, so it decides the whole insert height.
- **Oversized cards and boards are the top layer.** Commercial inserts (Folded
  Space and friends) lay villain dashboards and location sheets flat on top of the
  trays below, then the rulebook. Don't try to stand them up or give them a tray of
  their own footprint.
- **Finish level with the box, by default.** Assume a rulebook has to lie flat on
  top; you need a reason *not* to fill the height. Two things this is easy to get
  half-right. The invariant is that every **stack** reaches the box height, not that
  every part shares one `size.z` — a 61 mm tray with an 8 mm plate on it is level
  with a 69 mm tray. And reaching the height is not enough on its own: the top plane
  has to be **fully covered**, or the uncovered strips are just a lower shelf. A
  206 mm plate centred in a 250 mm box tops out correctly and still leaves 22 mm of
  bare rim down each side; two 125 mm plates, wells offset flush to the shared seam
  so they join into one well, cover it and print smaller.
- **Filament is a design axis, and the lever is cavity depth.** Material hides in
  floors, not in walls. Write every cavity depth as `#Part.Z - <floor>` rather than a
  literal, so each floor is auditable at a glance and stays correct when the part
  height moves. Then check them: on the Marvel United insert a single tray left with
  a 9 mm floor instead of 1 mm was **16% of the whole model**, and it was invisible
  until the parts were weighed individually (see the volume check in step 4).

## Workflow

### 1. Gather inputs — never guess measurements

You need, at minimum:
- The box's **internal** size in mm (`x` × `y` × `z`).
- Every component with its **physical mm size and count** — the user supplies
  these (per the project convention, the human measures; you only lay out).
- The **per-card thickness** if any card stacks are involved (e.g. `0,65` mm
  sleeved in the reference files).

If a measurement you need is missing, **stop and ask**. A plausible-looking wrong
number produces a tray that doesn't fit — worse than a question.

### 2. Plan the layout in prose, with fit arithmetic

Before writing any JSON, sketch it in words: which parts, their footprints, how
they tile in x, what rows sit at negative y, what stacks in z. Show the sums:
part widths along x ≤ box `x`; depths ≤ box `y`; stack heights ≤ box `z`.

This step is the single biggest lever on correctness — it's a packing problem,
and reasoning first prevents the overlaps and out-of-bounds parts that are easy
to emit blind. Show this plan to the user before committing to JSON for anything
non-trivial.

### 3. Write the JSON to a file

Follow the schema doc exactly. Prefer `computed` expressions with `#PartName.X`
references over hard-coded absolute positions for anything relational (walls,
tiling, "same height as") — it makes the result editable and self-consistent,
matching how the existing hand-made files are built.

Ask the user where to save it, or default next to the game's other files (the
existing pattern is a per-game folder under `3D Projects/`, e.g.
`3D Projects/Deckers/Deckers (33).json`). Don't overwrite an existing numbered
file — pick the next free number.

### 4. Validate with the preview tool

There is a committed tool for this — **do not hand-roll a console**. Run:

```
dotnet run --project tools/insert-preview -- "<path-to-insert.json>"
```

It loads the JSON through the real pipeline (case-insensitive, like the API) and:

- **Prints a fit/overlap report** (text — cheap): each part's corner + size, then
  an `issues:` list. `[OutsideBox]` means a part exceeds the declared box (only
  checked on axes where the top-level `size` is non-zero); `[Overlap]` means two
  parts partially interpenetrate, which is a real bug; `[Nested]` means one part
  sits entirely inside another, which is normal for stacked trays. A `LOAD FAILED`
  line means a bad expression, an unresolved `#ref`, or an infinite alignment
  loop — the message says which.
- **Renders one small iso PNG** next to the JSON (`<name>.preview.png`), unless
  you pass `--no-render`.

It also accepts a directory (checks every `.json` under it) and `--strict`
(exit 1 when anything is wrong). The analysis comes from
`DotNetInserts.Core/Validation`, the same code the test suite uses — see
[docs/testing.md](../../../docs/testing.md).

Use it in two tiers to keep cost down:

- **Simple inserts / quick check:** run with `--no-render` and trust the text
  report. If it says `issues: none`, the geometry fits and nothing overlaps — no
  image read needed.
- **Scoops, tight fits, or mixed orientations:** the report can't see these, but
  **do not judge them from the PNG either** — measure them. See below. The render
  is still worth a look for gross layout sanity ("is that tray where I meant it"),
  just never for a dimension. Batch several edits before each render to minimise
  render rounds, and read the image at low resolution.

Note the tool writes `<name>.preview.scad` and `<name>.extras.scad` **only when it
actually renders** — `--no-render` skips them, so ask for the render when you want
the SCAD to measure.

If the tool ever fails to build, it is because the `OSCADSharp` HintPath or the
`DotNetInserts.Core` reference moved — fix the paths in
`tools/insert-preview/insert-preview.csproj`; don't fall back to a scratch console.

#### Measuring geometry — the render lies

An OpenSCAD preview shades a correct through-scoop as a thin hatched band that
reads as intact wall. Judging an opening from the image gives false negatives, and
has already sent this project chasing a bug that did not exist. When a dimension
matters, measure the solid:

- **Point-sample the STL.** Render the part's `.preview.scad` to STL
  (`openscad -o x.stl x.preview.scad`), parse the triangles, and test points by ray
  parity (cast +z, count crossings, odd = inside). Sweep along a wall at mid-height
  and print the run lengths: that gives the real opening width in mm. Sweep inward
  from a face for cut depth, or across a bin for a `roundBottom` floor profile.
- **Compare mesh volumes** (divergence theorem over the triangles) when a change
  churns snapshots. It separates "the SCAD text changed" from "the solid changed" —
  on one cutout fix, 7 of 13 changed fixtures were byte-different but geometrically
  identical. It is also how you weigh parts for the filament principle above.
- **Check the sign, across the whole corpus.** A fix usually has a known direction:
  "this should remove more material". One fixture moving the *wrong* way, or moving
  implausibly far, is the signal. A bad fix passed a four-wall spot check and looked
  perfect; the volume table is what exposed it.
- **Isolate before accepting.** With several changes in the tree the deltas are
  unattributable. Temporarily revert *only* the change under test, regenerate
  snapshots to a temp directory, and diff volumes against the current ones.

### 5. Set expectations

This is a strong first draft, not a finished part. Tell the user to load it,
look at it in the builder, and describe any geometry problem back ("Objectives
column overlaps Others", "tray is 4 mm too tall") — fixing a described error is
reliable, and the loop converges fast.

### 6. Once the design is settled, add it to the test corpus

Copy the final JSON into `DotNetInserts.Tests/Fixtures/Games/<Game>.json` and run
`UPDATE_SNAPSHOTS=1 dotnet test DotNetInserts.Tests` to record its snapshots, then
review the generated `Snapshots/Games/<Game>.report.txt` before committing.

That is what stops a later change to the C# generator from silently breaking this
insert. A design that is not in the corpus is not protected. Suggest this to the
user when a design is finished; see [docs/testing.md](../../../docs/testing.md).

## Output rules

- One self-contained JSON object matching the top-level structure in the schema
  doc. No comments, no trailing commas.
- Generate unique placeholder GUID-ish `id` strings for every part and component.
- When you present the result, give the file path and a one-paragraph summary of
  the layout you built, so the user knows what to expect before opening it.
