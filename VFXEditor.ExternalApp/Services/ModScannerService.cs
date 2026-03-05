using VFXEditor.ExternalApp.Models;

namespace VFXEditor.ExternalApp.Services;

public sealed class ModScannerService {
    private static readonly HashSet<string> SupportedExtensions = [
        ".pap", ".avfx", ".tmb", ".eid", ".uld", ".atex", ".tex", ".atch", ".sklb", ".skp", ".shpk", ".shcd", ".mtrl", ".mdl"
    ];

    public IReadOnlyList<ModAssetEntry> Scan(string modsDirectory) {
        if(string.IsNullOrWhiteSpace(modsDirectory) || !Directory.Exists(modsDirectory)) return [];

        var files = Directory.EnumerateFiles(modsDirectory, "*", SearchOption.AllDirectories)
            .Where(path => SupportedExtensions.Contains(Path.GetExtension(path).ToLowerInvariant()))
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .Select(path => new ModAssetEntry {
                AbsolutePath = path,
                RelativePath = Path.GetRelativePath(modsDirectory, path),
                ModName = ExtractModName(modsDirectory, path),
            })
            .ToList();

        return files;
    }

    private static string ExtractModName(string modsDirectory, string filePath) {
        var relative = Path.GetRelativePath(modsDirectory, filePath);
        var firstSegment = relative.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        return string.IsNullOrWhiteSpace(firstSegment) ? "Unknown Mod" : firstSegment;
    }
}
