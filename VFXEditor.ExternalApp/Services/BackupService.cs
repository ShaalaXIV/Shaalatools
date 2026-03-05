namespace VFXEditor.ExternalApp.Services;

public sealed class BackupService {
    public string CreateBackup(string originalPath) {
        var directory = Path.GetDirectoryName(originalPath) ?? throw new InvalidOperationException("Invalid file path");
        var backupDirectory = Path.Combine(directory, "backup");
        Directory.CreateDirectory(backupDirectory);

        var fileName = Path.GetFileName(originalPath);
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var backupPath = Path.Combine(backupDirectory, $"{fileName}.{timestamp}.bak");

        File.Copy(originalPath, backupPath, overwrite: false);
        return backupPath;
    }
}
