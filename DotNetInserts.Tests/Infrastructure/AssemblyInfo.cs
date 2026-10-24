using Xunit;

// The loader touches process-global state (OSCADSharp's OutputSettings, and BoxPartMapper
// writing lid_preview.scad into the working directory), so tests must not run in parallel.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
