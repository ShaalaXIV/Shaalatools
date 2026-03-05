namespace VFXEditor.ExternalApp.Models;

public sealed class ModAssetEntry {
    public required string ModName { get; init; }
    public required string AbsolutePath { get; init; }
    public required string RelativePath { get; init; }
}
