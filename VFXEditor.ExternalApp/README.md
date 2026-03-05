# VFXEditor External App (WinUI 3 Prototype)

This project is a standalone Windows app prototype intended to replace plugin-only workflows with a simpler desktop editing experience.

## Goals in this first pass
- Read a Penumbra mods directory directly.
- Discover editable VFX-related assets while excluding `.scd` files.
- Present files in a clean list + editor layout.
- Always create a backup before writing changes.

## Current behavior
1. Enter your Penumbra mods directory path.
2. Click **Scan** to discover editable files.
3. Select a file to load it in the editor.
4. Edit raw hex content.
5. Click **Save** to:
   - create `backup/<filename>.<timestamp>.bak` next to the source file,
   - overwrite the source using edited content.

### Included file types
- `.pap`, `.avfx`, `.tmb`, `.eid`, `.uld`, `.atex`, `.tex`, `.atch`, `.sklb`, `.skp`, `.shpk`, `.shcd`, `.mtrl`, `.mdl`

### Excluded file types
- `.scd` (intentionally skipped per requested workflow)

## Next steps to reach feature parity with plugin workflows
- Add strongly typed editors per file format instead of raw hex editing.
- Port existing VFXEditor parser/command logic into shared libraries consumable by both plugin and desktop app.
- Add guided editors (timelines, particle previews, animation event mapping) with safer validation and undo/redo.
- Add UX improvements (search/filtering, favorites, conflict warnings, diff preview, restore-from-backup flow).

## Build notes
- This app targets WinUI 3 with Windows App SDK.
- Build on Windows with .NET 8 SDK and WinUI workload.
