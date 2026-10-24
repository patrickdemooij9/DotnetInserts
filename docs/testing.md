# Testing inserts

The problem this solves: you write insert JSON by hand, and there was no way to know
whether a change to the C# generator quietly broke one of the games you already printed.

The answer here is a **corpus plus snapshots**. Every insert definition worth protecting
lives in `DotNetInserts.Tests/Fixtures/`, and the committed output for each one lives in
`DotNetInserts.Tests/Snapshots/`. A change that alters geometry shows up as a diff in a
file you can read, before it shows up as a wasted print.

```bash
dotnet test DotNetInserts.Tests
```

Roughly 2 seconds, no OpenSCAD needed.

## Adding a game to the corpus

Copy the JSON into `DotNetInserts.Tests/Fixtures/Games/`, then:

```bash
UPDATE_SNAPSHOTS=1 dotnet test DotNetInserts.Tests
```

That writes the snapshots. **Read them before committing** — `git diff` on the report is
how you confirm the generator understood the file the way you meant it. Nothing else is
needed: the fixture folder is scanned at runtime, so a new file is picked up by every
suite automatically.

Do this whenever you finish a design you care about. The corpus is the whole safety net;
a game that is not in it is a game that can break silently.

## What runs

| Suite | Catches |
|---|---|
| `DimensionTests` | the dimension system in isolation — `fixed`, `constant`, `computed`, `parentAlign`, `partAlign`, `componentAlign`, and the error messages for a bad `#ref` |
| `LoadTests` | a file that no longer parses or maps at all, and parts that render to nothing |
| `GeometryInvariantTests` | parts clashing, parts outside the declared box, duplicate part names |
| `SnapshotTests` | any change to the generated geometry, per fixture |
| `PortabilityTests` | expressions evaluating differently under a different machine locale |
| `CutoutTests` | cutout modes, shapes, idempotent rendering, legacy/modern equivalence |
| `RenderSmokeTests` | SCAD that OpenSCAD refuses to render (opt-in, see below) |

## When a snapshot fails

That is the system working. Read the diff it prints, then either fix the regression, or
accept it:

```bash
UPDATE_SNAPSHOTS=1 dotnet test DotNetInserts.Tests
git diff DotNetInserts.Tests/Snapshots
```

Two snapshots exist per fixture and they answer different questions:

- `*.report.txt` — each part's world position and bounding box, plus any issues. Small,
  readable, and where you look first. A wall that moved shows up here as a changed number.
- `*.scad` — the full generated OpenSCAD. Noisy, but it is the only thing that catches a
  change *inside* a part (a fillet, a cutout profile) that leaves the bounding box alone.

## Invariants that fail on their own

`GeometryInvariantTests` does not need a snapshot to know something is wrong:

- **Clashing parts.** Two parts partially interpenetrating is a layout bug. A part that
  sits *entirely* inside another is reported as `Nested` and allowed — that is how a token
  tray drops into a deeper well. If a clash is deliberate, add the fixture to
  `KnownOverlaps` with a reason.
- **Parts outside the declared box.** Only checked on axes where the top-level `size` is
  actually set; several older files leave it at `0`, which is treated as "not declared".
  Setting a real `size` gets you this check.
- **Duplicate part names.** Expressions resolve `#Name.X` to the first match, so a repeated
  name silently points at the wrong part.

## Render smoke test (opt-in)

Everything above works on the object graph, so none of it can catch SCAD that OpenSCAD
itself rejects. This one actually renders:

```bash
RENDER_TESTS=1 dotnet test DotNetInserts.Tests --filter RenderSmokeTests
```

