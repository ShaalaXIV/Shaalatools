using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VFXEditor.ExternalApp.Models;
using VFXEditor.ExternalApp.Services;

namespace VFXEditor.ExternalApp.ViewModels;

public partial class MainViewModel : ObservableObject {
    private readonly ModScannerService _modScanner = new();
    private readonly BackupService _backupService = new();
    private readonly AssetContentService _assetContentService = new();

    [ObservableProperty] private string _modsDirectory = string.Empty;
    [ObservableProperty] private string _editorContent = string.Empty;
    [ObservableProperty] private string _statusText = "Select a Penumbra mods directory and click Scan.";
    [ObservableProperty] private ModAssetEntry? _selectedEntry;

    public ObservableCollection<ModAssetEntry> Entries { get; } = [];

    partial void OnSelectedEntryChanged(ModAssetEntry? value) {
        if(value is null) return;

        try {
            EditorContent = _assetContentService.ReadAsEditableText(value.AbsolutePath);
            StatusText = $"Loaded {value.RelativePath}";
        }
        catch(Exception ex) {
            StatusText = $"Failed to load file: {ex.Message}";
        }
    }

    [RelayCommand]
    private void Scan() {
        Entries.Clear();
        foreach(var entry in _modScanner.Scan(ModsDirectory)) Entries.Add(entry);

        StatusText = Entries.Count == 0
            ? "No editable VFX assets found. Check path and try again."
            : $"Found {Entries.Count} editable assets.";
    }

    [RelayCommand]
    private void Save() {
        if(SelectedEntry is null) {
            StatusText = "Select a file before saving.";
            return;
        }

        try {
            var backupPath = _backupService.CreateBackup(SelectedEntry.AbsolutePath);
            _assetContentService.SaveFromEditableText(SelectedEntry.AbsolutePath, EditorContent);
            StatusText = $"Saved and backed up to {backupPath}";
        }
        catch(Exception ex) {
            StatusText = $"Save failed: {ex.Message}";
        }
    }
}