Set `OPENSCAD_PATH` if OpenSCAD is not in `C:\Program Files\OpenSCAD\`. It renders two
fixtures and asserts a non-empty, 2-manifold STL. Worth running before a print session,
not on every edit.

## Checking files that are not in the corpus

For a working file you have not committed as a fixture, the preview tool applies the same
analysis and prints the same report:

```bash
dotnet run --project tools/insert-preview -- "path/to/insert.json" --no-render
```

It also takes a directory, and `--strict` makes it exit non-zero when anything is wrong —
which is what you want if you ever wire this into a pre-commit hook or CI:

```bash
dotnet run --project tools/insert-preview -- "C:/Users/user/Desktop/Projecten/3D Projects" --strict --no-render
```

Drop `--no-render` to also get a `<name>.preview.png` to look at. The report cannot tell
you whether a scoop actually reaches the cards — for that you still have to look at the
image.

## Verifying a refactor is geometry-neutral

The snapshots are what let you rewrite the generator with confidence. The cutout rewrite
was done this way and it is the pattern to repeat:

1. Run the suite, so `Snapshots/` is current, and copy it somewhere outside the repo.
2. Make the change.
3. `UPDATE_SNAPSHOTS=1 dotnet test DotNetInserts.Tests`, then `diff -r` the copy against
   the regenerated folder.

An empty diff means every fixture — including all six real games — produces byte-identical
OpenSCAD, which also means the STL render cache in `Insert.RenderStl` stays valid. If you
intended a geometry change, the diff shows exactly which parts moved and by how much.

### When the diff is not empty

A text diff tells you *something* moved, not *what*. On a cutout change that touched
13 fixtures, 7 of them were byte-different and geometrically identical — the same solid
emitted as different OpenSCAD. Three things separate signal from churn:

- **Compare mesh volumes, not text.** Render both snapshot `.scad` files to STL and
  integrate volume over the triangles (divergence theorem). Equal volume plus equal
  bounding box is strong evidence the solid is unchanged; a real change shows up as a
  number you can sanity-check against the mm you expected to remove.
- **Check the sign across the whole corpus.** A fix usually has a known direction — "this
  should remove more material". One fixture moving the *wrong* way, or moving implausibly
  far, is the bug. A bad cutout fix passed a four-wall spot check and looked perfect; what
  exposed it was Skyteam moving +29 mm³ when every other fixture moved negative, and
  Deckers moving 68,805 mm³ when the wall slivers at stake were worth a few hundred.
- **Isolate before accepting.** With more than one change in the tree the deltas are
  unattributable. Temporarily revert *only* the change under test, regenerate snapshots to
  a temp directory, and diff volumes against the current ones. That is what turned an
  incoherent table into "these six moved, all the same way, for one reason".

### Careful: `Bounds()` is not reliable for composite shapes

This fork of OSCADSharp reports a `Difference`'s bounds as its **first child's**, and a
`Union` containing one inherits the error — `ScoopCutout` is exactly that shape.
`PositionalHelper.CalculatePositionOffset` anchors cutouts on the bounds of the *rendered*
shape, so **any change that alters a shape's reported bounding box silently moves every
cutout using it**.

This has bitten once already. Orienting cutouts by rotating them per side (0/90/180/270)
in `BoxComponent` looked correct on a four-sided test, but changing the rotation angle
changed the bogus reported bounds by a different amount and slid Deckers' 400 mm scoop
60 mm along its wall. The fix that worked instead flips the profile *inside* the shape
(`ICutoutShape.Render(height, mirrorDepth)`), which leaves the bounding box identical and
therefore cannot move anything.

The rule: when a shape needs reorienting, do it inside the shape and keep its bounding box
invariant. Never transform it in the placer.

Two tests in `CutoutTests` do the same job permanently:

- `Legacy_and_modern_definitions_generate_identical_scad` — `Features/Cutouts.json`
  (legacy) and `Features/CutoutsModern.json` (current) are the same tray written two ways.
- `A_file_migrated_by_the_builder_generates_identical_scad` —
  `Games/AgentAvenueMigrated.json` is the real AgentAvenue file put through the website
  builder's `migrateInsert` (`DotNetInserts.Website/services/CutoutMigration.ts`). This is
  what stops "open an old insert in the builder and save it" from quietly reprinting
  differently. To regenerate it after changing the migration, run the migration over
  `Fixtures/Games/AgentAvenue.json` and overwrite the file.

## Expression gotchas the tests pin down

- **The decimal separator inside `expression` strings is a comma** (`68+1,2`), and this is
  now enforced explicitly rather than inherited from the machine's locale. Before that,
  every insert file only parsed on a comma-locale machine and would have failed on en-US
  or in a container — see `ComputedDimension.DecimalSeparator` and `PortabilityTests`.
- **Integer division truncates.** `100/8` is `12`, not `12.5`, and it fails silently.
  Write `100/8,0` whenever the result can be fractional. This bites on tiling maths like
  `#Tray.X/3`.
